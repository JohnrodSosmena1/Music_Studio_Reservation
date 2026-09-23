using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Customers
{
    public partial class CustomerManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerService _customerService;

        private List<CustomerDto> _allCustomers = new();

        public CustomerManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customerService = new CustomerService(api);
        }

        private async void CustomerManagementForm_Load(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        // ==================== DATA LOADING ====================

        private async Task LoadCustomersAsync()
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
                var statusText = c.IsActive ? "Active" : "Inactive";

                // 👇 Action button label depends on active state
                var actionLabel = c.IsActive ? "Edit / Disable" : "Edit / Enable";

                var idx = dgvCustomers.Rows.Add(
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    c.ContactNumber ?? "—",
                    c.EmailAddress ?? "—",
                    statusText,
                    actionLabel
                );

                // Color-code Status cell
                var statusCell = dgvCustomers.Rows[idx].Cells[colStatus.Index];
                statusCell.Style.ForeColor = c.IsActive
                    ? Color.FromArgb(16, 185, 129)     // green
                    : Color.FromArgb(239, 68, 68);     // red
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Tag row with customer ID
                dgvCustomers.Rows[idx].Tag = c.CustomerId;
            }
        }

        // ==================== SEARCH ====================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(search))
            {
                RenderRows(_allCustomers);
                return;
            }

            var filtered = _allCustomers
                .Where(c => c.CustomerName.ToLowerInvariant().Contains(search)
                         || c.CustomerCode.ToLowerInvariant().Contains(search)
                         || (c.EmailAddress ?? "").ToLowerInvariant().Contains(search))
                .ToList();

            RenderRows(filtered);
            lblCount.Text = $"{filtered.Count} of {_allCustomers.Count} customer(s)";
        }

        // ==================== BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            using var dialog = new CustomerEditForm(_customerService, companyId, null);
            dialog.ShowDialog(this);

            if (dialog.SavedSuccessfully)
                await LoadCustomersAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        // ==================== ROW ACTIONS (Edit / Enable / Disable) ====================

        private async void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only handle clicks in the Actions column
            if (e.RowIndex < 0 || e.ColumnIndex != colActions.Index) return;

            var row = dgvCustomers.Rows[e.RowIndex];
            var customerId = (int)(row.Tag ?? 0);

            var customer = _allCustomers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null) return;

            // Show different dialog based on current state
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
                // ============ EDIT ============
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                using var dialog = new CustomerEditForm(_customerService, companyId, customer);
                dialog.ShowDialog(this);

                if (dialog.SavedSuccessfully)
                    await LoadCustomersAsync();
            }
            else if (choice == DialogResult.No)
            {
                // ============ TOGGLE ACTIVE STATUS ============
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