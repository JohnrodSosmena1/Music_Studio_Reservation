using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Organizations
{
    public class OrganizationDetailModal : Form
    {
        private readonly SuperAdminService _service;
        private readonly int _companyId;

        private Label lblHeaderTitle = null!;
        private Label lblHeaderSubtitle = null!;
        private TabControl tabControl = null!;

        // Tabs
        private TabPage tabOverview = null!;
        private TabPage tabUsers = null!;
        private TabPage tabBilling = null!;
        private TabPage tabTerms = null!;

        // Overview controls
        private Label lblOverviewInfo = null!;

        // Users grid
        private DataGridView dgvUsers = null!;

        // Invoices grid
        private DataGridView dgvInvoices = null!;

        // Terms list
        private ListBox lbTerms = null!;

        private Button btnClose = null!;

        public OrganizationDetailModal(SuperAdminService service, int companyId)
        {
            _service = service;
            _companyId = companyId;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Organization Details";
            this.Size = new Size(720, 620);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            lblHeaderTitle = new Label
            {
                Text = "Loading Organization...",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(25, 18),
                AutoSize = true,
                UseMnemonic = false
            };

            lblHeaderSubtitle = new Label
            {
                Text = "Tenant Scope Details & Subscriptions",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(25, 58),
                AutoSize = true,
                UseMnemonic = false
            };

            tabControl = new TabControl
            {
                Location = new Point(25, 96),
                Size = new Size(650, 410),
                Font = new Font("Segoe UI", 9.5F)
            };

            // Tab 1: Overview
            tabOverview = new TabPage("Overview");
            lblOverviewInfo = new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(55, 65, 81),
                Text = "Loading details..."
            };
            tabOverview.Controls.Add(lblOverviewInfo);

            // Tab 2: Users
            tabUsers = new TabPage("Tenant Users");
            dgvUsers = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvUsers.Columns.Add("colUId", "ID");
            dgvUsers.Columns.Add("colUName", "Full Name");
            dgvUsers.Columns.Add("colUEmail", "Email");
            dgvUsers.Columns.Add("colURole", "Role");
            dgvUsers.Columns.Add("colULast", "Last Login");
            tabUsers.Controls.Add(dgvUsers);

            // Tab 3: Billing
            tabBilling = new TabPage("Invoices & Billing");
            dgvInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvInvoices.Columns.Add("colInvNum", "Invoice #");
            dgvInvoices.Columns.Add("colInvAmt", "Amount");
            dgvInvoices.Columns.Add("colInvStat", "Status");
            dgvInvoices.Columns.Add("colInvIssued", "Issued Date");
            dgvInvoices.Columns.Add("colInvPaid", "Paid Date");
            tabBilling.Controls.Add(dgvInvoices);

            // Tab 4: Terms
            tabTerms = new TabPage("Platform T&C Acceptance");
            lbTerms = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                ItemHeight = 24
            };
            tabTerms.Controls.Add(lbTerms);

            tabControl.TabPages.Add(tabOverview);
            tabControl.TabPages.Add(tabUsers);
            tabControl.TabPages.Add(tabBilling);
            tabControl.TabPages.Add(tabTerms);

            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(575, 515),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(55, 65, 81),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblHeaderTitle);
            this.Controls.Add(lblHeaderSubtitle);
            this.Controls.Add(tabControl);
            this.Controls.Add(btnClose);

            this.Load += async (s, e) => await LoadDetailsAsync();
        }

        private async Task LoadDetailsAsync()
        {
            try
            {
                var detail = await _service.GetOrganizationDetailAsync(_companyId);
                if (detail == null || this.IsDisposed) return;

                lblHeaderTitle.Text = $"{detail.CompanyName} ({detail.CompanyCode})";
                lblHeaderSubtitle.Text = $"Status: {detail.Status}   ·   Subdomain: {(detail.Subdomain != null ? detail.Subdomain + ".crmapp.com" : "none")}";

                lblOverviewInfo.Text =
                    $"• Tenant ID: {detail.CompanyId}\n" +
                    $"• Tenant Code: {detail.CompanyCode}\n" +
                    $"• Studio Organization: {detail.CompanyName}\n" +
                    $"• Subdomain: {(detail.Subdomain != null ? detail.Subdomain + ".crmapp.com" : "—")}\n\n" +
                    $"• Owner / Admin: {detail.OwnerFirstName} {detail.OwnerLastName}\n" +
                    $"• Owner Email: {detail.OwnerEmail}\n" +
                    $"• Contact Number: {detail.ContactNumber ?? "—"}\n" +
                    $"• Time Zone: {detail.TimeZone}\n\n" +
                    $"• Subscription Plan: {detail.PlanName} (₱{detail.PlanPrice:N2}/month)\n" +
                    $"• Status: {detail.Status}\n" +
                    $"• Subscription Period: {(detail.SubscriptionStart.HasValue ? detail.SubscriptionStart.Value.ToString("MMM d, yyyy") : "—")} to {(detail.SubscriptionEnd.HasValue ? detail.SubscriptionEnd.Value.ToString("MMM d, yyyy") : "—")}\n" +
                    $"• Created Date: {detail.CreatedAt:MMM d, yyyy hh:mm tt}\n\n" +
                    $"• Total Tenant Users: {detail.TotalUsers}";

                // Load Users
                dgvUsers.Rows.Clear();
                foreach (var u in detail.Users)
                {
                    dgvUsers.Rows.Add(
                        u.UserId,
                        u.FullName,
                        u.Email,
                        u.Role,
                        u.LastLoginAt.HasValue ? u.LastLoginAt.Value.ToString("MMM d, yyyy") : "Never"
                    );
                }

                // Load Invoices
                dgvInvoices.Rows.Clear();
                foreach (var inv in detail.Invoices)
                {
                    dgvInvoices.Rows.Add(
                        inv.InvoiceNumber,
                        $"₱{inv.Amount:N2}",
                        inv.Status,
                        inv.IssuedAt.ToString("MMM d, yyyy"),
                        inv.PaidAt.HasValue ? inv.PaidAt.Value.ToString("MMM d, yyyy") : "—"
                    );
                }

                // Load T&C Acknowledgments
                lbTerms.Items.Clear();
                if (detail.TandCAcknowledgments.Count == 0)
                {
                    lbTerms.Items.Add("No platform terms acknowledged yet.");
                }
                else
                {
                    foreach (var a in detail.TandCAcknowledgments)
                    {
                        lbTerms.Items.Add($"✓ [{a.Version}] {a.TandCTitle} — Accepted by {a.AcknowledgedByEmail} on {a.AcknowledgedAt:MMM d, yyyy}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OrganizationDetailModal] {ex.Message}");
            }
        }
    }
}
