using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;


namespace CRM_MusicStudioReservation.Forms.Studios
{
    public partial class StudioEditForm : Form
    {
        private readonly StudioService _studioService;
        private readonly int _companyId;
        private readonly StudioDto? _existingStudio;

        public bool SavedSuccessfully { get; private set; } = false;

        // Must match the domain enum values: 1=Rehearsal, 2=Recording, 3=Vocal, 4=Mixing, 5=Mastering
        private static readonly (int Id, string Name)[] StudioTypes =
        {
            (1, "Rehearsal"),
            (2, "Recording"),
            (3, "Vocal"),
            (4, "Mixing"),
            (5, "Mastering")
        };

        public StudioEditForm(StudioService studioService, int companyId, StudioDto? existingStudio = null)
        {
            InitializeComponent();
            _studioService = studioService;
            _companyId = companyId;
            _existingStudio = existingStudio;

            // Populate dropdown
            foreach (var t in StudioTypes)
                cmbType.Items.Add(t.Name);
            cmbType.SelectedIndex = 0;

            if (_existingStudio != null)
            {
                lblTitle.Text = "Edit Studio";
                txtCode.Text = _existingStudio.StudioCode;
                txtName.Text = _existingStudio.StudioName;
                numRate.Value = _existingStudio.HourlyRate;
                numCapacity.Value = _existingStudio.Capacity;
                txtDescription.Text = _existingStudio.Description ?? "";
                chkActive.Checked = _existingStudio.IsActive;

                var idx = System.Array.FindIndex(StudioTypes, t => t.Id == _existingStudio.StudioType);
                if (idx >= 0) cmbType.SelectedIndex = idx;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ShowError("Studio Code is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Studio Name is required.");
                return;
            }

            var selectedTypeId = StudioTypes[cmbType.SelectedIndex].Id;

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existingStudio == null)
                {
                    // ============ CREATE ============
                    var request = new StudioCreateRequest
                    {
                        StudioCode = txtCode.Text.Trim(),
                        StudioName = txtName.Text.Trim(),
                        StudioType = selectedTypeId,                 // 👈 int
                        HourlyRate = numRate.Value,
                        Capacity = (int)numCapacity.Value,
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
                    };

                    var result = await _studioService.CreateAsync(_companyId, request);

                    if (result == null)
                    {
                        ShowError("Failed to create studio.");
                        return;
                    }

                    SavedSuccessfully = true;
                    this.Close();
                }
                else
                {
                    // ============ UPDATE ============
                    var request = new StudioUpdateRequest
                    {
                        StudioCode = txtCode.Text.Trim(),
                        StudioName = txtName.Text.Trim(),
                        StudioType = selectedTypeId,                 // 👈 int
                        HourlyRate = numRate.Value,
                        Capacity = (int)numCapacity.Value,
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        IsActive = chkActive.Checked
                    };

                    var result = await _studioService.UpdateAsync(_companyId, _existingStudio.StudioId, request);

                    if (result == null)
                    {
                        ShowError("Failed to update studio.");
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