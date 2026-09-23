using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class InquiryEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerInquiryService _inquiryService;
        private readonly CustomerInquiryDto? _existing;

        public InquiryEditForm(AuthService auth, ApiClient api, CustomerInquiryDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inquiryService = new CustomerInquiryService(api);
            _existing = existing;
        }

        private void InquiryEditForm_Load(object sender, EventArgs e)
        {
            // Populate priority dropdown
            cmbPriority.Items.Clear();
            cmbPriority.Items.Add("Low");
            cmbPriority.Items.Add("Normal");
            cmbPriority.Items.Add("High");
            cmbPriority.Items.Add("Urgent");

            if (_existing == null)
            {
                this.Text = "New Inquiry";
                lblHeader.Text = "New Inquiry";
                lblSubheader.Text = "Record a customer inquiry";

                numCustomerId.Value = 1;
                cmbPriority.SelectedIndex = 1;  // Normal
            }
            else
            {
                this.Text = "Edit Inquiry";
                lblHeader.Text = "Edit Inquiry";
                lblSubheader.Text = $"Editing inquiry #{_existing.CustomerInquiryId}";

                // Customer locked in edit mode
                numCustomerId.Value = Math.Max(1, Math.Min(numCustomerId.Maximum, _existing.CustomerId));
                numCustomerId.Enabled = false;
                numCustomerId.BackColor = Color.FromArgb(243, 244, 246);

                var priorityIdx = cmbPriority.Items.IndexOf(_existing.Priority);
                if (priorityIdx >= 0) cmbPriority.SelectedIndex = priorityIdx;

                txtSubject.Text = _existing.Subject;
                txtMessage.Text = _existing.Message;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideError();

            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                ShowError("Subject is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                ShowError("Message is required.");
                return;
            }
            if (cmbPriority.SelectedIndex < 0)
            {
                ShowError("Please select a priority.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var customerId = (int)numCustomerId.Value;
            var subject = txtSubject.Text.Trim();
            var message = txtMessage.Text.Trim();
            var priority = cmbPriority.SelectedItem?.ToString() ?? "Normal";

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new CustomerInquiryCreateRequest
                    {
                        CustomerId = customerId,
                        Subject = subject,
                        Message = message,
                        Priority = priority
                    };

                    var created = await _inquiryService.CreateAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create inquiry. Please verify the customer ID exists.");
                        return;
                    }
                }
                else
                {
                    var request = new CustomerInquiryUpdateRequest
                    {
                        Subject = subject,
                        Message = message,
                        Priority = priority
                    };

                    var updated = await _inquiryService.UpdateAsync(companyId, _existing.CustomerInquiryId, request);
                    if (updated == null)
                    {
                        ShowError("Failed to update inquiry. Please try again.");
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