using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// Small pill-shaped badge used for status labels.
    /// </summary>
    public class StatusBadge : UserControl
    {
        private Label _label;
        private Color _badgeColor = AppTheme.Success;

        public StatusBadge()
        {
            Height = 24;
            MinimumSize = new Size(24, 24);
            _label = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = AppTheme.Small,
                ForeColor = Color.White
            };

            Controls.Add(_label);
            BackColor = Color.Transparent;
            Padding = new Padding(8, 2, 8, 2);
        }

        [Category("Appearance")]
        public string BadgeText
        {
            get => _label.Text;
            set => _label.Text = value;
        }

        [Category("Appearance")]
        public Color BadgeColor
        {
            get => _badgeColor;
            set
            {
                _badgeColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = ClientRectangle;
            using var brush = new SolidBrush(_badgeColor);
            using var path = GetRoundedPath(rect, rect.Height / 2);
            e.Graphics.FillPath(brush, path);
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            var diameter = radius * 2;
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
