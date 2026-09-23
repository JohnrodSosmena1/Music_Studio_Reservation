using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class PromotionEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly PromotionService _promotionService;
        private readonly PromotionDto? _existing;

        public PromotionEditForm(AuthService auth, ApiClient api, PromotionDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _promotionService = new PromotionService(api);
            _existing = existing;

            // 👇 Force independent top-level window so parent can't clip us
            this.TopLevel = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        // 👇 Force the exact size AFTER Windows applies its modal clamp
        
        private void PromotionEditForm_Load(object sender, EventArgs e)
        {
            if (_existing == null)
            {
                this.Text = "New Promotion";
                lblHeader.Text = "New Promotion";
                lblSubheader.Text = "Fill in the details below";

                dtpStart.Value = DateTime.Today;
                dtpEnd.Value = DateTime.Today.AddDays(30);
                numDiscount.Value = 10;
            }
            else
            {
                this.Text = "Edit Promotion";
                lblHeader.Text = "Edit Promotion";
                lblSubheader.Text = $"Editing: {_existing.PromotionCode}";

                txtCode.Text = _existing.PromotionCode;
                txtCode.ReadOnly = true;
                txtCode.BackColor = Color.FromArgb(243, 244, 246);

                txtName.Text = _existing.PromotionName;
                txtDescription.Text = _existing.Description ?? "";

                numDiscount.Value = Math.Max(0, Math.Min(100, _existing.DiscountPercent));

                if (_existing.StartDate >= dtpStart.MinDate && _existing.StartDate <= dtpStart.MaxDate)
                    dtpStart.Value = _existing.StartDate;
                if (_existing.EndDate >= dtpEnd.MinDate && _existing.EndDate <= dtpEnd.MaxDate)
                    dtpEnd.Value = _existing.EndDate;
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

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ShowError("Promotion code is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Promotion name is required.");
                return;
            }
            if (numDiscount.Value <= 0 || numDiscount.Value > 100)
            {
                ShowError("Discount must be between 0 and 100.");
                return;
            }
            if (dtpEnd.Value.Date < dtpStart.Value.Date)
            {
                ShowError("End date must be on or after start date.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new PromotionCreateRequest
                    {
                        PromotionCode = txtCode.Text.Trim().ToUpperInvariant(),
                        PromotionName = txtName.Text.Trim(),
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        DiscountPercent = numDiscount.Value,
                        StartDate = dtpStart.Value.Date,
                        EndDate = dtpEnd.Value.Date
                    };

                    var created = await _promotionService.CreateAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create promotion. Please try again.");
                        return;
                    }
                }
                else
                {
                    var request = new PromotionUpdateRequest
                    {
                        PromotionName = txtName.Text.Trim(),
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        DiscountPercent = numDiscount.Value,
                        EndDate = dtpEnd.Value.Date
                    };

                    var updated = await _promotionService.UpdateAsync(companyId, _existing.PromotionId, request);
                    if (updated == null)
                    {
                        ShowError("Failed to update promotion. Please try again.");
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