namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class BookingManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnReschedule = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewBooking = new System.Windows.Forms.Button();

            this.pnlBody = new System.Windows.Forms.Panel();          // 👈 NEW
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // BookingManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Name = "BookingManagementForm";
            this.Text = "Booking Management";
            this.Load += new System.EventHandler(this.BookingManagementForm_Load);

            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 150;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Booking Management";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblSubtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(500, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Text = "Manage customer bookings";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // lblFrom
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(30, 100);
            this.lblFrom.Text = "From:";
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblFrom.Name = "lblFrom";

            // dtpFrom
            this.dtpFrom.Location = new System.Drawing.Point(75, 97);
            this.dtpFrom.Size = new System.Drawing.Size(140, 26);
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);

            // lblTo
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(225, 100);
            this.lblTo.Text = "To:";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblTo.Name = "lblTo";

            // dtpTo
            this.dtpTo.Location = new System.Drawing.Point(260, 97);
            this.dtpTo.Size = new System.Drawing.Size(140, 26);
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);

            // cmbStatus
            this.cmbStatus.Location = new System.Drawing.Point(415, 97);
            this.cmbStatus.Size = new System.Drawing.Size(150, 26);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(575, 97);
            this.txtSearch.Size = new System.Drawing.Size(190, 26);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by code...";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // btnReschedule
            this.btnReschedule.Size = new System.Drawing.Size(160, 32);
            this.btnReschedule.Location = new System.Drawing.Point(775, 94);
            this.btnReschedule.Text = "Reschedule Booking";
            this.btnReschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReschedule.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnReschedule.FlatAppearance.BorderSize = 1;
            this.btnReschedule.BackColor = System.Drawing.Color.White;
            this.btnReschedule.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnReschedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReschedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);

            // btnNewBooking
            this.btnNewBooking.Size = new System.Drawing.Size(130, 32);
            this.btnNewBooking.Location = new System.Drawing.Point(945, 94);
            this.btnNewBooking.Text = "+ New Booking";
            this.btnNewBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBooking.FlatAppearance.BorderSize = 0;
            this.btnNewBooking.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnNewBooking.ForeColor = System.Drawing.Color.White;
            this.btnNewBooking.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.Click += new System.EventHandler(this.btnNewBooking_Click);

            // btnRefresh
            this.btnRefresh.Size = new System.Drawing.Size(40, 32);
            this.btnRefresh.Location = new System.Drawing.Point(1085, 94);
            this.btnRefresh.Text = "↻";
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblFrom);
            this.pnlHeader.Controls.Add(this.dtpFrom);
            this.pnlHeader.Controls.Add(this.lblTo);
            this.pnlHeader.Controls.Add(this.dtpTo);
            this.pnlHeader.Controls.Add(this.cmbStatus);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.btnReschedule);
            this.pnlHeader.Controls.Add(this.btnNewBooking);
            this.pnlHeader.Controls.Add(this.btnRefresh);

            // pnlBody — the wrapper that matches Inventory's pattern
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlBody.Name = "pnlBody";

            // dgvBookings — docked Fill inside pnlBody, mirrors Inventory exactly
            this.dgvBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.AllowUserToResizeRows = false;
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvBookings.RowHeadersVisible = false;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookings.ColumnHeadersHeight = 40;
            this.dgvBookings.RowTemplate.Height = 50;
            this.dgvBookings.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvBookings.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvBookings.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvBookings.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvBookings.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvBookings.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.EnableHeadersVisualStyles = false;
            this.dgvBookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvBookings_CellContentClick);

            // Text columns
            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 30;
            this.colId.Name = "colId";
            this.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 80;
            this.colCode.Name = "colCode";
            this.colCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colCustomer.HeaderText = "Client Name";
            this.colCustomer.FillWeight = 90;
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colStudio.HeaderText = "Studio";
            this.colStudio.FillWeight = 65;
            this.colStudio.Name = "colStudio";
            this.colStudio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colStart.HeaderText = "Start";
            this.colStart.FillWeight = 100;
            this.colStart.Name = "colStart";
            this.colStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colAmount.HeaderText = "Amount";
            this.colAmount.FillWeight = 60;
            this.colAmount.Name = "colAmount";
            this.colAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 70;
            this.colStatus.Name = "colStatus";
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // colActions — styled exactly like Inventory's
            this.colActions.HeaderText = "Actions";
            this.colActions.Name = "colActions";
            this.colActions.FillWeight = 100;
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.colActions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.dgvBookings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colCustomer,
                this.colStudio,
                this.colStart,
                this.colAmount,
                this.colStatus,
                this.colActions
            });

            this.pnlBody.Controls.Add(this.dgvBookings);

            // pnlFooter
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 50;
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Name = "pnlFooter";

            // lblCount
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(30, 15);
            this.lblCount.Text = "0 booking(s)";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.Name = "lblCount";

            this.pnlFooter.Controls.Add(this.lblCount);

            // ==== FORM ASSEMBLY — ORDER MATTERS ====
            // Fill must be added FIRST so it takes the remaining space.
            // Top/Bottom are added AFTER so they keep their edges.
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnReschedule;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNewBooking;
        private System.Windows.Forms.Panel pnlBody;                 // 👈 NEW field
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}