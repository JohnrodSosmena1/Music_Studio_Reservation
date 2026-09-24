namespace CRM.winforms.Forms.Customers.Dialogs
{
    partial class AdjustPointsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCustomerLabel = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.lblPlanSummary = new System.Windows.Forms.Label();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblCurrentLabel = new System.Windows.Forms.Label();
            this.lblCurrentPoints = new System.Windows.Forms.Label();
            this.lblExistingAdjustment = new System.Windows.Forms.Label();

            this.lblAmountLabel = new System.Windows.Forms.Label();
            this.numPoints = new System.Windows.Forms.NumericUpDown();

            this.rbAdd = new System.Windows.Forms.RadioButton();
            this.rbSubtract = new System.Windows.Forms.RadioButton();

            this.lblReasonLabel = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();

            this.lblPreviewHeader = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // AdjustPointsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 580);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "AdjustPointsForm";
            this.Text = "Adjust Points";
            this.Load += new System.EventHandler(this.AdjustPointsForm_Load);

            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 130;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 15);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(460, 32);
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Text = "⚙  Adjust Loyalty Points";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblCustomerLabel
            this.lblCustomerLabel.AutoSize = false;
            this.lblCustomerLabel.Size = new System.Drawing.Size(100, 20);
            this.lblCustomerLabel.Location = new System.Drawing.Point(30, 58);
            this.lblCustomerLabel.Text = "Customer:";
            this.lblCustomerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomerLabel.Name = "lblCustomerLabel";

            // lblCustomerName
            this.lblCustomerName.AutoSize = false;
            this.lblCustomerName.Size = new System.Drawing.Size(300, 20);
            this.lblCustomerName.Location = new System.Drawing.Point(120, 58);
            this.lblCustomerName.Text = "—";
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblCustomerName.Name = "lblCustomerName";

            // lblCustomerCode
            this.lblCustomerCode.AutoSize = false;
            this.lblCustomerCode.Size = new System.Drawing.Size(400, 20);
            this.lblCustomerCode.Location = new System.Drawing.Point(30, 80);
            this.lblCustomerCode.Text = "";
            this.lblCustomerCode.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustomerCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomerCode.Name = "lblCustomerCode";

            // lblPlanSummary
            this.lblPlanSummary.AutoSize = false;
            this.lblPlanSummary.Size = new System.Drawing.Size(460, 22);
            this.lblPlanSummary.Location = new System.Drawing.Point(30, 100);
            this.lblPlanSummary.Text = "";
            this.lblPlanSummary.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPlanSummary.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblPlanSummary.Name = "lblPlanSummary";

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblCustomerLabel);
            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Controls.Add(this.lblCustomerCode);
            this.pnlHeader.Controls.Add(this.lblPlanSummary);

            // pnlBody
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);

            // lblCurrentLabel
            this.lblCurrentLabel.AutoSize = false;
            this.lblCurrentLabel.Size = new System.Drawing.Size(200, 24);
            this.lblCurrentLabel.Location = new System.Drawing.Point(30, 20);
            this.lblCurrentLabel.Text = "Current Total Points";
            this.lblCurrentLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCurrentLabel.Name = "lblCurrentLabel";

            // lblCurrentPoints
            this.lblCurrentPoints.AutoSize = false;
            this.lblCurrentPoints.Size = new System.Drawing.Size(460, 40);
            this.lblCurrentPoints.Location = new System.Drawing.Point(30, 45);
            this.lblCurrentPoints.Text = "0";
            this.lblCurrentPoints.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPoints.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblCurrentPoints.Name = "lblCurrentPoints";

            // lblExistingAdjustment
            this.lblExistingAdjustment.AutoSize = false;
            this.lblExistingAdjustment.Size = new System.Drawing.Size(460, 20);
            this.lblExistingAdjustment.Location = new System.Drawing.Point(30, 88);
            this.lblExistingAdjustment.Text = "";
            this.lblExistingAdjustment.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblExistingAdjustment.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblExistingAdjustment.Visible = false;
            this.lblExistingAdjustment.Name = "lblExistingAdjustment";

            // lblAmountLabel
            this.lblAmountLabel.AutoSize = false;
            this.lblAmountLabel.Size = new System.Drawing.Size(200, 24);
            this.lblAmountLabel.Location = new System.Drawing.Point(30, 125);
            this.lblAmountLabel.Text = "Amount";
            this.lblAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAmountLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblAmountLabel.Name = "lblAmountLabel";

            // numPoints
            this.numPoints.Location = new System.Drawing.Point(30, 152);
            this.numPoints.Size = new System.Drawing.Size(150, 30);
            this.numPoints.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numPoints.Minimum = 0;
            this.numPoints.Maximum = 1000000;
            this.numPoints.Value = 0;
            this.numPoints.ThousandsSeparator = true;
            this.numPoints.Name = "numPoints";
            this.numPoints.ValueChanged += new System.EventHandler(this.numPoints_ValueChanged);

            // rbAdd
            this.rbAdd.AutoSize = true;
            this.rbAdd.Location = new System.Drawing.Point(210, 157);
            this.rbAdd.Text = "Add points";
            this.rbAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbAdd.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.rbAdd.Checked = true;
            this.rbAdd.Name = "rbAdd";
            this.rbAdd.CheckedChanged += new System.EventHandler(this.rbAdd_CheckedChanged);

            // rbSubtract
            this.rbSubtract.AutoSize = true;
            this.rbSubtract.Location = new System.Drawing.Point(320, 157);
            this.rbSubtract.Text = "Subtract points";
            this.rbSubtract.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbSubtract.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.rbSubtract.Name = "rbSubtract";
            this.rbSubtract.CheckedChanged += new System.EventHandler(this.rbSubtract_CheckedChanged);

            // lblReasonLabel
            this.lblReasonLabel.AutoSize = false;
            this.lblReasonLabel.Size = new System.Drawing.Size(200, 24);
            this.lblReasonLabel.Location = new System.Drawing.Point(30, 205);
            this.lblReasonLabel.Text = "Reason (required)";
            this.lblReasonLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReasonLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblReasonLabel.Name = "lblReasonLabel";

            // txtReason
            this.txtReason.Location = new System.Drawing.Point(30, 232);
            this.txtReason.Size = new System.Drawing.Size(440, 80);
            this.txtReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReason.Multiline = true;
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.PlaceholderText = "e.g., Customer complaint refund, referral bonus, correction for system glitch...";
            this.txtReason.Name = "txtReason";

            // lblPreviewHeader
            this.lblPreviewHeader.AutoSize = false;
            this.lblPreviewHeader.Size = new System.Drawing.Size(200, 24);
            this.lblPreviewHeader.Location = new System.Drawing.Point(30, 330);
            this.lblPreviewHeader.Text = "Preview";
            this.lblPreviewHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPreviewHeader.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblPreviewHeader.Name = "lblPreviewHeader";

            // lblPreview
            this.lblPreview.AutoSize = false;
            this.lblPreview.Size = new System.Drawing.Size(440, 60);
            this.lblPreview.Location = new System.Drawing.Point(30, 358);
            this.lblPreview.Text = "Enter an amount to see the effect.";
            this.lblPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblPreview.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.lblPreview.Padding = new System.Windows.Forms.Padding(15);
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPreview.Name = "lblPreview";

            this.pnlBody.Controls.Add(this.lblCurrentLabel);
            this.pnlBody.Controls.Add(this.lblCurrentPoints);
            this.pnlBody.Controls.Add(this.lblExistingAdjustment);
            this.pnlBody.Controls.Add(this.lblAmountLabel);
            this.pnlBody.Controls.Add(this.numPoints);
            this.pnlBody.Controls.Add(this.rbAdd);
            this.pnlBody.Controls.Add(this.rbSubtract);
            this.pnlBody.Controls.Add(this.lblReasonLabel);
            this.pnlBody.Controls.Add(this.txtReason);
            this.pnlBody.Controls.Add(this.lblPreviewHeader);
            this.pnlBody.Controls.Add(this.lblPreview);

            // pnlFooter
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 70;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            // btnCancel
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Location = new System.Drawing.Point(240, 15);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnSave
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.Location = new System.Drawing.Point(370, 15);
            this.btnSave.Text = "Save";
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);

            // Assembly
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomerLabel;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label lblPlanSummary;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblCurrentLabel;
        private System.Windows.Forms.Label lblCurrentPoints;
        private System.Windows.Forms.Label lblExistingAdjustment;
        private System.Windows.Forms.Label lblAmountLabel;
        private System.Windows.Forms.NumericUpDown numPoints;
        private System.Windows.Forms.RadioButton rbAdd;
        private System.Windows.Forms.RadioButton rbSubtract;
        private System.Windows.Forms.Label lblReasonLabel;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblPreviewHeader;
        private System.Windows.Forms.Label lblPreview;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}