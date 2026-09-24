using CRM.winforms.DTOs;
using CRM.winforms.Forms.Customers.Dialogs;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Customers.Tabs
{
    public partial class LoyaltyPointsTab : System.Windows.Forms.UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerService _customerService;

        private List<CustomerLoyaltyDto> _allItems = new();

        public LoyaltyPointsTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customerService = new CustomerService(api);
        }

        private async void LoyaltyPointsTab_Load(object sender, EventArgs e)
        {
            EnsureGridColumns();
            await LoadAsync();
        }

        // ==================== SAFETY: ensure grid columns exist ====================

        private void EnsureGridColumns()
        {
            if (dgvLoyalty == null || dgvLoyalty.IsDisposed) return;
            if (dgvLoyalty.Columns.Count > 0) return;

            dgvLoyalty.Columns.Clear();

            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 40 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Code", FillWeight = 80 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Customer", FillWeight = 140 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPlan", HeaderText = "Plan", FillWeight = 90 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBookings", HeaderText = "Total Bookings", FillWeight = 90 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRate", HeaderText = "Points/Booking", FillWeight = 100 });
            dgvLoyalty.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPoints", HeaderText = "Total Points", FillWeight = 90 });

            var actionsCol = new DataGridViewButtonColumn
            {
                Name = "colActions",
                HeaderText = "Actions",
                FillWeight = 90,
                FlatStyle = FlatStyle.Flat
            };
            actionsCol.DefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            actionsCol.DefaultCellStyle.ForeColor = Color.FromArgb(139, 92, 246);
            actionsCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 233, 254);
            actionsCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvLoyalty.Columns.Add(actionsCol);
        }

        // ==================== LOADING ====================

        private async System.Threading.Tasks.Task LoadAsync()
        {
            if (this.IsDisposed || dgvLoyalty.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            _allItems = await _customerService.GetLoyaltyAsync(companyId);

            if (this.IsDisposed || dgvLoyalty.IsDisposed) return;
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (this.IsDisposed || dgvLoyalty.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var list = _allItems.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(x =>
                    (x.CustomerName ?? "").ToLowerInvariant().Contains(search) ||
                    (x.CustomerCode ?? "").ToLowerInvariant().Contains(search));
            }

            RenderRows(list.ToList());
        }

        private void RenderRows(List<CustomerLoyaltyDto> items)
        {
            if (this.IsDisposed || dgvLoyalty.IsDisposed) return;
            dgvLoyalty.Rows.Clear();

            foreach (var c in items)
            {
                var idx = dgvLoyalty.Rows.Add(
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    c.PlanDisplay,
                    c.TotalBookings,
                    c.PointsPerBookingDisplay,
                    c.TotalPointsEarned,
                    "⋯ Actions"
                );

                var row = dgvLoyalty.Rows[idx];
                row.Tag = c;

                // Color the plan cell
                var planCell = row.Cells["colPlan"];
                if (string.IsNullOrEmpty(c.PlanName))
                {
                    planCell.Style.ForeColor = Color.FromArgb(107, 114, 128);
                }
                else
                {
                    planCell.Style.ForeColor = GetPlanColor(c.PlanName);
                    planCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }

                // Highlight points
                var pointsCell = row.Cells["colPoints"];
                pointsCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                pointsCell.Style.ForeColor = Color.FromArgb(139, 92, 246);
            }

            lblCount.Text = $"{items.Count} customer(s)";
        }

        private static Color GetPlanColor(string planName) => planName?.ToLowerInvariant() switch
        {
            "bronze" => Color.FromArgb(180, 83, 9),
            "silver" => Color.FromArgb(107, 114, 128),
            "gold" => Color.FromArgb(202, 138, 4),
            "platinum" => Color.FromArgb(139, 92, 246),
            "diamond" => Color.FromArgb(59, 130, 246),
            _ => Color.FromArgb(139, 92, 246)
        };

        // ==================== EVENTS ====================

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadAsync();
        }

        private void dgvLoyalty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != dgvLoyalty.Columns["colActions"].Index) return;

            var customer = dgvLoyalty.Rows[e.RowIndex].Tag as CustomerLoyaltyDto;
            if (customer == null) return;

            var menu = new ContextMenuStrip();

            // -------- View Booking History (always) --------
            menu.Items.Add("👁  View Booking History", null, (s, args) =>
            {
                var dialog = new BookingHistoryForm(_auth, _api, customer);
                dialog.ShowDialog(this.FindForm());
            });

            // -------- Enroll / Already Enrolled --------
            if (string.IsNullOrEmpty(customer.PlanName))
            {
                menu.Items.Add("🏆  Enroll in Plan", null, async (s, args) =>
                {
                    var dialog = new EnrollInPlanForm(_auth, _api, customer);
                    dialog.ShowDialog(this.FindForm());

                    if (dialog.EnrolledSuccessfully)
                        await LoadAsync();
                });
            }
            else
            {
                menu.Items.Add($"🏆  Currently: {customer.PlanName}", null, (s, args) =>
                {
                    MessageBox.Show(
                        $"{customer.CustomerName} is already enrolled in the {customer.PlanName} plan.\n\n" +
                        "To change plans, first cancel the current membership from the Engagement → Loyalty tab.",
                        "Already Enrolled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }

            // -------- Adjust Points (members only) --------
            menu.Items.Add(new ToolStripSeparator());

            if (customer.MembershipId.HasValue)
            {
                menu.Items.Add("⚙  Adjust Points", null, async (s, args) =>
                {
                    var dialog = new AdjustPointsForm(_auth, _api, customer);
                    dialog.ShowDialog(this.FindForm());

                    if (dialog.AdjustmentSaved)
                        await LoadAsync();
                });
            }
            else
            {
                var disabled = new ToolStripMenuItem("⚙  Adjust Points (must be enrolled)")
                {
                    Enabled = false,
                    ForeColor = Color.FromArgb(180, 180, 180)
                };
                menu.Items.Add(disabled);
            }

            var cellRect = dgvLoyalty.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvLoyalty, cellRect.Left, cellRect.Bottom);
        }
    }
}