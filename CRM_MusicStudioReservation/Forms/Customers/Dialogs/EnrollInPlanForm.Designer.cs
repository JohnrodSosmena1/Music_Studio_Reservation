namespace CRM.winforms.Forms.Customers.Dialogs
{
    partial class EnrollInPlanForm
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

            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblPlanLabel = new System.Windows.Forms.Label();
            this.cmbPlan = new System.Windows.Forms.ComboBox();

            this.lblStartLabel = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEndLabel = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();

            this.lblDetailsHeader = new System.Windows.Forms.Label();
            this.lblPlanDetails = new System.Windows.Forms.Label();
            this.lblPointsPreview = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEnroll = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // EnrollInPlanForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "EnrollInPlanForm";
            this.Text = "Enroll in Plan";
            this.Load += new System.EventHandler(this.EnrollInPlanForm_Load);

            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 110;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 15);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(460, 32);
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Text = "🏆  Enroll Customer in a Plan";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            // lblCustomerLabel
            this.lblCustomerLabel.AutoSize = false;
            this.lblCustomerLabel.Size = new System.Drawing.Size(120, 20);
            this.lblCustomerLabel.Location = new System.Drawing.Point(30, 60);
            this.lblCustomerLabel.Text = "Customer:";
            this.lblCustomerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomerLabel.Name = "lblCustomerLabel";

            // lblCustomerName
            this.lblCustomerName.AutoSize = false;
            this.lblCustomerName.Size = new System.Drawing.Size(300, 20);
            this.lblCustomerName.Location = new System.Drawing.Point(110, 60);
            this.lblCustomerName.Text = "—";
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblCustomerName.Name = "lblCustomerName";

            // lblCustomerCode
            this.lblCustomerCode.AutoSize = false;
            this.lblCustomerCode.Size = new System.Drawing.Size(300, 20);
            this.lblCustomerCode.Location = new System.Drawing.Point(30, 82);
            this.lblCustomerCode.Text = "";
            this.lblCustomerCode.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustomerCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomerCode.Name = "lblCustomerCode";

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblCustomerLabel);
            this.pnlHeader.Controls.Add(this.lblCustomerName);
            this.pnlHeader.Controls.Add(this.lblCustomerCode);

            // pnlBody
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);

            // lblPlanLabel
            this.lblPlanLabel.AutoSize = false;
            this.lblPlanLabel.Size = new System.Drawing.Size(120, 24);
            this.lblPlanLabel.Location = new System.Drawing.Point(30, 20);
            this.lblPlanLabel.Text = "Membership Plan";
            this.lblPlanLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlanLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblPlanLabel.Name = "lblPlanLabel";

            // cmbPlan
            this.cmbPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlan.Location = new System.Drawing.Point(30, 48);
            this.cmbPlan.Size = new System.Drawing.Size(460, 30);
            this.cmbPlan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlan.Name = "cmbPlan";
            this.cmbPlan.SelectedIndexChanged += new System.EventHandler(this.cmbPlan_SelectedIndexChanged);

            // lblStartLabel
            this.lblStartLabel.AutoSize = false;
            this.lblStartLabel.Size = new System.Drawing.Size(120, 24);
            this.lblStartLabel.Location = new System.Drawing.Point(30, 95);
            this.lblStartLabel.Text = "Start Date";
            this.lblStartLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblStartLabel.Name = "lblStartLabel";

            // dtpStart
            this.dtpStart.Location = new System.Drawing.Point(30, 122);
            this.dtpStart.Size = new System.Drawing.Size(215, 30);
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStart.Name = "dtpStart";

            // lblEndLabel
            this.lblEndLabel.AutoSize = false;
            this.lblEndLabel.Size = new System.Drawing.Size(120, 24);
            this.lblEndLabel.Location = new System.Drawing.Point(275, 95);
            this.lblEndLabel.Text = "End Date";
            this.lblEndLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEndLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblEndLabel.Name = "lblEndLabel";

            // dtpEnd
            this.dtpEnd.Location = new System.Drawing.Point(275, 122);
            this.dtpEnd.Size = new System.Drawing.Size(215, 30);
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpEnd.Name = "dtpEnd";

            // lblDetailsHeader
            this.lblDetailsHeader.AutoSize = false;
            this.lblDetailsHeader.Size = new System.Drawing.Size(460, 24);
            this.lblDetailsHeader.Location = new System.Drawing.Point(30, 175);
            this.lblDetailsHeader.Text = "Plan Details";
            this.lblDetailsHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblDetailsHeader.Name = "lblDetailsHeader";

            // lblPlanDetails
            this.lblPlanDetails.AutoSize = false;
            this.lblPlanDetails.Size = new System.Drawing.Size(460, 130);
            this.lblPlanDetails.Location = new System.Drawing.Point(30, 202);
            this.lblPlanDetails.Text = "Select a plan above to see its details.";
            this.lblPlanDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPlanDetails.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblPlanDetails.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.lblPlanDetails.Padding = new System.Windows.Forms.Padding(15);
            this.lblPlanDetails.Name = "lblPlanDetails";

            // lblPointsPreview
            this.lblPointsPreview.AutoSize = false;
            this.lblPointsPreview.Size = new System.Drawing.Size(460, 24);
            this.lblPointsPreview.Location = new System.Drawing.Point(30, 340);
            this.lblPointsPreview.Text = "";
            this.lblPointsPreview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblPointsPreview.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblPointsPreview.Name = "lblPointsPreview";

            this.pnlBody.Controls.Add(this.lblPlanLabel);
            this.pnlBody.Controls.Add(this.cmbPlan);
            this.pnlBody.Controls.Add(this.lblStartLabel);
            this.pnlBody.Controls.Add(this.dtpStart);
            this.pnlBody.Controls.Add(this.lblEndLabel);
            this.pnlBody.Controls.Add(this.dtpEnd);
            this.pnlBody.Controls.Add(this.lblDetailsHeader);
            this.pnlBody.Controls.Add(this.lblPlanDetails);
            this.pnlBody.Controls.Add(this.lblPointsPreview);

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

            // btnEnroll
            this.btnEnroll.Size = new System.Drawing.Size(150, 40);
            this.btnEnroll.Location = new System.Drawing.Point(370, 15);
            this.btnEnroll.Text = "Enroll";
            this.btnEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnroll.FlatAppearance.BorderSize = 0;
            this.btnEnroll.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnEnroll.ForeColor = System.Drawing.Color.White;
            this.btnEnroll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Click += new System.EventHandler(this.btnEnroll_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnEnroll);

            // Assembly
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustomerLabel;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblPlanLabel;
        private System.Windows.Forms.ComboBox cmbPlan;
        private System.Windows.Forms.Label lblStartLabel;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEndLabel;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblDetailsHeader;
        private System.Windows.Forms.Label lblPlanDetails;
        private System.Windows.Forms.Label lblPointsPreview;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEnroll;
    }
}