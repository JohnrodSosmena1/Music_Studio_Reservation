using System;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM.winforms.Pages;

namespace CRM.winforms.Forms
{
    public class MainForm : Form
    {
        private Panel _sidebar;
        private Panel _topbar;
        private Panel _content;
        private Label _titleLabel;

        public MainForm()
        {
            InitializeComponents();
            ApplyRoleBasedNavigation();
            // Default landing page
            LoadPage(new DashboardPage());
        }

        private void InitializeComponents()
        {
            Text = "Music Studio - Dashboard";
            WindowState = FormWindowState.Maximized;
            BackColor = AppTheme.Background;

            _sidebar = new Panel
            {
                BackColor = AppTheme.PrimaryDark,
                Width = AppTheme.SidebarWidth,
                Dock = DockStyle.Left
            };

            var logo = new Label
            {
                Text = "Music Studio",
                ForeColor = Color.White,
                Font = AppTheme.SectionTitle,
                Location = new Point(16, 12),
                AutoSize = true
            };

            _sidebar.Controls.Add(logo);

            _topbar = new Panel
            {
                BackColor = Color.White,
                Height = 64,
                Dock = DockStyle.Top
            };

            _titleLabel = new Label
            {
                Text = "Dashboard",
                Font = AppTheme.PageTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(AppTheme.SidebarWidth + 24, 12),
                AutoSize = true
            };

            var logout = new LinkLabel
            {
                Text = "Logout",
                Location = new Point(ClientSize.Width - 100, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            logout.Click += (s, e) => DoLogout();

            _topbar.Controls.Add(_titleLabel);
            _topbar.Controls.Add(logout);

            _content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.Background,
                AutoScroll = true
            };

            Controls.Add(_content);
            Controls.Add(_topbar);
            Controls.Add(_sidebar);
        }

        private void ApplyRoleBasedNavigation()
        {
            _sidebar.Controls.Clear();

            var logo = new Label
            {
                Text = "Music Studio",
                ForeColor = Color.White,
                Font = AppTheme.SectionTitle,
                Location = new Point(16, 12),
                AutoSize = true
            };
            _sidebar.Controls.Add(logo);

            var role = ServiceLocator.Session?.Role ?? "Client";

            // Always available
            AddNavButton("Dashboard", () => LoadPage(new DashboardPage()));

            // Booking access for all authenticated users
            AddNavButton("Bookings", () => LoadPage(new BookingsPage()));

            // Studio management for Admin+ roles
            if (role == "Admin" || role == "SuperAdmin")
            {
                AddNavButton("Studios", () => LoadPage(new StudiosPage()));
            }

            // Admin panel only for SuperAdmin
            if (role == "SuperAdmin")
            {
                AddNavButton("Administration", () => LoadPage(new AdminPage()));
            }

            // Show current user / role at bottom
            var roleLabel = new Label
            {
                Text = $"{ServiceLocator.Session?.Email ?? "Guest"}\n{role}",
                ForeColor = Color.WhiteSmoke,
                Font = AppTheme.Body,
                AutoSize = true,
                Location = new Point(12, Height - 80),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            _sidebar.Controls.Add(roleLabel);
        }

        private void AddNavButton(string text, Action onClick)
        {
            var btn = new RoundedButton
            {
                Text = text,
                Width = _sidebar.Width - 32,
                Height = 40,
                Location = new Point(16, 60 + _sidebar.Controls.Count * 48),
                BackColor = AppTheme.PrimaryDark,
                BorderRadius = 8,
                ForeColor = Color.White
            };
            btn.Click += (s, e) =>
            {
                _titleLabel.Text = text;
                onClick();
            };
            _sidebar.Controls.Add(btn);
        }

        private void LoadPage(UserControl page)
        {
            _content.Controls.Clear();
            page.Dock = DockStyle.Fill;
            _content.Controls.Add(page);
        }

        private void DoLogout()
        {
            ServiceLocator.Session?.Clear();
            // Return to login
            Hide();
            using var login = new LoginForm();
            login.ShowDialog();
            Close();
        }
    }
}
