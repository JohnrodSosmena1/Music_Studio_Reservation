namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class PromotionEditForm
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

            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();

            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();

            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();

            this.lblDiscount = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();

            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();

            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== PromotionEditForm — COMPACT ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 540);   // 👈 compact
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromotionEditForm";
            this.Text = "Promotion";
            this.Load += new System.EventHandler(this.PromotionEditForm_Load);

            // ============ HEADER (compact) ============
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 75);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 30);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "New Promotion";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 20);
            this.lblSubheader.Location = new System.Drawing.Point(30, 48);
            this.lblSubheader.Text = "Fill in the details below";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ============ BODY (compact — fields close together) ============
            this.pnlBody.Location = new System.Drawing.Point(0, 75);
            this.pnlBody.Size = new System.Drawing.Size(620, 385);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Promotion Code (y=10) ----
            this.lblCode.AutoSize = false;
            this.lblCode.Size = new System.Drawing.Size(560, 16);
            this.lblCode.Location = new System.Drawing.Point(30, 10);
            this.lblCode.Text = "Promotion Code *";
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCode.Name = "lblCode";

            this.txtCode.Size = new System.Drawing.Size(560, 26);
            this.txtCode.Location = new System.Drawing.Point(30, 28);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Name = "txtCode";

            // ---- Promotion Name (y=62) ----
            this.lblName.AutoSize = false;
            this.lblName.Size = new System.Drawing.Size(560, 16);
            this.lblName.Location = new System.Drawing.Point(30, 62);
            this.lblName.Text = "Promotion Name *";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblName.Name = "lblName";

            this.txtName.Size = new System.Drawing.Size(560, 26);
            this.txtName.Location = new System.Drawing.Point(30, 80);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Name = "txtName";

            // ---- Description (y=114) ----
            this.lblDescription.AutoSize = false;
            this.lblDescription.Size = new System.Drawing.Size(560, 16);
            this.lblDescription.Location = new System.Drawing.Point(30, 114);
            this.lblDescription.Text = "Description";
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDescription.Name = "lblDescription";

            this.txtDescription.Size = new System.Drawing.Size(560, 50);
            this.txtDescription.Location = new System.Drawing.Point(30, 132);
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Name = "txtDescription";

            // ---- Discount (y=190) ----
            this.lblDiscount.AutoSize = false;
            this.lblDiscount.Size = new System.Drawing.Size(260, 16);
            this.lblDiscount.Location = new System.Drawing.Point(30, 190);
            this.lblDiscount.Text = "Discount Percent *";
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblDiscount.Name = "lblDiscount";

            this.numDiscount.Size = new System.Drawing.Size(260, 26);
            this.numDiscount.Location = new System.Drawing.Point(30, 208);
            this.numDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numDiscount.DecimalPlaces = 2;
            this.numDiscount.Maximum = 100;
            this.numDiscount.Minimum = 0;
            this.numDiscount.Increment = 1;
            this.numDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numDiscount.Name = "numDiscount";

            // ---- Start Date (y=242) ----
            this.lblStart.AutoSize = false;
            this.lblStart.Size = new System.Drawing.Size(260, 16);
            this.lblStart.Location = new System.Drawing.Point(30, 242);
            this.lblStart.Text = "Start Date *";
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStart.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStart.Name = "lblStart";

            this.dtpStart.Size = new System.Drawing.Size(260, 26);
            this.dtpStart.Location = new System.Drawing.Point(30, 260);
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Name = "dtpStart";

            // ---- End Date (y=242, right side) ----
            this.lblEnd.AutoSize = false;
            this.lblEnd.Size = new System.Drawing.Size(260, 16);
            this.lblEnd.Location = new System.Drawing.Point(330, 242);
            this.lblEnd.Text = "End Date *";
            this.lblEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEnd.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEnd.Name = "lblEnd";

            this.dtpEnd.Size = new System.Drawing.Size(260, 26);
            this.dtpEnd.Location = new System.Drawing.Point(330, 260);
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Name = "dtpEnd";

            // ---- Error Label (y=295) ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 295);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCode);
            this.pnlBody.Controls.Add(this.txtCode);
            this.pnlBody.Controls.Add(this.lblName);
            this.pnlBody.Controls.Add(this.txtName);
            this.pnlBody.Controls.Add(this.lblDescription);
            this.pnlBody.Controls.Add(this.txtDescription);
            this.pnlBody.Controls.Add(this.lblDiscount);
            this.pnlBody.Controls.Add(this.numDiscount);
            this.pnlBody.Controls.Add(this.lblStart);
            this.pnlBody.Controls.Add(this.dtpStart);
            this.pnlBody.Controls.Add(this.lblEnd);
            this.pnlBody.Controls.Add(this.dtpEnd);
            this.pnlBody.Controls.Add(this.lblError);

            // ============ FOOTER (y=460) ============
            this.pnlFooter.Location = new System.Drawing.Point(0, 460);
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
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;

        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.NumericUpDown numDiscount;

        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;

        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}