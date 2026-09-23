namespace CRM.winforms.Forms.Inventory
{
    partial class InventoryItemEditForm
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

            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();

            this.lblCondition = new System.Windows.Forms.Label();
            this.cmbCondition = new System.Windows.Forms.ComboBox();

            this.lblAvailability = new System.Windows.Forms.Label();
            this.cmbAvailability = new System.Windows.Forms.ComboBox();

            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();

            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();

            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.numReorderLevel = new System.Windows.Forms.NumericUpDown();

            this.lblUnitCost = new System.Windows.Forms.Label();
            this.numUnitCost = new System.Windows.Forms.NumericUpDown();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorderLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== InventoryItemEditForm — EXPANDED ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(640, 780);              // 👈 wider + taller
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryItemEditForm";
            this.Text = "Item";
            this.Load += new System.EventHandler(this.InventoryItemEditForm_Load);

            // ==== Footer (dock first so it takes bottom) ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 80;
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Name = "pnlFooter";

            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Size = new System.Drawing.Size(130, 44);
            this.btnCancel.Location = new System.Drawing.Point(370, 20);
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

            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Size = new System.Drawing.Size(130, 44);
            this.btnSave.Location = new System.Drawing.Point(510, 20);
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

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(580, 34);
            this.lblHeader.Location = new System.Drawing.Point(30, 20);
            this.lblHeader.Text = "Add Item";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(580, 22);
            this.lblSubheader.Location = new System.Drawing.Point(30, 60);
            this.lblSubheader.Text = "Fill in the details below";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlBody.AutoScroll = false;
            this.pnlBody.Name = "pnlBody";

            // ---- Item Code ----
            this.lblCode.AutoSize = false;
            this.lblCode.Size = new System.Drawing.Size(580, 20);
            this.lblCode.Location = new System.Drawing.Point(30, 15);
            this.lblCode.Text = "Item Code *";
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCode.Name = "lblCode";

            this.txtCode.Size = new System.Drawing.Size(580, 28);
            this.txtCode.Location = new System.Drawing.Point(30, 37);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Name = "txtCode";

            // ---- Item Name ----
            this.lblName.AutoSize = false;
            this.lblName.Size = new System.Drawing.Size(580, 20);
            this.lblName.Location = new System.Drawing.Point(30, 83);
            this.lblName.Text = "Item Name *";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblName.Name = "lblName";

            this.txtName.Size = new System.Drawing.Size(580, 28);
            this.txtName.Location = new System.Drawing.Point(30, 105);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Name = "txtName";

            // ---- Category ----
            this.lblCategory.AutoSize = false;
            this.lblCategory.Size = new System.Drawing.Size(580, 20);
            this.lblCategory.Location = new System.Drawing.Point(30, 151);
            this.lblCategory.Text = "Category *";
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCategory.Name = "lblCategory";

            this.cmbCategory.Size = new System.Drawing.Size(580, 28);
            this.cmbCategory.Location = new System.Drawing.Point(30, 173);
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Name = "cmbCategory";

            // ---- Condition (left) ----
            this.lblCondition.AutoSize = false;
            this.lblCondition.Size = new System.Drawing.Size(280, 20);
            this.lblCondition.Location = new System.Drawing.Point(30, 219);
            this.lblCondition.Text = "Condition *";
            this.lblCondition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCondition.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCondition.Name = "lblCondition";

            this.cmbCondition.Size = new System.Drawing.Size(280, 28);
            this.cmbCondition.Location = new System.Drawing.Point(30, 241);
            this.cmbCondition.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCondition.Name = "cmbCondition";

            // ---- Availability (right) ----
            this.lblAvailability.AutoSize = false;
            this.lblAvailability.Size = new System.Drawing.Size(280, 20);
            this.lblAvailability.Location = new System.Drawing.Point(330, 219);
            this.lblAvailability.Text = "Availability *";
            this.lblAvailability.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvailability.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAvailability.Name = "lblAvailability";

            this.cmbAvailability.Size = new System.Drawing.Size(280, 28);
            this.cmbAvailability.Location = new System.Drawing.Point(330, 241);
            this.cmbAvailability.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAvailability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAvailability.Name = "cmbAvailability";

            // ---- Location ----
            this.lblLocation.AutoSize = false;
            this.lblLocation.Size = new System.Drawing.Size(580, 20);
            this.lblLocation.Location = new System.Drawing.Point(30, 287);
            this.lblLocation.Text = "Location";
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblLocation.Name = "lblLocation";

            this.cmbLocation.Size = new System.Drawing.Size(580, 28);
            this.cmbLocation.Location = new System.Drawing.Point(30, 309);
            this.cmbLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLocation.Name = "cmbLocation";

            // ---- Quantity (left) ----
            this.lblQuantity.AutoSize = false;
            this.lblQuantity.Size = new System.Drawing.Size(180, 20);
            this.lblQuantity.Location = new System.Drawing.Point(30, 355);
            this.lblQuantity.Text = "Quantity *";
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblQuantity.Name = "lblQuantity";

            this.numQuantity.Size = new System.Drawing.Size(180, 28);
            this.numQuantity.Location = new System.Drawing.Point(30, 377);
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numQuantity.Maximum = 1000000;
            this.numQuantity.Minimum = 0;
            this.numQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numQuantity.Name = "numQuantity";

            // ---- Reorder Level (middle) ----
            this.lblReorderLevel.AutoSize = false;
            this.lblReorderLevel.Size = new System.Drawing.Size(180, 20);
            this.lblReorderLevel.Location = new System.Drawing.Point(230, 355);
            this.lblReorderLevel.Text = "Reorder Level *";
            this.lblReorderLevel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReorderLevel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblReorderLevel.Name = "lblReorderLevel";

            this.numReorderLevel.Size = new System.Drawing.Size(180, 28);
            this.numReorderLevel.Location = new System.Drawing.Point(230, 377);
            this.numReorderLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numReorderLevel.Maximum = 1000000;
            this.numReorderLevel.Minimum = 0;
            this.numReorderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numReorderLevel.Name = "numReorderLevel";

            // ---- Unit Cost (right) ----
            this.lblUnitCost.AutoSize = false;
            this.lblUnitCost.Size = new System.Drawing.Size(180, 20);
            this.lblUnitCost.Location = new System.Drawing.Point(430, 355);
            this.lblUnitCost.Text = "Unit Cost (₱) *";
            this.lblUnitCost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnitCost.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblUnitCost.Name = "lblUnitCost";

            this.numUnitCost.Size = new System.Drawing.Size(180, 28);
            this.numUnitCost.Location = new System.Drawing.Point(430, 377);
            this.numUnitCost.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numUnitCost.Maximum = 10000000;
            this.numUnitCost.Minimum = 0;
            this.numUnitCost.DecimalPlaces = 2;
            this.numUnitCost.ThousandsSeparator = true;
            this.numUnitCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUnitCost.Name = "numUnitCost";

            // ---- Error Label ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(580, 24);
            this.lblError.Location = new System.Drawing.Point(30, 425);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCode);
            this.pnlBody.Controls.Add(this.txtCode);
            this.pnlBody.Controls.Add(this.lblName);
            this.pnlBody.Controls.Add(this.txtName);
            this.pnlBody.Controls.Add(this.lblCategory);
            this.pnlBody.Controls.Add(this.cmbCategory);
            this.pnlBody.Controls.Add(this.lblCondition);
            this.pnlBody.Controls.Add(this.cmbCondition);
            this.pnlBody.Controls.Add(this.lblAvailability);
            this.pnlBody.Controls.Add(this.cmbAvailability);
            this.pnlBody.Controls.Add(this.lblLocation);
            this.pnlBody.Controls.Add(this.cmbLocation);
            this.pnlBody.Controls.Add(this.lblQuantity);
            this.pnlBody.Controls.Add(this.numQuantity);
            this.pnlBody.Controls.Add(this.lblReorderLevel);
            this.pnlBody.Controls.Add(this.numReorderLevel);
            this.pnlBody.Controls.Add(this.lblUnitCost);
            this.pnlBody.Controls.Add(this.numUnitCost);
            this.pnlBody.Controls.Add(this.lblError);

            // ==== Form assembly ====
            // ORDER MATTERS: Body (fill) first, Footer (bottom), Header (top)
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorderLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).EndInit();
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

        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;

        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.ComboBox cmbCondition;

        private System.Windows.Forms.Label lblAvailability;
        private System.Windows.Forms.ComboBox cmbAvailability;

        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbLocation;

        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;

        private System.Windows.Forms.Label lblReorderLevel;
        private System.Windows.Forms.NumericUpDown numReorderLevel;

        private System.Windows.Forms.Label lblUnitCost;
        private System.Windows.Forms.NumericUpDown numUnitCost;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}