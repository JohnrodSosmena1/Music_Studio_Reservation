namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    partial class StaffDashboardForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlStats = new System.Windows.Forms.Panel();

            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTodaySchedule = new System.Windows.Forms.Panel();
            this.lblTodayScheduleTitle = new System.Windows.Forms.Label();
            this.pnlScheduleContent = new System.Windows.Forms.Panel();

            this.pnlQuickActions = new System.Windows.Forms.Panel();
            this.lblQuickActionsTitle = new System.Windows.Forms.Label();
            this.btnCheckInClient = new System.Windows.Forms.Button();
            this.btnCheckOutClient = new System.Windows.Forms.Button();
            this.btnViewBookings = new System.Windows.Forms.Button();
            this.btnViewAvailability = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlTodaySchedule.SuspendLayout();
            this.pnlQuickActions.SuspendLayout();
            this.SuspendLayout();

            // ==== StaffDashboardForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StaffDashboardForm";
            this.Text = "Staff Dashboard";
            this.Load += new System.EventHandler(this.StaffDashboardForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";

            this.lblWelcome.AutoSize = false;
            this.lblWelcome.Size = new System.Drawing.Size(900, 40);
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Text = "Staff Dashboard";
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblWelcome.Name = "lblWelcome";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(900, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Manage today's bookings and studio operations";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Controls.Add(this.lblSubtitle);

            // ==== Stats Row ====
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height = 140;
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);

            // ==== Main Content ====
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30, 10, 30, 30);

            // ==== Today's Schedule (Left, 65%) ====
            this.pnlTodaySchedule.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTodaySchedule.Width = 660;
            this.pnlTodaySchedule.BackColor = System.Drawing.Color.White;
            this.pnlTodaySchedule.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTodaySchedule.Name = "pnlTodaySchedule";

            this.lblTodayScheduleTitle.AutoSize = false;
            this.lblTodayScheduleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTodayScheduleTitle.Height = 40;
            this.lblTodayScheduleTitle.Text = "Today's Schedule";
            this.lblTodayScheduleTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTodayScheduleTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTodayScheduleTitle.Name = "lblTodayScheduleTitle";

            this.pnlScheduleContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScheduleContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlScheduleContent.AutoScroll = true;
            this.pnlScheduleContent.Name = "pnlScheduleContent";

            this.pnlTodaySchedule.Controls.Add(this.pnlScheduleContent);
            this.pnlTodaySchedule.Controls.Add(this.lblTodayScheduleTitle);

            // ==== Quick Actions (Right, 35%) ====
            this.pnlQuickActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuickActions.BackColor = System.Drawing.Color.White;
            this.pnlQuickActions.Padding = new System.Windows.Forms.Padding(20);
            this.pnlQuickActions.Name = "pnlQuickActions";

            this.lblQuickActionsTitle.AutoSize = false;
            this.lblQuickActionsTitle.Size = new System.Drawing.Size(300, 40);
            this.lblQuickActionsTitle.Location = new System.Drawing.Point(20, 20);
            this.lblQuickActionsTitle.Text = "Quick Actions";
            this.lblQuickActionsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblQuickActionsTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblQuickActionsTitle.Name = "lblQuickActionsTitle";

            // Quick action buttons — larger, prominent
            this.btnCheckInClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckInClient.FlatAppearance.BorderSize = 0;
            this.btnCheckInClient.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCheckInClient.ForeColor = System.Drawing.Color.White;
            this.btnCheckInClient.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnCheckInClient.Location = new System.Drawing.Point(20, 70);
            this.btnCheckInClient.Size = new System.Drawing.Size(260, 55);
            this.btnCheckInClient.Text = "✓   Check-In Client";
            this.btnCheckInClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckInClient.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCheckInClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckInClient.Name = "btnCheckInClient";

            this.btnCheckOutClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckOutClient.FlatAppearance.BorderSize = 0;
            this.btnCheckOutClient.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCheckOutClient.ForeColor = System.Drawing.Color.White;
            this.btnCheckOutClient.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnCheckOutClient.Location = new System.Drawing.Point(20, 135);
            this.btnCheckOutClient.Size = new System.Drawing.Size(260, 55);
            this.btnCheckOutClient.Text = "←   Check-Out Client";
            this.btnCheckOutClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckOutClient.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCheckOutClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckOutClient.Name = "btnCheckOutClient";

            this.btnViewBookings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewBookings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnViewBookings.FlatAppearance.BorderSize = 1;
            this.btnViewBookings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewBookings.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnViewBookings.BackColor = System.Drawing.Color.White;
            this.btnViewBookings.Location = new System.Drawing.Point(20, 210);
            this.btnViewBookings.Size = new System.Drawing.Size(260, 45);
            this.btnViewBookings.Text = "📋   View Bookings";
            this.btnViewBookings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewBookings.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnViewBookings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewBookings.Name = "btnViewBookings";

            this.btnViewAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewAvailability.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnViewAvailability.FlatAppearance.BorderSize = 1;
            this.btnViewAvailability.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewAvailability.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnViewAvailability.BackColor = System.Drawing.Color.White;
            this.btnViewAvailability.Location = new System.Drawing.Point(20, 265);
            this.btnViewAvailability.Size = new System.Drawing.Size(260, 45);
            this.btnViewAvailability.Text = "🔍   View Studio Availability";
            this.btnViewAvailability.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewAvailability.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnViewAvailability.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewAvailability.Name = "btnViewAvailability";

            this.pnlQuickActions.Controls.Add(this.lblQuickActionsTitle);
            this.pnlQuickActions.Controls.Add(this.btnCheckInClient);
            this.pnlQuickActions.Controls.Add(this.btnCheckOutClient);
            this.pnlQuickActions.Controls.Add(this.btnViewBookings);
            this.pnlQuickActions.Controls.Add(this.btnViewAvailability);

            // ==== Assemble ====
            this.pnlMain.Controls.Add(this.pnlQuickActions);
            this.pnlMain.Controls.Add(this.pnlTodaySchedule);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlTodaySchedule.ResumeLayout(false);
            this.pnlQuickActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlStats;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTodaySchedule;
        private System.Windows.Forms.Label lblTodayScheduleTitle;
        private System.Windows.Forms.Panel pnlScheduleContent;

        private System.Windows.Forms.Panel pnlQuickActions;
        private System.Windows.Forms.Label lblQuickActionsTitle;
        private System.Windows.Forms.Button btnCheckInClient;
        private System.Windows.Forms.Button btnCheckOutClient;
        private System.Windows.Forms.Button btnViewBookings;
        private System.Windows.Forms.Button btnViewAvailability;
    }
}