using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM.winforms.Forms.Inventory;
using CRM.winforms.Forms.CustomerEngagement;
using CRM.winforms.Forms.Reports;
using CRM_MusicStudioReservation.Forms.Dashboards;
using CRM_MusicStudioReservation.Forms.Bookings;
using CRM_MusicStudioReservation.Forms.Customers;
using CRM_MusicStudioReservation.Forms.Studios;
using CRM_MusicStudioReservation.Forms.Terms;
using CRM_MusicStudioReservation.Forms.Organizations;
using CRM_MusicStudioReservation.Forms.Subscriptions;


namespace CRM_MusicStudioReservation.Forms
{
    public partial class MainForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private Form? _currentChildForm;

        public MainForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_auth.CurrentUser is not null)
            {
                lblUserInfo.Text = $"{_auth.CurrentUser.FullName}   ·   {_auth.CurrentUser.Role}";
            }

            lblPageTitle.UseMnemonic = false;
            btnNavTerms.UseMnemonic = false;

            // Wire nav buttons
            WireNavButton(btnNavDashboard, "Dashboard", ShowDashboard);
            WireNavButton(btnNavOrganizations, "Studio Organizations", ShowOrganizations);
            WireNavButton(btnNavSubscriptions, "Subscriptions", ShowSubscriptions);
            WireNavButton(btnNavTerms, "Terms & Conditions", ShowTermsManagement);
            WireNavButton(btnNavBookings, "Bookings", ShowBookings);
            WireNavButton(btnNavCustomers, "Customers", ShowCustomerManagement);
            WireNavButton(btnNavStudios, "Studios", ShowStudioManagement);
            WireNavButton(btnNavInventory, "Inventory", ShowInventoryManagement);
            WireNavButton(btnNavEngagement, "Customer Engagement", ShowCustomerEngagement);
            WireNavButton(btnNavReports, "Reports", ShowReports);

            pnlNavItems.Resize += (s, e) => RepositionNavButtons();
            SetActiveNav(btnNavDashboard);
            ApplyRoleBasedAccess();
            ShowDashboard();

            // Wire logout button hover
            btnLogout.MouseEnter += (s, e) =>
            {
                btnLogout.BackColor = Color.FromArgb(60, 40, 100);
                btnLogout.ForeColor = Color.White;
            };
            btnLogout.MouseLeave += (s, e) =>
            {
                btnLogout.BackColor = AppTheme.PrimaryDark;
                btnLogout.ForeColor = Color.FromArgb(200, 200, 220);
            };
        }

        // ==================== DASHBOARD ROUTING ====================

        private void ShowDashboard()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            Form dashboard = role switch
            {
                "superadmin" => new SuperAdminDashboardForm(_auth, _api),
                "admin" => new AdminDashboardForm(_auth, _api),
                "staff" => new StaffDashboardForm(_auth, _api),
                _ => new ClientDashboardForm(_auth, _api)
            };

            LoadChildForm(dashboard);
            lblPageTitle.Text = "Dashboard";
        }

        // ==================== BOOKINGS (ROLE-BASED) ====================

        private void ShowBookings()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            if (role == "admin" || role == "superadmin" || role == "staff")
            {
                var mgmtForm = new BookingManagementForm(_auth, _api);
                LoadChildForm(mgmtForm);
                lblPageTitle.Text = "Booking Management";
            }
            else
            {
                var bookForm = new BookStudioForm(_auth, _api);
                LoadChildForm(bookForm);
                lblPageTitle.Text = "Book a Studio";
            }
        }

        // ==================== REPORTS ====================

        private void ShowReports()
        {
            var reportForm = new ReportsForm(_auth, _api);
            LoadChildForm(reportForm);
            lblPageTitle.Text = "Reports";
        }

        // ==================== EXTERNAL NAVIGATION ====================

        public void NavigateToBooking(int bookingId)
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            if (role != "admin" && role != "superadmin" && role != "staff")
            {
                MessageBox.Show(
                    "Booking details are not available for your role.",
                    "Not Available",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var mgmtForm = new BookingManagementForm(_auth, _api);
            LoadChildForm(mgmtForm);
            lblPageTitle.Text = "Booking Management";
            SetActiveNav(btnNavBookings);

            mgmtForm.PreselectBooking(bookingId);
        }

        public void NavigateToOrganizations()
        {
            SetActiveNav(btnNavOrganizations);
            ShowOrganizations();
        }

        public void NavigateToSubscriptions()
        {
            SetActiveNav(btnNavSubscriptions);
            ShowSubscriptions();
        }

        public void NavigateToTerms()
        {
            SetActiveNav(btnNavTerms);
            ShowTermsManagement();
        }

        // ==================== BOOK STUDIO WIZARD ====================

        private void ShowBookStudio()
        {
            var bookForm = new BookStudioForm(_auth, _api);
            LoadChildForm(bookForm);
            lblPageTitle.Text = "Book a Studio";
        }

        // ==================== CUSTOMER MANAGEMENT ====================

        private void ShowCustomerManagement()
        {
            var customerForm = new CustomerManagementForm(_auth, _api);
            LoadChildForm(customerForm);
            lblPageTitle.Text = "Customers";
        }

        // ==================== STUDIO MANAGEMENT ====================

        private void ShowStudioManagement()
        {
            var studioForm = new StudioManagementForm(_auth, _api);
            LoadChildForm(studioForm);
            lblPageTitle.Text = "Studios";
        }

        private void ShowInventoryManagement()
        {
            if (_auth.CurrentUser?.CompanyId != 1)
            {
                MessageBox.Show(
                    "The Inventory Management module is not included in your organization's subscription plan.",
                    "Plan Feature Locked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var invForm = new InventoryManagementForm(_auth, _api);
            LoadChildForm(invForm);
            lblPageTitle.Text = "Inventory";
        }

        // ==================== CUSTOMER ENGAGEMENT ====================

        private void ShowCustomerEngagement()
        {
            var engagementForm = new CustomerEngagementForm(_auth, _api);
            LoadChildForm(engagementForm);
            lblPageTitle.Text = "Customer Engagement";
        }

        // ==================== STUDIO ORGANIZATIONS ====================

        private void ShowOrganizations()
        {
            var orgForm = new OrganizationManagementForm(_auth, _api);
            LoadChildForm(orgForm);
            lblPageTitle.Text = "Studio Organizations";
        }

        // ==================== SUBSCRIPTIONS ====================

        private void ShowSubscriptions()
        {
            var subForm = new SubscriptionManagementForm(_auth, _api);
            LoadChildForm(subForm);
            lblPageTitle.Text = "Subscription Management";
        }

        // ==================== TERMS & CONDITIONS ====================

        private void ShowTermsManagement()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";
            if (role == "superadmin")
            {
                var platformTermsForm = new PlatformTermsManagementForm(_auth, _api);
                LoadChildForm(platformTermsForm);
                lblPageTitle.Text = "Platform Terms & Policies";
                return;
            }

            var termsForm = new TermsManagementForm(_auth, _api);
            LoadChildForm(termsForm);
            lblPageTitle.Text = "Terms & Conditions";
        }

        // ==================== EMBED CHILD FORM (delayed dispose) ====================

        private void LoadChildForm(Form child)
        {
            var old = _currentChildForm;

            if (old != null && !old.IsDisposed)
            {
                pnlContent.Controls.Remove(old);
                old.Hide();

                var timer = new System.Windows.Forms.Timer { Interval = 1500 };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    try
                    {
                        if (!old.IsDisposed)
                            old.Dispose();
                    }
                    catch { /* ignore */ }
                };
                timer.Start();
            }

            pnlContent.Controls.Clear();

            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(child);
            child.Show();

            _currentChildForm = child;
        }

        // ==================== NAVIGATION ====================

        private void WireNavButton(Button btn, string label, Action action)
        {
            btn.MouseEnter += (s, e) =>
            {
                if (btn.BackColor != AppTheme.Primary)
                {
                    btn.BackColor = Color.FromArgb(60, 40, 100);
                    btn.ForeColor = Color.White;
                }
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn.BackColor != AppTheme.Primary)
                {
                    btn.BackColor = AppTheme.PrimaryDark;
                    btn.ForeColor = Color.FromArgb(200, 200, 220);
                }
            };

            btn.Click += (s, e) =>
            {
                SetActiveNav(btn);
                lblPageTitle.Text = label;
                action.Invoke();
            };
        }

        private void SetActiveNav(Button active)
        {
            foreach (var ctrl in pnlNavItems.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = AppTheme.PrimaryDark;
                    btn.ForeColor = Color.FromArgb(200, 200, 220);
                }
            }

            active.BackColor = AppTheme.Primary;
            active.ForeColor = Color.White;
        }

        // ==================== ROLE-BASED ACCESS ====================

        private void ApplyRoleBasedAccess()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            // Default all SA-specific to false, tenant-specific to true
            btnNavOrganizations.Visible = false;
            btnNavSubscriptions.Visible = false;

            btnNavDashboard.Visible = true;
            btnNavBookings.Visible = true;
            btnNavCustomers.Visible = true;
            btnNavStudios.Visible = true;
            // Feature gating: Inventory is exclusively active for Company 1
            bool hasInventoryModule = (_auth.CurrentUser?.CompanyId == 1);
            btnNavInventory.Visible = hasInventoryModule;
            btnNavEngagement.Visible = true;
            btnNavReports.Visible = true;
            btnNavTerms.Visible = true;

            switch (role)
            {
                case "superadmin":
                    // Super Admin sees ONLY: Dashboard, Organizations, Subscriptions, Terms & Conditions
                    btnNavOrganizations.Visible = true;
                    btnNavSubscriptions.Visible = true;
                    btnNavTerms.Visible = true;

                    btnNavBookings.Visible = false;
                    btnNavCustomers.Visible = false;
                    btnNavStudios.Visible = false;
                    btnNavInventory.Visible = false;
                    btnNavEngagement.Visible = false;
                    btnNavReports.Visible = false;
                    break;

                case "client":
                    btnNavCustomers.Visible = false;
                    btnNavStudios.Visible = false;
                    btnNavInventory.Visible = false;
                    btnNavEngagement.Visible = false;
                    btnNavReports.Visible = false;
                    btnNavTerms.Visible = false;
                    break;

                case "staff":
                    btnNavReports.Visible = false;
                    btnNavInventory.Visible = hasInventoryModule;
                    break;

                case "admin":
                    btnNavInventory.Visible = hasInventoryModule;
                    break;
            }

            RepositionNavButtons();
        }

        private void RepositionNavButtons()
        {
            Button[] navButtons = new[]
            {
                btnNavDashboard,
                btnNavOrganizations,
                btnNavSubscriptions,
                btnNavTerms,
                btnNavBookings,
                btnNavCustomers,
                btnNavStudios,
                btnNavInventory,
                btnNavReports,
                btnNavEngagement
            };

            float dpi = this.DeviceDpi > 0 ? this.DeviceDpi / 96.0f : 1.0f;
            int buttonX = Math.Max(10, (int)(12 * dpi));
            int topMargin = Math.Max(10, (int)(14 * dpi));
            int buttonHeight = Math.Max(44, (int)(46 * dpi));
            int spacing = Math.Max(6, (int)(10 * dpi));
            int radius = Math.Max(6, (int)(8 * dpi));

            int containerWidth = pnlNavItems.ClientSize.Width;
            if (containerWidth <= 0)
                containerWidth = pnlSidebar.ClientSize.Width;
            if (containerWidth <= 0)
                containerWidth = (int)(240 * dpi);

            int buttonWidth = Math.Max(200, containerWidth - (buttonX * 2));

            int y = topMargin;
            foreach (var btn in navButtons)
            {
                if (btn.Visible)
                {
                    btn.Location = new Point(buttonX, y);
                    btn.Size = new Size(buttonWidth, buttonHeight);
                    btn.AutoEllipsis = true;
                    btn.UseMnemonic = false;
                    RoundedCorners.Apply(btn, radius);
                    y += buttonHeight + spacing;
                }
            }
        }

        // ==================== PLACEHOLDERS ====================

        private void ShowPlaceholder(string section)
        {
            var lbl = new Label
            {
                Text = $"{section}\n\nComing soon — this section is under construction.",
                Font = new Font("Segoe UI", 14F),
                ForeColor = AppTheme.TextSecondary,
                AutoSize = true,
                Location = new Point(40, 40)
            };

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.Background
            };
            panel.Controls.Add(lbl);

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(panel);

            _currentChildForm?.Close();
            _currentChildForm?.Dispose();
            _currentChildForm = null;
        }

        // ==================== ACTIONS ====================

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _auth.Logout();

                var loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}