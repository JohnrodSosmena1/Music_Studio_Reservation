namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class FeedbackEditForm
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

            this.lblCustomer = new System.Windows.Forms.Label();
            this.numCustomerId = new System.Windows.Forms.NumericUpDown();

            this.lblRating = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblStarPreview = new System.Windows.Forms.Label();

            this.lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerId)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== FeedbackEditForm — COMPACT (fits in 520px) ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeedbackEditForm";
            this.Text = "Feedback";
            this.Load += new System.EventHandler(this.FeedbackEditForm_Load);

            // ============ HEADER ============
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 75);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 30);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "Record Feedback";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 20);
            this.lblSubheader.Location = new System.Drawing.Point(30, 48);
            this.lblSubheader.Text = "Enter customer's rating and comments";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ============ BODY ============
            this.pnlBody.Location = new System.Drawing.Point(0, 75);
            this.pnlBody.Size = new System.Drawing.Size(620, 365);
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Name = "pnlBody";

            // ---- Customer ID ----
            this.lblCustomer.AutoSize = false;
            this.lblCustomer.Size = new System.Drawing.Size(560, 16);
            this.lblCustomer.Location = new System.Drawing.Point(30, 20);
            this.lblCustomer.Text = "Customer ID *";
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomer.Name = "lblCustomer";

            this.numCustomerId.Size = new System.Drawing.Size(560, 26);
            this.numCustomerId.Location = new System.Drawing.Point(30, 38);
            this.numCustomerId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCustomerId.Minimum = 1;
            this.numCustomerId.Maximum = 1000000;
            this.numCustomerId.Value = 1;
            this.numCustomerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCustomerId.Name = "numCustomerId";

            // ---- Rating ----
            this.lblRating.AutoSize = false;
            this.lblRating.Size = new System.Drawing.Size(260, 16);
            this.lblRating.Location = new System.Drawing.Point(30, 82);
            this.lblRating.Text = "Rating *";
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblRating.Name = "lblRating";

            this.cmbRating.Size = new System.Drawing.Size(260, 26);
            this.cmbRating.Location = new System.Drawing.Point(30, 100);
            this.cmbRating.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.SelectedIndexChanged += new System.EventHandler(this.cmbRating_SelectedIndexChanged);

            // ---- Star Preview ----
            this.lblStarPreview.AutoSize = false;
            this.lblStarPreview.Size = new System.Drawing.Size(260, 26);
            this.lblStarPreview.Location = new System.Drawing.Point(310, 100);
            this.lblStarPreview.Text = "☆☆☆☆☆";
            this.lblStarPreview.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStarPreview.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.lblStarPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStarPreview.Name = "lblStarPreview";

            // ---- Comments ----
            this.lblComments.AutoSize = false;
            this.lblComments.Size = new System.Drawing.Size(560, 16);
            this.lblComments.Location = new System.Drawing.Point(30, 148);
            this.lblComments.Text = "Comments";
            this.lblComments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblComments.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblComments.Name = "lblComments";

            this.txtComments.Size = new System.Drawing.Size(560, 120);
            this.txtComments.Location = new System.Drawing.Point(30, 166);
            this.txtComments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComments.Multiline = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Name = "txtComments";

            // ---- Error Label ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 298);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCustomer);
            this.pnlBody.Controls.Add(this.numCustomerId);
            this.pnlBody.Controls.Add(this.lblRating);
            this.pnlBody.Controls.Add(this.cmbRating);
            this.pnlBody.Controls.Add(this.lblStarPreview);
            this.pnlBody.Controls.Add(this.lblComments);
            this.pnlBody.Controls.Add(this.txtComments);
            this.pnlBody.Controls.Add(this.lblError);

            // ============ FOOTER ============
            this.pnlFooter.Location = new System.Drawing.Point(0, 440);
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
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerId)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.NumericUpDown numCustomerId;

        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Label lblStarPreview;

        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.TextBox txtComments;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}