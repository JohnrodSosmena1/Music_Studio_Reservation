using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Studios
{
    public partial class StudioDetailsForm : Form
    {
        private readonly StudioService _studioService;
        private readonly int _companyId;
        private readonly StudioDto _studio;
        private List<StudioInventoryItemDto> _items = new();

        // Win32 Interop for smooth window dragging
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public StudioDetailsForm(StudioService studioService, int companyId, StudioDto studio)
        {
            InitializeComponent();
            _studioService = studioService;
            _companyId = companyId;
            _studio = studio;

            PopulateStudioInfo();
            SetupDraggingAndInteraction();
        }

        private void SetupDraggingAndInteraction()
        {
            // Allow dragging the modal by clicking and holding any header or info area
            EnableDragging(pnlHeader, lblTitle, lblSubtitle);
            EnableDragging(pnlStudioInfo, lblInfoCode, lblInfoType, lblInfoRate, lblInfoCapacity, lblInfoStatus, lblInfoDescription);
            EnableDragging(pnlSectionHeader, lblInventoryTitle, lblInventoryCount);
            EnableDragging(pnlFooter, lblTotalValue);

            // Header close button hover styling
            btnCloseHeader.MouseEnter += (s, e) =>
            {
                btnCloseHeader.ForeColor = Color.FromArgb(239, 68, 68);
                btnCloseHeader.BackColor = Color.FromArgb(254, 242, 242);
            };
            btnCloseHeader.MouseLeave += (s, e) =>
            {
                btnCloseHeader.ForeColor = Color.FromArgb(156, 163, 175);
                btnCloseHeader.BackColor = Color.White;
            };
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

            // Constrain form size so it never exceeds the usable monitor working area
            int targetW = Math.Min(this.Width, wa.Width - 30);
            int targetH = Math.Min(this.Height, wa.Height - 50);
            if (this.Width != targetW || this.Height != targetH)
            {
                this.Size = new Size(targetW, targetH);
            }

            // Center within screen working area
            int left = wa.Left + (wa.Width - this.Width) / 2;
            int top = wa.Top + (wa.Height - this.Height) / 2;

            // CRITICAL: Top must NEVER be above wa.Top + 25
            if (top < wa.Top + 25)
                top = wa.Top + 25;

            // Bottom clamp
            if (top + this.Height > wa.Bottom - 10)
                top = Math.Max(wa.Top + 25, wa.Bottom - this.Height - 10);

            // Left clamp
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

        private void PopulateStudioInfo()
        {
            lblTitle.Text = $"{_studio.StudioName} ({_studio.StudioCode})";
            lblInfoCode.Text = $"Code: {_studio.StudioCode}";
            lblInfoType.Text = $"Type: {GetTypeName(_studio.StudioType)}";
            lblInfoRate.Text = $"Hourly Rate: ₱{_studio.HourlyRate:N2}";
            lblInfoCapacity.Text = $"Capacity: {_studio.Capacity} person(s)";
            lblInfoStatus.Text = $"Status: {(_studio.IsActive ? "Active" : "Inactive")}";
            lblInfoStatus.ForeColor = _studio.IsActive ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68);
            lblInfoDescription.Text = string.IsNullOrWhiteSpace(_studio.Description)
                ? "Description: (No description provided)"
                : $"Description: {_studio.Description}";
        }

        private async void StudioDetailsForm_Load(object sender, EventArgs e)
        {
            await LoadAssignedEquipmentAsync();
        }

        private async Task LoadAssignedEquipmentAsync()
        {
            lblInventoryCount.Text = "Loading equipment...";
            dgvEquipment.Rows.Clear();

            _items = await _studioService.GetStudioInventoryAsync(_companyId, _studio.StudioId) ?? new List<StudioInventoryItemDto>();

            if (_items.Count == 0)
            {
                lblInventoryCount.Text = "No equipment currently stationed in this studio.";
                lblTotalValue.Text = "Total Equipment Value: ₱0.00";
                return;
            }

            lblInventoryCount.Text = $"{_items.Count} equipment item(s) stationed in this studio";
            decimal totalVal = 0;

            foreach (var item in _items)
            {
                var idx = dgvEquipment.Rows.Add(
                    item.ItemCode,
                    item.ItemName,
                    item.CategoryName ?? "General",
                    item.QuantityOnHand,
                    item.Condition,
                    item.Availability,
                    $"₱{item.UnitCost:N2}",
                    "⋯ Actions"
                );

                totalVal += (item.QuantityOnHand * item.UnitCost);

                var row = dgvEquipment.Rows[idx];
                row.Tag = item;

                // Condition style
                var condCell = row.Cells[colCondition.Index];
                condCell.Style.ForeColor = item.Condition.ToLowerInvariant() switch
                {
                    "new" => Color.FromArgb(16, 185, 129),
                    "good" => Color.FromArgb(59, 130, 246),
                    "fair" => Color.FromArgb(245, 158, 11),
                    "needsrepair" => Color.FromArgb(239, 68, 68),
                    _ => Color.Gray
                };
                condCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Status/Availability style
                var statusCell = row.Cells[colStatus.Index];
                statusCell.Style.ForeColor = item.Availability.ToLowerInvariant() switch
                {
                    "available" => Color.FromArgb(16, 185, 129),
                    "inuse" => Color.FromArgb(139, 92, 246),
                    "maintenance" => Color.FromArgb(245, 158, 11),
                    "lost" => Color.FromArgb(239, 68, 68),
                    _ => Color.Gray
                };
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            lblTotalValue.Text = $"Total Equipment Value: ₱{totalVal:N2}";
        }

        private void dgvEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colActions.Index) return;

            var item = dgvEquipment.Rows[e.RowIndex].Tag as StudioInventoryItemDto;
            if (item == null) return;

            var menu = new ContextMenuStrip();
            menu.Items.Add("ℹ  Item Information", null, (s, args) =>
            {
                MessageBox.Show(
                    $"Item Code: {item.ItemCode}\n" +
                    $"Item Name: {item.ItemName}\n" +
                    $"Category: {item.CategoryName ?? "General"}\n" +
                    $"Stationed In: {_studio.StudioName}\n" +
                    $"Condition: {item.Condition}\n" +
                    $"Status: {item.Availability}\n" +
                    $"Quantity: {item.QuantityOnHand}\n" +
                    $"Unit Value: ₱{item.UnitCost:N2}\n" +
                    $"Total Value: ₱{(item.QuantityOnHand * item.UnitCost):N2}",
                    "Equipment Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            });

            var cellRect = dgvEquipment.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvEquipment, cellRect.Left, cellRect.Bottom);
        }

        private static string GetTypeName(int typeId) => typeId switch
        {
            1 => "Rehearsal",
            2 => "Recording",
            3 => "Vocal",
            4 => "Mixing",
            5 => "Mastering",
            _ => "General"
        };

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
