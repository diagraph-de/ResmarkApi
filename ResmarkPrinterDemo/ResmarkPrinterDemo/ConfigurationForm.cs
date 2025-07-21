using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo
{
    public partial class ConfigurationForm : CustomMaterialRoundedForm
    {
        private readonly ResmarkPrinterWrapper _printer;

        public ConfigurationForm(ResmarkPrinterWrapper printer)
        {
            _printer = printer;
            InitializeComponent();
             
            Text = Resource.ConfigTitle;
            btnSave.Text = Resource.ConfigSaveButton;
            btnCancel.Text = Resource.ConfigCancelButton;
        }

        private async void ConfigurationForm_Load(object sender, EventArgs e)
        {
            try
            {
                var configXml = await _printer.GetConfiguration();
                txtConfiguration.Text = configXml;
            }
            catch
            {
                MessageBox.Show(Resource.ConfigLoadError, Resource.ConfigTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var newConfig = txtConfiguration.Text;
            var result = await _printer.SetConfiguration(newConfig);

            if (result.Success)
            {
                MessageBox.Show(Resource.ConfigSaved, Resource.ConfigTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Resource.ConfigSaveError, Resource.ConfigTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}