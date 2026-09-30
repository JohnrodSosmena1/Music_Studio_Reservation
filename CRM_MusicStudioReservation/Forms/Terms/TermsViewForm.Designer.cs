namespace CRM_MusicStudioReservation.Forms.Terms
{
    partial class TermsViewForm
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMeta = new System.Windows.Forms.Panel();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblApprover = new System.Windows.Forms.Label();
            this.lblReAccept = new System.Windows.Forms.Label();
            this.lblChangeNotesHeader = new System.Windows.Forms.Label();
            this.lblChangeNotes = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlMeta.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== TermsViewForm ====
            this.ClientSize = new System.Drawing.Size(780, 680);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terms & Conditions Viewer";
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 85;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 15, 24, 10);

            this.lblCode.Location = new System.Drawing.Point(24, 12);
            this.lblCode.Size = new System.Drawing.Size(120, 22);
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);

            this.lblType.Location = new System.Drawing.Point(150, 12);
            this.lblType.Size = new System.Drawing.Size(200, 22);
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            this.lblVersion.Location = new System.Drawing.Point(550, 12);
            this.lblVersion.Size = new System.Drawing.Size(80, 22);
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.lblStatus.Location = new System.Drawing.Point(640, 12);
            this.lblStatus.Size = new System.Drawing.Size(110, 22);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.lblTitle.Location = new System.Drawing.Point(24, 40);
            this.lblTitle.Size = new System.Drawing.Size(730, 35);
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);

            this.pnlHeader.Controls.Add(this.lblCode);
            this.pnlHeader.Controls.Add(this.lblType);
            this.pnlHeader.Controls.Add(this.lblVersion);
            this.pnlHeader.Controls.Add(this.lblStatus);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // ==== Meta Panel ====
            this.pnlMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMeta.Height = 90;
            this.pnlMeta.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.pnlMeta.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);

            this.lblAuthor.Location = new System.Drawing.Point(24, 8);
            this.lblAuthor.Size = new System.Drawing.Size(350, 20);
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.lblApprover.Location = new System.Drawing.Point(390, 8);
            this.lblApprover.Size = new System.Drawing.Size(350, 20);
            this.lblApprover.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblApprover.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.lblReAccept.Location = new System.Drawing.Point(24, 32);
            this.lblReAccept.Size = new System.Drawing.Size(350, 20);
            this.lblReAccept.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReAccept.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);

            this.lblChangeNotesHeader.Location = new System.Drawing.Point(24, 56);
            this.lblChangeNotesHeader.Size = new System.Drawing.Size(100, 20);
            this.lblChangeNotesHeader.Text = "Version Notes:";
            this.lblChangeNotesHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChangeNotesHeader.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.lblChangeNotes.Location = new System.Drawing.Point(130, 56);
            this.lblChangeNotes.Size = new System.Drawing.Size(610, 30);
            this.lblChangeNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChangeNotes.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            this.pnlMeta.Controls.Add(this.lblAuthor);
            this.pnlMeta.Controls.Add(this.lblApprover);
            this.pnlMeta.Controls.Add(this.lblReAccept);
            this.pnlMeta.Controls.Add(this.lblChangeNotesHeader);
            this.pnlMeta.Controls.Add(this.lblChangeNotes);

            // ==== Content Box ====
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.ReadOnly = true;
            this.txtContent.BackColor = System.Drawing.Color.White;
            this.txtContent.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtContent.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.txtContent.Padding = new System.Windows.Forms.Padding(15);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 60;
            this.pnlFooter.BackColor = System.Drawing.Color.White;

            this.btnClose.Location = new System.Drawing.Point(645, 12);
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlFooter.Controls.Add(this.btnClose);

            // ==== Assemble ====
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.pnlMeta);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);

            this.pnlHeader.ResumeLayout(false);
            this.pnlMeta.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMeta;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblApprover;
        private System.Windows.Forms.Label lblReAccept;
        private System.Windows.Forms.Label lblChangeNotesHeader;
        private System.Windows.Forms.Label lblChangeNotes;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnClose;
    }
}
