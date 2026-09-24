using System;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM.winforms.Forms.Customers.Dialogs
{
    public partial class AdjustPointsForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerLoyaltyDto _customer;
        private readonly MembershipService _membershipService;

        public bool AdjustmentSaved { get; private set; } = false;

        public AdjustPointsForm(AuthService auth, ApiClient api, CustomerLoyaltyDto customer)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customer = customer;
            _membershipService = new MembershipService(api);
        }

        private void AdjustPointsForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = _customer.CustomerName;
            lblCustomerCode.Text = _customer.CustomerCode;
            lblPlanSummary.Text = $"{_customer.PlanName}  ·  {_customer.PointsPerBooking} pts/booking";
            lblCurrentPoints.Text = _customer.TotalPointsEarned.ToString("N0");

            // Show existing adjustment if any
            if (_customer.PointsAdjustment != 0)
            {
                var sign = _customer.PointsAdjustment > 0 ? "+" : "";
                lblExistingAdjustment.Text =
                    $"Existing adjustment: {sign}{_customer.PointsAdjustment:N0} points";
                lblExistingAdjustment.Visible = true;
            }
            else
            {
                lblExistingAdjustment.Visible = false;
            }

            // Defaults
            rbAdd.Checked = true;
            numPoints.Value = 0;

            UpdatePreview();
        }

        // ==================== PREVIEW ====================

        private void UpdatePreview()
        {
            var amount = (int)numPoints.Value;
            var isAdd = rbAdd.Checked;
            var delta = isAdd ? amount : -amount;
            var newAdjustment = _customer.PointsAdjustment + delta;
            var newTotal = _customer.PointsFromBookings + newAdjustment;

            if (amount == 0)
            {
                lblPreview.Text = "Enter an amount to see the effect.";
                lblPreview.ForeColor = Color.FromArgb(107, 114, 128);
                return;
            }

            if (newTotal < 0)
            {
                lblPreview.Text =
                    $"⚠ New total would be negative ({newTotal:N0}). Reduce the amount.";
                lblPreview.ForeColor = Color.FromArgb(239, 68, 68);
                return;
            }

            var sign = delta > 0 ? "+" : "−";
            lblPreview.Text =
                $"Preview: {_customer.TotalPointsEarned:N0} {sign} {amount:N0} = {newTotal:N0} points";
            lblPreview.ForeColor = delta > 0
                ? Color.FromArgb(16, 185, 129)
                : Color.FromArgb(239, 68, 68);
        }

        private void numPoints_ValueChanged(object sender, EventArgs e) => UpdatePreview();

        private void rbAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAdd.Checked) UpdatePreview();
        }

        private void rbSubtract_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSubtract.Checked) UpdatePreview();
        }

        // ==================== SAVE ====================

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var amount = (int)numPoints.Value;

            if (amount <= 0)
            {
                MessageBox.Show("Please enter a positive number of points.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason for this adjustment.",
                    "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return;
            }

            if (!_customer.MembershipId.HasValue)
            {
                MessageBox.Show(
                    "This customer has no active membership. Enroll them in a plan first.",
                    "Not Enrolled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var isAdd = rbAdd.Checked;
            var delta = isAdd ? amount : -amount;
            var newAdjustment = _customer.PointsAdjustment + delta;
            var newTotal = _customer.PointsFromBookings + newAdjustment;

            if (newTotal < 0)
            {
                MessageBox.Show(
                    $"This adjustment would result in a negative balance ({newTotal:N0}).\n" +
                    "Please reduce the amount or change to addition.",
                    "Invalid Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var action = isAdd ? "add" : "subtract";
            var confirm = MessageBox.Show(
                $"Are you sure you want to {action} {amount:N0} points for {_customer.CustomerName}?\n\n" +
                $"Reason: {txtReason.Text.Trim()}\n\n" +
                $"Current: {_customer.TotalPointsEarned:N0} → New: {newTotal:N0}",
                "Confirm Adjustment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            // Send the new cumulative adjustment value to the API
            var request = new MembershipUpdateRequest
            {
                LoyaltyPoints = newAdjustment
            };

            var result = await _membershipService.UpdateAsync(
                companyId,
                _customer.MembershipId!.Value,
                request);

            if (this.IsDisposed) return;

            btnSave.Enabled = true;
            btnSave.Text = "Save";

            if (result != null)
            {
                AdjustmentSaved = true;
                MessageBox.Show(
                    $"Adjusted {amount:N0} points for {_customer.CustomerName}.\n\n" +
                    $"New total: {newTotal:N0} points.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Failed to save the adjustment. Please try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}