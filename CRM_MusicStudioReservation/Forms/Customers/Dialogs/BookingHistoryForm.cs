using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM.winforms.Forms.Customers.Dialogs
{
    public partial class BookingHistoryForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly CustomerLoyaltyDto _customer;
        private readonly BookingService _bookingService;

        private List<BookingAdminDto> _bookings = new();

        public BookingHistoryForm(AuthService auth, ApiClient api, CustomerLoyaltyDto customer)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _customer = customer;
            _bookingService = new BookingService(api);
        }

        private async void BookingHistoryForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = _customer.CustomerName;
            lblCustomerCode.Text = _customer.CustomerCode;
            lblPlanSummary.Text = string.IsNullOrEmpty(_customer.PlanName)
                ? "No membership plan"
                : $"{_customer.PlanName}  ·  {_customer.PointsPerBooking} pts/booking";

            EnsureGridColumns();
            await LoadBookingsAsync();
        }

        // ==================== GRID SETUP ====================

        private void EnsureGridColumns()
        {
            if (dgvHistory == null || dgvHistory.IsDisposed) return;
            if (dgvHistory.Columns.Count > 0) return;

            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 40 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Booking Code", FillWeight = 110 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStudio", HeaderText = "Studio", FillWeight = 60 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStart", HeaderText = "Start", FillWeight = 110 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAmount", HeaderText = "Amount", FillWeight = 70 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", FillWeight = 80 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPoints", HeaderText = "Points Earned", FillWeight = 80 });
        }

        // ==================== LOADING ====================

        private async System.Threading.Tasks.Task LoadBookingsAsync()
        {
            if (this.IsDisposed || dgvHistory.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            // Fetch ALL bookings, then filter by this customer
            var allBookings = await _bookingService.GetAllAsync(companyId);

            if (this.IsDisposed || dgvHistory.IsDisposed) return;

            _bookings = (allBookings ?? new List<BookingAdminDto>())
                .Where(b => b.CustomerId == _customer.CustomerId)
                .OrderByDescending(b => b.StartTime)
                .ToList();

            RenderRows();
        }

        private void RenderRows()
        {
            if (this.IsDisposed || dgvHistory.IsDisposed) return;
            dgvHistory.Rows.Clear();

            int totalPoints = 0;

            foreach (var b in _bookings)
            {
                var statusText = BookingStatuses.GetName(b.BookingStatus);
                var isCancelled = b.BookingStatus == BookingStatuses.Cancelled;

                // Points: only awarded for non-cancelled bookings
                var pointsEarned = isCancelled ? 0 : _customer.PointsPerBooking;
                totalPoints += pointsEarned;

                var idx = dgvHistory.Rows.Add(
                    b.BookingId,
                    b.BookingCode,
                    $"Studio {b.StudioId}",
                    b.StartTime.ToString("MMM d, yyyy hh:mm tt"),
                    $"₱{b.TotalAmount:N2}",
                    statusText,
                    isCancelled ? "—" : $"+{pointsEarned}"
                );

                var row = dgvHistory.Rows[idx];

                // Color status
                var statusCell = row.Cells["colStatus"];
                statusCell.Style.ForeColor = GetStatusColor(b.BookingStatus);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                // Color points
                var pointsCell = row.Cells["colPoints"];
                if (isCancelled)
                {
                    pointsCell.Style.ForeColor = Color.FromArgb(107, 114, 128);
                }
                else
                {
                    pointsCell.Style.ForeColor = Color.FromArgb(139, 92, 246);
                    pointsCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }

            lblCount.Text = $"{_bookings.Count} booking(s)  ·  {totalPoints} total points earned";
        }

        private static Color GetStatusColor(int status) => status switch
        {
            BookingStatuses.Pending => Color.FromArgb(245, 158, 11),
            BookingStatuses.Confirmed => Color.FromArgb(16, 185, 129),
            BookingStatuses.CheckedIn => Color.FromArgb(59, 130, 246),
            BookingStatuses.CheckedOut => Color.FromArgb(107, 114, 128),
            BookingStatuses.Cancelled => Color.FromArgb(239, 68, 68),
            BookingStatuses.Rescheduled => Color.FromArgb(139, 92, 246),
            _ => Color.Gray
        };

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}