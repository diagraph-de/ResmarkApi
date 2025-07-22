using System;
using System.Drawing;
using System.IO;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo;

public partial class MessagePreviewFormImageOnly : CustomMaterialRoundedForm
{
    private readonly byte[] imageData;

    public MessagePreviewFormImageOnly(byte[] imageBytes)
    {
        InitializeComponent();
        imageData = imageBytes;

        ApplyLanguage();
    }

    private void ApplyLanguage()
    {
        Text = Resource.PreviewTitle; 
        btnClose.Text = Resource.Close;
    } 

    private void MessagePreviewFormImageOnly_Load(object sender, EventArgs e)
    {
        if (imageData != null && imageData.Length > 0)
        {
            using var ms = new MemoryStream(imageData);
            pictureBoxPreview.Image = Image.FromStream(ms);
        }
    }
}