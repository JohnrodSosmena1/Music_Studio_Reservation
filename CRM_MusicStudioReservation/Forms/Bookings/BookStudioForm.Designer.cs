namespace CRM_MusicStudioReservation.Forms.Bookings
{
    partial class BookStudioForm
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStepIndicator = new System.Windows.Forms.Label();

            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomRight = new System.Windows.Forms.Panel();   // ← NEW container
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // ==== Form ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Name = "BookStudioForm";
            this.Text = "Book a Studio";
            this.Load += new System.EventHandler(this.BookStudioForm_Load);

            // ==== Top ====
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 110;
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(800, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Text = "Book a Studio";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(800, 22);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 50);
            this.lblSubtitle.Text = "Follow the steps to reserve your session";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            this.lblStepIndicator.AutoSize = false;
            this.lblStepIndicator.Size = new System.Drawing.Size(400, 25);
            this.lblStepIndicator.Location = new System.Drawing.Point(30, 80);
            this.lblStepIndicator.Text = "Step 1 of 4 — Select a Studio";
            this.lblStepIndicator.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStepIndicator.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblStepIndicator.Name = "lblStepIndicator";

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Controls.Add(this.lblStepIndicator);

            // ==== Content ====
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Name = "pnlContent";

            // ==== Bottom (navigation) ====
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 80;
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Name = "pnlBottom";

            // ---- Cancel button (docked left) ----
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Location = new System.Drawing.Point(30, 20);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ---- Right-side container for Back/Next buttons ----
            this.pnlBottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBottomRight.Width = 320;
            this.pnlBottomRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottomRight.Name = "pnlBottomRight";

            // ---- Back button ----
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.Location = new System.Drawing.Point(40, 20);
            this.btnBack.Text = "Back";
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ---- Next button ----
            this.btnNext.Size = new System.Drawing.Size(140, 40);
            this.btnNext.Location = new System.Drawing.Point(150, 20);
            this.btnNext.Text = "Next →";
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // Assemble the bottom panel
            this.pnlBottomRight.Controls.Add(this.btnBack);
            this.pnlBottomRight.Controls.Add(this.btnNext);

            this.pnlBottom.Controls.Add(this.pnlBottomRight);
            this.pnlBottom.Controls.Add(this.btnCancel);

            // ==== Add to Form ====
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStepIndicator;

        private System.Windows.Forms.Panel pnlContent;

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomRight;   // ← NEW
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
    }
}