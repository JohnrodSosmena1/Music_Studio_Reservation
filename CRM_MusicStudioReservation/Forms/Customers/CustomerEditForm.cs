using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Customers
{
    public partial class CustomerEditForm : Form
    {
        private readonly CustomerService _customerService;
        private readonly int _companyId;
        private readonly CustomerDto? _existingCustomer;

        /// <summary>True if Save succeeded. Caller should refresh its grid.</summary>
        public bool SavedSuccessfully { get; private set; } = false;

        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex ContactRegex = new(
            @"^[0-9\+\-\s\(\)]{7,20}$",
            RegexOptions.Compiled);

        // Win32 Interop for smooth window dragging
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public CustomerEditForm(CustomerService customerService, int companyId, CustomerDto? existingCustomer = null)
        {
            InitializeComponent();
            _customerService = customerService;
            _companyId = companyId;
            _existingCustomer = existingCustomer;

            EnableDragging(lblTitle, this);
            WireValidationEvents();

            if (_existingCustomer != null)
            {
                lblTitle.Text = "Edit Customer";
                txtCode.Text = _existingCustomer.CustomerCode;

                // Split name if FirstName/LastName not populated
                if (!string.IsNullOrWhiteSpace(_existingCustomer.FirstName) || !string.IsNullOrWhiteSpace(_existingCustomer.LastName))
                {
                    txtFirstName.Text = _existingCustomer.FirstName;
                    txtLastName.Text = _existingCustomer.LastName;
                }
                else if (!string.IsNullOrWhiteSpace(_existingCustomer.CustomerName))
                {
                    var parts = _existingCustomer.CustomerName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                    txtFirstName.Text = parts.Length > 0 ? parts[0] : "";
                    txtLastName.Text = parts.Length > 1 ? parts[1] : "";
                }

                txtContact.Text = _existingCustomer.ContactNumber ?? "";
                txtEmail.Text = _existingCustomer.EmailAddress ?? "";
                txtAddress.Text = _existingCustomer.Address ?? "";
                chkActive.Checked = _existingCustomer.IsActive;
                btnSave.Text = "Update";
            }
            else
            {
                lblTitle.Text = "Add Customer";
                txtCode.Text = "Auto-generated upon save (e.g., CUST-0000X)";
                chkActive.Checked = true;
                btnSave.Text = "Save";
            }
        }

        private void EnableDragging(params Control[] controls)
        {
            foreach (var ctrl in controls)
            {
                ctrl.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ReleaseCapture();
                        SendMessage(this.Handle, 0xA1 /* WM_NCLBUTTONDOWN */, 0x2 /* HT_CAPTION */, 0);
                    }
                };
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            EnsureFormWithinScreen();
        }

        private void EnsureFormWithinScreen()
        {
            var screen = Screen.FromControl(this);
            var wa = screen.WorkingArea;

            int left = wa.Left + (wa.Width - this.Width) / 2;
            int top = wa.Top + (wa.Height - this.Height) / 2;

            if (top < wa.Top + 25)
                top = wa.Top + 25;

            if (top + this.Height > wa.Bottom - 10)
                top = Math.Max(wa.Top + 25, wa.Bottom - this.Height - 10);

            if (left < wa.Left + 15)
                left = wa.Left + 15;

            this.Location = new Point(left, top);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void WireValidationEvents()
        {
            txtFirstName.TextChanged += (s, e) =>
            {
                if (lblFirstNameError.Visible && !string.IsNullOrWhiteSpace(txtFirstName.Text))
                    lblFirstNameError.Visible = false;
            };

            txtLastName.TextChanged += (s, e) =>
            {
                if (lblLastNameError.Visible && !string.IsNullOrWhiteSpace(txtLastName.Text))
                    lblLastNameError.Visible = false;
            };

            txtContact.KeyPress += (s, e) =>
            {
                // Allow control keys (backspace, delete, etc.)
                if (char.IsControl(e.KeyChar)) return;

                // Allow digits, plus, minus, spaces, parentheses
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != ' ' && e.KeyChar != '(' && e.KeyChar != ')')
                {
                    e.Handled = true;
                    lblContactError.Text = "Contact number accepts numbers, +, -, and () only.";
                    lblContactError.Visible = true;
                }
            };

            txtContact.TextChanged += (s, e) =>
            {
                if (lblContactError.Visible && (string.IsNullOrWhiteSpace(txtContact.Text) || ContactRegex.IsMatch(txtContact.Text.Trim())))
                    lblContactError.Visible = false;
            };

            txtEmail.TextChanged += (s, e) =>
            {
                if (lblEmailError.Visible && (string.IsNullOrWhiteSpace(txtEmail.Text) || EmailRegex.IsMatch(txtEmail.Text.Trim())))
                    lblEmailError.Visible = false;
            };
        }

        private bool ValidateForm()
        {
            var isValid = true;
            lblError.Visible = false;

            // 1. First Name required
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                lblFirstNameError.Text = "First Name is required.";
                lblFirstNameError.Visible = true;
                isValid = false;
            }
            else
            {
                lblFirstNameError.Visible = false;
            }

            // 2. Last Name required
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                lblLastNameError.Text = "Last Name is required.";
                lblLastNameError.Visible = true;
                isValid = false;
            }
            else
            {
                lblLastNameError.Visible = false;
            }

            // 3. Contact Number format validation (optional, but if provided must be valid)
            var contact = txtContact.Text.Trim();
            if (!string.IsNullOrEmpty(contact))
            {
                if (!ContactRegex.IsMatch(contact))
                {
                    lblContactError.Text = "Must contain digits only (e.g., 09171234567 or +1-555-0101).";
                    lblContactError.Visible = true;
                    isValid = false;
                }
                else
                {
                    lblContactError.Visible = false;
                }
            }
            else
            {
                lblContactError.Visible = false;
            }

            // 4. Email Address format validation (optional, but if provided must be valid)
            var email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                if (!EmailRegex.IsMatch(email))
                {
                    lblEmailError.Text = "Please enter a valid email address (e.g., user@domain.com).";
                    lblEmailError.Visible = true;
                    isValid = false;
                }
                else
                {
                    lblEmailError.Visible = false;
                }
            }
            else
            {
                lblEmailError.Visible = false;
            }

            return isValid;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existingCustomer == null)
                {
                    // CREATE
                    var request = new CustomerCreateRequest
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
                        EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                        IsActive = chkActive.Checked
                    };

                    var result = await _customerService.CreateAsync(_companyId, request);

                    if (result == null)
                    {
                        ShowError("Failed to create customer. Please verify server connectivity and input data.");
                        return;
                    }

                    SavedSuccessfully = true;

                    MessageBox.Show(
                        $"Customer created successfully!\n\n" +
                        $"Customer Code: {result.CustomerCode}\n" +
                        $"Name: {result.FirstName} {result.LastName}\n" +
                        $"Status: {(result.IsActive ? "Active" : "Inactive")}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    // UPDATE
                    var request = new CustomerUpdateRequest
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
                        EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                        IsActive = chkActive.Checked
                    };

                    var result = await _customerService.UpdateAsync(_companyId, _existingCustomer.CustomerId, request);

                    if (result == null)
                    {
                        ShowError("Failed to update customer. Server returned an error.");
                        return;
                    }

                    SavedSuccessfully = true;

                    MessageBox.Show(
                        $"Customer '{result.CustomerCode}' updated successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = _existingCustomer == null ? "Save" : "Update";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }
    }
}