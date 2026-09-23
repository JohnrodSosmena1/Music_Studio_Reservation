using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class MembershipPlansTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly MembershipPlanService _planService;

        private List<MembershipPlanDto> _allPlans = new();

        public MembershipPlansTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _planService = new MembershipPlanService(api);
        }

        // ==================== LOAD ====================

        private async void MembershipPlansTab_Load(object sender, EventArgs e)
        {
            PositionTopBarButtons();
            await LoadPlansAsync();
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

        private async System.Threading.Tasks.Task LoadPlansAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var list = await _planService.GetAllAsync(companyId);

            if (this.IsDisposed) return;

            _allPlans = list;
            ApplyFilters();
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();

            var filtered = _allPlans.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(p =>
                    (p.PlanName ?? "").ToLowerInvariant().Contains(search));

            var list = filtered.OrderByDescending(p => p.MonthlyFee).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        // ==================== RENDER ====================

        private void RenderRows(List<MembershipPlanDto> list)
        {
            if (this.IsDisposed || dgvPlans.IsDisposed) return;
            if (dgvPlans.Columns.Count == 0) return;

            dgvPlans.Rows.Clear();

            foreach (var p in list)
            {
                var idx = dgvPlans.Rows.Add(
                    p.MembershipPlanId,
                    p.PlanName,
                    p.FeeDisplay,
                    p.PointsDisplay,
                    p.BenefitsPreview,
                    p.Status,
                    "⋯ Actions"
                );

                var row = dgvPlans.Rows[idx];
                row.Tag = p;

                var statusCell = row.Cells[colStatus.Index];
                statusCell.Style.ForeColor = p.IsActive
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                var feeCell = row.Cells[colFee.Index];
                feeCell.Style.ForeColor = Color.FromArgb(139, 92, 246);
                feeCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            lblCount.Text = $"{list.Count} of {_allPlans.Count} plan(s)";
        }

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new MembershipPlanEditForm(_auth, _api, null);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadPlansAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadPlansAsync();
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvPlans_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var plan = dgvPlans.Rows[e.RowIndex].Tag as MembershipPlanDto;
            if (plan == null) return;

            var dialog = new MembershipPlanEditForm(_auth, _api, plan);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadPlansAsync();
        }

        private async void dgvPlans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var plan = dgvPlans.Rows[e.RowIndex].Tag as MembershipPlanDto;
            if (plan == null) return;

            var menu = new ContextMenuStrip();
            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new MembershipPlanEditForm(_auth, _api, plan);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadPlansAsync();
            });
            menu.Items.Add(plan.IsActive ? "🚫  Deactivate" : "✅  Activate", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var req = new MembershipPlanUpdateRequest { IsActive = !plan.IsActive };
                var result = await _planService.UpdateAsync(companyId, plan.MembershipPlanId, req);
                if (this.IsDisposed) return;
                if (result != null) await LoadPlansAsync();
                else MessageBox.Show("Failed to update status.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete plan '{plan.PlanName}'?\n\nThis will deactivate the plan.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _planService.DeleteAsync(companyId, plan.MembershipPlanId);

                if (this.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Plan deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPlansAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete plan.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvPlans.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvPlans, cellRect.Left, cellRect.Bottom);
        }
    }
}