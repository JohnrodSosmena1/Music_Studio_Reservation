using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Subscriptions
{
    public class SubscriptionManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly SuperAdminService _service;

        // UI Controls
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;

        private TabControl tabControl = null!;
        private TabPage tabSubscriptions = null!;
        private TabPage tabPlans = null!;
        private TabPage tabInvoices = null!;

        // Grids
        private DataGridView dgvSubs = null!;
        private DataGridView dgvPlans = null!;
        private DataGridView dgvInvoices = null!;

        private Button btnAssignPlan = null!;
        private Button btnAddPlan = null!;
        private Button btnRefresh = null!;

        public SubscriptionManagementForm(AuthService auth, ApiClient api)
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
            this.Name = "SubscriptionManagementForm";
            this.Text = "Subscriptions";
            this.Load += async (s, e) => await LoadAllDataAsync();

            // Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(30, 20, 30, 10),
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "Subscriptions & Billing",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = true,
                Location = new Point(30, 16),
                UseMnemonic = false
            };

            lblSubtitle = new Label
            {
                Text = "Manage tenant plans, billing cycles, pricing tiers, and platform revenue invoices",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(30, 60),
                UseMnemonic = false
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                Padding = new Point(16, 8)
            };

            // Tab 1: Subscriptions
            tabSubscriptions = new TabPage("Tenant Subscriptions");
            tabSubscriptions.BackColor = Color.White;
            SetupSubscriptionsTab();

            // Tab 2: Plans
            tabPlans = new TabPage("Subscription Plans (Tiers)");
            tabPlans.BackColor = Color.White;
            SetupPlansTab();

            // Tab 3: Invoices
            tabInvoices = new TabPage("Platform Invoices");
            tabInvoices.BackColor = Color.White;
            SetupInvoicesTab();

            tabControl.TabPages.Add(tabSubscriptions);
            tabControl.TabPages.Add(tabPlans);
            tabControl.TabPages.Add(tabInvoices);

            var pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 10, 30, 30),
                BackColor = Color.Transparent
            };
            pnlContainer.Controls.Add(tabControl);

            this.Controls.Add(pnlContainer);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private void SetupSubscriptionsTab()
        {
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(15) };
            btnAssignPlan = new Button
            {
                Text = "+ Assign / Upgrade Plan",
                Size = new Size(220, 34),
                Location = new Point(15, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 92, 246),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnAssignPlan.FlatAppearance.BorderSize = 0;
            btnAssignPlan.Click += (s, e) => OpenAssignModal();

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Size = new Size(100, 34),
                Location = new Point(245, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(55, 65, 81),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnRefresh.Click += async (s, e) => await LoadSubscriptionsAsync();

            pnlTop.Controls.Add(btnAssignPlan);
            pnlTop.Controls.Add(btnRefresh);

            dgvSubs = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 44 }
            };
            dgvSubs.Columns.Add("colSOrg", "Organization Name");
            dgvSubs.Columns.Add("colSCode", "Tenant Code");
            dgvSubs.Columns.Add("colSPlan", "Assigned Plan");
            dgvSubs.Columns.Add("colSPrice", "Monthly Fee");
            dgvSubs.Columns.Add("colSStatus", "Status");
            dgvSubs.Columns.Add("colSStarted", "Start Date");
            dgvSubs.Columns.Add("colSExpires", "Expiry Date");
            dgvSubs.Columns.Add("colSAuto", "Auto-Renew");

            tabSubscriptions.Controls.Add(dgvSubs);
            tabSubscriptions.Controls.Add(pnlTop);
            dgvSubs.BringToFront();
        }

        private void SetupPlansTab()
        {
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(15) };
            btnAddPlan = new Button
            {
                Text = "+ Create New Plan",
                Size = new Size(180, 34),
                Location = new Point(15, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 92, 246),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            btnAddPlan.FlatAppearance.BorderSize = 0;
            btnAddPlan.Click += (s, e) => OpenCreatePlanModal();
            pnlTop.Controls.Add(btnAddPlan);

            dgvPlans = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 44 }
            };
            dgvPlans.Columns.Add("colPCode", "Plan Code");
            dgvPlans.Columns.Add("colPName", "Plan Name");
            dgvPlans.Columns.Add("colPPrice", "Price (₱)");
            dgvPlans.Columns.Add("colPCycle", "Billing Cycle");
            dgvPlans.Columns.Add("colPUsers", "Max Users");
            dgvPlans.Columns.Add("colPBookings", "Max Bookings/Mo");
            dgvPlans.Columns.Add("colPStorage", "Storage (MB)");
            dgvPlans.Columns.Add("colPSubs", "Active Tenants");
            dgvPlans.Columns.Add("colPStatus", "Status");

            tabPlans.Controls.Add(dgvPlans);
            tabPlans.Controls.Add(pnlTop);
            dgvPlans.BringToFront();
        }

        private void SetupInvoicesTab()
        {
            dgvInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 44 }
            };
            dgvInvoices.Columns.Add("colInvNum", "Invoice #");
            dgvInvoices.Columns.Add("colInvOrg", "Organization Name");
            dgvInvoices.Columns.Add("colInvAmt", "Amount");
            dgvInvoices.Columns.Add("colInvStat", "Status");
            dgvInvoices.Columns.Add("colInvIssued", "Issued Date");
            dgvInvoices.Columns.Add("colInvDue", "Due Date");
            dgvInvoices.Columns.Add("colInvPaid", "Paid Date");
            dgvInvoices.Columns.Add("colInvMethod", "Payment Method");

            var actionCol = new DataGridViewButtonColumn
            {
                Name = "colAction",
                HeaderText = "Action",
                Text = "Mark Paid",
                UseColumnTextForButtonValue = true,
                FillWeight = 60,
                FlatStyle = FlatStyle.Flat
            };
            actionCol.DefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            actionCol.DefaultCellStyle.ForeColor = Color.FromArgb(16, 185, 129);
            actionCol.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dgvInvoices.Columns.Add(actionCol);
            dgvInvoices.CellContentClick += async (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvInvoices.Columns[e.ColumnIndex].Name == "colAction")
                {
                    if (dgvInvoices.Rows[e.RowIndex].Tag is SubscriptionInvoiceDto inv && inv.Status != "Paid")
                    {
                        if (await _service.MarkInvoicePaidAsync(inv.SubscriptionInvoiceId))
                        {
                            MessageBox.Show($"Invoice {inv.InvoiceNumber} marked as Paid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadInvoicesAsync();
                        }
                    }
                }
            };

            tabInvoices.Controls.Add(dgvInvoices);
        }

        private async Task LoadAllDataAsync()
        {
            await Task.WhenAll(LoadSubscriptionsAsync(), LoadPlansAsync(), LoadInvoicesAsync());
        }

        private async Task LoadSubscriptionsAsync()
        {
            try
            {
                var list = await _service.GetSubscriptionsAsync();
                dgvSubs.Rows.Clear();
                foreach (var s in list ?? new())
                {
                    var rIdx = dgvSubs.Rows.Add(
                        s.CompanyName,
                        s.CompanyCode,
                        s.PlanName,
                        $"₱{s.Price:N2}",
                        s.Status,
                        s.StartedAt.ToString("MMM d, yyyy"),
                        s.ExpiresAt.ToString("MMM d, yyyy"),
                        s.AutoRenew ? "Yes" : "No"
                    );
                    dgvSubs.Rows[rIdx].Tag = s;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoadSubscriptions] {ex.Message}");
            }
        }

        private async Task LoadPlansAsync()
        {
            try
            {
                var list = await _service.GetPlansAsync();
                dgvPlans.Rows.Clear();
                foreach (var p in list ?? new())
                {
                    var rIdx = dgvPlans.Rows.Add(
                        p.PlanCode,
                        p.PlanName,
                        p.Price.ToString("N2"),
                        p.BillingCycle,
                        p.MaxUsers,
                        p.MaxBookingsPerMonth,
                        p.MaxStorageMb,
                        p.ActiveSubscribersCount,
                        p.IsActive ? "Active" : "Inactive"
                    );
                    dgvPlans.Rows[rIdx].Tag = p;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoadPlans] {ex.Message}");
            }
        }

        private async Task LoadInvoicesAsync()
        {
            try
            {
                var list = await _service.GetInvoicesAsync();
                dgvInvoices.Rows.Clear();
                foreach (var i in list ?? new())
                {
                    var rIdx = dgvInvoices.Rows.Add(
                        i.InvoiceNumber,
                        i.CompanyName,
                        $"₱{i.Amount:N2}",
                        i.Status,
                        i.IssuedAt.ToString("MMM d, yyyy"),
                        i.DueAt.ToString("MMM d, yyyy"),
                        i.PaidAt.HasValue ? i.PaidAt.Value.ToString("MMM d, yyyy") : "—",
                        i.PaymentMethod ?? "Credit Card"
                    );
                    dgvInvoices.Rows[rIdx].Tag = i;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoadInvoices] {ex.Message}");
            }
        }

        private void OpenAssignModal()
        {
            using var modal = new AssignPlanModal(_service);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadSubscriptionsAsync();
                _ = LoadInvoicesAsync();
            }
        }

        private void OpenCreatePlanModal()
        {
            using var modal = new EditPlanModal(_service);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadPlansAsync();
            }
        }
    }

    // Modal to Assign Plan
    public class AssignPlanModal : Form
    {
        private readonly SuperAdminService _service;
        private ComboBox cmbOrg = null!;
        private ComboBox cmbPlan = null!;
        private NumericUpDown numMonths = null!;
        private CheckBox chkAuto = null!;
        private CheckBox chkInvoice = null!;
        private Button btnSave = null!;

        private List<OrganizationListItemDto> _orgs = new();
        private List<SubscriptionPlanDto> _plans = new();

        public AssignPlanModal(SuperAdminService service)
        {
            _service = service;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Assign / Upgrade Subscription Plan";
            this.Size = new Size(480, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "Assign Plan to Organization", Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };

            int y = 70;
            this.Controls.Add(new Label { Text = "Select Organization *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            cmbOrg = new ComboBox { Location = new Point(25, y + 22), Size = new Size(410, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(cmbOrg);
            y += 65;

            this.Controls.Add(new Label { Text = "Select Subscription Plan *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            cmbPlan = new ComboBox { Location = new Point(25, y + 22), Size = new Size(410, 30), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(cmbPlan);
            y += 65;

            this.Controls.Add(new Label { Text = "Duration (Months)", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            numMonths = new NumericUpDown { Location = new Point(25, y + 22), Size = new Size(150, 30), Minimum = 1, Maximum = 36, Value = 1, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(numMonths);

            chkAuto = new CheckBox { Text = "Auto-Renew", Location = new Point(200, y + 22), AutoSize = true, Checked = true, Font = new Font("Segoe UI", 9F) };
            this.Controls.Add(chkAuto);
            y += 65;

            chkInvoice = new CheckBox { Text = "Generate invoice for this plan assignment", Location = new Point(25, y), AutoSize = true, Checked = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.Controls.Add(chkInvoice);
            y += 45;

            btnSave = new Button { Text = "Confirm Assignment", Location = new Point(285, y), Size = new Size(150, 36), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(139, 92, 246), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += async (s, e) => await SaveAsync();

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnSave);

            this.Load += async (s, e) => await LoadLookupsAsync();
        }

        private async Task LoadLookupsAsync()
        {
            var orgsRes = await _service.GetOrganizationsAsync(pageSize: 100);
            _orgs = orgsRes?.Items ?? new();
            foreach (var o in _orgs) cmbOrg.Items.Add($"{o.CompanyName} ({o.CompanyCode})");
            if (cmbOrg.Items.Count > 0) cmbOrg.SelectedIndex = 0;

            _plans = await _service.GetPlansAsync();
            foreach (var p in _plans) cmbPlan.Items.Add($"{p.PlanName} — ₱{p.Price:N0}/{p.BillingCycle}");
            if (cmbPlan.Items.Count > 0) cmbPlan.SelectedIndex = 0;
        }

        private async Task SaveAsync()
        {
            if (cmbOrg.SelectedIndex < 0 || cmbPlan.SelectedIndex < 0) return;

            var org = _orgs[cmbOrg.SelectedIndex];
            var plan = _plans[cmbPlan.SelectedIndex];

            var dto = new AssignSubscriptionDto
            {
                CompanyId = org.CompanyId,
                SubscriptionPlanId = plan.SubscriptionPlanId,
                DurationMonths = (int)numMonths.Value,
                AutoRenew = chkAuto.Checked,
                GenerateInvoice = chkInvoice.Checked
            };

            if (await _service.AssignPlanAsync(dto))
            {
                MessageBox.Show($"Assigned {plan.PlanName} to {org.CompanyName} successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }

    // Modal to Create Plan
    public class EditPlanModal : Form
    {
        private readonly SuperAdminService _service;
        private TextBox txtCode = null!;
        private TextBox txtName = null!;
        private NumericUpDown numPrice = null!;
        private NumericUpDown numUsers = null!;
        private NumericUpDown numBookings = null!;
        private TextBox txtFeatures = null!;
        private Button btnSave = null!;

        public EditPlanModal(SuperAdminService service)
        {
            _service = service;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Create Subscription Plan";
            this.Size = new Size(460, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "New Subscription Plan", Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };

            int y = 65;
            this.Controls.Add(new Label { Text = "Plan Code * (e.g. PLAN-PRO)", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtCode = new TextBox { Location = new Point(25, y + 22), Size = new Size(390, 28), Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtCode);
            y += 60;

            this.Controls.Add(new Label { Text = "Plan Name *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtName = new TextBox { Location = new Point(25, y + 22), Size = new Size(390, 28), Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtName);
            y += 60;

            this.Controls.Add(new Label { Text = "Price (₱/Month) *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            numPrice = new NumericUpDown { Location = new Point(25, y + 22), Size = new Size(180, 28), Maximum = 100000, DecimalPlaces = 2, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(numPrice);

            this.Controls.Add(new Label { Text = "User Limit", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(230, y), AutoSize = true });
            numUsers = new NumericUpDown { Location = new Point(230, y + 22), Size = new Size(185, 28), Maximum = 500, Value = 10, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(numUsers);
            y += 60;

            this.Controls.Add(new Label { Text = "Bookings Limit / Month", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            numBookings = new NumericUpDown { Location = new Point(25, y + 22), Size = new Size(180, 28), Maximum = 10000, Value = 500, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(numBookings);
            y += 60;

            this.Controls.Add(new Label { Text = "Key Features (comma-separated)", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtFeatures = new TextBox { Location = new Point(25, y + 22), Size = new Size(390, 28), Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtFeatures);
            y += 65;

            btnSave = new Button { Text = "Save Plan", Location = new Point(295, y), Size = new Size(120, 36), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(139, 92, 246), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += async (s, e) => await SaveAsync();

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnSave);
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please fill in Code and Name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new SubscriptionPlanCreateUpdateDto
            {
                PlanCode = txtCode.Text.Trim().ToUpperInvariant(),
                PlanName = txtName.Text.Trim(),
                Price = numPrice.Value,
                MaxUsers = (int)numUsers.Value,
                MaxBookingsPerMonth = (int)numBookings.Value,
                Features = txtFeatures.Text.Trim()
            };

            if (await _service.CreatePlanAsync(dto))
            {
                MessageBox.Show("Plan created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
