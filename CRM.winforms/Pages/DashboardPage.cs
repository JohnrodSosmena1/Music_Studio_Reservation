using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM.winforms.Pages
{
    public class DashboardPage : UserControl
    {
        private StatCard _activeBookingsCard;
        private StatCard _studiosCard;
        private StatCard _totalRevenueCard;
        private Label _welcomeLabel;
        private Button _refreshBtn;

        public DashboardPage()
        {
            Initialize();
            _ = LoadDashboardAsync();
        }

        private void Initialize()
        {
            BackColor = AppTheme.Background;

            _welcomeLabel = new Label
            {
                Text = $"Welcome, {ServiceLocator.Session?.Email ?? "User"}",
                Font = AppTheme.SectionTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new System.Drawing.Point(24, 24),
                AutoSize = true
            };
            Controls.Add(_welcomeLabel);

            _refreshBtn = new Button { Text = "Refresh", Location = new System.Drawing.Point(24, 60) };
            _refreshBtn.Click += (s, e) => _ = LoadDashboardAsync();
            Controls.Add(_refreshBtn);

            _activeBookingsCard = new StatCard
            {
                LabelText = "Active Bookings",
                ValueText = "—",
                Location = new System.Drawing.Point(24, 110)
            };
            Controls.Add(_activeBookingsCard);

            _studiosCard = new StatCard
            {
                LabelText = "Studios",
                ValueText = "—",
                Location = new System.Drawing.Point(280, 110)
            };
            Controls.Add(_studiosCard);

            _totalRevenueCard = new StatCard
            {
                LabelText = "Total Revenue",
                ValueText = "$0",
                Location = new System.Drawing.Point(536, 110)
            };
            Controls.Add(_totalRevenueCard);
        }

        private async Task LoadDashboardAsync()
        {
            try
            {
                _refreshBtn.Enabled = false;

                var studiosResp = await ServiceLocator.ApiClient!.GetStudiosAsync(1, 1, 1000);
                var bookingsResp = await ServiceLocator.ApiClient!.GetBookingsAsync(1, 1, 1000);

                var studioCount = studiosResp.TotalItems;
                var bookingCount = bookingsResp.TotalItems;
                var totalRevenue = 0.0;

                foreach (var b in bookingsResp.Items)
                {
                    totalRevenue += b.TotalAmount;
                }

                _studiosCard.ValueText = studioCount.ToString();
                _activeBookingsCard.ValueText = bookingCount.ToString();
                _totalRevenueCard.ValueText = totalRevenue.ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _refreshBtn.Enabled = true;
            }
        }
    }
}
