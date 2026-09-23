namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class CancelBookingForm
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
            this.lblWarning = new System.Windows.Forms.Label();

            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();
            this.btnKeep = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // CancelBookingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 420);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CancelBookingForm";
            this.Text = "Cancel Booking";

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(460, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Cancel Booking";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblSubtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(460, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 55);
            this.lblSubtitle.Text = "BKG-XXXXXX";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // lblWarning
            this.lblWarning.AutoSize = false;
            this.lblWarning.Size = new System.Drawing.Size(460, 50);
            this.lblWarning.Location = new System.Drawing.Point(30, 90);
            this.lblWarning.Text = "Are you sure you want to cancel this booking?\nThis action cannot be undone.";
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblWarning.Name = "lblWarning";

            // lblReason
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(30, 165);
            this.lblReason.Text = "Cancellation Reason (optional)";
            this.lblReason.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReason.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblReason.Name = "lblReason";

            // txtReason
            this.txtReason.Location = new System.Drawing.Point(30, 185);
            this.txtReason.Size = new System.Drawing.Size(460, 100);
            this.txtReason.Multiline = true;
            this.txtReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.PlaceholderText = "Example: Customer requested, no-show, weather, etc.";
            this.txtReason.Name = "txtReason";

            // lblError
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(460, 25);
            this.lblError.Location = new System.Drawing.Point(30, 300);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // btnKeep
            this.btnKeep.Size = new System.Drawing.Size(140, 40);
            this.btnKeep.Location = new System.Drawing.Point(30, 355);
            this.btnKeep.Text = "No, Keep Booking";
            this.btnKeep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeep.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnKeep.FlatAppearance.BorderSize = 1;
            this.btnKeep.BackColor = System.Drawing.Color.White;
            this.btnKeep.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnKeep.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKeep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKeep.Name = "btnKeep";
            this.btnKeep.Click += new System.EventHandler(this.btnKeep_Click);

            // btnCancel
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.Location = new System.Drawing.Point(350, 355);
            this.btnCancel.Text = "Yes, Cancel Booking";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnKeep);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.Button btnCancel;
    }
}