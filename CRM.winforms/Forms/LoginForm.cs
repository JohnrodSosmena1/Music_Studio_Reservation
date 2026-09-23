using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM.winforms.Forms
{
    public class LoginForm : Form
    {
        private RoundedPanel _leftPanel;
        private RoundedPanel _rightPanel;
        private RoundedTextBox _emailBox;
        private RoundedTextBox _passwordBox;
        private RoundedButton _signInButton;
        private CheckBox _rememberBox;
        private LinkLabel _forgotLink;

        public LoginForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Music Studio - Sign In";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(900, 520);
            BackColor = AppTheme.Background;

            _leftPanel = new RoundedPanel
            {
                BackColor = AppTheme.PrimaryDark,
                BorderRadius = 0,
                Width = 380,
                Dock = DockStyle.Left
            };

            var logoLabel = new Label
            {
                Text = "Music Studio",
                ForeColor = Color.White,
                Font = AppTheme.PageTitle,
                Location = new Point(24, 40),
                AutoSize = true
            };

            var tagline = new Label
            {
                Text = "Practice · Record · Create",
                ForeColor = Color.White,
                Font = AppTheme.Body,
                Location = new Point(24, 90),
                AutoSize = true
            };

            _leftPanel.Controls.Add(logoLabel);
            _leftPanel.Controls.Add(tagline);

            _rightPanel = new RoundedPanel
            {
                BackColor = Color.White,
                BorderRadius = 0,
                Dock = DockStyle.Fill
            };

            var header = new Label
            {
                Text = "Sign in to your account",
                Font = AppTheme.SectionTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(60, 40),
                AutoSize = true
            };

            _emailBox = new RoundedTextBox { Location = new Point(60, 90), Width = 420, Height = 40 };
            _emailBox.InnerTextBox.PlaceholderText = "Email";

            _passwordBox = new RoundedTextBox { Location = new Point(60, 150), Width = 420, Height = 40 };
            _passwordBox.InnerTextBox.PlaceholderText = "Password";
            _passwordBox.InnerTextBox.UseSystemPasswordChar = true;

            _rememberBox = new CheckBox { Text = "Remember me", Location = new Point(60, 205), ForeColor = AppTheme.TextSecondary };
            _forgotLink = new LinkLabel { Text = "Forgot password?", Location = new Point(200, 205), AutoSize = true };

            _signInButton = new RoundedButton
            {
                Text = "Sign In",
                Location = new Point(60, 250),
                Width = 420,
                Height = 44,
                BackColor = AppTheme.Primary,
                HoverColor = AppTheme.PrimaryHover
            };

            _signInButton.Click += async (s, e) => await SignInAsync();

            _rightPanel.Controls.Add(header);
            _rightPanel.Controls.Add(_emailBox);
            _rightPanel.Controls.Add(_passwordBox);
            _rightPanel.Controls.Add(_rememberBox);
            _rightPanel.Controls.Add(_forgotLink);
            _rightPanel.Controls.Add(_signInButton);

            Controls.Add(_rightPanel);
            Controls.Add(_leftPanel);
        }

        private async Task SignInAsync()
        {
            _signInButton.Enabled = false;
            try
            {
                var email = _emailBox.Text.Trim();
                var password = _passwordBox.Text;
                var remember = _rememberBox.Checked;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter email and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var token = await ServiceLocator.AuthService!.LoginAsync(email, password, remember);
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Invalid credentials.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ServiceLocator.Session!.SetSession(token);

                // Open main form
                Hide();
                using var main = new MainForm();
                main.FormClosed += (s, e) => Close();
                main.ShowDialog();
            }
            finally
            {
                _signInButton.Enabled = true;
            }
        }
    }
}
