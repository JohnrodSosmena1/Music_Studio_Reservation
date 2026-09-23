using System;
using System.Windows.Forms;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM.winforms.Pages
{
    public class AdminPage : UserControl
    {
        public AdminPage()
        {
            Initialize();
        }

        private void Initialize()
        {
            BackColor = AppTheme.Background;

            var title = new Label
            {
                Text = "Administration",
                Font = AppTheme.SectionTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new System.Drawing.Point(24, 24),
                AutoSize = true
            };
            Controls.Add(title);

            var sessionInfo = new GroupBox
            {
                Text = "Session Information",
                Location = new System.Drawing.Point(24, 80),
                Width = 400,
                Height = 180
            };

            var emailLabel = new Label
            {
                Text = $"Email: {ServiceLocator.Session?.Email ?? "N/A"}",
                Location = new System.Drawing.Point(12, 32),
                Width = 360,
                AutoSize = true
            };
            sessionInfo.Controls.Add(emailLabel);

            var roleLabel = new Label
            {
                Text = $"Role: {ServiceLocator.Session?.Role ?? "N/A"}",
                Location = new System.Drawing.Point(12, 62),
                Width = 360,
                AutoSize = true
            };
            sessionInfo.Controls.Add(roleLabel);

            var tenantLabel = new Label
            {
                Text = "Tenant ID: 1 (Default)",
                Location = new System.Drawing.Point(12, 92),
                Width = 360,
                AutoSize = true
            };
            sessionInfo.Controls.Add(tenantLabel);

            var apiLabel = new Label
            {
                Text = "API: http://localhost:5000/",
                Location = new System.Drawing.Point(12, 122),
                Width = 360,
                AutoSize = true
            };
            sessionInfo.Controls.Add(apiLabel);

            Controls.Add(sessionInfo);

            var systemInfo = new GroupBox
            {
                Text = "System Information",
                Location = new System.Drawing.Point(24, 280),
                Width = 400,
                Height = 140
            };

            var versionLabel = new Label
            {
                Text = "Version: 1.0.0",
                Location = new System.Drawing.Point(12, 32),
                Width = 360,
                AutoSize = true
            };
            systemInfo.Controls.Add(versionLabel);

            var frameworkLabel = new Label
            {
                Text = "Framework: .NET 10 (WinForms)",
                Location = new System.Drawing.Point(12, 62),
                Width = 360,
                AutoSize = true
            };
            systemInfo.Controls.Add(frameworkLabel);

            Controls.Add(systemInfo);

            var notesLabel = new Label
            {
                Text = "Note: This is the Administration panel. Manage system settings and view diagnostics here.",
                Font = AppTheme.Body,
                ForeColor = Color.Gray,
                Location = new System.Drawing.Point(24, 440),
                Width = 750,
                AutoSize = true
            };
            Controls.Add(notesLabel);
        }
    }
}
