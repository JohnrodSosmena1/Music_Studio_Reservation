namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    partial class AdminDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlStats = new System.Windows.Forms.Panel();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();

            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlChartsRow = new System.Windows.Forms.TableLayoutPanel();

            this.pnlOverviewChart = new System.Windows.Forms.Panel();
            this.lblOverviewTitle = new System.Windows.Forms.Label();
            this.chartOverview = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlDonutChart = new System.Windows.Forms.Panel();
            this.lblDonutTitle = new System.Windows.Forms.Label();
            this.chartDonut = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblDonutCenter = new System.Windows.Forms.Label();

            this.pnlUtilization = new System.Windows.Forms.Panel();
            this.lblUtilizationTitle = new System.Windows.Forms.Label();
            this.pnlUtilizationBody = new System.Windows.Forms.Panel();

            this.pnlRecentBookings = new System.Windows.Forms.Panel();
            this.lblRecentBookingsTitle = new System.Windows.Forms.Label();
            this.dgvRecentBookings = new System.Windows.Forms.DataGridView();
            this.colRB_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Studio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRB_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.tlpBottomRow = new System.Windows.Forms.TableLayoutPanel();

            this.pnlUpcomingBookings = new System.Windows.Forms.Panel();
            this.lblUpcomingTitle = new System.Windows.Forms.Label();
            this.dgvUpcoming = new System.Windows.Forms.DataGridView();
            this.colUB_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUB_Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUB_Studio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUB_When = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUB_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // 👇 Phase D — Audit logs is now a single full-width panel (no more tlpFooterRow)
            this.pnlAuditLogs = new System.Windows.Forms.Panel();
            this.lblAuditLogsTitle = new System.Windows.Forms.Label();
            this.pnlAuditLogsBody = new System.Windows.Forms.Panel();

            this.pnlHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlChartsRow.SuspendLayout();
            this.pnlOverviewChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOverview)).BeginInit();
            this.pnlDonutChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDonut)).BeginInit();
            this.pnlUtilization.SuspendLayout();
            this.pnlRecentBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentBookings)).BeginInit();
            this.tlpBottomRow.SuspendLayout();
            this.pnlUpcomingBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcoming)).BeginInit();
            this.pnlAuditLogs.SuspendLayout();
            this.SuspendLayout();

            // ==== AdminDashboardForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminDashboardForm";
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";

            this.lblWelcome.AutoSize = false;
            this.lblWelcome.Size = new System.Drawing.Size(900, 40);
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Text = "Admin Dashboard";
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblWelcome.Name = "lblWelcome";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(900, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Overview of your music studio operations";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Stats Row ====
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height = 180;
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.ColumnCount = 5;
            this.tlpStats.RowCount = 1;
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.BackColor = System.Drawing.Color.Transparent;

            this.tlpStats.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.pnlStats.Controls.Add(this.tlpStats);

            // ==== Main Content — SCROLLABLE ====
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30, 10, 30, 30);

            // ==== Content Host ====
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContent.AutoSize = true;
            this.pnlContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Name = "pnlContent";

            // ---- Row 1: Charts (3-column TableLayoutPanel) ----
            this.pnlChartsRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartsRow.Height = 400;
            this.pnlChartsRow.BackColor = System.Drawing.Color.Transparent;
            this.pnlChartsRow.Name = "pnlChartsRow";
            this.pnlChartsRow.ColumnCount = 3;
            this.pnlChartsRow.RowCount = 1;

            this.pnlChartsRow.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.pnlChartsRow.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.pnlChartsRow.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.pnlChartsRow.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ---- Panel 1: Booking Overview (stacked bar) ----
            this.pnlOverviewChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOverviewChart.BackColor = System.Drawing.Color.White;
            this.pnlOverviewChart.Padding = new System.Windows.Forms.Padding(20);
            this.pnlOverviewChart.Name = "pnlOverviewChart";
            this.pnlOverviewChart.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.lblOverviewTitle.AutoSize = false;
            this.lblOverviewTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOverviewTitle.Height = 40;
            this.lblOverviewTitle.Text = "Booking Overview";
            this.lblOverviewTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOverviewTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblOverviewTitle.Name = "lblOverviewTitle";

            this.chartOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartOverview.Name = "chartOverview";

            this.pnlOverviewChart.Controls.Add(this.chartOverview);
            this.pnlOverviewChart.Controls.Add(this.lblOverviewTitle);

            // ---- Panel 2: Booking Status (donut) ----
            this.pnlDonutChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDonutChart.BackColor = System.Drawing.Color.White;
            this.pnlDonutChart.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDonutChart.Name = "pnlDonutChart";
            this.pnlDonutChart.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.lblDonutTitle.AutoSize = false;
            this.lblDonutTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDonutTitle.Height = 40;
            this.lblDonutTitle.Text = "Booking Status";
            this.lblDonutTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDonutTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblDonutTitle.Name = "lblDonutTitle";

            this.chartDonut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDonut.Name = "chartDonut";

            // Center label — bigger font, centered via BringToFront + manual positioning in code
            this.lblDonutCenter.AutoSize = false;
            this.lblDonutCenter.BackColor = System.Drawing.Color.Transparent;
            this.lblDonutCenter.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblDonutCenter.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblDonutCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDonutCenter.Name = "lblDonutCenter";
            this.lblDonutCenter.Text = "";

            this.pnlDonutChart.Controls.Add(this.lblDonutCenter);
            this.pnlDonutChart.Controls.Add(this.chartDonut);
            this.pnlDonutChart.Controls.Add(this.lblDonutTitle);
            this.lblDonutCenter.BringToFront();

            // ---- Panel 3: Studio Utilization ----
            this.pnlUtilization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUtilization.BackColor = System.Drawing.Color.White;
            this.pnlUtilization.Padding = new System.Windows.Forms.Padding(20);
            this.pnlUtilization.Name = "pnlUtilization";
            this.pnlUtilization.Margin = new System.Windows.Forms.Padding(0);

            this.lblUtilizationTitle.AutoSize = false;
            this.lblUtilizationTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUtilizationTitle.Height = 40;
            this.lblUtilizationTitle.Text = "Studio Utilization";
            this.lblUtilizationTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUtilizationTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblUtilizationTitle.Name = "lblUtilizationTitle";

            this.pnlUtilizationBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUtilizationBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlUtilizationBody.AutoScroll = true;
            this.pnlUtilizationBody.Name = "pnlUtilizationBody";

            this.pnlUtilization.Controls.Add(this.pnlUtilizationBody);
            this.pnlUtilization.Controls.Add(this.lblUtilizationTitle);

            this.pnlChartsRow.Controls.Add(this.pnlOverviewChart, 0, 0);
            this.pnlChartsRow.Controls.Add(this.pnlDonutChart, 1, 0);
            this.pnlChartsRow.Controls.Add(this.pnlUtilization, 2, 0);

            // ---- Row 2: Recent Transactions ----
            this.pnlRecentBookings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecentBookings.Height = 480;
            this.pnlRecentBookings.BackColor = System.Drawing.Color.White;
            this.pnlRecentBookings.Padding = new System.Windows.Forms.Padding(20);
            this.pnlRecentBookings.Name = "pnlRecentBookings";
            this.pnlRecentBookings.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);

            this.lblRecentBookingsTitle.AutoSize = false;
            this.lblRecentBookingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecentBookingsTitle.Height = 36;
            this.lblRecentBookingsTitle.Text = "Recent Transactions";
            this.lblRecentBookingsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblRecentBookingsTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblRecentBookingsTitle.Name = "lblRecentBookingsTitle";

            this.dgvRecentBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentBookings.AllowUserToAddRows = false;
            this.dgvRecentBookings.AllowUserToDeleteRows = false;
            this.dgvRecentBookings.ReadOnly = true;
            this.dgvRecentBookings.RowHeadersVisible = false;
            this.dgvRecentBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentBookings.MultiSelect = false;
            this.dgvRecentBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentBookings.ColumnHeadersHeight = 34;
            this.dgvRecentBookings.RowTemplate.Height = 34;
            this.dgvRecentBookings.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvRecentBookings.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvRecentBookings.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvRecentBookings.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvRecentBookings.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvRecentBookings.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvRecentBookings.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvRecentBookings.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvRecentBookings.Name = "dgvRecentBookings";
            this.dgvRecentBookings.EnableHeadersVisualStyles = false;

            this.colRB_Id.HeaderText = "ID";
            this.colRB_Id.FillWeight = 40;
            this.colRB_Id.Name = "colRB_Id";

            this.colRB_Code.HeaderText = "Booking Code";
            this.colRB_Code.FillWeight = 100;
            this.colRB_Code.Name = "colRB_Code";

            this.colRB_Customer.HeaderText = "Customer Name";
            this.colRB_Customer.FillWeight = 110;
            this.colRB_Customer.Name = "colRB_Customer";

            this.colRB_Studio.HeaderText = "Studio";
            this.colRB_Studio.FillWeight = 80;
            this.colRB_Studio.Name = "colRB_Studio";

            this.colRB_Start.HeaderText = "Date & Time";
            this.colRB_Start.FillWeight = 130;
            this.colRB_Start.Name = "colRB_Start";

            this.colRB_Amount.HeaderText = "Amount";
            this.colRB_Amount.FillWeight = 80;
            this.colRB_Amount.Name = "colRB_Amount";

            this.colRB_Status.HeaderText = "Status";
            this.colRB_Status.FillWeight = 90;
            this.colRB_Status.Name = "colRB_Status";

            this.dgvRecentBookings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colRB_Id,
                this.colRB_Code,
                this.colRB_Customer,
                this.colRB_Studio,
                this.colRB_Start,
                this.colRB_Amount,
                this.colRB_Status
            });

            this.pnlRecentBookings.Controls.Add(this.dgvRecentBookings);
            this.pnlRecentBookings.Controls.Add(this.lblRecentBookingsTitle);

            // ---- Row 3: Upcoming Bookings (now full width) ----
            this.tlpBottomRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpBottomRow.Height = 400;
            this.tlpBottomRow.BackColor = System.Drawing.Color.Transparent;
            this.tlpBottomRow.Name = "tlpBottomRow";
            this.tlpBottomRow.ColumnCount = 1;
            this.tlpBottomRow.RowCount = 1;
            this.tlpBottomRow.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);

            this.tlpBottomRow.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottomRow.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ---- Upcoming Bookings ----
            this.pnlUpcomingBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUpcomingBookings.BackColor = System.Drawing.Color.White;
            this.pnlUpcomingBookings.Padding = new System.Windows.Forms.Padding(20);
            this.pnlUpcomingBookings.Name = "pnlUpcomingBookings";
            this.pnlUpcomingBookings.Margin = new System.Windows.Forms.Padding(0);

            this.lblUpcomingTitle.AutoSize = false;
            this.lblUpcomingTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUpcomingTitle.Height = 36;
            this.lblUpcomingTitle.Text = "Upcoming Bookings";
            this.lblUpcomingTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblUpcomingTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblUpcomingTitle.Name = "lblUpcomingTitle";

            this.dgvUpcoming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUpcoming.BackgroundColor = System.Drawing.Color.White;
            this.dgvUpcoming.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUpcoming.AllowUserToAddRows = false;
            this.dgvUpcoming.AllowUserToDeleteRows = false;
            this.dgvUpcoming.ReadOnly = true;
            this.dgvUpcoming.RowHeadersVisible = false;
            this.dgvUpcoming.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUpcoming.MultiSelect = false;
            this.dgvUpcoming.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUpcoming.ColumnHeadersHeight = 34;
            this.dgvUpcoming.RowTemplate.Height = 34;
            this.dgvUpcoming.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvUpcoming.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvUpcoming.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvUpcoming.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvUpcoming.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvUpcoming.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvUpcoming.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvUpcoming.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvUpcoming.Name = "dgvUpcoming";
            this.dgvUpcoming.EnableHeadersVisualStyles = false;

            this.colUB_Code.HeaderText = "Code";
            this.colUB_Code.FillWeight = 90;
            this.colUB_Code.Name = "colUB_Code";

            this.colUB_Customer.HeaderText = "Customer";
            this.colUB_Customer.FillWeight = 110;
            this.colUB_Customer.Name = "colUB_Customer";

            this.colUB_Studio.HeaderText = "Studio";
            this.colUB_Studio.FillWeight = 80;
            this.colUB_Studio.Name = "colUB_Studio";

            this.colUB_When.HeaderText = "Date & Time";
            this.colUB_When.FillWeight = 130;
            this.colUB_When.Name = "colUB_When";

            this.colUB_Status.HeaderText = "Status";
            this.colUB_Status.FillWeight = 90;
            this.colUB_Status.Name = "colUB_Status";

            this.dgvUpcoming.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colUB_Code,
                this.colUB_Customer,
                this.colUB_Studio,
                this.colUB_When,
                this.colUB_Status
            });

            this.pnlUpcomingBookings.Controls.Add(this.dgvUpcoming);
            this.pnlUpcomingBookings.Controls.Add(this.lblUpcomingTitle);

            this.tlpBottomRow.Controls.Add(this.pnlUpcomingBookings, 0, 0);

            // ---- Row 4: Audit Logs (full width now) ----
            this.pnlAuditLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAuditLogs.Height = 400;
            this.pnlAuditLogs.BackColor = System.Drawing.Color.White;
            this.pnlAuditLogs.Padding = new System.Windows.Forms.Padding(20);
            this.pnlAuditLogs.Name = "pnlAuditLogs";
            this.pnlAuditLogs.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);

            this.lblAuditLogsTitle.AutoSize = false;
            this.lblAuditLogsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAuditLogsTitle.Height = 36;
            this.lblAuditLogsTitle.Text = "Recent Activity";
            this.lblAuditLogsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAuditLogsTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblAuditLogsTitle.Name = "lblAuditLogsTitle";

            this.pnlAuditLogsBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuditLogsBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlAuditLogsBody.AutoScroll = true;
            this.pnlAuditLogsBody.Name = "pnlAuditLogsBody";

            this.pnlAuditLogs.Controls.Add(this.pnlAuditLogsBody);
            this.pnlAuditLogs.Controls.Add(this.lblAuditLogsTitle);

            // ==== Add rows to pnlContent in REVERSE order ====
            this.pnlContent.Controls.Add(this.pnlAuditLogs);
            this.pnlContent.Controls.Add(this.tlpBottomRow);
            this.pnlContent.Controls.Add(this.pnlRecentBookings);
            this.pnlContent.Controls.Add(this.pnlChartsRow);

            // ==== pnlMain hosts pnlContent ====
            this.pnlMain.Controls.Add(this.pnlContent);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlChartsRow.ResumeLayout(false);
            this.pnlOverviewChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartOverview)).EndInit();
            this.pnlDonutChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDonut)).EndInit();
            this.pnlUtilization.ResumeLayout(false);
            this.pnlRecentBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentBookings)).EndInit();
            this.tlpBottomRow.ResumeLayout(false);
            this.pnlUpcomingBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcoming)).EndInit();
            this.pnlAuditLogs.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.TableLayoutPanel tlpStats;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel pnlChartsRow;

        private System.Windows.Forms.Panel pnlOverviewChart;
        private System.Windows.Forms.Label lblOverviewTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOverview;

        private System.Windows.Forms.Panel pnlDonutChart;
        private System.Windows.Forms.Label lblDonutTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDonut;
        private System.Windows.Forms.Label lblDonutCenter;

        private System.Windows.Forms.Panel pnlUtilization;
        private System.Windows.Forms.Label lblUtilizationTitle;
        private System.Windows.Forms.Panel pnlUtilizationBody;

        private System.Windows.Forms.Panel pnlRecentBookings;
        private System.Windows.Forms.Label lblRecentBookingsTitle;
        private System.Windows.Forms.DataGridView dgvRecentBookings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Studio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRB_Status;

        private System.Windows.Forms.TableLayoutPanel tlpBottomRow;
        private System.Windows.Forms.Panel pnlUpcomingBookings;
        private System.Windows.Forms.Label lblUpcomingTitle;
        private System.Windows.Forms.DataGridView dgvUpcoming;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUB_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUB_Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUB_Studio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUB_When;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUB_Status;

        private System.Windows.Forms.Panel pnlAuditLogs;
        private System.Windows.Forms.Label lblAuditLogsTitle;
        private System.Windows.Forms.Panel pnlAuditLogsBody;
    }
}