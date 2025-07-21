using System;

namespace ResmarkPrinterGroupDemo
{
    partial class MessagePreviewFormImageOnly
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private Diagraph.Controls.Buttons.FlexButton btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagePreviewFormImageOnly));
            pictureBoxPreview = new System.Windows.Forms.PictureBox();
            btnClose = new Diagraph.Controls.Buttons.FlexButton();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pictureBoxPreview.Location = new System.Drawing.Point(23, 87);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new System.Drawing.Size(556, 340);
            pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 0;
            pictureBoxPreview.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnClose.ButtonImage = null;
            btnClose.ButtonText = "Close";
            btnClose.CanGetFocus = true;
            btnClose.CenterText = false;
            btnClose.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnClose.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnClose.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnClose.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnClose.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnClose.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnClose.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnClose.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnClose.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnClose.ControlText = "Close";
            btnClose.CornerRadius = 4;
            btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnClose.DriverAssignmentKeys = null;
            btnClose.EnableBlink = false;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnClose.GradientPosition = 0.4F;
            btnClose.ImageAboveText = false;
            btnClose.ImageHeight = 0;
            btnClose.IsAutoSizing = false;
            btnClose.IsSelectable = true;
            btnClose.IsSelected = false;
            btnClose.Location = new System.Drawing.Point(0, 450);
            btnClose.LoggingEnabled = true;
            btnClose.MirrorImage = false;
            btnClose.Name = "btnClose";
            btnClose.Password_Category = null;
            btnClose.Password_ID = new Guid("00000000-0000-0000-0000-000000000000");
            btnClose.Password_Name = null;
            btnClose.PrinterGroup_Category = null;
            btnClose.PrinterGroup_ID = new Guid("00000000-0000-0000-0000-000000000000");
            btnClose.PrinterGroup_Name = null;
            btnClose.Size = new System.Drawing.Size(600, 40);
            btnClose.SupportedPrinter = null;
            btnClose.SuppressSelect = false;
            btnClose.SuppressUpdate = false;
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnClose.TextCorrection = 0;
            btnClose.UpdateEnabled = true;
            btnClose.UseMouseOver = false;
            btnClose.VersionKey = null;
            btnClose.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnClose.Click += BtnClose_Click;
            // 
            // MessagePreviewFormImageOnly
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(600, 490);
            Controls.Add(pictureBoxPreview);
            Controls.Add(btnClose);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MessagePreviewFormImageOnly";
            Text = "Preview";
            Load += MessagePreviewFormImageOnly_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            ResumeLayout(false);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
