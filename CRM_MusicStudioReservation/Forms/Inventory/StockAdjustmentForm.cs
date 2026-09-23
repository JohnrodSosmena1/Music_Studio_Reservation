using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Inventory
{
    public partial class StockAdjustmentForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly InventoryService _inventoryService;
        private readonly InventoryItemDto _item;

        public StockAdjustmentForm(AuthService auth, ApiClient api, InventoryItemDto item)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inventoryService = new InventoryService(api);
            _item = item;
        }

        private void StockAdjustmentForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Adjust Stock — {_item.ItemName}";
            lblSubheader.Text = _item.ItemName;

            lblCurrentQtyValue.Text = _item.QuantityOnHand.ToString();
            numAmount.Value = 1;

            UpdatePreview();
        }

        private void rbStock_CheckedChanged(object sender, EventArgs e)
        {
            lblNewQtyValue.ForeColor = rbStockIn.Checked
                ? Color.FromArgb(16, 185, 129)
                : Color.FromArgb(239, 68, 68);

            UpdatePreview();
        }

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            HideError();

            var delta = rbStockIn.Checked ? (int)numAmount.Value : -(int)numAmount.Value;
            var newQty = _item.QuantityOnHand + delta;

            lblNewQtyValue.Text = newQty.ToString();

            if (newQty < 0)
            {
                ShowError($"Cannot stock out {numAmount.Value}. Only {_item.QuantityOnHand} available.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            HideError();

            var delta = rbStockIn.Checked ? (int)numAmount.Value : -(int)numAmount.Value;

            if (delta == 0)
            {
                ShowError("Quantity must be greater than zero.");
                return;
            }

            var newQty = _item.QuantityOnHand + delta;
            if (newQty < 0)
            {
                ShowError($"Cannot stock out {numAmount.Value}. Only {_item.QuantityOnHand} available.");
                return;
            }

            btnApply.Enabled = false;
            btnApply.Text = "Applying...";

            try
            {
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();

                var result = await _inventoryService.AdjustStockAsync(
                    companyId, _item.InventoryItemId, delta, notes);

                if (result == null)
                {
                    ShowError("Failed to adjust stock. Please try again.");
                    return;
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
                btnApply.Enabled = true;
                btnApply.Text = "Apply";
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