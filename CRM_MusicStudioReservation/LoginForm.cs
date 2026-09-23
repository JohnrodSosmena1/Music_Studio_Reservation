using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM_MusicStudioReservation.Forms;

namespace CRM_MusicStudioReservation
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _api;
        private readonly AuthService _auth;

        public LoginForm()
        {
            InitializeComponent();

            _api = new ApiClient("https://localhost:7297");
            _auth = new AuthService(_api);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            pnlLeft.Paint += (s, ev) =>
            {
                using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    pnlLeft.ClientRectangle,
                    AppTheme.PrimaryDark,
                    AppTheme.PrimaryDarker,
                    90f);
                ev.Graphics.FillRectangle(brush, pnlLeft.ClientRectangle);
            };

            lblLogo.Paint += (s, ev) =>
            {
                ev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using var brush = new SolidBrush(AppTheme.Primary);
                ev.Graphics.FillEllipse(brush, 0, 0, lblLogo.Width - 1, lblLogo.Height - 1);

                using var textBrush = new SolidBrush(Color.White);
                using var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                ev.Graphics.DrawString("MS", lblLogo.Font, textBrush,
                    new RectangleF(0, 0, lblLogo.Width, lblLogo.Height), format);
            };

            lblBrandName.BackColor = Color.Transparent;
            lblTagline.BackColor = Color.Transparent;
            lblLogo.BackColor = Color.Transparent;

            btnSignIn.BackColor = AppTheme.Primary;
            pnlRight.BackColor = Color.White;

            txtEmail.Focus();

            txtEmail.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) btnSignIn.PerformClick(); };
            txtPassword.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) btnSignIn.PerformClick(); };
        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblError.Text = "";

            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowError("Please enter your email.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            btnSignIn.Enabled = false;
            btnSignIn.Text = "Signing in...";

            try
            {
                var success = await _auth.LoginAsync(email, password);

                if (success && _auth.CurrentUser is not null)
                {
                    // ✅ FIXED — now passes both _auth AND _api
                    var mainForm = new MainForm(_auth, _api);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    ShowError("Invalid email or password.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Login failed: {ex.Message}");
            }
            finally
            {
                btnSignIn.Enabled = true;
                btnSignIn.Text = "Sign In";
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
    }
}