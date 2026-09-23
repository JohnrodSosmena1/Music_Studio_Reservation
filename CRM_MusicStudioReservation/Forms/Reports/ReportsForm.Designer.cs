namespace CRM.winforms.Forms.Reports
{
    partial class ReportsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabBooking = new System.Windows.Forms.Button();
            this.btnTabRevenue = new System.Windows.Forms.Button();

            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();

            // ==== ReportsForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportsForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.ReportsForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(900, 40);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Reports";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(900, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Business intelligence and analytics";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Tab Bar ====
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Height = 60;
            this.pnlTabs.BackColor = System.Drawing.Color.White;
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);

            this.btnTabBooking.AutoSize = false;
            this.btnTabBooking.Size = new System.Drawing.Size(180, 60);
            this.btnTabBooking.Location = new System.Drawing.Point(30, 0);
            this.btnTabBooking.Text = "📅  Booking Report";
            this.btnTabBooking.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabBooking.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnTabBooking.BackColor = System.Drawing.Color.White;
            this.btnTabBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabBooking.FlatAppearance.BorderSize = 0;
            this.btnTabBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabBooking.Name = "btnTabBooking";
            this.btnTabBooking.Click += new System.EventHandler(this.btnTabBooking_Click);

            this.btnTabRevenue.AutoSize = false;
            this.btnTabRevenue.Size = new System.Drawing.Size(180, 60);
            this.btnTabRevenue.Location = new System.Drawing.Point(210, 0);
            this.btnTabRevenue.Text = "💰  Revenue Report";
            this.btnTabRevenue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabRevenue.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabRevenue.BackColor = System.Drawing.Color.White;
            this.btnTabRevenue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabRevenue.FlatAppearance.BorderSize = 0;
            this.btnTabRevenue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabRevenue.Name = "btnTabRevenue";
            this.btnTabRevenue.Click += new System.EventHandler(this.btnTabRevenue_Click);

            this.pnlTabs.Controls.Add(this.btnTabBooking);
            this.pnlTabs.Controls.Add(this.btnTabRevenue);

            // ==== Content ====
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30, 20, 30, 30);
            this.pnlContent.Name = "pnlContent";

            // ==== Form assembly ====
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnTabBooking;
        private System.Windows.Forms.Button btnTabRevenue;

        private System.Windows.Forms.Panel pnlContent;
    }
}