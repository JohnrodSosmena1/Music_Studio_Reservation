namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class ReviewsTab
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

            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblStatsSummary = new System.Windows.Forms.Label();

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVerified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReply = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlTopBar.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== ReviewsTab ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "ReviewsTab";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.ReviewsTab_Load);

            // ==== Top Bar ====
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 70;
            this.pnlTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(400, 25);
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Text = "Customer Reviews";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(400, 20);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 46);
            this.lblSubtitle.Text = "Moderate reviews, star ratings, and comments";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

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
            this.btnAdd.Text = "＋  Record Review";
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
            this.pnlTopBar.Controls.Add(this.btnRefresh);
            this.pnlTopBar.Controls.Add(this.btnAdd);

            // ==== Stats Summary ====
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height = 50;
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            this.lblStatsSummary.AutoSize = false;
            this.lblStatsSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatsSummary.Text = "No reviews yet";
            this.lblStatsSummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatsSummary.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblStatsSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatsSummary.Name = "lblStatsSummary";

            this.pnlStats.Controls.Add(this.lblStatsSummary);

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

            this.txtSearch.Size = new System.Drawing.Size(280, 28);
            this.txtSearch.Location = new System.Drawing.Point(85, 16);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblStatus.AutoSize = false;
            this.lblStatus.Size = new System.Drawing.Size(60, 36);
            this.lblStatus.Location = new System.Drawing.Point(385, 12);
            this.lblStatus.Text = "Status";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Name = "lblStatus";

            this.cmbStatus.Size = new System.Drawing.Size(160, 28);
            this.cmbStatus.Location = new System.Drawing.Point(450, 16);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            this.lblRating.AutoSize = false;
            this.lblRating.Size = new System.Drawing.Size(60, 36);
            this.lblRating.Location = new System.Drawing.Point(630, 12);
            this.lblRating.Text = "Rating";
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRating.Name = "lblRating";

            this.cmbRating.Size = new System.Drawing.Size(180, 28);
            this.cmbRating.Location = new System.Drawing.Point(695, 16);
            this.cmbRating.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.SelectedIndexChanged += new System.EventHandler(this.cmbRating_SelectedIndexChanged);

            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblStatus);
            this.pnlFilters.Controls.Add(this.cmbStatus);
            this.pnlFilters.Controls.Add(this.lblRating);
            this.pnlFilters.Controls.Add(this.cmbRating);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlBody.Name = "pnlBody";

            this.dgvReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.White;
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AllowUserToDeleteRows = false;
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersVisible = false;
            this.dgvReviews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReviews.MultiSelect = false;
            this.dgvReviews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.ColumnHeadersHeight = 40;
            this.dgvReviews.RowTemplate.Height = 40;
            this.dgvReviews.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvReviews.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvReviews.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvReviews.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvReviews.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvReviews.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.EnableHeadersVisualStyles = false;
            this.dgvReviews.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReviews_CellDoubleClick);
            this.dgvReviews.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReviews_CellContentClick);

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 35;
            this.colId.Name = "colId";

            this.colCustomer.HeaderText = "Customer";
            this.colCustomer.FillWeight = 70;
            this.colCustomer.Name = "colCustomer";

            this.colType.HeaderText = "Type";
            this.colType.FillWeight = 70;
            this.colType.Name = "colType";

            this.colRating.HeaderText = "Rating";
            this.colRating.FillWeight = 110;
            this.colRating.Name = "colRating";

            this.colTitle.HeaderText = "Title";
            this.colTitle.FillWeight = 150;
            this.colTitle.Name = "colTitle";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 80;
            this.colStatus.Name = "colStatus";

            this.colVerified.HeaderText = "Verified";
            this.colVerified.FillWeight = 60;
            this.colVerified.Name = "colVerified";

            this.colReply.HeaderText = "Reply";
            this.colReply.FillWeight = 60;
            this.colReply.Name = "colReply";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 90;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvReviews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCustomer,
                this.colType,
                this.colRating,
                this.colTitle,
                this.colStatus,
                this.colVerified,
                this.colReply,
                this.colActions
            });

            this.pnlBody.Controls.Add(this.dgvReviews);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 40;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);

            this.lblCount.AutoSize = false;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.Width = 400;
            this.lblCount.Text = "0 reviews";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Name = "lblCount";

            this.pnlFooter.Controls.Add(this.lblCount);

            // ==== Tab assembly ====
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlTopBar);

            this.pnlTopBar.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatsSummary;

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVerified;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReply;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}