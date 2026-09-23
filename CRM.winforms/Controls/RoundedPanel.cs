using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// Panel with rounded corners and optional border.
    /// </summary>
    public class RoundedPanel : Panel
    {
        private int _borderRadius = AppTheme.CardBorderRadius;
        private int _borderSize = 1;
        private Color _borderColor = AppTheme.Border;

        public RoundedPanel()
        {
            DoubleBuffered = true;
            BackColor = AppTheme.CardBackground;
        }

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, value);
                RoundedCorners.Apply(this, _borderRadius);
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = ClientRectangle;
            using var path = GetRoundedPath(rect, _borderRadius);
            using var brush = new SolidBrush(BackColor);
            e.Graphics.FillPath(brush, path);

            if (_borderSize > 0)
            {
                using var pen = new Pen(_borderColor, _borderSize);
                e.Graphics.DrawPath(pen, path);
            }
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
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);
            path.CloseFigure();
            return path;
        }
    }
}
