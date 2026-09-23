using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// A Button with rounded corners and theming support.
    /// </summary>
    public class RoundedButton : Button
    {
        private Color _backgroundColor = AppTheme.Primary;
        private Color _hoverColor = AppTheme.PrimaryHover;
        private Color _borderColor = AppTheme.Border;
        private int _borderSize = 0;
        private int _borderRadius = AppTheme.ButtonBorderRadius;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = _backgroundColor;
            ForeColor = Color.White;
            Font = AppTheme.Button;
            Cursor = Cursors.Hand;

            MouseEnter += (s, e) => OnHoverEnter();
            MouseLeave += (s, e) => OnHoverLeave();
        }

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, value);
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
                FlatAppearance.BorderSize = _borderSize;
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

        [Category("Appearance")]
        public Color HoverColor
        {
            get => _hoverColor;
            set => _hoverColor = value;
        }

        [Category("Appearance")]
        public new Color BackColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                base.BackColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Ensure rounded region
            RoundedCorners.Apply(this, _borderRadius);

            // Draw border if needed
            if (_borderSize > 0)
            {
                using var pen = new Pen(_borderColor, _borderSize);
                pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = ClientRectangle;
                rect.Inflate(-_borderSize / 2, -_borderSize / 2);
                pevent.Graphics.DrawPath(pen, RoundedRectPath(rect, _borderRadius));
            }
        }

        private void OnHoverEnter()
        {
            base.BackColor = _hoverColor;
            Invalidate();
        }

        private void OnHoverLeave()
        {
            base.BackColor = _backgroundColor;
            Invalidate();
        }

        private System.Drawing.Drawing2D.GraphicsPath RoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            var diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
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
