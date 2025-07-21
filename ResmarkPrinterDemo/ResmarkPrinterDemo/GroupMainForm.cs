using Diagraph.ResmarkApi;
using Diagraph.ResmarkApi.Services;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Properties;
using ResmarkPrinterGroupDemo.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        Shown += async (s, e) =>
        {
            await ScanForPrinters();
            btnRefreshStatus_ClickAsync(this,null);
        };

        gridStatus.DataSource = _statusTable;
    
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

        if (listPrinters.Items.Count > 0)
            listPrinters.SelectedIndex = 0;
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
        tabPrinters.Text = Resource.Printers;
        tabGroup.Text = Resource.Groups;
        lblStatus.Text = Resource.StatusReady;
        lblAvailableMessages.Text = Resource.AvailableMessages;
        lblStatusList.Text = Resource.Status;
        btnEditGroupVariables.Text = Resource.EditGroupVariables;
    }

    private void InitStatusTable()
    {
        _statusTable.Clear();
        _statusTable.Columns.Clear();
        _statusTable.Columns.Add("IP");
        _statusTable.Columns.Add("Message"); // neue Spalte
        _statusTable.Columns.Add("Status");
        _statusTable.Columns.Add("Connected");
        _statusTable.Columns.Add("Success");
    }

    private void UpdateStatusTable()
    {
        _statusTable.Rows.Clear();

        int rowCnt = 0;
        foreach (var printer in _groupManager.Printers)
        {
            var statusText = printer.Status + Environment.NewLine;

            if (printer.PrinterErrors.Count > 0)
                statusText += Environment.NewLine + "Errors:" + Environment.NewLine +
                              string.Join(Environment.NewLine, printer.PrinterErrors);

            if (printer.PrinterErrorDetails.Count > 0)
                statusText += Environment.NewLine + Environment.NewLine + "Details:" + Environment.NewLine +
                              string.Join(Environment.NewLine, printer.PrinterErrorDetails);

            _statusTable.Rows.Add(
                printer.PrinterId + " - " + printer.IpAddress,
                printer.MessageName,
                statusText,
                printer.IsConnected ? "✓" : "✗",
                printer.LastSuccess ? "✓" : "✗"
            );

            // Color logic based on PrinterErrorDetails count
            var row = gridStatus.Rows[rowCnt];
            if (printer.PrinterErrorDetails.Count > 0)
                row.DefaultCellStyle.BackColor = Color.LightCoral; // red
            else
                row.DefaultCellStyle.BackColor = Color.LightGreen; // green
            rowCnt++;
        }
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
                        lblStatus.Text = $"{printer.IpAddress}: {(ok ? "✓" : "✗")} - {Resource.StatusSent}"
                    );
                }
                else
                {
                    lblStatus.Invoke(() =>
                        lblStatus.Text = $"{printer.IpAddress}: {Resource.ConnectionFailed}"
                    );
                }
            });

        UpdateStatusTable();
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

        Thread.Sleep(3000); // Wait for status updates
        btnRefreshStatus_ClickAsync(this, null);
    }

    private void btnEditVariables_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is not string printer) return;

        var id = printer.Split("-")[0].Trim();
        var ip = printer.Split("-")[1].Trim();

        var wrapper = new ResmarkPrinterWrapper(_printerService, id, ip);
        var form = new VariableEditorForm(wrapper);
        form.ShowDialog();

        lblStatus.Text = Resource.StatusEditVariables;
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

        btnRefreshStatus_ClickAsync(this, null);
    }

    private void btnResume_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.ResumePrintingAsync();
            lblStatus.Text = Resource.StatusResume;
        }

        btnRefreshStatus_ClickAsync(this, null);
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            _groupManager.GetPrinter(printer)?.CancelPrintAsync();
            lblStatus.Text = Resource.StatusStop;
        }

        btnRefreshStatus_ClickAsync(this, null);
    }

    private async void btnSelectMessage_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            var wrapper = _groupManager.GetPrinter(printer);
            if (wrapper == null) return;

            if (!int.TryParse(txtPrintCount.Text, out var printCount)) printCount = 1;

            await wrapper.SetPrintCountAsync(printCount);
            await wrapper.PrintStoredMessageAsync(cboSelectMessage.Text);
            var status = await wrapper.GetStatusAsync();
            lblStatus.Text =
                $" {wrapper.IpAddress}: {Resource.StatusSelectMessage} \"{cboSelectMessage.Text}\" – {status}";

            btnRefreshStatus_ClickAsync(this, null);
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

        btnRefreshStatus_ClickAsync(this, null);
    }

    private async void btnRefreshStatus_ClickAsync(object sender, EventArgs e)
    {
        foreach (var printer in _groupManager.Printers) await printer.GetStatusAsync();

        UpdateStatusTable();
    }

    private void btnPreview_Click(object sender, EventArgs e)
    {
        if (cmbMessages.SelectedItem == null)
        {
            MessageBox.Show(Resource.SelectMessage);
            return;
        }

        if (listPrinters.SelectedItem is not string printerKey) return;
        var wrapper = _groupManager.GetPrinter(printerKey);
        if (wrapper == null) return;

        var fileName = cmbMessages.SelectedItem.ToString();
        var folder = Environment.ExpandEnvironmentVariables(Settings.Default.MessageFolderPath);
        var path = Path.Combine(folder, fileName);

        if (!File.Exists(path))
        {
            MessageBox.Show(Resource.FileNotFound);
            return;
        }

        var xml = File.ReadAllText(path);
        var previewForm = new MessagePreviewForm(wrapper, xml);
        previewForm.ShowDialog();
    }

    private async void btnPathPreviewMessage_Click(object sender, EventArgs e)
    {
        if (cboSelectMessage.SelectedItem == null)
        {
            MessageBox.Show(Resource.SelectMessage);
            return;
        }

        if (listPrinters.SelectedItem is not string printerKey) return;
        var wrapper = _groupManager.GetPrinter(printerKey);
        if (wrapper == null) return;

        var messageName = cboSelectMessage.SelectedItem.ToString();
        if (string.IsNullOrWhiteSpace(messageName))
        {
            MessageBox.Show(Resource.SelectMessage);
            return;
        }

        try
        {
            var imageData = await wrapper.PathPrintPreviewAsync(messageName);
            if (imageData != null && imageData.Length > 0)
            {
                var previewForm = new MessagePreviewFormImageOnly(imageData);
                previewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(Resource.ErrorPreview);
            }
        }
        catch
        {
            MessageBox.Show(Resource.ErrorPreview);
        }
    }

    private async void btnToXML_Click(object sender, EventArgs e)
    {
        // Ensure a printer is selected
        if (listPrinters.SelectedItem is not string printer)
            return;

        // Split printer string into UID and IP address
        var parts = printer.Split("-");
        if (parts.Length != 2)
            return;

        var id = parts[0].Trim();
        var ip = parts[1].Trim();

        var wrapper = new ResmarkPrinterWrapper(_printerService, id, ip);

        try
        {
            // Attempt to recall the active message
            var xml = await wrapper.RecallMessageAsync(cboSelectMessage.Text);
            if (string.IsNullOrWhiteSpace(xml))
            {
                MessageBox.Show(Resource.ErrorEmptyRecall, Resource.Done, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the configured output folder path
            var folder = Environment.ExpandEnvironmentVariables(Settings.Default.MessageFolderPath);
            Directory.CreateDirectory(folder); // Ensure directory exists

            var filename = Path.Combine(folder, cboSelectMessage.Text);
            File.WriteAllText(filename, xml);

            // Notify user
            MessageBox.Show($"{Resource.MessageSaved}\n{filename}", Resource.Done, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{Resource.ErrorRecall}\n{ex.Message}", Resource.Done, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        LoadMessages();
    }

    private void btnConfig_Click(object sender, EventArgs e)
    {
        if (listPrinters.SelectedItem is string printer)
        {
            var wrapper = _groupManager.GetPrinter(printer);
            if (wrapper == null) return;

            using var configForm = new ConfigurationForm(wrapper);
            configForm.ShowDialog();
        }
    }

    private void btnDeleteXML_Click(object sender, EventArgs e)
    {
        var folder = Environment.ExpandEnvironmentVariables(Settings.Default.MessageFolderPath);
        Directory.CreateDirectory(folder); // Ensure directory exists

        var filename = Path.Combine(folder, cmbMessages.Text);
        if (File.Exists(filename))
        {
            var confirm = MessageBox.Show(
                string.Format(Resource.ConfirmDeleteMessage, Path.GetFileName(filename)),
                Resource.DeleteTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
                try
                {
                    File.Delete(filename);
                    MessageBox.Show(Resource.DeleteSuccess, Resource.DeleteTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format(Resource.DeleteError, ex.Message),
                        Resource.DeleteTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
        }
        else
        {
            MessageBox.Show(Resource.FileNotFound, Resource.DeleteTitle, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        LoadMessages();
    }

    public class FoundPrinter
    {
        public string IP { get; set; }
        public string Detail { get; set; }
    }

    private async void btnEditGroupVariables_Click(object sender, EventArgs e)
    {
        if (_groupManager.Printers.Count == 0)
        {
            MessageBox.Show(Resource.NoPrintersFound, Resource.GroupVariablesTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
         
        var templatePrinter = _groupManager.Printers.FirstOrDefault(p => p.IsConnected);
        if (templatePrinter == null)
        {
            MessageBox.Show(Resource.ConnectionFailed, Resource.GroupVariablesTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (listPrinters.SelectedItem is not string p) return;

        var id = p.Split("-")[0].Trim();
        var ip = p.Split("-")[1].Trim();

        var wrapper = new ResmarkPrinterWrapper(_printerService, id, ip); 
        var initialVars = await templatePrinter.GetVariablesAsync();
        var editedVars = new Dictionary<string, string>(initialVars);
         
        using var form = new VariableEditorForm(editedVars, wrapper);
        if (form.ShowDialog() != DialogResult.OK)
            return;
          
        lblStatus.Text = Resource.StatusSending;

        foreach (var printer in _groupManager.Printers)
        {
            foreach (var kv in editedVars)
                await printer.SetVariableAsync(kv.Key, kv.Value);
        }
        
        MessageBox.Show(Resource.VariablesSaved, Resource.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);

        Thread.Sleep(1000);
        btnRefreshStatus_ClickAsync(this, null);
    }

}