namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class ReschedulePickerForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();

            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReschedule = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.SuspendLayout();

            // ReschedulePickerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReschedulePickerForm";
            this.Text = "Reschedule a Booking";

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(700, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Reschedule a Booking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblSubtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(700, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Text = "Pick a booking below, then choose a new date and time";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(30, 90);
            this.txtSearch.Size = new System.Drawing.Size(700, 28);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by booking code or customer...";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // dgvBookings
            this.dgvBookings.Location = new System.Drawing.Point(30, 130);
            this.dgvBookings.Size = new System.Drawing.Size(700, 340);
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.AllowUserToResizeRows = false;
            this.dgvBookings.RowHeadersVisible = false;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookings.ColumnHeadersHeight = 36;
            this.dgvBookings.RowTemplate.Height = 36;
            this.dgvBookings.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvBookings.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvBookings.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvBookings.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvBookings.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvBookings.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBookings_CellDoubleClick);

            // Columns
            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 100;
            this.colCode.Name = "colCode";

            this.colCustomer.HeaderText = "Customer";
            this.colCustomer.FillWeight = 100;
            this.colCustomer.Name = "colCustomer";

            this.colStudio.HeaderText = "Studio";
            this.colStudio.FillWeight = 80;
            this.colStudio.Name = "colStudio";

            this.colStart.HeaderText = "Current Start";
            this.colStart.FillWeight = 130;
            this.colStart.Name = "colStart";

            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 90;
            this.colStatus.Name = "colStatus";

            this.dgvBookings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colCustomer,
                this.colStudio,
                this.colStart,
                this.colStatus
            });

            // lblError
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(700, 25);
            this.lblError.Location = new System.Drawing.Point(30, 480);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // btnCancel
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Location = new System.Drawing.Point(30, 505);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnReschedule
            this.btnReschedule.Size = new System.Drawing.Size(200, 40);
            this.btnReschedule.Location = new System.Drawing.Point(530, 505);
            this.btnReschedule.Text = "Reschedule Selected";
            this.btnReschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReschedule.FlatAppearance.BorderSize = 0;
            this.btnReschedule.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnReschedule.ForeColor = System.Drawing.Color.White;
            this.btnReschedule.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReschedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvBookings);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReschedule);

            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReschedule;
    }
}