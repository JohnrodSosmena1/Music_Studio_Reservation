using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class InquiriesTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerInquiryService _inquiryService;

        private List<CustomerInquiryDto> _allInquiries = new();
        private bool _suppressFilterEvents = false;

        public InquiriesTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inquiryService = new CustomerInquiryService(api);
        }

        // ==================== LOAD ====================

        private async void InquiriesTab_Load(object sender, EventArgs e)
        {
            SetupFilters();
            PositionTopBarButtons();
            await LoadInquiriesAsync();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionTopBarButtons();
        }

        private void PositionTopBarButtons()
        {
            if (this.IsDisposed || pnlTopBar == null || pnlTopBar.IsDisposed) return;
            if (btnAdd == null || btnRefresh == null) return;

            int rightMargin = 20;
            int gap = 10;
            int y = 25;
            int panelWidth = pnlTopBar.Width;

            int addX = panelWidth - rightMargin - btnAdd.Width;
            int refreshX = addX - gap - btnRefresh.Width;

            btnAdd.Location = new Point(addX, y);
            btnRefresh.Location = new Point(refreshX, y);

            btnAdd.BringToFront();
            btnRefresh.BringToFront();
        }

        private void SetupFilters()
        {
            _suppressFilterEvents = true;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Statuses");
            cmbStatus.Items.Add("Open");
            cmbStatus.Items.Add("In Progress");
            cmbStatus.Items.Add("Resolved");
            cmbStatus.Items.Add("Closed");
            cmbStatus.SelectedIndex = 0;

            cmbPriority.Items.Clear();
            cmbPriority.Items.Add("All Priorities");
            cmbPriority.Items.Add("Low");
            cmbPriority.Items.Add("Normal");
            cmbPriority.Items.Add("High");
            cmbPriority.Items.Add("Urgent");
            cmbPriority.SelectedIndex = 0;

            _suppressFilterEvents = false;
        }

        private async System.Threading.Tasks.Task LoadInquiriesAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var list = await _inquiryService.GetAllAsync(companyId);

            if (this.IsDisposed) return;

            _allInquiries = list;
            UpdateStatsSummary();
            ApplyFilters();
        }

        private void UpdateStatsSummary()
        {
            if (this.IsDisposed || lblStatsSummary.IsDisposed) return;

            if (_allInquiries.Count == 0)
            {
                lblStatsSummary.Text = "No inquiries yet";
                lblStatsSummary.ForeColor = Color.FromArgb(107, 114, 128);
                return;
            }

            var total = _allInquiries.Count;
            var open = _allInquiries.Count(i => i.Status == "Open");
            var inProgress = _allInquiries.Count(i => i.Status == "InProgress");
            var resolved = _allInquiries.Count(i => i.Status == "Resolved");
            var closed = _allInquiries.Count(i => i.Status == "Closed");
            var urgent = _allInquiries.Count(i => i.Priority == "Urgent" && i.Status != "Closed");

            lblStatsSummary.Text =
                $"Total: {total}   ·   Open: {open}   ·   In Progress: {inProgress}   ·   Resolved: {resolved}   ·   Closed: {closed}   ·   Urgent Active: {urgent}";
            lblStatsSummary.ForeColor = Color.FromArgb(31, 41, 55);
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var statusFilter = cmbStatus.SelectedItem?.ToString() ?? "All Statuses";
            var priorityFilter = cmbPriority.SelectedItem?.ToString() ?? "All Priorities";

            var filtered = _allInquiries.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(i =>
                    (i.Subject ?? "").ToLowerInvariant().Contains(search) ||
                    (i.Message ?? "").ToLowerInvariant().Contains(search));

            if (statusFilter != "All Statuses")
            {
                // Map UI text "In Progress" to API "InProgress"
                var apiStatus = statusFilter == "In Progress" ? "InProgress" : statusFilter;
                filtered = filtered.Where(i => i.Status == apiStatus);
            }

            if (priorityFilter != "All Priorities")
                filtered = filtered.Where(i => i.Priority == priorityFilter);

            var list = filtered.OrderByDescending(i => i.CreatedAt).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<CustomerInquiryDto> list)
        {
            if (this.IsDisposed || dgvInquiries.IsDisposed) return;
            if (dgvInquiries.Columns.Count == 0) return;

            dgvInquiries.Rows.Clear();

            foreach (var i in list)
            {
                var idx = dgvInquiries.Rows.Add(
                    i.CustomerInquiryId,
                    $"Customer {i.CustomerId}",
                    i.Subject,
                    i.Priority,
                    i.StatusDisplay,
                    i.CreatedAt.ToString("MMM d, hh:mm tt"),
                    i.HasResponse ? "✓" : "—",
                    "⋯ Actions"
                );

                var row = dgvInquiries.Rows[idx];
                row.Tag = i;

                var priorityCell = row.Cells[colPriority.Index];
                priorityCell.Style.ForeColor = GetPriorityColor(i.Priority);
                priorityCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                var statusCell = row.Cells[colStatus.Index];
                statusCell.Style.ForeColor = GetStatusColor(i.Status);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                var repliedCell = row.Cells[colReplied.Index];
                repliedCell.Style.ForeColor = i.HasResponse
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(156, 163, 175);
                repliedCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            lblCount.Text = $"{list.Count} of {_allInquiries.Count} inquiry(ies)";
        }

        private static Color GetPriorityColor(string priority) => priority switch
        {
            "Low" => Color.FromArgb(107, 114, 128),
            "Normal" => Color.FromArgb(59, 130, 246),
            "High" => Color.FromArgb(245, 158, 11),
            "Urgent" => Color.FromArgb(239, 68, 68),
            _ => Color.Gray
        };

        private static Color GetStatusColor(string status) => status switch
        {
            "Open" => Color.FromArgb(239, 68, 68),
            "InProgress" => Color.FromArgb(245, 158, 11),
            "Resolved" => Color.FromArgb(16, 185, 129),
            "Closed" => Color.FromArgb(107, 114, 128),
            _ => Color.Gray
        };

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new InquiryEditForm(_auth, _api, null);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadInquiriesAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadInquiriesAsync();
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvInquiries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var inq = dgvInquiries.Rows[e.RowIndex].Tag as CustomerInquiryDto;
            if (inq == null) return;

            var dialog = new InquiryEditForm(_auth, _api, inq);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadInquiriesAsync();
        }

        private async void dgvInquiries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var inq = dgvInquiries.Rows[e.RowIndex].Tag as CustomerInquiryDto;
            if (inq == null) return;

            var menu = new ContextMenuStrip();

            menu.Items.Add("💬  Respond", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new InquiryRespondForm(_auth, _api, inq);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadInquiriesAsync();
            });

            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new InquiryEditForm(_auth, _api, inq);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadInquiriesAsync();
            });

            // Status submenu
            var statusMenu = new ToolStripMenuItem("🛡  Change Status");
            statusMenu.DropDownItems.Add("🔵  Mark Open", null, async (s, args) =>
                await SetStatusAsync(inq, "Open"));
            statusMenu.DropDownItems.Add("🟡  Mark In Progress", null, async (s, args) =>
                await SetStatusAsync(inq, "InProgress"));
            statusMenu.DropDownItems.Add("🟢  Mark Resolved", null, async (s, args) =>
                await SetStatusAsync(inq, "Resolved"));
            statusMenu.DropDownItems.Add("⚫  Mark Closed", null, async (s, args) =>
                await SetStatusAsync(inq, "Closed"));
            menu.Items.Add(statusMenu);

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete inquiry #{inq.CustomerInquiryId}?\n\nSubject: {inq.Subject}",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _inquiryService.DeleteAsync(companyId, inq.CustomerInquiryId);

                if (this.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Inquiry deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadInquiriesAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete inquiry.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvInquiries.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvInquiries, cellRect.Left, cellRect.Bottom);
        }

        private async System.Threading.Tasks.Task SetStatusAsync(CustomerInquiryDto inq, string newStatus)
        {
            if (this.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var success = await _inquiryService.ChangeStatusAsync(companyId, inq.CustomerInquiryId, newStatus);

            if (this.IsDisposed) return;

            if (success)
            {
                inq.Status = newStatus;
                UpdateStatsSummary();
                ApplyFilters();
            }
            else
            {
                MessageBox.Show($"Failed to change status to {newStatus}.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}