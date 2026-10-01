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
    public partial class RevenueReportTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly ReportService _reportService;

        private RevenueReportDto? _lastReport;

        // Controls
        private Panel pnlFilters;
        private Label lblRange, lblFrom, lblTo;
        private ComboBox cmbRange;
        private DateTimePicker dtpFrom, dtpTo;
        private Button btnRun, btnExport;
        private TableLayoutPanel pnlStats;
        private Panel pnlChart;
        private Label lblChartTitle;
        private Chart chart;
        private Panel pnlTable;
        private Label lblTableTitle;
        private DataGridView dgvTopCustomers;
        private DataGridViewTextBoxColumn colCustId, colCustName, colCustSpent, colCustCount;

        public RevenueReportTab(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _reportService = new ReportService(api);
        }

        private void InitializeComponent()
        {
            this.pnlFilters = new Panel();
            this.lblRange = new Label();
            this.cmbRange = new ComboBox();
            this.lblFrom = new Label();
            this.dtpFrom = new DateTimePicker();
            this.lblTo = new Label();
            this.dtpTo = new DateTimePicker();
            this.btnRun = new Button();
            this.btnExport = new Button();

            this.pnlStats = new TableLayoutPanel();
            this.pnlChart = new Panel();
            this.lblChartTitle = new Label();
            this.chart = new Chart();

            this.pnlTable = new Panel();
            this.lblTableTitle = new Label();
            this.dgvTopCustomers = new DataGridView();
            this.colCustId = new DataGridViewTextBoxColumn();
            this.colCustName = new DataGridViewTextBoxColumn();
            this.colCustSpent = new DataGridViewTextBoxColumn();
            this.colCustCount = new DataGridViewTextBoxColumn();

            this.pnlFilters.SuspendLayout();
            this.pnlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).BeginInit();
            this.SuspendLayout();

            // ==== Tab ====
            this.BackColor = Color.Transparent;
            this.Name = "RevenueReportTab";
            this.Size = new Size(1200, 800);
            this.Load += new EventHandler(this.RevenueReportTab_Load);

            // ==== Filters ====
            this.pnlFilters.Dock = DockStyle.Top;
            this.pnlFilters.Height = 70;
            this.pnlFilters.BackColor = Color.White;
            this.pnlFilters.Padding = new Padding(20, 15, 20, 15);
            this.pnlFilters.Name = "pnlFilters";

            this.lblRange.AutoSize = false;
            this.lblRange.Size = new Size(70, 36);
            this.lblRange.Location = new Point(20, 15);
            this.lblRange.Text = "Range";
            this.lblRange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRange.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblRange.TextAlign = ContentAlignment.MiddleLeft;

            this.cmbRange.Size = new Size(170, 28);
            this.cmbRange.Location = new Point(90, 19);
            this.cmbRange.Font = new Font("Segoe UI", 10F);
            this.cmbRange.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRange.FlatStyle = FlatStyle.Flat;
            this.cmbRange.SelectedIndexChanged += new EventHandler(this.cmbRange_SelectedIndexChanged);

            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new Point(270, 22);
            this.lblFrom.Text = "From";
            this.lblFrom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFrom.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblFrom.TextAlign = ContentAlignment.MiddleLeft;

            this.dtpFrom.Size = new Size(130, 28);
            this.dtpFrom.Location = new Point(315, 18);
            this.dtpFrom.Font = new Font("Segoe UI", 10F);
            this.dtpFrom.Format = DateTimePickerFormat.Short;

            this.lblTo.AutoSize = true;
            this.lblTo.Location = new Point(460, 22);
            this.lblTo.Text = "To";
            this.lblTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTo.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblTo.TextAlign = ContentAlignment.MiddleLeft;

            this.dtpTo.Size = new Size(130, 28);
            this.dtpTo.Location = new Point(490, 18);
            this.dtpTo.Font = new Font("Segoe UI", 10F);
            this.dtpTo.Format = DateTimePickerFormat.Short;

            this.btnRun.Size = new Size(120, 36);
            this.btnRun.Location = new Point(635, 14);
            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = Color.FromArgb(139, 92, 246);
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Click += new EventHandler(this.btnRun_Click);

            this.btnExport.Size = new Size(130, 36);
            this.btnExport.Location = new Point(765, 14);
            this.btnExport.Text = "⬇  Export CSV";
            this.btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnExport.ForeColor = Color.FromArgb(31, 41, 55);
            this.btnExport.BackColor = Color.White;
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderColor = Color.FromArgb(229, 231, 235);
            this.btnExport.FlatAppearance.BorderSize = 1;
            this.btnExport.Cursor = Cursors.Hand;
            this.btnExport.Click += new EventHandler(this.btnExport_Click);

            this.pnlFilters.Controls.Add(this.lblRange);
            this.pnlFilters.Controls.Add(this.cmbRange);
            this.pnlFilters.Controls.Add(this.lblFrom);
            this.pnlFilters.Controls.Add(this.dtpFrom);
            this.pnlFilters.Controls.Add(this.lblTo);
            this.pnlFilters.Controls.Add(this.dtpTo);
            this.pnlFilters.Controls.Add(this.btnRun);
            this.pnlFilters.Controls.Add(this.btnExport);

            // ==== Stats ====
            this.pnlStats.Dock = DockStyle.Top;
            this.pnlStats.Height = 90;
            this.pnlStats.ColumnCount = 5;
            this.pnlStats.RowCount = 1;
            this.pnlStats.BackColor = Color.Transparent;
            this.pnlStats.Padding = new Padding(0, 8, 0, 8);
            for (int i = 0; i < 5; i++)
                this.pnlStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.pnlStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // ==== Chart ====
            this.pnlChart.Dock = DockStyle.Top;
            this.pnlChart.Height = 240;
            this.pnlChart.BackColor = Color.White;
            this.pnlChart.Padding = new Padding(20);
            this.pnlChart.Name = "pnlChart";

            this.lblChartTitle.AutoSize = false;
            this.lblChartTitle.Dock = DockStyle.Top;
            this.lblChartTitle.Height = 30;
            this.lblChartTitle.Text = "Daily Revenue";
            this.lblChartTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblChartTitle.ForeColor = Color.FromArgb(31, 41, 55);

            this.chart.Dock = DockStyle.Fill;

            this.pnlChart.Controls.Add(this.chart);
            this.pnlChart.Controls.Add(this.lblChartTitle);

            // ==== Table ====
            this.pnlTable.Dock = DockStyle.Fill;
            this.pnlTable.BackColor = Color.White;
            this.pnlTable.Padding = new Padding(20);

            this.lblTableTitle.AutoSize = false;
            this.lblTableTitle.Dock = DockStyle.Top;
            this.lblTableTitle.Height = 30;
            this.lblTableTitle.Text = "Top 10 Customers by Spend";
            this.lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTableTitle.ForeColor = Color.FromArgb(31, 41, 55);

            this.dgvTopCustomers.Dock = DockStyle.Fill;
            this.dgvTopCustomers.BackgroundColor = Color.White;
            this.dgvTopCustomers.BorderStyle = BorderStyle.None;
            this.dgvTopCustomers.AllowUserToAddRows = false;
            this.dgvTopCustomers.AllowUserToDeleteRows = false;
            this.dgvTopCustomers.ReadOnly = true;
            this.dgvTopCustomers.RowHeadersVisible = false;
            this.dgvTopCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopCustomers.MultiSelect = false;
            this.dgvTopCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopCustomers.ColumnHeadersHeight = 34;
            this.dgvTopCustomers.RowTemplate.Height = 34;

            var dgvHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251),
                ForeColor = Color.FromArgb(107, 114, 128),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(249, 250, 251),
                SelectionForeColor = Color.FromArgb(107, 114, 128)
            };
            this.dgvTopCustomers.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvTopCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            this.dgvTopCustomers.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            this.dgvTopCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 233, 254);
            this.dgvTopCustomers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            this.dgvTopCustomers.GridColor = Color.FromArgb(229, 231, 235);
            this.dgvTopCustomers.EnableHeadersVisualStyles = false;
            this.dgvTopCustomers.ScrollBars = ScrollBars.Both;

            this.colCustId.HeaderText = "Customer ID";
            this.colCustId.FillWeight = 60;
            this.colCustId.HeaderCell.Style = dgvHeaderStyle;

            this.colCustName.HeaderText = "Customer Name";
            this.colCustName.FillWeight = 200;
            this.colCustName.HeaderCell.Style = dgvHeaderStyle;

            this.colCustSpent.HeaderText = "Total Spent";
            this.colCustSpent.FillWeight = 100;
            this.colCustSpent.HeaderCell.Style = dgvHeaderStyle;

            this.colCustCount.HeaderText = "Bookings";
            this.colCustCount.FillWeight = 80;
            this.colCustCount.HeaderCell.Style = dgvHeaderStyle;

            this.dgvTopCustomers.Columns.AddRange(new DataGridViewColumn[] {
                this.colCustId, this.colCustName, this.colCustSpent, this.colCustCount
            });

            this.pnlTable.Controls.Add(this.dgvTopCustomers);
            this.pnlTable.Controls.Add(this.lblTableTitle);

            // ==== Assembly ====
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlFilters);

            this.pnlFilters.ResumeLayout(false);
            this.pnlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).EndInit();
            this.ResumeLayout(false);
        }

        // ==================== LOAD ====================

        private async void RevenueReportTab_Load(object sender, EventArgs e)
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
            cmbRange.SelectedIndex = 1;
            ApplyRangeDefaults();
        }

        private void ApplyRangeDefaults()
        {
            var today = DateTime.Today;
            switch (cmbRange.SelectedIndex)
            {
                case 0: dtpFrom.Value = today.AddDays(-6); dtpTo.Value = today; break;
                case 1: dtpFrom.Value = today.AddDays(-29); dtpTo.Value = today; break;
                case 2: dtpFrom.Value = today.AddDays(-89); dtpTo.Value = today; break;
                case 3: dtpFrom.Value = new DateTime(today.Year, today.Month, 1); dtpTo.Value = today; break;
                case 4: dtpFrom.Value = new DateTime(today.Year, 1, 1); dtpTo.Value = today; break;
            }
        }

        private void cmbRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRange.SelectedIndex < 5) ApplyRangeDefaults();
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

            try
            {
                btnRun.Enabled = false;
                btnRun.Text = "⏳ Loading...";

                var report = await _reportService.GetRevenueReportAsync(companyId, from, to);
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
            finally
            {
                if (!this.IsDisposed)
                {
                    btnRun.Enabled = true;
                    btnRun.Text = "▶  Run Report";
                }
            }
        }

        // ==================== STAT CARDS ====================

        private void BuildStatCards(RevenueReportDto r)
        {
            pnlStats.Controls.Clear();

            var cards = new (string Title, string Value, Color Color)[]
            {
                ("Total Revenue", $"₱{r.TotalRevenue:N2}", AppTheme.Success),
                ("Avg Booking", $"₱{r.AverageBookingValue:N2}", AppTheme.Primary),
                ("Paid Bookings", r.PaidBookings.ToString(), AppTheme.Info),
                ("Highest", $"₱{r.HighestBooking:N2}", AppTheme.Warning),
                ("Lowest", $"₱{r.LowestBooking:N2}", AppTheme.TextSecondary)
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
                Padding = new Padding(16, 10, 16, 8),
                Height = 74
            };

            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(229, 231, 235), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 18,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = color,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            pnl.Controls.Add(lblValue);
            pnl.Controls.Add(lblTitle);
            return pnl;
        }

        // ==================== CHART ====================

        private void BuildChart(RevenueReportDto r)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();

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
            area.AxisX.Maximum = Math.Max(1, (r.DailyRevenue?.Count ?? 1) - 1);
            area.AxisX.IsMarginVisible = true;
            chart.ChartAreas.Add(area);

            var series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                Color = AppTheme.Success,
                Font = new Font("Segoe UI", 8F),
                XValueType = ChartValueType.Int32
            };

            if (r.DailyRevenue != null)
            {
                for (int i = 0; i < r.DailyRevenue.Count; i++)
                {
                    var d = r.DailyRevenue[i];
                    var idx = series.Points.AddXY(i, d.Revenue);
                    series.Points[idx].AxisLabel = d.Date;
                    series.Points[idx].ToolTip = $"₱{d.Revenue:N2} ({d.BookingCount} bookings)";
                }
            }

            chart.Series.Add(series);
        }

        // ==================== TABLE ====================

        private void BuildTable(RevenueReportDto r)
        {
            dgvTopCustomers.Rows.Clear();

            if (r.TopCustomers == null || r.TopCustomers.Count == 0)
            {
                dgvTopCustomers.Rows.Add("—", "No customers in this range", "", "");
                return;
            }

            foreach (var c in r.TopCustomers)
            {
                dgvTopCustomers.Rows.Add(
                    c.CustomerId,
                    c.CustomerName,
                    $"₱{c.TotalSpent:N2}",
                    c.BookingCount
                );
            }
            dgvTopCustomers.ClearSelection();
        }

        // ==================== EXPORT ====================

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_lastReport == null)
            {
                MessageBox.Show("No report to export.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"RevenueReport_{_lastReport.From:yyyyMMdd}_{_lastReport.To:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var ok = ReportService.ExportRevenueReportToCsv(_lastReport, sfd.FileName);

            MessageBox.Show(
                ok ? $"Report exported to:\n{sfd.FileName}" : "Failed to export.",
                ok ? "Export Successful" : "Export Failed",
                MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}