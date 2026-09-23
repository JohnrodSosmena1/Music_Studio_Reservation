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

            // Wire nav buttons
            WireNavButton(btnNavDashboard, "Dashboard", ShowDashboard);
            WireNavButton(btnNavBookings, "Bookings", ShowBookings);
            WireNavButton(btnNavCustomers, "Customers", ShowCustomerManagement);
            WireNavButton(btnNavStudios, "Studios", ShowStudioManagement);
            WireNavButton(btnNavInventory, "Inventory", ShowInventoryManagement);
            WireNavButton(btnNavEngagement, "Customer Engagement", ShowCustomerEngagement);
            WireNavButton(btnNavReports, "Reports", ShowReports);

            SetActiveNav(btnNavDashboard);
            ApplyRoleBasedAccess();
            ShowDashboard();
        }

        // ==================== DASHBOARD ROUTING ====================

        private void ShowDashboard()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            Form dashboard = role switch
            {
                "superadmin" or "admin" => new AdminDashboardForm(_auth, _api),
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

        // ==================== INVENTORY MANAGEMENT ====================

        private void ShowInventoryManagement()
        {
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
                    btn.BackColor = Color.FromArgb(60, 40, 100);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn.BackColor != AppTheme.Primary)
                    btn.BackColor = AppTheme.PrimaryDark;
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
                }
            }

            active.BackColor = AppTheme.Primary;
        }

        // ==================== ROLE-BASED ACCESS ====================

        private void ApplyRoleBasedAccess()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "client";

            switch (role)
            {
                case "client":
                    btnNavCustomers.Visible = false;
                    btnNavStudios.Visible = false;
                    btnNavInventory.Visible = false;
                    btnNavEngagement.Visible = false;
                    btnNavReports.Visible = false;
                    break;

                case "staff":
                    btnNavReports.Visible = false;
                    // Staff CAN see Customer Engagement
                    break;

                case "admin":
                case "superadmin":
                    break;
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