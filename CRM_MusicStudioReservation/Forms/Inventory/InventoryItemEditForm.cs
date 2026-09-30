using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Inventory
{
    public partial class InventoryItemEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly InventoryService _inventoryService;
        private readonly StudioService _studioService;
        private readonly List<InventoryCategoryDto> _categories;
        private readonly InventoryItemDto? _existing;

        private List<StudioDto> _studios = new();

        private static readonly string[] Conditions =
            { "New", "Good", "Fair", "NeedsRepair", "Retired" };

        private static readonly string[] Availabilities =
            { "Available", "InUse", "Maintenance", "Lost" };

        private static readonly string[] DefaultLocations =
            { "Storage Room", "Audio Rack A", "Instrument Cabinet", "Studio A", "Studio B", "Studio C", "Studio D", "Studio E" };

        private class StudioComboItem
        {
            public int? StudioId { get; set; }
            public string DisplayText { get; set; } = string.Empty;
            public string StudioName { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }

        // Win32 Interop for smooth window dragging
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public InventoryItemEditForm(
            AuthService auth,
            ApiClient api,
            List<InventoryCategoryDto> categories,
            InventoryItemDto? existing)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inventoryService = new InventoryService(api);
            _studioService = new StudioService(api);
            _categories = categories ?? new List<InventoryCategoryDto>();
            _existing = existing;

            EnableDragging(lblHeader, lblSubheader, this);
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

        private async void InventoryItemEditForm_Load(object sender, EventArgs e)
        {
            // Populate dropdowns
            cmbCondition.Items.Clear();
            foreach (var c in Conditions) cmbCondition.Items.Add(c);

            cmbAvailability.Items.Clear();
            foreach (var a in Availabilities) cmbAvailability.Items.Add(a);

            cmbLocation.Items.Clear();
            foreach (var l in DefaultLocations) cmbLocation.Items.Add(l);

            cmbCategory.Items.Clear();
            foreach (var c in _categories) cmbCategory.Items.Add(c.CategoryName);

            // Load studios asynchronously
            await LoadStudiosAsync();

            if (_existing == null)
            {
                this.Text = "Add Inventory Item";
                lblHeader.Text = "Add Inventory Item";
                lblSubheader.Text = "Fill in the details below. Item code will be generated automatically.";

                lblCode.Text = "Item Code (System Generated)";
                txtCode.Text = "Auto-generated upon save (e.g., MIC-0001)";
                txtCode.ForeColor = Color.FromArgb(107, 114, 128);

                cmbCondition.SelectedIndex = 1; // Good
                cmbAvailability.SelectedIndex = 0; // Available
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                cmbLocation.SelectedIndex = 0; // Storage Room
            }
            else
            {
                this.Text = "Edit Inventory Item";
                lblHeader.Text = "Edit Inventory Item";
                lblSubheader.Text = $"Editing: {_existing.ItemCode}";

                lblCode.Text = "Item Code";
                txtCode.Text = _existing.ItemCode;
                txtCode.ForeColor = Color.FromArgb(31, 41, 55);

                txtName.Text = _existing.ItemName;

                if (!string.IsNullOrWhiteSpace(_existing.CategoryName))
                {
                    var idx = cmbCategory.Items.IndexOf(_existing.CategoryName);
                    if (idx >= 0) cmbCategory.SelectedIndex = idx;
                }

                cmbCondition.SelectedItem = _existing.Condition;
                cmbAvailability.SelectedItem = _existing.Availability;

                if (!string.IsNullOrWhiteSpace(_existing.Location))
                {
                    var idx = cmbLocation.Items.IndexOf(_existing.Location);
                    if (idx >= 0) cmbLocation.SelectedIndex = idx;
                    else
                    {
                        cmbLocation.Items.Add(_existing.Location);
                        cmbLocation.SelectedItem = _existing.Location;
                    }
                }

                numQuantity.Value = Math.Max(0, Math.Min(numQuantity.Maximum, _existing.QuantityOnHand));
                numReorderLevel.Value = Math.Max(0, Math.Min(numReorderLevel.Maximum, _existing.ReorderLevel));
                numUnitCost.Value = Math.Max(0, Math.Min(numUnitCost.Maximum, _existing.UnitCost));

                SelectStudioById(_existing.StudioId);
            }
        }

        private async Task LoadStudiosAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            try
            {
                _studios = await _studioService.GetAllAsync(companyId);
            }
            catch
            {
                _studios = new List<StudioDto>();
            }

            cmbStudio.Items.Clear();
            cmbStudio.Items.Add(new StudioComboItem
            {
                StudioId = null,
                DisplayText = "(Unassigned / Storage)",
                StudioName = "Storage Room"
            });

            foreach (var s in _studios)
            {
                cmbStudio.Items.Add(new StudioComboItem
                {
                    StudioId = s.StudioId,
                    DisplayText = $"{s.StudioName} ({s.StudioCode})",
                    StudioName = s.StudioName
                });
            }

            if (_existing != null)
            {
                SelectStudioById(_existing.StudioId);
            }
            else
            {
                cmbStudio.SelectedIndex = 0;
            }

            cmbStudio.SelectedIndexChanged += cmbStudio_SelectedIndexChanged;
        }

        private void SelectStudioById(int? studioId)
        {
            for (int i = 0; i < cmbStudio.Items.Count; i++)
            {
                if (cmbStudio.Items[i] is StudioComboItem item && item.StudioId == studioId)
                {
                    cmbStudio.SelectedIndex = i;
                    return;
                }
            }
            if (cmbStudio.Items.Count > 0)
                cmbStudio.SelectedIndex = 0;
        }

        private void cmbStudio_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbStudio.SelectedItem is StudioComboItem selected && selected.StudioId.HasValue)
            {
                // Auto-suggest studio location if blank or default
                if (string.IsNullOrWhiteSpace(cmbLocation.Text) ||
                    DefaultLocations.Contains(cmbLocation.Text) ||
                    _studios.Any(s => s.StudioName == cmbLocation.Text))
                {
                    var locIdx = cmbLocation.Items.IndexOf(selected.StudioName);
                    if (locIdx >= 0)
                        cmbLocation.SelectedIndex = locIdx;
                    else
                        cmbLocation.Text = selected.StudioName;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            HideError();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Item name is required.");
                txtName.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                ShowError("Please select an inventory category.");
                cmbCategory.Focus();
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var categoryId = _categories[cmbCategory.SelectedIndex].InventoryCategoryId;
            var selectedStudio = cmbStudio.SelectedItem as StudioComboItem;
            var studioId = selectedStudio?.StudioId;
            var location = string.IsNullOrWhiteSpace(cmbLocation.Text)
                ? (selectedStudio?.StudioName ?? "Storage Room")
                : cmbLocation.Text.Trim();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new InventoryItemCreateRequest
                    {
                        ItemCode = null, // Auto-generated sequentially by backend
                        ItemName = txtName.Text.Trim(),
                        InventoryCategoryId = categoryId,
                        StudioId = studioId,
                        QuantityOnHand = (int)numQuantity.Value,
                        ReorderLevel = (int)numReorderLevel.Value,
                        UnitCost = numUnitCost.Value,
                        Condition = cmbCondition.SelectedItem?.ToString() ?? "Good",
                        Availability = cmbAvailability.SelectedItem?.ToString() ?? "Available",
                        Location = location
                    };

                    var created = await _inventoryService.CreateItemAsync(companyId, request);
                    if (created == null)
                    {
                        ShowError("Failed to create item. Please verify connection and try again.");
                        return;
                    }

                    var assignedStudioText = created.StudioName ?? (studioId.HasValue ? $"Studio #{studioId}" : "Storage");
                    MessageBox.Show(
                        $"Inventory item created successfully!\n\n" +
                        $"Item Code: {created.ItemCode}\n" +
                        $"Item Name: {created.ItemName}\n" +
                        $"Assigned To: {assignedStudioText}\n" +
                        $"Location: {created.Location}",
                        "Item Created",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    var request = new InventoryItemUpdateRequest
                    {
                        ItemCode = txtCode.Text.Trim(),
                        ItemName = txtName.Text.Trim(),
                        InventoryCategoryId = categoryId,
                        StudioId = studioId,
                        QuantityOnHand = (int)numQuantity.Value,
                        ReorderLevel = (int)numReorderLevel.Value,
                        UnitCost = numUnitCost.Value,
                        Condition = cmbCondition.SelectedItem?.ToString(),
                        Availability = cmbAvailability.SelectedItem?.ToString(),
                        Location = location
                    };

                    var updated = await _inventoryService.UpdateItemAsync(
                        companyId, _existing.InventoryItemId, request);

                    if (updated == null)
                    {
                        ShowError("Failed to update item. Please try again.");
                        return;
                    }

                    MessageBox.Show(
                        $"Inventory item updated successfully!\n\n" +
                        $"Item Code: {updated.ItemCode}\n" +
                        $"Item Name: {updated.ItemName}\n" +
                        $"Assigned To: {(updated.StudioName ?? "Unassigned")}",
                        "Item Updated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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