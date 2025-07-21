using Diagraph.aControl.XML.xs;
using Org.BouncyCastle.Asn1.Crmf;
using System.Windows.Forms;

namespace ResmarkPrinterGroupDemo
{
    partial class ConfigurationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtConfiguration;
        private Diagraph.Controls.Buttons.FlexButton btnSave;
        private Diagraph.Controls.Buttons.FlexButton btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            txtConfiguration = new TextBox();
            btnSave = new Diagraph.Controls.Buttons.FlexButton();
            btnCancel = new Diagraph.Controls.Buttons.FlexButton();
            SuspendLayout();
            // 
            // txtConfiguration
            // 
            txtConfiguration.AcceptsReturn = true;
            txtConfiguration.AcceptsTab = true;
            txtConfiguration.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtConfiguration.Font = new System.Drawing.Font("Consolas", 10F);
            txtConfiguration.Location = new System.Drawing.Point(12, 80);
            txtConfiguration.Multiline = true;
            txtConfiguration.Name = "txtConfiguration";
            txtConfiguration.ScrollBars = ScrollBars.Both;
            txtConfiguration.Size = new System.Drawing.Size(760, 382);
            txtConfiguration.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnSave.ButtonImage = null;
            btnSave.ButtonText = "Speichern";
            btnSave.CanGetFocus = true;
            btnSave.CenterText = false;
            btnSave.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnSave.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnSave.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSave.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSave.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSave.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnSave.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSave.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSave.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnSave.ControlText = "Speichern";
            btnSave.CornerRadius = 4;
            btnSave.DriverAssignmentKeys = null;
            btnSave.EnableBlink = false;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnSave.GradientPosition = 0.4F;
            btnSave.ImageAboveText = false;
            btnSave.ImageHeight = 0;
            btnSave.IsAutoSizing = false;
            btnSave.IsSelectable = true;
            btnSave.IsSelected = false;
            btnSave.Location = new System.Drawing.Point(12, 480);
            btnSave.LoggingEnabled = true;
            btnSave.MirrorImage = false;
            btnSave.Name = "btnSave";
            btnSave.Password_Category = null;
            btnSave.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.Password_Name = null;
            btnSave.PrinterGroup_Category = null;
            btnSave.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.PrinterGroup_Name = null;
            btnSave.Size = new System.Drawing.Size(150, 40);
            btnSave.SupportedPrinter = null;
            btnSave.SuppressSelect = false;
            btnSave.SuppressUpdate = false;
            btnSave.TabIndex = 1;
            btnSave.Text = "Speichern";
            btnSave.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnSave.TextCorrection = 0;
            btnSave.UpdateEnabled = true;
            btnSave.UseMouseOver = false;
            btnSave.VersionKey = null;
            btnSave.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnCancel.ButtonImage = null;
            btnCancel.ButtonText = "Abbrechen";
            btnCancel.CanGetFocus = true;
            btnCancel.CenterText = false;
            btnCancel.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnCancel.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnCancel.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnCancel.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnCancel.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnCancel.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnCancel.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnCancel.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnCancel.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnCancel.ControlText = "Abbrechen";
            btnCancel.CornerRadius = 4;
            btnCancel.DriverAssignmentKeys = null;
            btnCancel.EnableBlink = false;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnCancel.GradientPosition = 0.4F;
            btnCancel.ImageAboveText = false;
            btnCancel.ImageHeight = 0;
            btnCancel.IsAutoSizing = false;
            btnCancel.IsSelectable = true;
            btnCancel.IsSelected = false;
            btnCancel.Location = new System.Drawing.Point(622, 480);
            btnCancel.LoggingEnabled = true;
            btnCancel.MirrorImage = false;
            btnCancel.Name = "btnCancel";
            btnCancel.Password_Category = null;
            btnCancel.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnCancel.Password_Name = null;
            btnCancel.PrinterGroup_Category = null;
            btnCancel.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnCancel.PrinterGroup_Name = null;
            btnCancel.Size = new System.Drawing.Size(150, 40);
            btnCancel.SupportedPrinter = null;
            btnCancel.SuppressSelect = false;
            btnCancel.SuppressUpdate = false;
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Abbrechen";
            btnCancel.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnCancel.TextCorrection = 0;
            btnCancel.UpdateEnabled = true;
            btnCancel.UseMouseOver = false;
            btnCancel.VersionKey = null;
            btnCancel.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnCancel.Click += btnCancel_Click;
            // 
            // ConfigurationForm
            // 
            ClientSize = new System.Drawing.Size(784, 541);
            Controls.Add(txtConfiguration);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ConfigurationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuration";
            Load += ConfigurationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
