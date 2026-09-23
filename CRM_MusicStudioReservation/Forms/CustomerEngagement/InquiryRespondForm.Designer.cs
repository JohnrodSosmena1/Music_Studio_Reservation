namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class InquiryRespondForm
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

            this.lblOriginal = new System.Windows.Forms.Label();
            this.txtOriginal = new System.Windows.Forms.TextBox();

            this.lblResponse = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();

            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== InquiryRespondForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 620);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InquiryRespondForm";
            this.Text = "Respond to Inquiry";
            this.Load += new System.EventHandler(this.InquiryRespondForm_Load);

            // ============ HEADER ============
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 70);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 28);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "Respond to Inquiry";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 18);
            this.lblSubheader.Location = new System.Drawing.Point(30, 45);
            this.lblSubheader.Text = "Send a reply to the customer";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ============ BODY ============
            this.pnlBody.Location = new System.Drawing.Point(0, 70);
            this.pnlBody.Size = new System.Drawing.Size(620, 470);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Original (read-only) ----
            this.lblOriginal.AutoSize = false;
            this.lblOriginal.Size = new System.Drawing.Size(560, 16);
            this.lblOriginal.Location = new System.Drawing.Point(30, 15);
            this.lblOriginal.Text = "Customer's Message";
            this.lblOriginal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginal.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblOriginal.Name = "lblOriginal";

            this.txtOriginal.Size = new System.Drawing.Size(560, 100);
            this.txtOriginal.Location = new System.Drawing.Point(30, 33);
            this.txtOriginal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOriginal.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.txtOriginal.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.txtOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOriginal.Multiline = true;
            this.txtOriginal.ReadOnly = true;
            this.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOriginal.Name = "txtOriginal";

            // ---- Response ----
            this.lblResponse.AutoSize = false;
            this.lblResponse.Size = new System.Drawing.Size(560, 16);
            this.lblResponse.Location = new System.Drawing.Point(30, 145);
            this.lblResponse.Text = "Your Response *";
            this.lblResponse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResponse.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblResponse.Name = "lblResponse";

            this.txtResponse.Size = new System.Drawing.Size(560, 180);
            this.txtResponse.Location = new System.Drawing.Point(30, 163);
            this.txtResponse.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResponse.Multiline = true;
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Name = "txtResponse";

            // ---- Status ----
            this.lblStatus.AutoSize = false;
            this.lblStatus.Size = new System.Drawing.Size(260, 16);
            this.lblStatus.Location = new System.Drawing.Point(30, 355);
            this.lblStatus.Text = "Set Status To *";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStatus.Name = "lblStatus";

            this.cmbStatus.Size = new System.Drawing.Size(260, 26);
            this.cmbStatus.Location = new System.Drawing.Point(30, 373);
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Name = "cmbStatus";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 410);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblOriginal);
            this.pnlBody.Controls.Add(this.txtOriginal);
            this.pnlBody.Controls.Add(this.lblResponse);
            this.pnlBody.Controls.Add(this.txtResponse);
            this.pnlBody.Controls.Add(this.lblStatus);
            this.pnlBody.Controls.Add(this.cmbStatus);
            this.pnlBody.Controls.Add(this.lblError);

            // ============ FOOTER ============
            this.pnlFooter.Location = new System.Drawing.Point(0, 540);
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

            this.btnSend.Size = new System.Drawing.Size(130, 42);
            this.btnSend.Location = new System.Drawing.Point(480, 18);
            this.btnSend.Text = "Send Reply";
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Name = "btnSend";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSend);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.TextBox txtOriginal;

        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.TextBox txtResponse;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSend;
    }
}