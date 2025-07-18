using System.Windows.Forms;
using Diagraph.Controls.Buttons;
using MaterialSkin.Controls;
using MaterialSkinPlus.Controls;
using ResmarkPrinterGroupDemo.Resources;

namespace ResmarkPrinterGroupDemo
{
    partial class GroupMainForm
    {
        private System.ComponentModel.IContainer components = null;

        private FlexButton btnAddPrinter;
        private FlexButton btnEditVariables;
        private FlexButton btnRemovePrinter;
        private FlexButton btnScan;
        private FlexButton btnSendMessage;
        private ComboBox cmbLanguage;
        private ComboBox cmbMessages;
        private DataGridView gridStatus;
        private MaterialLabel lblStatus;
        private ListBox listPrinters;
        private ListBox listScanResults;
        private MaterialSingleLineTextField txtPrintCount;
        private MaterialLabel lblPrintCount; 
        private FlexButton btnPause;
        private FlexButton btnResume;
        private FlexButton btnStop;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            btnAddPrinter = new FlexButton();
            btnEditVariables = new FlexButton();
            btnRemovePrinter = new FlexButton();
            btnScan = new FlexButton();
            btnSendMessage = new FlexButton();
            cmbLanguage = new ComboBox();
            cmbMessages = new ComboBox();
            gridStatus = new DataGridView();
            lblStatus = new MaterialLabel();
            listPrinters = new ListBox();
            listScanResults = new ListBox();
            txtPrintCount = new MaterialSingleLineTextField();
            lblPrintCount = new MaterialLabel();
            lblPrinters = new MaterialLabel();
            btnPause = new FlexButton();
            btnResume = new FlexButton();
            btnStop = new FlexButton();
            cboSelectMessage = new ComboBox();
            lblAvailableMessages = new MaterialLabel();
            btnSelectMessage = new FlexButton();
            lblMessage = new MaterialLabel();
            lblStatusList = new MaterialLabel();
            btnRefreshStatus = new FlexButton();
            ((System.ComponentModel.ISupportInitialize)gridStatus).BeginInit();
            SuspendLayout();
            // 
            // btnAddPrinter
            // 
            btnAddPrinter.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnAddPrinter.ButtonImage = null;
            btnAddPrinter.ButtonText = ">>";
            btnAddPrinter.CanGetFocus = true;
            btnAddPrinter.CenterText = false;
            btnAddPrinter.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnAddPrinter.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnAddPrinter.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnAddPrinter.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnAddPrinter.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnAddPrinter.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnAddPrinter.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnAddPrinter.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnAddPrinter.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnAddPrinter.ControlText = ">>";
            btnAddPrinter.CornerRadius = 4;
            btnAddPrinter.DriverAssignmentKeys = null;
            btnAddPrinter.EnableBlink = false;
            btnAddPrinter.FlatStyle = FlatStyle.Flat;
            btnAddPrinter.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnAddPrinter.GradientPosition = 0.4F;
            btnAddPrinter.ImageAboveText = false;
            btnAddPrinter.ImageHeight = 0;
            btnAddPrinter.IsAutoSizing = false;
            btnAddPrinter.IsSelectable = false;
            btnAddPrinter.IsSelected = false;
            btnAddPrinter.Location = new System.Drawing.Point(341, 202);
            btnAddPrinter.LoggingEnabled = true;
            btnAddPrinter.MirrorImage = false;
            btnAddPrinter.Name = "btnAddPrinter";
            btnAddPrinter.Password_Category = null;
            btnAddPrinter.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnAddPrinter.Password_Name = null;
            btnAddPrinter.PrinterGroup_Category = null;
            btnAddPrinter.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnAddPrinter.PrinterGroup_Name = null;
            btnAddPrinter.Size = new System.Drawing.Size(33, 33);
            btnAddPrinter.SupportedPrinter = null;
            btnAddPrinter.SuppressSelect = false;
            btnAddPrinter.SuppressUpdate = false;
            btnAddPrinter.TabIndex = 2;
            btnAddPrinter.Text = ">>";
            btnAddPrinter.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnAddPrinter.TextCorrection = 0;
            btnAddPrinter.UpdateEnabled = true;
            btnAddPrinter.UseMouseOver = false;
            btnAddPrinter.VersionKey = null;
            btnAddPrinter.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnAddPrinter.Click += btnAddPrinter_Click;
            // 
            // btnEditVariables
            // 
            btnEditVariables.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditVariables.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnEditVariables.ButtonImage = null;
            btnEditVariables.ButtonText = Resource.EditVariables;
            btnEditVariables.CanGetFocus = true;
            btnEditVariables.CenterText = false;
            btnEditVariables.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnEditVariables.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnEditVariables.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnEditVariables.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnEditVariables.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnEditVariables.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnEditVariables.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnEditVariables.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnEditVariables.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnEditVariables.ControlText = Resource.EditVariables;
            btnEditVariables.CornerRadius = 4;
            btnEditVariables.DriverAssignmentKeys = null;
            btnEditVariables.EnableBlink = false;
            btnEditVariables.FlatStyle = FlatStyle.Flat;
            btnEditVariables.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnEditVariables.GradientPosition = 0.4F;
            btnEditVariables.ImageAboveText = false;
            btnEditVariables.ImageHeight = 0;
            btnEditVariables.IsAutoSizing = false;
            btnEditVariables.IsSelectable = false;
            btnEditVariables.IsSelected = false;
            btnEditVariables.Location = new System.Drawing.Point(23, 556);
            btnEditVariables.LoggingEnabled = true;
            btnEditVariables.MirrorImage = false;
            btnEditVariables.Name = "btnEditVariables";
            btnEditVariables.Password_Category = null;
            btnEditVariables.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnEditVariables.Password_Name = null;
            btnEditVariables.PrinterGroup_Category = null;
            btnEditVariables.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnEditVariables.PrinterGroup_Name = null;
            btnEditVariables.Size = new System.Drawing.Size(300, 33);
            btnEditVariables.SupportedPrinter = null;
            btnEditVariables.SuppressSelect = false;
            btnEditVariables.SuppressUpdate = false;
            btnEditVariables.TabIndex = 13;
            btnEditVariables.Text = Resource.EditVariables;
            btnEditVariables.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnEditVariables.TextCorrection = 0;
            btnEditVariables.UpdateEnabled = true;
            btnEditVariables.UseMouseOver = false;
            btnEditVariables.VersionKey = null;
            btnEditVariables.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnEditVariables.Click += btnEditVariables_Click;
            // 
            // btnRemovePrinter
            // 
            btnRemovePrinter.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnRemovePrinter.ButtonImage = null;
            btnRemovePrinter.ButtonText = Resource.RemovePrinter;
            btnRemovePrinter.CanGetFocus = true;
            btnRemovePrinter.CenterText = false;
            btnRemovePrinter.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnRemovePrinter.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnRemovePrinter.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRemovePrinter.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRemovePrinter.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnRemovePrinter.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnRemovePrinter.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRemovePrinter.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRemovePrinter.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnRemovePrinter.ControlText = Resource.RemovePrinter;
            btnRemovePrinter.CornerRadius = 4;
            btnRemovePrinter.DriverAssignmentKeys = null;
            btnRemovePrinter.EnableBlink = false;
            btnRemovePrinter.FlatStyle = FlatStyle.Flat;
            btnRemovePrinter.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnRemovePrinter.GradientPosition = 0.4F;
            btnRemovePrinter.ImageAboveText = false;
            btnRemovePrinter.ImageHeight = 0;
            btnRemovePrinter.IsAutoSizing = false;
            btnRemovePrinter.IsSelectable = false;
            btnRemovePrinter.IsSelected = false;
            btnRemovePrinter.Location = new System.Drawing.Point(515, 290);
            btnRemovePrinter.LoggingEnabled = true;
            btnRemovePrinter.MirrorImage = false;
            btnRemovePrinter.Name = "btnRemovePrinter";
            btnRemovePrinter.Password_Category = null;
            btnRemovePrinter.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRemovePrinter.Password_Name = null;
            btnRemovePrinter.PrinterGroup_Category = null;
            btnRemovePrinter.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRemovePrinter.PrinterGroup_Name = null;
            btnRemovePrinter.Size = new System.Drawing.Size(100, 33);
            btnRemovePrinter.SupportedPrinter = null;
            btnRemovePrinter.SuppressSelect = false;
            btnRemovePrinter.SuppressUpdate = false;
            btnRemovePrinter.TabIndex = 4;
            btnRemovePrinter.Text = Resource.RemovePrinter;
            btnRemovePrinter.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnRemovePrinter.TextCorrection = 0;
            btnRemovePrinter.UpdateEnabled = true;
            btnRemovePrinter.UseMouseOver = false;
            btnRemovePrinter.VersionKey = null;
            btnRemovePrinter.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnRemovePrinter.Click += btnRemovePrinter_Click;
            // 
            // btnScan
            // 
            btnScan.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnScan.ButtonImage = null;
            btnScan.ButtonText = Resource.ScanPrinters;
            btnScan.CanGetFocus = true;
            btnScan.CenterText = false;
            btnScan.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnScan.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnScan.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnScan.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnScan.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnScan.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnScan.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnScan.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnScan.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnScan.ControlText = Resource.ScanPrinters;
            btnScan.CornerRadius = 4;
            btnScan.DriverAssignmentKeys = null;
            btnScan.EnableBlink = false;
            btnScan.FlatStyle = FlatStyle.Flat;
            btnScan.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnScan.GradientPosition = 0.4F;
            btnScan.ImageAboveText = false;
            btnScan.ImageHeight = 0;
            btnScan.IsAutoSizing = false;
            btnScan.IsSelectable = false;
            btnScan.IsSelected = false;
            btnScan.Location = new System.Drawing.Point(23, 290);
            btnScan.LoggingEnabled = true;
            btnScan.MirrorImage = false;
            btnScan.Name = "btnScan";
            btnScan.Password_Category = null;
            btnScan.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnScan.Password_Name = null;
            btnScan.PrinterGroup_Category = null;
            btnScan.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnScan.PrinterGroup_Name = null;
            btnScan.Size = new System.Drawing.Size(300, 33);
            btnScan.SupportedPrinter = null;
            btnScan.SuppressSelect = false;
            btnScan.SuppressUpdate = false;
            btnScan.TabIndex = 5;
            btnScan.Text = Resource.ScanPrinters;
            btnScan.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnScan.TextCorrection = 0;
            btnScan.UpdateEnabled = true;
            btnScan.UseMouseOver = false;
            btnScan.VersionKey = null;
            btnScan.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnScan.Click += btnScan_Click;
            // 
            // btnSendMessage
            // 
            btnSendMessage.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnSendMessage.ButtonImage = null;
            btnSendMessage.ButtonText = Resource.SendToAll;
            btnSendMessage.CanGetFocus = true;
            btnSendMessage.CenterText = false;
            btnSendMessage.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnSendMessage.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnSendMessage.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSendMessage.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSendMessage.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSendMessage.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnSendMessage.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSendMessage.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSendMessage.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnSendMessage.ControlText = Resource.SendToAll;
            btnSendMessage.CornerRadius = 4;
            btnSendMessage.DriverAssignmentKeys = null;
            btnSendMessage.EnableBlink = false;
            btnSendMessage.FlatStyle = FlatStyle.Flat;
            btnSendMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnSendMessage.GradientPosition = 0.4F;
            btnSendMessage.ImageAboveText = false;
            btnSendMessage.ImageHeight = 0;
            btnSendMessage.IsAutoSizing = false;
            btnSendMessage.IsSelectable = false;
            btnSendMessage.IsSelected = false;
            btnSendMessage.Location = new System.Drawing.Point(23, 482);
            btnSendMessage.LoggingEnabled = true;
            btnSendMessage.MirrorImage = false;
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Password_Category = null;
            btnSendMessage.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSendMessage.Password_Name = null;
            btnSendMessage.PrinterGroup_Category = null;
            btnSendMessage.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSendMessage.PrinterGroup_Name = null;
            btnSendMessage.Size = new System.Drawing.Size(300, 33);
            btnSendMessage.SupportedPrinter = null;
            btnSendMessage.SuppressSelect = false;
            btnSendMessage.SuppressUpdate = false;
            btnSendMessage.TabIndex = 11;
            btnSendMessage.Text = Resource.SendToAll;
            btnSendMessage.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnSendMessage.TextCorrection = 0;
            btnSendMessage.UpdateEnabled = true;
            btnSendMessage.UseMouseOver = false;
            btnSendMessage.VersionKey = null;
            btnSendMessage.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Location = new System.Drawing.Point(825, 85);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new System.Drawing.Size(150, 23);
            cmbLanguage.TabIndex = 14;
            // 
            // cmbMessages
            // 
            cmbMessages.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMessages.Location = new System.Drawing.Point(23, 402);
            cmbMessages.Name = "cmbMessages";
            cmbMessages.Size = new System.Drawing.Size(300, 23);
            cmbMessages.TabIndex = 8;
            // 
            // gridStatus
            // 
            gridStatus.AllowUserToAddRows = false;
            gridStatus.AllowUserToDeleteRows = false;
            gridStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridStatus.Location = new System.Drawing.Point(389, 402);
            gridStatus.Name = "gridStatus";
            gridStatus.ReadOnly = true;
            gridStatus.Size = new System.Drawing.Size(560, 187);
            gridStatus.TabIndex = 15;
            // 
            // lblStatus
            // 
            lblStatus.Depth = 0;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Font = new System.Drawing.Font("Roboto", 11F);
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblStatus.Location = new System.Drawing.Point(0, 616);
            lblStatus.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(1002, 23);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Status: ready";
            // 
            // listPrinters
            // 
            listPrinters.Location = new System.Drawing.Point(389, 145);
            listPrinters.Name = "listPrinters";
            listPrinters.Size = new System.Drawing.Size(226, 139);
            listPrinters.TabIndex = 3;
            listPrinters.SelectedIndexChanged += listPrinters_SelectedIndexChanged;
            // 
            // listScanResults
            // 
            listScanResults.Location = new System.Drawing.Point(23, 145);
            listScanResults.Name = "listScanResults";
            listScanResults.Size = new System.Drawing.Size(300, 139);
            listScanResults.TabIndex = 6;
            listScanResults.DoubleClick += listScanResults_DoubleClick;
            // 
            // txtPrintCount
            // 
            txtPrintCount.Depth = 0;
            txtPrintCount.Hint = null;
            txtPrintCount.Location = new System.Drawing.Point(123, 442);
            txtPrintCount.MaxLength = 32767;
            txtPrintCount.MouseState = MaterialSkinPlus.MouseState.HOVER;
            txtPrintCount.Name = "txtPrintCount";
            txtPrintCount.PasswordChar = '\0';
            txtPrintCount.SelectedText = "";
            txtPrintCount.SelectionLength = 0;
            txtPrintCount.SelectionStart = 0;
            txtPrintCount.Size = new System.Drawing.Size(100, 23);
            txtPrintCount.TabIndex = 10;
            txtPrintCount.TabStop = false;
            txtPrintCount.Text = "1";
            txtPrintCount.UseSystemPasswordChar = false;
            // 
            // lblPrintCount
            // 
            lblPrintCount.Depth = 0;
            lblPrintCount.Font = new System.Drawing.Font("Roboto", 11F);
            lblPrintCount.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblPrintCount.Location = new System.Drawing.Point(23, 442);
            lblPrintCount.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblPrintCount.Name = "lblPrintCount";
            lblPrintCount.Size = new System.Drawing.Size(100, 23);
            lblPrintCount.TabIndex = 9;
            lblPrintCount.Text = "Print Count";
            // 
            // lblPrinters
            // 
            lblPrinters.Depth = 0;
            lblPrinters.Font = new System.Drawing.Font("Roboto", 11F);
            lblPrinters.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblPrinters.Location = new System.Drawing.Point(23, 119);
            lblPrinters.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblPrinters.Name = "lblPrinters";
            lblPrinters.Size = new System.Drawing.Size(100, 23);
            lblPrinters.TabIndex = 16;
            lblPrinters.Text = "Printers";
            // 
            // btnPause
            // 
            btnPause.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnPause.ButtonImage = null;
            btnPause.ButtonText = Resource.Pause;
            btnPause.CanGetFocus = true;
            btnPause.CenterText = false;
            btnPause.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnPause.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnPause.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnPause.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnPause.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnPause.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnPause.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnPause.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnPause.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnPause.ControlText = Resource.Pause;
            btnPause.CornerRadius = 4;
            btnPause.DriverAssignmentKeys = null;
            btnPause.EnableBlink = false;
            btnPause.FlatStyle = FlatStyle.Flat;
            btnPause.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnPause.GradientPosition = 0.4F;
            btnPause.ImageAboveText = false;
            btnPause.ImageHeight = 0;
            btnPause.IsAutoSizing = false;
            btnPause.IsSelectable = false;
            btnPause.IsSelected = false;
            btnPause.Location = new System.Drawing.Point(634, 145);
            btnPause.LoggingEnabled = true;
            btnPause.MirrorImage = false;
            btnPause.Name = "btnPause";
            btnPause.Password_Category = null;
            btnPause.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnPause.Password_Name = null;
            btnPause.PrinterGroup_Category = null;
            btnPause.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnPause.PrinterGroup_Name = null;
            btnPause.Size = new System.Drawing.Size(95, 30);
            btnPause.SupportedPrinter = null;
            btnPause.SuppressSelect = false;
            btnPause.SuppressUpdate = false;
            btnPause.TabIndex = 0;
            btnPause.Text = Resource.Pause;
            btnPause.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnPause.TextCorrection = 0;
            btnPause.UpdateEnabled = true;
            btnPause.UseMouseOver = false;
            btnPause.VersionKey = null;
            btnPause.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnPause.Click += btnPause_Click;
            // 
            // btnResume
            // 
            btnResume.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnResume.ButtonImage = null;
            btnResume.ButtonText = Resource.Resume;
            btnResume.CanGetFocus = true;
            btnResume.CenterText = false;
            btnResume.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnResume.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnResume.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnResume.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnResume.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnResume.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnResume.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnResume.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnResume.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnResume.ControlText = Resource.Resume;
            btnResume.CornerRadius = 4;
            btnResume.DriverAssignmentKeys = null;
            btnResume.EnableBlink = false;
            btnResume.FlatStyle = FlatStyle.Flat;
            btnResume.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnResume.GradientPosition = 0.4F;
            btnResume.ImageAboveText = false;
            btnResume.ImageHeight = 0;
            btnResume.IsAutoSizing = false;
            btnResume.IsSelectable = false;
            btnResume.IsSelected = false;
            btnResume.Location = new System.Drawing.Point(634, 184);
            btnResume.LoggingEnabled = true;
            btnResume.MirrorImage = false;
            btnResume.Name = "btnResume";
            btnResume.Password_Category = null;
            btnResume.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnResume.Password_Name = null;
            btnResume.PrinterGroup_Category = null;
            btnResume.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnResume.PrinterGroup_Name = null;
            btnResume.Size = new System.Drawing.Size(95, 30);
            btnResume.SupportedPrinter = null;
            btnResume.SuppressSelect = false;
            btnResume.SuppressUpdate = false;
            btnResume.TabIndex = 1;
            btnResume.Text = Resource.Resume;
            btnResume.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnResume.TextCorrection = 0;
            btnResume.UpdateEnabled = true;
            btnResume.UseMouseOver = false;
            btnResume.VersionKey = null;
            btnResume.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnResume.Click += btnResume_Click;
            // 
            // btnStop
            // 
            btnStop.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnStop.ButtonImage = null;
            btnStop.ButtonText = Resource.Stop;
            btnStop.CanGetFocus = true;
            btnStop.CenterText = false;
            btnStop.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnStop.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnStop.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnStop.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnStop.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnStop.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnStop.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnStop.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnStop.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnStop.ControlText = Resource.Stop;
            btnStop.CornerRadius = 4;
            btnStop.DriverAssignmentKeys = null;
            btnStop.EnableBlink = false;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnStop.GradientPosition = 0.4F;
            btnStop.ImageAboveText = false;
            btnStop.ImageHeight = 0;
            btnStop.IsAutoSizing = false;
            btnStop.IsSelectable = false;
            btnStop.IsSelected = false;
            btnStop.Location = new System.Drawing.Point(634, 224);
            btnStop.LoggingEnabled = true;
            btnStop.MirrorImage = false;
            btnStop.Name = "btnStop";
            btnStop.Password_Category = null;
            btnStop.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnStop.Password_Name = null;
            btnStop.PrinterGroup_Category = null;
            btnStop.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnStop.PrinterGroup_Name = null;
            btnStop.Size = new System.Drawing.Size(95, 30);
            btnStop.SupportedPrinter = null;
            btnStop.SuppressSelect = false;
            btnStop.SuppressUpdate = false;
            btnStop.TabIndex = 2;
            btnStop.Text = Resource.Stop;
            btnStop.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnStop.TextCorrection = 0;
            btnStop.UpdateEnabled = true;
            btnStop.UseMouseOver = false;
            btnStop.VersionKey = null;
            btnStop.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnStop.Click += btnStop_Click;
            // 
            // cboSelectMessage
            // 
            cboSelectMessage.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSelectMessage.Location = new System.Drawing.Point(753, 171);
            cboSelectMessage.Name = "cboSelectMessage";
            cboSelectMessage.Size = new System.Drawing.Size(222, 23);
            cboSelectMessage.TabIndex = 17;
            // 
            // lblAvailableMessages
            // 
            lblAvailableMessages.Depth = 0;
            lblAvailableMessages.Font = new System.Drawing.Font("Roboto", 11F);
            lblAvailableMessages.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblAvailableMessages.Location = new System.Drawing.Point(751, 145);
            lblAvailableMessages.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblAvailableMessages.Name = "lblAvailableMessages";
            lblAvailableMessages.Size = new System.Drawing.Size(200, 23);
            lblAvailableMessages.TabIndex = 18;
            lblAvailableMessages.Text = "Available Messages";
            // 
            // btnSelectMessage
            // 
            btnSelectMessage.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnSelectMessage.ButtonImage = null;
            btnSelectMessage.ButtonText = "Select Message";
            btnSelectMessage.CanGetFocus = true;
            btnSelectMessage.CenterText = false;
            btnSelectMessage.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnSelectMessage.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnSelectMessage.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSelectMessage.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnSelectMessage.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSelectMessage.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnSelectMessage.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSelectMessage.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnSelectMessage.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnSelectMessage.ControlText = "Select Message";
            btnSelectMessage.CornerRadius = 4;
            btnSelectMessage.DriverAssignmentKeys = null;
            btnSelectMessage.EnableBlink = false;
            btnSelectMessage.FlatStyle = FlatStyle.Flat;
            btnSelectMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnSelectMessage.GradientPosition = 0.4F;
            btnSelectMessage.ImageAboveText = false;
            btnSelectMessage.ImageHeight = 0;
            btnSelectMessage.IsAutoSizing = false;
            btnSelectMessage.IsSelectable = false;
            btnSelectMessage.IsSelected = false;
            btnSelectMessage.Location = new System.Drawing.Point(834, 205);
            btnSelectMessage.LoggingEnabled = true;
            btnSelectMessage.MirrorImage = false;
            btnSelectMessage.Name = "btnSelectMessage";
            btnSelectMessage.Password_Category = null;
            btnSelectMessage.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSelectMessage.Password_Name = null;
            btnSelectMessage.PrinterGroup_Category = null;
            btnSelectMessage.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnSelectMessage.PrinterGroup_Name = null;
            btnSelectMessage.Size = new System.Drawing.Size(141, 30);
            btnSelectMessage.SupportedPrinter = null;
            btnSelectMessage.SuppressSelect = false;
            btnSelectMessage.SuppressUpdate = false;
            btnSelectMessage.TabIndex = 19;
            btnSelectMessage.Text = "Select Message";
            btnSelectMessage.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnSelectMessage.TextCorrection = 0;
            btnSelectMessage.UpdateEnabled = true;
            btnSelectMessage.UseMouseOver = false;
            btnSelectMessage.VersionKey = null;
            btnSelectMessage.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnSelectMessage.Click += btnSelectMessage_Click;
            // 
            // lblMessage
            // 
            lblMessage.Depth = 0;
            lblMessage.Font = new System.Drawing.Font("Roboto", 11F);
            lblMessage.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblMessage.Location = new System.Drawing.Point(23, 372);
            lblMessage.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(200, 23);
            lblMessage.TabIndex = 7;
            lblMessage.Text = "Message File (XML)";
            // 
            // lblStatusList
            // 
            lblStatusList.Depth = 0;
            lblStatusList.Font = new System.Drawing.Font("Roboto", 11F);
            lblStatusList.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            lblStatusList.Location = new System.Drawing.Point(389, 376);
            lblStatusList.MouseState = MaterialSkinPlus.MouseState.HOVER;
            lblStatusList.Name = "lblStatusList";
            lblStatusList.Size = new System.Drawing.Size(200, 23);
            lblStatusList.TabIndex = 20;
            lblStatusList.Text = "Status";
            // 
            // btnRefreshStatus
            // 
            btnRefreshStatus.BlinkType = Diagraph.Controls.Global.ErrorType.None;
            btnRefreshStatus.ButtonImage = null;
            btnRefreshStatus.ButtonText = "Refresh";
            btnRefreshStatus.CanGetFocus = true;
            btnRefreshStatus.CenterText = false;
            btnRefreshStatus.Color = System.Drawing.Color.FromArgb(153, 180, 209);
            btnRefreshStatus.Color_Border = System.Drawing.Color.FromArgb(105, 105, 105);
            btnRefreshStatus.Color_Disabled = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRefreshStatus.Color_Glow = System.Drawing.Color.FromArgb(191, 205, 219);
            btnRefreshStatus.Color_Hover = System.Drawing.Color.FromArgb(0, 120, 215);
            btnRefreshStatus.Color_Selected = System.Drawing.Color.FromArgb(255, 184, 77);
            btnRefreshStatus.Color_Selected_Border = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRefreshStatus.Color_Selected_Glow = System.Drawing.Color.FromArgb(255, 194, 47);
            btnRefreshStatus.Color_Selected_Hover = System.Drawing.Color.FromArgb(255, 198, 102);
            btnRefreshStatus.ControlText = "Refresh";
            btnRefreshStatus.CornerRadius = 4;
            btnRefreshStatus.DriverAssignmentKeys = null;
            btnRefreshStatus.EnableBlink = false;
            btnRefreshStatus.FlatStyle = FlatStyle.Flat;
            btnRefreshStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnRefreshStatus.GradientPosition = 0.4F;
            btnRefreshStatus.ImageAboveText = false;
            btnRefreshStatus.ImageHeight = 0;
            btnRefreshStatus.IsAutoSizing = false;
            btnRefreshStatus.IsSelectable = false;
            btnRefreshStatus.IsSelected = false;
            btnRefreshStatus.Location = new System.Drawing.Point(849, 363);
            btnRefreshStatus.LoggingEnabled = true;
            btnRefreshStatus.MirrorImage = false;
            btnRefreshStatus.Name = "btnRefreshStatus";
            btnRefreshStatus.Password_Category = null;
            btnRefreshStatus.Password_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRefreshStatus.Password_Name = null;
            btnRefreshStatus.PrinterGroup_Category = null;
            btnRefreshStatus.PrinterGroup_ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            btnRefreshStatus.PrinterGroup_Name = null;
            btnRefreshStatus.Size = new System.Drawing.Size(100, 33);
            btnRefreshStatus.SupportedPrinter = null;
            btnRefreshStatus.SuppressSelect = false;
            btnRefreshStatus.SuppressUpdate = false;
            btnRefreshStatus.TabIndex = 21;
            btnRefreshStatus.Text = "Refresh";
            btnRefreshStatus.TextColor = System.Drawing.Color.FromArgb(32, 77, 137);
            btnRefreshStatus.TextCorrection = 0;
            btnRefreshStatus.UpdateEnabled = true;
            btnRefreshStatus.UseMouseOver = false;
            btnRefreshStatus.VersionKey = null;
            btnRefreshStatus.ViewMode = Diagraph.Controls.ViewMode.Simple;
            btnRefreshStatus.Click += btnRefreshStatus_ClickAsync;
            // 
            // GroupMainForm
            // 
            ClientSize = new System.Drawing.Size(1002, 639);
            Controls.Add(btnRefreshStatus);
            Controls.Add(lblStatusList);
            Controls.Add(btnSelectMessage);
            Controls.Add(lblAvailableMessages);
            Controls.Add(cboSelectMessage);
            Controls.Add(btnPause);
            Controls.Add(btnResume);
            Controls.Add(btnStop);
            Controls.Add(lblPrinters);
            Controls.Add(btnAddPrinter);
            Controls.Add(listPrinters);
            Controls.Add(btnRemovePrinter);
            Controls.Add(btnScan);
            Controls.Add(listScanResults);
            Controls.Add(lblMessage);
            Controls.Add(cmbMessages);
            Controls.Add(lblPrintCount);
            Controls.Add(txtPrintCount);
            Controls.Add(btnSendMessage);
            Controls.Add(lblStatus);
            Controls.Add(btnEditVariables);
            Controls.Add(cmbLanguage);
            Controls.Add(gridStatus);
            Name = "GroupMainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Resmark Printer Group Demo";
            FormClosing += GroupMainForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)gridStatus).EndInit();
            ResumeLayout(false);
        }
        private MaterialLabel lblPrinters;
        private ComboBox cboSelectMessage;
        private MaterialLabel lblAvailableMessages;
        private FlexButton btnSelectMessage;
        private MaterialLabel lblMessage;
        private MaterialLabel lblStatusList;
        private FlexButton btnRefreshStatus;
    }
}
