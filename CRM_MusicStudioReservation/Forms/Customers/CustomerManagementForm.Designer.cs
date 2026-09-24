namespace CRM_MusicStudioReservation.Forms.Customers
{
    partial class CustomerManagementForm
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

            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabAllCustomers = new System.Windows.Forms.Button();
            this.btnTabLoyalty = new System.Windows.Forms.Button();

            this.pnlContent = new System.Windows.Forms.Panel();

            // ---- All Customers view (moved from old layout) ----
            this.pnlAllCustomers = new System.Windows.Forms.Panel();
            this.pnlAllTopBar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.pnlAllBody = new System.Windows.Forms.Panel();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlAllFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlAllCustomers.SuspendLayout();
            this.pnlAllTopBar.SuspendLayout();
            this.pnlAllBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.pnlAllFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== CustomerManagementForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Name = "CustomerManagementForm";
            this.Text = "Customers";
            this.Load += new System.EventHandler(this.CustomerManagementForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(900, 40);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Customer Management";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblSubtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(900, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Manage your studio's customer records";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Tab Bar ====
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Height = 60;
            this.pnlTabs.BackColor = System.Drawing.Color.White;
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);

            // -- Tab 1: All Customers --
            this.btnTabAllCustomers.AutoSize = false;
            this.btnTabAllCustomers.Size = new System.Drawing.Size(180, 60);
            this.btnTabAllCustomers.Location = new System.Drawing.Point(30, 0);
            this.btnTabAllCustomers.Text = "👥  All Customers";
            this.btnTabAllCustomers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabAllCustomers.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnTabAllCustomers.BackColor = System.Drawing.Color.White;
            this.btnTabAllCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabAllCustomers.FlatAppearance.BorderSize = 0;
            this.btnTabAllCustomers.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabAllCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabAllCustomers.Name = "btnTabAllCustomers";
            this.btnTabAllCustomers.Click += new System.EventHandler(this.btnTabAllCustomers_Click);

            // -- Tab 2: Loyalty Points --
            this.btnTabLoyalty.AutoSize = false;
            this.btnTabLoyalty.Size = new System.Drawing.Size(180, 60);
            this.btnTabLoyalty.Location = new System.Drawing.Point(210, 0);
            this.btnTabLoyalty.Text = "🏆  Loyalty Points";
            this.btnTabLoyalty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabLoyalty.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabLoyalty.BackColor = System.Drawing.Color.White;
            this.btnTabLoyalty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabLoyalty.FlatAppearance.BorderSize = 0;
            this.btnTabLoyalty.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabLoyalty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabLoyalty.Name = "btnTabLoyalty";
            this.btnTabLoyalty.Click += new System.EventHandler(this.btnTabLoyalty_Click);

            this.pnlTabs.Controls.Add(this.btnTabAllCustomers);
            this.pnlTabs.Controls.Add(this.btnTabLoyalty);

            // ==== Content Area ====
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Name = "pnlContent";

            // ==== All Customers sub-view ====
            this.pnlAllCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAllCustomers.BackColor = System.Drawing.Color.Transparent;
            this.pnlAllCustomers.Name = "pnlAllCustomers";

            // -- top bar (search + buttons) --
            this.pnlAllTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAllTopBar.Height = 60;
            this.pnlAllTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlAllTopBar.Name = "pnlAllTopBar";
            this.pnlAllTopBar.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(600, 15);
            this.txtSearch.Size = new System.Drawing.Size(240, 26);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by name or code...";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // btnAdd
            this.btnAdd.Size = new System.Drawing.Size(150, 30);
            this.btnAdd.Location = new System.Drawing.Point(860, 13);
            this.btnAdd.Text = "+  Add Customer";
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnRefresh
            this.btnRefresh.Size = new System.Drawing.Size(50, 30);
            this.btnRefresh.Location = new System.Drawing.Point(1020, 13);
            this.btnRefresh.Text = "↻";
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlAllTopBar.Controls.Add(this.txtSearch);
            this.pnlAllTopBar.Controls.Add(this.btnAdd);
            this.pnlAllTopBar.Controls.Add(this.btnRefresh);

            // -- body (grid) --
            this.pnlAllBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAllBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlAllBody.Padding = new System.Windows.Forms.Padding(30, 0, 30, 15);
            this.pnlAllBody.Name = "pnlAllBody";

            // dgvCustomers
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AllowUserToResizeRows = false;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.ColumnHeadersHeight = 40;
            this.dgvCustomers.RowTemplate.Height = 40;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvCustomers.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCustomers.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvCustomers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvCustomers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvCustomers.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_CellContentClick);

            // Columns
            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 70;
            this.colCode.Name = "colCode";

            this.colName.HeaderText = "Name";
            this.colName.FillWeight = 130;
            this.colName.Name = "colName";

            this.colContact.HeaderText = "Contact";
            this.colContact.FillWeight = 100;
            this.colContact.Name = "colContact";

            this.colEmail.HeaderText = "Email";
            this.colEmail.FillWeight = 130;
            this.colEmail.Name = "colEmail";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 60;
            this.colStatus.Name = "colStatus";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 100;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colName,
                this.colContact,
                this.colEmail,
                this.colStatus,
                this.colActions
            });

            this.pnlAllBody.Controls.Add(this.dgvCustomers);

            // -- footer (count) --
            this.pnlAllFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAllFooter.Height = 40;
            this.pnlAllFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlAllFooter.Name = "pnlAllFooter";

            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(30, 10);
            this.lblCount.Text = "0 customer(s)";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.Name = "lblCount";

            this.pnlAllFooter.Controls.Add(this.lblCount);

            // Assemble All Customers sub-view
            this.pnlAllCustomers.Controls.Add(this.pnlAllBody);
            this.pnlAllCustomers.Controls.Add(this.pnlAllFooter);
            this.pnlAllCustomers.Controls.Add(this.pnlAllTopBar);

            // Attach to content area
            this.pnlContent.Controls.Add(this.pnlAllCustomers);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlAllCustomers.ResumeLayout(false);
            this.pnlAllTopBar.ResumeLayout(false);
            this.pnlAllBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.pnlAllFooter.ResumeLayout(false);
            this.pnlAllFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnTabAllCustomers;
        private System.Windows.Forms.Button btnTabLoyalty;

        private System.Windows.Forms.Panel pnlContent;

        private System.Windows.Forms.Panel pnlAllCustomers;
        private System.Windows.Forms.Panel pnlAllTopBar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlAllBody;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlAllFooter;
        private System.Windows.Forms.Label lblCount;
    }
}