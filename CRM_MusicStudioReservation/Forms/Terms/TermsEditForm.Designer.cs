namespace CRM_MusicStudioReservation.Forms.Terms
{
    partial class TermsEditForm
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.chkReAccept = new System.Windows.Forms.CheckBox();
            this.lblChangeNotes = new System.Windows.Forms.Label();
            this.txtChangeNotes = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // ==== TermsEditForm ====
            this.ClientSize = new System.Drawing.Size(760, 680);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terms & Conditions Editor";
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);

            // ==== Top Header ====
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 65;
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Padding = new System.Windows.Forms.Padding(25, 15, 25, 10);

            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblFormTitle.Location = new System.Drawing.Point(24, 15);

            this.pnlTop.Controls.Add(this.lblFormTitle);

            // ==== Policy Type ====
            this.lblType.Location = new System.Drawing.Point(28, 80);
            this.lblType.Size = new System.Drawing.Size(180, 20);
            this.lblType.Text = "Policy Type:";
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.cmbType.Location = new System.Drawing.Point(28, 102);
            this.cmbType.Size = new System.Drawing.Size(700, 28);
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ==== Title ====
            this.lblTitle.Location = new System.Drawing.Point(28, 140);
            this.lblTitle.Size = new System.Drawing.Size(180, 20);
            this.lblTitle.Text = "Policy Title:";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtTitle.Location = new System.Drawing.Point(28, 162);
            this.txtTitle.Size = new System.Drawing.Size(700, 28);
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ==== Content ====
            this.lblContent.Location = new System.Drawing.Point(28, 200);
            this.lblContent.Size = new System.Drawing.Size(200, 20);
            this.lblContent.Text = "Policy Content (Rich Text):";
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblContent.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtContent.Location = new System.Drawing.Point(28, 222);
            this.txtContent.Size = new System.Drawing.Size(700, 240);
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ==== Re-acceptance & Change Notes ====
            this.chkReAccept.Location = new System.Drawing.Point(28, 472);
            this.chkReAccept.Size = new System.Drawing.Size(700, 24);
            this.chkReAccept.Text = "Mandatory Re-acceptance (requires existing clients to acknowledge on next booking)";
            this.chkReAccept.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.chkReAccept.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);

            this.lblChangeNotes.Location = new System.Drawing.Point(28, 502);
            this.lblChangeNotes.Size = new System.Drawing.Size(250, 20);
            this.lblChangeNotes.Text = "Version Notes / Summary of Changes:";
            this.lblChangeNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChangeNotes.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);

            this.txtChangeNotes.Location = new System.Drawing.Point(28, 524);
            this.txtChangeNotes.Size = new System.Drawing.Size(700, 48);
            this.txtChangeNotes.Multiline = true;
            this.txtChangeNotes.Font = new System.Drawing.Font("Segoe UI", 9F);

            // ==== Error label ====
            this.lblError.Location = new System.Drawing.Point(28, 578);
            this.lblError.Size = new System.Drawing.Size(700, 24);
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Visible = false;

            // ==== Bottom Bar ====
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 65;
            this.pnlBottom.BackColor = System.Drawing.Color.White;

            this.btnCancel.Location = new System.Drawing.Point(470, 14);
            this.btnCancel.Size = new System.Drawing.Size(115, 38);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.Location = new System.Drawing.Point(600, 14);
            this.btnSave.Size = new System.Drawing.Size(128, 38);
            this.btnSave.Text = "Save Draft";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnSave);

            // ==== Form controls assemble ====
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtChangeNotes);
            this.Controls.Add(this.lblChangeNotes);
            this.Controls.Add(this.chkReAccept);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.pnlTop);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.CheckBox chkReAccept;
        private System.Windows.Forms.Label lblChangeNotes;
        private System.Windows.Forms.TextBox txtChangeNotes;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
