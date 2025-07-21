using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo;

public partial class VariableEditorForm : CustomMaterialRoundedForm
{
    private readonly ResmarkPrinterWrapper _printer;
    private readonly DataTable _table = new();
    private readonly Dictionary<string, string> _sharedVariables;

    public VariableEditorForm(Dictionary<string, string> variables, ResmarkPrinterWrapper printer)
    {
        InitializeComponent();
        _sharedVariables = variables;
        Text = Resource.GroupVariablesTitle;

        _table.Clear();
        _table.Columns.Clear();
        _table.Columns.Add("Name", typeof(string));
        _table.Columns.Add("Wert", typeof(string));

        foreach (var kv in _sharedVariables)
            _table.Rows.Add(kv.Key, kv.Value);

        dataGridVariables.DataSource = _table;

        if (dataGridVariables.Columns.Count >= 2)
        {
            dataGridVariables.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridVariables.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        _printer = printer;
    }

    public VariableEditorForm(ResmarkPrinterWrapper printer)
    {
        InitializeComponent();
        this._printer = printer;
        Text = string.Format(Resource.VariablesTitle, printer.IpAddress);
    }

    private async void VariableEditorForm_Load(object sender, EventArgs e)
    {
        await LoadVariablesAsync();
    }

    private async Task LoadVariablesAsync()
    {
        _table.Clear();
        _table.Columns.Clear();
        _table.Columns.Add("Name", typeof(string));
        _table.Columns.Add("Wert", typeof(string));

        var vars = await _printer.GetVariablesAsync();
        foreach (var kv in vars)
            _table.Rows.Add(kv.Key, kv.Value);

        dataGridVariables.DataSource = _table;

        if (dataGridVariables.Columns.Count >= 2)
        {
            dataGridVariables.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // „Name“
            dataGridVariables.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // „Wert“
        }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        var vars = new Dictionary<string, string>();

        foreach (DataRow row in _table.Rows)
        {
            var key = row["Name"].ToString();
            var value = row["Wert"].ToString();
            if (!string.IsNullOrWhiteSpace(key) && value != null)
                vars[key] = value;
        }

        if (_sharedVariables == null && _printer != null)
        {
            //Single printer
            foreach (var kv in vars)
                await _printer.SetVariableAsync(kv.Key, kv.Value);

            MessageBox.Show(Resource.VariablesSaved, Resource.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (_sharedVariables != null)
        {
            // Printer group
            _sharedVariables.Clear();
            foreach (var kv in vars)
                _sharedVariables[kv.Key] = kv.Value;
        } 

        DialogResult = DialogResult.OK;
        Close();
    }

}