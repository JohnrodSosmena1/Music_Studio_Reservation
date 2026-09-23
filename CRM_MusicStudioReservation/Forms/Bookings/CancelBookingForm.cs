using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Bookings
{
    public partial class CancelBookingForm : Form
    {
        private readonly BookingService _bookingService;
        private readonly int _companyId;
        private readonly BookingAdminDto _booking;

        public bool CancelledSuccessfully { get; private set; } = false;

        public CancelBookingForm(ApiClient api, int companyId, BookingAdminDto booking)
        {
            InitializeComponent();
            _bookingService = new BookingService(api);
            _companyId = companyId;
            _booking = booking;

            lblSubtitle.Text = booking.BookingCode;
            this.Text = $"Cancel Booking — {booking.BookingCode}";
        }

        // ==================== ACTIONS ====================

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            var reason = txtReason.Text.Trim();

            var request = new BookingUpdateRequest
            {
                BookingStatus = BookingStatuses.Cancelled,
                Notes = string.IsNullOrWhiteSpace(reason)
                    ? _booking.Notes  // keep existing notes if no reason
                    : $"[CANCELLED: {reason}] {_booking.Notes ?? ""}".Trim()
            };

            btnCancel.Enabled = false;
            btnCancel.Text = "Cancelling...";

            try
            {
                var result = await _bookingService.UpdateAsync(_companyId, _booking.BookingId, request);

                if (result == null)
                {
                    ShowError("Failed to cancel booking. Server rejected the request.");
                    return;
                }

                CancelledSuccessfully = true;

                MessageBox.Show(
                    "Booking cancelled successfully.",
                    "Cancelled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
            }
            finally
            {
                btnCancel.Enabled = true;
                btnCancel.Text = "Yes, Cancel Booking";
            }
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }
    }
}
