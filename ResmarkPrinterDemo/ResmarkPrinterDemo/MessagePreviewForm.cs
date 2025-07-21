using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo;

public partial class MessagePreviewForm : CustomMaterialRoundedForm
{
    private readonly ResmarkPrinterWrapper printer;
    private readonly string xmlMessage;

    public MessagePreviewForm(ResmarkPrinterWrapper printer, string xml)
    {
        this.printer = printer;
        xmlMessage = xml;

        InitializeComponent();

        Text = Resource.PreviewTitle;
        btnPrint.Text = Resource.Print;
    }

    private async void MessagePreviewForm_Load(object sender, EventArgs e)
    {
        await LoadPreviewAsync();
    }

    private async void BtnPrint_Click(object sender, EventArgs e)
    {
        var success = await printer.SendMessageAsync(xmlMessage);
        if (success)
            MessageBox.Show(Resource.PrintSuccess, Resource.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);

        Close();
    }

    private async Task LoadPreviewAsync()
    {
        try
        {
            var imageData = await printer.PrintPreviewAsync(xmlMessage);
            if (imageData != null && imageData.Length > 0)
            {
                using var ms = new MemoryStream(imageData);
                pictureBox.Image = Image.FromStream(ms);
            }
        }
        catch
        {
            MessageBox.Show(Resource.ErrorPreview);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}