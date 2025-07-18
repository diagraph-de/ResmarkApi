using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diagraph.ResmarkApi;
using Diagraph.ResmarkApi.Services;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Properties;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo;

public partial class GroupMainForm : CustomMaterialRoundedForm
{
    private readonly PrinterGroupManager _groupManager = new();
    private readonly ResmarkPrinterService _printerService = new(new ClientService());
    private readonly DataTable _statusTable = new();

    public GroupMainForm()
    {
        Width = 1200;
        Height = 800;
        InitializeComponent();
        InitLanguage();
        LoadMessages();
        InitStatusTable();
        RestoreSettings();
        Shown += async (s, e) => await ScanForPrinters();
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
                    _groupManager.AddPrinter(printer);
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
        _statusTable.Clear();
        _statusTable.Columns.Clear();
        _statusTable.Columns.Add("IP");
        _statusTable.Columns.Add("Connected");
        _statusTable.Columns.Add("Status");
        _statusTable.Columns.Add("Success");
    }

    private void UpdateStatusTable()
    {
        _statusTable.Rows.Clear();
        foreach (var printer in _groupManager.Printers)
            _statusTable.Rows.Add(
                printer.IpAddress,
                printer.IsConnected ? "Yes" : "No",
                printer.LastStatus,
                printer.LastSendSuccess ? "✓" : "✗"
            );
    }

    private void LoadMessages()
    {
        var folder = Environment.ExpandEnvironmentVariables(Settings.Default.MessageFolderPath);
        if (!Directory.Exists(folder)) return;

        var files = Directory.GetFiles(folder, "*.next");
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

            _groupManager.AddPrinter(ip);
            if (!listPrinters.Items.Contains(ip))
                listPrinters.Items.Add(ip);

            listScanResults.Items.Remove(ip);

            lblStatus.Text = Resource.StatusPrinterAdded;
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
            if (sp.Length == 0) continue;

            var ip = sp[0].Trim();
            var detail = printerTask.Substring(ip.Length);
            printers.Add(new FoundPrinter { IP = ip, Detail = detail });
        }

        var existingIPs = new HashSet<string>(
            listPrinters.Items
                .OfType<string>()
                .Select(item => item.Split('-')[1].Trim())
        );

        var addedCount = 0;
        foreach (var foundPrinter in printers)
            try
            {
                var match = Regex.Match(foundPrinter.Detail, @"UID=([\w\d]+)");
                var printerUID = match.Success ? match.Groups[1].Value : "";

                if (!existingIPs.Contains(foundPrinter.IP))
                {
                    listScanResults.Items.Add($"{printerUID} - {foundPrinter.IP}");
                    addedCount++;
                }
            }
            catch
            {
                Console.WriteLine($"Invalid result format: {foundPrinter}");
            }

        lblStatus.Text = addedCount > 0
            ? string.Format(Resource.FoundPrinters, addedCount)
            : Resource.NoPrintersFound;
    }

    private void SendMessageToAll()
    {
        if (cmbMessages.SelectedItem == null)
        {
            MessageBox.Show(Resource.SelectMessage);
            return;
        }

        lblStatus.Text = Resource.StatusSending;
        var filename = cmbMessages.SelectedItem.ToString();
        var folder = Environment.ExpandEnvironmentVariables(Settings.Default.MessageFolderPath);
        var filepath = Path.Combine(folder, filename);
        if (!File.Exists(filepath)) return;

        var xml = File.ReadAllText(filepath);
        if (!int.TryParse(txtPrintCount.Text, out var printCount)) printCount = 1;

        foreach (var printer in _groupManager.Printers)
            Task.Run(async () =>
            {
                var status = await printer.GetStatusAsync();
                if (status.Length > 0)
                {
                    await printer.SetPrintCountAsync(printCount);
                    var ok = await printer.SendMessageAsync(xml);
                    lblStatus.Invoke(() =>
                        lblStatus.Text = $"> {printer.IpAddress}: {(ok ? "✓" : "✗")} - {Resource.StatusSent}"
                    );
                }
                else
                {
                    lblStatus.Invoke(() =>
                        lblStatus.Text = $"> {printer.IpAddress}: {Resource.ConnectionFailed}"
                    );
                }
            });

        UpdateStatusTable();
    }

    private void EditSelectedPrinterVariables()
    {
        if (listPrinters.SelectedItem is not string printer) return;

        var id = printer.Split("-")[0].Trim();
        var ip = printer.Split("-")[1].Trim();

        var wrapper = new ResmarkPrinterWrapper(_printerService, id, ip);
        var form = new VariableEditorForm(wrapper);
        form.ShowDialog();

        lblStatus.Text = Resource.StatusEditVariables;
    }

    private void btnRemovePrinter_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.RemovePrinter(printer);
            listPrinters.Items.Remove(printer);
            lblStatus.Text = Resource.StatusPrinterRemoved;
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
                collection.Add(printerEntry);

        Settings.Default.SavedPrinters = collection;
        Settings.Default.Save();
    }

    private void btnPause_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.PausePrintingAsync();
            lblStatus.Text = Resource.StatusPause;
        }
    }

    private void btnResume_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.ResumePrintingAsync();
            lblStatus.Text = Resource.StatusResume;
        }
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.CancelPrintAsync();
            lblStatus.Text = Resource.StatusStop;
        }
    }

    private void btnSelectMessage_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.PrintStoredMessageAsync(cboSelectMessage.Text);
            lblStatus.Text = string.Format(Resource.StatusSelectMessage, cboSelectMessage.Text);
        }
    }

    private async void listPrinters_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            var wrapper = _groupManager.GetPrinter(printer);
            if (wrapper == null) return;

            var messages = await wrapper.GetMessagesAsync();
            if (messages != null)
            {
                cboSelectMessage.Items.Clear();
                cboSelectMessage.DisplayMember = "Name";
                cboSelectMessage.Items.AddRange(messages.ToArray());
                if (cboSelectMessage.Items.Count > 0)
                    cboSelectMessage.SelectedIndex = 0;
            }
        }
    }

    public class FoundPrinter
    {
        public string IP { get; set; }
        public string Detail { get; set; }
    }
}