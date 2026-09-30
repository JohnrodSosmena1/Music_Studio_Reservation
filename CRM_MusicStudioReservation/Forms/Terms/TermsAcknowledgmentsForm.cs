using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Terms
{
    public partial class TermsAcknowledgmentsForm : Form
    {
        private readonly TermsService _termsService;
        private readonly int _companyId;
        private readonly int? _tandCId;
        private readonly string? _policyTitle;

        public TermsAcknowledgmentsForm(TermsService termsService, int companyId, int? tandCId = null, string? policyTitle = null)
        {
            InitializeComponent();
            _termsService = termsService;
            _companyId = companyId;
            _tandCId = tandCId;
            _policyTitle = policyTitle;

            lblSubtitle.Text = string.IsNullOrWhiteSpace(_policyTitle)
                ? "Full log of all client acceptances across policies"
                : $"Acceptances for policy: {_policyTitle}";
        }

        private async void TermsAcknowledgmentsForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                dgvAcks.Rows.Clear();
                var list = await _termsService.GetAcknowledgmentsAsync(_companyId, _tandCId);

                foreach (var a in list)
                {
                    dgvAcks.Rows.Add(
                        a.AcknowledgmentId,
                        a.TandCCode,
                        a.TandCVersion,
                        a.CustomerId,
                        a.AcknowledgmentContext,
                        a.AcknowledgedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        a.IpAddress ?? "Unknown",
                        a.UserAgent ?? "Standard Client"
                    );
                }

                lblCount.Text = $"{list.Count} acknowledgment record(s) logged";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load acknowledgments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
