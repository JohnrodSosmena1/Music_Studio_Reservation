using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// A rounded textbox composed of a container panel and a real TextBox.
    /// Supports BorderRadius, BorderSize, BorderColor, HoverColor and BackColor override.
    /// </summary>
    public class RoundedTextBox : UserControl
    {
        private readonly TextBox _textBox;

        private int _borderRadius = 6;
        private int _borderSize = 1;
        private Color _borderColor = AppTheme.Border;
        private Color _hoverColor = AppTheme.PrimaryHover;
        private Color _backColor = Color.White;

        public RoundedTextBox()
        {
            DoubleBuffered = true;
            MinimumSize = new Size(80, 32);

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = _backColor,
                ForeColor = AppTheme.TextPrimary,
                Font = AppTheme.Body,
                Location = new Point(8, 8),
                Width = Width - 16,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            Controls.Add(_textBox);

            _textBox.TextChanged += (s, e) => OnTextChanged(e);
            _textBox.GotFocus += (s, e) => Invalidate();
            _textBox.LostFocus += (s, e) => Invalidate();

            Resize += (s, e) => LayoutTextBox();
            BackColor = Color.Transparent;
        }

        private void LayoutTextBox()
        {
            _textBox.Location = new Point(_borderSize + 6, (_borderSize) + 6);
            _textBox.Width = Math.Max(10, Width - (_borderSize + 6) * 2);
            _textBox.Height = Math.Max(10, Height - (_borderSize + 6) * 2);
            RoundedCorners.Apply(this, _borderRadius);
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

        [Category("Appearance")]
        public Color HoverColor
        {
            get => _hoverColor;
            set => _hoverColor = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        public override Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                _textBox.BackColor = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        public override string Text
        {
            get => _textBox.Text;
            set => _textBox.Text = value;
        }

        [Category("Behavior")]
        public char PasswordChar
        {
            get => _textBox.UseSystemPasswordChar ? '•' : '\0';
            set => _textBox.UseSystemPasswordChar = value != '\0';
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw background
            using var brush = new SolidBrush(_backColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width, Height);
            using var path = GetRoundedPath(rect, _borderRadius);
            e.Graphics.FillPath(brush, path);

            // Draw border
            using var pen = new Pen(_textBox.Focused ? _hoverColor : _borderColor, _borderSize);
            e.Graphics.DrawPath(pen, path);
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

        // Expose inner TextBox for advanced scenarios
        public TextBox InnerTextBox => _textBox;
    }
}
