using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM.winforms.Forms.Customers.Dialogs
{
    public partial class EnrollInPlanForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerLoyaltyDto _customer;
        private readonly MembershipService _membershipService;

        private List<MembershipPlanDto> _plans = new();

        public bool EnrolledSuccessfully { get; private set; } = false;

        public EnrollInPlanForm(AuthService auth, ApiClient api, CustomerLoyaltyDto customer)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customer = customer;
            _membershipService = new MembershipService(api);
        }

        private async void EnrollInPlanForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = _customer.CustomerName;
            lblCustomerCode.Text = _customer.CustomerCode;

            // Default dates
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today.AddDays(30);

            await LoadPlansAsync();
        }

        // ==================== LOAD PLANS ====================

        private async System.Threading.Tasks.Task LoadPlansAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var plans = await _membershipService.GetPlansAsync(companyId);

            if (this.IsDisposed) return;

            _plans = plans.Where(p => p.IsActive).ToList();

            cmbPlan.Items.Clear();
            cmbPlan.Items.Add("— Select a plan —");
            foreach (var p in _plans)
            {
                cmbPlan.Items.Add($"{p.PlanName}  ·  {p.FeeDisplay}  ·  {p.PointsDisplay}");
            }
            cmbPlan.SelectedIndex = 0;
        }

        // ==================== PLAN CHANGE ====================

        private void cmbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Index 0 = placeholder
            if (cmbPlan.SelectedIndex <= 0 || cmbPlan.SelectedIndex > _plans.Count)
            {
                lblPlanDetails.Text = "";
                lblPointsPreview.Text = "";
                return;
            }

            var plan = _plans[cmbPlan.SelectedIndex - 1];

            lblPlanDetails.Text =
                $"Monthly fee: {plan.FeeDisplay}\n" +
                $"Points per booking: {plan.LoyaltyPointsPerBooking}\n" +
                $"Benefits: {plan.Benefits ?? "(none)"}";

            // Preview: how many points they'd earn going forward
            if (_customer.TotalBookings > 0)
            {
                var projected = _customer.TotalBookings * plan.LoyaltyPointsPerBooking;
                lblPointsPreview.Text =
                    $"At {_customer.TotalBookings} existing booking(s) × " +
                    $"{plan.LoyaltyPointsPerBooking} pts = {projected} points";
            }
            else
            {
                lblPointsPreview.Text = "No bookings yet — points start accumulating on their next booking.";
            }
        }

        // ==================== ENROLL ====================

        private async void btnEnroll_Click(object sender, EventArgs e)
        {
            if (cmbPlan.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a plan.", "No Plan Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpEnd.Value.Date < dtpStart.Value.Date)
            {
                MessageBox.Show("End date must be on or after the start date.",
                    "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var plan = _plans[cmbPlan.SelectedIndex - 1];

            var confirm = MessageBox.Show(
                $"Enroll {_customer.CustomerName} in the {plan.PlanName} plan?\n\n" +
                $"Start: {dtpStart.Value:MMM d, yyyy}\n" +
                $"End: {dtpEnd.Value:MMM d, yyyy}\n" +
                $"Monthly fee: {plan.FeeDisplay}",
                "Confirm Enrollment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnEnroll.Enabled = false;
            btnEnroll.Text = "Enrolling...";

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            var request = new MembershipCreateRequest
            {
                CustomerId = _customer.CustomerId,
                MembershipPlanId = plan.MembershipPlanId,
                StartDate = dtpStart.Value.Date,
                EndDate = dtpEnd.Value.Date
            };

            var result = await _membershipService.CreateAsync(companyId, request);

            if (this.IsDisposed) return;

            if (result != null)
            {
                EnrolledSuccessfully = true;
                MessageBox.Show(
                    $"{_customer.CustomerName} enrolled in {plan.PlanName} successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                btnEnroll.Enabled = true;
                btnEnroll.Text = "Enroll";
                MessageBox.Show(
                    "Failed to enroll customer. They may already have an active membership.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}