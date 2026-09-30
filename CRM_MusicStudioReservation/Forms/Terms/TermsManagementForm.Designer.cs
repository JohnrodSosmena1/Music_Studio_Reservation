namespace CRM_MusicStudioReservation.Forms.Terms
{
    partial class TermsManagementForm
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNewTerms = new System.Windows.Forms.Button();
            this.btnViewAllAcks = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvTerms = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlHeaderActions.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerms)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== TermsManagementForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1040, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TermsManagementForm";
            this.Text = "Terms & Conditions";
            this.Load += new System.EventHandler(this.TermsManagementForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(500, 40);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Text = "Terms & Conditions";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(550, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 57);
            this.lblSubtitle.Text = "Manage, version-control, draft, and publish policies for staff and clients";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            // ==== Header Actions ====
            this.pnlHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeaderActions.Width = 440;
            this.pnlHeaderActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlHeaderActions.WrapContents = false;
            this.pnlHeaderActions.BackColor = System.Drawing.Color.Transparent;

            this.btnNewTerms.Size = new System.Drawing.Size(160, 40);
            this.btnNewTerms.Text = "+ New T&&C Draft";
            this.btnNewTerms.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewTerms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTerms.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnNewTerms.ForeColor = System.Drawing.Color.White;
            this.btnNewTerms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewTerms.Margin = new System.Windows.Forms.Padding(8, 20, 0, 0);
            this.btnNewTerms.Click += new System.EventHandler(this.btnNewTerms_Click);

            this.btnViewAllAcks.Size = new System.Drawing.Size(130, 40);
            this.btnViewAllAcks.Text = "📋 Client Log";
            this.btnViewAllAcks.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewAllAcks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewAllAcks.BackColor = System.Drawing.Color.White;
            this.btnViewAllAcks.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnViewAllAcks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewAllAcks.Margin = new System.Windows.Forms.Padding(8, 20, 0, 0);
            this.btnViewAllAcks.Click += new System.EventHandler(this.btnViewAllAcks_Click);

            this.btnRefresh.Size = new System.Drawing.Size(100, 40);
            this.btnRefresh.Text = "↻ Refresh";
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlHeaderActions.Controls.Add(this.btnNewTerms);
            this.pnlHeaderActions.Controls.Add(this.btnViewAllAcks);
            this.pnlHeaderActions.Controls.Add(this.btnRefresh);

            this.pnlHeader.Controls.Add(this.pnlHeaderActions);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Filters Bar ====
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 65;
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(30, 12, 30, 12);

            this.lblSearch.Location = new System.Drawing.Point(30, 18);
            this.lblSearch.Size = new System.Drawing.Size(55, 25);
            this.lblSearch.Text = "Search:";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.txtSearch.Location = new System.Drawing.Point(90, 16);
            this.txtSearch.Size = new System.Drawing.Size(220, 26);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.PlaceholderText = "Search by code or title...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblType.Location = new System.Drawing.Point(340, 18);
            this.lblType.Size = new System.Drawing.Size(45, 25);
            this.lblType.Text = "Type:";
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.cmbType.Location = new System.Drawing.Point(390, 16);
            this.cmbType.Size = new System.Drawing.Size(180, 26);
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);

            this.lblStatus.Location = new System.Drawing.Point(600, 18);
            this.lblStatus.Size = new System.Drawing.Size(55, 25);
            this.lblStatus.Text = "Status:";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.cmbStatus.Location = new System.Drawing.Point(660, 16);
            this.cmbStatus.Size = new System.Drawing.Size(160, 26);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblType);
            this.pnlFilters.Controls.Add(this.cmbType);
            this.pnlFilters.Controls.Add(this.lblStatus);
            this.pnlFilters.Controls.Add(this.cmbStatus);

            // ==== Body Panel (Grid) ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 15, 30, 10);

            this.dgvTerms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTerms.BackgroundColor = System.Drawing.Color.White;
            this.dgvTerms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTerms.RowHeadersVisible = false;
            this.dgvTerms.AllowUserToAddRows = false;
            this.dgvTerms.AllowUserToDeleteRows = false;
            this.dgvTerms.ReadOnly = true;
            this.dgvTerms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTerms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTerms.RowTemplate.Height = 44;
            this.dgvTerms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTerms_CellContentClick);

            this.pnlBody.Controls.Add(this.dgvTerms);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 45;
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);

            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            this.pnlFooter.Controls.Add(this.lblCount);

            // ==== Assemble Form ====
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeaderActions.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerms)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlHeaderActions;
        private System.Windows.Forms.Button btnNewTerms;
        private System.Windows.Forms.Button btnViewAllAcks;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvTerms;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}
