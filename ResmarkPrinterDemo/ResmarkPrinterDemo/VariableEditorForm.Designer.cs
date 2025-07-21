using Diagraph.Controls.Buttons;

namespace ResmarkPrinterGroupDemo
{
    partial class VariableEditorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridVariables;
        private FlexButton btnSave;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariableEditorForm));
            dataGridVariables = new System.Windows.Forms.DataGridView();
            btnSave = new FlexButton();
            ((System.ComponentModel.ISupportInitialize)dataGridVariables).BeginInit();
            SuspendLayout();
            // 
            // dataGridVariables
            // 
            dataGridVariables.AllowUserToAddRows = false;
            dataGridVariables.AllowUserToDeleteRows = false;
            dataGridVariables.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridVariables.Location = new System.Drawing.Point(12, 79);
            dataGridVariables.Name = "dataGridVariables";
            dataGridVariables.Size = new System.Drawing.Size(560, 316);
            dataGridVariables.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
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
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnSave.GradientPosition = 0.4F;
            btnSave.ImageAboveText = false;
            btnSave.ImageHeight = 0;
            btnSave.IsAutoSizing = false;
            btnSave.IsSelectable = false;
            btnSave.IsSelected = false;
            btnSave.Location = new System.Drawing.Point(472, 412);
            btnSave.LoggingEnabled = true;
            btnSave.MirrorImage = false;
            btnSave.Name = "btnSave";
            btnSave.Password_Category = null;
            btnSave.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.Password_Name = null;
            btnSave.PrinterGroup_Category = null;
            btnSave.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.PrinterGroup_Name = null;
            btnSave.Size = new System.Drawing.Size(100, 30);
            btnSave.SupportedPrinter = null;
            btnSave.SuppressSelect = false;
            btnSave.SuppressUpdate = false;
            btnSave.TabIndex = 1;
            btnSave.Text = "Speichern";
            btnSave.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnSave.TextCorrection = 0;
            btnSave.UpdateEnabled = true;
            btnSave.UseMouseOver = false;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.VersionKey = null;
            btnSave.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnSave.Click += btnSave_Click;
            // 
            // VariableEditorForm
            // 
            ClientSize = new System.Drawing.Size(584, 454);
            Controls.Add(btnSave);
            Controls.Add(dataGridVariables);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "VariableEditorForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Variablen";
            Load += VariableEditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridVariables).EndInit();
            ResumeLayout(false);
        }
    }
}
