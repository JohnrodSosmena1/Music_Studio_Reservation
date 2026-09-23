namespace CRM_MusicStudioReservation
{
    partial class LoginForm
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblBrandName = new System.Windows.Forms.Label();
            this.lblTagline = new System.Windows.Forms.Label();

            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lnkForgot = new System.Windows.Forms.LinkLabel();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblNoAccount = new System.Windows.Forms.Label();
            this.lnkContactAdmin = new System.Windows.Forms.LinkLabel();

            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();

            // ==== Form ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "LoginForm";
            this.Text = "Music Studio — Sign In";
            this.Load += new System.EventHandler(this.LoginForm_Load);

            // ==== Left Panel (Purple) ====
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Width = 500;
            this.pnlLeft.Name = "pnlLeft";

            // Logo circle (styled as a label with big MS text)
            this.lblLogo.AutoSize = false;
            this.lblLogo.Size = new System.Drawing.Size(120, 120);
            this.lblLogo.Location = new System.Drawing.Point(190, 130);
            this.lblLogo.Text = "MS";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Name = "lblLogo";

            // Brand name
            this.lblBrandName.AutoSize = false;
            this.lblBrandName.Size = new System.Drawing.Size(400, 50);
            this.lblBrandName.Location = new System.Drawing.Point(50, 280);
            this.lblBrandName.Text = "MUSIC STUDIO";
            this.lblBrandName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBrandName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblBrandName.ForeColor = System.Drawing.Color.White;
            this.lblBrandName.Name = "lblBrandName";

            // Tagline
            this.lblTagline.AutoSize = false;
            this.lblTagline.Size = new System.Drawing.Size(400, 30);
            this.lblTagline.Location = new System.Drawing.Point(50, 335);
            this.lblTagline.Text = "Practice  ·  Record  ·  Create";
            this.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.lblTagline.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.lblTagline.Name = "lblTagline";

            this.pnlLeft.Controls.Add(this.lblLogo);
            this.pnlLeft.Controls.Add(this.lblBrandName);
            this.pnlLeft.Controls.Add(this.lblTagline);

            // ==== Right Panel (White) ====
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(70, 0, 70, 0);

            // Welcome
            this.lblWelcome.AutoSize = false;
            this.lblWelcome.Size = new System.Drawing.Size(360, 50);
            this.lblWelcome.Location = new System.Drawing.Point(70, 100);
            this.lblWelcome.Text = "Welcome Back";
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblWelcome.Name = "lblWelcome";

            // Subtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(360, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(70, 155);
            this.lblSubtitle.Text = "Sign in to your Music Studio account";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // Email textbox
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtEmail.Location = new System.Drawing.Point(70, 215);
            this.txtEmail.Size = new System.Drawing.Size(360, 32);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Email or Username";

            // Password textbox
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.Location = new System.Drawing.Point(70, 265);
            this.txtPassword.Size = new System.Drawing.Size(360, 32);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.UseSystemPasswordChar = true;

            // Remember me
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(70, 315);
            this.chkRemember.Text = "Remember me";
            this.chkRemember.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.chkRemember.Name = "chkRemember";

            // Forgot password link
            this.lnkForgot.AutoSize = true;
            this.lnkForgot.Location = new System.Drawing.Point(355, 317);
            this.lnkForgot.Text = "Forgot password?";
            this.lnkForgot.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkForgot.LinkColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lnkForgot.Name = "lnkForgot";

            // Sign In button
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.FlatAppearance.BorderSize = 0;
            this.btnSignIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSignIn.ForeColor = System.Drawing.Color.White;
            this.btnSignIn.Location = new System.Drawing.Point(70, 355);
            this.btnSignIn.Size = new System.Drawing.Size(360, 48);
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);

            // Error label (hidden by default)
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(360, 25);
            this.lblError.Location = new System.Drawing.Point(70, 415);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // No account text
            this.lblNoAccount.AutoSize = true;
            this.lblNoAccount.Location = new System.Drawing.Point(155, 460);
            this.lblNoAccount.Text = "Don't have an account?";
            this.lblNoAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoAccount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNoAccount.Name = "lblNoAccount";

            // Contact admin link
            this.lnkContactAdmin.AutoSize = true;
            this.lnkContactAdmin.Location = new System.Drawing.Point(305, 460);
            this.lnkContactAdmin.Text = "Contact Admin";
            this.lnkContactAdmin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lnkContactAdmin.LinkColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lnkContactAdmin.Name = "lnkContactAdmin";

            this.pnlRight.Controls.Add(this.lblWelcome);
            this.pnlRight.Controls.Add(this.lblSubtitle);
            this.pnlRight.Controls.Add(this.txtEmail);
            this.pnlRight.Controls.Add(this.txtPassword);
            this.pnlRight.Controls.Add(this.chkRemember);
            this.pnlRight.Controls.Add(this.lnkForgot);
            this.pnlRight.Controls.Add(this.btnSignIn);
            this.pnlRight.Controls.Add(this.lblError);
            this.pnlRight.Controls.Add(this.lblNoAccount);
            this.pnlRight.Controls.Add(this.lnkContactAdmin);

            // ==== Add to Form ====
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);

            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // ==================== CONTROLS ====================
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.LinkLabel lnkForgot;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblNoAccount;
        private System.Windows.Forms.LinkLabel lnkContactAdmin;
    }
}
