namespace CRM.winforms.Controls
{
    partial class StatusBadge
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
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // StatusBadge
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "StatusBadge";
            this.Size = new System.Drawing.Size(100, 26);
            this.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);

            // lblText
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Text = "Confirmed";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Name = "lblText";

            this.Controls.Add(this.lblText);

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblText;
    }
}