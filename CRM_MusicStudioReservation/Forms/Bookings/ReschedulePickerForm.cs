using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Bookings
{
    public partial class ReschedulePickerForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly BookingService _bookingService;

        private List<BookingAdminDto> _allBookings = new();

        /// <summary>True if a reschedule was completed.</summary>
        public bool RescheduledSomething { get; private set; } = false;

        public ReschedulePickerForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _bookingService = new BookingService(api);

            this.Load += (s, e) => LoadBookingsAsync();
        }

        // ==================== LOAD ====================

        private async System.Threading.Tasks.Task LoadBookingsAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            _allBookings = await _bookingService.GetAllAsync(companyId);

            // Only show bookings that CAN be rescheduled
            // (not Cancelled, not CheckedIn, not CheckedOut)
            var reschedulable = _allBookings
                .Where(b => b.BookingStatus == BookingStatuses.Confirmed
                         || b.BookingStatus == BookingStatuses.Rescheduled)
                .ToList();

            RenderRows(reschedulable);
        }

        private void RenderRows(List<BookingAdminDto> bookings)
        {
            dgvBookings.Rows.Clear();

            foreach (var b in bookings)
            {
                var idx = dgvBookings.Rows.Add(
                    b.BookingId,
                    b.BookingCode,
                    $"Customer {b.CustomerId}",
                    $"Studio {b.StudioId}",
                    b.StartTime.ToString("MMM d, yyyy hh:mm tt"),
                    BookingStatuses.GetName(b.BookingStatus)
                );

                var statusCell = dgvBookings.Rows[idx].Cells[colStatus.Index];
                statusCell.Style.ForeColor = GetStatusColor(b.BookingStatus);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                dgvBookings.Rows[idx].Tag = b.BookingId;
            }
        }

        private static Color GetStatusColor(int status) => status switch
        {
            BookingStatuses.Confirmed => Color.FromArgb(16, 185, 129),
            BookingStatuses.Rescheduled => Color.FromArgb(139, 92, 246),
            _ => Color.Gray
        };

        // ==================== FILTER ====================

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLowerInvariant();

            var reschedulable = _allBookings
                .Where(b => b.BookingStatus == BookingStatuses.Confirmed
                         || b.BookingStatus == BookingStatuses.Rescheduled);

            if (!string.IsNullOrEmpty(search))
            {
                reschedulable = reschedulable.Where(b =>
                    b.BookingCode.ToLowerInvariant().Contains(search));
            }

            RenderRows(reschedulable.ToList());
        }

        // ==================== ACTIONS ====================

        private void dgvBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            TriggerReschedule();
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            TriggerReschedule();
        }

        private void TriggerReschedule()
        {
            if (dgvBookings.SelectedRows.Count == 0)
            {
                ShowError("Please select a booking to reschedule.");
                return;
            }

            var row = dgvBookings.SelectedRows[0];
            var bookingId = (int)(row.Tag ?? 0);

            var booking = _allBookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                ShowError("Booking not found.");
                return;
            }

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            using var dialog = new RescheduleBookingForm(_api, companyId, booking);
            dialog.ShowDialog(this);

            if (dialog.SavedSuccessfully)
            {
                RescheduledSomething = true;
                this.Close();
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