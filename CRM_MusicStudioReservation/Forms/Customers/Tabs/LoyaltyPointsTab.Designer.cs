namespace CRM.winforms.Forms.Customers.Tabs
{
    partial class LoyaltyPointsTab
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblSubheading = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvLoyalty = new System.Windows.Forms.DataGridView();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.pnlTop.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoyalty)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // LoyaltyPointsTab
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "LoyaltyPointsTab";
            this.Size = new System.Drawing.Size(1100, 600);
            this.Load += new System.EventHandler(this.LoyaltyPointsTab_Load);

            // pnlTop
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 100;
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            // lblHeading
            this.lblHeading.AutoSize = false;
            this.lblHeading.Size = new System.Drawing.Size(500, 32);
            this.lblHeading.Location = new System.Drawing.Point(30, 15);
            this.lblHeading.Text = "Loyalty Points";
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeading.Name = "lblHeading";

            // lblSubheading
            this.lblSubheading.AutoSize = false;
            this.lblSubheading.Size = new System.Drawing.Size(700, 20);
            this.lblSubheading.Location = new System.Drawing.Point(30, 45);
            this.lblSubheading.Text = "Non-members earn 5 pts/booking · Members earn their plan's rate";
            this.lblSubheading.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheading.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheading.Name = "lblSubheading";

            // lblSearch
            this.lblSearch.AutoSize = false;
            this.lblSearch.Size = new System.Drawing.Size(60, 30);
            this.lblSearch.Location = new System.Drawing.Point(30, 63);
            this.lblSearch.Text = "Search";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearch.Name = "lblSearch";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(90, 66);
            this.txtSearch.Size = new System.Drawing.Size(260, 24);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by name or code...";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // btnRefresh
            this.btnRefresh.Size = new System.Drawing.Size(110, 28);
            this.btnRefresh.Location = new System.Drawing.Point(950, 63);
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Text = "↻  Refresh";
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlTop.Controls.Add(this.lblHeading);
            this.pnlTop.Controls.Add(this.lblSubheading);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.btnRefresh);

            // pnlBody
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlBody.Name = "pnlBody";

            // dgvLoyalty
            this.dgvLoyalty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoyalty.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoyalty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLoyalty.AllowUserToAddRows = false;
            this.dgvLoyalty.AllowUserToDeleteRows = false;
            this.dgvLoyalty.ReadOnly = true;
            this.dgvLoyalty.RowHeadersVisible = false;
            this.dgvLoyalty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoyalty.MultiSelect = false;
            this.dgvLoyalty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoyalty.ColumnHeadersHeight = 40;
            this.dgvLoyalty.RowTemplate.Height = 42;
            this.dgvLoyalty.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvLoyalty.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvLoyalty.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvLoyalty.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLoyalty.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvLoyalty.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvLoyalty.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvLoyalty.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvLoyalty.Name = "dgvLoyalty";
            this.dgvLoyalty.EnableHeadersVisualStyles = false;
            this.dgvLoyalty.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoyalty_CellContentClick);

            this.pnlBody.Controls.Add(this.dgvLoyalty);

            // pnlFooter
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 40;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";

            // lblCount
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(30, 10);
            this.lblCount.Text = "0 customer(s)";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.Name = "lblCount";

            this.pnlFooter.Controls.Add(this.lblCount);

            // Assembly — Fill first, then Bottom, then Top (matches Inventory/Booking pattern)
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTop);

            this.pnlTop.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoyalty)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblSubheading;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvLoyalty;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}