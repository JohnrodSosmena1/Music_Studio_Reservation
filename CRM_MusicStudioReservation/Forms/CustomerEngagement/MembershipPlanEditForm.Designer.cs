namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class MembershipPlanEditForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubheader = new System.Windows.Forms.Label();

            this.pnlBody = new System.Windows.Forms.Panel();

            this.lblPlanName = new System.Windows.Forms.Label();
            this.txtPlanName = new System.Windows.Forms.TextBox();

            this.lblFee = new System.Windows.Forms.Label();
            this.numFee = new System.Windows.Forms.NumericUpDown();

            this.lblPoints = new System.Windows.Forms.Label();
            this.numPoints = new System.Windows.Forms.NumericUpDown();

            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();

            this.lblBenefits = new System.Windows.Forms.Label();
            this.txtBenefits = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== MembershipPlanEditForm — compact (~560px) ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MembershipPlanEditForm";
            this.Text = "Membership Plan";
            this.Load += new System.EventHandler(this.MembershipPlanEditForm_Load);

            // ==== HEADER ====
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 70);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 28);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "New Membership Plan";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 18);
            this.lblSubheader.Location = new System.Drawing.Point(30, 45);
            this.lblSubheader.Text = "Define the plan details and benefits";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ==== BODY ====
            this.pnlBody.Location = new System.Drawing.Point(0, 70);
            this.pnlBody.Size = new System.Drawing.Size(620, 410);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Plan Name ----
            this.lblPlanName.AutoSize = false;
            this.lblPlanName.Size = new System.Drawing.Size(560, 16);
            this.lblPlanName.Location = new System.Drawing.Point(30, 15);
            this.lblPlanName.Text = "Plan Name *";
            this.lblPlanName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlanName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblPlanName.Name = "lblPlanName";

            this.txtPlanName.Size = new System.Drawing.Size(560, 26);
            this.txtPlanName.Location = new System.Drawing.Point(30, 33);
            this.txtPlanName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPlanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanName.Name = "txtPlanName";

            // ---- Monthly Fee + Points (side by side) ----
            this.lblFee.AutoSize = false;
            this.lblFee.Size = new System.Drawing.Size(260, 16);
            this.lblFee.Location = new System.Drawing.Point(30, 71);
            this.lblFee.Text = "Monthly Fee (₱) *";
            this.lblFee.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFee.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFee.Name = "lblFee";

            this.numFee.Size = new System.Drawing.Size(260, 26);
            this.numFee.Location = new System.Drawing.Point(30, 89);
            this.numFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numFee.DecimalPlaces = 2;
            this.numFee.Maximum = 100000;
            this.numFee.Minimum = 0;
            this.numFee.ThousandsSeparator = true;
            this.numFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numFee.Name = "numFee";

            this.lblPoints.AutoSize = false;
            this.lblPoints.Size = new System.Drawing.Size(260, 16);
            this.lblPoints.Location = new System.Drawing.Point(330, 71);
            this.lblPoints.Text = "Loyalty Points Per Booking *";
            this.lblPoints.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPoints.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblPoints.Name = "lblPoints";

            this.numPoints.Size = new System.Drawing.Size(260, 26);
            this.numPoints.Location = new System.Drawing.Point(330, 89);
            this.numPoints.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPoints.Maximum = 10000;
            this.numPoints.Minimum = 0;
            this.numPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numPoints.Name = "numPoints";

            // ---- Description ----
            this.lblDescription.AutoSize = false;
            this.lblDescription.Size = new System.Drawing.Size(560, 16);
            this.lblDescription.Location = new System.Drawing.Point(30, 127);
            this.lblDescription.Text = "Description";
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDescription.Name = "lblDescription";

            this.txtDescription.Size = new System.Drawing.Size(560, 60);
            this.txtDescription.Location = new System.Drawing.Point(30, 145);
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Name = "txtDescription";

            // ---- Benefits ----
            this.lblBenefits.AutoSize = false;
            this.lblBenefits.Size = new System.Drawing.Size(560, 16);
            this.lblBenefits.Location = new System.Drawing.Point(30, 218);
            this.lblBenefits.Text = "Benefits";
            this.lblBenefits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBenefits.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblBenefits.Name = "lblBenefits";

            this.txtBenefits.Size = new System.Drawing.Size(560, 90);
            this.txtBenefits.Location = new System.Drawing.Point(30, 236);
            this.txtBenefits.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBenefits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenefits.Multiline = true;
            this.txtBenefits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBenefits.Name = "txtBenefits";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 335);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblPlanName);
            this.pnlBody.Controls.Add(this.txtPlanName);
            this.pnlBody.Controls.Add(this.lblFee);
            this.pnlBody.Controls.Add(this.numFee);
            this.pnlBody.Controls.Add(this.lblPoints);
            this.pnlBody.Controls.Add(this.numPoints);
            this.pnlBody.Controls.Add(this.lblDescription);
            this.pnlBody.Controls.Add(this.txtDescription);
            this.pnlBody.Controls.Add(this.lblBenefits);
            this.pnlBody.Controls.Add(this.txtBenefits);
            this.pnlBody.Controls.Add(this.lblError);

            // ==== FOOTER ====
            this.pnlFooter.Location = new System.Drawing.Point(0, 480);
            this.pnlFooter.Size = new System.Drawing.Size(620, 80);
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Name = "pnlFooter";

            this.btnCancel.Size = new System.Drawing.Size(130, 42);
            this.btnCancel.Location = new System.Drawing.Point(340, 18);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.Size = new System.Drawing.Size(130, 42);
            this.btnSave.Location = new System.Drawing.Point(480, 18);
            this.btnSave.Text = "Save";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblPlanName;
        private System.Windows.Forms.TextBox txtPlanName;

        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.NumericUpDown numFee;

        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.NumericUpDown numPoints;

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;

        private System.Windows.Forms.Label lblBenefits;
        private System.Windows.Forms.TextBox txtBenefits;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}