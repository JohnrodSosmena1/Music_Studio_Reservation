using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Bookings
{
    public partial class EditBookingForm : Form
    {
        private readonly BookingService _bookingService;
        private readonly CustomerService _customerService;
        private readonly StudioService _studioService;
        private readonly int _companyId;
        private readonly BookingAdminDto? _booking;   // null = Create mode

        public bool SavedSuccessfully { get; private set; } = false;

        private bool IsCreateMode => _booking == null;

        // Time slots (whole hours from 9 AM to 9 PM)
        private static readonly string[] TimeSlots = new[]
        {
            "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM",
            "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM",
            "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM"
        };

        // Booking statuses for the dropdown
        private static readonly (int Id, string Name)[] Statuses =
        {
            (BookingStatuses.Pending, "Pending"),
            (BookingStatuses.Confirmed, "Confirmed"),
            (BookingStatuses.CheckedIn, "Checked In"),
            (BookingStatuses.CheckedOut, "Checked Out"),
            (BookingStatuses.Cancelled, "Cancelled"),
            (BookingStatuses.Rescheduled, "Rescheduled")
        };

        private List<CustomerDto> _customers = new();
        private List<StudioDto> _studios = new();

        public EditBookingForm(ApiClient api, int companyId, BookingAdminDto? booking)
        {
            InitializeComponent();
            _bookingService = new BookingService(api);
            _customerService = new CustomerService(api);
            _studioService = new StudioService(api);
            _companyId = companyId;
            _booking = booking;

            if (IsCreateMode)
            {
                lblTitle.Text = "New Booking";
                lblBookingCode.Text = "Auto-generated on save";
                this.Text = "New Booking";
            }
            else
            {
                lblTitle.Text = "Edit Booking";
                lblBookingCode.Text = booking!.BookingCode;
                this.Text = $"Edit Booking — {booking.BookingCode}";
            }
        }

        private async void EditBookingForm_Load(object sender, EventArgs e)
        {
            foreach (var s in Statuses)
                cmbStatus.Items.Add(s.Name);

            foreach (var t in TimeSlots)
            {
                cmbStartTime.Items.Add(t);
                cmbEndTime.Items.Add(t);
            }

            await LoadLookupsAsync();

            if (IsCreateMode)
            {
                dtpDate.Value = DateTime.Today;
                if (cmbCustomer.Items.Count > 0) cmbCustomer.SelectedIndex = 0;
                if (cmbStudio.Items.Count > 0) cmbStudio.SelectedIndex = 0;
                cmbStartTime.SelectedIndex = 0;
                cmbEndTime.SelectedIndex = 1;
                cmbStatus.SelectedIndex = 0;
            }
            else
            {
                ApplyBookingValues();
            }
        }

        private async System.Threading.Tasks.Task LoadLookupsAsync()
        {
            _customers = await _customerService.GetAllAsync(_companyId);
            _studios = await _studioService.GetAllAsync(_companyId);

            cmbCustomer.Items.Clear();
            foreach (var c in _customers)
                cmbCustomer.Items.Add($"{c.CustomerCode} — {c.CustomerName}");

            cmbStudio.Items.Clear();
            foreach (var s in _studios)
                cmbStudio.Items.Add($"{s.StudioCode} — {s.StudioName}");
        }

        private void ApplyBookingValues()
        {
            if (_booking == null) return;

            var custIdx = _customers.FindIndex(c => c.CustomerId == _booking.CustomerId);
            if (custIdx >= 0) cmbCustomer.SelectedIndex = custIdx;

            var studioIdx = _studios.FindIndex(s => s.StudioId == _booking.StudioId);
            if (studioIdx >= 0) cmbStudio.SelectedIndex = studioIdx;

            dtpDate.Value = _booking.StartTime.Date;

            var startStr = _booking.StartTime.ToString("hh:mm tt");
            var startIdx = Array.IndexOf(TimeSlots, startStr);
            if (startIdx >= 0) cmbStartTime.SelectedIndex = startIdx;

            var endStr = _booking.EndTime.ToString("hh:mm tt");
            var endIdx = Array.IndexOf(TimeSlots, endStr);
            if (endIdx >= 0) cmbEndTime.SelectedIndex = endIdx;

            var statusIdx = Array.FindIndex(Statuses, s => s.Id == _booking.BookingStatus);
            if (statusIdx >= 0) cmbStatus.SelectedIndex = statusIdx;

            txtNotes.Text = _booking.Notes ?? "";
        }

        private void cmbStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStartTime.SelectedIndex >= 0 && cmbStartTime.SelectedIndex + 1 < TimeSlots.Length)
            {
                cmbEndTime.SelectedIndex = cmbStartTime.SelectedIndex + 1;
            }
        }

        // ==================== SAVE (Create or Update) ====================

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (cmbCustomer.SelectedIndex < 0)
            {
                ShowError("Please select a customer.");
                return;
            }
            if (cmbStudio.SelectedIndex < 0)
            {
                ShowError("Please select a studio.");
                return;
            }
            if (cmbStartTime.SelectedIndex < 0)
            {
                ShowError("Please select a start time.");
                return;
            }
            if (cmbEndTime.SelectedIndex < 0)
            {
                ShowError("Please select an end time.");
                return;
            }
            if (cmbEndTime.SelectedIndex <= cmbStartTime.SelectedIndex)
            {
                ShowError("End time must be after start time.");
                return;
            }

            var customerId = _customers[cmbCustomer.SelectedIndex].CustomerId;
            var studioId = _studios[cmbStudio.SelectedIndex].StudioId;
            var date = dtpDate.Value.Date;
            var startTime = ParseTimeSlot(TimeSlots[cmbStartTime.SelectedIndex]);
            var endTime = ParseTimeSlot(TimeSlots[cmbEndTime.SelectedIndex]);
            var status = Statuses[cmbStatus.SelectedIndex].Id;
            var notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                if (IsCreateMode)
                {
                    // 👇 CREATE — BookingCreateRequest has NO Services property in your DTO
                    var request = new BookingCreateRequest
                    {
                        CustomerId = customerId,
                        StudioId = studioId,
                        StartTime = DateTime.SpecifyKind(date + startTime, DateTimeKind.Utc),
                        EndTime = DateTime.SpecifyKind(date + endTime, DateTimeKind.Utc),
                        Notes = notes
                    };

                    var created = await _bookingService.CreateAsync(_companyId, request);

                    if (created == null)
                    {
                        ShowError("Failed to create booking. Please check the details and try again.");
                        return;
                    }
                }
                else
                {
                    // 👇 UPDATE
                    var request = new BookingUpdateRequest
                    {
                        CustomerId = customerId,
                        StudioId = studioId,
                        StartTime = DateTime.SpecifyKind(date + startTime, DateTimeKind.Utc),
                        EndTime = DateTime.SpecifyKind(date + endTime, DateTimeKind.Utc),
                        BookingStatus = status,
                        Notes = notes
                    };

                    var updated = await _bookingService.UpdateAsync(_companyId, _booking!.BookingId, request);

                    if (updated == null)
                    {
                        ShowError("Failed to update booking. Server rejected the request.");
                        return;
                    }
                }

                SavedSuccessfully = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }
        }

        // ==================== HELPERS ====================

        private static TimeSpan ParseTimeSlot(string slot)
        {
            return DateTime.Parse(slot).TimeOfDay;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }
    }
}