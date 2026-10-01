namespace CRM.winforms.Forms.Reports
{
    partial class BookingReportTab
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblRange = new System.Windows.Forms.Label();
            this.cmbRange = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();

            this.pnlStats = new System.Windows.Forms.TableLayoutPanel();

            this.pnlChart = new System.Windows.Forms.Panel();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlTable = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.pnlFilters.SuspendLayout();
            this.pnlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();

            // ==== BookingReportTab ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "BookingReportTab";
            this.Size = new System.Drawing.Size(1200, 800);
            this.Load += new System.EventHandler(this.BookingReportTab_Load);

            // ==== Filters ====
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 70;
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);

            this.lblRange.AutoSize = false;
            this.lblRange.Size = new System.Drawing.Size(70, 36);
            this.lblRange.Location = new System.Drawing.Point(20, 15);
            this.lblRange.Text = "Range";
            this.lblRange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRange.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRange.Name = "lblRange";

            this.cmbRange.Size = new System.Drawing.Size(170, 28);
            this.cmbRange.Location = new System.Drawing.Point(90, 19);
            this.cmbRange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRange.Name = "cmbRange";
            this.cmbRange.SelectedIndexChanged += new System.EventHandler(this.cmbRange_SelectedIndexChanged);

            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(270, 22);
            this.lblFrom.Text = "From";
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFrom.Name = "lblFrom";

            this.dtpFrom.Size = new System.Drawing.Size(130, 28);
            this.dtpFrom.Location = new System.Drawing.Point(315, 18);
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Name = "dtpFrom";

            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(460, 22);
            this.lblTo.Text = "To";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTo.Name = "lblTo";

            this.dtpTo.Size = new System.Drawing.Size(130, 28);
            this.dtpTo.Location = new System.Drawing.Point(490, 18);
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Name = "dtpTo";

            this.btnRun.Size = new System.Drawing.Size(120, 36);
            this.btnRun.Location = new System.Drawing.Point(635, 14);
            this.btnRun.Text = "▶  Run Report";
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Name = "btnRun";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);

            this.btnExport.Size = new System.Drawing.Size(130, 36);
            this.btnExport.Location = new System.Drawing.Point(765, 14);
            this.btnExport.Text = "⬇  Export CSV";
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnExport.FlatAppearance.BorderSize = 1;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Name = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            this.pnlFilters.Controls.Add(this.lblRange);
            this.pnlFilters.Controls.Add(this.cmbRange);
            this.pnlFilters.Controls.Add(this.lblFrom);
            this.pnlFilters.Controls.Add(this.dtpFrom);
            this.pnlFilters.Controls.Add(this.lblTo);
            this.pnlFilters.Controls.Add(this.dtpTo);
            this.pnlFilters.Controls.Add(this.btnRun);
            this.pnlFilters.Controls.Add(this.btnExport);

            // ==== Stats row (5 mini cards) ====
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height = 90;
            this.pnlStats.ColumnCount = 5;
            this.pnlStats.RowCount = 1;
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            for (int i = 0; i < 5; i++)
                this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ==== Chart panel ====
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChart.Height = 240;
            this.pnlChart.BackColor = System.Drawing.Color.White;
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Padding = new System.Windows.Forms.Padding(20);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);

            this.lblChartTitle.AutoSize = false;
            this.lblChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChartTitle.Height = 30;
            this.lblChartTitle.Text = "Bookings per Day";
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblChartTitle.Name = "lblChartTitle";

            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Name = "chart";

            this.pnlChart.Controls.Add(this.chart);
            this.pnlChart.Controls.Add(this.lblChartTitle);

            // ==== Table panel ====
            this.pnlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTable.BackColor = System.Drawing.Color.White;
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Padding = new System.Windows.Forms.Padding(20);

            this.lblTableTitle.AutoSize = false;
            this.lblTableTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTableTitle.Height = 30;
            this.lblTableTitle.Text = "Booking Details";
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTableTitle.Name = "lblTableTitle";

            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.MultiSelect = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.ColumnHeadersHeight = 34;
            this.dgvDetails.RowTemplate.Height = 34;
            this.dgvDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvDetails.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvDetails.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvDetails.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvDetails.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.EnableHeadersVisualStyles = false;

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Booking Code";
            this.colCode.FillWeight = 100;
            this.colCode.Name = "colCode";

            this.colCustomer.HeaderText = "Customer";
            this.colCustomer.FillWeight = 130;
            this.colCustomer.Name = "colCustomer";

            this.colStudio.HeaderText = "Studio";
            this.colStudio.FillWeight = 90;
            this.colStudio.Name = "colStudio";

            this.colStart.HeaderText = "Start";
            this.colStart.FillWeight = 130;
            this.colStart.Name = "colStart";

            this.colAmount.HeaderText = "Amount";
            this.colAmount.FillWeight = 80;
            this.colAmount.Name = "colAmount";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 90;
            this.colStatus.Name = "colStatus";

            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colCustomer,
                this.colStudio,
                this.colStart,
                this.colAmount,
                this.colStatus
            });

            this.pnlTable.Controls.Add(this.dgvDetails);
            this.pnlTable.Controls.Add(this.lblTableTitle);

            // ==== Tab assembly — chart top, table fill ====
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlFilters);

            this.pnlFilters.ResumeLayout(false);
            this.pnlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.ComboBox cmbRange;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.TableLayoutPanel pnlStats;

        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;

        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}