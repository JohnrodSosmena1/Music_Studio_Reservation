using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM.winforms.Forms.Customers.Tabs;

namespace CRM_MusicStudioReservation.Forms.Customers
{
    public partial class CustomerManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerService _customerService;

        private List<CustomerDto> _allCustomers = new();
        private UserControl? _currentTab;
        private Button? _activeTabButton;

        // Tab colors (matching Engagement)
        private static readonly Color ActiveTabColor = Color.FromArgb(139, 92, 246);
        private static readonly Color InactiveTabColor = Color.FromArgb(107, 114, 128);
        private static readonly Color ActiveUnderlineColor = Color.FromArgb(139, 92, 246);

        public CustomerManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customerService = new CustomerService(api);
        }

        private async void CustomerManagementForm_Load(object sender, EventArgs e)
        {
            // Default to All Customers tab
            ShowAllCustomers();
        }

        // ==================== TAB SWITCHING ====================

        private void SetActiveTab(Button tab)
        {
            // Reset all tabs to inactive
            foreach (Control ctrl in pnlTabs.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.ForeColor = InactiveTabColor;
                    btn.BackColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Paint -= TabButton_Paint;
                }
            }

            // Highlight active
            tab.ForeColor = ActiveTabColor;
            tab.BackColor = Color.White;
            tab.FlatAppearance.BorderSize = 0;
            tab.Paint += TabButton_Paint;

            _activeTabButton = tab;
        }

        private void TabButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn != _activeTabButton) return;

            using var pen = new Pen(ActiveUnderlineColor, 3);
            e.Graphics.DrawLine(pen, 0, btn.Height - 3, btn.Width, btn.Height - 3);
        }

        private void LoadTab(UserControl tab)
        {
            pnlContent.Controls.Clear();

            _currentTab?.Dispose();
            _currentTab = tab;

            tab.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(tab);
        }

        // ==================== TAB HANDLERS ====================

        private async void btnTabAllCustomers_Click(object sender, EventArgs e) => ShowAllCustomers();
        private void btnTabLoyalty_Click(object sender, EventArgs e) => ShowLoyalty();

        // ==================== TAB CONTENT ====================

        private async void ShowAllCustomers()
        {
            SetActiveTab(btnTabAllCustomers);

            // Re-attach the existing pnlAllCustomers panel (which holds the grid)
            pnlContent.Controls.Clear();
            _currentTab?.Dispose();
            _currentTab = null;

            pnlContent.Controls.Add(pnlAllCustomers);
            pnlAllCustomers.Dock = DockStyle.Fill;

            // Load customer data
            await LoadCustomersAsync();
        }

        private void ShowLoyalty()
        {
            SetActiveTab(btnTabLoyalty);

            var tab = new LoyaltyPointsTab(_auth, _api);
            LoadTab(tab);
        }

        // ==================== DATA LOADING (Original) ====================

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            dgvCustomers.Rows.Clear();
            _allCustomers = await _customerService.GetAllAsync(companyId);

            RenderRows(_allCustomers);
            lblCount.Text = $"{_allCustomers.Count} customer(s)";
        }

        private void RenderRows(List<CustomerDto> customers)
        {
            dgvCustomers.Rows.Clear();

            foreach (var c in customers)
            {
                var displayName = !string.IsNullOrWhiteSpace(c.FirstName) || !string.IsNullOrWhiteSpace(c.LastName)
                    ? $"{c.FirstName} {c.LastName}".Trim()
                    : c.CustomerName;

                var statusText = c.IsActive ? "Active" : "Inactive";
                var actionLabel = c.IsActive ? "Edit / Disable" : "Edit / Enable";

                var idx = dgvCustomers.Rows.Add(
                    c.CustomerId,
                    c.CustomerCode,
                    displayName,
                    c.ContactNumber ?? "—",
                    c.EmailAddress ?? "—",
                    statusText,
                    actionLabel
                );

                var statusCell = dgvCustomers.Rows[idx].Cells[colStatus.Index];
                statusCell.Style.ForeColor = c.IsActive
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                dgvCustomers.Rows[idx].Tag = c.CustomerId;
            }
        }

        // ==================== SEARCH (Original) ====================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(search))
            {
                RenderRows(_allCustomers);
                return;
            }

            var filtered = _allCustomers
                .Where(c => (c.CustomerName ?? "").ToLowerInvariant().Contains(search)
                         || (c.FirstName ?? "").ToLowerInvariant().Contains(search)
                         || (c.LastName ?? "").ToLowerInvariant().Contains(search)
                         || (c.CustomerCode ?? "").ToLowerInvariant().Contains(search)
                         || (c.ContactNumber ?? "").ToLowerInvariant().Contains(search)
                         || (c.EmailAddress ?? "").ToLowerInvariant().Contains(search))
                .ToList();

            RenderRows(filtered);
            lblCount.Text = $"{filtered.Count} of {_allCustomers.Count} customer(s)";
        }

        // ==================== BUTTONS (Original) ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            using var dialog = new CustomerEditForm(_customerService, companyId, null);
            dialog.ShowDialog(this.FindForm() ?? this);

            if (dialog.SavedSuccessfully)
                await LoadCustomersAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        // ==================== ROW ACTIONS (Original) ====================

        private async void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colActions.Index) return;

            var row = dgvCustomers.Rows[e.RowIndex];
            var customerId = (int)(row.Tag ?? 0);

            var customer = _allCustomers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null) return;

            var action = customer.IsActive ? "Disable" : "Enable";

            var choice = MessageBox.Show(
                $"Customer: {customer.CustomerName}\n\n" +
                $"Yes = Edit\n" +
                $"No = {action}\n" +
                $"Cancel = Do nothing",
                "Choose Action",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                using var dialog = new CustomerEditForm(_customerService, companyId, customer);
                dialog.ShowDialog(this.FindForm() ?? this);

                if (dialog.SavedSuccessfully)
                    await LoadCustomersAsync();
            }
            else if (choice == DialogResult.No)
            {
                var newStatus = !customer.IsActive;

                var confirm = MessageBox.Show(
                    $"Are you sure you want to {(newStatus ? "enable" : "disable")} '{customer.CustomerName}'?\n\n" +
                    (newStatus
                        ? "The customer will be marked as active."
                        : "The customer will be marked as inactive (stays in the system)."),
                    $"Confirm {action}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;

                var updateReq = new CustomerUpdateRequest
                {
                    IsActive = newStatus
                };

                var result = await _customerService.UpdateAsync(companyId, customerId, updateReq);

                if (result != null)
                {
                    MessageBox.Show(
                        $"Customer {action.ToLower()}d successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomersAsync();
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to {action.ToLower()} customer.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}