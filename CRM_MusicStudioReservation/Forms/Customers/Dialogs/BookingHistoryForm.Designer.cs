namespace CRM.winforms.Forms.Customers.Dialogs
{
    partial class BookingHistoryForm
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
            this.dgvHistory = new System.Windows.Forms.DataGridView();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // BookingHistoryForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "BookingHistoryForm";
            this.Text = "Booking History";
            this.Load += new System.EventHandler(this.BookingHistoryForm_Load);

            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 130;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 15);

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(840, 32);
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Text = "📅  Booking History";
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
            this.lblCustomerName.Size = new System.Drawing.Size(400, 20);
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
            this.lblPlanSummary.Size = new System.Drawing.Size(840, 22);
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
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            // dgvHistory
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersHeight = 38;
            this.dgvHistory.RowTemplate.Height = 38;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvHistory.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvHistory.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvHistory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvHistory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvHistory.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.EnableHeadersVisualStyles = false;

            this.pnlBody.Controls.Add(this.dgvHistory);

            // pnlFooter
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 70;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            // lblCount
            this.lblCount.AutoSize = false;
            this.lblCount.Size = new System.Drawing.Size(500, 40);
            this.lblCount.Location = new System.Drawing.Point(30, 15);
            this.lblCount.Text = "0 booking(s)";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Name = "lblCount";

            // btnClose
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.Location = new System.Drawing.Point(720, 15);
            this.btnClose.Text = "Close";
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.pnlFooter.Controls.Add(this.lblCount);
            this.pnlFooter.Controls.Add(this.btnClose);

            // Assembly
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvHistory;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnClose;
    }
}