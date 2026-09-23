using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CRM.winforms.Helpers
{
    /// <summary>
    /// Helper for applying rounded corners to WinForms controls.
    /// Uses a GraphicsPath region on the control.
    /// </summary>
    public static class RoundedCorners
    {
        /// <summary>
        /// Applies rounded corners to a control with the given radius.
        /// Call this after the control's size is finalized.
        /// </summary>
        public static void Apply(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            radius = Math.Min(radius, Math.Min(control.Width, control.Height) / 2);

            using var path = CreateRoundedPath(
                new Rectangle(0, 0, control.Width, control.Height),
                radius);

            control.Region?.Dispose();
            control.Region = new Region(path);
        }

        /// <summary>
        /// Applies rounded corners on the top edge only (for tabs, headers).
        /// </summary>
        public static void ApplyTopRounded(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            radius = Math.Min(radius, Math.Min(control.Width, control.Height) / 2);

            using var path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(control.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height);
            path.AddLine(control.Width, control.Height, 0, control.Height);
            path.CloseFigure();

            control.Region?.Dispose();
            control.Region = new Region(path);
        }

        /// <summary>
        /// Creates a rounded rectangle GraphicsPath.
        /// Useful when custom painting controls.
        /// </summary>
        public static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Draws a rounded rectangle border with optional fill.
        /// </summary>
        public static void DrawRoundedRect(
            Graphics g,
            Rectangle bounds,
            int radius,
            Color fillColor,
            Color? borderColor = null,
            int borderWidth = 1)
        {
            using var path = CreateRoundedPath(bounds, radius);

            using (var brush = new SolidBrush(fillColor))
            {
                g.FillPath(brush, path);
            }

            if (borderColor.HasValue)
            {
                using var pen = new Pen(borderColor.Value, borderWidth);
                g.DrawPath(pen, path);
            }
        }
    }
}
