namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class EditBookingForm
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
            this.lblBookingCode = new System.Windows.Forms.Label();

            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();

            this.lblStudio = new System.Windows.Forms.Label();
            this.cmbStudio = new System.Windows.Forms.ComboBox();

            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();

            this.lblStartTime = new System.Windows.Forms.Label();
            this.cmbStartTime = new System.Windows.Forms.ComboBox();

            this.lblEndTime = new System.Windows.Forms.Label();
            this.cmbEndTime = new System.Windows.Forms.ComboBox();

            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();

            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // EditBookingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 620);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditBookingForm";
            this.Text = "Edit Booking";
            this.Load += new System.EventHandler(this.EditBookingForm_Load);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Edit Booking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblBookingCode
            this.lblBookingCode.AutoSize = false;
            this.lblBookingCode.Size = new System.Drawing.Size(500, 22);
            this.lblBookingCode.Location = new System.Drawing.Point(30, 55);
            this.lblBookingCode.Text = "BKG-XXXXXX";
            this.lblBookingCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBookingCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblBookingCode.Name = "lblBookingCode";

            // lblCustomer
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(30, 95);
            this.lblCustomer.Text = "Customer *";
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblCustomer.Name = "lblCustomer";

            // cmbCustomer
            this.cmbCustomer.Location = new System.Drawing.Point(30, 115);
            this.cmbCustomer.Size = new System.Drawing.Size(500, 28);
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Name = "cmbCustomer";

            // lblStudio
            this.lblStudio.AutoSize = true;
            this.lblStudio.Location = new System.Drawing.Point(30, 155);
            this.lblStudio.Text = "Studio *";
            this.lblStudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStudio.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblStudio.Name = "lblStudio";

            // cmbStudio
            this.cmbStudio.Location = new System.Drawing.Point(30, 175);
            this.cmbStudio.Size = new System.Drawing.Size(500, 28);
            this.cmbStudio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudio.Name = "cmbStudio";

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(30, 215);
            this.lblDate.Text = "Date *";
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblDate.Name = "lblDate";

            // dtpDate
            this.dtpDate.Location = new System.Drawing.Point(30, 235);
            this.dtpDate.Size = new System.Drawing.Size(500, 28);
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDate.Name = "dtpDate";

            // lblStartTime
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(30, 275);
            this.lblStartTime.Text = "Start Time *";
            this.lblStartTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartTime.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblStartTime.Name = "lblStartTime";

            // cmbStartTime
            this.cmbStartTime.Location = new System.Drawing.Point(30, 295);
            this.cmbStartTime.Size = new System.Drawing.Size(240, 28);
            this.cmbStartTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStartTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartTime.Name = "cmbStartTime";
            this.cmbStartTime.SelectedIndexChanged += new System.EventHandler(this.cmbStartTime_SelectedIndexChanged);

            // lblEndTime
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(290, 275);
            this.lblEndTime.Text = "End Time *";
            this.lblEndTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEndTime.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblEndTime.Name = "lblEndTime";

            // cmbEndTime
            this.cmbEndTime.Location = new System.Drawing.Point(290, 295);
            this.cmbEndTime.Size = new System.Drawing.Size(240, 28);
            this.cmbEndTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEndTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndTime.Name = "cmbEndTime";

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(30, 335);
            this.lblStatus.Text = "Status *";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblStatus.Name = "lblStatus";

            // cmbStatus
            this.cmbStatus.Location = new System.Drawing.Point(30, 355);
            this.cmbStatus.Size = new System.Drawing.Size(500, 28);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Name = "cmbStatus";

            // lblNotes
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(30, 395);
            this.lblNotes.Text = "Notes";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNotes.Name = "lblNotes";

            // txtNotes
            this.txtNotes.Location = new System.Drawing.Point(30, 415);
            this.txtNotes.Size = new System.Drawing.Size(500, 60);
            this.txtNotes.Multiline = true;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Name = "txtNotes";

            // lblError
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(500, 25);
            this.lblError.Location = new System.Drawing.Point(30, 485);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // btnCancel
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Location = new System.Drawing.Point(30, 555);
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

            // btnSave
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Location = new System.Drawing.Point(410, 555);
            this.btnSave.Text = "Save";
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblBookingCode);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.lblStudio);
            this.Controls.Add(this.cmbStudio);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.cmbStartTime);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.cmbEndTime);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBookingCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblStudio;
        private System.Windows.Forms.ComboBox cmbStudio;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.ComboBox cmbStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.ComboBox cmbEndTime;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}