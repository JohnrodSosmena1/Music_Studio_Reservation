using CRM.winforms.DTOs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Inventory
{
    public partial class InventoryManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly InventoryService _inventoryService;

        private List<InventoryItemDto> _allItems = new();
        private List<InventoryCategoryDto> _categories = new();
        private bool _suppressFilterEvents = false;

        public InventoryManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _inventoryService = new InventoryService(api);
        }

        private async void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            EnsureGridColumns();

            await LoadCategoriesAsync();

            if (this.IsDisposed) return;      // 👈 guard

            SetupConditionFilter();

            await LoadItemsAsync();
        }

        // ==================== SAFETY: ensure grid columns exist ====================

        private void EnsureGridColumns()
        {
            if (dgvItems == null || dgvItems.IsDisposed) return;
            if (dgvItems.Columns.Count > 0) return;

            dgvItems.Columns.Clear();

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 30 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Code", FillWeight = 70 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Name", FillWeight = 130 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategory", HeaderText = "Category", FillWeight = 90 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "Qty", FillWeight = 50 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCondition", HeaderText = "Condition", FillWeight = 70 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAvailability", HeaderText = "Availability", FillWeight = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLocation", HeaderText = "Location", FillWeight = 90 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUnitCost", HeaderText = "Unit Cost", FillWeight = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalValue", HeaderText = "Total Value", FillWeight = 90 });

            var actionsCol = new DataGridViewButtonColumn
            {
                Name = "colActions",
                HeaderText = "Actions",
                FillWeight = 100,
                FlatStyle = FlatStyle.Flat
            };
            actionsCol.DefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            actionsCol.DefaultCellStyle.ForeColor = Color.FromArgb(139, 92, 246);
            actionsCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 233, 254);
            actionsCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvItems.Columns.Add(actionsCol);
        }

        // ==================== LOADING ====================

        private async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var categories = await _inventoryService.GetCategoriesAsync(companyId);

            if (this.IsDisposed || cmbCategory.IsDisposed) return;   // 👈 guard

            _categories = categories;

            _suppressFilterEvents = true;
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");
            foreach (var c in _categories)
                cmbCategory.Items.Add(c.CategoryName);
            cmbCategory.SelectedIndex = 0;
            _suppressFilterEvents = false;
        }

        private void SetupConditionFilter()
        {
            if (this.IsDisposed || cmbCondition.IsDisposed) return;

            _suppressFilterEvents = true;
            cmbCondition.Items.Clear();
            cmbCondition.Items.Add("All Conditions");
            cmbCondition.Items.Add("New");
            cmbCondition.Items.Add("Good");
            cmbCondition.Items.Add("Fair");
            cmbCondition.Items.Add("NeedsRepair");
            cmbCondition.Items.Add("Retired");
            cmbCondition.SelectedIndex = 0;
            _suppressFilterEvents = false;
        }

        private async System.Threading.Tasks.Task LoadItemsAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var items = await _inventoryService.GetItemsAsync(companyId);

            if (this.IsDisposed || dgvItems.IsDisposed) return;      // 👈 guard

            _allItems = items;
            ApplyFilters();
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed || dgvItems.IsDisposed) return;      // 👈 guard

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var categoryFilter = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            var conditionFilter = cmbCondition.SelectedItem?.ToString() ?? "All Conditions";
            var lowStockOnly = chkLowStockOnly.Checked;

            var filtered = _allItems.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(i =>
                    (i.ItemName ?? "").ToLowerInvariant().Contains(search) ||
                    (i.ItemCode ?? "").ToLowerInvariant().Contains(search));

            if (categoryFilter != "All Categories")
                filtered = filtered.Where(i => i.CategoryName == categoryFilter);

            if (conditionFilter != "All Conditions")
                filtered = filtered.Where(i => i.Condition == conditionFilter);

            if (lowStockOnly)
                filtered = filtered.Where(i => i.IsLowStock);

            var list = filtered.OrderBy(i => i.ItemName).ToList();
            RenderRows(list);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void cmbCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void chkLowStockOnly_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<InventoryItemDto> items)
        {
            if (this.IsDisposed || dgvItems.IsDisposed) return;      // 👈 guard
            if (dgvItems.Columns.Count == 0) EnsureGridColumns();    // 👈 safety
            if (dgvItems.Columns.Count == 0) return;                 // 👈 still no columns? bail

            dgvItems.Rows.Clear();

            foreach (var i in items)
            {
                var idx = dgvItems.Rows.Add(
                    i.InventoryItemId,
                    i.ItemCode,
                    i.ItemName,
                    i.CategoryName ?? "—",
                    i.QuantityOnHand,
                    i.Condition,
                    i.Availability,
                    i.Location ?? "—",
                    $"₱{i.UnitCost:N2}",
                    $"₱{i.TotalValue:N2}",
                    "⋯ Actions"
                );

                var row = dgvItems.Rows[idx];
                row.Tag = i;

                var condCell = row.Cells["colCondition"];
                condCell.Style.ForeColor = GetConditionColor(i.Condition);
                condCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                var availCell = row.Cells["colAvailability"];
                availCell.Style.ForeColor = GetAvailabilityColor(i.Availability);

                var qtyCell = row.Cells["colQty"];
                if (i.IsLowStock)
                {
                    qtyCell.Style.ForeColor = Color.FromArgb(239, 68, 68);
                    qtyCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }

            lblCount.Text = $"{items.Count} of {_allItems.Count} item(s)";
            var totalValue = items.Sum(x => x.TotalValue);
            lblTotalValue.Text = $"Total Value: ₱{totalValue:N2}";
        }

        private static Color GetConditionColor(string cond) => cond?.ToLowerInvariant() switch
        {
            "new" => Color.FromArgb(16, 185, 129),
            "good" => Color.FromArgb(59, 130, 246),
            "fair" => Color.FromArgb(245, 158, 11),
            "needsrepair" => Color.FromArgb(239, 68, 68),
            "retired" => Color.FromArgb(107, 114, 128),
            _ => Color.Gray
        };

        private static Color GetAvailabilityColor(string avail) => avail?.ToLowerInvariant() switch
        {
            "available" => Color.FromArgb(16, 185, 129),
            "inuse" => Color.FromArgb(139, 92, 246),
            "maintenance" => Color.FromArgb(245, 158, 11),
            "lost" => Color.FromArgb(239, 68, 68),
            _ => Color.Gray
        };

        // ==================== TOP BUTTONS ====================

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var dialog = new InventoryItemEditForm(_auth, _api, _categories, null);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (this.IsDisposed || dgvItems.IsDisposed) return;
                await LoadItemsAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadItemsAsync();
        }

        // ==================== ROW ACTIONS ====================

        private async void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;

            var item = dgvItems.Rows[e.RowIndex].Tag as InventoryItemDto;
            if (item == null) return;

            var dialog = new InventoryItemEditForm(_auth, _api, _categories, item);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (this.IsDisposed || dgvItems.IsDisposed) return;
                await LoadItemsAsync();
            }
        }

        private async void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var item = dgvItems.Rows[e.RowIndex].Tag as InventoryItemDto;
            if (item == null) return;

            var menu = new ContextMenuStrip();
            menu.Items.Add("✏  Edit", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new InventoryItemEditForm(_auth, _api, _categories, item);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.IsDisposed || dgvItems.IsDisposed) return;
                    await LoadItemsAsync();
                }
            });
            menu.Items.Add("📦  Adjust Stock", null, async (s, args) =>
            {
                if (this.IsDisposed) return;
                var dialog = new StockAdjustmentForm(_auth, _api, item);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.IsDisposed || dgvItems.IsDisposed) return;
                    await LoadItemsAsync();
                }
            });
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Delete", null, async (s, args) =>
            {
                if (this.IsDisposed) return;

                var confirm = MessageBox.Show(
                    $"Delete '{item.ItemName}'?\n\nThis item will be archived (IsActive = false).",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;

                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var success = await _inventoryService.DeleteItemAsync(companyId, item.InventoryItemId);

                if (this.IsDisposed || dgvItems.IsDisposed) return;

                if (success)
                {
                    MessageBox.Show("Item archived.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadItemsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to archive item.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            var cellRect = dgvItems.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvItems, cellRect.Left, cellRect.Bottom);
        }
    }
}