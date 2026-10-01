using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Organizations
{
    public class AddOrganizationModal : Form
    {
        private readonly SuperAdminService _service;

        private TextBox txtName = null!;
        private TextBox txtSubdomain = null!;
        private TextBox txtOwnerFirst = null!;
        private TextBox txtOwnerLast = null!;
        private TextBox txtOwnerEmail = null!;
        private TextBox txtContact = null!;
        private ComboBox cmbPlan = null!;
        private CheckBox chkActive = null!;
        private Button btnCancel = null!;
        private Button btnSave = null!;
        private Label lblError = null!;

        private List<SubscriptionPlanDto> _plans = new();

        public AddOrganizationModal(SuperAdminService service)
        {
            _service = service;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Add Studio Organization";
            this.Size = new Size(560, 710);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "New Studio Organization",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(30, 18),
                AutoSize = true,
                UseMnemonic = false
            };

            var lblSub = new Label
            {
                Text = "Set up tenant data scope, subdomain, and initial owner credentials",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(30, 58),
                AutoSize = true,
                UseMnemonic = false
            };

            int y = 96;

            // Organization Name
            this.Controls.Add(CreateLabel("Organization / Studio Name *", 30, y));
            txtName = CreateTextBox(30, y + 22, 480);
            this.Controls.Add(txtName);
            y += 65;

            // Subdomain
            this.Controls.Add(CreateLabel("Subdomain * (e.g. soundwave -> soundwave.crmapp.com)", 30, y));
            txtSubdomain = CreateTextBox(30, y + 22, 480);
            txtSubdomain.PlaceholderText = "lowercase letters and hyphens only";
            this.Controls.Add(txtSubdomain);
            y += 65;

            // Owner Name (2 columns)
            this.Controls.Add(CreateLabel("Owner First Name *", 30, y));
            txtOwnerFirst = CreateTextBox(30, y + 22, 230);
            this.Controls.Add(txtOwnerFirst);

            this.Controls.Add(CreateLabel("Owner Last Name *", 280, y));
            txtOwnerLast = CreateTextBox(280, y + 22, 230);
            this.Controls.Add(txtOwnerLast);
            y += 65;

            // Owner Email
            this.Controls.Add(CreateLabel("Owner / Admin Email * (login username)", 30, y));
            txtOwnerEmail = CreateTextBox(30, y + 22, 480);
            txtOwnerEmail.PlaceholderText = "owner@studio.com";
            this.Controls.Add(txtOwnerEmail);
            y += 65;

            // Contact Number & Plan
            this.Controls.Add(CreateLabel("Contact Number", 30, y));
            txtContact = CreateTextBox(30, y + 22, 230);
            this.Controls.Add(txtContact);

            this.Controls.Add(CreateLabel("Subscription Plan", 280, y));
            cmbPlan = new ComboBox
            {
                Location = new Point(280, y + 22),
                Size = new Size(230, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(cmbPlan);
            y += 65;

            // Active Checkbox
            chkActive = new CheckBox
            {
                Text = "Active (Enable immediate tenant access)",
                Location = new Point(30, y),
                AutoSize = true,
                Checked = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81)
            };
            this.Controls.Add(chkActive);
            y += 35;

            // Error Label
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

            // Buttons
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
                Text = "Create Tenant",
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
            this.Controls.Add(lblSub);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);

            this.Load += async (s, e) => await LoadPlansAsync();
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

        private async Task LoadPlansAsync()
        {
            try
            {
                _plans = await _service.GetPlansAsync();
                cmbPlan.Items.Clear();
                foreach (var p in _plans)
                {
                    cmbPlan.Items.Add($"{p.PlanName} (₱{p.Price:N0}/{p.BillingCycle})");
                }
                if (cmbPlan.Items.Count > 0) cmbPlan.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AddOrganizationModal] {ex.Message}");
            }
        }

        private async Task SaveAsync()
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Please enter Organization Name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSubdomain.Text))
            {
                ShowError("Please enter a Subdomain.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOwnerFirst.Text) || string.IsNullOrWhiteSpace(txtOwnerLast.Text))
            {
                ShowError("Please enter Owner First and Last name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOwnerEmail.Text) || !txtOwnerEmail.Text.Contains("@"))
            {
                ShowError("Please enter a valid Owner Email address.");
                return;
            }

            int? selectedPlanId = null;
            if (cmbPlan.SelectedIndex >= 0 && cmbPlan.SelectedIndex < _plans.Count)
            {
                selectedPlanId = _plans[cmbPlan.SelectedIndex].SubscriptionPlanId;
            }

            var dto = new OrganizationCreateDto
            {
                CompanyName = txtName.Text.Trim(),
                Subdomain = txtSubdomain.Text.Trim().ToLowerInvariant(),
                OwnerFirstName = txtOwnerFirst.Text.Trim(),
                OwnerLastName = txtOwnerLast.Text.Trim(),
                OwnerEmail = txtOwnerEmail.Text.Trim(),
                ContactNumber = txtContact.Text.Trim(),
                SubscriptionPlanId = selectedPlanId,
                IsActive = chkActive.Checked
            };

            btnSave.Enabled = false;
            btnSave.Text = "Creating...";

            var success = await _service.CreateOrganizationAsync(dto);
            if (success)
            {
                MessageBox.Show(
                    $"Organization '{dto.CompanyName}' created successfully.\n\n" +
                    $"Initial Admin Login: {dto.OwnerEmail}\n" +
                    $"Temporary Password: ChangeMe123!",
                    "Tenant Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ShowError("Failed to create organization. Please verify subdomain uniqueness and email format.");
                btnSave.Enabled = true;
                btnSave.Text = "Create Tenant";
            }
        }

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }
    }
}
