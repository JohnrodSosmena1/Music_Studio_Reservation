using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    public class SuperAdminDashboardForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly SuperAdminService _service;

        // UI Controls
        private Panel pnlHeader = null!;
        private Label lblWelcome = null!;
        private Label lblSubtitle = null!;

        private FlowLayoutPanel pnlQuickActions = null!;
        private Button btnAddOrg = null!;
        private Button btnViewOrgs = null!;
        private Button btnViewSubs = null!;
        private Button btnViewTerms = null!;

        private Panel pnlStats = null!;
        private TableLayoutPanel tlpStats = null!;
        private StatCard cardTotalOrgs = null!;
        private StatCard cardActiveOrgs = null!;
        private StatCard cardTotalUsers = null!;
        private StatCard cardMrr = null!;

        private Panel pnlContent = null!;
        private TableLayoutPanel tlpBottom = null!;
        private Panel pnlRecentActivity = null!;
        private Label lblActivityTitle = null!;
        private ListBox lbActivities = null!;

        private Panel pnlSummary = null!;
        private Label lblSummaryTitle = null!;
        private Label lblSummaryText = null!;

        public SuperAdminDashboardForm(AuthService auth, ApiClient api)
        {
            _auth = auth;
            _api = api;
            _service = new SuperAdminService(api);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(249, 250, 251);
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "SuperAdminDashboardForm";
            this.Text = "Platform Super Admin Dashboard";
            this.Load += async (s, e) => await LoadDashboardDataAsync();

            // Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(30, 20, 30, 10),
                BackColor = Color.Transparent
            };

            lblWelcome = new Label
            {
                Text = "Platform Super Admin Dashboard",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = true,
                Location = new Point(30, 16),
                UseMnemonic = false
            };

            lblSubtitle = new Label
            {
                Text = "Global multi-tenant overview, studio organization lifecycles, and MRR metrics",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(30, 60),
                UseMnemonic = false
            };

            pnlHeader.Controls.Add(lblWelcome);
            pnlHeader.Controls.Add(lblSubtitle);

            // Quick Actions Bar
            pnlQuickActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(30, 8, 30, 8),
                BackColor = Color.Transparent,
                WrapContents = false,
                AutoScroll = false
            };

            btnAddOrg = CreateQuickButton("+ Add Organization", Color.FromArgb(139, 92, 246), Color.White);
            btnAddOrg.Click += (s, e) => (this.ParentForm as MainForm)?.NavigateToOrganizations();

            btnViewOrgs = CreateQuickButton("🏢 All Organizations", Color.White, Color.FromArgb(55, 65, 81));
            btnViewOrgs.Click += (s, e) => (this.ParentForm as MainForm)?.NavigateToOrganizations();

            btnViewSubs = CreateQuickButton("💳 Subscriptions & MRR", Color.White, Color.FromArgb(55, 65, 81));
            btnViewSubs.Click += (s, e) => (this.ParentForm as MainForm)?.NavigateToSubscriptions();

            btnViewTerms = CreateQuickButton("📜 Platform Terms", Color.White, Color.FromArgb(55, 65, 81));
            btnViewTerms.Click += (s, e) => (this.ParentForm as MainForm)?.NavigateToTerms();

            pnlQuickActions.Controls.Add(btnAddOrg);
            pnlQuickActions.Controls.Add(btnViewOrgs);
            pnlQuickActions.Controls.Add(btnViewSubs);
            pnlQuickActions.Controls.Add(btnViewTerms);

            // Stats row (4 cards)
            pnlStats = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.Transparent
            };

            tlpStats = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            cardTotalOrgs = new StatCard { Dock = DockStyle.Fill, Title = "Total Organizations", Value = "—", Icon = "🏢", IconColor = Color.FromArgb(139, 92, 246), Subtext = "Across platform" };
            cardActiveOrgs = new StatCard { Dock = DockStyle.Fill, Title = "Active Organizations", Value = "—", Icon = "✓", IconColor = Color.FromArgb(16, 185, 129), Subtext = "Paying / operational" };
            cardTotalUsers = new StatCard { Dock = DockStyle.Fill, Title = "Platform Users", Value = "—", Icon = "👥", IconColor = Color.FromArgb(59, 130, 246), Subtext = "Admins, staff, clients" };
            cardMrr = new StatCard { Dock = DockStyle.Fill, Title = "Monthly Recurring Rev", Value = "₱0.00", Icon = "₱", IconColor = Color.FromArgb(245, 158, 11), Subtext = "Active subscriptions" };

            tlpStats.Controls.Add(cardTotalOrgs, 0, 0);
            tlpStats.Controls.Add(cardActiveOrgs, 1, 0);
            tlpStats.Controls.Add(cardTotalUsers, 2, 0);
            tlpStats.Controls.Add(cardMrr, 3, 0);
            pnlStats.Controls.Add(tlpStats);

            // Content Area
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 10, 30, 20),
                BackColor = Color.Transparent
            };

            tlpBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55f));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));

            // Activity panel
            pnlRecentActivity = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            lblActivityTitle = new Label
            {
                Text = "Recent Platform Activity & Audit Logs",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Dock = DockStyle.Top,
                Height = 35,
                UseMnemonic = false
            };

            lbActivities = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10F),
                ItemHeight = 26,
                IntegralHeight = false
            };
            pnlRecentActivity.Controls.Add(lbActivities);
            pnlRecentActivity.Controls.Add(lblActivityTitle);

            // Platform health / summary panel
            pnlSummary = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            lblSummaryTitle = new Label
            {
                Text = "Tenant Isolation & Architecture Status",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Dock = DockStyle.Top,
                Height = 35,
                UseMnemonic = false
            };

            lblSummaryText = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(75, 85, 99),
                Text = "• Multi-Tenant Isolation: Active (EF Core Global Query Filters enforced)\n" +
                       "• Tenant Resolution: JWT Claims + Subdomain matching\n" +
                       "• Database Mode: Platform Master Schema with isolated tenant scopes\n" +
                       "• Security: Role-Based Access Control (RBAC) enforced\n\n" +
                       "Loading telemetry...",
                Padding = new Padding(0, 10, 0, 0)
            };
            pnlSummary.Controls.Add(lblSummaryText);
            pnlSummary.Controls.Add(lblSummaryTitle);

            tlpBottom.Controls.Add(pnlRecentActivity, 0, 0);
            tlpBottom.Controls.Add(pnlSummary, 1, 0);
            pnlContent.Controls.Add(tlpBottom);

            // Add all in order
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlStats);
            this.Controls.Add(pnlQuickActions);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private Button CreateQuickButton(string text, Color bg, Color fg)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Height = 36,
                MinimumSize = new Size(160, 36),
                Padding = new Padding(14, 4, 14, 4),
                Margin = new Padding(0, 0, 12, 0),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btn.FlatAppearance.BorderSize = bg == Color.White ? 1 : 0;
            btn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            return btn;
        }

        private async Task LoadDashboardDataAsync()
        {
            try
            {
                var data = await _service.GetDashboardAsync();
                if (data == null || this.IsDisposed) return;

                cardTotalOrgs.Value = data.TotalOrganizations.ToString();
                cardActiveOrgs.Value = data.ActiveOrganizations.ToString();
                cardTotalUsers.Value = data.TotalPlatformUsers.ToString();
                cardMrr.Value = $"₱{data.MonthlyRecurringRevenue:N2}";

                lbActivities.Items.Clear();
                if (data.RecentActivities.Count == 0)
                {
                    lbActivities.Items.Add("No recent activities recorded.");
                }
                else
                {
                    foreach (var a in data.RecentActivities)
                    {
                        lbActivities.Items.Add($"[{a.TimeAgo}]  {a.Title} — {a.Description}");
                    }
                }

                lblSummaryText.Text =
                    $"• Total Organizations: {data.TotalOrganizations} ({data.ActiveOrganizations} Active, {data.SuspendedOrganizations} Suspended, {data.TrialOrganizations} Trial)\n" +
                    $"• Subscriptions: {data.ActiveSubscriptions} active subscriptions\n" +
                    $"• Expiring Soon: {data.ExpiringSoonCount} organization subscriptions expire in the next 7 days\n" +
                    $"• Monthly Recurring Revenue: ₱{data.MonthlyRecurringRevenue:N2}\n\n" +
                    "• Multi-Tenant Isolation: Active (Global Query Filters enforced)\n" +
                    "• Tenant Resolution: JWT Claim + Subdomain routing\n" +
                    "• Super Admin Scope: Platform-wide (Tenant filter bypassed)";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SuperAdminDashboardForm] {ex.Message}");
            }
        }
    }
}
