using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class ReviewReplyForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerReviewService _reviewService;
        private readonly CustomerReviewDto _review;

        public ReviewReplyForm(AuthService auth, ApiClient api, CustomerReviewDto review)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _reviewService = new CustomerReviewService(api);
            _review = review;
        }

        private void ReviewReplyForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Reply to Review #{_review.CustomerReviewId}";
            lblSubheader.Text = $"Review by Customer {_review.CustomerId}  ·  {_review.Stars}";

            txtOriginalComment.Text = _review.Comment ?? "(no comment)";

            // Pre-fill with existing reply if there is one
            if (!string.IsNullOrWhiteSpace(_review.AdminReply))
            {
                txtReply.Text = _review.AdminReply;
                btnSend.Text = "Update Reply";
                lblHeader.Text = "Update Reply";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            HideError();

            if (string.IsNullOrWhiteSpace(txtReply.Text))
            {
                ShowError("Reply cannot be empty.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var reply = txtReply.Text.Trim();

            btnSend.Enabled = false;
            btnSend.Text = "Sending...";

            try
            {
                var success = await _reviewService.ReplyAsync(companyId, _review.CustomerReviewId, reply);

                if (!success)
                {
                    ShowError("Failed to send reply. Please try again.");
                    return;
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
                btnSend.Enabled = true;
                btnSend.Text = "Send Reply";
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