using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Inventory
{
    public partial class InventoryItemEditForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly InventoryService _inventoryService;
        private readonly List<InventoryCategoryDto> _categories;
        private readonly InventoryItemDto? _existing;

        private static readonly string[] Conditions =
            { "New", "Good", "Fair", "NeedsRepair", "Retired" };

        private static readonly string[] Availabilities =
            { "Available", "InUse", "Maintenance", "Lost" };

        private static readonly string[] Locations =
            { "Studio A", "Studio B", "Studio C", "Studio D", "Storage Room", "Other" };

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
            _categories = categories ?? new List<InventoryCategoryDto>();
            _existing = existing;

            // Don't let the parent MainForm clip us
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InventoryItemEditForm_Load(object sender, EventArgs e)
        {
            ForceLayoutFix();

            // Populate dropdowns
            cmbCondition.Items.Clear();
            foreach (var c in Conditions) cmbCondition.Items.Add(c);

            cmbAvailability.Items.Clear();
            foreach (var a in Availabilities) cmbAvailability.Items.Add(a);

            cmbLocation.Items.Clear();
            foreach (var l in Locations) cmbLocation.Items.Add(l);

            cmbCategory.Items.Clear();
            foreach (var c in _categories) cmbCategory.Items.Add(c.CategoryName);

            if (_existing == null)
            {
                this.Text = "Add Item";
                lblHeader.Text = "Add Item";
                lblSubheader.Text = "Fill in the details below";

                cmbCondition.SelectedIndex = 1;
                cmbAvailability.SelectedIndex = 0;
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Edit Item";
                lblHeader.Text = "Edit Item";
                lblSubheader.Text = $"Editing: {_existing.ItemCode}";

                txtCode.Text = _existing.ItemCode;
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
            }
        }

        // 👇 Force the size AFTER Windows applies the modal clamp
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Set explicit size AFTER ShowDialog's internal constraints run
            int targetWidth = 580;
            int targetHeight = 780;         // fits comfortably on any 1080p screen

            this.Size = new Size(targetWidth, targetHeight);
            this.MinimumSize = new Size(targetWidth, targetHeight);
            this.MaximumSize = new Size(targetWidth, targetHeight);

            // Center on the active screen
            var wa = Screen.FromControl(this).WorkingArea;
            this.Location = new Point(
                wa.Left + (wa.Width - targetWidth) / 2,
                wa.Top + (wa.Height - targetHeight) / 2);

            // Re-apply our layout to fill the new size
            ForceLayoutFix();
        }

        // ==================== LAYOUT ====================

        private void ForceLayoutFix()
        {
            // Match the target size (client area excludes borders/titlebar)
            this.ClientSize = new Size(580, 720);

            // Header
            pnlHeader.Dock = DockStyle.None;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Size = new Size(580, 90);
            pnlHeader.BackColor = Color.White;

            // Body
            pnlBody.Dock = DockStyle.None;
            pnlBody.Location = new Point(0, 90);
            pnlBody.Size = new Size(580, 560);
            pnlBody.BackColor = Color.White;
            pnlBody.AutoScroll = false;

            // Footer
            pnlFooter.Dock = DockStyle.None;
            pnlFooter.Location = new Point(0, 650);
            pnlFooter.Size = new Size(580, 70);
            pnlFooter.BackColor = Color.FromArgb(249, 250, 251);

            // Buttons
            btnCancel.Location = new Point(290, 13);
            btnCancel.Size = new Size(120, 42);
            btnCancel.Visible = true;

            btnSave.Location = new Point(420, 13);
            btnSave.Size = new Size(120, 42);
            btnSave.Visible = true;

            // ==== Position fields inside body — comfortable spacing ====
            int y = 18;

            // Item Code
            lblCode.Location = new Point(30, y); y += 22;
            txtCode.Location = new Point(30, y); txtCode.Size = new Size(510, 30); y += 48;

            // Item Name
            lblName.Location = new Point(30, y); y += 22;
            txtName.Location = new Point(30, y); txtName.Size = new Size(510, 30); y += 48;

            // Category
            lblCategory.Location = new Point(30, y); y += 22;
            cmbCategory.Location = new Point(30, y); cmbCategory.Size = new Size(510, 30); y += 48;

            // Condition + Availability
            lblCondition.Location = new Point(30, y);
            lblAvailability.Location = new Point(300, y);
            y += 22;
            cmbCondition.Location = new Point(30, y); cmbCondition.Size = new Size(250, 30);
            cmbAvailability.Location = new Point(300, y); cmbAvailability.Size = new Size(240, 30);
            y += 48;

            // Location
            lblLocation.Location = new Point(30, y); y += 22;
            cmbLocation.Location = new Point(30, y); cmbLocation.Size = new Size(510, 30); y += 48;

            // Qty + Reorder + Unit Cost
            lblQuantity.Location = new Point(30, y);
            lblReorderLevel.Location = new Point(200, y);
            lblUnitCost.Location = new Point(370, y);
            y += 22;
            numQuantity.Location = new Point(30, y); numQuantity.Size = new Size(160, 30);
            numReorderLevel.Location = new Point(200, y); numReorderLevel.Size = new Size(160, 30);
            numUnitCost.Location = new Point(370, y); numUnitCost.Size = new Size(170, 30);
            y += 42;

            // Error
            lblError.Location = new Point(30, y);
            lblError.Size = new Size(510, 24);

            // Z-order
            pnlFooter.BringToFront();
            btnSave.BringToFront();
            btnCancel.BringToFront();
        }

        // ==================== ACTIONS ====================

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

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ShowError("Item code is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError("Item name is required.");
                return;
            }
            if (cmbCategory.SelectedIndex < 0)
            {
                ShowError("Please select a category.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var categoryId = _categories[cmbCategory.SelectedIndex].InventoryCategoryId;
            var location = cmbLocation.SelectedItem?.ToString();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (_existing == null)
                {
                    var request = new InventoryItemCreateRequest
                    {
                        ItemCode = txtCode.Text.Trim(),
                        ItemName = txtName.Text.Trim(),
                        InventoryCategoryId = categoryId,
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
                        ShowError("Failed to create item. Please try again.");
                        return;
                    }
                }
                else
                {
                    var request = new InventoryItemUpdateRequest
                    {
                        ItemCode = txtCode.Text.Trim(),
                        ItemName = txtName.Text.Trim(),
                        InventoryCategoryId = categoryId,
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