using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// Small dashboard statistic card with icon, label, value and optional percent change.
    /// </summary>
    public class StatCard : UserControl
    {
        private PictureBox _iconBox;
        private Label _label;
        private Label _valueLabel;
        private Label _changeLabel;
        private RoundedPanel _panel;

        public StatCard()
        {
            Width = 240;
            Height = 96;
            _panel = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.CardBackground,
                BorderRadius = AppTheme.CardBorderRadius,
                BorderSize = 1,
                BorderColor = AppTheme.Border
            };

            _iconBox = new PictureBox
            {
                Size = new Size(36, 36),
                Location = new Point(16, 20),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            };

            _label = new Label
            {
                AutoSize = false,
                Location = new Point(64, 16),
                Size = new Size(150, 20),
                Font = AppTheme.CardLabel,
                ForeColor = AppTheme.TextSecondary
            };

            _valueLabel = new Label
            {
                AutoSize = false,
                Location = new Point(64, 36),
                Size = new Size(150, 36),
                Font = AppTheme.CardValue,
                ForeColor = AppTheme.TextPrimary
            };

            _changeLabel = new Label
            {
                AutoSize = false,
                Location = new Point(64, 70),
                Size = new Size(150, 16),
                Font = AppTheme.Small,
                ForeColor = AppTheme.TextSecondary
            };

            _panel.Controls.Add(_iconBox);
            _panel.Controls.Add(_label);
            _panel.Controls.Add(_valueLabel);
            _panel.Controls.Add(_changeLabel);

            Controls.Add(_panel);
        }

        [Category("Appearance")]
        public Image Icon
        {
            get => _iconBox.Image;
            set => _iconBox.Image = value;
        }

        [Category("Appearance")]
        public string LabelText
        {
            get => _label.Text;
            set => _label.Text = value;
        }

        [Category("Appearance")]
        public string ValueText
        {
            get => _valueLabel.Text;
            set => _valueLabel.Text = value;
        }

        [Category("Appearance")]
        public string ChangeText
        {
            get => _changeLabel.Text;
            set => _changeLabel.Text = value;
        }

        [Category("Appearance")]
        public Color CardBackground
        {
            get => _panel.BackColor;
            set => _panel.BackColor = value;
        }
    }
}
