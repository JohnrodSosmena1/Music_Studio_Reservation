using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.winforms.Helpers;

namespace CRM.winforms.Controls
{
    public partial class StatusBadge : UserControl
    {
        public StatusBadge()
        {
            InitializeComponent();
            RoundedCorners.Apply(this, 12);    // Pill shape
        }

        // ==================== PROPERTIES ====================

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get => lblText.Text;
            set
            {
                lblText.Text = value;
                // Auto-color based on status text
                this.BackColor = AppTheme.GetStatusColor(value);
            }
        }

        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BadgeColor
        {
            get => this.BackColor;
            set => this.BackColor = value;
        }
    }
}