using System;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Terms
{
    public partial class TermsEditForm : Form
    {
        private readonly TermsService _termsService;
        private readonly int _companyId;
        private readonly TermsDetailDto? _existingTerms;

        public bool SavedSuccessfully { get; private set; } = false;

        private static readonly (string Value, string Label)[] PolicyTypes =
        {
            ("GeneralTerms", "General Terms & Conditions"),
            ("BookingPolicy", "Booking & Reservation Policy"),
            ("CancellationPolicy", "Cancellation & Refund Policy"),
            ("PrivacyPolicy", "Privacy & Data Policy"),
            ("StudioUsageRules", "Studio Equipment & Usage Rules")
        };

        public TermsEditForm(TermsService termsService, int companyId, TermsDetailDto? existingTerms = null)
        {
            InitializeComponent();
            _termsService = termsService;
            _companyId = companyId;
            _existingTerms = existingTerms;

            foreach (var item in PolicyTypes)
                cmbType.Items.Add(item.Label);
            cmbType.SelectedIndex = 0;

            if (_existingTerms != null)
            {
                lblFormTitle.Text = $"Edit Draft ({_existingTerms.TandCCode})";
                txtTitle.Text = _existingTerms.Title;
                txtContent.Text = _existingTerms.Content;
                chkReAccept.Checked = _existingTerms.RequiresReAcceptance;
                txtChangeNotes.Text = _existingTerms.ChangeNotes ?? string.Empty;

                var idx = Array.FindIndex(PolicyTypes, p => p.Value == _existingTerms.TandCType);
                if (idx >= 0) cmbType.SelectedIndex = idx;
                cmbType.Enabled = false; // Cannot change policy type on existing draft
            }
            else
            {
                lblFormTitle.Text = "New Terms & Conditions Draft";
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                ShowError("Please enter a title for the policy.");
                txtTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                ShowError("Policy content cannot be empty.");
                txtContent.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existingTerms == null)
                {
                    var selectedType = PolicyTypes[cmbType.SelectedIndex].Value;
                    var request = new TermsCreateRequest
                    {
                        TandCType = selectedType,
                        Title = txtTitle.Text.Trim(),
                        Content = txtContent.Text.Trim(),
                        RequiresReAcceptance = chkReAccept.Checked,
                        ChangeNotes = string.IsNullOrWhiteSpace(txtChangeNotes.Text) ? null : txtChangeNotes.Text.Trim()
                    };

                    await _termsService.CreateDraftAsync(_companyId, request);
                }
                else
                {
                    var request = new TermsUpdateRequest
                    {
                        Title = txtTitle.Text.Trim(),
                        Content = txtContent.Text.Trim(),
                        RequiresReAcceptance = chkReAccept.Checked,
                        ChangeNotes = string.IsNullOrWhiteSpace(txtChangeNotes.Text) ? null : txtChangeNotes.Text.Trim()
                    };

                    await _termsService.UpdateDraftAsync(_companyId, _existingTerms.TandCId, request);
                }

                SavedSuccessfully = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Failed to save: {ex.Message}");
                btnSave.Enabled = true;
                btnSave.Text = "Save Draft";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
    }
}
