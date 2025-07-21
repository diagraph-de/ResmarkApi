// MessagePreviewForm.Designer.cs
namespace ResmarkPrinterGroupDemo
{
    partial class MessagePreviewForm
    {
        private System.ComponentModel.IContainer components = null;
        private Diagraph.Controls.Buttons.FlexButton btnPrint;
        private System.Windows.Forms.PictureBox pictureBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagePreviewForm));
            btnPrint = new Diagraph.Controls.Buttons.FlexButton();
            pictureBox = new System.Windows.Forms.PictureBox();
            btnClose = new Diagraph.Controls.Buttons.FlexButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // btnPrint
            // 
            btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnPrint.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnPrint.ButtonImage = null;
            btnPrint.ButtonText = "Drucken";
            btnPrint.CanGetFocus = true;
            btnPrint.CenterText = false;
            btnPrint.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnPrint.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnPrint.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnPrint.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnPrint.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnPrint.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnPrint.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnPrint.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnPrint.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnPrint.ControlText = "Drucken";
            btnPrint.CornerRadius = 4;
            btnPrint.DriverAssignmentKeys = null;
            btnPrint.EnableBlink = false;
            btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrint.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnPrint.GradientPosition = 0.4F;
            btnPrint.ImageAboveText = false;
            btnPrint.ImageHeight = 0;
            btnPrint.IsAutoSizing = false;
            btnPrint.IsSelectable = true;
            btnPrint.IsSelected = false;
            btnPrint.Location = new System.Drawing.Point(13, 519);
            btnPrint.LoggingEnabled = true;
            btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrint.MirrorImage = false;
            btnPrint.Name = "btnPrint";
            btnPrint.Password_Category = null;
            btnPrint.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnPrint.Password_Name = null;
            btnPrint.PrinterGroup_Category = null;
            btnPrint.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnPrint.PrinterGroup_Name = null;
            btnPrint.Size = new System.Drawing.Size(229, 46);
            btnPrint.SupportedPrinter = null;
            btnPrint.SuppressSelect = false;
            btnPrint.SuppressUpdate = false;
            btnPrint.TabIndex = 1;
            btnPrint.Text = "Drucken";
            btnPrint.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnPrint.TextCorrection = 0;
            btnPrint.UpdateEnabled = true;
            btnPrint.UseMouseOver = false;
            btnPrint.VersionKey = null;
            btnPrint.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnPrint.Visible = false;
            btnPrint.Click += BtnPrint_Click;
            // 
            // pictureBox
            // 
            pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pictureBox.Location = new System.Drawing.Point(13, 75);
            pictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(674, 423);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
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
            btnClose.Location = new System.Drawing.Point(458, 519);
            btnClose.LoggingEnabled = true;
            btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClose.MirrorImage = false;
            btnClose.Name = "btnClose";
            btnClose.Password_Category = null;
            btnClose.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnClose.Password_Name = null;
            btnClose.PrinterGroup_Category = null;
            btnClose.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnClose.PrinterGroup_Name = null;
            btnClose.Size = new System.Drawing.Size(229, 46);
            btnClose.SupportedPrinter = null;
            btnClose.SuppressSelect = false;
            btnClose.SuppressUpdate = false;
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnClose.TextCorrection = 0;
            btnClose.UpdateEnabled = true;
            btnClose.UseMouseOver = false;
            btnClose.VersionKey = null;
            btnClose.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnClose.Click += btnClose_Click;
            // 
            // MessagePreviewForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(700, 577);
            Controls.Add(btnClose);
            Controls.Add(pictureBox);
            Controls.Add(btnPrint);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MessagePreviewForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Vorschau";
            Load += MessagePreviewForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }
        private Diagraph.Controls.Buttons.FlexButton btnClose;
    }
}