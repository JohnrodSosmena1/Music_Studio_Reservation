namespace CRM.winforms.Forms.CustomerEngagement
{
    partial class ReviewEditForm
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

            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();

            this.lblStudio = new System.Windows.Forms.Label();
            this.txtStudioId = new System.Windows.Forms.TextBox();

            this.lblRating = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblStarPreview = new System.Windows.Forms.Label();

            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();

            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerId)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== ReviewEditForm — COMPACT (560px tall) ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(620, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReviewEditForm";
            this.Text = "Review";
            this.Load += new System.EventHandler(this.ReviewEditForm_Load);

            // ==== HEADER ====
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(620, 70);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(560, 28);
            this.lblHeader.Location = new System.Drawing.Point(30, 15);
            this.lblHeader.Text = "Record Review";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(560, 18);
            this.lblSubheader.Location = new System.Drawing.Point(30, 45);
            this.lblSubheader.Text = "Enter customer review details";
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

            // ---- Customer ID ----
            this.lblCustomer.AutoSize = false;
            this.lblCustomer.Size = new System.Drawing.Size(260, 16);
            this.lblCustomer.Location = new System.Drawing.Point(30, 15);
            this.lblCustomer.Text = "Customer ID *";
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCustomer.Name = "lblCustomer";

            this.numCustomerId.Size = new System.Drawing.Size(260, 26);
            this.numCustomerId.Location = new System.Drawing.Point(30, 33);
            this.numCustomerId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCustomerId.Minimum = 1;
            this.numCustomerId.Maximum = 1000000;
            this.numCustomerId.Value = 1;
            this.numCustomerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCustomerId.Name = "numCustomerId";

            // ---- Review Type ----
            this.lblType.AutoSize = false;
            this.lblType.Size = new System.Drawing.Size(260, 16);
            this.lblType.Location = new System.Drawing.Point(330, 15);
            this.lblType.Text = "Review Type *";
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblType.Name = "lblType";

            this.cmbType.Size = new System.Drawing.Size(260, 26);
            this.cmbType.Location = new System.Drawing.Point(330, 33);
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.Name = "cmbType";

            // ---- Studio ID (Optional) ----
            this.lblStudio.AutoSize = false;
            this.lblStudio.Size = new System.Drawing.Size(260, 16);
            this.lblStudio.Location = new System.Drawing.Point(30, 71);
            this.lblStudio.Text = "Studio ID (optional)";
            this.lblStudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStudio.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStudio.Name = "lblStudio";

            this.txtStudioId.Size = new System.Drawing.Size(260, 26);
            this.txtStudioId.Location = new System.Drawing.Point(30, 89);
            this.txtStudioId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStudioId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStudioId.Name = "txtStudioId";

            // ---- Rating ----
            this.lblRating.AutoSize = false;
            this.lblRating.Size = new System.Drawing.Size(260, 16);
            this.lblRating.Location = new System.Drawing.Point(330, 71);
            this.lblRating.Text = "Rating *";
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblRating.Name = "lblRating";

            this.cmbRating.Size = new System.Drawing.Size(180, 26);
            this.cmbRating.Location = new System.Drawing.Point(330, 89);
            this.cmbRating.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.SelectedIndexChanged += new System.EventHandler(this.cmbRating_SelectedIndexChanged);

            this.lblStarPreview.AutoSize = false;
            this.lblStarPreview.Size = new System.Drawing.Size(80, 26);
            this.lblStarPreview.Location = new System.Drawing.Point(520, 89);
            this.lblStarPreview.Text = "☆☆☆☆☆";
            this.lblStarPreview.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStarPreview.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.lblStarPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStarPreview.Name = "lblStarPreview";

            // ---- Title ----
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(560, 16);
            this.lblTitle.Location = new System.Drawing.Point(30, 127);
            this.lblTitle.Text = "Title";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblTitle.Name = "lblTitle";

            this.txtTitle.Size = new System.Drawing.Size(560, 26);
            this.txtTitle.Location = new System.Drawing.Point(30, 145);
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Name = "txtTitle";

            // ---- Comment ----
            this.lblComment.AutoSize = false;
            this.lblComment.Size = new System.Drawing.Size(560, 16);
            this.lblComment.Location = new System.Drawing.Point(30, 183);
            this.lblComment.Text = "Comment *";
            this.lblComment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblComment.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblComment.Name = "lblComment";

            this.txtComment.Size = new System.Drawing.Size(560, 140);
            this.txtComment.Location = new System.Drawing.Point(30, 201);
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Multiline = true;
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Name = "txtComment";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(560, 22);
            this.lblError.Location = new System.Drawing.Point(30, 350);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCustomer);
            this.pnlBody.Controls.Add(this.numCustomerId);
            this.pnlBody.Controls.Add(this.lblType);
            this.pnlBody.Controls.Add(this.cmbType);
            this.pnlBody.Controls.Add(this.lblStudio);
            this.pnlBody.Controls.Add(this.txtStudioId);
            this.pnlBody.Controls.Add(this.lblRating);
            this.pnlBody.Controls.Add(this.cmbRating);
            this.pnlBody.Controls.Add(this.lblStarPreview);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Controls.Add(this.txtTitle);
            this.pnlBody.Controls.Add(this.lblComment);
            this.pnlBody.Controls.Add(this.txtComment);
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

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;

        private System.Windows.Forms.Label lblStudio;
        private System.Windows.Forms.TextBox txtStudioId;

        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Label lblStarPreview;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;

        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}