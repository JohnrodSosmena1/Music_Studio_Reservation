using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CRM_MusicStudioReservation.Forms.Studios
{
    public partial class StudioManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly StudioService _studioService;

        private List<StudioDto> _allStudios = new();

        public StudioManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _studioService = new StudioService(api);
        }

        private async void StudioManagementForm_Load(object sender, EventArgs e)
        {
            await LoadStudiosAsync();
        }

        // ==================== DATA LOADING ====================

        private async Task LoadStudiosAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            dgvStudios.Rows.Clear();
            _allStudios = await _studioService.GetAllAsync(companyId);

            RenderRows(_allStudios);
            lblCount.Text = $"{_allStudios.Count} studio(s)";
        }

        private void RenderRows(List<StudioDto> studios)
        {
            dgvStudios.Rows.Clear();

            foreach (var s in studios)
            {
                var statusText = s.IsActive ? "Active" : "Inactive";
                var typeName = GetTypeName(s.StudioType);

                var idx = dgvStudios.Rows.Add(
                    s.StudioId,
                    s.StudioCode,
                    s.StudioName,
                    typeName,
                    $"₱{s.HourlyRate:N2}",
                    s.Capacity,
                    statusText,
                    "⋯ Actions"
                );

                var statusCell = dgvStudios.Rows[idx].Cells[colStatus.Index];
                statusCell.Style.ForeColor = s.IsActive
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                dgvStudios.Rows[idx].Tag = s.StudioId;
            }
        }

        private static string GetTypeName(int typeId) => typeId switch
        {
            1 => "Rehearsal",
            2 => "Recording",
            3 => "Vocal",
            4 => "Mixing",
            5 => "Mastering",
            _ => "Unknown"
        };

        // ==================== SEARCH ====================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(search))
            {
                RenderRows(_allStudios);
                lblCount.Text = $"{_allStudios.Count} studio(s)";
                return;
            }

            var filtered = _allStudios
                .Where(s => s.StudioName.ToLowerInvariant().Contains(search)
                         || s.StudioCode.ToLowerInvariant().Contains(search))
                .ToList();

            RenderRows(filtered);
            lblCount.Text = $"{filtered.Count} of {_allStudios.Count} studio(s)";
        }

        // ==================== BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            using var dialog = new StudioEditForm(_studioService, companyId, null);
            dialog.ShowDialog(this.FindForm() ?? this);

            if (dialog.SavedSuccessfully)
                await LoadStudiosAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefresh.Enabled = false;
                btnRefresh.Text = "↻";
                await LoadStudiosAsync();
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        // ==================== ROW ACTIONS & INTERACTIONS ====================

        private void dgvStudios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var studioId = (int)(dgvStudios.Rows[e.RowIndex].Tag ?? 0);
            var studio = _allStudios.FirstOrDefault(s => s.StudioId == studioId);
            if (studio == null) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var detailsDialog = new StudioDetailsForm(_studioService, companyId, studio);
            detailsDialog.ShowDialog(this.FindForm() ?? this);
        }

        private void dgvStudios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colActions.Index) return;

            var row = dgvStudios.Rows[e.RowIndex];
            var studioId = (int)(row.Tag ?? 0);

            var studio = _allStudios.FirstOrDefault(s => s.StudioId == studioId);
            if (studio == null) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            var menu = new ContextMenuStrip();

            // 1. View Equipment & Details
            var itemView = new ToolStripMenuItem("👁  View Equipment & Details");
            itemView.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            itemView.Click += (s, args) =>
            {
                using var details = new StudioDetailsForm(_studioService, companyId, studio);
                details.ShowDialog(this.FindForm() ?? this);
            };
            menu.Items.Add(itemView);

            // 2. Edit Studio
            var itemEdit = new ToolStripMenuItem("✏  Edit Studio");
            itemEdit.Click += async (s, args) =>
            {
                using var dialog = new StudioEditForm(_studioService, companyId, studio);
                dialog.ShowDialog(this.FindForm() ?? this);
                if (dialog.SavedSuccessfully)
                    await LoadStudiosAsync();
            };
            menu.Items.Add(itemEdit);

            // 3. Toggle Enable / Disable
            var toggleLabel = studio.IsActive ? "⊘  Disable Studio" : "✓  Enable Studio";
            var itemToggle = new ToolStripMenuItem(toggleLabel);
            itemToggle.Click += async (s, args) =>
            {
                var newStatus = !studio.IsActive;
                var actionVerb = newStatus ? "enable" : "disable";

                var confirm = MessageBox.Show(
                    $"Are you sure you want to {actionVerb} '{studio.StudioName}' ({studio.StudioCode})?",
                    $"Confirm {(newStatus ? "Enable" : "Disable")}",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var updateReq = new StudioUpdateRequest { IsActive = newStatus };
                var result = await _studioService.UpdateAsync(companyId, studioId, updateReq);

                if (result != null)
                {
                    MessageBox.Show($"Studio {actionVerb}d successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadStudiosAsync();
                }
                else
                {
                    MessageBox.Show($"Failed to {actionVerb} studio.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            menu.Items.Add(itemToggle);

            menu.Items.Add(new ToolStripSeparator());

            // 4. Delete / Archive
            var itemDelete = new ToolStripMenuItem("🗑  Delete Studio");
            itemDelete.ForeColor = Color.FromArgb(239, 68, 68);
            itemDelete.Click += async (s, args) =>
            {
                var confirm = MessageBox.Show(
                    $"Delete studio '{studio.StudioName}' ({studio.StudioCode})?\n\nIf the studio has past bookings, it will be deactivated instead of deleted.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var success = await _studioService.DeleteAsync(companyId, studioId);
                if (success)
                {
                    MessageBox.Show("Studio deleted/archived successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadStudiosAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete studio. It may be referenced by active bookings.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            menu.Items.Add(itemDelete);

            var cellRect = dgvStudios.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvStudios, cellRect.Left, cellRect.Bottom);
        }
    }
}