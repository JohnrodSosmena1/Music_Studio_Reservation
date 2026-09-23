using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CRM.winforms.Helpers
{
    /// <summary>
    /// Central theme for the entire application.
    /// Colors, fonts, sizes, and spacing defined in one place.
    /// Matches the mockup design system.
    /// </summary>
    public static class AppTheme
    {
        // ==================== COLORS ====================
        // Primary purple palette
        public static readonly Color Primary = Color.FromArgb(139, 92, 246);       // #8B5CF6
        public static readonly Color PrimaryHover = Color.FromArgb(124, 58, 237);  // #7C3AED
        public static readonly Color PrimaryDark = Color.FromArgb(45, 27, 78);     // #2D1B4E
        public static readonly Color PrimaryDarker = Color.FromArgb(26, 15, 46);   // #1a0f2e
        public static readonly Color PrimaryLight = Color.FromArgb(167, 139, 250); // #A78BFA

        // Status colors
        public static readonly Color Success = Color.FromArgb(16, 185, 129);       // #10B981
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);       // #F59E0B
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);         // #EF4444
        public static readonly Color Info = Color.FromArgb(59, 130, 246);          // #3B82F6

        // Neutrals
        public static readonly Color Background = Color.FromArgb(249, 250, 251);   // #F9FAFB
        public static readonly Color CardBackground = Color.White;                  // #FFFFFF
        public static readonly Color Border = Color.FromArgb(229, 231, 235);       // #E5E7EB
        public static readonly Color TextPrimary = Color.FromArgb(31, 41, 55);     // #1F2937
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128);// #6B7280
        public static readonly Color TextMuted = Color.FromArgb(156, 163, 175);    // #9CA3AF
        public static readonly Color TextOnDark = Color.White;

        // ==================== FONTS ====================
        public const string FontFamily = "Segoe UI";

        public static readonly Font PageTitle = new(FontFamily, 24, FontStyle.Bold);
        public static readonly Font SectionTitle = new(FontFamily, 16, FontStyle.Bold);
        public static readonly Font CardValue = new(FontFamily, 22, FontStyle.Bold);
        public static readonly Font CardLabel = new(FontFamily, 11, FontStyle.Regular);
        public static readonly Font Body = new(FontFamily, 11, FontStyle.Regular);
        public static readonly Font BodyBold = new(FontFamily, 11, FontStyle.Bold);
        public static readonly Font Button = new(FontFamily, 11, FontStyle.Bold);
        public static readonly Font Small = new(FontFamily, 9, FontStyle.Regular);
        public static readonly Font SidebarButton = new(FontFamily, 11, FontStyle.Bold);

        // ==================== SIZES ====================
        public const int CardRadius = 8;
        public const int ButtonRadius = 6;
        public const int CardPadding = 20;
        public const int CardShadowOffset = 2;

        public const int SidebarWidth = 220;
        public const int SidebarButtonHeight = 44;
        public const int TopBarHeight = 60;
        public const int FormPadding = 20;

        public static readonly Size PrimaryButtonSize = new(160, 42);
        public static readonly Size SmallButtonSize = new(100, 34);
        public static readonly Size IconButtonSize = new(36, 36);

        // ==================== SPACING ====================
        public const int SpacingXS = 4;
        public const int SpacingS = 8;
        public const int SpacingM = 16;
        public const int SpacingL = 24;
        public const int SpacingXL = 32;

        // ==================== HELPER METHODS ====================

        /// <summary>Returns the color for a booking/status badge.</summary>
        public static Color GetStatusColor(string status) => status?.ToLowerInvariant() switch
        {
            "confirmed" => Success,
            "pending" => Warning,
            "cancelled" => Danger,
            "checkedin" or "checked-in" => Info,
            "checkedout" or "checked-out" => TextSecondary,
            "active" => Success,
            "inactive" => Danger,
            "available" => Success,
            "booked" => Danger,
            _ => TextSecondary
        };

        /// <summary>Lightens a color by blending with white.</summary>
        public static Color Lighten(Color color, double amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            int r = (int)(color.R + (255 - color.R) * amount);
            int g = (int)(color.G + (255 - color.G) * amount);
            int b = (int)(color.B + (255 - color.B) * amount);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>Darkens a color by blending with black.</summary>
        public static Color Darken(Color color, double amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            int r = (int)(color.R * (1 - amount));
            int g = (int)(color.G * (1 - amount));
            int b = (int)(color.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }
    }
}
