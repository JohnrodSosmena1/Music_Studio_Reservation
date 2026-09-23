using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

        public CustomerEditForm(CustomerService customerService, int companyId, CustomerDto? existingCustomer = null)
        {
            InitializeComponent();
            _customerService = customerService;
            _companyId = companyId;
            _existingCustomer = existingCustomer;

            if (_existingCustomer != null)
            {
                lblTitle.Text = "Edit Customer";
                txtCode.Text = _existingCustomer.CustomerCode;
                txtName.Text = _existingCustomer.CustomerName;
                txtContact.Text = _existingCustomer.ContactNumber ?? "";
                txtEmail.Text = _existingCustomer.EmailAddress ?? "";
                txtAddress.Text = _existingCustomer.Address ?? "";
                chkActive.Checked = _existingCustomer.IsActive;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            // Validate
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ShowError("Customer Code is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Customer Name is required.");
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existingCustomer == null)
                {
                    // CREATE
                    var request = new CustomerCreateRequest
                    {
                        CustomerCode = txtCode.Text.Trim(),
                        CustomerName = txtName.Text.Trim(),
                        ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
                        EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                        IsActive = chkActive.Checked
                    };

                    var result = await _customerService.CreateAsync(_companyId, request);

                    if (result == null)
                    {
                        ShowError("Failed to create customer. The server rejected the request.");
                        return;
                    }

                    SavedSuccessfully = true;
                    this.Close();
                }
                else
                {
                    // UPDATE
                    var request = new CustomerUpdateRequest
                    {
                        CustomerCode = txtCode.Text.Trim(),
                        CustomerName = txtName.Text.Trim(),
                        ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
                        EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                        IsActive = chkActive.Checked
                    };

                    var result = await _customerService.UpdateAsync(_companyId, _existingCustomer.CustomerId, request);

                    if (result == null)
                    {
                        ShowError("Failed to update customer. The server rejected the request.");
                        return;
                    }

                    SavedSuccessfully = true;
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
                btnSave.Text = "Save";
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