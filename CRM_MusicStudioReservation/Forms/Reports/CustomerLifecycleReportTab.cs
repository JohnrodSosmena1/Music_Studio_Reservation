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
    public class CustomerLifecycleReportTab : UserControl
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly ReportService _reportService;

        private CrmAnalyticsDto? _lastReport;

        // UI Controls
        private Panel pnlFilters = null!;
        private Label lblRange = null!;
        private ComboBox cmbRange = null!;
        private Label lblFrom = null!;
        private DateTimePicker dtpFrom = null!;
        private Label lblTo = null!;
        private DateTimePicker dtpTo = null!;
        private Label lblSegmentFilter = null!;
        private ComboBox cmbSegmentFilter = null!;
        private TextBox txtSearch = null!;
        private Button btnRun = null!;
        private Button btnExport = null!;

        private TableLayoutPanel pnlStats = null!;
        private Panel pnlVisuals = null!;
        private Panel pnlPeakChart = null!;
        private Chart chartPeakHours = null!;
        private Label lblPeakTitle = null!;
        private Panel pnlRfmSummary = null!;
        private Label lblRfmTitle = null!;
        private DataGridView dgvRfmSummary = null!;

        private Panel pnlTable = null!;
        private Label lblTableTitle = null!;
        private DataGridView dgvCustomers = null!;

        public CustomerLifecycleReportTab(AuthService auth, ApiClient api)
        {
            _auth = auth;
            _api = api;
            _reportService = new ReportService(_api);

            InitializeComponent();
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
            this.lblSegmentFilter = new Label();
            this.cmbSegmentFilter = new ComboBox();
            this.txtSearch = new TextBox();
            this.btnRun = new Button();
            this.btnExport = new Button();

            this.pnlStats = new TableLayoutPanel();
            this.pnlVisuals = new Panel();
            this.pnlPeakChart = new Panel();
            this.chartPeakHours = new Chart();
            this.lblPeakTitle = new Label();
            this.pnlRfmSummary = new Panel();
            this.lblRfmTitle = new Label();
            this.dgvRfmSummary = new DataGridView();

            this.pnlTable = new Panel();
            this.lblTableTitle = new Label();
            this.dgvCustomers = new DataGridView();

            this.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlVisuals.SuspendLayout();
            this.pnlPeakChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPeakHours)).BeginInit();
            this.pnlRfmSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRfmSummary)).BeginInit();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();

            // ==== UserControl ====
            this.BackColor = Color.FromArgb(249, 250, 251);
            this.Size = new Size(1220, 800);
            this.Load += CustomerLifecycleReportTab_Load;

            // ==== Filters ====
            this.pnlFilters.Dock = DockStyle.Top;
            this.pnlFilters.Height = 65;
            this.pnlFilters.BackColor = Color.White;
            this.pnlFilters.Padding = new Padding(20, 15, 20, 15);

            // Range
            this.lblRange.Text = "Range:";
            this.lblRange.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRange.ForeColor = Color.FromArgb(75, 85, 99);
            this.lblRange.Location = new Point(16, 20);
            this.lblRange.AutoSize = true;

            this.cmbRange.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRange.Font = new Font("Segoe UI", 9F);
            this.cmbRange.Location = new Point(70, 17);
            this.cmbRange.Width = 110;
            this.cmbRange.SelectedIndexChanged += cmbRange_SelectedIndexChanged;

            // From
            this.lblFrom.Text = "From:";
            this.lblFrom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFrom.ForeColor = Color.FromArgb(75, 85, 99);
            this.lblFrom.Location = new Point(190, 20);
            this.lblFrom.AutoSize = true;

            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Font = new Font("Segoe UI", 9F);
            this.dtpFrom.Location = new Point(232, 17);
            this.dtpFrom.Width = 105;

            // To
            this.lblTo.Text = "To:";
            this.lblTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTo.ForeColor = Color.FromArgb(75, 85, 99);
            this.lblTo.Location = new Point(345, 20);
            this.lblTo.AutoSize = true;

            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Font = new Font("Segoe UI", 9F);
            this.dtpTo.Location = new Point(372, 17);
            this.dtpTo.Width = 105;

            // Segment Filter
            this.lblSegmentFilter.Text = "Segment:";
            this.lblSegmentFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSegmentFilter.ForeColor = Color.FromArgb(75, 85, 99);
            this.lblSegmentFilter.Location = new Point(488, 20);
            this.lblSegmentFilter.AutoSize = true;

            this.cmbSegmentFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSegmentFilter.Font = new Font("Segoe UI", 9F);
            this.cmbSegmentFilter.Location = new Point(555, 17);
            this.cmbSegmentFilter.Width = 135;
            this.cmbSegmentFilter.SelectedIndexChanged += (s, e) => FilterCustomerTable();

            // Search
            this.txtSearch.Font = new Font("Segoe UI", 9F);
            this.txtSearch.Location = new Point(700, 17);
            this.txtSearch.Width = 135;
            this.txtSearch.PlaceholderText = "Search customer...";
            this.txtSearch.TextChanged += (s, e) => FilterCustomerTable();

            // Buttons
            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRun.ForeColor = Color.White;
            this.btnRun.BackColor = AppTheme.Primary;
            this.btnRun.FlatStyle = FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = Cursors.Hand;
            this.btnRun.Location = new Point(845, 14);
            this.btnRun.Size = new Size(115, 32);
            this.btnRun.Click += btnRun_Click;

            this.btnExport.Text = "📥  Export CSV";
            this.btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnExport.ForeColor = Color.FromArgb(55, 65, 81);
            this.btnExport.BackColor = Color.FromArgb(243, 244, 246);
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.Cursor = Cursors.Hand;
            this.btnExport.Location = new Point(970, 14);
            this.btnExport.Size = new Size(115, 32);
            this.btnExport.Click += btnExport_Click;

            this.pnlFilters.Controls.Add(this.lblRange);
            this.pnlFilters.Controls.Add(this.cmbRange);
            this.pnlFilters.Controls.Add(this.lblFrom);
            this.pnlFilters.Controls.Add(this.dtpFrom);
            this.pnlFilters.Controls.Add(this.lblTo);
            this.pnlFilters.Controls.Add(this.dtpTo);
            this.pnlFilters.Controls.Add(this.lblSegmentFilter);
            this.pnlFilters.Controls.Add(this.cmbSegmentFilter);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.btnRun);
            this.pnlFilters.Controls.Add(this.btnExport);

            // ==== Stats Cards ====
            this.pnlStats.Dock = DockStyle.Top;
            this.pnlStats.Height = 90;
            this.pnlStats.ColumnCount = 5;
            this.pnlStats.RowCount = 1;
            this.pnlStats.BackColor = Color.Transparent;
            this.pnlStats.Padding = new Padding(0, 8, 0, 8);
            for (int i = 0; i < 5; i++)
                this.pnlStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.pnlStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // ==== Visuals (Split Peak Hours + RFM Segments) ====
            this.pnlVisuals.Dock = DockStyle.Top;
            this.pnlVisuals.Height = 230;
            this.pnlVisuals.BackColor = Color.Transparent;
            this.pnlVisuals.Padding = new Padding(0, 0, 0, 8);

            // Peak Chart (Left side 58%)
            this.pnlPeakChart.Dock = DockStyle.Left;
            this.pnlPeakChart.Width = 680;
            this.pnlPeakChart.BackColor = Color.White;
            this.pnlPeakChart.Padding = new Padding(16, 12, 16, 12);

            this.lblPeakTitle.AutoSize = false;
            this.lblPeakTitle.Dock = DockStyle.Top;
            this.lblPeakTitle.Height = 24;
            this.lblPeakTitle.Text = "Customer Peak Booking Hours";
            this.lblPeakTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblPeakTitle.ForeColor = Color.FromArgb(31, 41, 55);

            this.chartPeakHours.Dock = DockStyle.Fill;
            this.pnlPeakChart.Controls.Add(this.chartPeakHours);
            this.pnlPeakChart.Controls.Add(this.lblPeakTitle);

            // RFM Summary Table (Right side Fill)
            this.pnlRfmSummary.Dock = DockStyle.Fill;
            this.pnlRfmSummary.BackColor = Color.White;
            this.pnlRfmSummary.Padding = new Padding(16, 12, 16, 12);
            this.pnlRfmSummary.Margin = new Padding(8, 0, 0, 0);

            this.lblRfmTitle.AutoSize = false;
            this.lblRfmTitle.Dock = DockStyle.Top;
            this.lblRfmTitle.Height = 24;
            this.lblRfmTitle.Text = "RFM Customer Segmentation";
            this.lblRfmTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRfmTitle.ForeColor = Color.FromArgb(31, 41, 55);

            this.dgvRfmSummary.Dock = DockStyle.Fill;
            this.dgvRfmSummary.BackgroundColor = Color.White;
            this.dgvRfmSummary.BorderStyle = BorderStyle.None;
            this.dgvRfmSummary.AllowUserToAddRows = false;
            this.dgvRfmSummary.AllowUserToDeleteRows = false;
            this.dgvRfmSummary.ReadOnly = true;
            this.dgvRfmSummary.RowHeadersVisible = false;
            this.dgvRfmSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvRfmSummary.MultiSelect = false;
            this.dgvRfmSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRfmSummary.ColumnHeadersHeight = 28;
            this.dgvRfmSummary.RowTemplate.Height = 28;
            this.dgvRfmSummary.EnableHeadersVisualStyles = false;

            var rfmHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251),
                ForeColor = Color.FromArgb(107, 114, 128),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(249, 250, 251),
                SelectionForeColor = Color.FromArgb(107, 114, 128)
            };
            this.dgvRfmSummary.ColumnHeadersDefaultCellStyle = rfmHeaderStyle;
            this.dgvRfmSummary.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
            this.dgvRfmSummary.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            this.dgvRfmSummary.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            this.dgvRfmSummary.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            this.dgvRfmSummary.GridColor = Color.FromArgb(243, 244, 246);

            var colRfmSegment = new DataGridViewTextBoxColumn { HeaderText = "Segment", FillWeight = 110 };
            var colRfmCount = new DataGridViewTextBoxColumn { HeaderText = "Clients", FillWeight = 50 };
            var colRfmPct = new DataGridViewTextBoxColumn { HeaderText = "Share", FillWeight = 50 };
            var colRfmRev = new DataGridViewTextBoxColumn { HeaderText = "Revenue", FillWeight = 80 };
            this.dgvRfmSummary.Columns.AddRange(new DataGridViewColumn[] { colRfmSegment, colRfmCount, colRfmPct, colRfmRev });

            this.pnlRfmSummary.Controls.Add(this.dgvRfmSummary);
            this.pnlRfmSummary.Controls.Add(this.lblRfmTitle);

            this.pnlVisuals.Controls.Add(this.pnlRfmSummary);
            this.pnlVisuals.Controls.Add(this.pnlPeakChart);

            // ==== Customer Detail Table ====
            this.pnlTable.Dock = DockStyle.Fill;
            this.pnlTable.BackColor = Color.White;
            this.pnlTable.Padding = new Padding(20, 15, 20, 15);

            this.lblTableTitle.AutoSize = false;
            this.lblTableTitle.Dock = DockStyle.Top;
            this.lblTableTitle.Height = 28;
            this.lblTableTitle.Text = "Customer Lifecycle, RFM & Retention Analysis";
            this.lblTableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTableTitle.ForeColor = Color.FromArgb(31, 41, 55);

            this.dgvCustomers.Dock = DockStyle.Fill;
            this.dgvCustomers.BackgroundColor = Color.White;
            this.dgvCustomers.BorderStyle = BorderStyle.None;
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.ColumnHeadersHeight = 34;
            this.dgvCustomers.RowTemplate.Height = 32;
            this.dgvCustomers.ScrollBars = ScrollBars.Both;
            this.dgvCustomers.EnableHeadersVisualStyles = false;

            var dgvHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251),
                ForeColor = Color.FromArgb(107, 114, 128),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(249, 250, 251),
                SelectionForeColor = Color.FromArgb(107, 114, 128)
            };
            this.dgvCustomers.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            this.dgvCustomers.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            this.dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(237, 233, 254);
            this.dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            this.dgvCustomers.GridColor = Color.FromArgb(229, 231, 235);

            var colCustId = new DataGridViewTextBoxColumn { HeaderText = "ID", FillWeight = 40, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustCode = new DataGridViewTextBoxColumn { HeaderText = "Code", FillWeight = 70, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustName = new DataGridViewTextBoxColumn { HeaderText = "Customer Name", FillWeight = 160, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustSeg = new DataGridViewTextBoxColumn { HeaderText = "RFM Segment", FillWeight = 110, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustBookings = new DataGridViewTextBoxColumn { HeaderText = "Bookings", FillWeight = 60, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustSpend = new DataGridViewTextBoxColumn { HeaderText = "Lifetime Spend", FillWeight = 90, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustLast = new DataGridViewTextBoxColumn { HeaderText = "Last Visit", FillWeight = 80, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustRecency = new DataGridViewTextBoxColumn { HeaderText = "Recency", FillWeight = 70, HeaderCell = { Style = dgvHeaderStyle } };
            var colCustStatus = new DataGridViewTextBoxColumn { HeaderText = "Status", FillWeight = 50, HeaderCell = { Style = dgvHeaderStyle } };

            this.dgvCustomers.Columns.AddRange(new DataGridViewColumn[] {
                colCustId, colCustCode, colCustName, colCustSeg, colCustBookings, colCustSpend, colCustLast, colCustRecency, colCustStatus
            });

            this.pnlTable.Controls.Add(this.dgvCustomers);
            this.pnlTable.Controls.Add(this.lblTableTitle);

            // Assembly
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlVisuals);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlFilters);

            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlVisuals.ResumeLayout(false);
            this.pnlPeakChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPeakHours)).EndInit();
            this.pnlRfmSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRfmSummary)).EndInit();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
        }

        // ==================== LOAD & FILTERS ====================

        private async void CustomerLifecycleReportTab_Load(object? sender, EventArgs e)
        {
            SetupRangeFilter();
            SetupSegmentFilter();
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
            cmbRange.SelectedIndex = 1; // Last 30 Days
            ApplyRangeDefaults();
        }

        private void SetupSegmentFilter()
        {
            cmbSegmentFilter.Items.Clear();
            cmbSegmentFilter.Items.Add("All Segments");
            cmbSegmentFilter.Items.Add("Champions (VIP)");
            cmbSegmentFilter.Items.Add("Loyal Regulars");
            cmbSegmentFilter.Items.Add("New Customers");
            cmbSegmentFilter.Items.Add("At-Risk");
            cmbSegmentFilter.Items.Add("Hibernating");
            cmbSegmentFilter.SelectedIndex = 0;
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

        private void cmbRange_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbRange.SelectedIndex < 5) ApplyRangeDefaults();
        }

        private async void btnRun_Click(object? sender, EventArgs e)
        {
            await RunReportAsync();
        }

        // ==================== DATA FETCH & BIND ====================

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

                var report = await _reportService.GetCrmAnalyticsAsync(companyId, from, to);
                if (this.IsDisposed) return;

                if (report == null)
                {
                    MessageBox.Show("Failed to load CRM report. Please verify connection.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _lastReport = report;
                BuildStatCards(report);
                BuildPeakChart(report);
                BuildRfmSummary(report);
                FilterCustomerTable();
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

        private void BuildStatCards(CrmAnalyticsDto r)
        {
            pnlStats.Controls.Clear();

            var cards = new (string Title, string Value, Color Color)[]
            {
                ("Retention Rate", $"{r.RetentionRate:0.0}%", AppTheme.Success),
                ("Churn Rate", $"{r.ChurnRate:0.0}%", r.ChurnRate > 30 ? AppTheme.Danger : AppTheme.Warning),
                ("Avg Lifetime Value", $"₱{r.AverageCLV:N2}", AppTheme.Primary),
                ("Pareto 80/20 Share", $"{r.Top20PercentRevenueShare:0.0}%", AppTheme.Info),
                ("Repeat Booking", $"{r.RepeatBookingRate:0.0}%", AppTheme.PrimaryLight)
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

        // ==================== PEAK HOURS CHART ====================

        private void BuildPeakChart(CrmAnalyticsDto r)
        {
            chartPeakHours.Series.Clear();
            chartPeakHours.ChartAreas.Clear();
            chartPeakHours.Legends.Clear();

            var area = new ChartArea("main");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.LineColor = AppTheme.Border;
            area.AxisY.LineColor = AppTheme.Border;
            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;
            area.AxisX.IsMarginVisible = true;
            chartPeakHours.ChartAreas.Add(area);

            var series = new Series("Bookings")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(139, 92, 246),
                Font = new Font("Segoe UI", 8F),
                XValueType = ChartValueType.Int32
            };

            if (r.PeakHours != null)
            {
                for (int i = 0; i < r.PeakHours.Count; i++)
                {
                    var p = r.PeakHours[i];
                    var idx = series.Points.AddXY(i, p.BookingCount);
                    series.Points[idx].AxisLabel = p.TimeLabel;
                    series.Points[idx].ToolTip = $"{p.TimeLabel}: {p.BookingCount} booking(s)";
                }
            }

            chartPeakHours.Series.Add(series);
        }

        // ==================== RFM SUMMARY TABLE ====================

        private void BuildRfmSummary(CrmAnalyticsDto r)
        {
            dgvRfmSummary.Rows.Clear();
            if (r.RfmSegments == null) return;

            foreach (var seg in r.RfmSegments)
            {
                dgvRfmSummary.Rows.Add(
                    seg.SegmentName,
                    seg.CustomerCount,
                    $"{seg.Percentage:0.0}%",
                    $"₱{seg.TotalRevenue:N2}"
                );
            }
            dgvRfmSummary.ClearSelection();
        }

        // ==================== CUSTOMER DETAIL TABLE ====================

        private void FilterCustomerTable()
        {
            if (_lastReport == null || _lastReport.CustomerDetails == null) return;

            dgvCustomers.Rows.Clear();

            var search = txtSearch.Text.Trim().ToLowerInvariant();
            var selectedSegment = cmbSegmentFilter.SelectedIndex > 0
                ? cmbSegmentFilter.SelectedItem?.ToString()
                : null;

            var filtered = _lastReport.CustomerDetails.AsEnumerable();

            if (!string.IsNullOrEmpty(selectedSegment))
            {
                filtered = filtered.Where(c => c.RfmSegment.Equals(selectedSegment, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(search))
            {
                filtered = filtered.Where(c =>
                    c.CustomerName.ToLowerInvariant().Contains(search) ||
                    c.CustomerCode.ToLowerInvariant().Contains(search) ||
                    c.CustomerId.ToString().Contains(search));
            }

            var list = filtered.ToList();
            if (list.Count == 0)
            {
                dgvCustomers.Rows.Add("—", "—", "No matching customers found", "", "", "", "", "", "");
                return;
            }

            foreach (var c in list)
            {
                dgvCustomers.Rows.Add(
                    c.CustomerId,
                    c.CustomerCode,
                    c.CustomerName,
                    c.RfmSegment,
                    c.TotalBookings,
                    $"₱{c.LifetimeSpend:N2}",
                    c.LastBookingDate?.ToString("MMM dd, yyyy") ?? "Never",
                    c.RecencyDays == 999 ? "—" : $"{c.RecencyDays}d ago",
                    c.IsActive ? "Active" : "Inactive"
                );
            }
            dgvCustomers.ClearSelection();
        }

        // ==================== EXPORT ====================

        private void btnExport_Click(object? sender, EventArgs e)
        {
            if (_lastReport == null)
            {
                MessageBox.Show("No report to export. Please run the report first.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"CRM_Analytics_{_lastReport.From:yyyyMMdd}_{_lastReport.To:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var ok = ReportService.ExportCrmAnalyticsToCsv(_lastReport, sfd.FileName);

            MessageBox.Show(
                ok ? $"CRM report exported successfully to:\n{sfd.FileName}" : "Failed to export CRM report.",
                ok ? "Export Successful" : "Export Failed",
                MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}
