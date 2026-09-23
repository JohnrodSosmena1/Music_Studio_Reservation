using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRM.winforms.Forms.Reports
{
    public partial class BookingReportTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly ReportService _reportService;

        private BookingReportDto? _lastReport;

        public BookingReportTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _reportService = new ReportService(api);
        }

        // ==================== LOAD ====================

        private async void BookingReportTab_Load(object sender, EventArgs e)
        {
            SetupRangeFilter();
            await RunReportAsync();
        }

        private void SetupRangeFilter()
        {
            cmbRange.Items.Clear();
            cmbRange.Items.Add("Last 7 Days");
            cmbRange.Items.Add("Last 30 Days");
            cmbRange.Items.Add("Last 90 Days");
            cmbRange.Items.Add("This Month");
            cmbRange.Items.Add("This Year");
            cmbRange.Items.Add("Custom");
            cmbRange.SelectedIndex = 1;   // Last 30 Days default

            ApplyRangeDefaults();
        }

        private void ApplyRangeDefaults()
        {
            var today = DateTime.Today;

            switch (cmbRange.SelectedIndex)
            {
                case 0:  // Last 7 Days
                    dtpFrom.Value = today.AddDays(-6);
                    dtpTo.Value = today;
                    break;
                case 1:  // Last 30 Days
                    dtpFrom.Value = today.AddDays(-29);
                    dtpTo.Value = today;
                    break;
                case 2:  // Last 90 Days
                    dtpFrom.Value = today.AddDays(-89);
                    dtpTo.Value = today;
                    break;
                case 3:  // This Month
                    dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
                    dtpTo.Value = today;
                    break;
                case 4:  // This Year
                    dtpFrom.Value = new DateTime(today.Year, 1, 1);
                    dtpTo.Value = today;
                    break;
                    // Custom: leave dtp values as-is
            }
        }

        private void cmbRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRange.SelectedIndex < 5)   // not Custom
            {
                ApplyRangeDefaults();
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            await RunReportAsync();
        }

        // ==================== DATA ====================

        private async System.Threading.Tasks.Task RunReportAsync()
        {
            if (this.IsDisposed) return;

            var companyId = _auth.CurrentUser?.CompanyId ?? 1;
            var from = dtpFrom.Value.Date;
            var to = dtpTo.Value.Date;

            if (from > to)
            {
                MessageBox.Show("From date must be on or before To date.", "Invalid Range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var report = await _reportService.GetBookingReportAsync(companyId, from, to);

            if (this.IsDisposed) return;

            if (report == null)
            {
                MessageBox.Show("Failed to load report. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _lastReport = report;
            BuildStatCards(report);
            BuildChart(report);
            BuildTable(report);
        }

        // ==================== STAT CARDS ====================

        private void BuildStatCards(BookingReportDto r)
        {
            pnlStats.Controls.Clear();

            var cards = new (string Title, string Value, Color Color)[]
            {
                ("Total Bookings", r.TotalBookings.ToString(), AppTheme.Primary),
                ("Completed", r.CompletedBookings.ToString(), AppTheme.Success),
                ("Pending", r.PendingBookings.ToString(), AppTheme.Warning),
                ("Cancelled", r.CancelledBookings.ToString(), AppTheme.Danger),
                ("Total Revenue", $"₱{r.TotalRevenue:N2}", AppTheme.Info)
            };

            foreach (var c in cards)
            {
                var card = BuildStatCard(c.Title, c.Value, c.Color);
                card.Dock = DockStyle.Fill;
                card.Margin = new Padding(6, 0, 6, 0);
                pnlStats.Controls.Add(card);
            }
        }

        private Panel BuildStatCard(string title, string value, Color color)
        {
            var pnl = new Panel
            {
                BackColor = Color.White,
                Padding = new Padding(15),
                Height = 70
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 18,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = color,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnl.Controls.Add(lblValue);
            pnl.Controls.Add(lblTitle);
            return pnl;
        }

        // ==================== CHART ====================

        private void BuildChart(BookingReportDto r)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();

            var legend = new Legend("default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Far,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent
            };
            chart.Legends.Add(legend);

            var area = new ChartArea("main");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LineColor = AppTheme.Border;
            area.AxisY.LineColor = AppTheme.Border;
            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = Math.Max(1, (r.DailyBreakdown?.Count ?? 1) - 1);
            area.AxisX.IsMarginVisible = true;
            chart.ChartAreas.Add(area);

            var sCompleted = new Series("Completed")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Primary,
                Font = new Font("Segoe UI", 8F),
                XValueType = ChartValueType.Int32
            };
            var sPending = new Series("Pending")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Warning,
                Font = new Font("Segoe UI", 8F),
                XValueType = ChartValueType.Int32
            };
            var sCancelled = new Series("Cancelled")
            {
                ChartType = SeriesChartType.StackedColumn,
                Color = AppTheme.Danger,
                Font = new Font("Segoe UI", 8F),
                XValueType = ChartValueType.Int32
            };

            if (r.DailyBreakdown != null)
            {
                for (int i = 0; i < r.DailyBreakdown.Count; i++)
                {
                    var d = r.DailyBreakdown[i];

                    var c = sCompleted.Points.AddXY(i, d.Completed);
                    sCompleted.Points[c].AxisLabel = d.Date;

                    var p = sPending.Points.AddXY(i, d.Pending);
                    sPending.Points[p].AxisLabel = d.Date;

                    var x = sCancelled.Points.AddXY(i, d.Cancelled);
                    sCancelled.Points[x].AxisLabel = d.Date;
                }
            }

            chart.Series.Add(sCompleted);
            chart.Series.Add(sPending);
            chart.Series.Add(sCancelled);
        }

        // ==================== TABLE ====================

        private void BuildTable(BookingReportDto r)
        {
            dgvDetails.Rows.Clear();

            if (r.Details == null || r.Details.Count == 0)
            {
                dgvDetails.Rows.Add("—", "No bookings in this range", "", "", "", "", "");
                return;
            }

            foreach (var d in r.Details)
            {
                var idx = dgvDetails.Rows.Add(
                    d.BookingId,
                    d.BookingCode,
                    d.CustomerName,
                    d.StudioName,
                    d.StartTime.ToString("MMM d, hh:mm tt"),
                    $"₱{d.TotalAmount:N2}",
                    d.BookingStatus
                );

                var statusCell = dgvDetails.Rows[idx].Cells[colStatus.Index];
                statusCell.Style.ForeColor = GetStatusColor(d.BookingStatus);
                statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private static Color GetStatusColor(string status) => status?.ToLowerInvariant() switch
        {
            "pending" => Color.FromArgb(245, 158, 11),
            "confirmed" => Color.FromArgb(16, 185, 129),
            "checkedin" => Color.FromArgb(59, 130, 246),
            "checkedout" => Color.FromArgb(107, 114, 128),
            "cancelled" => Color.FromArgb(239, 68, 68),
            "rescheduled" => Color.FromArgb(139, 92, 246),
            _ => Color.Gray
        };

        // ==================== EXPORT ====================

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_lastReport == null || _lastReport.Details == null || _lastReport.Details.Count == 0)
            {
                MessageBox.Show("No report to export. Run the report first.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"BookingReport_{_lastReport.From:yyyyMMdd}_{_lastReport.To:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var ok = ReportService.ExportBookingReportToCsv(_lastReport, sfd.FileName);

            if (ok)
            {
                MessageBox.Show($"Report exported to:\n{sfd.FileName}", "Export Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to export. Check file permissions.", "Export Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}