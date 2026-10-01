using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Organizations
{
    public class EditOrganizationModal : Form
    {
        private readonly SuperAdminService _service;
        private readonly int _companyId;

        private Label lblCode = null!;
        private TextBox txtName = null!;
        private TextBox txtSubdomain = null!;
        private TextBox txtOwnerFirst = null!;
        private TextBox txtOwnerLast = null!;
        private TextBox txtOwnerEmail = null!;
        private TextBox txtContact = null!;
        private ComboBox cmbStatus = null!;
        private ComboBox cmbPlan = null!;
        private CheckBox chkActive = null!;
        private Button btnCancel = null!;
        private Button btnSave = null!;
        private Label lblError = null!;

        private List<SubscriptionPlanDto> _plans = new();
        private OrganizationDetailDto? _detail;

        public EditOrganizationModal(SuperAdminService service, int companyId)
        {
            _service = service;
            _companyId = companyId;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Studio Organization";
            this.Size = new Size(560, 710);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "Edit Organization",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(30, 18),
                AutoSize = true,
                UseMnemonic = false
            };

            lblCode = new Label
            {
                Text = "TEN-XXXXX",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(139, 92, 246),
                Location = new Point(30, 58),
                AutoSize = true,
                UseMnemonic = false
            };

            int y = 96;

            this.Controls.Add(CreateLabel("Organization Name *", 30, y));
            txtName = CreateTextBox(30, y + 22, 480);
            this.Controls.Add(txtName);
            y += 65;

            this.Controls.Add(CreateLabel("Subdomain (e.g. soundwave -> soundwave.crmapp.com)", 30, y));
            txtSubdomain = CreateTextBox(30, y + 22, 480);
            this.Controls.Add(txtSubdomain);
            y += 65;

            this.Controls.Add(CreateLabel("Owner First Name", 30, y));
            txtOwnerFirst = CreateTextBox(30, y + 22, 230);
            this.Controls.Add(txtOwnerFirst);

            this.Controls.Add(CreateLabel("Owner Last Name", 280, y));
            txtOwnerLast = CreateTextBox(280, y + 22, 230);
            this.Controls.Add(txtOwnerLast);
            y += 65;

            this.Controls.Add(CreateLabel("Owner Email", 30, y));
            txtOwnerEmail = CreateTextBox(30, y + 22, 480);
            this.Controls.Add(txtOwnerEmail);
            y += 65;

            this.Controls.Add(CreateLabel("Contact Number", 30, y));
            txtContact = CreateTextBox(30, y + 22, 230);
            this.Controls.Add(txtContact);

            this.Controls.Add(CreateLabel("Status", 280, y));
            cmbStatus = new ComboBox
            {
                Location = new Point(280, y + 22),
                Size = new Size(230, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive", "Suspended", "Trial", "Archived" });
            this.Controls.Add(cmbStatus);
            y += 65;

            this.Controls.Add(CreateLabel("Subscription Plan", 30, y));
            cmbPlan = new ComboBox
            {
                Location = new Point(30, y + 22),
                Size = new Size(230, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(cmbPlan);

            chkActive = new CheckBox
            {
                Text = "Active (Tenant enabled)",
                Location = new Point(280, y + 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81)
            };
            this.Controls.Add(chkActive);
            y += 65;

            lblError = new Label
            {
                ForeColor = Color.FromArgb(239, 68, 68),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(30, y),
                Size = new Size(480, 25),
                Visible = false
            };
            this.Controls.Add(lblError);
            y += 30;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(280, y),
                Size = new Size(110, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnCancel.Click += (s, e) => this.Close();

            btnSave = new Button
            {
                Text = "Save Changes",
                Location = new Point(400, y),
                Size = new Size(110, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(139, 92, 246),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += async (s, e) => await SaveAsync();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblCode);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);

            this.Load += async (s, e) => await LoadDataAsync();
        }

        private Label CreateLabel(string text, int x, int y) => new()
        {
            Text = text,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(x, y),
            AutoSize = true
        };

        private TextBox CreateTextBox(int x, int y, int width) => new()
        {
            Location = new Point(x, y),
            Size = new Size(width, 28),
            Font = new Font("Segoe UI", 10F)
        };

        private async Task LoadDataAsync()
        {
            try
            {
                _plans = await _service.GetPlansAsync();
                cmbPlan.Items.Clear();
                foreach (var p in _plans)
                {
                    cmbPlan.Items.Add($"{p.PlanName} (₱{p.Price:N0})");
                }

                _detail = await _service.GetOrganizationDetailAsync(_companyId);
                if (_detail == null) return;

                lblCode.Text = $"Tenant Code: {_detail.CompanyCode}   ·   Created: {_detail.CreatedAt:MMM d, yyyy}";
                txtName.Text = _detail.CompanyName;
                txtSubdomain.Text = _detail.Subdomain ?? "";
                txtOwnerFirst.Text = _detail.OwnerFirstName ?? "";
                txtOwnerLast.Text = _detail.OwnerLastName ?? "";
                txtOwnerEmail.Text = _detail.OwnerEmail ?? "";
                txtContact.Text = _detail.ContactNumber ?? "";
                chkActive.Checked = _detail.IsActive;

                var statusIdx = cmbStatus.FindStringExact(_detail.Status);
                if (statusIdx >= 0) cmbStatus.SelectedIndex = statusIdx;
                else cmbStatus.SelectedIndex = 0;

                var planIdx = _plans.FindIndex(p => p.SubscriptionPlanId == _detail.SubscriptionPlanId);
                if (planIdx >= 0) cmbPlan.SelectedIndex = planIdx;
                else if (cmbPlan.Items.Count > 0) cmbPlan.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EditOrganizationModal] {ex.Message}");
            }
        }

        private async Task SaveAsync()
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblError.Text = "Organization name cannot be empty.";
                lblError.Visible = true;
                return;
            }

            int? selectedPlanId = null;
            if (cmbPlan.SelectedIndex >= 0 && cmbPlan.SelectedIndex < _plans.Count)
            {
                selectedPlanId = _plans[cmbPlan.SelectedIndex].SubscriptionPlanId;
            }

            var dto = new OrganizationUpdateDto
            {
                CompanyName = txtName.Text.Trim(),
                Subdomain = txtSubdomain.Text.Trim(),
                OwnerFirstName = txtOwnerFirst.Text.Trim(),
                OwnerLastName = txtOwnerLast.Text.Trim(),
                OwnerEmail = txtOwnerEmail.Text.Trim(),
                ContactNumber = txtContact.Text.Trim(),
                Status = cmbStatus.SelectedItem?.ToString() ?? "Active",
                SubscriptionPlanId = selectedPlanId,
                IsActive = chkActive.Checked
            };

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            var success = await _service.UpdateOrganizationAsync(_companyId, dto);
            if (success)
            {
                MessageBox.Show("Organization updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblError.Text = "Failed to update organization. Verify subdomain uniqueness.";
                lblError.Visible = true;
                btnSave.Enabled = true;
                btnSave.Text = "Save Changes";
            }
        }
    }
}
