using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class PromotionsTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly PromotionService _promotionService;

        private List<PromotionDto> _allPromotions = new();
        private bool _suppressFilterEvents = false;

        public PromotionsTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _promotionService = new PromotionService(api);
        }

        // ==================== LOAD ====================

        private async void PromotionsTab_Load(object sender, EventArgs e)
        {
            SetupStatusFilter();
            PositionTopBarButtons();       // 👈 position buttons first
            await LoadPromotionsAsync();
        }

        // ==================== DYNAMIC BUTTON POSITIONING ====================

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionTopBarButtons();
        }

        private void PositionTopBarButtons()
        {
            if (this.IsDisposed || pnlTopBar == null || pnlTopBar.IsDisposed) return;
            if (btnAdd == null || btnRefresh == null || btnValidate == null) return;

            int rightMargin = 20;
            int gap = 10;
            int y = 25;

            int addWidth = btnAdd.Width;
            int refreshWidth = btnRefresh.Width;
            int validateWidth = btnValidate.Width;

            int panelWidth = pnlTopBar.Width;

            // Place from right to left
            int addX = panelWidth - rightMargin - addWidth;
            int refreshX = addX - gap - refreshWidth;
            int validateX = refreshX - gap - validateWidth;

            // If panel is too narrow, hide validate button
            if (validateX < 400)
            {
                btnValidate.Visible = false;
                validateX = refreshX;
            }
            else
            {
                btnValidate.Visible = true;
            }

            btnAdd.Location = new Point(addX, y);
            btnRefresh.Location = new Point(refreshX, y);
            btnValidate.Location = new Point(validateX, y);

            // Ensure they render on top of the panel
            btnAdd.BringToFront();
            btnRefresh.BringToFront();
            btnValidate.BringToFront();
        }

        // ==================== STATUS FILTER ====================

        private void SetupStatusFilter()
        {
            _suppressFilterEvents = true;
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Statuses");
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Upcoming");
            cmbStatus.Items.Add("Expired");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;
            _suppressFilterEvents = false;
        }

        private async System.Threading.Tasks.Task LoadPromotionsAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var list = await _promotionService.GetAllAsync(companyId);

            if (this.IsDisposed) return;

            _allPromotions = list;
            ApplyFilters();
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var statusFilter = cmbStatus.SelectedItem?.ToString() ?? "All Statuses";
            var activeOnly = chkActiveOnly.Checked;

            var filtered = _allPromotions.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(p =>
                    (p.PromotionCode ?? "").ToLowerInvariant().Contains(search) ||
                    (p.PromotionName ?? "").ToLowerInvariant().Contains(search));

            if (statusFilter != "All Statuses")
                filtered = filtered.Where(p => p.Status == statusFilter);

            if (activeOnly)
                filtered = filtered.Where(p => p.IsActive);

            var list = filtered.OrderByDescending(p => p.StartDate).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void chkActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<PromotionDto> list)
        {
            if (this.IsDisposed || dgvPromotions.IsDisposed) return;
            if (dgvPromotions.Columns.Count == 0) return;

            dgvPromotions.Rows.Clear();

            foreach (var p in list)
            {
                var idx = dgvPromotions.Rows.Add(
                    p.PromotionId,
                    p.PromotionCode,
                    p.PromotionName,
                    $"{p.DiscountPercent:0.##}%",
                    p.StartDate.ToString("MMM d, yyyy"),
                    p.EndDate.ToString("MMM d, yyyy"),
                    p.Status,
                    "⋯ Actions"
                );

                var row = dgvPromotions.Rows[idx];
                row.Tag = p;

                var statusCell = row.Cells[colStatus.Index];
                statusCell.Style.ForeColor = GetStatusColor(p.Status);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            lblCount.Text = $"{list.Count} of {_allPromotions.Count} promotion(s)";
        }

        private static Color GetStatusColor(string status) => status switch
        {
            "Active" => Color.FromArgb(16, 185, 129),
            "Upcoming" => Color.FromArgb(59, 130, 246),
            "Expired" => Color.FromArgb(107, 114, 128),
            "Inactive" => Color.FromArgb(239, 68, 68),
            _ => Color.Gray
        };

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new PromotionEditForm(_auth, _api, null);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadPromotionsAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadPromotionsAsync();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            MessageBox.Show("Code validation dialog coming soon.", "Coming Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvPromotions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var promo = dgvPromotions.Rows[e.RowIndex].Tag as PromotionDto;
            if (promo == null) return;

            var dialog = new PromotionEditForm(_auth, _api, promo);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadPromotionsAsync();
        }

        private async void dgvPromotions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var promo = dgvPromotions.Rows[e.RowIndex].Tag as PromotionDto;
            if (promo == null) return;

            var menu = new ContextMenuStrip();
            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new PromotionEditForm(_auth, _api, promo);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadPromotionsAsync();
            });
            menu.Items.Add(promo.IsActive ? "🚫  Deactivate" : "✅  Activate", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var req = new PromotionUpdateRequest { IsActive = !promo.IsActive };
                var result = await _promotionService.UpdateAsync(companyId, promo.PromotionId, req);
                if (this.IsDisposed) return;
                if (result != null) await LoadPromotionsAsync();
                else MessageBox.Show("Failed to update status.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete promotion '{promo.PromotionCode}'?\n\nThis will deactivate the promotion.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _promotionService.DeleteAsync(companyId, promo.PromotionId);

                if (this.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Promotion deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPromotionsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete promotion.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvPromotions.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvPromotions, cellRect.Left, cellRect.Bottom);
        }

        // ==================== PUBLIC REFRESH ====================

        public async System.Threading.Tasks.Task RefreshAsync()
        {
            await LoadPromotionsAsync();
        }
    }
}