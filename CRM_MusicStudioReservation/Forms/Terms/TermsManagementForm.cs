using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Terms
{
    public partial class TermsManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly TermsService _termsService;

        private List<TermsListItemDto> _allTerms = new();
        private bool _suppressFilterEvents = false;

        public TermsManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _termsService = new TermsService(api);
        }

        private bool IsAdmin()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "";
            return role == "admin" || role == "superadmin";
        }

        private async void TermsManagementForm_Load(object sender, EventArgs e)
        {
            EnsureGridColumns();
            SetupFilters();
            await LoadTermsAsync();
        }

        private void SetupFilters()
        {
            _suppressFilterEvents = true;

            cmbType.Items.Clear();
            cmbType.Items.Add("All Types");
            cmbType.Items.Add("GeneralTerms");
            cmbType.Items.Add("BookingPolicy");
            cmbType.Items.Add("CancellationPolicy");
            cmbType.Items.Add("PrivacyPolicy");
            cmbType.Items.Add("StudioUsageRules");
            cmbType.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Statuses");
            cmbStatus.Items.Add("Draft");
            cmbStatus.Items.Add("PendingApproval");
            cmbStatus.Items.Add("Published");
            cmbStatus.Items.Add("Archived");
            cmbStatus.SelectedIndex = 0;

            _suppressFilterEvents = false;
        }

        private void EnsureGridColumns()
        {
            if (dgvTerms == null || dgvTerms.IsDisposed) return;
            if (dgvTerms.Columns.Count > 0) return;

            dgvTerms.Columns.Clear();

            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 35 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Code", FillWeight = 85 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colType", HeaderText = "Policy Type", FillWeight = 110 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTitle", HeaderText = "Title", FillWeight = 180 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVersion", HeaderText = "Version", FillWeight = 60 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", FillWeight = 90 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAuthor", HeaderText = "Author", FillWeight = 90 });
            dgvTerms.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUpdated", HeaderText = "Updated At", FillWeight = 100 });

            var actionsCol = new DataGridViewButtonColumn
            {
                Name = "colActions",
                HeaderText = "Actions",
                FillWeight = 80,
                FlatStyle = FlatStyle.Flat
            };
            actionsCol.DefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            actionsCol.DefaultCellStyle.ForeColor = Color.FromArgb(139, 92, 246);
            actionsCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 233, 254);
            actionsCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTerms.Columns.Add(actionsCol);
        }

        // ==================== LOAD DATA ====================

        private async Task LoadTermsAsync()
        {
            try
            {
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                _allTerms = await _termsService.GetAllAsync(companyId);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load Terms & Conditions: {ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== FILTERING & RENDERING ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed || dgvTerms == null || dgvTerms.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var selectedType = cmbType.SelectedItem?.ToString() ?? "All Types";
            var selectedStatus = cmbStatus.SelectedItem?.ToString() ?? "All Statuses";

            var filtered = _allTerms.AsEnumerable();

            if (selectedType != "All Types")
                filtered = filtered.Where(t => t.TandCType.Equals(selectedType, StringComparison.OrdinalIgnoreCase));

            if (selectedStatus != "All Statuses")
                filtered = filtered.Where(t => t.Status.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(search))
            {
                filtered = filtered.Where(t => 
                    (t.TandCCode ?? "").ToLowerInvariant().Contains(search) ||
                    (t.Title ?? "").ToLowerInvariant().Contains(search) ||
                    (t.TandCType ?? "").ToLowerInvariant().Contains(search));
            }

            var list = filtered.OrderByDescending(t => t.UpdatedAt).ToList();
            RenderRows(list);

            if (!lblCount.IsDisposed)
                lblCount.Text = $"{list.Count} of {_allTerms.Count} policy record(s)";
        }

        private void RenderRows(List<TermsListItemDto> items)
        {
            dgvTerms.Rows.Clear();

            foreach (var item in items)
            {
                var idx = dgvTerms.Rows.Add(
                    item.TandCId,
                    item.TandCCode,
                    FormatType(item.TandCType),
                    item.Title,
                    $"v{item.Version}",
                    item.Status,
                    item.AuthorName ?? "System",
                    item.UpdatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                    "Actions ▾"
                );

                var statusCell = dgvTerms.Rows[idx].Cells["colStatus"];
                statusCell.Style.ForeColor = item.Status switch
                {
                    "Published" => AppTheme.Success,
                    "PendingApproval" => AppTheme.Info,
                    "Draft" => AppTheme.Warning,
                    "Archived" => AppTheme.TextMuted,
                    _ => AppTheme.TextSecondary
                };
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                dgvTerms.Rows[idx].Tag = item;
            }
        }

        private static string FormatType(string type) => type switch
        {
            "GeneralTerms" => "General Terms",
            "BookingPolicy" => "Booking Policy",
            "CancellationPolicy" => "Cancellation Policy",
            "PrivacyPolicy" => "Privacy Policy",
            "StudioUsageRules" => "Studio Usage Rules",
            _ => type
        };

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e) { if (!_suppressFilterEvents) ApplyFilters(); }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { if (!_suppressFilterEvents) ApplyFilters(); }

        // ==================== ACTIONS (MIRRORS MANAGE BOOKINGS) ====================

        private async void dgvTerms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != dgvTerms.Columns["colActions"].Index) return;

            if (dgvTerms.Rows[e.RowIndex].Tag is not TermsListItemDto item) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var isAdmin = IsAdmin();
            var isDraft = item.Status.Equals("Draft", StringComparison.OrdinalIgnoreCase);
            var isPending = item.Status.Equals("PendingApproval", StringComparison.OrdinalIgnoreCase);
            var isPublished = item.Status.Equals("Published", StringComparison.OrdinalIgnoreCase);
            var isArchived = item.Status.Equals("Archived", StringComparison.OrdinalIgnoreCase);

            var menu = new ContextMenuStrip();

            // 1. View (Always available)
            menu.Items.Add("👁  View Content", null, async (s, args) => await OpenViewAsync(item.TandCId));

            // 2. Edit (Draft only)
            if (isDraft)
            {
                menu.Items.Add("✏  Edit Draft", null, async (s, args) => await OpenEditAsync(item.TandCId));
            }

            // 3. Submit for Approval (Draft only)
            if (isDraft)
            {
                menu.Items.Add("➡️  Submit for Approval", null, async (s, args) =>
                {
                    var confirm = MessageBox.Show(
                        $"Submit '{item.TandCCode}: {item.Title}' for Admin approval?",
                        "Confirm Submission", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        await _termsService.SubmitForApprovalAsync(companyId, item.TandCId);
                        MessageBox.Show("Submitted for approval successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTermsAsync();
                    }
                });
            }

            // 4. Approve & Publish (PendingApproval only, Admin only)
            if (isPending && isAdmin)
            {
                menu.Items.Add("✓  Approve & Publish", null, async (s, args) =>
                {
                    var confirm = MessageBox.Show(
                        $"Approve and publish '{item.TandCCode}'? This will automatically archive the previous published version of this policy.",
                        "Confirm Publication", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        await _termsService.PublishAsync(companyId, item.TandCId);
                        MessageBox.Show("Policy published successfully.", "Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTermsAsync();
                    }
                });
            }

            // 5. Reject (PendingApproval only, Admin only)
            if (isPending && isAdmin)
            {
                menu.Items.Add("✕  Reject to Draft", null, async (s, args) =>
                {
                    var reason = Microsoft.VisualBasic.Interaction.InputBox(
                        "Please provide the reason for rejection:",
                        "Reject Terms & Conditions",
                        "Changes required before approval.");

                    if (!string.IsNullOrWhiteSpace(reason))
                    {
                        await _termsService.RejectAsync(companyId, item.TandCId, reason);
                        MessageBox.Show("Policy rejected and returned to Draft.", "Returned to Draft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTermsAsync();
                    }
                });
            }

            // 6. Archive (Published or Draft)
            if (!isArchived && isAdmin)
            {
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add("📦  Archive Version", null, async (s, args) =>
                {
                    var confirm = MessageBox.Show(
                        $"Are you sure you want to archive '{item.TandCCode}'? It will no longer be active.",
                        "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        await _termsService.ArchiveAsync(companyId, item.TandCId);
                        MessageBox.Show("Policy archived.", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTermsAsync();
                    }
                });
            }

            // 7. View Acknowledgments
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("📋  View Client Acceptances", null, (s, args) =>
            {
                using var ackDialog = new TermsAcknowledgmentsForm(_termsService, companyId, item.TandCId, item.Title);
                ackDialog.ShowDialog(this);
            });

            var cellRect = dgvTerms.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvTerms, cellRect.Left, cellRect.Bottom);
        }

        private async Task OpenViewAsync(int id)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var details = await _termsService.GetByIdAsync(companyId, id);
            if (details != null)
            {
                using var dialog = new TermsViewForm(details);
                dialog.ShowDialog(this);
            }
        }

        private async Task OpenEditAsync(int id)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var details = await _termsService.GetByIdAsync(companyId, id);
            if (details != null)
            {
                using var dialog = new TermsEditForm(_termsService, companyId, details);
                dialog.ShowDialog(this);
                if (dialog.SavedSuccessfully)
                    await LoadTermsAsync();
            }
        }

        private async void btnNewTerms_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new TermsEditForm(_termsService, companyId, null);
            dialog.ShowDialog(this);
            if (dialog.SavedSuccessfully)
                await LoadTermsAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadTermsAsync();
        }

        private void btnViewAllAcks_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new TermsAcknowledgmentsForm(_termsService, companyId, null, null);
            dialog.ShowDialog(this);
        }
    }
}
