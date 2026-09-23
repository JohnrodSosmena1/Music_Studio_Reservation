namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class FeedbackTab
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

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvFeedback = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlTopBar.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== FeedbackTab ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "FeedbackTab";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.FeedbackTab_Load);

            // ==== Top Bar ====
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 70;
            this.pnlTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(300, 25);
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Text = "Customer Feedback";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(400, 20);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 46);
            this.lblSubtitle.Text = "Post-service surveys, NPS scores, and satisfaction ratings";
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
            this.btnAdd.Text = "＋  Record Feedback";
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

            this.txtSearch.Size = new System.Drawing.Size(320, 28);
            this.txtSearch.Location = new System.Drawing.Point(85, 16);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblRating.AutoSize = false;
            this.lblRating.Size = new System.Drawing.Size(60, 36);
            this.lblRating.Location = new System.Drawing.Point(425, 12);
            this.lblRating.Text = "Rating";
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRating.Name = "lblRating";

            this.cmbRating.Size = new System.Drawing.Size(180, 28);
            this.cmbRating.Location = new System.Drawing.Point(490, 16);
            this.cmbRating.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.SelectedIndexChanged += new System.EventHandler(this.cmbRating_SelectedIndexChanged);

            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblRating);
            this.pnlFilters.Controls.Add(this.cmbRating);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlBody.Name = "pnlBody";

            this.dgvFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFeedback.BackgroundColor = System.Drawing.Color.White;
            this.dgvFeedback.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFeedback.AllowUserToAddRows = false;
            this.dgvFeedback.AllowUserToDeleteRows = false;
            this.dgvFeedback.ReadOnly = true;
            this.dgvFeedback.RowHeadersVisible = false;
            this.dgvFeedback.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFeedback.MultiSelect = false;
            this.dgvFeedback.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFeedback.ColumnHeadersHeight = 40;
            this.dgvFeedback.RowTemplate.Height = 40;
            this.dgvFeedback.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvFeedback.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvFeedback.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvFeedback.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvFeedback.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvFeedback.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvFeedback.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvFeedback.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvFeedback.Name = "dgvFeedback";
            this.dgvFeedback.EnableHeadersVisualStyles = false;
            this.dgvFeedback.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFeedback_CellDoubleClick);
            this.dgvFeedback.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFeedback_CellContentClick);

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colId.Name = "colId";

            this.colCustomerId.HeaderText = "Customer";
            this.colCustomerId.FillWeight = 80;
            this.colCustomerId.Name = "colCustomerId";

            this.colRating.HeaderText = "Rating";
            this.colRating.FillWeight = 110;
            this.colRating.Name = "colRating";

            this.colComments.HeaderText = "Comments";
            this.colComments.FillWeight = 300;
            this.colComments.Name = "colComments";

            this.colCreated.HeaderText = "Date";
            this.colCreated.FillWeight = 110;
            this.colCreated.Name = "colCreated";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 90;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvFeedback.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCustomerId,
                this.colRating,
                this.colComments,
                this.colCreated,
                this.colActions
            });

            this.pnlBody.Controls.Add(this.dgvFeedback);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 40;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);

            this.lblCount.AutoSize = false;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.Width = 400;
            this.lblCount.Text = "0 feedback entries";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvFeedback;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreated;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}