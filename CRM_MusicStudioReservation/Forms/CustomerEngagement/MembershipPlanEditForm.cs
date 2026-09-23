using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class MembershipPlanEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly MembershipPlanService _planService;
        private readonly MembershipPlanDto? _existing;

        public MembershipPlanEditForm(AuthService auth, ApiClient api, MembershipPlanDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _planService = new MembershipPlanService(api);
            _existing = existing;
        }

        private void MembershipPlanEditForm_Load(object sender, EventArgs e)
        {
            if (_existing == null)
            {
                this.Text = "New Membership Plan";
                lblHeader.Text = "New Membership Plan";
                lblSubheader.Text = "Define the plan details and benefits";

                numFee.Value = 0;
                numPoints.Value = 10;
            }
            else
            {
                this.Text = "Edit Membership Plan";
                lblHeader.Text = "Edit Membership Plan";
                lblSubheader.Text = $"Editing: {_existing.PlanName}";

                txtPlanName.Text = _existing.PlanName;
                numFee.Value = Math.Max(numFee.Minimum, Math.Min(numFee.Maximum, _existing.MonthlyFee));
                numPoints.Value = Math.Max(numPoints.Minimum, Math.Min(numPoints.Maximum, _existing.LoyaltyPointsPerBooking));
                txtDescription.Text = _existing.Description ?? "";
                txtBenefits.Text = _existing.Benefits ?? "";
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

            if (string.IsNullOrWhiteSpace(txtPlanName.Text))
            {
                ShowError("Plan name is required.");
                return;
            }
            if (numFee.Value < 0)
            {
                ShowError("Monthly fee cannot be negative.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var planName = txtPlanName.Text.Trim();
            var description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
            var benefits = string.IsNullOrWhiteSpace(txtBenefits.Text) ? null : txtBenefits.Text.Trim();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new MembershipPlanCreateRequest
                    {
                        PlanName = planName,
                        Description = description,
                        MonthlyFee = numFee.Value,
                        LoyaltyPointsPerBooking = (int)numPoints.Value,
                        Benefits = benefits
                    };

                    var created = await _planService.CreateAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create plan. Please try again.");
                        return;
                    }
                }
                else
                {
                    var request = new MembershipPlanUpdateRequest
                    {
                        PlanName = planName,
                        Description = description,
                        MonthlyFee = numFee.Value,
                        LoyaltyPointsPerBooking = (int)numPoints.Value,
                        Benefits = benefits
                    };

                    var updated = await _planService.UpdateAsync(companyId, _existing.MembershipPlanId, request);
                    if (updated == null)
                    {
                        ShowError("Failed to update plan. Please try again.");
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