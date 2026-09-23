using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM_MusicStudioReservation.domain.entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CRM.winforms.DTOs;


namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    public partial class AdminDashboardForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly DashboardService _dashboardService;

        private List<UpcomingBookingItem> _lastUpcoming = new();

        public AdminDashboardForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _dashboardService = new DashboardService(api);
        }

        private async void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            if (_auth.CurrentUser is not null)
            {
                lblWelcome.Text = "Admin Dashboard";
                lblSubtitle.Text = $"Welcome, {_auth.CurrentUser.FullName} — Overview of your music studio operations";
            }

            await LoadDataAsync();

            if (this.IsDisposed) return;

            this.PerformLayout();
            this.Refresh();
        }

        // ==================== API DATA LOADING ====================

        private async Task LoadDataAsync()
        {
            var companyId = _auth.CurrentUser?.CompanyId ?? 1;

            AdminDashboardDto? data = null;
            bool usingFallback = false;

            try
            {
                data = await _dashboardService.GetAdminDashboardAsync(companyId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] API failed: {ex.Message}");
                data = null;
            }

            // 👇 GUARD — bail if form was disposed while waiting on the API
            if (this.IsDisposed) return;

            if (data == null)
            {
                data = GetFallbackData();
                usingFallback = true;
            }

            System.Diagnostics.Debug.WriteLine(
                $"[Dashboard] UsingFallback={usingFallback}, " +
                $"Recent={data.RecentBookings?.Count ?? -1}, " +
                $"Upcoming={data.UpcomingBookings?.Count ?? -1}, " +
                $"StatusByDay={data.BookingsByStatusByDay?.Count ?? -1}, " +
                $"StudioUtil={data.StudioUtilization?.Count ?? -1}, " +
                $"AuditLogs={data.RecentAuditLogs?.Count ?? -1}");

            if (this.IsDisposed) return;
            BuildStatCards(data);

            if (this.IsDisposed) return;
            BuildStackedBarChart(data.BookingsByStatusByDay);

            if (this.IsDisposed) return;
            BuildStatusDonut(data);

            if (this.IsDisposed) return;
            BuildStudioUtilization(data.StudioUtilization);

            if (this.IsDisposed) return;
            BuildRecentBookings(data.RecentBookings);

            if (this.IsDisposed) return;
            BuildUpcomingBookings(data.UpcomingBookings);

            if (this.IsDisposed) return;
            BuildAuditLogs(data.RecentAuditLogs);

            if (this.IsDisposed) return;
            WireRowClickHandlers();
        }

        // ==================== PUBLIC REFRESH ====================

        public async System.Threading.Tasks.Task RefreshDashboardAsync()
        {
            DashboardService.InvalidateAdminDashboardCache();

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var data = await _dashboardService.GetAdminDashboardAsync(companyId, forceRefresh: true);

            if (this.IsDisposed) return;

            if (data == null) data = GetFallbackData();

            if (this.IsDisposed) return;
            BuildStatCards(data);

            if (this.IsDisposed) return;
            BuildStackedBarChart(data.BookingsByStatusByDay);

            if (this.IsDisposed) return;
            BuildStatusDonut(data);

            if (this.IsDisposed) return;
            BuildStudioUtilization(data.StudioUtilization);

            if (this.IsDisposed) return;
            BuildRecentBookings(data.RecentBookings);

            if (this.IsDisposed) return;
            BuildUpcomingBookings(data.UpcomingBookings);

            if (this.IsDisposed) return;
            BuildAuditLogs(data.RecentAuditLogs);

            if (this.IsDisposed) return;
            WireRowClickHandlers();
        }

        private static AdminDashboardDto GetFallbackData()
        {
            return new AdminDashboardDto
            {
                TotalBookings = 48,
                TotalCustomers = 32,
                TotalRevenue = 24560m,
                ActiveStudios = 5,

                TodayBookings = 12,
                PendingBookings = 6,
                CompletedBookings = 36,
                CancelledBookings = 4,

                TotalBookingsTrend = 12m,
                TodayBookingsTrend = 33m,
                PendingBookingsTrend = 20m,
                CompletedBookingsTrend = 18m,
                CancelledBookingsTrend = -50m,

                BookingsByStatusByDay = new List<BookingsByStatusDayItem>
                {
                    new() { Date = "Sep 11", Completed = 3, Pending = 1, Cancelled = 0 },
                    new() { Date = "Sep 12", Completed = 5, Pending = 2, Cancelled = 1 },
                    new() { Date = "Sep 13", Completed = 4, Pending = 2, Cancelled = 1 },
                    new() { Date = "Sep 14", Completed = 7, Pending = 3, Cancelled = 1 },
                    new() { Date = "Sep 15", Completed = 6, Pending = 2, Cancelled = 1 },
                    new() { Date = "Sep 16", Completed = 9, Pending = 4, Cancelled = 1 },
                    new() { Date = "Sep 17", Completed = 10, Pending = 3, Cancelled = 0 }
                },

                BookingStatusCompleted = 36,
                BookingStatusPending = 6,
                BookingStatusCancelled = 4,
                BookingStatusOnGoing = 2,

                StudioUtilization = new List<StudioUtilizationItem>
                {
                    new() { StudioName = "Studio A", UtilizationPercent = 80, BookingsThisWeek = 45 },
                    new() { StudioName = "Studio B", UtilizationPercent = 65, BookingsThisWeek = 36 },
                    new() { StudioName = "Studio C", UtilizationPercent = 45, BookingsThisWeek = 25 },
                    new() { StudioName = "Studio D", UtilizationPercent = 30, BookingsThisWeek = 17 }
                },

                RecentBookings = new List<RecentBookingItem>
                {
                    new() { BookingId = 1, BookingCode = "BKG-0013", CustomerId = 1, CustomerName = "Mark Villanueva", StudioId = 1, StudioName = "Studio B",
                            StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2),
                            TotalAmount = 1200m, BookingStatus = "CheckedOut", CreatedAt = DateTime.Now.AddMinutes(-5) },
                    new() { BookingId = 2, BookingCode = "BKG-0012", CustomerId = 2, CustomerName = "Alyssa Tan", StudioId = 2, StudioName = "Studio A",
                            StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(3),
                            TotalAmount = 2500m, BookingStatus = "Cancelled", CreatedAt = DateTime.Now.AddMinutes(-30) },
                    new() { BookingId = 3, BookingCode = "BKG-0011", CustomerId = 3, CustomerName = "Kyle Ramirez", StudioId = 3, StudioName = "Studio C",
                            StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2),
                            TotalAmount = 1200m, BookingStatus = "Confirmed", CreatedAt = DateTime.Now.AddHours(-2) },
                    new() { BookingId = 4, BookingCode = "BKG-0010", CustomerId = 4, CustomerName = "Maria Santos", StudioId = 2, StudioName = "Studio B",
                            StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(4),
                            TotalAmount = 3500m, BookingStatus = "Pending", CreatedAt = DateTime.Now.AddHours(-26) },
                    new() { BookingId = 5, BookingCode = "BKG-0009", CustomerId = 5, CustomerName = "Juan Dela Cruz", StudioId = 1, StudioName = "Studio A",
                            StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2),
                            TotalAmount = 1200m, BookingStatus = "Confirmed", CreatedAt = DateTime.Now.AddHours(-50) }
                },

                UpcomingBookings = new List<UpcomingBookingItem>
                {
                    new() { BookingId = 11, BookingCode = "BKG-0014", CustomerName = "Daniel Santos",  StudioName = "Studio D",
                            StartTime = DateTime.Now.AddDays(1).Date.AddHours(13), EndTime = DateTime.Now.AddDays(1).Date.AddHours(15), BookingStatus = "Confirmed" },
                    new() { BookingId = 12, BookingCode = "BKG-0015", CustomerName = "Ella Cruz",      StudioName = "Studio A",
                            StartTime = DateTime.Now.AddDays(1).Date.AddHours(16), EndTime = DateTime.Now.AddDays(1).Date.AddHours(18), BookingStatus = "Pending" },
                    new() { BookingId = 13, BookingCode = "BKG-0016", CustomerName = "Ria Santos",     StudioName = "Studio B",
                            StartTime = DateTime.Now.AddDays(2).Date.AddHours(9),  EndTime = DateTime.Now.AddDays(2).Date.AddHours(11), BookingStatus = "Pending" },
                    new() { BookingId = 14, BookingCode = "BKG-0017", CustomerName = "Kenji Lopez",    StudioName = "Studio C",
                            StartTime = DateTime.Now.AddDays(2).Date.AddHours(13), EndTime = DateTime.Now.AddDays(2).Date.AddHours(16), BookingStatus = "Confirmed" },
                    new() { BookingId = 15, BookingCode = "BKG-0018", CustomerName = "Sofia Reyes",    StudioName = "Studio A",
                            StartTime = DateTime.Now.AddDays(3).Date.AddHours(10), EndTime = DateTime.Now.AddDays(3).Date.AddHours(12), BookingStatus = "Confirmed" }
                },

                RecentAuditLogs = new List<AuditLogFeedItem>()
            };
        }

        // ==================== STAT CARDS ====================

        private void BuildStatCards(AdminDashboardDto data)
        {
            if (this.IsDisposed || tlpStats == null || tlpStats.IsDisposed) return;

            tlpStats.Controls.Clear();

            var cards = new[]
            {
                new StatCard
                {
                    Icon = "📅", IconColor = AppTheme.Primary,
                    Title = "Total Bookings", Value = data.TotalBookings.ToString(),
                    Subtext = "", TrendPercent = data.TotalBookingsTrend, TrendCaption = "vs. last week"
                },
                new StatCard
                {
                    Icon = "📆", IconColor = AppTheme.Info,
                    Title = "Today's Bookings", Value = data.TodayBookings.ToString(),
                    Subtext = "", TrendPercent = data.TodayBookingsTrend, TrendCaption = "vs. yesterday"
                },
                new StatCard
                {
                    Icon = "⏱", IconColor = AppTheme.Warning,
                    Title = "Pending Bookings", Value = data.PendingBookings.ToString(),
                    Subtext = "", TrendPercent = data.PendingBookingsTrend, TrendCaption = "vs. last week"
                },
                new StatCard
                {
                    Icon = "✓", IconColor = AppTheme.Success,
                    Title = "Completed Bookings", Value = data.CompletedBookings.ToString(),
                    Subtext = "", TrendPercent = data.CompletedBookingsTrend, TrendCaption = "vs. last week"
                },
                new StatCard
                {
                    Icon = "✕", IconColor = AppTheme.Danger,
                    Title = "Cancelled Bookings", Value = data.CancelledBookings.ToString(),
                    Subtext = "", TrendPercent = data.CancelledBookingsTrend, TrendCaption = "vs. last week"
                }
            };

            foreach (var card in cards)
            {
                card.Dock = DockStyle.Fill;
                card.Margin = new Padding(6, 0, 6, 0);
                tlpStats.Controls.Add(card);
            }
        }

        // ==================== STACKED BAR CHART ====================

        private void BuildStackedBarChart(List<BookingsByStatusDayItem> data)
        {
            if (this.IsDisposed || chartOverview == null || chartOverview.IsDisposed) return;

            chartOverview.Series.Clear();
            chartOverview.ChartAreas.Clear();
            chartOverview.Legends.Clear();
            chartOverview.Titles.Clear();

            var legend = new Legend("default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Far,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent,
                IsTextAutoFit = false
            };
            chartOverview.Legends.Add(legend);

            var area = new ChartArea("main");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LineColor = AppTheme.Border;
            area.AxisY.LineColor = AppTheme.Border;
            area.AxisX.Interval = 1;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.Minimum = 0;

            // 👇 KEY FIX: use numeric X axis, not string
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = Math.Max(1, (data?.Count ?? 1) - 1);
            area.AxisX.IsMarginVisible = true;

            chartOverview.ChartAreas.Add(area);

            var sCompleted = new Series("Completed")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Primary,
                Font = new Font("Segoe UI", 8F),
                IsVisibleInLegend = true,
                XValueType = ChartValueType.Int32      // 👈 force numeric X
            };
            var sPending = new Series("Pending")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Warning,
                Font = new Font("Segoe UI", 8F),
                IsVisibleInLegend = true,
                XValueType = ChartValueType.Int32
            };
            var sCancelled = new Series("Cancelled")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Danger,
                Font = new Font("Segoe UI", 8F),
                IsVisibleInLegend = true,
                XValueType = ChartValueType.Int32
            };

            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var d = data[i];

                    // 👇 Add using INTEGER X, then set custom AxisLabel
                    var idxC = sCompleted.Points.AddXY(i, d.Completed);
                    sCompleted.Points[idxC].AxisLabel = d.Date;

                    var idxP = sPending.Points.AddXY(i, d.Pending);
                    sPending.Points[idxP].AxisLabel = d.Date;

                    var idxX = sCancelled.Points.AddXY(i, d.Cancelled);
                    sCancelled.Points[idxX].AxisLabel = d.Date;
                }
            }

            chartOverview.Series.Add(sCompleted);
            chartOverview.Series.Add(sPending);
            chartOverview.Series.Add(sCancelled);
        }

        // ==================== BOOKING STATUS DONUT ====================

        private void BuildStatusDonut(AdminDashboardDto data)
        {
            if (this.IsDisposed || chartDonut == null || chartDonut.IsDisposed) return;

            chartDonut.Series.Clear();
            chartDonut.ChartAreas.Clear();
            chartDonut.Legends.Clear();

            var area = new ChartArea("donut");
            area.BackColor = Color.White;
            area.Position = new ElementPosition(0, 10, 100, 70);
            chartDonut.ChartAreas.Add(area);

            var legend = new Legend("default")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent
            };
            chartDonut.Legends.Add(legend);

            var series = new Series("Status")
            {
                ChartType = SeriesChartType.Doughnut,
                Font = new Font("Segoe UI", 9F)
            };

            var slices = new (string Label, int Value, Color Color)[]
            {
                ("Completed", data.BookingStatusCompleted, AppTheme.Primary),
                ("Pending",   data.BookingStatusPending,   AppTheme.Warning),
                ("Cancelled", data.BookingStatusCancelled, AppTheme.Danger),
                ("On-Going",  data.BookingStatusOnGoing,   AppTheme.Info)
            };

            foreach (var s in slices)
            {
                if (s.Value <= 0) continue;
                var idx = series.Points.AddXY(s.Label, s.Value);
                series.Points[idx].Color = s.Color;
                series.Points[idx].LegendText = $"{s.Label}   {s.Value}";
            }

            series["DoughnutRadius"] = "55";
            series["PieLabelStyle"] = "Disabled";
            series["PieStartAngle"] = "270";

            chartDonut.Series.Add(series);

            var total = data.BookingStatusCompleted
                      + data.BookingStatusPending
                      + data.BookingStatusCancelled
                      + data.BookingStatusOnGoing;

            if (!this.IsDisposed && lblDonutCenter != null && !lblDonutCenter.IsDisposed)
                lblDonutCenter.Text = total.ToString();

            PositionDonutCenter();
        }

        private void PositionDonutCenter()
        {
            if (this.IsDisposed) return;

            if (!this.IsHandleCreated || !chartDonut.IsHandleCreated || !lblDonutCenter.IsHandleCreated)
            {
                EventHandler onHandleCreated = null;
                onHandleCreated = (s, e) =>
                {
                    this.HandleCreated -= onHandleCreated;
                    PositionDonutCenter();
                };
                this.HandleCreated += onHandleCreated;
                return;
            }

            BeginInvoke(new Action(() =>
            {
                try
                {
                    if (this.IsDisposed || chartDonut.IsDisposed || lblDonutCenter.IsDisposed) return;
                    if (chartDonut.ChartAreas.Count == 0) return;

                    chartDonut.Update();

                    var area = chartDonut.ChartAreas[0];
                    var pos = area.Position;

                    int areaX = chartDonut.Left + (int)(chartDonut.Width * (pos.X / 100.0));
                    int areaY = chartDonut.Top + (int)(chartDonut.Height * (pos.Y / 100.0));
                    int areaW = (int)(chartDonut.Width * (pos.Width / 100.0));
                    int areaH = (int)(chartDonut.Height * (pos.Height / 100.0));

                    int cx = areaX + areaW / 2;
                    int cy = areaY + areaH / 2;

                    int w = 100;
                    int h = 40;

                    lblDonutCenter.AutoSize = false;
                    lblDonutCenter.Size = new Size(w, h);
                    lblDonutCenter.Location = new Point(cx - (w / 2) - chartDonut.Left + pnlDonutChart.Padding.Left,
                                                       cy - (h / 2) - chartDonut.Top + pnlDonutChart.Padding.Top);
                    lblDonutCenter.BringToFront();
                    lblDonutCenter.Invalidate();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[DonutCenter] {ex.Message}");
                }
            }));
        }

        // ==================== STUDIO UTILIZATION ====================

        private void BuildStudioUtilization(List<StudioUtilizationItem> items)
        {
            if (this.IsDisposed || pnlUtilizationBody == null || pnlUtilizationBody.IsDisposed) return;

            pnlUtilizationBody.Controls.Clear();

            if (items == null || items.Count == 0)
            {
                var empty = new Label
                {
                    Text = "No studio data available",
                    ForeColor = AppTheme.TextMuted,
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlUtilizationBody.Controls.Add(empty);
                return;
            }

            int y = 8;
            foreach (var item in items)
            {
                var row = BuildUtilizationRow(item);
                row.Location = new Point(0, y);
                row.Width = pnlUtilizationBody.ClientSize.Width - 4;
                row.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                pnlUtilizationBody.Controls.Add(row);
                y += row.Height + 12;
            }
        }

        private Panel BuildUtilizationRow(StudioUtilizationItem item)
        {
            int rowH = 58;

            var row = new Panel
            {
                Height = rowH,
                BackColor = Color.Transparent
            };

            var lblName = new Label
            {
                Text = item.StudioName,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                AutoSize = false,
                Location = new Point(0, 0),
                Size = new Size(180, 22)
            };

            var lblPct = new Label
            {
                Text = $"{item.UtilizationPercent}%",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(180, 0),
                Size = new Size(80, 22),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var track = new Panel
            {
                Location = new Point(0, 28),
                Height = 10,
                BackColor = Color.FromArgb(243, 244, 246),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var fill = new Panel
            {
                Location = new Point(0, 0),
                Height = 10,
                BackColor = AppTheme.Primary
            };

            track.Controls.Add(fill);

            row.Resize += (s, e) =>
            {
                int trackWidth = row.ClientSize.Width;
                track.Width = trackWidth;
                int fillWidth = (int)(trackWidth * (item.UtilizationPercent / 100.0));
                fill.Width = Math.Max(0, Math.Min(trackWidth, fillWidth));
            };

            row.Controls.Add(lblName);
            row.Controls.Add(lblPct);
            row.Controls.Add(track);

            return row;
        }

        // ==================== RECENT BOOKINGS ====================

        private void BuildRecentBookings(List<RecentBookingItem> bookings)
        {
            if (this.IsDisposed || dgvRecentBookings == null || dgvRecentBookings.IsDisposed) return;
            if (dgvRecentBookings.Columns.Count == 0) return;   // 👈 safety

            dgvRecentBookings.Rows.Clear();

            if (bookings == null || bookings.Count == 0)
            {
                dgvRecentBookings.Rows.Add("—", "No recent bookings", "", "", "", "", "");
                return;
            }

            foreach (var b in bookings)
            {
                var customerDisplay = string.IsNullOrWhiteSpace(b.CustomerName)
                    ? $"Customer {b.CustomerId}"
                    : b.CustomerName;

                var studioDisplay = string.IsNullOrWhiteSpace(b.StudioName)
                    ? $"Studio {b.StudioId}"
                    : b.StudioName;

                var idx = dgvRecentBookings.Rows.Add(
                    b.BookingId,
                    b.BookingCode,
                    customerDisplay,
                    studioDisplay,
                    b.StartTime.ToString("MMM d, hh:mm tt"),
                    $"₱{b.TotalAmount:N2}",
                    b.BookingStatus
                );

                if (dgvRecentBookings.Columns.Contains("colRB_Status"))
                {
                    var statusCell = dgvRecentBookings.Rows[idx].Cells["colRB_Status"];
                    statusCell.Style.ForeColor = GetBookingStatusColor(b.BookingStatus);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        // ==================== UPCOMING BOOKINGS ====================

        private void BuildUpcomingBookings(List<UpcomingBookingItem> items)
        {
            if (this.IsDisposed || dgvUpcoming == null || dgvUpcoming.IsDisposed) return;
            if (dgvUpcoming.Columns.Count == 0) return;         // 👈 safety

            _lastUpcoming = items ?? new();
            dgvUpcoming.Rows.Clear();

            if (items == null || items.Count == 0)
            {
                dgvUpcoming.Rows.Add("No upcoming bookings", "", "", "", "");
                return;
            }

            foreach (var u in items)
            {
                var customerDisplay = string.IsNullOrWhiteSpace(u.CustomerName) ? "—" : u.CustomerName;
                var studioDisplay = string.IsNullOrWhiteSpace(u.StudioName) ? "—" : u.StudioName;

                var idx = dgvUpcoming.Rows.Add(
                    u.BookingCode,
                    customerDisplay,
                    studioDisplay,
                    u.StartTime.ToString("MMM d, hh:mm tt"),
                    u.BookingStatus
                );

                if (dgvUpcoming.Columns.Contains("colUB_Status"))
                {
                    var statusCell = dgvUpcoming.Rows[idx].Cells["colUB_Status"];
                    statusCell.Style.ForeColor = GetBookingStatusColor(u.BookingStatus);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        // ==================== AUDIT LOGS ====================

        private void BuildAuditLogs(List<AuditLogFeedItem> logs)
        {
            if (this.IsDisposed || pnlAuditLogsBody == null || pnlAuditLogsBody.IsDisposed) return;

            pnlAuditLogsBody.Controls.Clear();

            if (logs == null || logs.Count == 0)
            {
                var placeholder = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };

                var icon = new Label
                {
                    Text = "🕐",
                    Font = new Font("Segoe UI", 32F),
                    ForeColor = AppTheme.TextMuted,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 60
                };

                var msg = new Label
                {
                    Text = "Activity log will appear here",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = AppTheme.TextSecondary,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 24
                };

                var sub = new Label
                {
                    Text = "Coming in a future update",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = AppTheme.TextMuted,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 20
                };

                placeholder.Controls.Add(sub);
                placeholder.Controls.Add(msg);
                placeholder.Controls.Add(icon);
                pnlAuditLogsBody.Controls.Add(placeholder);
                return;
            }

            int y = 12;
            foreach (var log in logs)
            {
                var row = BuildAuditLogRow(log);
                row.Location = new Point(0, y);
                row.Width = pnlAuditLogsBody.ClientSize.Width - 8;
                row.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                pnlAuditLogsBody.Controls.Add(row);
                y += row.Height + 10;
            }
        }

        private Panel BuildAuditLogRow(AuditLogFeedItem log)
        {
            var row = new Panel
            {
                Height = 58,
                BackColor = Color.Transparent
            };

            var lblDot = new Label
            {
                Text = "●",
                Font = new Font("Segoe UI", 10F),
                ForeColor = AppTheme.Primary,
                AutoSize = false,
                Location = new Point(0, 18),
                Size = new Size(16, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblText = new Label
            {
                Text = $"{log.Action} {log.EntityName}" + (log.EntityId.HasValue ? $" #{log.EntityId}" : ""),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                AutoSize = false,
                Location = new Point(22, 10),
                Size = new Size(300, 22),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblWhen = new Label
            {
                Text = $"{log.UserEmail ?? "system"} · {log.CreatedAt:MMM d, hh:mm tt}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = AppTheme.TextMuted,
                AutoSize = false,
                Location = new Point(22, 32),
                Size = new Size(300, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            row.Controls.Add(lblDot);
            row.Controls.Add(lblText);
            row.Controls.Add(lblWhen);

            return row;
        }

        // ==================== ROW CLICK NAVIGATION ====================

        private void WireRowClickHandlers()
        {
            if (this.IsDisposed) return;
            if (dgvRecentBookings == null || dgvRecentBookings.IsDisposed) return;
            if (dgvUpcoming == null || dgvUpcoming.IsDisposed) return;

            dgvRecentBookings.CellDoubleClick -= DgvRecentBookings_CellDoubleClick;
            dgvRecentBookings.CellDoubleClick += DgvRecentBookings_CellDoubleClick;

            dgvUpcoming.CellDoubleClick -= DgvUpcoming_CellDoubleClick;
            dgvUpcoming.CellDoubleClick += DgvUpcoming_CellDoubleClick;

            dgvRecentBookings.Cursor = Cursors.Hand;
            dgvUpcoming.Cursor = Cursors.Hand;
        }

        private void DgvRecentBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (!dgvRecentBookings.Columns.Contains("colRB_Id")) return;

            var idObj = dgvRecentBookings.Rows[e.RowIndex].Cells["colRB_Id"].Value;
            if (idObj == null) return;
            if (!int.TryParse(idObj.ToString(), out var bookingId)) return;

            NavigateToBooking(bookingId);
        }

        private void DgvUpcoming_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.RowIndex < 0) return;
            if (!dgvUpcoming.Columns.Contains("colUB_Code")) return;

            var codeObj = dgvUpcoming.Rows[e.RowIndex].Cells["colUB_Code"].Value;
            if (codeObj == null) return;
            var code = codeObj.ToString();
            if (string.IsNullOrWhiteSpace(code)) return;

            var bookingId = _lastUpcoming.FirstOrDefault(u => u.BookingCode == code)?.BookingId;
            if (bookingId == null)
            {
                MessageBox.Show(
                    "Could not locate this booking. Try refreshing the dashboard.",
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            NavigateToBooking(bookingId.Value);
        }

        private void NavigateToBooking(int bookingId)
        {
            if (this.IsDisposed) return;

            var main = this.ParentForm as MainForm;
            if (main == null)
            {
                MessageBox.Show(
                    "Navigation is unavailable in this context.",
                    "Navigation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            main.NavigateToBooking(bookingId);
        }

        // ==================== HELPERS ====================

        private static Color GetBookingStatusColor(string status) => status?.ToLowerInvariant() switch
        {
            "pending" => Color.FromArgb(245, 158, 11),
            "confirmed" => Color.FromArgb(16, 185, 129),
            "checkedin" => Color.FromArgb(59, 130, 246),
            "checkedout" => Color.FromArgb(107, 114, 128),
            "cancelled" => Color.FromArgb(239, 68, 68),
            "rescheduled" => Color.FromArgb(139, 92, 246),
            _ => Color.Gray
        };
    }
}