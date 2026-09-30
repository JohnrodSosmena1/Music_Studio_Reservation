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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubheader = new System.Windows.Forms.Label();

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

            this.lblStudio = new System.Windows.Forms.Label();
            this.cmbStudio = new System.Windows.Forms.ComboBox();

            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();

            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();

            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.numReorderLevel = new System.Windows.Forms.NumericUpDown();

            this.lblUnitCost = new System.Windows.Forms.Label();
            this.numUnitCost = new System.Windows.Forms.NumericUpDown();

            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorderLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).BeginInit();
            this.SuspendLayout();

            // ==== InventoryItemEditForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 540);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryItemEditForm";
            this.Text = "Inventory Item";
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.Load += new System.EventHandler(this.InventoryItemEditForm_Load);

            // ==== Title & Subtitle ====
            this.lblHeader.AutoSize = false;
            this.lblHeader.Location = new System.Drawing.Point(30, 18);
            this.lblHeader.Size = new System.Drawing.Size(480, 32);
            this.lblHeader.Text = "Add Inventory Item";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Location = new System.Drawing.Point(30, 50);
            this.lblSubheader.Size = new System.Drawing.Size(480, 20);
            this.lblSubheader.Text = "Fill in the details below. Item code will be generated automatically.";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            // ==== 1. Item Code (Read-Only) ====
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(30, 78);
            this.lblCode.Text = "Item Code (System Generated)";
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCode.Name = "lblCode";

            this.txtCode.Location = new System.Drawing.Point(30, 98);
            this.txtCode.Size = new System.Drawing.Size(480, 26);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCode.ReadOnly = true;
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Name = "txtCode";

            // ==== 2. Item Name ====
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 134);
            this.lblName.Text = "Item Name *";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblName.Name = "lblName";

            this.txtName.Location = new System.Drawing.Point(30, 154);
            this.txtName.Size = new System.Drawing.Size(480, 26);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Name = "txtName";

            // ==== 3. Category + Condition (2 Columns) ====
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(30, 190);
            this.lblCategory.Text = "Category *";
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblCategory.Name = "lblCategory";

            this.cmbCategory.Location = new System.Drawing.Point(30, 210);
            this.cmbCategory.Size = new System.Drawing.Size(230, 26);
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Name = "cmbCategory";

            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(280, 190);
            this.lblCondition.Text = "Condition *";
            this.lblCondition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCondition.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblCondition.Name = "lblCondition";

            this.cmbCondition.Location = new System.Drawing.Point(280, 210);
            this.cmbCondition.Size = new System.Drawing.Size(230, 26);
            this.cmbCondition.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition.Name = "cmbCondition";

            // ==== 4. Availability + Assigned Studio (2 Columns) ====
            this.lblAvailability.AutoSize = true;
            this.lblAvailability.Location = new System.Drawing.Point(30, 246);
            this.lblAvailability.Text = "Availability *";
            this.lblAvailability.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvailability.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblAvailability.Name = "lblAvailability";

            this.cmbAvailability.Location = new System.Drawing.Point(30, 266);
            this.cmbAvailability.Size = new System.Drawing.Size(230, 26);
            this.cmbAvailability.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbAvailability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvailability.Name = "cmbAvailability";

            this.lblStudio.AutoSize = true;
            this.lblStudio.Location = new System.Drawing.Point(280, 246);
            this.lblStudio.Text = "Assigned Studio";
            this.lblStudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStudio.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblStudio.Name = "lblStudio";

            this.cmbStudio.Location = new System.Drawing.Point(280, 266);
            this.cmbStudio.Size = new System.Drawing.Size(230, 26);
            this.cmbStudio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbStudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudio.Name = "cmbStudio";

            // ==== 5. Location / Storage Notes (Full Width) ====
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(30, 302);
            this.lblLocation.Text = "Location / Storage Notes";
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblLocation.Name = "lblLocation";

            this.cmbLocation.Location = new System.Drawing.Point(30, 322);
            this.cmbLocation.Size = new System.Drawing.Size(480, 26);
            this.cmbLocation.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbLocation.Name = "cmbLocation";

            // ==== 6. Quantity + Reorder Level + Unit Cost (3 Columns) ====
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(30, 358);
            this.lblQuantity.Text = "Quantity *";
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblQuantity.Name = "lblQuantity";

            this.numQuantity.Location = new System.Drawing.Point(30, 378);
            this.numQuantity.Size = new System.Drawing.Size(145, 26);
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numQuantity.Maximum = 1000000;
            this.numQuantity.Minimum = 0;
            this.numQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numQuantity.Name = "numQuantity";

            this.lblReorderLevel.AutoSize = true;
            this.lblReorderLevel.Location = new System.Drawing.Point(197, 358);
            this.lblReorderLevel.Text = "Reorder Level *";
            this.lblReorderLevel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReorderLevel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblReorderLevel.Name = "lblReorderLevel";

            this.numReorderLevel.Location = new System.Drawing.Point(197, 378);
            this.numReorderLevel.Size = new System.Drawing.Size(145, 26);
            this.numReorderLevel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numReorderLevel.Maximum = 1000000;
            this.numReorderLevel.Minimum = 0;
            this.numReorderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numReorderLevel.Name = "numReorderLevel";

            this.lblUnitCost.AutoSize = true;
            this.lblUnitCost.Location = new System.Drawing.Point(365, 358);
            this.lblUnitCost.Text = "Unit Cost (₱) *";
            this.lblUnitCost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnitCost.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblUnitCost.Name = "lblUnitCost";

            this.numUnitCost.Location = new System.Drawing.Point(365, 378);
            this.numUnitCost.Size = new System.Drawing.Size(145, 26);
            this.numUnitCost.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numUnitCost.Maximum = 10000000;
            this.numUnitCost.Minimum = 0;
            this.numUnitCost.DecimalPlaces = 2;
            this.numUnitCost.ThousandsSeparator = true;
            this.numUnitCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUnitCost.Name = "numUnitCost";

            // ==== 7. Error Label ====
            this.lblError.AutoSize = false;
            this.lblError.Location = new System.Drawing.Point(30, 422);
            this.lblError.Size = new System.Drawing.Size(480, 24);
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            // ==== 8. Bottom-Right Action Buttons ====
            this.btnCancel.Location = new System.Drawing.Point(280, 470);
            this.btnCancel.Size = new System.Drawing.Size(110, 38);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.Location = new System.Drawing.Point(400, 470);
            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.Text = "Save";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Add all controls to form
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblSubheader);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.cmbCondition);
            this.Controls.Add(this.lblAvailability);
            this.Controls.Add(this.cmbAvailability);
            this.Controls.Add(this.lblStudio);
            this.Controls.Add(this.cmbStudio);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblReorderLevel);
            this.Controls.Add(this.numReorderLevel);
            this.Controls.Add(this.lblUnitCost);
            this.Controls.Add(this.numUnitCost);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorderLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnitCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;
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
        private System.Windows.Forms.Label lblStudio;
        private System.Windows.Forms.ComboBox cmbStudio;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblReorderLevel;
        private System.Windows.Forms.NumericUpDown numReorderLevel;
        private System.Windows.Forms.Label lblUnitCost;
        private System.Windows.Forms.NumericUpDown numUnitCost;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}