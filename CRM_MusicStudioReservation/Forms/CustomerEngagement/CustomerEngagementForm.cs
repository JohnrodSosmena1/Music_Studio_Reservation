using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.CustomerEngagement
{
    public partial class CustomerEngagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;

        private UserControl? _currentTab;
        private Button? _activeTabButton;

        // Tab colors
        private static readonly Color ActiveTabColor = Color.FromArgb(139, 92, 246);
        private static readonly Color InactiveTabColor = Color.FromArgb(107, 114, 128);
        private static readonly Color ActiveUnderlineColor = Color.FromArgb(139, 92, 246);

        public CustomerEngagementForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
        }

        private void CustomerEngagementForm_Load(object sender, EventArgs e)
        {
            // Default to Promotions tab
            ShowPromotions();
        }

        // ==================== TAB SWITCHING ====================

        private void SetActiveTab(Button tab)
        {
            // Reset all tabs to inactive style
            foreach (Control ctrl in pnlTabs.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.ForeColor = InactiveTabColor;
                    btn.BackColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                }
            }

            // Highlight the active tab
            tab.ForeColor = ActiveTabColor;
            tab.BackColor = Color.White;
            tab.FlatAppearance.BorderSize = 0;

            // Add bottom border to indicate active (via Paint event)
            tab.Paint -= TabButton_Paint;
            tab.Paint += TabButton_Paint;

            _activeTabButton = tab;
        }

        private void TabButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn != _activeTabButton) return;

            using var pen = new Pen(ActiveUnderlineColor, 3);
            e.Graphics.DrawLine(pen, 0, btn.Height - 3, btn.Width, btn.Height - 3);
        }

        private void LoadTab(UserControl tab)
        {
            pnlContent.Controls.Clear();

            _currentTab?.Dispose();
            _currentTab = tab;

            tab.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(tab);
        }

        // ==================== TAB HANDLERS ====================

        private void btnTabPromotions_Click(object sender, EventArgs e) => ShowPromotions();
        private void btnTabFeedback_Click(object sender, EventArgs e) => ShowFeedback();
        private void btnTabReviews_Click(object sender, EventArgs e) => ShowReviews();
        private void btnTabLoyalty_Click(object sender, EventArgs e) => ShowLoyalty();
        private void btnTabInquiries_Click(object sender, EventArgs e) => ShowInquiries();

        // ==================== TAB CONTENT ====================

        private void ShowPromotions()
        {
            SetActiveTab(btnTabPromotions);
            var tab = new PromotionsTab(_auth, _api);
            LoadTab(tab);
        }

        private void ShowFeedback()
        {
            SetActiveTab(btnTabFeedback);
            var tab = new FeedbackTab(_auth, _api);
            LoadTab(tab);
        }

        private void ShowReviews()
        {
            SetActiveTab(btnTabReviews);
            var tab = new ReviewsTab(_auth, _api);
            LoadTab(tab);
        }

        private void ShowLoyalty()
        {
            SetActiveTab(btnTabLoyalty);
            var tab = new MembershipPlansTab(_auth, _api);
            LoadTab(tab);
        }

        private void ShowInquiries()
        {
            SetActiveTab(btnTabInquiries);
            var tab = new InquiriesTab(_auth, _api);
            LoadTab(tab);
        }

        // ==================== PLACEHOLDER BUILDER ====================

        private UserControl BuildPlaceholderTab(string title, string description, string note)
        {
            var uc = new UserControl
            {
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill
            };

            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(40)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = false,
                Size = new Size(800, 40),
                Location = new Point(40, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = false,
                Size = new Size(800, 24),
                Location = new Point(40, 90),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblIcon = new Label
            {
                Text = "🚧",
                Font = new Font("Segoe UI", 48F),
                ForeColor = Color.FromArgb(200, 200, 200),
                AutoSize = false,
                Size = new Size(800, 100),
                Location = new Point(40, 180),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblNote = new Label
            {
                Text = note,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.FromArgb(139, 92, 246),
                AutoSize = false,
                Size = new Size(800, 26),
                Location = new Point(40, 300),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblIcon);
            card.Controls.Add(lblNote);

            uc.Controls.Add(card);
            return uc;
        }
    }
}