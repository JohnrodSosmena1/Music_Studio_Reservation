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
            ApplyCardStyle();
            EnsureTrendLabel();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Width > 0 && this.Height > 0)
            {
                RoundedCorners.Apply(this, AppTheme.CardRadius);
                if (lblTitle != null) lblTitle.Width = Math.Max(50, this.Width - 75);
                if (lblValue != null) lblValue.Width = Math.Max(50, this.Width - 32);
                if (lblSubtext != null) lblSubtext.Width = Math.Max(50, this.Width - 32);
            }
            this.Invalidate();
        }

        private void ApplyCardStyle()
        {
            this.BackColor = AppTheme.CardBackground;
            this.Paint += (s, e) =>
            {
                if (this.Width > 2 && this.Height > 2)
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using var path = RoundedCorners.CreateRoundedPath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), AppTheme.CardRadius);
                    using var pen = new Pen(AppTheme.Border, 1);
                    e.Graphics.DrawPath(pen, path);
                }
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