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
    public partial class RescheduleBookingForm : Form
    {
        private readonly BookingService _bookingService;
        private readonly int _companyId;
        private readonly BookingAdminDto _booking;

        public bool SavedSuccessfully { get; private set; } = false;

        private static readonly string[] TimeSlots = new[]
        {
            "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM",
            "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM",
            "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM"
        };

        public RescheduleBookingForm(ApiClient api, int companyId, BookingAdminDto booking)
        {
            InitializeComponent();
            _bookingService = new BookingService(api);
            _companyId = companyId;
            _booking = booking;

            lblSubtitle.Text = booking.BookingCode;
            this.Text = $"Reschedule — {booking.BookingCode}";
        }

        private void RescheduleBookingForm_Load(object sender, EventArgs e)
        {
            // Show current schedule
            lblCurrentValue.Text =
                $"{_booking.StartTime:MMM d, yyyy}  ·  " +
                $"{_booking.StartTime:hh:mm tt} - {_booking.EndTime:hh:mm tt}";

            // Populate time slot dropdowns
            foreach (var t in TimeSlots)
            {
                cmbNewStart.Items.Add(t);
                cmbNewEnd.Items.Add(t);
            }

            // Pre-select same day
            dtpNewDate.Value = _booking.StartTime.Date;

            // Pre-select current start time
            var startStr = _booking.StartTime.ToString("hh:mm tt");
            var startIdx = Array.IndexOf(TimeSlots, startStr);
            if (startIdx >= 0) cmbNewStart.SelectedIndex = startIdx;

            // Pre-select current end time
            var endStr = _booking.EndTime.ToString("hh:mm tt");
            var endIdx = Array.IndexOf(TimeSlots, endStr);
            if (endIdx >= 0) cmbNewEnd.SelectedIndex = endIdx;
        }

        // ==================== TIME SLOT AUTO-FILL ====================

        private void cmbNewStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Auto-select the next slot for End Time (1 hour after start)
            if (cmbNewStart.SelectedIndex >= 0 && cmbNewStart.SelectedIndex + 1 < TimeSlots.Length)
            {
                cmbNewEnd.SelectedIndex = cmbNewStart.SelectedIndex + 1;
            }
        }

        // ==================== CONFIRM ====================

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (cmbNewStart.SelectedIndex < 0)
            {
                ShowError("Please select a start time.");
                return;
            }
            if (cmbNewEnd.SelectedIndex < 0)
            {
                ShowError("Please select an end time.");
                return;
            }
            if (cmbNewEnd.SelectedIndex <= cmbNewStart.SelectedIndex)
            {
                ShowError("End time must be after start time.");
                return;
            }

            var date = dtpNewDate.Value.Date;
            var startTime = ParseTimeSlot(TimeSlots[cmbNewStart.SelectedIndex]);
            var endTime = ParseTimeSlot(TimeSlots[cmbNewEnd.SelectedIndex]);

            // Warn if the new slot is the same as the current
            var sameDate = date == _booking.StartTime.Date;
            var sameStart = TimeSlots[cmbNewStart.SelectedIndex] == _booking.StartTime.ToString("hh:mm tt");
            if (sameDate && sameStart)
            {
                var confirm = MessageBox.Show(
                    "The new schedule is the same as the current one.\n\nContinue anyway?",
                    "No Change",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }

            var request = new BookingUpdateRequest
            {
                StartTime = DateTime.SpecifyKind(date + startTime, DateTimeKind.Utc),
                EndTime = DateTime.SpecifyKind(date + endTime, DateTimeKind.Utc),
                BookingStatus = BookingStatuses.Rescheduled   // optional: mark as Rescheduled
            };

            btnConfirm.Enabled = false;
            btnConfirm.Text = "Rescheduling...";

            try
            {
                var result = await _bookingService.UpdateAsync(_companyId, _booking.BookingId, request);

                if (result == null)
                {
                    ShowError("Failed to reschedule booking. Server rejected the request.");
                    return;
                }

                SavedSuccessfully = true;

                MessageBox.Show(
                    $"Booking rescheduled successfully!\n\n" +
                    $"New Schedule:\n" +
                    $"{date:MMM d, yyyy}  ·  {TimeSlots[cmbNewStart.SelectedIndex]} - {TimeSlots[cmbNewEnd.SelectedIndex]}",
                    "Reschedule Confirmed",
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
                btnConfirm.Enabled = true;
                btnConfirm.Text = "Confirm Reschedule";
            }
        }

        // ==================== HELPERS ====================

        private static TimeSpan ParseTimeSlot(string slot)
        {
            return DateTime.Parse(slot).TimeOfDay;
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }
    }
}