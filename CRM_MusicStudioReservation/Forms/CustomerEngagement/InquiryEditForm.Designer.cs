namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class InquiryEditForm
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubheader = new System.Windows.Forms.Label();

            this.pnlBody = new System.Windows.Forms.Panel();

            this.lblCustomer = new System.Windows.Forms.Label();
            this.numCustomerId = new System.Windows.Forms.NumericUpDown();

            this.lblPriority = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();

            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();

            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerId)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== InquiryEditForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InquiryEditForm";
            this.Text = "Inquiry";
            this.Load += new System.EventHandler(this.InquiryEditForm_Load);

            // ============ HEADER ============
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 70);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 28);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "New Inquiry";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 18);
            this.lblSubheader.Location = new System.Drawing.Point(30, 45);
            this.lblSubheader.Text = "Record a customer inquiry";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ============ BODY ============
            this.pnlBody.Location = new System.Drawing.Point(0, 70);
            this.pnlBody.Size = new System.Drawing.Size(620, 410);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Customer ID ----
            this.lblCustomer.AutoSize = false;
            this.lblCustomer.Size = new System.Drawing.Size(260, 16);
            this.lblCustomer.Location = new System.Drawing.Point(30, 15);
            this.lblCustomer.Text = "Customer ID *";
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomer.Name = "lblCustomer";

            this.numCustomerId.Size = new System.Drawing.Size(260, 26);
            this.numCustomerId.Location = new System.Drawing.Point(30, 33);
            this.numCustomerId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCustomerId.Minimum = 1;
            this.numCustomerId.Maximum = 1000000;
            this.numCustomerId.Value = 1;
            this.numCustomerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCustomerId.Name = "numCustomerId";

            // ---- Priority ----
            this.lblPriority.AutoSize = false;
            this.lblPriority.Size = new System.Drawing.Size(260, 16);
            this.lblPriority.Location = new System.Drawing.Point(330, 15);
            this.lblPriority.Text = "Priority *";
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPriority.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblPriority.Name = "lblPriority";

            this.cmbPriority.Size = new System.Drawing.Size(260, 26);
            this.cmbPriority.Location = new System.Drawing.Point(330, 33);
            this.cmbPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPriority.Name = "cmbPriority";

            // ---- Subject ----
            this.lblSubject.AutoSize = false;
            this.lblSubject.Size = new System.Drawing.Size(560, 16);
            this.lblSubject.Location = new System.Drawing.Point(30, 75);
            this.lblSubject.Text = "Subject *";
            this.lblSubject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubject.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubject.Name = "lblSubject";

            this.txtSubject.Size = new System.Drawing.Size(560, 26);
            this.txtSubject.Location = new System.Drawing.Point(30, 93);
            this.txtSubject.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubject.Name = "txtSubject";

            // ---- Message ----
            this.lblMessage.AutoSize = false;
            this.lblMessage.Size = new System.Drawing.Size(560, 16);
            this.lblMessage.Location = new System.Drawing.Point(30, 135);
            this.lblMessage.Text = "Message *";
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblMessage.Name = "lblMessage";

            this.txtMessage.Size = new System.Drawing.Size(560, 180);
            this.txtMessage.Location = new System.Drawing.Point(30, 153);
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Multiline = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Name = "txtMessage";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 340);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCustomer);
            this.pnlBody.Controls.Add(this.numCustomerId);
            this.pnlBody.Controls.Add(this.lblPriority);
            this.pnlBody.Controls.Add(this.cmbPriority);
            this.pnlBody.Controls.Add(this.lblSubject);
            this.pnlBody.Controls.Add(this.txtSubject);
            this.pnlBody.Controls.Add(this.lblMessage);
            this.pnlBody.Controls.Add(this.txtMessage);
            this.pnlBody.Controls.Add(this.lblError);

            // ============ FOOTER ============
            this.pnlFooter.Location = new System.Drawing.Point(0, 480);
            this.pnlFooter.Size = new System.Drawing.Size(620, 80);
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Name = "pnlFooter";

            this.btnCancel.Size = new System.Drawing.Size(130, 42);
            this.btnCancel.Location = new System.Drawing.Point(340, 18);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.Size = new System.Drawing.Size(130, 42);
            this.btnSave.Location = new System.Drawing.Point(480, 18);
            this.btnSave.Text = "Save";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerId)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.NumericUpDown numCustomerId;

        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cmbPriority;

        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}