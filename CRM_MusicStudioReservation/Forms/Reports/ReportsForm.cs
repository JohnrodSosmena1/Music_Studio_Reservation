using CRM.winforms.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Forms.Reports
{
    public partial class ReportsForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;

        private UserControl? _currentTab;
        private Button? _activeTabButton;

        private static readonly Color ActiveTabColor = Color.FromArgb(139, 92, 246);
        private static readonly Color InactiveTabColor = Color.FromArgb(107, 114, 128);

        public ReportsForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            ShowBookingReport();
        }

        // ==================== TAB SWITCHING ====================

        private void SetActiveTab(Button tab)
        {
            foreach (Control ctrl in pnlTabs.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.ForeColor = InactiveTabColor;
                    btn.BackColor = Color.White;
                }
            }

            tab.ForeColor = ActiveTabColor;
            tab.BackColor = Color.White;

            tab.Paint -= TabButton_Paint;
            tab.Paint += TabButton_Paint;

            _activeTabButton = tab;
        }

        private void TabButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn != _activeTabButton) return;

            using var pen = new Pen(ActiveTabColor, 3);
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

        private void btnTabBooking_Click(object sender, EventArgs e) => ShowBookingReport();
        private void btnTabRevenue_Click(object sender, EventArgs e) => ShowRevenueReport();

        private void ShowBookingReport()
        {
            SetActiveTab(btnTabBooking);
            var tab = new BookingReportTab(_auth, _api);
            LoadTab(tab);
        }

        private void ShowRevenueReport()
        {
            SetActiveTab(btnTabRevenue);
            var tab = new RevenueReportTab(_auth, _api);
            LoadTab(tab);
        }
    }
}