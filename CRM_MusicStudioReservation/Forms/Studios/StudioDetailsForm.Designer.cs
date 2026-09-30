namespace CRM_MusicStudioReservation.Forms.Studios
{
    partial class StudioDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnCloseHeader = new System.Windows.Forms.Button();

            this.pnlStudioInfo = new System.Windows.Forms.Panel();
            this.lblInfoCode = new System.Windows.Forms.Label();
            this.lblInfoType = new System.Windows.Forms.Label();
            this.lblInfoRate = new System.Windows.Forms.Label();
            this.lblInfoCapacity = new System.Windows.Forms.Label();
            this.lblInfoStatus = new System.Windows.Forms.Label();
            this.lblInfoDescription = new System.Windows.Forms.Label();

            this.pnlSectionHeader = new System.Windows.Forms.Panel();
            this.lblInventoryTitle = new System.Windows.Forms.Label();
            this.lblInventoryCount = new System.Windows.Forms.Label();

            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvEquipment = new System.Windows.Forms.DataGridView();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlStudioInfo.SuspendLayout();
            this.pnlSectionHeader.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== StudioDetailsForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 520);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.Name = "StudioDetailsForm";
            this.Text = "Studio Details & Equipment";
            this.Load += new System.EventHandler(this.StudioDetailsForm_Load);

            // ==== Header Panel ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 56;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 8, 24, 6);
            this.pnlHeader.Name = "pnlHeader";

            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(24, 8);
            this.lblTitle.Text = "Studio Details";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(24, 32);
            this.lblSubtitle.Text = "Room information and assigned studio equipment";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.btnCloseHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseHeader.BackColor = System.Drawing.Color.White;
            this.btnCloseHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseHeader.FlatAppearance.BorderSize = 0;
            this.btnCloseHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCloseHeader.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.btnCloseHeader.Location = new System.Drawing.Point(868, 10);
            this.btnCloseHeader.Name = "btnCloseHeader";
            this.btnCloseHeader.Size = new System.Drawing.Size(36, 32);
            this.btnCloseHeader.TabIndex = 99;
            this.btnCloseHeader.Text = "✕";
            this.btnCloseHeader.UseVisualStyleBackColor = false;
            this.btnCloseHeader.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlHeader.Controls.Add(this.btnCloseHeader);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Studio Info Card ====
            this.pnlStudioInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStudioInfo.Height = 68;
            this.pnlStudioInfo.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlStudioInfo.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.pnlStudioInfo.Name = "pnlStudioInfo";

            this.lblInfoCode.Location = new System.Drawing.Point(24, 8);
            this.lblInfoCode.Size = new System.Drawing.Size(180, 20);
            this.lblInfoCode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblInfoCode.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblInfoCode.Text = "Code: STD001";
            this.lblInfoCode.Name = "lblInfoCode";

            this.lblInfoType.Location = new System.Drawing.Point(210, 8);
            this.lblInfoType.Size = new System.Drawing.Size(180, 20);
            this.lblInfoType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblInfoType.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblInfoType.Text = "Type: Rehearsal";
            this.lblInfoType.Name = "lblInfoType";

            this.lblInfoRate.Location = new System.Drawing.Point(400, 8);
            this.lblInfoRate.Size = new System.Drawing.Size(220, 20);
            this.lblInfoRate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblInfoRate.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblInfoRate.Text = "Hourly Rate: ₱300.00";
            this.lblInfoRate.Name = "lblInfoRate";

            this.lblInfoStatus.Location = new System.Drawing.Point(630, 8);
            this.lblInfoStatus.Size = new System.Drawing.Size(180, 20);
            this.lblInfoStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblInfoStatus.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblInfoStatus.Text = "Status: Active";
            this.lblInfoStatus.Name = "lblInfoStatus";

            this.lblInfoCapacity.Location = new System.Drawing.Point(24, 34);
            this.lblInfoCapacity.Size = new System.Drawing.Size(180, 20);
            this.lblInfoCapacity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoCapacity.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblInfoCapacity.Text = "Capacity: 8 person(s)";
            this.lblInfoCapacity.Name = "lblInfoCapacity";

            this.lblInfoDescription.Location = new System.Drawing.Point(210, 34);
            this.lblInfoDescription.Size = new System.Drawing.Size(680, 22);
            this.lblInfoDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblInfoDescription.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblInfoDescription.Text = "Description: Acoustic treated room with vocal booth.";
            this.lblInfoDescription.Name = "lblInfoDescription";

            this.pnlStudioInfo.Controls.Add(this.lblInfoCode);
            this.pnlStudioInfo.Controls.Add(this.lblInfoType);
            this.pnlStudioInfo.Controls.Add(this.lblInfoRate);
            this.pnlStudioInfo.Controls.Add(this.lblInfoCapacity);
            this.pnlStudioInfo.Controls.Add(this.lblInfoStatus);
            this.pnlStudioInfo.Controls.Add(this.lblInfoDescription);

            // ==== Section Header ====
            this.pnlSectionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSectionHeader.Height = 44;
            this.pnlSectionHeader.BackColor = System.Drawing.Color.White;
            this.pnlSectionHeader.Padding = new System.Windows.Forms.Padding(24, 6, 24, 0);
            this.pnlSectionHeader.Name = "pnlSectionHeader";

            this.lblInventoryTitle.AutoSize = true;
            this.lblInventoryTitle.Location = new System.Drawing.Point(24, 4);
            this.lblInventoryTitle.Text = "Assigned Studio Equipment & Instruments";
            this.lblInventoryTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblInventoryTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblInventoryTitle.Name = "lblInventoryTitle";

            this.lblInventoryCount.AutoSize = true;
            this.lblInventoryCount.Location = new System.Drawing.Point(24, 24);
            this.lblInventoryCount.Text = "Loading equipment...";
            this.lblInventoryCount.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInventoryCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblInventoryCount.Name = "lblInventoryCount";

            this.pnlSectionHeader.Controls.Add(this.lblInventoryTitle);
            this.pnlSectionHeader.Controls.Add(this.lblInventoryCount);

            // ==== Grid Container (Contained with 24px margins) ====
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.BackColor = System.Drawing.Color.White;
            this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(24, 4, 24, 4);
            this.pnlGridContainer.Name = "pnlGridContainer";

            // ==== DataGridView ====
            this.dgvEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquipment.BackgroundColor = System.Drawing.Color.White;
            this.dgvEquipment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEquipment.AllowUserToAddRows = false;
            this.dgvEquipment.AllowUserToDeleteRows = false;
            this.dgvEquipment.ReadOnly = true;
            this.dgvEquipment.RowHeadersVisible = false;
            this.dgvEquipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipment.MultiSelect = false;
            this.dgvEquipment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEquipment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvEquipment.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvEquipment.Name = "dgvEquipment";
            this.dgvEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEquipment_CellContentClick);

            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvEquipment.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvEquipment.ColumnHeadersHeight = 32;
            this.dgvEquipment.EnableHeadersVisualStyles = false;

            dgvRowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvRowStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            dgvRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            dgvRowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvEquipment.DefaultCellStyle = dgvRowStyle;
            this.dgvEquipment.RowTemplate.Height = 32;

            // Columns
            this.colItemCode.HeaderText = "Item Code";
            this.colItemCode.FillWeight = 85;
            this.colItemCode.MinimumWidth = 85;
            this.colItemCode.Name = "colItemCode";

            this.colItemName.HeaderText = "Item Name";
            this.colItemName.FillWeight = 150;
            this.colItemName.MinimumWidth = 130;
            this.colItemName.Name = "colItemName";

            this.colCategory.HeaderText = "Category";
            this.colCategory.FillWeight = 90;
            this.colCategory.MinimumWidth = 85;
            this.colCategory.Name = "colCategory";

            this.colQty.HeaderText = "Qty";
            this.colQty.FillWeight = 45;
            this.colQty.MinimumWidth = 45;
            this.colQty.Name = "colQty";

            this.colCondition.HeaderText = "Condition";
            this.colCondition.FillWeight = 70;
            this.colCondition.MinimumWidth = 70;
            this.colCondition.Name = "colCondition";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 75;
            this.colStatus.MinimumWidth = 75;
            this.colStatus.Name = "colStatus";

            this.colUnitCost.HeaderText = "Unit Cost";
            this.colUnitCost.FillWeight = 80;
            this.colUnitCost.MinimumWidth = 80;
            this.colUnitCost.Name = "colUnitCost";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 75;
            this.colActions.MinimumWidth = 75;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvEquipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItemCode,
                this.colItemName,
                this.colCategory,
                this.colQty,
                this.colCondition,
                this.colStatus,
                this.colUnitCost,
                this.colActions
            });

            this.pnlGridContainer.Controls.Add(this.dgvEquipment);

            // ==== Footer Panel ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 50;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.pnlFooter.Name = "pnlFooter";

            this.lblTotalValue.Location = new System.Drawing.Point(24, 14);
            this.lblTotalValue.Size = new System.Drawing.Size(500, 22);
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTotalValue.Text = "Total Equipment Value: ₱0.00";
            this.lblTotalValue.Name = "lblTotalValue";

            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Size = new System.Drawing.Size(95, 34);
            this.btnClose.Location = new System.Drawing.Point(800, 8);
            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlFooter.Controls.Add(this.lblTotalValue);
            this.pnlFooter.Controls.Add(this.btnClose);

            // ==== Form Assembly ====
            // Order: GridContainer (Fill), Footer (Bottom), SectionHeader (Top), StudioInfo (Top), Header (Top)
            this.Controls.Add(this.pnlGridContainer);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlSectionHeader);
            this.Controls.Add(this.pnlStudioInfo);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlStudioInfo.ResumeLayout(false);
            this.pnlSectionHeader.ResumeLayout(false);
            this.pnlSectionHeader.PerformLayout();
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnCloseHeader;

        private System.Windows.Forms.Panel pnlStudioInfo;
        private System.Windows.Forms.Label lblInfoCode;
        private System.Windows.Forms.Label lblInfoType;
        private System.Windows.Forms.Label lblInfoRate;
        private System.Windows.Forms.Label lblInfoCapacity;
        private System.Windows.Forms.Label lblInfoStatus;
        private System.Windows.Forms.Label lblInfoDescription;

        private System.Windows.Forms.Panel pnlSectionHeader;
        private System.Windows.Forms.Label lblInventoryTitle;
        private System.Windows.Forms.Label lblInventoryCount;

        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.DataGridView dgvEquipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitCost;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnClose;
    }
}
