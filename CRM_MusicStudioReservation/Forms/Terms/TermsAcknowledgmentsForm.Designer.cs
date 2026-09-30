namespace CRM_MusicStudioReservation.Forms.Terms
{
    partial class TermsAcknowledgmentsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvAcks = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserAgent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcks)).BeginInit();
            this.SuspendLayout();

            // ==== TermsAcknowledgmentsForm ====
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client Acknowledgment Audit Log";
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Load += new System.EventHandler(this.TermsAcknowledgmentsForm_Load);

            // ==== Top Header ====
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 75;
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Padding = new System.Windows.Forms.Padding(25, 12, 25, 10);

            this.lblTitle.Text = "Terms & Conditions Acknowledgment Log";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Location = new System.Drawing.Point(24, 12);
            this.lblTitle.Size = new System.Drawing.Size(700, 30);

            this.lblSubtitle.Text = "Legal compliance & audit trail of customer acceptance";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Location = new System.Drawing.Point(24, 44);
            this.lblSubtitle.Size = new System.Drawing.Size(700, 20);

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblSubtitle);

            // ==== Bottom Bar ====
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 55;
            this.pnlBottom.BackColor = System.Drawing.Color.White;

            this.lblCount.Location = new System.Drawing.Point(25, 18);
            this.lblCount.Size = new System.Drawing.Size(350, 20);
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            this.btnClose.Location = new System.Drawing.Point(820, 10);
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlBottom.Controls.Add(this.lblCount);
            this.pnlBottom.Controls.Add(this.btnClose);

            // ==== Grid ====
            this.dgvAcks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAcks.BackgroundColor = System.Drawing.Color.White;
            this.dgvAcks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAcks.RowHeadersVisible = false;
            this.dgvAcks.AllowUserToAddRows = false;
            this.dgvAcks.AllowUserToDeleteRows = false;
            this.dgvAcks.ReadOnly = true;
            this.dgvAcks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 40;
            this.colCode.HeaderText = "Policy Code";
            this.colCode.FillWeight = 80;
            this.colVersion.HeaderText = "Version";
            this.colVersion.FillWeight = 50;
            this.colCustomerId.HeaderText = "Customer ID";
            this.colCustomerId.FillWeight = 70;
            this.colContext.HeaderText = "Context";
            this.colContext.FillWeight = 70;
            this.colTimestamp.HeaderText = "Acknowledged At";
            this.colTimestamp.FillWeight = 110;
            this.colIp.HeaderText = "IP Address";
            this.colIp.FillWeight = 90;
            this.colUserAgent.HeaderText = "Client / Device";
            this.colUserAgent.FillWeight = 130;

            this.dgvAcks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colCode, this.colVersion, this.colCustomerId,
                this.colContext, this.colTimestamp, this.colIp, this.colUserAgent
            });

            // ==== Assemble ====
            this.Controls.Add(this.dgvAcks);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);

            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcks)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvAcks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContext;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserAgent;
    }
}
