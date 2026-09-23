using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class ReviewsTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerReviewService _reviewService;

        private List<CustomerReviewDto> _allReviews = new();
        private bool _suppressFilterEvents = false;

        public ReviewsTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _reviewService = new CustomerReviewService(api);
        }

        // ==================== LOAD ====================

        private async void ReviewsTab_Load(object sender, EventArgs e)
        {
            SetupStatusFilter();
            SetupRatingFilter();
            PositionTopBarButtons();
            await LoadReviewsAsync();
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

        // ==================== FILTERS SETUP ====================

        private void SetupStatusFilter()
        {
            _suppressFilterEvents = true;
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Statuses");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Approved");
            cmbStatus.Items.Add("Rejected");
            cmbStatus.SelectedIndex = 0;
            _suppressFilterEvents = false;
        }

        private void SetupRatingFilter()
        {
            _suppressFilterEvents = true;
            cmbRating.Items.Clear();
            cmbRating.Items.Add("All Ratings");
            cmbRating.Items.Add("★★★★★  Excellent (5)");
            cmbRating.Items.Add("★★★★☆  Good (4)");
            cmbRating.Items.Add("★★★☆☆  Average (3)");
            cmbRating.Items.Add("★★☆☆☆  Poor (2)");
            cmbRating.Items.Add("★☆☆☆☆  Very Bad (1)");
            cmbRating.SelectedIndex = 0;
            _suppressFilterEvents = false;
        }

        private async System.Threading.Tasks.Task LoadReviewsAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var list = await _reviewService.GetAllAsync(companyId);

            if (this.IsDisposed) return;

            _allReviews = list;
            UpdateStatsSummary();
            ApplyFilters();
        }

        private void UpdateStatsSummary()
        {
            if (this.IsDisposed || lblStatsSummary.IsDisposed) return;

            if (_allReviews.Count == 0)
            {
                lblStatsSummary.Text = "No reviews yet";
                lblStatsSummary.ForeColor = Color.FromArgb(107, 114, 128);
                return;
            }

            var total = _allReviews.Count;
            var approved = _allReviews.Count(r => r.ModerationStatus == "Approved");
            var pending = _allReviews.Count(r => r.ModerationStatus == "Pending");
            var rejected = _allReviews.Count(r => r.ModerationStatus == "Rejected");
            var avgRating = _allReviews.Average(r => r.Rating);

            lblStatsSummary.Text =
                $"Total: {total}   ·   Approved: {approved}   ·   Pending: {pending}   ·   Rejected: {rejected}   ·   Avg Rating: {avgRating:0.0} ★";
            lblStatsSummary.ForeColor = Color.FromArgb(31, 41, 55);
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var statusFilter = cmbStatus.SelectedItem?.ToString() ?? "All Statuses";
            var ratingFilter = cmbRating.SelectedIndex;

            var filtered = _allReviews.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(r =>
                    (r.Comment ?? "").ToLowerInvariant().Contains(search) ||
                    (r.Title ?? "").ToLowerInvariant().Contains(search));

            if (statusFilter != "All Statuses")
                filtered = filtered.Where(r => r.ModerationStatus == statusFilter);

            if (ratingFilter > 0)
            {
                int targetRating = 6 - ratingFilter;   // 1 → 5 stars, 2 → 4 stars, etc.
                filtered = filtered.Where(r => r.Rating == targetRating);
            }

            var list = filtered.OrderByDescending(r => r.CreatedAt).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void cmbRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<CustomerReviewDto> list)
        {
            if (this.IsDisposed || dgvReviews.IsDisposed) return;
            if (dgvReviews.Columns.Count == 0) return;

            dgvReviews.Rows.Clear();

            foreach (var r in list)
            {
                var idx = dgvReviews.Rows.Add(
                    r.CustomerReviewId,
                    $"Customer {r.CustomerId}",
                    r.ReviewType,
                    r.Stars,
                    r.DisplayTitle,
                    r.ModerationStatus,
                    r.IsVerified ? "✓" : "—",
                    string.IsNullOrWhiteSpace(r.AdminReply) ? "—" : "✓",
                    "⋯ Actions"
                );

                var row = dgvReviews.Rows[idx];
                row.Tag = r;

                // Color-code rating
                var ratingCell = row.Cells[colRating.Index];
                ratingCell.Style.ForeColor = GetRatingColor(r.Rating);
                ratingCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Color-code status
                var statusCell = row.Cells[colStatus.Index];
                statusCell.Style.ForeColor = GetStatusColor(r.ModerationStatus);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Color verified
                var verifiedCell = row.Cells[colVerified.Index];
                verifiedCell.Style.ForeColor = r.IsVerified
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(156, 163, 175);
            }

            lblCount.Text = $"{list.Count} of {_allReviews.Count} review(s)";
        }

        private static Color GetRatingColor(int rating) => rating switch
        {
            5 => Color.FromArgb(16, 185, 129),
            4 => Color.FromArgb(59, 130, 246),
            3 => Color.FromArgb(245, 158, 11),
            2 => Color.FromArgb(239, 68, 68),
            1 => Color.FromArgb(220, 38, 38),
            _ => Color.Gray
        };

        private static Color GetStatusColor(string status) => status switch
        {
            "Approved" => Color.FromArgb(16, 185, 129),
            "Pending" => Color.FromArgb(245, 158, 11),
            "Rejected" => Color.FromArgb(239, 68, 68),
            _ => Color.Gray
        };

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new ReviewEditForm(_auth, _api, null);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadReviewsAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadReviewsAsync();
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvReviews_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var review = dgvReviews.Rows[e.RowIndex].Tag as CustomerReviewDto;
            if (review == null) return;

            var dialog = new ReviewEditForm(_auth, _api, review);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadReviewsAsync();
        }

        private async void dgvReviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var review = dgvReviews.Rows[e.RowIndex].Tag as CustomerReviewDto;
            if (review == null) return;

            var menu = new ContextMenuStrip();

            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new ReviewEditForm(_auth, _api, review);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadReviewsAsync();
            });

            // Moderate submenu
            var moderateMenu = new ToolStripMenuItem("🛡  Moderate");
            moderateMenu.DropDownItems.Add("✅  Approve", null, async (s, args) =>
                await SetModerationStatus(review, "Approved"));
            moderateMenu.DropDownItems.Add("⏸  Set Pending", null, async (s, args) =>
                await SetModerationStatus(review, "Pending"));
            moderateMenu.DropDownItems.Add("❌  Reject", null, async (s, args) =>
                await SetModerationStatus(review, "Rejected"));
            menu.Items.Add(moderateMenu);

            // Reply
            menu.Items.Add("💬  Reply", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new ReviewReplyForm(_auth, _api, review);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadReviewsAsync();
            });

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete review #{review.CustomerReviewId}?\n\nRating: {review.Stars}\nTitle: {review.DisplayTitle}",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _reviewService.DeleteAsync(companyId, review.CustomerReviewId);

                if (this.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Review deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadReviewsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete review.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvReviews.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvReviews, cellRect.Left, cellRect.Bottom);
        }

        private async System.Threading.Tasks.Task SetModerationStatus(CustomerReviewDto review, string newStatus)
        {
            if (this.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var success = await _reviewService.ModerateAsync(companyId, review.CustomerReviewId, newStatus);

            if (this.IsDisposed) return;

            if (success)
            {
                review.ModerationStatus = newStatus;
                UpdateStatsSummary();
                ApplyFilters();
            }
            else
            {
                MessageBox.Show($"Failed to set status to {newStatus}.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== PUBLIC REFRESH ====================

        public async System.Threading.Tasks.Task RefreshAsync()
        {
            await LoadReviewsAsync();
        }
    }
}