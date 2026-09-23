namespace CRM_MusicStudioReservation.Forms.Dashboards
{
    partial class ClientDashboardForm
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
            this.pnlUpcoming = new System.Windows.Forms.Panel();
            this.lblUpcomingTitle = new System.Windows.Forms.Label();
            this.pnlUpcomingContent = new System.Windows.Forms.Panel();

            this.pnlQuickActions = new System.Windows.Forms.Panel();
            this.lblQuickActionsTitle = new System.Windows.Forms.Label();
            this.btnBookStudio = new System.Windows.Forms.Button();
            this.btnMyBookings = new System.Windows.Forms.Button();
            this.btnCheckAvailability = new System.Windows.Forms.Button();
            this.pnlLoyaltyCard = new System.Windows.Forms.Panel();
            this.lblLoyaltyTitle = new System.Windows.Forms.Label();
            this.lblLoyaltyPoints = new System.Windows.Forms.Label();
            this.btnViewRewards = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlUpcoming.SuspendLayout();
            this.pnlQuickActions.SuspendLayout();
            this.pnlLoyaltyCard.SuspendLayout();
            this.SuspendLayout();

            // ==== ClientDashboardForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClientDashboardForm";
            this.Text = "Client Dashboard";
            this.Load += new System.EventHandler(this.ClientDashboardForm_Load);

            // ==== Header (Welcome) ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";

            this.lblWelcome.AutoSize = false;
            this.lblWelcome.Size = new System.Drawing.Size(800, 40);
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Text = "Hello, Client!";
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblWelcome.Name = "lblWelcome";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(800, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Welcome back to Music Studio";
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

            // ==== Main Content (Upcoming + Quick Actions) ====
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30, 10, 30, 30);

            // ==== Upcoming Bookings (Left, 65%) ====
            this.pnlUpcoming.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUpcoming.Width = 660;
            this.pnlUpcoming.BackColor = System.Drawing.Color.White;
            this.pnlUpcoming.Padding = new System.Windows.Forms.Padding(20);
            this.pnlUpcoming.Name = "pnlUpcoming";

            this.lblUpcomingTitle.AutoSize = false;
            this.lblUpcomingTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUpcomingTitle.Height = 40;
            this.lblUpcomingTitle.Text = "Upcoming Bookings";
            this.lblUpcomingTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUpcomingTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblUpcomingTitle.Name = "lblUpcomingTitle";

            this.pnlUpcomingContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUpcomingContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlUpcomingContent.AutoScroll = true;
            this.pnlUpcomingContent.Name = "pnlUpcomingContent";

            this.pnlUpcoming.Controls.Add(this.pnlUpcomingContent);
            this.pnlUpcoming.Controls.Add(this.lblUpcomingTitle);

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

            // Quick action buttons
            this.btnBookStudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookStudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnBookStudio.FlatAppearance.BorderSize = 1;
            this.btnBookStudio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBookStudio.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnBookStudio.BackColor = System.Drawing.Color.White;
            this.btnBookStudio.Location = new System.Drawing.Point(20, 70);
            this.btnBookStudio.Size = new System.Drawing.Size(260, 42);
            this.btnBookStudio.Text = "🎸   Book a Studio";
            this.btnBookStudio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBookStudio.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBookStudio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookStudio.Name = "btnBookStudio";

            this.btnMyBookings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyBookings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnMyBookings.FlatAppearance.BorderSize = 1;
            this.btnMyBookings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMyBookings.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnMyBookings.BackColor = System.Drawing.Color.White;
            this.btnMyBookings.Location = new System.Drawing.Point(20, 120);
            this.btnMyBookings.Size = new System.Drawing.Size(260, 42);
            this.btnMyBookings.Text = "📋   View My Bookings";
            this.btnMyBookings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMyBookings.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnMyBookings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMyBookings.Name = "btnMyBookings";

            this.btnCheckAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAvailability.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnCheckAvailability.FlatAppearance.BorderSize = 1;
            this.btnCheckAvailability.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckAvailability.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnCheckAvailability.BackColor = System.Drawing.Color.White;
            this.btnCheckAvailability.Location = new System.Drawing.Point(20, 170);
            this.btnCheckAvailability.Size = new System.Drawing.Size(260, 42);
            this.btnCheckAvailability.Text = "🔍   Check Availability";
            this.btnCheckAvailability.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckAvailability.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCheckAvailability.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckAvailability.Name = "btnCheckAvailability";

            // ==== Loyalty Card (bottom of quick actions) ====
            this.pnlLoyaltyCard.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.pnlLoyaltyCard.Location = new System.Drawing.Point(20, 240);
            this.pnlLoyaltyCard.Size = new System.Drawing.Size(260, 130);
            this.pnlLoyaltyCard.Name = "pnlLoyaltyCard";

            this.lblLoyaltyTitle.AutoSize = false;
            this.lblLoyaltyTitle.Size = new System.Drawing.Size(240, 25);
            this.lblLoyaltyTitle.Location = new System.Drawing.Point(15, 15);
            this.lblLoyaltyTitle.Text = "Earn More Points!";
            this.lblLoyaltyTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoyaltyTitle.ForeColor = System.Drawing.Color.White;
            this.lblLoyaltyTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblLoyaltyTitle.Name = "lblLoyaltyTitle";

            this.lblLoyaltyPoints.AutoSize = false;
            this.lblLoyaltyPoints.Size = new System.Drawing.Size(240, 50);
            this.lblLoyaltyPoints.Location = new System.Drawing.Point(15, 45);
            this.lblLoyaltyPoints.Text = "0 points";
            this.lblLoyaltyPoints.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLoyaltyPoints.ForeColor = System.Drawing.Color.White;
            this.lblLoyaltyPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblLoyaltyPoints.Name = "lblLoyaltyPoints";

            this.btnViewRewards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRewards.FlatAppearance.BorderSize = 0;
            this.btnViewRewards.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViewRewards.ForeColor = System.Drawing.Color.White;
            this.btnViewRewards.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.btnViewRewards.Location = new System.Drawing.Point(15, 95);
            this.btnViewRewards.Size = new System.Drawing.Size(120, 25);
            this.btnViewRewards.Text = "View Rewards";
            this.btnViewRewards.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewRewards.Name = "btnViewRewards";

            this.pnlLoyaltyCard.Controls.Add(this.lblLoyaltyTitle);
            this.pnlLoyaltyCard.Controls.Add(this.lblLoyaltyPoints);
            this.pnlLoyaltyCard.Controls.Add(this.btnViewRewards);

            this.pnlQuickActions.Controls.Add(this.lblQuickActionsTitle);
            this.pnlQuickActions.Controls.Add(this.btnBookStudio);
            this.pnlQuickActions.Controls.Add(this.btnMyBookings);
            this.pnlQuickActions.Controls.Add(this.btnCheckAvailability);
            this.pnlQuickActions.Controls.Add(this.pnlLoyaltyCard);

            // ==== Assemble ====
            this.pnlMain.Controls.Add(this.pnlQuickActions);
            this.pnlMain.Controls.Add(this.pnlUpcoming);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlUpcoming.ResumeLayout(false);
            this.pnlQuickActions.ResumeLayout(false);
            this.pnlLoyaltyCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlStats;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlUpcoming;
        private System.Windows.Forms.Label lblUpcomingTitle;
        private System.Windows.Forms.Panel pnlUpcomingContent;

        private System.Windows.Forms.Panel pnlQuickActions;
        private System.Windows.Forms.Label lblQuickActionsTitle;
        private System.Windows.Forms.Button btnBookStudio;
        private System.Windows.Forms.Button btnMyBookings;
        private System.Windows.Forms.Button btnCheckAvailability;
        private System.Windows.Forms.Panel pnlLoyaltyCard;
        private System.Windows.Forms.Label lblLoyaltyTitle;
        private System.Windows.Forms.Label lblLoyaltyPoints;
        private System.Windows.Forms.Button btnViewRewards;
    }
}