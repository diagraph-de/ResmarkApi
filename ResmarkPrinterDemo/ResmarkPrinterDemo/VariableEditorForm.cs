using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diagraph.ResmarkApi.Services;
using MaterialSkin.Controls;

namespace ResmarkPrinterGroupDemo
{
    public partial class VariableEditorForm : CustomMaterialRoundedForm
    {
        private readonly ResmarkPrinterWrapper printer;
        private readonly DataTable table = new();

        public VariableEditorForm(ResmarkPrinterWrapper printer)
        {
            InitializeComponent();
            this.printer = printer;
            Text = $"Variablen – {printer.IpAddress}";
        }

        private async void VariableEditorForm_Load(object sender, EventArgs e)
        {
            await LoadVariablesAsync();
        }

        private async Task LoadVariablesAsync()
        {
            table.Clear();
            table.Columns.Clear();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Wert", typeof(string));

            var vars = await printer.GetVariablesAsync();
            foreach (var kv in vars)
                table.Rows.Add(kv.Key, kv.Value);

            dataGridVariables.DataSource = table;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in table.Rows)
            {
                var key = row["Name"].ToString();
                var value = row["Wert"].ToString();
                if (key != null && value != null)
                    await printer.SetVariableAsync(key, value);
            }

            MessageBox.Show("Variablen gespeichert.", "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}