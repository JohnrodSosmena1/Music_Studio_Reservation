namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class PromotionsTab
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.chkActiveOnly = new System.Windows.Forms.CheckBox();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvPromotions = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlTopBar.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotions)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== PromotionsTab ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "PromotionsTab";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.PromotionsTab_Load);

            // ==== Top Bar ====
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 70;
            this.pnlTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(300, 25);
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Text = "Promotions";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(300, 20);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 46);
            this.lblSubtitle.Text = "Manage discount codes and campaigns";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.btnValidate.Size = new System.Drawing.Size(130, 40);
            this.btnValidate.Location = new System.Drawing.Point(560, 25);
            this.btnValidate.Text = "🔍  Validate Code";
            this.btnValidate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnValidate.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnValidate.BackColor = System.Drawing.Color.White;
            this.btnValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnValidate.FlatAppearance.BorderSize = 1;
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);

            this.btnRefresh.Size = new System.Drawing.Size(110, 40);
            this.btnRefresh.Location = new System.Drawing.Point(700, 25);
            this.btnRefresh.Text = "↻  Refresh";
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnAdd.Size = new System.Drawing.Size(150, 40);
            this.btnAdd.Location = new System.Drawing.Point(820, 25);
            this.btnAdd.Text = "＋  New Promotion";
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.pnlTopBar.Controls.Add(this.lblTitle);
            this.pnlTopBar.Controls.Add(this.lblSubtitle);
            this.pnlTopBar.Controls.Add(this.btnValidate);
            this.pnlTopBar.Controls.Add(this.btnRefresh);
            this.pnlTopBar.Controls.Add(this.btnAdd);

            // ==== Filters ====
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 60;
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);

            this.lblSearch.AutoSize = false;
            this.lblSearch.Size = new System.Drawing.Size(60, 36);
            this.lblSearch.Location = new System.Drawing.Point(20, 12);
            this.lblSearch.Text = "Search";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearch.Name = "lblSearch";

            this.txtSearch.Size = new System.Drawing.Size(240, 28);
            this.txtSearch.Location = new System.Drawing.Point(85, 16);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblStatus.AutoSize = false;
            this.lblStatus.Size = new System.Drawing.Size(60, 36);
            this.lblStatus.Location = new System.Drawing.Point(345, 12);
            this.lblStatus.Text = "Status";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Name = "lblStatus";

            this.cmbStatus.Size = new System.Drawing.Size(160, 28);
            this.cmbStatus.Location = new System.Drawing.Point(410, 16);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            this.chkActiveOnly.AutoSize = false;
            this.chkActiveOnly.Size = new System.Drawing.Size(150, 28);
            this.chkActiveOnly.Location = new System.Drawing.Point(590, 16);
            this.chkActiveOnly.Text = "Active Only";
            this.chkActiveOnly.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkActiveOnly.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.chkActiveOnly.Name = "chkActiveOnly";
            this.chkActiveOnly.CheckedChanged += new System.EventHandler(this.chkActiveOnly_CheckedChanged);

            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblStatus);
            this.pnlFilters.Controls.Add(this.cmbStatus);
            this.pnlFilters.Controls.Add(this.chkActiveOnly);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlBody.Name = "pnlBody";

            this.dgvPromotions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPromotions.BackgroundColor = System.Drawing.Color.White;
            this.dgvPromotions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPromotions.AllowUserToAddRows = false;
            this.dgvPromotions.AllowUserToDeleteRows = false;
            this.dgvPromotions.ReadOnly = true;
            this.dgvPromotions.RowHeadersVisible = false;
            this.dgvPromotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPromotions.MultiSelect = false;
            this.dgvPromotions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPromotions.ColumnHeadersHeight = 40;
            this.dgvPromotions.RowTemplate.Height = 40;
            this.dgvPromotions.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvPromotions.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvPromotions.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvPromotions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPromotions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvPromotions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvPromotions.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvPromotions.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvPromotions.Name = "dgvPromotions";
            this.dgvPromotions.EnableHeadersVisualStyles = false;
            this.dgvPromotions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPromotions_CellDoubleClick);
            this.dgvPromotions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPromotions_CellContentClick);

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 30;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 90;
            this.colCode.Name = "colCode";

            this.colName.HeaderText = "Name";
            this.colName.FillWeight = 150;
            this.colName.Name = "colName";

            this.colDiscount.HeaderText = "Discount";
            this.colDiscount.FillWeight = 70;
            this.colDiscount.Name = "colDiscount";

            this.colStart.HeaderText = "Start";
            this.colStart.FillWeight = 100;
            this.colStart.Name = "colStart";

            this.colEnd.HeaderText = "End";
            this.colEnd.FillWeight = 100;
            this.colEnd.Name = "colEnd";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 80;
            this.colStatus.Name = "colStatus";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 90;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvPromotions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colName,
                this.colDiscount,
                this.colStart,
                this.colEnd,
                this.colStatus,
                this.colActions
            });

            this.pnlBody.Controls.Add(this.dgvPromotions);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 40;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);

            this.lblCount.AutoSize = false;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.Width = 400;
            this.lblCount.Text = "0 promotions";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Name = "lblCount";

            this.pnlFooter.Controls.Add(this.lblCount);

            // ==== Tab assembly ====
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlTopBar);

            this.pnlTopBar.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotions)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnValidate;

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckBox chkActiveOnly;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvPromotions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}