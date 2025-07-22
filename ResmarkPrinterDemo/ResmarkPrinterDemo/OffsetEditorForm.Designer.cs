using MaterialSkinPlus.Controls;

namespace ResmarkPrinterGroupDemo
{
    partial class OffsetEditorForm
    {
        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridOffsets;
        private Diagraph.Controls.Buttons.FlexButton btnSave;
        private Diagraph.Controls.Buttons.FlexButton btnCancel;
        private Diagraph.Controls.Buttons.FlexButton btnRemove;
        private MaterialLabel lblPrinterId;
        private MaterialLabel lblIp;
        private MaterialLabel lblMessage;
        private MaterialLabel lblOffset;
        private Diagraph.Controls.Buttons.FlexButton btnAddOrUpdate;

        private void InitializeComponent()
        {
            dataGridOffsets = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            btnSave = new Diagraph.Controls.Buttons.FlexButton();
            btnCancel = new Diagraph.Controls.Buttons.FlexButton();
            btnRemove = new Diagraph.Controls.Buttons.FlexButton();
            txtPrinterId = new System.Windows.Forms.TextBox();
            txtIp = new System.Windows.Forms.TextBox();
            txtMessage = new System.Windows.Forms.TextBox();
            txtOffset = new System.Windows.Forms.TextBox();
            lblPrinterId = new MaterialLabel();
            lblIp = new MaterialLabel();
            lblMessage = new MaterialLabel();
            lblOffset = new MaterialLabel();
            btnAddOrUpdate = new Diagraph.Controls.Buttons.FlexButton();
            ((System.ComponentModel.ISupportInitialize)dataGridOffsets).BeginInit();
            SuspendLayout();
            // 
            // dataGridOffsets
            // 
            dataGridOffsets.AllowUserToAddRows = false;
            dataGridOffsets.AllowUserToDeleteRows = false;
            dataGridOffsets.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridOffsets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridOffsets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridOffsets.Location = new System.Drawing.Point(12, 82);
            dataGridOffsets.Name = "dataGridOffsets";
            dataGridOffsets.Size = new System.Drawing.Size(796, 455);
            dataGridOffsets.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Printer ID";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "IP Address";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Message Name";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Offset";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
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
            btnSave.Location = new System.Drawing.Point(739, 623);
            btnSave.LoggingEnabled = true;
            btnSave.MirrorImage = false;
            btnSave.Name = "btnSave";
            btnSave.Password_Category = null;
            btnSave.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.Password_Name = null;
            btnSave.PrinterGroup_Category = null;
            btnSave.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSave.PrinterGroup_Name = null;
            btnSave.Size = new System.Drawing.Size(120, 40);
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
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
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
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnCancel.GradientPosition = 0.4F;
            btnCancel.ImageAboveText = false;
            btnCancel.ImageHeight = 0;
            btnCancel.IsAutoSizing = false;
            btnCancel.IsSelectable = false;
            btnCancel.IsSelected = false;
            btnCancel.Location = new System.Drawing.Point(865, 623);
            btnCancel.LoggingEnabled = true;
            btnCancel.MirrorImage = false;
            btnCancel.Name = "btnCancel";
            btnCancel.Password_Category = null;
            btnCancel.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnCancel.Password_Name = null;
            btnCancel.PrinterGroup_Category = null;
            btnCancel.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnCancel.PrinterGroup_Name = null;
            btnCancel.Size = new System.Drawing.Size(120, 40);
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
            // btnRemove
            // 
            btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRemove.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnRemove.ButtonImage = null;
            btnRemove.ButtonText = "Entfernen";
            btnRemove.CanGetFocus = true;
            btnRemove.CenterText = false;
            btnRemove.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnRemove.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnRemove.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRemove.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRemove.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnRemove.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnRemove.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRemove.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRemove.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnRemove.ControlText = "Entfernen";
            btnRemove.CornerRadius = 4;
            btnRemove.DriverAssignmentKeys = null;
            btnRemove.EnableBlink = false;
            btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRemove.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnRemove.GradientPosition = 0.4F;
            btnRemove.ImageAboveText = false;
            btnRemove.ImageHeight = 0;
            btnRemove.IsAutoSizing = false;
            btnRemove.IsSelectable = false;
            btnRemove.IsSelected = false;
            btnRemove.Location = new System.Drawing.Point(829, 82);
            btnRemove.LoggingEnabled = true;
            btnRemove.MirrorImage = false;
            btnRemove.Name = "btnRemove";
            btnRemove.Password_Category = null;
            btnRemove.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRemove.Password_Name = null;
            btnRemove.PrinterGroup_Category = null;
            btnRemove.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRemove.PrinterGroup_Name = null;
            btnRemove.Size = new System.Drawing.Size(156, 40);
            btnRemove.SupportedPrinter = null;
            btnRemove.SuppressSelect = false;
            btnRemove.SuppressUpdate = false;
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Entfernen";
            btnRemove.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnRemove.TextCorrection = 0;
            btnRemove.UpdateEnabled = true;
            btnRemove.UseMouseOver = false;
            btnRemove.VersionKey = null;
            btnRemove.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnRemove.Click += btnRemove_Click;
            // 
            // txtPrinterId
            // 
            txtPrinterId.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtPrinterId.Location = new System.Drawing.Point(123, 555);
            txtPrinterId.Name = "txtPrinterId";
            txtPrinterId.Size = new System.Drawing.Size(150, 23);
            txtPrinterId.TabIndex = 2;
            // 
            // txtIp
            // 
            txtIp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtIp.Location = new System.Drawing.Point(403, 555);
            txtIp.Name = "txtIp";
            txtIp.Size = new System.Drawing.Size(150, 23);
            txtIp.TabIndex = 4;
            // 
            // txtMessage
            // 
            txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtMessage.Location = new System.Drawing.Point(123, 585);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new System.Drawing.Size(150, 23);
            txtMessage.TabIndex = 6;
            // 
            // txtOffset
            // 
            txtOffset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtOffset.Location = new System.Drawing.Point(403, 585);
            txtOffset.Name = "txtOffset";
            txtOffset.Size = new System.Drawing.Size(150, 23);
            txtOffset.TabIndex = 8;
            // 
            // lblPrinterId
            // 
            lblPrinterId.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblPrinterId.Depth = 0;
            lblPrinterId.Font = new System.Drawing.Font("Roboto", 11F);
            lblPrinterId.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblPrinterId.Location = new System.Drawing.Point(15, 555);
            lblPrinterId.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblPrinterId.Name = "lblPrinterId";
            lblPrinterId.Size = new System.Drawing.Size(100, 23);
            lblPrinterId.TabIndex = 1;
            lblPrinterId.Text = "Printer ID:";
            // 
            // lblIp
            // 
            lblIp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblIp.Depth = 0;
            lblIp.Font = new System.Drawing.Font("Roboto", 11F);
            lblIp.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblIp.Location = new System.Drawing.Point(293, 555);
            lblIp.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblIp.Name = "lblIp";
            lblIp.Size = new System.Drawing.Size(100, 23);
            lblIp.TabIndex = 3;
            lblIp.Text = "IP Address:";
            // 
            // lblMessage
            // 
            lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblMessage.Depth = 0;
            lblMessage.Font = new System.Drawing.Font("Roboto", 11F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblMessage.Location = new System.Drawing.Point(15, 585);
            lblMessage.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(100, 23);
            lblMessage.TabIndex = 5;
            lblMessage.Text = "Message Name:";
            // 
            // lblOffset
            // 
            lblOffset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblOffset.Depth = 0;
            lblOffset.Font = new System.Drawing.Font("Roboto", 11F);
            lblOffset.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblOffset.Location = new System.Drawing.Point(293, 585);
            lblOffset.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblOffset.Name = "lblOffset";
            lblOffset.Size = new System.Drawing.Size(100, 23);
            lblOffset.TabIndex = 7;
            lblOffset.Text = "Offset:";
            // 
            // btnAddOrUpdate
            // 
            btnAddOrUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnAddOrUpdate.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnAddOrUpdate.ButtonImage = null;
            btnAddOrUpdate.ButtonText = "Add / Update";
            btnAddOrUpdate.CanGetFocus = true;
            btnAddOrUpdate.CenterText = false;
            btnAddOrUpdate.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnAddOrUpdate.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnAddOrUpdate.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnAddOrUpdate.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnAddOrUpdate.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnAddOrUpdate.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnAddOrUpdate.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnAddOrUpdate.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnAddOrUpdate.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnAddOrUpdate.ControlText = "Add / Update";
            btnAddOrUpdate.CornerRadius = 4;
            btnAddOrUpdate.DriverAssignmentKeys = null;
            btnAddOrUpdate.EnableBlink = false;
            btnAddOrUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddOrUpdate.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnAddOrUpdate.GradientPosition = 0.4F;
            btnAddOrUpdate.ImageAboveText = false;
            btnAddOrUpdate.ImageHeight = 0;
            btnAddOrUpdate.IsAutoSizing = false;
            btnAddOrUpdate.IsSelectable = false;
            btnAddOrUpdate.IsSelected = false;
            btnAddOrUpdate.Location = new System.Drawing.Point(829, 567);
            btnAddOrUpdate.LoggingEnabled = true;
            btnAddOrUpdate.MirrorImage = false;
            btnAddOrUpdate.Name = "btnAddOrUpdate";
            btnAddOrUpdate.Password_Category = null;
            btnAddOrUpdate.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnAddOrUpdate.Password_Name = null;
            btnAddOrUpdate.PrinterGroup_Category = null;
            btnAddOrUpdate.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnAddOrUpdate.PrinterGroup_Name = null;
            btnAddOrUpdate.Size = new System.Drawing.Size(156, 40);
            btnAddOrUpdate.SupportedPrinter = null;
            btnAddOrUpdate.SuppressSelect = false;
            btnAddOrUpdate.SuppressUpdate = false;
            btnAddOrUpdate.TabIndex = 9;
            btnAddOrUpdate.Text = "Add / Update";
            btnAddOrUpdate.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnAddOrUpdate.TextCorrection = 0;
            btnAddOrUpdate.UpdateEnabled = true;
            btnAddOrUpdate.UseMouseOver = false;
            btnAddOrUpdate.VersionKey = null;
            btnAddOrUpdate.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnAddOrUpdate.Click += btnAddOrUpdate_Click;
            // 
            // OffsetEditorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(997, 679);
            Controls.Add(dataGridOffsets);
            Controls.Add(lblPrinterId);
            Controls.Add(txtPrinterId);
            Controls.Add(lblIp);
            Controls.Add(txtIp);
            Controls.Add(lblMessage);
            Controls.Add(txtMessage);
            Controls.Add(lblOffset);
            Controls.Add(txtOffset);
            Controls.Add(btnAddOrUpdate);
            Controls.Add(btnRemove);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            MinimumSize = new System.Drawing.Size(845, 373);
            Name = "OffsetEditorForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Offsets bearbeiten";
            Load += OffsetEditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridOffsets).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        public System.Windows.Forms.TextBox txtPrinterId;
        public System.Windows.Forms.TextBox txtIp;
        public System.Windows.Forms.TextBox txtMessage;
        public System.Windows.Forms.TextBox txtOffset;
    }
}
