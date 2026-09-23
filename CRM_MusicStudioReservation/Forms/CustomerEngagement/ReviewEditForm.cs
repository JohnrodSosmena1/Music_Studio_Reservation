using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class ReviewEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerReviewService _reviewService;
        private readonly CustomerReviewDto? _existing;

        public ReviewEditForm(AuthService auth, ApiClient api, CustomerReviewDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _reviewService = new CustomerReviewService(api);
            _existing = existing;
        }

        private void ReviewEditForm_Load(object sender, EventArgs e)
        {
            // Populate type dropdown
            cmbType.Items.Clear();
            cmbType.Items.Add("General");
            cmbType.Items.Add("Studio");
            cmbType.Items.Add("Service");
            cmbType.SelectedIndex = 0;

            // Populate rating dropdown
            cmbRating.Items.Clear();
            cmbRating.Items.Add("★★★★★  Excellent (5)");
            cmbRating.Items.Add("★★★★☆  Good (4)");
            cmbRating.Items.Add("★★★☆☆  Average (3)");
            cmbRating.Items.Add("★★☆☆☆  Poor (2)");
            cmbRating.Items.Add("★☆☆☆☆  Very Bad (1)");
            cmbRating.SelectedIndex = 0;

            if (_existing == null)
            {
                this.Text = "Record Review";
                lblHeader.Text = "Record Review";
                lblSubheader.Text = "Enter customer review details";

                numCustomerId.Value = 1;
            }
            else
            {
                this.Text = "Edit Review";
                lblHeader.Text = "Edit Review";
                lblSubheader.Text = $"Editing review #{_existing.CustomerReviewId} from {_existing.CreatedAt:MMM d, yyyy}";

                // Customer locked in edit mode
                numCustomerId.Value = Math.Max(1, Math.Min(numCustomerId.Maximum, _existing.CustomerId));
                numCustomerId.Enabled = false;
                numCustomerId.BackColor = Color.FromArgb(243, 244, 246);

                // Type
                var typeIdx = cmbType.Items.IndexOf(_existing.ReviewType);
                if (typeIdx >= 0) cmbType.SelectedIndex = typeIdx;

                // Studio
                if (_existing.StudioId.HasValue)
                    txtStudioId.Text = _existing.StudioId.Value.ToString();

                // Rating (5 → index 0, 4 → index 1, ..., 1 → index 4)
                var ratingIdx = 5 - _existing.Rating;
                if (ratingIdx >= 0 && ratingIdx < cmbRating.Items.Count)
                    cmbRating.SelectedIndex = ratingIdx;

                txtTitle.Text = _existing.Title ?? "";
                txtComment.Text = _existing.Comment ?? "";
            }

            UpdateStarPreview();
        }

        // ==================== STAR PREVIEW ====================

        private void cmbRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStarPreview();
        }

        private void UpdateStarPreview()
        {
            int rating = 5 - cmbRating.SelectedIndex;
            if (rating < 1 || rating > 5) rating = 5;

            lblStarPreview.Text = new string('★', rating) + new string('☆', 5 - rating);
        }

        // ==================== ACTIONS ====================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideError();

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                ShowError("Comment is required.");
                return;
            }

            var rating = 5 - cmbRating.SelectedIndex;
            if (rating < 1 || rating > 5)
            {
                ShowError("Please select a rating.");
                return;
            }

            int? studioId = null;
            if (!string.IsNullOrWhiteSpace(txtStudioId.Text))
            {
                if (!int.TryParse(txtStudioId.Text.Trim(), out var parsed))
                {
                    ShowError("Studio ID must be a number.");
                    return;
                }
                studioId = parsed;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var customerId = (int)numCustomerId.Value;
            var reviewType = cmbType.SelectedItem?.ToString() ?? "General";
            var title = string.IsNullOrWhiteSpace(txtTitle.Text) ? null : txtTitle.Text.Trim();
            var comment = txtComment.Text.Trim();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new CustomerReviewCreateRequest
                    {
                        CustomerId = customerId,
                        StudioId = studioId,
                        BookingId = null,
                        ReviewType = reviewType,
                        Title = title,
                        Rating = rating,
                        Comment = comment
                    };

                    var created = await _reviewService.CreateAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create review. Please verify the customer and studio IDs exist.");
                        return;
                    }
                }
                else
                {
                    var request = new CustomerReviewUpdateRequest
                    {
                        Title = title,
                        Rating = rating,
                        Comment = comment,
                        ReviewType = reviewType
                    };

                    var updated = await _reviewService.UpdateAsync(companyId, _existing.CustomerReviewId, request);
                    if (updated == null)
                    {
                        ShowError("Failed to update review. Please try again.");
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private void HideError()
        {
            lblError.Visible = false;
            lblError.Text = "";
        }
    }
}