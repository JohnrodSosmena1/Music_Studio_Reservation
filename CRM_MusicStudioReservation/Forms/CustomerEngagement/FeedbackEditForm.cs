using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class FeedbackEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerFeedbackService _feedbackService;
        private readonly CustomerFeedbackDto? _existing;

        public FeedbackEditForm(AuthService auth, ApiClient api, CustomerFeedbackDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _feedbackService = new CustomerFeedbackService(api);
            _existing = existing;
        }

        private void FeedbackEditForm_Load(object sender, EventArgs e)
        {
            // Populate rating dropdown
            cmbRating.Items.Clear();
            cmbRating.Items.Add("★★★★★  Excellent (5)");
            cmbRating.Items.Add("★★★★☆  Good (4)");
            cmbRating.Items.Add("★★★☆☆  Average (3)");
            cmbRating.Items.Add("★★☆☆☆  Poor (2)");
            cmbRating.Items.Add("★☆☆☆☆  Very Bad (1)");

            if (_existing == null)
            {
                this.Text = "Record Feedback";
                lblHeader.Text = "Record Feedback";
                lblSubheader.Text = "Enter customer's rating and comments";

                cmbRating.SelectedIndex = 0;    // 5 stars default
                numCustomerId.Value = 1;
            }
            else
            {
                this.Text = "Edit Feedback";
                lblHeader.Text = "Edit Feedback";
                lblSubheader.Text = $"Editing feedback #{_existing.FeedbackId} from {_existing.CreatedAt:MMM d, yyyy}";

                // Rating: 5 → index 0, 4 → index 1, ..., 1 → index 4
                var ratingIndex = 5 - _existing.Rating;
                if (ratingIndex >= 0 && ratingIndex < cmbRating.Items.Count)
                    cmbRating.SelectedIndex = ratingIndex;

                numCustomerId.Value = Math.Max(1, Math.Min(numCustomerId.Maximum, _existing.CustomerId));
                txtComments.Text = _existing.Comments ?? "";

                // Lock the customer ID in edit mode (can't reassign feedback to another customer)
                numCustomerId.Enabled = false;
                numCustomerId.BackColor = Color.FromArgb(243, 244, 246);
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
            // 5 → 5 stars, 4 → 4 stars, ..., 1 → 1 star
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

            var rating = 5 - cmbRating.SelectedIndex;
            if (rating < 1 || rating > 5)
            {
                ShowError("Please select a rating.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var customerId = (int)numCustomerId.Value;
            var comments = string.IsNullOrWhiteSpace(txtComments.Text) ? null : txtComments.Text.Trim();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new CustomerFeedbackCreateRequest
                    {
                        CustomerId = customerId,
                        Rating = rating,
                        Comments = comments
                    };

                    var created = await _feedbackService.CreateAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create feedback. Please verify the customer ID exists.");
                        return;
                    }
                }
                else
                {
                    var request = new CustomerFeedbackCreateRequest
                    {
                        CustomerId = _existing.CustomerId,   // keep existing customer
                        Rating = rating,
                        Comments = comments
                    };

                    var updated = await _feedbackService.UpdateAsync(companyId, _existing.FeedbackId, request);
                    if (updated == null)
                    {
                        ShowError("Failed to update feedback. Please try again.");
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