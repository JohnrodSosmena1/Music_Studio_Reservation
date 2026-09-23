using System;
using System.ComponentModel;
using System.Drawing;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    public partial class StatCard : UserControl
    {
        private decimal _trendPercent;
        private string _trendCaption = string.Empty;

        public StatCard()
        {
            InitializeComponent();
            RoundedCorners.Apply(this, AppTheme.CardRadius);
            ApplyCardStyle();
            EnsureTrendLabel();
        }

        private void ApplyCardStyle()
        {
            this.BackColor = AppTheme.CardBackground;
            this.Paint += (s, e) =>
            {
                using var pen = new Pen(AppTheme.Border, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            };
        }

        /// <summary>Creates lblTrend lazily and places it under lblSubtext.</summary>
        private void EnsureTrendLabel()
        {
            if (lblTrend != null) return;

            lblTrend = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Bottom,
                Height = 20,
                Font = AppTheme.Small,
                ForeColor = AppTheme.TextMuted,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                Name = "lblTrend",
                Padding = new Padding(20, 0, 20, 8),
                Visible = false
            };

            this.Controls.Add(lblTrend);
            lblTrend.BringToFront();
        }

        // ==================== EXISTING PROPERTIES ====================

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Icon
        {
            get => lblIcon.Text;
            set => lblIcon.Text = value;
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color IconColor
        {
            get => lblIcon.ForeColor;
            set => lblIcon.ForeColor = value;
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Value
        {
            get => lblValue.Text;
            set => lblValue.Text = value;
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Subtext
        {
            get => lblSubtext.Text;
            set => lblSubtext.Text = value;
        }

        // ==================== NEW TREND PROPERTIES ====================

        /// <summary>
        /// Trend percentage. Positive = up arrow (green), negative = down arrow (red), 0 = neutral.
        /// Set this OR leave 0 and set TrendCaption empty to hide the trend row.
        /// </summary>
        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0)]
        public decimal TrendPercent
        {
            get => _trendPercent;
            set
            {
                _trendPercent = value;
                RenderTrend();
            }
        }

        /// <summary>
        /// Caption shown after the trend arrow. Example: "vs. last week".
        /// Leave empty to hide the trend row entirely.
        /// </summary>
        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public string TrendCaption
        {
            get => _trendCaption;
            set
            {
                _trendCaption = value ?? string.Empty;
                RenderTrend();
            }
        }

        private void RenderTrend()
        {
            if (lblTrend == null) EnsureTrendLabel();

            if (string.IsNullOrWhiteSpace(_trendCaption))
            {
                lblTrend.Visible = false;
                return;
            }

            string arrow;
            Color color;

            if (_trendPercent > 0)
            {
                arrow = "▲";
                color = AppTheme.Success;
            }
            else if (_trendPercent < 0)
            {
                arrow = "▼";
                color = AppTheme.Danger;
            }
            else
            {
                arrow = "—";
                color = AppTheme.TextMuted;
            }

            lblTrend.Text = $"{arrow} {Math.Abs(_trendPercent):0.#}%   {_trendCaption}";
            lblTrend.ForeColor = color;
            lblTrend.Visible = true;
            lblTrend.BringToFront();
        }
    }
}