namespace CRM_MusicStudioReservation.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlSidebarHeader = new System.Windows.Forms.Panel();
            this.lblSidebarLogo = new System.Windows.Forms.Label();
            this.lblSidebarTitle = new System.Windows.Forms.Label();
            this.pnlNavItems = new System.Windows.Forms.Panel();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.btnNavBookings = new System.Windows.Forms.Button();
            this.btnNavCustomers = new System.Windows.Forms.Button();
            this.btnNavStudios = new System.Windows.Forms.Button();
            this.btnNavInventory = new System.Windows.Forms.Button();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnNavEngagement = new System.Windows.Forms.Button();
            this.btnNavTerms = new System.Windows.Forms.Button();
            this.pnlSidebarFooter = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();

            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlSidebarHeader.SuspendLayout();
            this.pnlNavItems.SuspendLayout();
            this.pnlSidebarFooter.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.SuspendLayout();

            // ==== MainForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.Text = "Music Studio CRM";
            this.Load += new System.EventHandler(this.MainForm_Load);

            // ==== Sidebar ====
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 240;
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(45, 27, 78);

            // Sidebar header (logo + title)
            this.pnlSidebarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSidebarHeader.Height = 80;
            this.pnlSidebarHeader.Name = "pnlSidebarHeader";
            this.pnlSidebarHeader.BackColor = System.Drawing.Color.FromArgb(35, 20, 60);

            this.lblSidebarLogo.AutoSize = false;
            this.lblSidebarLogo.Size = new System.Drawing.Size(48, 48);
            this.lblSidebarLogo.Location = new System.Drawing.Point(20, 16);
            this.lblSidebarLogo.Text = "MS";
            this.lblSidebarLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSidebarLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSidebarLogo.ForeColor = System.Drawing.Color.White;
            this.lblSidebarLogo.Name = "lblSidebarLogo";
            this.lblSidebarLogo.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);

            this.lblSidebarTitle.AutoSize = false;
            this.lblSidebarTitle.Size = new System.Drawing.Size(150, 60);
            this.lblSidebarTitle.Location = new System.Drawing.Point(78, 10);
            this.lblSidebarTitle.Text = "Music Studio";
            this.lblSidebarTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSidebarTitle.ForeColor = System.Drawing.Color.White;
            this.lblSidebarTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSidebarTitle.Name = "lblSidebarTitle";

            this.pnlSidebarHeader.Controls.Add(this.lblSidebarLogo);
            this.pnlSidebarHeader.Controls.Add(this.lblSidebarTitle);

            // Nav items container
            this.pnlNavItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavItems.Name = "pnlNavItems";
            this.pnlNavItems.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);

            // Nav buttons — all created via MakeNavButton with uniform sizing and generous spacing
            this.btnNavDashboard = MakeNavButton("▶   Dashboard", "btnNavDashboard", 14);
            this.btnNavOrganizations = MakeNavButton("🏢   Organizations", "btnNavOrganizations", 70);
            this.btnNavSubscriptions = MakeNavButton("💳   Subscriptions", "btnNavSubscriptions", 126);
            this.btnNavTerms = MakeNavButton("📜   Terms & Policy", "btnNavTerms", 182);
            this.btnNavBookings = MakeNavButton("📅   Bookings", "btnNavBookings", 238);
            this.btnNavCustomers = MakeNavButton("👥   Customers", "btnNavCustomers", 294);
            this.btnNavStudios = MakeNavButton("🎸   Studios", "btnNavStudios", 350);
            this.btnNavInventory = MakeNavButton("📦   Inventory", "btnNavInventory", 406);
            this.btnNavReports = MakeNavButton("📊   Reports", "btnNavReports", 462);
            this.btnNavEngagement = MakeNavButton("🎁   Engagement", "btnNavEngagement", 518);

            this.pnlNavItems.Controls.Add(this.btnNavDashboard);
            this.pnlNavItems.Controls.Add(this.btnNavOrganizations);
            this.pnlNavItems.Controls.Add(this.btnNavSubscriptions);
            this.pnlNavItems.Controls.Add(this.btnNavTerms);
            this.pnlNavItems.Controls.Add(this.btnNavBookings);
            this.pnlNavItems.Controls.Add(this.btnNavCustomers);
            this.pnlNavItems.Controls.Add(this.btnNavStudios);
            this.pnlNavItems.Controls.Add(this.btnNavInventory);
            this.pnlNavItems.Controls.Add(this.btnNavReports);
            this.pnlNavItems.Controls.Add(this.btnNavEngagement);

            // Sidebar footer (logout)
            this.pnlSidebarFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarFooter.Height = 70;
            this.pnlSidebarFooter.Padding = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.pnlSidebarFooter.Name = "pnlSidebarFooter";

            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(45, 27, 78);
            this.btnLogout.Text = "←   Log Out";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            CRM.winforms.Helpers.RoundedCorners.Apply(this.btnLogout, 8);
            this.btnLogout.Resize += (s, e) => CRM.winforms.Helpers.RoundedCorners.Apply(this.btnLogout, 8);

            this.pnlSidebarFooter.Controls.Add(this.btnLogout);

            this.pnlSidebar.Controls.Add(this.pnlNavItems);
            this.pnlSidebar.Controls.Add(this.pnlSidebarFooter);
            this.pnlSidebar.Controls.Add(this.pnlSidebarHeader);

            // ==== Top Bar ====
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Height = 70;
            this.pnlTopBar.BackColor = System.Drawing.Color.White;
            this.pnlTopBar.Name = "pnlTopBar";

            this.lblPageTitle.AutoSize = false;
            this.lblPageTitle.Size = new System.Drawing.Size(400, 70);
            this.lblPageTitle.Location = new System.Drawing.Point(30, 0);
            this.lblPageTitle.Text = "Dashboard";
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPageTitle.Name = "lblPageTitle";

            this.lblUserInfo.AutoSize = false;
            this.lblUserInfo.Size = new System.Drawing.Size(400, 70);
            this.lblUserInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblUserInfo.Location = new System.Drawing.Point(1030, 0);
            this.lblUserInfo.Text = "Loading...";
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUserInfo.Padding = new System.Windows.Forms.Padding(0, 0, 90, 0);
            this.lblUserInfo.Name = "lblUserInfo";

            this.btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMinimize.Size = new System.Drawing.Size(40, 40);
            this.btnMinimize.Location = new System.Drawing.Point(1310, 15);
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.Text = "─";
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);

            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.Location = new System.Drawing.Point(1350, 15);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Text = "✕";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlTopBar.Controls.Add(this.lblPageTitle);
            this.pnlTopBar.Controls.Add(this.lblUserInfo);
            this.pnlTopBar.Controls.Add(this.btnMinimize);
            this.pnlTopBar.Controls.Add(this.btnClose);

            // ==== Content Area ====
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Name = "pnlContent";

            // ==== Add to Form ====
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlSidebar);

            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebarHeader.ResumeLayout(false);
            this.pnlNavItems.ResumeLayout(false);
            this.pnlSidebarFooter.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Helper method to create nav buttons consistently
        private System.Windows.Forms.Button MakeNavButton(string text, string name, int yPos)
        {
            var btn = new System.Windows.Forms.Button();
            btn.Size = new System.Drawing.Size(216, 46);
            btn.Location = new System.Drawing.Point(12, yPos);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Text = text;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            btn.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular);
            btn.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            btn.BackColor = System.Drawing.Color.FromArgb(45, 27, 78);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Name = name;
            btn.AutoEllipsis = true;
            btn.UseMnemonic = false;
            CRM.winforms.Helpers.RoundedCorners.Apply(btn, 8);
            btn.Resize += (s, e) => CRM.winforms.Helpers.RoundedCorners.Apply(btn, 8);
            return btn;
        }

        #endregion

        // ==================== CONTROLS ====================
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private System.Windows.Forms.Label lblSidebarLogo;
        private System.Windows.Forms.Label lblSidebarTitle;
        private System.Windows.Forms.Panel pnlNavItems;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavOrganizations;
        private System.Windows.Forms.Button btnNavSubscriptions;
        private System.Windows.Forms.Button btnNavBookings;
        private System.Windows.Forms.Button btnNavCustomers;
        private System.Windows.Forms.Button btnNavStudios;
        private System.Windows.Forms.Button btnNavInventory;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Button btnNavEngagement;
        private System.Windows.Forms.Button btnNavTerms;
        private System.Windows.Forms.Panel pnlSidebarFooter;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;

        private System.Windows.Forms.Panel pnlContent;
    }
}