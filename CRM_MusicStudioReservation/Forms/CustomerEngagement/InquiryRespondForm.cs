using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class InquiryRespondForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerInquiryService _inquiryService;
        private readonly CustomerInquiryDto _inquiry;

        public InquiryRespondForm(AuthService auth, ApiClient api, CustomerInquiryDto inquiry)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inquiryService = new CustomerInquiryService(api);
            _inquiry = inquiry;
        }

        private void InquiryRespondForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Respond to Inquiry #{_inquiry.CustomerInquiryId}";
            lblSubheader.Text = $"Subject: {_inquiry.Subject}";

            txtOriginal.Text = _inquiry.Message ?? "(no message)";

            // Populate status dropdown (default to Resolved)
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Open");
            cmbStatus.Items.Add("InProgress");
            cmbStatus.Items.Add("Resolved");
            cmbStatus.Items.Add("Closed");

            var defaultIdx = cmbStatus.Items.IndexOf("Resolved");
            cmbStatus.SelectedIndex = defaultIdx >= 0 ? defaultIdx : 2;

            // Pre-fill if there's an existing reply
            if (_inquiry.HasResponse)
            {
                txtResponse.Text = _inquiry.Response;
                btnSend.Text = "Update Reply";

                // Pre-select current status
                var curIdx = cmbStatus.Items.IndexOf(_inquiry.Status);
                if (curIdx >= 0) cmbStatus.SelectedIndex = curIdx;
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

            if (string.IsNullOrWhiteSpace(txtResponse.Text))
            {
                ShowError("Response cannot be empty.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var response = txtResponse.Text.Trim();
            var status = cmbStatus.SelectedItem?.ToString() ?? "Resolved";

            btnSend.Enabled = false;
            btnSend.Text = "Sending...";

            try
            {
                var result = await _inquiryService.RespondAsync(companyId, _inquiry.CustomerInquiryId, response, status);

                if (result == null)
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