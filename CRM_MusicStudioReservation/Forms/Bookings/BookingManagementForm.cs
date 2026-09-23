using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.DTOs;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Bookings
{
    public partial class BookingManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly BookingService _bookingService;

        private List<BookingAdminDto> _allBookings = new();
        private ActionButtonsColumn _actionsColumn;
        private bool _suppressFilterEvents = false;
        private bool _dateFiltersInitialized = false;
        private int _lastHoveredRowIndex = -1;

        private int? _preselectedBookingId;
        private bool _preselectionConsumed = false;

        public BookingManagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _bookingService = new BookingService(api);
        }

        private bool IsStaff()
        {
            var role = _auth.CurrentUser?.Role?.ToLowerInvariant() ?? "";
            return role == "staff";
        }

        // ==================== PUBLIC NAVIGATION API ====================

        public void PreselectBooking(int bookingId)
        {
            _preselectedBookingId = bookingId;
            _preselectionConsumed = false;

            TryApplyPreselection();
        }

        private void TryApplyPreselection()
        {
            if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;
            if (_preselectionConsumed) return;
            if (_preselectedBookingId == null) return;
            if (dgvBookings.Rows.Count == 0) return;

            var target = _allBookings.FirstOrDefault(b => b.BookingId == _preselectedBookingId.Value);
            if (target == null) return;

            var fromDate = dtpFrom.Value.Date;
            var toDate = dtpTo.Value.Date.AddDays(1);
            if (target.StartTime < fromDate || target.StartTime >= toDate)
            {
                _suppressFilterEvents = true;
                if (target.StartTime < fromDate) dtpFrom.Value = target.StartTime.Date.AddDays(-1);
                if (target.StartTime >= toDate) dtpTo.Value = target.StartTime.Date.AddDays(1);
                _suppressFilterEvents = false;

                ApplyFilters();
            }

            foreach (DataGridViewRow row in dgvBookings.Rows)
            {
                if (row.Tag is int id && id == _preselectedBookingId.Value)
                {
                    row.Selected = true;
                    if (dgvBookings.Columns.Contains("colStatus"))
                        dgvBookings.CurrentCell = row.Cells["colStatus"];
                    try
                    {
                        dgvBookings.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    catch { }

                    ShowDetails(target);

                    _preselectionConsumed = true;
                    _preselectedBookingId = null;
                    return;
                }
            }
        }

        // ==================== LOAD ====================

        private async void BookingManagementForm_Load(object sender, EventArgs e)
        {
            SetupActionsColumn();
            SetupFilters();
            await LoadBookingsAsync();
        }

        private void SetupActionsColumn()
        {
            if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;

            _actionsColumn = new ActionButtonsColumn();
            dgvBookings.Columns.Add(_actionsColumn);

            dgvBookings.CellMouseClick += DgvBookings_CellMouseClick;
            dgvBookings.CellMouseMove += DgvBookings_CellMouseMove;
            dgvBookings.CellMouseLeave += DgvBookings_CellMouseLeave;
        }

        private void SetupFilters()
        {
            if (this.IsDisposed) return;

            _suppressFilterEvents = true;

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today.AddDays(30);
            _dateFiltersInitialized = true;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Statuses");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Confirmed");
            cmbStatus.Items.Add("Checked In");
            cmbStatus.Items.Add("Checked Out");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.Items.Add("Rescheduled");
            cmbStatus.SelectedIndex = 0;

            _suppressFilterEvents = false;
        }

        private async System.Threading.Tasks.Task LoadBookingsAsync()
        {
            try
            {
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var bookings = await _bookingService.GetAllAsync(companyId);

                // 👇 GUARD — bail if form was disposed while waiting on the API
                if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;
                if (dgvBookings.Columns.Count == 0) return;

                _allBookings = bookings ?? new List<BookingAdminDto>();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                if (this.IsDisposed) return;

                _allBookings = new List<BookingAdminDto>();
                ApplyFilters();
                MessageBox.Show(
                    $"Failed to load bookings: {ex.Message}",
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==================== FILTERS ====================

        private void ApplyFilters()
        {
            if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var statusFilter = cmbStatus.SelectedItem?.ToString() ?? "All Statuses";
            var fromDate = dtpFrom.Value.Date;
            var toDate = dtpTo.Value.Date.AddDays(1);

            var filtered = _allBookings.AsEnumerable();

            filtered = filtered.Where(b =>
                b.StartTime >= fromDate && b.StartTime < toDate);

            if (statusFilter != "All Statuses")
                filtered = filtered.Where(b => BookingStatuses.GetName(b.BookingStatus) == statusFilter);

            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(b =>
                    (b.BookingCode ?? "").ToLowerInvariant().Contains(search));

            var list = filtered.OrderByDescending(b => b.StartTime).ToList();
            RenderRows(list);

            if (!lblCount.IsDisposed)
                lblCount.Text = $"{list.Count} of {_allBookings.Count} booking(s)";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (!_dateFiltersInitialized) return;
            ApplyFilters();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (!_dateFiltersInitialized) return;
            ApplyFilters();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;
            await LoadBookingsAsync();
        }

        // ==================== RENDER ====================

        private void RenderRows(List<BookingAdminDto> bookings)
        {
            if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;
            if (dgvBookings.Columns.Count == 0) return;   // 👈 safety

            dgvBookings.Rows.Clear();
            var staff = IsStaff();

            if (_actionsColumn == null || _actionsColumn.Index < 0)
                return;

            foreach (var b in bookings)
            {
                var statusText = BookingStatuses.GetName(b.BookingStatus);

                var isPending = b.BookingStatus == BookingStatuses.Pending;
                var isRescheduled = b.BookingStatus == BookingStatuses.Rescheduled;
                var isConfirmed = b.BookingStatus == BookingStatuses.Confirmed;
                var isCheckedIn = b.BookingStatus == BookingStatuses.CheckedIn;
                var isCheckedOut = b.BookingStatus == BookingStatuses.CheckedOut;
                var isCancelled = b.BookingStatus == BookingStatuses.Cancelled;

                var showEdit = isPending || isRescheduled || isConfirmed;
                var showConfirm = isPending || isRescheduled;
                var showCheckIn = isConfirmed && staff;
                var showCheckOut = isCheckedIn && staff;
                var showDelete = isPending || isRescheduled || isConfirmed;
                var showView = isCheckedIn || isCheckedOut || isCancelled;

                var idx = dgvBookings.Rows.Add(
                    b.BookingId,
                    b.BookingCode,
                    $"Customer {b.CustomerId}",
                    $"Studio {b.StudioId}",
                    b.StartTime.ToString("MMM d, hh:mm tt"),
                    $"₱{b.TotalAmount:N2}",
                    statusText
                );

                if (dgvBookings.Columns.Contains("colStatus"))
                {
                    var statusCell = dgvBookings.Rows[idx].Cells["colStatus"];
                    statusCell.Style.ForeColor = GetStatusColor(b.BookingStatus);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }

                var buttons = new List<ActionButtonInfo>();

                if (showEdit)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "edit",
                        Icon = "✏️",
                        Label = "Edit",
                        BackgroundColor = Color.FromArgb(139, 92, 246)
                    });

                if (showConfirm)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "confirm",
                        Icon = "✓",
                        Label = "Confirm",
                        BackgroundColor = Color.FromArgb(16, 185, 129)
                    });

                if (showCheckIn)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "checkin",
                        Icon = "▶",
                        Label = "Check-In",
                        BackgroundColor = Color.FromArgb(59, 130, 246)
                    });

                if (showCheckOut)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "checkout",
                        Icon = "■",
                        Label = "Check-Out",
                        BackgroundColor = Color.FromArgb(245, 158, 11)
                    });

                if (showDelete)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "delete",
                        Icon = "🗑️",
                        Label = "Delete",
                        BackgroundColor = Color.FromArgb(239, 68, 68)
                    });

                if (showView)
                    buttons.Add(new ActionButtonInfo
                    {
                        ActionKey = "view",
                        Icon = "👁",
                        Label = "View",
                        BackgroundColor = Color.FromArgb(167, 139, 250)
                    });

                dgvBookings.Rows[idx].Cells[_actionsColumn.Index].Value = buttons;
                dgvBookings.Rows[idx].Tag = b.BookingId;
            }

            TryApplyPreselection();
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

        // ==================== ACTION CLICKS ====================

        private async void DgvBookings_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0 || _actionsColumn == null || e.ColumnIndex != _actionsColumn.Index) return;

            var cell = dgvBookings.Rows[e.RowIndex].Cells[e.ColumnIndex] as ActionButtonsCell;
            if (cell == null) return;

            var buttons = cell.GetButtons();
            if (buttons == null || buttons.Count == 0) return;

            var cellRect = dgvBookings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int gridX = cellRect.X + e.X;
            int gridY = cellRect.Y + e.Y;

            string clickedKey = null;
            foreach (var btn in buttons)
            {
                if (btn.Bounds.Contains(gridX, gridY))
                {
                    clickedKey = btn.ActionKey;
                    break;
                }
            }

            if (string.IsNullOrEmpty(clickedKey)) return;

            var bookingId = (int)(dgvBookings.Rows[e.RowIndex].Tag ?? 0);
            var booking = _allBookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null) return;

            switch (clickedKey)
            {
                case "edit":
                    await OpenEditAsync(booking);
                    break;
                case "confirm":
                    await ChangeStatusAsync(booking, BookingStatuses.Confirmed, "confirm");
                    break;
                case "checkin":
                    await ChangeStatusAsync(booking, BookingStatuses.CheckedIn, "check in");
                    break;
                case "checkout":
                    await ChangeStatusAsync(booking, BookingStatuses.CheckedOut, "check out");
                    break;
                case "delete":
                    await ArchiveBookingAsync(booking);
                    break;
                case "view":
                    ShowDetails(booking);
                    break;
            }
        }

        private void DgvBookings_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0 || _actionsColumn == null || e.ColumnIndex != _actionsColumn.Index)
            {
                ClearHoverOnPreviousRow();
                return;
            }

            var cell = dgvBookings.Rows[e.RowIndex].Cells[e.ColumnIndex] as ActionButtonsCell;
            if (cell == null) return;

            var buttons = cell.GetButtons();
            if (buttons == null) return;

            var cellRect = dgvBookings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int gridX = cellRect.X + e.X;
            int gridY = cellRect.Y + e.Y;

            bool changed = false;

            foreach (var btn in buttons)
            {
                bool isHovered = btn.Bounds.Contains(gridX, gridY);
                if (btn.IsHovered != isHovered)
                {
                    btn.IsHovered = isHovered;
                    changed = true;
                }
            }

            if (_lastHoveredRowIndex >= 0 && _lastHoveredRowIndex != e.RowIndex)
            {
                var prevCell = dgvBookings.Rows[_lastHoveredRowIndex].Cells[_actionsColumn.Index] as ActionButtonsCell;
                var prevButtons = prevCell?.GetButtons();
                if (prevButtons != null)
                {
                    foreach (var b in prevButtons)
                    {
                        if (b.IsHovered)
                        {
                            b.IsHovered = false;
                            changed = true;
                        }
                    }
                }
            }

            _lastHoveredRowIndex = e.RowIndex;

            if (changed)
                dgvBookings.InvalidateCell(cell);
        }

        private void DgvBookings_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0 || _actionsColumn == null || e.ColumnIndex != _actionsColumn.Index) return;

            var cell = dgvBookings.Rows[e.RowIndex].Cells[e.ColumnIndex] as ActionButtonsCell;
            if (cell == null) return;

            var buttons = cell.GetButtons();
            if (buttons == null) return;

            bool changed = false;

            foreach (var btn in buttons)
            {
                if (btn.IsHovered)
                {
                    btn.IsHovered = false;
                    changed = true;
                }
            }

            _lastHoveredRowIndex = -1;

            if (changed)
                dgvBookings.InvalidateCell(cell);
        }

        private void ClearHoverOnPreviousRow()
        {
            if (this.IsDisposed) return;
            if (_lastHoveredRowIndex < 0) return;
            if (_actionsColumn == null) return;

            var prevCell = dgvBookings.Rows[_lastHoveredRowIndex].Cells[_actionsColumn.Index] as ActionButtonsCell;
            var prevButtons = prevCell?.GetButtons();
            if (prevButtons != null)
            {
                bool changed = false;
                foreach (var b in prevButtons)
                {
                    if (b.IsHovered)
                    {
                        b.IsHovered = false;
                        changed = true;
                    }
                }
                if (changed)
                    dgvBookings.InvalidateCell(prevCell);
            }

            _lastHoveredRowIndex = -1;
        }

        // ==================== TOP BUTTONS ====================

        private async void btnReschedule_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            using var picker = new ReschedulePickerForm(_auth, _api);
            picker.ShowDialog(this);

            if (this.IsDisposed) return;

            if (picker.RescheduledSomething)
                await LoadBookingsAsync();
        }

        private async void btnNewBooking_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new EditBookingForm(_api, companyId, null);   // 👈 null = create mode
            dialog.ShowDialog(this);

            if (dialog.SavedSuccessfully)
                await LoadBookingsAsync();
        }

        // ==================== DIALOGS ====================

        private async System.Threading.Tasks.Task OpenEditAsync(BookingAdminDto booking)
        {
            if (this.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new EditBookingForm(_api, companyId, booking);
            dialog.ShowDialog(this);

            if (this.IsDisposed) return;

            if (dialog.SavedSuccessfully)
                await LoadBookingsAsync();
        }

        private async System.Threading.Tasks.Task ChangeStatusAsync(BookingAdminDto booking, int newStatus, string verb)
        {
            if (this.IsDisposed) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to {verb} booking '{booking.BookingCode}'?",
                $"Confirm {verb}",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var request = new BookingUpdateRequest { BookingStatus = newStatus };
            var result = await _bookingService.UpdateAsync(companyId, booking.BookingId, request);

            if (this.IsDisposed) return;

            if (result != null)
            {
                var pastTense = verb switch
                {
                    "confirm" => "confirmed",
                    "check in" => "checked in",
                    "check out" => "checked out",
                    _ => verb + "ed"
                };

                MessageBox.Show($"Booking {pastTense} successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBookingsAsync();
            }
            else
            {
                MessageBox.Show($"Failed to {verb} booking.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task ArchiveBookingAsync(BookingAdminDto booking)
        {
            if (this.IsDisposed) return;

            var confirm = MessageBox.Show(
                $"Delete (archive) booking '{booking.BookingCode}'?\n\n" +
                "The booking will be marked as Cancelled and kept in the database.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var request = new BookingUpdateRequest { BookingStatus = BookingStatuses.Cancelled };
            var result = await _bookingService.UpdateAsync(companyId, booking.BookingId, request);

            if (this.IsDisposed) return;

            if (result != null)
            {
                MessageBox.Show("Booking archived successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBookingsAsync();
            }
            else
            {
                MessageBox.Show("Failed to archive booking.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetails(BookingAdminDto b)
        {
            if (this.IsDisposed) return;

            MessageBox.Show(
                $"Booking Code: {b.BookingCode}\n" +
                $"Customer ID: {b.CustomerId}\n" +
                $"Studio ID: {b.StudioId}\n" +
                $"Start: {b.StartTime:MMM d, yyyy hh:mm tt}\n" +
                $"End: {b.EndTime:hh:mm tt}\n" +
                $"Amount: ₱{b.TotalAmount:N2}\n" +
                $"Status: {BookingStatuses.GetName(b.BookingStatus)}\n" +
                $"Check-In: {(b.CheckInTime.HasValue ? b.CheckInTime.Value.ToString("MMM d hh:mm tt") : "—")}\n" +
                $"Check-Out: {(b.CheckOutTime.HasValue ? b.CheckOutTime.Value.ToString("MMM d hh:mm tt") : "—")}\n" +
                $"Notes: {b.Notes ?? "(none)"}",
                "Booking Details",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}