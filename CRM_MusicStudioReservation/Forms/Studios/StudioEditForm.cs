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

        // Win32 Interop for smooth window dragging
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public StudioEditForm(StudioService studioService, int companyId, StudioDto? existingStudio = null)
        {
            InitializeComponent();
            _studioService = studioService;
            _companyId = companyId;
            _existingStudio = existingStudio;

            EnableDragging(lblTitle, this);

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
                btnSave.Text = "Update";

                var idx = System.Array.FindIndex(StudioTypes, t => t.Id == _existingStudio.StudioType);
                if (idx >= 0) cmbType.SelectedIndex = idx;
            }
            else
            {
                lblTitle.Text = "Add Studio";
                txtCode.Text = "Auto-generated upon save (e.g., STD001)";
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Studio Name is required.");
                txtName.Focus();
                return;
            }

            if (numCapacity.Value <= 0)
            {
                ShowError("Capacity must be at least 1 person.");
                numCapacity.Focus();
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
                        StudioCode = null,                           // 👈 auto-generated by backend
                        StudioName = txtName.Text.Trim(),
                        StudioType = selectedTypeId,
                        HourlyRate = numRate.Value,
                        Capacity = (int)numCapacity.Value,
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
                    };

                    var result = await _studioService.CreateAsync(_companyId, request);

                    if (result == null)
                    {
                        ShowError("Failed to create studio. Please check server connection.");
                        return;
                    }

                    SavedSuccessfully = true;

                    MessageBox.Show(
                        $"Studio created successfully!\n\n" +
                        $"Studio Code: {result.StudioCode}\n" +
                        $"Studio Name: {result.StudioName}\n" +
                        $"Hourly Rate: ₱{result.HourlyRate:N2}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    // ============ UPDATE ============
                    var request = new StudioUpdateRequest
                    {
                        StudioCode = _existingStudio.StudioCode,
                        StudioName = txtName.Text.Trim(),
                        StudioType = selectedTypeId,
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

                    MessageBox.Show(
                        $"Studio '{result.StudioCode}' updated successfully.",
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
                btnSave.Text = _existingStudio == null ? "Save" : "Update";
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