using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// A DataGridView cell that paints one or more pill-shaped action buttons side-by-side.
    /// </summary>
    public class ActionButtonsCell : DataGridViewCell
    {
        private const int ButtonHeight = 28;
        private const int ButtonSpacing = 6;
        private const int ButtonPadding = 12;
        private const int CellPaddingLeft = 8;
        private const int ButtonRadius = 6;

        public override Type ValueType => typeof(List<ActionButtonInfo>);
        public override Type FormattedValueType => typeof(string);

        public List<ActionButtonInfo> GetButtons()
        {
            return this.Value as List<ActionButtonInfo> ?? new List<ActionButtonInfo>();
        }

        // ==================== PAINT ====================

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Background
            using (var bgBrush = new SolidBrush(Color.White))
            {
                graphics.FillRectangle(bgBrush, cellBounds);
            }

            var buttons = GetButtons();
            if (buttons.Count == 0) return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var font = new Font("Segoe UI", 8.5F, FontStyle.Bold);

            int x = cellBounds.X + CellPaddingLeft;
            int y = cellBounds.Y + (cellBounds.Height - ButtonHeight) / 2;

            foreach (var btn in buttons)
            {
                var text = btn.FullText;
                var textSize = graphics.MeasureString(text, font);
                int btnWidth = (int)textSize.Width + (ButtonPadding * 2);

                btn.Bounds = new Rectangle(x, y, btnWidth, ButtonHeight);

                // Hover lightens the color
                var bgColor = btn.IsHovered
                    ? Lighten(btn.BackgroundColor, 0.15)
                    : btn.BackgroundColor;

                // Rounded rectangle
                using (var path = CreateRoundedPath(btn.Bounds, ButtonRadius))
                using (var brush = new SolidBrush(bgColor))
                {
                    graphics.FillPath(brush, path);
                }

                // Text centered
                using (var textBrush = new SolidBrush(btn.TextColor))
                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    graphics.DrawString(text, font, textBrush, btn.Bounds, sf);
                }

                x += btnWidth + ButtonSpacing;
            }
        }

        // ==================== HIT TESTING ====================

        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);

            var buttons = GetButtons();
            bool changed = false;

            foreach (var btn in buttons)
            {
                bool isHovered = btn.Bounds.Contains(e.X, e.Y);
                if (btn.IsHovered != isHovered)
                {
                    btn.IsHovered = isHovered;
                    changed = true;
                }
            }

            if (changed)
                this.DataGridView?.InvalidateCell(this);
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);

            var buttons = GetButtons();
            bool changed = false;

            foreach (var btn in buttons)
            {
                if (btn.IsHovered)
                {
                    btn.IsHovered = false;
                    changed = true;
                }
            }

            if (changed)
                this.DataGridView?.InvalidateCell(this);
        }

        // ==================== HELPERS ====================

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private static Color Lighten(Color color, double amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            int r = (int)(color.R + (255 - color.R) * amount);
            int g = (int)(color.G + (255 - color.G) * amount);
            int b = (int)(color.B + (255 - color.B) * amount);
            return Color.FromArgb(r, g, b);
        }

        public override object Clone() => new ActionButtonsCell();
    }
}