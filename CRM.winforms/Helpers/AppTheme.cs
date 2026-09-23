using System.Drawing;

namespace CRM.winforms.Helpers
{
    /// <summary>
    /// Centralized design system values: colors, fonts, sizes used across the WinForms app.
    /// </summary>
    public static class AppTheme
    {
        // Colors
        public static readonly Color Primary = ColorTranslator.FromHtml("#8B5CF6");
        public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#7C3AED");
        public static readonly Color PrimaryDark = ColorTranslator.FromHtml("#2D1B4E");
        public static readonly Color PrimaryDarker = ColorTranslator.FromHtml("#1a0f2e");
        public static readonly Color Success = ColorTranslator.FromHtml("#10B981");
        public static readonly Color Warning = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color Danger = ColorTranslator.FromHtml("#EF4444");
        public static readonly Color Info = ColorTranslator.FromHtml("#3B82F6");
        public static readonly Color Background = ColorTranslator.FromHtml("#F9FAFB");
        public static readonly Color CardBackground = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color Border = ColorTranslator.FromHtml("#E5E7EB");
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#1F2937");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#6B7280");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#9CA3AF");

        // Typography (Segoe UI)
        public const string FontFamilyName = "Segoe UI";
        public static readonly Font PageTitle = new Font(FontFamilyName, 24f, FontStyle.Bold);
        public static readonly Font SectionTitle = new Font(FontFamilyName, 16f, FontStyle.Bold);
        public static readonly Font CardLabel = new Font(FontFamilyName, 11f, FontStyle.Regular);
        public static readonly Font CardValue = new Font(FontFamilyName, 22f, FontStyle.Bold);
        public static readonly Font Body = new Font(FontFamilyName, 11f, FontStyle.Regular);
        public static readonly Font Small = new Font(FontFamilyName, 9f, FontStyle.Regular);
        public static readonly Font Button = new Font(FontFamilyName, 11f, FontStyle.Bold);

        // Layout
        public const int CardBorderRadius = 8;
        public const int ButtonBorderRadius = 6;
        public const int CardPadding = 20;
        public const int SidebarWidth = 220;
        public const int SidebarButtonHeight = 44;

        // Misc
        public static readonly Size StatCardSize = new Size(240, 96);

        /// <summary>
        /// Returns a status badge color for common statuses.
        /// </summary>
        public static Color GetStatusColor(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return TextMuted;

            return status.ToLowerInvariant() switch
            {
                "confirmed" => Success,
                "pending" => Warning,
                "cancelled" => Danger,
                "checked in" => Info,
                "checked out" => TextSecondary,
                "active" => Success,
                "inactive" => Danger,
                "available" => Success,
                "booked" => Danger,
                _ => TextMuted,
            };
        }
    }
}
