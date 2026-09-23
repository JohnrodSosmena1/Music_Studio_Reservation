using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM.winforms.DTOs;

namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    public partial class StaffDashboardForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly DashboardService _dashboardService;

        public StaffDashboardForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _dashboardService = new DashboardService(api);
        }

        private async void StaffDashboardForm_Load(object sender, EventArgs e)
        {
            if (_auth.CurrentUser is not null)
            {
                lblSubtitle.Text = $"Welcome, {_auth.CurrentUser.FullName} — Manage today's bookings";
            }

            // Wire quick action buttons
            btnCheckInClient.Click += (s, ev) => ShowInfo("Check-In Client", "Check-in form will open here.");
            btnCheckOutClient.Click += (s, ev) => ShowInfo("Check-Out Client", "Check-out form will open here.");
            btnViewBookings.Click += (s, ev) => ShowInfo("View Bookings", "Bookings list will open here.");
            btnViewAvailability.Click += (s, ev) => ShowInfo("Studio Availability", "Studio availability grid will open here.");

            // Load real data
            await LoadDataAsync();
        }

        // ==================== API DATA LOADING ====================

        private async Task LoadDataAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            StaffDashboardDto? data = null;

            try
            {
                data = await _dashboardService.GetStaffDashboardAsync(companyId);
            }
            catch
            {
                data = null;
            }

            data ??= GetFallbackData();

            BuildStatCards(data);
            BuildTodaySchedule(data.Schedule);
        }

        private static StaffDashboardDto GetFallbackData()
        {
            return new StaffDashboardDto
            {
                TodaysBookings = 8,
                CheckedInCount = 5,
                PendingBookings = 3,
                AvailableStudios = 2,
                Schedule = new List<ScheduleItem>
                {
                    new() { BookingId = 1, BookingCode = "BK001", StudioId = 1, StartTime = DateTime.Today.AddHours(10), EndTime = DateTime.Today.AddHours(12), Status = "CheckedIn" },
                    new() { BookingId = 2, BookingCode = "BK002", StudioId = 2, StartTime = DateTime.Today.AddHours(11), EndTime = DateTime.Today.AddHours(13), Status = "CheckedIn" },
                    new() { BookingId = 3, BookingCode = "BK003", StudioId = 3, StartTime = DateTime.Today.AddHours(14), EndTime = DateTime.Today.AddHours(16), Status = "Pending" },
                    new() { BookingId = 4, BookingCode = "BK004", StudioId = 4, StartTime = DateTime.Today.AddHours(15), EndTime = DateTime.Today.AddHours(17), Status = "Pending" }
                }
            };
        }

        // ==================== STAT CARDS ====================

        private void BuildStatCards(StaffDashboardDto data)
        {
            var cards = new[]
            {
                new StatCard
                {
                    Icon = "📅",
                    IconColor = AppTheme.Primary,
                    Title = "Today's Bookings",
                    Value = data.TodaysBookings.ToString(),
                    Subtext = DateTime.Today.ToString("MMM d, yyyy")
                },
                new StatCard
                {
                    Icon = "✓",
                    IconColor = AppTheme.Success,
                    Title = "Check-ins",
                    Value = data.CheckedInCount.ToString(),
                    Subtext = $"{data.TodaysBookings - data.CheckedInCount} remaining"
                },
                new StatCard
                {
                    Icon = "⏳",
                    IconColor = AppTheme.Warning,
                    Title = "Pending Bookings",
                    Value = data.PendingBookings.ToString(),
                    Subtext = "Awaiting confirmation"
                },
                new StatCard
                {
                    Icon = "🎸",
                    IconColor = AppTheme.Info,
                    Title = "Available Studios",
                    Value = data.AvailableStudios.ToString(),
                    Subtext = "Ready to book"
                }
            };

            pnlStats.Controls.Clear();

            int x = 30;
            foreach (var card in cards)
            {
                card.Size = new Size(240, 130);
                card.Location = new Point(x, 15);
                pnlStats.Controls.Add(card);
                x += 260;
            }
        }

        // ==================== TODAY'S SCHEDULE ====================

        private void BuildTodaySchedule(List<ScheduleItem> schedule)
        {
            pnlScheduleContent.Controls.Clear();

            if (schedule == null || schedule.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No bookings scheduled for today.",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = AppTheme.TextSecondary,
                    Location = new Point(20, 20),
                    AutoSize = true
                };
                pnlScheduleContent.Controls.Add(lbl);
                return;
            }

            int y = 0;
            foreach (var s in schedule)
            {
                var timeStr = $"{s.StartTime:hh:mm tt} - {s.EndTime:hh:mm tt}";
                var studioStr = $"Studio {s.StudioId}";
                var statusDisplay = s.CheckedIn && !s.CheckedOut ? "CheckedIn"
                                  : s.CheckedOut ? "CheckedOut"
                                  : s.Status;

                var row = BuildScheduleRow(timeStr, studioStr, "Booking", statusDisplay);
                row.Location = new Point(0, y);
                pnlScheduleContent.Controls.Add(row);
                y += 70;
            }
        }

        private Panel BuildScheduleRow(string time, string studio, string type, string status)
        {
            var row = new Panel
            {
                Size = new Size(620, 60),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(15)
            };
            RoundedCorners.Apply(row, 8);

            var lblTime = new Label
            {
                Text = time,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(15, 10),
                Size = new Size(200, 20)
            };

            var lblStudio = new Label
            {
                Text = $"{studio}  ·  {type}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = AppTheme.Primary,
                Location = new Point(15, 30),
                AutoSize = true
            };

            var badge = new StatusBadge
            {
                Text = status,
                Size = new Size(110, 26),
                Location = new Point(430, 17)
            };

            row.Controls.Add(lblTime);
            row.Controls.Add(lblStudio);
            row.Controls.Add(badge);

            return row;
        }

        // ==================== HELPERS ====================

        private void ShowInfo(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}