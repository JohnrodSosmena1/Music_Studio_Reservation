namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class CustomerEngagementForm
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
            this.btnTabPromotions = new System.Windows.Forms.Button();
            this.btnTabFeedback = new System.Windows.Forms.Button();
            this.btnTabReviews = new System.Windows.Forms.Button();
            this.btnTabLoyalty = new System.Windows.Forms.Button();
            this.btnTabInquiries = new System.Windows.Forms.Button();

            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();

            // ==== CustomerEngagementForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerEngagementForm";
            this.Text = "Customer Engagement";
            this.Load += new System.EventHandler(this.CustomerEngagementForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(900, 40);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Text = "Customer Engagement";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(900, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 62);
            this.lblSubtitle.Text = "Loyalty, promotions, feedback, and customer interactions";
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

            // -- Tab 1: Promotions --
            this.btnTabPromotions.AutoSize = false;
            this.btnTabPromotions.Size = new System.Drawing.Size(160, 60);
            this.btnTabPromotions.Location = new System.Drawing.Point(30, 0);
            this.btnTabPromotions.Text = "🎁  Promotions";
            this.btnTabPromotions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabPromotions.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnTabPromotions.BackColor = System.Drawing.Color.White;
            this.btnTabPromotions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabPromotions.FlatAppearance.BorderSize = 0;
            this.btnTabPromotions.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabPromotions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabPromotions.Name = "btnTabPromotions";
            this.btnTabPromotions.Click += new System.EventHandler(this.btnTabPromotions_Click);

            // -- Tab 2: Feedback --
            this.btnTabFeedback.AutoSize = false;
            this.btnTabFeedback.Size = new System.Drawing.Size(160, 60);
            this.btnTabFeedback.Location = new System.Drawing.Point(190, 0);
            this.btnTabFeedback.Text = "💬  Feedback";
            this.btnTabFeedback.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabFeedback.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabFeedback.BackColor = System.Drawing.Color.White;
            this.btnTabFeedback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabFeedback.FlatAppearance.BorderSize = 0;
            this.btnTabFeedback.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabFeedback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabFeedback.Name = "btnTabFeedback";
            this.btnTabFeedback.Click += new System.EventHandler(this.btnTabFeedback_Click);

            // -- Tab 3: Reviews --
            this.btnTabReviews.AutoSize = false;
            this.btnTabReviews.Size = new System.Drawing.Size(160, 60);
            this.btnTabReviews.Location = new System.Drawing.Point(350, 0);
            this.btnTabReviews.Text = "⭐  Reviews";
            this.btnTabReviews.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabReviews.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabReviews.BackColor = System.Drawing.Color.White;
            this.btnTabReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabReviews.FlatAppearance.BorderSize = 0;
            this.btnTabReviews.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabReviews.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabReviews.Name = "btnTabReviews";
            this.btnTabReviews.Click += new System.EventHandler(this.btnTabReviews_Click);

            // -- Tab 4: Loyalty --
            this.btnTabLoyalty.AutoSize = false;
            this.btnTabLoyalty.Size = new System.Drawing.Size(160, 60);
            this.btnTabLoyalty.Location = new System.Drawing.Point(510, 0);
            this.btnTabLoyalty.Text = "🏆  Loyalty";
            this.btnTabLoyalty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabLoyalty.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabLoyalty.BackColor = System.Drawing.Color.White;
            this.btnTabLoyalty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabLoyalty.FlatAppearance.BorderSize = 0;
            this.btnTabLoyalty.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabLoyalty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabLoyalty.Name = "btnTabLoyalty";
            this.btnTabLoyalty.Click += new System.EventHandler(this.btnTabLoyalty_Click);

            // -- Tab 5: Inquiries --
            this.btnTabInquiries.AutoSize = false;
            this.btnTabInquiries.Size = new System.Drawing.Size(160, 60);
            this.btnTabInquiries.Location = new System.Drawing.Point(670, 0);
            this.btnTabInquiries.Text = "📩  Inquiries";
            this.btnTabInquiries.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabInquiries.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnTabInquiries.BackColor = System.Drawing.Color.White;
            this.btnTabInquiries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabInquiries.FlatAppearance.BorderSize = 0;
            this.btnTabInquiries.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTabInquiries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabInquiries.Name = "btnTabInquiries";
            this.btnTabInquiries.Click += new System.EventHandler(this.btnTabInquiries_Click);

            this.pnlTabs.Controls.Add(this.btnTabPromotions);
            this.pnlTabs.Controls.Add(this.btnTabFeedback);
            this.pnlTabs.Controls.Add(this.btnTabReviews);
            this.pnlTabs.Controls.Add(this.btnTabLoyalty);
            this.pnlTabs.Controls.Add(this.btnTabInquiries);

            // ==== Content Area ====
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
        private System.Windows.Forms.Button btnTabPromotions;
        private System.Windows.Forms.Button btnTabFeedback;
        private System.Windows.Forms.Button btnTabReviews;
        private System.Windows.Forms.Button btnTabLoyalty;
        private System.Windows.Forms.Button btnTabInquiries;

        private System.Windows.Forms.Panel pnlContent;
    }
}