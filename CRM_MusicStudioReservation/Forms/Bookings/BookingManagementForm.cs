using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        private bool _suppressFilterEvents = false;
        private bool _dateFiltersInitialized = false;

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

        // ==================== SAFETY: ensure grid columns exist ====================

        private void EnsureGridColumns()
        {
            if (dgvBookings == null || dgvBookings.IsDisposed) return;
            if (dgvBookings.Columns.Count > 0) return;

            dgvBookings.Columns.Clear();

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 30 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Code", FillWeight = 80 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCustomer", HeaderText = "Client Name", FillWeight = 90 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStudio", HeaderText = "Studio", FillWeight = 65 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStart", HeaderText = "Start", FillWeight = 100 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAmount", HeaderText = "Amount", FillWeight = 60 });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", FillWeight = 70 });

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
            dgvBookings.Columns.Add(actionsCol);
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
                    try { dgvBookings.FirstDisplayedScrollingRowIndex = row.Index; } catch { }

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
            EnsureGridColumns();       // 👈 mirror Inventory
            SetupFilters();
            await LoadBookingsAsync();
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

                if (this.IsDisposed || dgvBookings == null || dgvBookings.IsDisposed) return;
                if (dgvBookings.Columns.Count == 0) EnsureGridColumns();

                _allBookings = bookings ?? new List<BookingAdminDto>();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                if (this.IsDisposed) return;
                _allBookings = new List<BookingAdminDto>();
                ApplyFilters();
                MessageBox.Show($"Failed to load bookings: {ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            filtered = filtered.Where(b => b.StartTime >= fromDate && b.StartTime < toDate);

            if (statusFilter != "All Statuses")
                filtered = filtered.Where(b => BookingStatuses.GetName(b.BookingStatus) == statusFilter);

            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(b => (b.BookingCode ?? "").ToLowerInvariant().Contains(search));

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
            if (dgvBookings.Columns.Count == 0) EnsureGridColumns();
            if (dgvBookings.Columns.Count == 0) return;

            dgvBookings.Rows.Clear();

            foreach (var b in bookings)
            {
                var statusText = BookingStatuses.GetName(b.BookingStatus);

                var idx = dgvBookings.Rows.Add(
                    b.BookingId,
                    b.BookingCode,
                    $"Customer {b.CustomerId}",
                    $"Studio {b.StudioId}",
                    b.StartTime.ToString("MMM d, hh:mm tt"),
                    $"₱{b.TotalAmount:N2}",
                    statusText,
                    "⋯ Actions"
                );

                if (dgvBookings.Columns.Contains("colStatus"))
                {
                    var statusCell = dgvBookings.Rows[idx].Cells["colStatus"];
                    statusCell.Style.ForeColor = GetStatusColor(b.BookingStatus);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }

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

        private async void DgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var bookingId = (int)(dgvBookings.Rows[e.RowIndex].Tag ?? 0);
            var booking = _allBookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null) return;

            var isStaff = IsStaff();
            var isPending = booking.BookingStatus == BookingStatuses.Pending;
            var isRescheduled = booking.BookingStatus == BookingStatuses.Rescheduled;
            var isConfirmed = booking.BookingStatus == BookingStatuses.Confirmed;
            var isCheckedIn = booking.BookingStatus == BookingStatuses.CheckedIn;
            var isCheckedOut = booking.BookingStatus == BookingStatuses.CheckedOut;
            var isCancelled = booking.BookingStatus == BookingStatuses.Cancelled;

            var menu = new ContextMenuStrip();

            if (isPending || isRescheduled || isConfirmed)
            {
                menu.Items.Add("✏  Edit", null, async (s, args) => await OpenEditAsync(booking));
            }
            if (isPending || isRescheduled)
            {
                menu.Items.Add("✓  Confirm", null, async (s, args) =>
                    await ChangeStatusAsync(booking, BookingStatuses.Confirmed, "confirm"));
            }
            if (isConfirmed && isStaff)
            {
                menu.Items.Add("▶  Check-In", null, async (s, args) =>
                    await ChangeStatusAsync(booking, BookingStatuses.CheckedIn, "check in"));
            }
            if (isCheckedIn && isStaff)
            {
                menu.Items.Add("■  Check-Out", null, async (s, args) =>
                    await ChangeStatusAsync(booking, BookingStatuses.CheckedOut, "check out"));
            }
            if (isCheckedIn || isCheckedOut || isCancelled)
            {
                menu.Items.Add("👁  View", null, (s, args) => ShowDetails(booking));
            }
            if (isPending || isRescheduled || isConfirmed)
            {
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add("🗑  Delete", null, async (s, args) => await ArchiveBookingAsync(booking));
            }

            if (menu.Items.Count == 0) return;

            var cellRect = dgvBookings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            menu.Show(dgvBookings, cellRect.Left, cellRect.Bottom);
        }

        // ==================== TOP BUTTONS ====================

        private async void btnReschedule_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            using var picker = new ReschedulePickerForm(_auth, _api);
            picker.ShowDialog(this);

            if (this.IsDisposed) return;
            if (picker.RescheduledSomething) await LoadBookingsAsync();
        }

        private async void btnNewBooking_Click(object sender, EventArgs e)
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new EditBookingForm(_api, companyId, null);
            dialog.ShowDialog(this);

            if (dialog.SavedSuccessfully) await LoadBookingsAsync();
        }

        // ==================== DIALOGS ====================

        private async System.Threading.Tasks.Task OpenEditAsync(BookingAdminDto booking)
        {
            if (this.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            using var dialog = new EditBookingForm(_api, companyId, booking);
            dialog.ShowDialog(this);

            if (this.IsDisposed) return;
            if (dialog.SavedSuccessfully) await LoadBookingsAsync();
        }

        private async System.Threading.Tasks.Task ChangeStatusAsync(BookingAdminDto booking, int newStatus, string verb)
        {
            if (this.IsDisposed) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to {verb} booking '{booking.BookingCode}'?",
                $"Confirm {verb}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                "Booking Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}