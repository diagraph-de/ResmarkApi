using Diagraph.ResmarkApi;
using Diagraph.ResmarkApi.Services;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Properties;
using ResmarkPrinterGroupDemo.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResmarkPrinterGroupDemo;

public partial class GroupMainForm : CustomMaterialRoundedForm
{
    private readonly PrinterGroupManager groupManager = new();
    private readonly ResmarkPrinterService printerService = new(new ClientService());
    private readonly DataTable statusTable = new();

    public GroupMainForm()
    {
        Width = 1200;
        Height = 800;
        InitializeComponent(); // call designer code
        InitLanguage();
        LoadMessages();
        InitStatusTable();
        RestoreSettings();
    }

    private void RestoreSettings()
    {
        var saved = Settings.Default.SavedPrinters;

        if (saved != null)
            foreach (var printerEntry in saved)
            {
                listPrinters.Items.Add(printerEntry);

                var parts = printerEntry.Split(" - ");
                if (parts.Length == 2)
                {
                    var printer = parts[0].Trim() + " - " + parts[1].Trim();
                    groupManager.AddPrinter(printer);
                }
            }
    }

    private void InitLanguage()
    {
        cmbLanguage.Items.Clear();
        cmbLanguage.Items.Add("Deutsch");
        cmbLanguage.Items.Add("English");

        cmbLanguage.SelectedIndexChanged += (s, e) =>
        {
            var selected = cmbLanguage.SelectedItem?.ToString();
            if (selected == "English")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            else
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

            ApplyLanguage();
        };

        cmbLanguage.SelectedIndex = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "en" ? 1 : 0;
    }

    private void ApplyLanguage()
    {
        btnScan.Text = Resource.ScanPrinters;
        btnPause.Text = Resource.Pause;
        btnStop.Text = Resource.Stop;
        btnResume.Text = Resource.Resume;
        btnSendMessage.Text = Resource.SendToAll;
        btnEditVariables.Text = Resource.EditVariables;
        btnRemovePrinter.Text = Resource.RemovePrinter;
        lblPrintCount.Text = Resource.PrintCount;
        lblMessage.Text = Resource.MessageFile;
        lblPrinters.Text = Resource.Printers;
        lblStatus.Text = Resource.StatusReady; 
    }

    private void InitStatusTable()
    {
        statusTable.Clear();
        statusTable.Columns.Clear();
        statusTable.Columns.Add("IP");
        statusTable.Columns.Add("Connected");
        statusTable.Columns.Add("Status");
        statusTable.Columns.Add("Success");
    }

    private void UpdateStatusTable()
    {
        statusTable.Rows.Clear();
        foreach (var printer in groupManager.Printers)
            statusTable.Rows.Add(
                printer.IpAddress,
                printer.IsConnected ? "Yes" : "No",
                printer.LastStatus,
                printer.LastSendSuccess ? "✓" : "✗"
            );
    }

    private void LoadMessages()
    {
        var folder = Environment.ExpandEnvironmentVariables("%ProgramData%\\Resmark\\Messages");
        if (!Directory.Exists(folder)) return;

        var files = Directory.GetFiles(folder, "*.xml");
        cmbMessages.Items.Clear();
        cmbMessages.Items.AddRange(files.Select(Path.GetFileName).ToArray());
        if (cmbMessages.Items.Count > 0)
            cmbMessages.SelectedIndex = 0;
    }

