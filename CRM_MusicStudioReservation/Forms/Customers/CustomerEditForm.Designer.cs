namespace CRM_MusicStudioReservation.Forms.Customers
{
    partial class CustomerEditForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstNameError = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastNameError = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblContactError = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmailError = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ==== CustomerEditForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 560);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerEditForm";
            this.Text = "Customer";
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;

            // ==== lblTitle ====
            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(480, 35);
            this.lblTitle.Location = new System.Drawing.Point(30, 16);
            this.lblTitle.Text = "Add Customer";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);

            // ==== Customer Code (Auto-generated & Read-Only) ====
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(30, 62);
            this.lblCode.Text = "Customer Code (System Generated)";
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);

            this.txtCode.Location = new System.Drawing.Point(30, 82);
            this.txtCode.Size = new System.Drawing.Size(480, 26);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCode.ReadOnly = true;
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ==== First Name ====
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(30, 120);
            this.lblFirstName.Text = "First Name *";
            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtFirstName.Location = new System.Drawing.Point(30, 140);
            this.txtFirstName.Size = new System.Drawing.Size(230, 26);
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblFirstNameError.AutoSize = true;
            this.lblFirstNameError.Location = new System.Drawing.Point(30, 168);
            this.lblFirstNameError.Text = "First Name is required.";
            this.lblFirstNameError.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFirstNameError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblFirstNameError.Visible = false;

            // ==== Last Name ====
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(280, 120);
            this.lblLastName.Text = "Last Name *";
            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtLastName.Location = new System.Drawing.Point(280, 140);
            this.txtLastName.Size = new System.Drawing.Size(230, 26);
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblLastNameError.AutoSize = true;
            this.lblLastNameError.Location = new System.Drawing.Point(280, 168);
            this.lblLastNameError.Text = "Last Name is required.";
            this.lblLastNameError.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblLastNameError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblLastNameError.Visible = false;

            // ==== Contact Number ====
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(30, 192);
            this.lblContact.Text = "Contact Number";
            this.lblContact.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblContact.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtContact.Location = new System.Drawing.Point(30, 212);
            this.txtContact.Size = new System.Drawing.Size(480, 26);
            this.txtContact.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblContactError.AutoSize = true;
            this.lblContactError.Location = new System.Drawing.Point(30, 240);
            this.lblContactError.Text = "Must contain digits only (e.g., 09171234567 or +1-555-0101).";
            this.lblContactError.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblContactError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblContactError.Visible = false;

            // ==== Email Address ====
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 262);
            this.lblEmail.Text = "Email Address";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtEmail.Location = new System.Drawing.Point(30, 282);
            this.txtEmail.Size = new System.Drawing.Size(480, 26);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblEmailError.AutoSize = true;
            this.lblEmailError.Location = new System.Drawing.Point(30, 310);
            this.lblEmailError.Text = "Please enter a valid email address (e.g., user@domain.com).";
            this.lblEmailError.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblEmailError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblEmailError.Visible = false;

            // ==== Address ====
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(30, 332);
            this.lblAddress.Text = "Address";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtAddress.Location = new System.Drawing.Point(30, 352);
            this.txtAddress.Size = new System.Drawing.Size(480, 55);
            this.txtAddress.Multiline = true;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ==== Active checkbox ====
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(30, 420);
            this.chkActive.Text = "Active Customer";
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.chkActive.Checked = true;

            // ==== Server-side Error label ====
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(480, 32);
            this.lblError.Location = new System.Drawing.Point(30, 450);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Visible = false;

            // ==== Buttons ====
            this.btnCancel.Size = new System.Drawing.Size(110, 38);
            this.btnCancel.Location = new System.Drawing.Point(280, 500);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.Location = new System.Drawing.Point(400, 500);
            this.btnSave.Text = "Save";
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstNameError);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastNameError);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.lblContactError);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmailError);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblFirstNameError;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblLastNameError;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblContactError;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmailError;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}