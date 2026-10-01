namespace CRM.winforms.Controls
{
    partial class StatCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblSubtext = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // StatCard
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "StatCard";
            this.Size = new System.Drawing.Size(240, 110);
            this.Padding = new System.Windows.Forms.Padding(16);

            // lblIcon
            this.lblIcon.AutoSize = false;
            this.lblIcon.Size = new System.Drawing.Size(40, 40);
            this.lblIcon.Location = new System.Drawing.Point(16, 16);
            this.lblIcon.Text = "★";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblIcon.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblIcon.Name = "lblIcon";

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(160, 20);
            this.lblTitle.Location = new System.Drawing.Point(64, 16);
            this.lblTitle.Text = "Title";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTitle.Name = "lblTitle";

            // lblValue
            this.lblValue.AutoSize = false;
            this.lblValue.Size = new System.Drawing.Size(200, 42);
            this.lblValue.Location = new System.Drawing.Point(16, 50);
            this.lblValue.Text = "0";
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblValue.Name = "lblValue";
            this.lblValue.AutoEllipsis = true;

            // lblSubtext (optional — used for trend, e.g. "+12% vs last week")
            this.lblSubtext.AutoSize = false;
            this.lblSubtext.Size = new System.Drawing.Size(200, 18);
            this.lblSubtext.Location = new System.Drawing.Point(16, 94);
            this.lblSubtext.Text = "";
            this.lblSubtext.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSubtext.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.lblSubtext.Name = "lblSubtext";
            this.lblSubtext.AutoEllipsis = true;

            // Add controls
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblSubtext);

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblSubtext;

        private System.Windows.Forms.Label lblTrend;
    }
}