    private void btnAddPrinter_Click(object sender, EventArgs e)
    {
        if (listScanResults.SelectedItem != null)
        {
            var ip = listScanResults.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(ip)) return;

            groupManager.AddPrinter(ip);
            if (!listPrinters.Items.Contains(ip))
                listPrinters.Items.Add(ip);
        }
    }

    private async Task ScanForPrinters()
    {
        lblStatus.Text = Resource.SearchStatus;
        listScanResults.Items.Clear();

        var printers = new List<FoundPrinter>();

        foreach (var printerTask in await new PrinterSearch().Search())
        {
            var sp = printerTask.Split(' ');
            printers.Add(new FoundPrinter { IP = sp[0], Detail = printerTask.Substring(sp[0].Length) });
        }


        if (printers.Count == 0)
        {
            Console.WriteLine("\nNo printers found.");
            lblStatus.Text = Resource.NoPrintersFound; // ← z. B. "Keine Drucker gefunden."
            return;
        }

        foreach (var foundPrinter in printers)
        {
            Console.WriteLine(foundPrinter);

            // Parse IP and UID from result
            try
            {
                var detail = foundPrinter.Detail;

                var printerUID = "";
                var match = Regex.Match(detail, @"UID=([\w\d]+)");

                if (match.Success) printerUID = match.Groups[1].Value;

                listScanResults.Items.Add($"{printerUID} - {foundPrinter.IP}");
            }
            catch
            {
                Console.WriteLine($"Invalid result format: {foundPrinter}");
            }
        }

        lblStatus.Text = string.Format(Resource.FoundPrinters, printers.Count); // z. B. "3 Drucker gefunden"
    }


    private void SendMessageToAll()
    {
        if (cmbMessages.SelectedItem == null)
        {
            MessageBox.Show(Resource.SelectMessage);
            return;
        }

        var filename = cmbMessages.SelectedItem.ToString();
        var folder = Environment.ExpandEnvironmentVariables("%ProgramData%\\Diagraph\\ResmarkManager\\Messages");
        var filepath = Path.Combine(folder, filename);
        if (!File.Exists(filepath)) return;

        var xml = File.ReadAllText(filepath);
        if (!int.TryParse(txtPrintCount.Text, out var printCount)) printCount = 1;

        foreach (var printer in groupManager.Printers)
            Task.Run(async () =>
            {
                var status = await printer.GetStatusAsync();

                if (status.Length > 0)
                {
                    await printer.SetPrintCountAsync(printCount);

                    var ok = await printer.SendMessageAsync(xml);
                    lblStatus.Text = $"> {printer.IpAddress}: {(ok ? "✓" : "✗")} - Status: {status}";
                }
                else
                {
                    lblStatus.Text = $"> {printer.IpAddress}: {Resource.ConnectionFailed}";
                }
            });

        UpdateStatusTable();
    }

    private void EditSelectedPrinterVariables()
    {
        if (listPrinters.SelectedItem is not string printer) return;
        var id = printer.Split("-")[0].Trim();
        var ip = printer.Split("-")[1].Trim();

        var wrapper = new ResmarkPrinterWrapper(printerService, id, ip);
        var form = new VariableEditorForm(wrapper);
        form.ShowDialog();
    }

    private void btnRemovePrinter_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string ip)
        {
            groupManager.RemovePrinter(ip);
            listPrinters.Items.Remove(ip);
        }
    }

    private void btnScan_Click(object sender, EventArgs e)
    {
        _ = ScanForPrinters();
    }

    private void listScanResults_DoubleClick(object sender, EventArgs e)
    {
        if (listScanResults.SelectedItem is string ip) btnAddPrinter_Click(this, null);
    }

    private void btnSendMessage_Click(object sender, EventArgs e)
    {
        SendMessageToAll();
    }

    private void btnEditVariables_Click(object sender, EventArgs e)
    {
        EditSelectedPrinterVariables();
    }

    private void GroupMainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        var collection = new StringCollection();

        foreach (var item in listPrinters.Items)
            if (item is string printerEntry && !string.IsNullOrWhiteSpace(printerEntry))
                collection.Add(printerEntry); // z. B. "05e9 - 192.168.0.12"

        Settings.Default.SavedPrinters = collection;
        Settings.Default.Save();
    }
      
    private void btnPause_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
            groupManager.GetPrinter(printer)?.PausePrintingAsync();
    }

    private void btnResume_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
            groupManager.GetPrinter(printer)?.ResumePrintingAsync(); 
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
            groupManager.GetPrinter(printer)?.CancelPrintAsync(); 
    }

    public class FoundPrinter
    {
        /// <summary>
        ///     Gets or sets the IP address of the printer.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        ///     Gets or sets additional details about the printer.
        /// </summary>
        public string Detail { get; set; }
    }
}