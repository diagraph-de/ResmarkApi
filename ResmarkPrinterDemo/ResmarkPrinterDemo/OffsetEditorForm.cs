using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo;

public partial class OffsetEditorForm : CustomMaterialRoundedForm
{
    private readonly OffsetConfigurationManager _offsetManager;

    public OffsetEditorForm(OffsetConfigurationManager offsetManager)
    {
        InitializeComponent();
        _offsetManager = offsetManager;

        ApplyLanguage();
    }

    private void ApplyLanguage()
    {
        Text = Resource.OffsetFormTitle;
        btnAddOrUpdate.Text = Resource.OffsetAddOrUpdate;
        btnRemove.Text = Resource.OffsetRemove;
        btnSave.Text = Resource.Save;
        btnCancel.Text = Resource.Cancel;
        lblPrinterId.Text = Resource.PrinterId;
        lblIp.Text = Resource.IpAddress;
        lblMessage.Text = Resource.MessageFile;
        lblOffset.Text = Resource.OffsetValue;
        dataGridOffsets.Columns[0].HeaderText = Resource.PrinterId;
        dataGridOffsets.Columns[1].HeaderText = Resource.IpAddress;
        dataGridOffsets.Columns[2].HeaderText = Resource.MessageFile;
        dataGridOffsets.Columns[3].HeaderText = Resource.OffsetValue;
    }


    private void OffsetEditorForm_Load(object sender, EventArgs e)
    {
        LoadOffsets();
    }

    private void LoadOffsets()
    {
        dataGridOffsets.Rows.Clear();

        foreach (var entry in _offsetManager.GetAll())
        {
            var keyParts = entry.Key.Split(" | ");
            if (keyParts.Length != 3)
                continue;

            var printerId = keyParts[0];
            var ipAddress = keyParts[1];
            var messageName = keyParts[2];
            var offset = entry.Value;

            dataGridOffsets.Rows.Add(printerId, ipAddress, messageName, offset);
        }
    }


    private void btnAddOrUpdate_Click(object sender, EventArgs e)
    {
        var printerId = txtPrinterId.Text.Trim();
        var ip = txtIp.Text.Trim();
        var message = txtMessage.Text.Trim();
        var offset = txtOffset.Text.Trim();

        if (string.IsNullOrWhiteSpace(printerId) ||
            string.IsNullOrWhiteSpace(ip) ||
            string.IsNullOrWhiteSpace(message) ||
            string.IsNullOrWhiteSpace(offset))
        {
            MessageBox.Show(Resource.FillAllFields, Resource.OffsetFormTitle, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        _offsetManager.SetOffset(printerId, ip, message, Convert.ToInt32(offset));
        LoadOffsets();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (dataGridOffsets.SelectedRows.Count > 0)
        {
            var row = dataGridOffsets.SelectedRows[0];
            var printerId = row.Cells[0].Value?.ToString();
            var ip = row.Cells[1].Value?.ToString();
            var message = row.Cells[2].Value?.ToString();

            if (!string.IsNullOrEmpty(printerId) &&
                !string.IsNullOrEmpty(ip) &&
                !string.IsNullOrEmpty(message))
            {
                _offsetManager.RemoveOffset(printerId, ip, message);
                LoadOffsets();
            }
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _offsetManager.Save();
        MessageBox.Show(Resource.OffsetSaved, Resource.OffsetFormTitle, MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}