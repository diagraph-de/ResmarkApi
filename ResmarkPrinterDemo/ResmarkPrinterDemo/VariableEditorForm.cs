using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diagraph.Controls.Buttons;
using MaterialSkin.Controls;

namespace ResmarkPrinterGroupDemo;

public class VariableEditorForm : CustomMaterialRoundedForm
{
    private readonly FlexButton btnSave = new();
    private readonly DataGridView grid = new();
    private readonly ResmarkPrinterWrapper printer;
    private readonly DataTable table = new();

    public VariableEditorForm(ResmarkPrinterWrapper printer)
    {
        this.printer = printer;
        Text = $"Variablen – {printer.IpAddress}";
        Width = 600;
        Height = 500;

        grid.SetBounds(10, 10, 560, 380);
        grid.DataSource = table;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;

        btnSave.Text = "Speichern";
        btnSave.SetBounds(10, 400, 100, 30);
        btnSave.Click += async (s, e) => await SaveChangesAsync();

        Controls.Add(grid);
        Controls.Add(btnSave);

        Load += async (_, _) => await LoadVariablesAsync();
    }

    private async Task LoadVariablesAsync()
    {
        table.Clear();
        table.Columns.Clear();
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Wert", typeof(string));

        var vars = await printer.GetVariablesAsync();
        foreach (var kv in vars) table.Rows.Add(kv.Key, kv.Value);
    }

    private async Task SaveChangesAsync()
    {
        foreach (DataRow row in table.Rows)
        {
            var key = row["Name"].ToString();
            var value = row["Wert"].ToString();
            await printer.SetVariableAsync(key, value);
        }

        MessageBox.Show("Variablen gespeichert.", "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}