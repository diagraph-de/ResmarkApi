using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Diagraph.Controls.Buttons;
using MaterialSkin.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo
{
    public partial class MessagePreviewFormImageOnly : CustomMaterialRoundedForm
    {
        private readonly byte[] imageData;

        public MessagePreviewFormImageOnly(byte[] imageBytes)
        {
            InitializeComponent();
            imageData = imageBytes;
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
}