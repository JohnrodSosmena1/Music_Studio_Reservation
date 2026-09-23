using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM.winforms.DTOs;

namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    public partial class ClientDashboardForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly DashboardService _dashboardService;

        public ClientDashboardForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _dashboardService = new DashboardService(api);
        }

        private async void ClientDashboardForm_Load(object sender, EventArgs e)
        {
            // Personalize welcome
            if (_auth.CurrentUser is not null)
            {
                var firstName = _auth.CurrentUser.FullName.Split(' ').FirstOrDefault() ?? "there";
                lblWelcome.Text = $"Hello, {firstName}!";
            }

            // Wire quick action buttons
            btnBookStudio.Click += (s, ev) => ShowInfo("Book a Studio", "Click 'Bookings' in the sidebar to open the booking wizard.");
            btnMyBookings.Click += (s, ev) => ShowInfo("My Bookings", "Your bookings list will appear here.");
            btnCheckAvailability.Click += (s, ev) => ShowInfo("Check Availability", "Studio availability calendar will open here.");
            btnViewRewards.Click += (s, ev) => ShowInfo("Rewards", "Your loyalty rewards will appear here.");

            // Load real data from API
            await LoadDataAsync();
        }

        // ==================== API DATA LOADING ====================

        private async Task LoadDataAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            // TODO: properly map AppUser.UserId → Customer.CustomerId (via email or link table)
            // For now, hardcode to the demo customer (CustomerId = 1 in tenant DB)
            var customerId = 1;

            Console.WriteLine($"[ClientDashboard] Loading for companyId={companyId}, customerId={customerId}");

            ClientDashboardDto? data = null;

            try
            {
                data = await _dashboardService.GetClientDashboardAsync(companyId, customerId);

                if (data == null)
                {
                    Console.WriteLine("[ClientDashboard] API returned null — using fallback data");
                }
                else
                {
                    Console.WriteLine($"[ClientDashboard] Loaded: {data.UpcomingBookings.Count} bookings, {data.LoyaltyPoints} points");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ClientDashboard] Exception: {ex.Message}");
                data = null;
            }

            // Fall back to sample data if API failed
            data ??= GetFallbackData();

            // Update UI with the data
            BuildStatCards(data);
            BuildUpcomingBookings(data.UpcomingBookings);

            // Update loyalty card
            lblLoyaltyPoints.Text = $"{data.LoyaltyPoints} pts";
        }

        private static ClientDashboardDto GetFallbackData()
        {
            return new ClientDashboardDto
            {
                CustomerId = 1,
                CustomerName = "Sample Client",
                TotalBookings = 5,
                LoyaltyPoints = 120,
                AvailableBalance = 1250m,
                UpcomingBookings = new List<ClientBookingItem>
                {
                    new() { BookingId = 1, BookingCode = "BK001", StudioId = 1, StartTime = DateTime.Today.AddDays(1).AddHours(10), EndTime = DateTime.Today.AddDays(1).AddHours(12), Status = "Pending" },
                    new() { BookingId = 2, BookingCode = "BK002", StudioId = 3, StartTime = DateTime.Today.AddDays(3).AddHours(14), EndTime = DateTime.Today.AddDays(3).AddHours(16), Status = "Pending" }
                }
            };
        }

        // ==================== STAT CARDS ====================

        private void BuildStatCards(ClientDashboardDto data)
        {
            var cards = new[]
            {
                new StatCard
                {
                    Icon = "📅",
                    IconColor = AppTheme.Primary,
                    Title = "Upcoming Bookings",
                    Value = data.UpcomingBookings.Count.ToString(),
                    Subtext = data.UpcomingBookings.Count > 0
                        ? $"Next: {data.UpcomingBookings[0].StartTime:MMM d}"
                        : "No upcoming"
                },
                new StatCard
                {
                    Icon = "🎵",
                    IconColor = AppTheme.Primary,
                    Title = "Total Bookings",
                    Value = data.TotalBookings.ToString(),
                    Subtext = "All time"
                },
                new StatCard
                {
                    Icon = "⭐",
                    IconColor = AppTheme.Warning,
                    Title = "Loyalty Points",
                    Value = data.LoyaltyPoints.ToString(),
                    Subtext = "Keep booking to earn more"
                },
                new StatCard
                {
                    Icon = "💰",
                    IconColor = AppTheme.Success,
                    Title = "Available Balance",
                    Value = $"₱{data.AvailableBalance:N2}",
                    Subtext = "Wallet balance"
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

        // ==================== UPCOMING BOOKINGS ====================

        private void BuildUpcomingBookings(List<ClientBookingItem> bookings)
        {
            pnlUpcomingContent.Controls.Clear();

            if (bookings == null || bookings.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No upcoming bookings.\nBook your first studio session!",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = AppTheme.TextSecondary,
                    Location = new Point(20, 20),
                    AutoSize = true
                };
                pnlUpcomingContent.Controls.Add(lbl);
                return;
            }

            int y = 0;
            foreach (var b in bookings.Take(5))
            {
                var studioLabel = $"Studio {b.StudioId}";
                var date = b.StartTime.ToString("MMM d, yyyy");
                var time = $"{b.StartTime:hh:mm tt} - {b.EndTime:hh:mm tt}";

                // Convert status code to readable text
                var statusText = b.Status;
                if (int.TryParse(b.Status, out var statusNum))
                {
                    statusText = statusNum switch
                    {
                        0 => "Pending",
                        1 => "Confirmed",
                        2 => "Confirmed",
                        3 => "CheckedIn",
                        4 => "CheckedOut",
                        5 => "Cancelled",
                        _ => "Pending"
                    };
                }

                var row = BuildBookingRow(studioLabel, "Booking " + b.BookingCode, date, time, statusText);
                row.Location = new Point(0, y);
                pnlUpcomingContent.Controls.Add(row);
                y += 90;
            }
        }

        private Panel BuildBookingRow(string studio, string type, string date, string time, string status)
        {
            var row = new Panel
            {
                Size = new Size(620, 80),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(15)
            };
            RoundedCorners.Apply(row, 8);

            var lblStudio = new Label
            {
                Text = studio,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(15, 10),
                AutoSize = true
            };

            var lblType = new Label
            {
                Text = type,
                Font = new Font("Segoe UI", 9F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(15, 35),
                AutoSize = true
            };

            var lblDate = new Label
            {
                Text = $"{date}  ·  {time}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(15, 55),
                AutoSize = true
            };

            var badge = new StatusBadge
            {
                Text = status,
                Size = new Size(100, 26),
                Location = new Point(380, 15)
            };

            var btnView = new Button
            {
                Text = "View",
                Size = new Size(70, 30),
                Location = new Point(500, 25),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AppTheme.Primary,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderColor = AppTheme.Primary;
            btnView.Click += (s, e) => ShowInfo(studio, $"Booking details\nDate: {date}\nTime: {time}\nStatus: {status}");

            row.Controls.Add(lblStudio);
            row.Controls.Add(lblType);
            row.Controls.Add(lblDate);
            row.Controls.Add(badge);       // 👈 ADDED — badge was missing before
            row.Controls.Add(btnView);

            return row;
        }

        // ==================== HELPERS ====================

        private void ShowInfo(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}