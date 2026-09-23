using System.Drawing;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// Describes a single action button rendered inside an ActionButtonsCell.
    /// </summary>
    public class ActionButtonInfo
    {
        public string ActionKey { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public Color BackgroundColor { get; set; } = Color.FromArgb(139, 92, 246);
        public Color TextColor { get; set; } = Color.White;

        /// <summary>Bounds computed by the cell painter. Used for hit-testing.</summary>
        public Rectangle Bounds { get; set; }

        /// <summary>True if the mouse is currently over this button.</summary>
        public bool IsHovered { get; set; }

        public string FullText => string.IsNullOrEmpty(Icon) ? Label : $"{Icon} {Label}";
    }
}