using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CRM.winforms.Helpers
{
    /// <summary>
    /// Helper to apply rounded corners to Windows Forms controls by setting a region.
    /// </summary>
    public static class RoundedCorners
    {
        /// <summary>
        /// Applies rounded corners to the given control with the specified radius.
        /// </summary>
        /// <param name="control">Control to style.</param>
        /// <param name="radius">Corner radius in pixels.</param>
        public static void Apply(Control control, int radius)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (radius <= 0)
            {
                control.Region = null!;
                return;
            }

            var rect = new Rectangle(0, 0, control.Width, control.Height);
            using var path = GetRoundedRectPath(rect, radius);
            control.Region = new Region(path);
            control.Resize += (s, e) =>
            {
                var c = (Control)s!;
                var r = new Rectangle(0, 0, c.Width, c.Height);
                using var p = GetRoundedRectPath(r, radius);
                c.Region = new Region(p);
            };
        }

        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            var diameter = radius * 2;

            // Top-left arc
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            // Top edge
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            // Top-right arc
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            // Right edge
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            // Bottom-right arc
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            // Bottom edge
            path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            // Bottom-left arc
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            // Left edge
            path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);

            path.CloseFigure();
            return path;
        }
    }
}
