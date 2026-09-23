using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class FeedbackTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerFeedbackService _feedbackService;

        private List<CustomerFeedbackDto> _allFeedback = new();
        private bool _suppressFilterEvents = false;

        public FeedbackTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _feedbackService = new CustomerFeedbackService(api);
        }

        // ==================== LOAD ====================

        private async void FeedbackTab_Load(object sender, EventArgs e)
        {
            SetupRatingFilter();
            PositionTopBarButtons();
            await LoadFeedbackAsync();
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

        // ==================== RATING FILTER ====================

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

        private async System.Threading.Tasks.Task LoadFeedbackAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var list = await _feedbackService.GetAllAsync(companyId);

            if (this.IsDisposed) return;

            _allFeedback = list;
            ApplyFilters();
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var ratingFilter = cmbRating.SelectedIndex;

            var filtered = _allFeedback.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(f =>
                    (f.Comments ?? "").ToLowerInvariant().Contains(search));

            // Rating filter: 0 = All, 1 = 5 stars, 2 = 4 stars, ..., 5 = 1 star
            if (ratingFilter > 0)
            {
                int targetRating = 6 - ratingFilter;   // 1 → 5, 2 → 4, ..., 5 → 1
                filtered = filtered.Where(f => f.Rating == targetRating);
            }

            var list = filtered.OrderByDescending(f => f.CreatedAt).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<CustomerFeedbackDto> list)
        {
            if (this.IsDisposed || dgvFeedback.IsDisposed) return;
            if (dgvFeedback.Columns.Count == 0) return;

            dgvFeedback.Rows.Clear();

            foreach (var f in list)
            {
                var commentsPreview = (f.Comments ?? "").Trim();
                if (commentsPreview.Length > 80)
                    commentsPreview = commentsPreview.Substring(0, 80) + "…";

                var idx = dgvFeedback.Rows.Add(
                    f.FeedbackId,
                    $"Customer {f.CustomerId}",
                    f.Stars + "  " + f.RatingLabel,
                    commentsPreview,
                    f.CreatedAt.ToString("MMM d, yyyy"),
                    "⋯ Actions"
                );

                var row = dgvFeedback.Rows[idx];
                row.Tag = f;

                var ratingCell = row.Cells[colRating.Index];
                ratingCell.Style.ForeColor = GetRatingColor(f.Rating);
                ratingCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            lblCount.Text = $"{list.Count} of {_allFeedback.Count} feedback entr{(list.Count == 1 ? "y" : "ies")}";
        }

        private static Color GetRatingColor(int rating) => rating switch
        {
            5 => Color.FromArgb(16, 185, 129),   // green
            4 => Color.FromArgb(59, 130, 246),   // blue
            3 => Color.FromArgb(245, 158, 11),   // amber
            2 => Color.FromArgb(239, 68, 68),    // red
            1 => Color.FromArgb(220, 38, 38),    // dark red
            _ => Color.Gray
        };

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new FeedbackEditForm(_auth, _api, null);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadFeedbackAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadFeedbackAsync();
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvFeedback_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var feedback = dgvFeedback.Rows[e.RowIndex].Tag as CustomerFeedbackDto;
            if (feedback == null) return;

            var dialog = new FeedbackEditForm(_auth, _api, feedback);
            if (dialog.ShowDialog() == DialogResult.OK)
                await LoadFeedbackAsync();
        }

        private async void dgvFeedback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var feedback = dgvFeedback.Rows[e.RowIndex].Tag as CustomerFeedbackDto;
            if (feedback == null) return;

            var menu = new ContextMenuStrip();
            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new FeedbackEditForm(_auth, _api, feedback);
                if (dialog.ShowDialog() == DialogResult.OK)
                    await LoadFeedbackAsync();
            });
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete this feedback entry?\n\nRating: {feedback.Stars}\nComments: {(feedback.Comments ?? "(none)")}",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _feedbackService.DeleteAsync(companyId, feedback.FeedbackId);

                if (this.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Feedback deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadFeedbackAsync();
                }
                else
                {
                    MessageBox.Show("Failed to delete feedback.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvFeedback.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvFeedback, cellRect.Left, cellRect.Bottom);
        }
    }
}