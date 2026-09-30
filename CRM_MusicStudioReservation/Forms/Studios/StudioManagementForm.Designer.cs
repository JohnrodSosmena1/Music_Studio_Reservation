namespace CRM_MusicStudioReservation.Forms.Studios
{
    partial class StudioManagementForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.dgvStudios = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvStudios)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== StudioManagementForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Name = "StudioManagementForm";
            this.Text = "Studios";
            this.Load += new System.EventHandler(this.StudioManagementForm_Load);

            // ==== pnlHeader ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 110;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";

            // ==== lblTitle ====
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Studio Management";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // ==== lblSubtitle ====
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(500, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Text = "Manage your studio rooms and rates";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // ==== txtSearch ====
            this.txtSearch.Location = new System.Drawing.Point(600, 30);
            this.txtSearch.Size = new System.Drawing.Size(240, 30);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by name or code...";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // ==== btnAdd ====
            this.btnAdd.Size = new System.Drawing.Size(130, 34);
            this.btnAdd.Location = new System.Drawing.Point(860, 28);
            this.btnAdd.Text = "+  Add Studio";
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // ==== btnRefresh ====
            this.btnRefresh.Size = new System.Drawing.Size(60, 34);
            this.btnRefresh.Location = new System.Drawing.Point(1000, 28);
            this.btnRefresh.Text = "↻";
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.btnAdd);
            this.pnlHeader.Controls.Add(this.btnRefresh);

            // ==== dgvStudios ====
            this.dgvStudios.Location = new System.Drawing.Point(30, 130);
            this.dgvStudios.Size = new System.Drawing.Size(1040, 500);
            this.dgvStudios.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStudios.AllowUserToAddRows = false;
            this.dgvStudios.AllowUserToDeleteRows = false;
            this.dgvStudios.AllowUserToResizeRows = false;
            this.dgvStudios.RowHeadersVisible = false;
            this.dgvStudios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudios.MultiSelect = false;
            this.dgvStudios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudios.ColumnHeadersHeight = 40;
            this.dgvStudios.RowTemplate.Height = 40;
            this.dgvStudios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvStudios.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvStudios.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvStudios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvStudios.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvStudios.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvStudios.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvStudios.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvStudios.Name = "dgvStudios";
            this.dgvStudios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudios_CellContentClick);
            this.dgvStudios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudios_CellDoubleClick);

            // ==== Columns ====
            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 70;
            this.colCode.Name = "colCode";

            this.colName.HeaderText = "Name";
            this.colName.FillWeight = 130;
            this.colName.Name = "colName";

            this.colType.HeaderText = "Type";
            this.colType.FillWeight = 100;
            this.colType.Name = "colType";

            this.colRate.HeaderText = "Rate/Hour";
            this.colRate.FillWeight = 80;
            this.colRate.Name = "colRate";

            this.colCapacity.HeaderText = "Capacity";
            this.colCapacity.FillWeight = 60;
            this.colCapacity.Name = "colCapacity";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 60;
            this.colStatus.Name = "colStatus";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 90;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvStudios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colName,
                this.colType,
                this.colRate,
                this.colCapacity,
                this.colStatus,
                this.colActions
            });

            // ==== pnlFooter ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 50;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";

            // ==== lblCount ====
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(30, 15);
            this.lblCount.Text = "0 studios";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.Name = "lblCount";

            this.pnlFooter.Controls.Add(this.lblCount);

            // ==== Add to Form ====
            this.Controls.Add(this.dgvStudios);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this.dgvStudios)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvStudios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}