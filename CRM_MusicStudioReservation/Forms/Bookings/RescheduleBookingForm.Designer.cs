namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class RescheduleBookingForm
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

            this.pnlCurrent = new System.Windows.Forms.Panel();
            this.lblCurrentLabel = new System.Windows.Forms.Label();
            this.lblCurrentValue = new System.Windows.Forms.Label();

            this.lblNewDate = new System.Windows.Forms.Label();
            this.dtpNewDate = new System.Windows.Forms.DateTimePicker();

            this.lblNewTime = new System.Windows.Forms.Label();
            this.cmbNewStart = new System.Windows.Forms.ComboBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.cmbNewEnd = new System.Windows.Forms.ComboBox();

            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();

            this.pnlCurrent.SuspendLayout();
            this.SuspendLayout();

            // RescheduleBookingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 480);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RescheduleBookingForm";
            this.Text = "Reschedule Booking";
            this.Load += new System.EventHandler(this.RescheduleBookingForm_Load);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(440, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Reschedule Booking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblSubtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(440, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Text = "BKG-XXXXXX";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // pnlCurrent (shows current date/time)
            this.pnlCurrent.Location = new System.Drawing.Point(30, 95);
            this.pnlCurrent.Size = new System.Drawing.Size(440, 70);
            this.pnlCurrent.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCurrent.Name = "pnlCurrent";

            // lblCurrentLabel
            this.lblCurrentLabel.AutoSize = true;
            this.lblCurrentLabel.Location = new System.Drawing.Point(15, 10);
            this.lblCurrentLabel.Text = "Current Schedule";
            this.lblCurrentLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCurrentLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCurrentLabel.Name = "lblCurrentLabel";

            // lblCurrentValue
            this.lblCurrentValue.AutoSize = false;
            this.lblCurrentValue.Size = new System.Drawing.Size(400, 30);
            this.lblCurrentValue.Location = new System.Drawing.Point(15, 30);
            this.lblCurrentValue.Text = "—";
            this.lblCurrentValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrentValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblCurrentValue.Name = "lblCurrentValue";

            this.pnlCurrent.Controls.Add(this.lblCurrentLabel);
            this.pnlCurrent.Controls.Add(this.lblCurrentValue);

            // lblNewDate
            this.lblNewDate.AutoSize = true;
            this.lblNewDate.Location = new System.Drawing.Point(30, 190);
            this.lblNewDate.Text = "New Date *";
            this.lblNewDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewDate.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNewDate.Name = "lblNewDate";

            // dtpNewDate
            this.dtpNewDate.Location = new System.Drawing.Point(30, 210);
            this.dtpNewDate.Size = new System.Drawing.Size(440, 28);
            this.dtpNewDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNewDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNewDate.Name = "dtpNewDate";

            // lblNewTime
            this.lblNewTime.AutoSize = true;
            this.lblNewTime.Location = new System.Drawing.Point(30, 255);
            this.lblNewTime.Text = "New Time Slot *";
            this.lblNewTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewTime.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNewTime.Name = "lblNewTime";

            // cmbNewStart
            this.cmbNewStart.Location = new System.Drawing.Point(30, 275);
            this.cmbNewStart.Size = new System.Drawing.Size(200, 28);
            this.cmbNewStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStart.Name = "cmbNewStart";
            this.cmbNewStart.SelectedIndexChanged += new System.EventHandler(this.cmbNewStart_SelectedIndexChanged);

            // lblTo
            this.lblTo.AutoSize = false;
            this.lblTo.Size = new System.Drawing.Size(30, 28);
            this.lblTo.Location = new System.Drawing.Point(240, 280);
            this.lblTo.Text = "to";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTo.Name = "lblTo";

            // cmbNewEnd
            this.cmbNewEnd.Location = new System.Drawing.Point(270, 275);
            this.cmbNewEnd.Size = new System.Drawing.Size(200, 28);
            this.cmbNewEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewEnd.Name = "cmbNewEnd";

            // lblError
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(440, 25);
            this.lblError.Location = new System.Drawing.Point(30, 320);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // btnCancel
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Location = new System.Drawing.Point(30, 415);
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

            // btnConfirm
            this.btnConfirm.Size = new System.Drawing.Size(140, 40);
            this.btnConfirm.Location = new System.Drawing.Point(330, 415);
            this.btnConfirm.Text = "Confirm Reschedule";
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pnlCurrent);
            this.Controls.Add(this.lblNewDate);
            this.Controls.Add(this.dtpNewDate);
            this.Controls.Add(this.lblNewTime);
            this.Controls.Add(this.cmbNewStart);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.cmbNewEnd);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);

            this.pnlCurrent.ResumeLayout(false);
            this.pnlCurrent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlCurrent;
        private System.Windows.Forms.Label lblCurrentLabel;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.Label lblNewDate;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
        private System.Windows.Forms.Label lblNewTime;
        private System.Windows.Forms.ComboBox cmbNewStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ComboBox cmbNewEnd;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
    }
}