namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class ReviewReplyForm
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

            this.lblOriginalComment = new System.Windows.Forms.Label();
            this.txtOriginalComment = new System.Windows.Forms.TextBox();

            this.lblReply = new System.Windows.Forms.Label();
            this.txtReply = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== ReviewReplyForm — small ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 460);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReviewReplyForm";
            this.Text = "Reply to Review";
            this.Load += new System.EventHandler(this.ReviewReplyForm_Load);

            // ==== HEADER ====
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 70);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 28);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "Reply to Review";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 18);
            this.lblSubheader.Location = new System.Drawing.Point(30, 45);
            this.lblSubheader.Text = "Your reply will be visible to the customer";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ==== BODY ====
            this.pnlBody.Location = new System.Drawing.Point(0, 70);
            this.pnlBody.Size = new System.Drawing.Size(620, 310);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Original Comment (read-only) ----
            this.lblOriginalComment.AutoSize = false;
            this.lblOriginalComment.Size = new System.Drawing.Size(560, 16);
            this.lblOriginalComment.Location = new System.Drawing.Point(30, 15);
            this.lblOriginalComment.Text = "Original Comment";
            this.lblOriginalComment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginalComment.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblOriginalComment.Name = "lblOriginalComment";

            this.txtOriginalComment.Size = new System.Drawing.Size(560, 80);
            this.txtOriginalComment.Location = new System.Drawing.Point(30, 33);
            this.txtOriginalComment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOriginalComment.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.txtOriginalComment.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.txtOriginalComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOriginalComment.Multiline = true;
            this.txtOriginalComment.ReadOnly = true;
            this.txtOriginalComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOriginalComment.Name = "txtOriginalComment";

            // ---- Reply ----
            this.lblReply.AutoSize = false;
            this.lblReply.Size = new System.Drawing.Size(560, 16);
            this.lblReply.Location = new System.Drawing.Point(30, 125);
            this.lblReply.Text = "Your Reply *";
            this.lblReply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReply.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblReply.Name = "lblReply";

            this.txtReply.Size = new System.Drawing.Size(560, 120);
            this.txtReply.Location = new System.Drawing.Point(30, 143);
            this.txtReply.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReply.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReply.Multiline = true;
            this.txtReply.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReply.Name = "txtReply";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 270);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblOriginalComment);
            this.pnlBody.Controls.Add(this.txtOriginalComment);
            this.pnlBody.Controls.Add(this.lblReply);
            this.pnlBody.Controls.Add(this.txtReply);
            this.pnlBody.Controls.Add(this.lblError);

            // ==== FOOTER ====
            this.pnlFooter.Location = new System.Drawing.Point(0, 380);
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

        private System.Windows.Forms.Label lblOriginalComment;
        private System.Windows.Forms.TextBox txtOriginalComment;

        private System.Windows.Forms.Label lblReply;
        private System.Windows.Forms.TextBox txtReply;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSend;
    }
}