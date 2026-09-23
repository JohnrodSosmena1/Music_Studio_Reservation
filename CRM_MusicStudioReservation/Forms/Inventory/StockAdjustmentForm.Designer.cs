namespace CRM.winforms.Forms.Inventory
{
    partial class StockAdjustmentForm
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

            this.lblCurrentQty = new System.Windows.Forms.Label();
            this.lblCurrentQtyValue = new System.Windows.Forms.Label();

            this.lblAdjustmentType = new System.Windows.Forms.Label();
            this.rbStockIn = new System.Windows.Forms.RadioButton();
            this.rbStockOut = new System.Windows.Forms.RadioButton();

            this.lblAmount = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();

            this.lblNewQty = new System.Windows.Forms.Label();
            this.lblNewQtyValue = new System.Windows.Forms.Label();

            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();

            this.lblError = new System.Windows.Forms.Label();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== StockAdjustmentForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(480, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockAdjustmentForm";
            this.Text = "Adjust Stock";
            this.Load += new System.EventHandler(this.StockAdjustmentForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 90;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Size = new System.Drawing.Size(420, 32);
            this.lblHeader.Location = new System.Drawing.Point(30, 18);
            this.lblHeader.Text = "Adjust Stock";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblHeader.Name = "lblHeader";

            this.lblSubheader.AutoSize = false;
            this.lblSubheader.Size = new System.Drawing.Size(420, 22);
            this.lblSubheader.Location = new System.Drawing.Point(30, 54);
            this.lblSubheader.Text = "Stock In / Stock Out";
            this.lblSubheader.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubheader.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubheader.Name = "lblSubheader";

            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Controls.Add(this.lblSubheader);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlBody.Name = "pnlBody";

            // ---- Current Quantity ----
            this.lblCurrentQty.AutoSize = false;
            this.lblCurrentQty.Size = new System.Drawing.Size(200, 24);
            this.lblCurrentQty.Location = new System.Drawing.Point(30, 20);
            this.lblCurrentQty.Text = "Current Quantity:";
            this.lblCurrentQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentQty.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCurrentQty.Name = "lblCurrentQty";

            this.lblCurrentQtyValue.AutoSize = false;
            this.lblCurrentQtyValue.Size = new System.Drawing.Size(180, 24);
            this.lblCurrentQtyValue.Location = new System.Drawing.Point(230, 20);
            this.lblCurrentQtyValue.Text = "0";
            this.lblCurrentQtyValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrentQtyValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblCurrentQtyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCurrentQtyValue.Name = "lblCurrentQtyValue";

            // ---- Adjustment Type ----
            this.lblAdjustmentType.AutoSize = false;
            this.lblAdjustmentType.Size = new System.Drawing.Size(400, 20);
            this.lblAdjustmentType.Location = new System.Drawing.Point(30, 60);
            this.lblAdjustmentType.Text = "Adjustment Type";
            this.lblAdjustmentType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdjustmentType.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAdjustmentType.Name = "lblAdjustmentType";

            this.rbStockIn.AutoSize = false;
            this.rbStockIn.Size = new System.Drawing.Size(180, 36);
            this.rbStockIn.Location = new System.Drawing.Point(30, 84);
            this.rbStockIn.Text = "  ↑  Stock In";
            this.rbStockIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbStockIn.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.rbStockIn.Checked = true;
            this.rbStockIn.Name = "rbStockIn";
            this.rbStockIn.CheckedChanged += new System.EventHandler(this.rbStock_CheckedChanged);

            this.rbStockOut.AutoSize = false;
            this.rbStockOut.Size = new System.Drawing.Size(180, 36);
            this.rbStockOut.Location = new System.Drawing.Point(220, 84);
            this.rbStockOut.Text = "  ↓  Stock Out";
            this.rbStockOut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbStockOut.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.rbStockOut.Name = "rbStockOut";
            this.rbStockOut.CheckedChanged += new System.EventHandler(this.rbStock_CheckedChanged);

            // ---- Amount ----
            this.lblAmount.AutoSize = false;
            this.lblAmount.Size = new System.Drawing.Size(400, 20);
            this.lblAmount.Location = new System.Drawing.Point(30, 140);
            this.lblAmount.Text = "Quantity";
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAmount.Name = "lblAmount";

            this.numAmount.Size = new System.Drawing.Size(400, 36);
            this.numAmount.Location = new System.Drawing.Point(30, 164);
            this.numAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.numAmount.Maximum = 1000000;
            this.numAmount.Minimum = 1;
            this.numAmount.Value = 1;
            this.numAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numAmount.Name = "numAmount";
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);

            // ---- New Quantity ----
            this.lblNewQty.AutoSize = false;
            this.lblNewQty.Size = new System.Drawing.Size(200, 24);
            this.lblNewQty.Location = new System.Drawing.Point(30, 214);
            this.lblNewQty.Text = "New Quantity:";
            this.lblNewQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNewQty.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNewQty.Name = "lblNewQty";

            this.lblNewQtyValue.AutoSize = false;
            this.lblNewQtyValue.Size = new System.Drawing.Size(180, 24);
            this.lblNewQtyValue.Location = new System.Drawing.Point(230, 214);
            this.lblNewQtyValue.Text = "0";
            this.lblNewQtyValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNewQtyValue.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.lblNewQtyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNewQtyValue.Name = "lblNewQtyValue";

            // ---- Notes ----
            this.lblNotes.AutoSize = false;
            this.lblNotes.Size = new System.Drawing.Size(400, 20);
            this.lblNotes.Location = new System.Drawing.Point(30, 254);
            this.lblNotes.Text = "Notes (optional)";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNotes.Name = "lblNotes";

            this.txtNotes.Size = new System.Drawing.Size(400, 60);
            this.txtNotes.Location = new System.Drawing.Point(30, 276);
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Name = "txtNotes";

            // ---- Error ----
            this.lblError.AutoSize = false;
            this.lblError.Size = new System.Drawing.Size(400, 30);
            this.lblError.Location = new System.Drawing.Point(30, 344);
            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblError.Name = "lblError";
            this.lblError.Visible = false;

            this.pnlBody.Controls.Add(this.lblCurrentQty);
            this.pnlBody.Controls.Add(this.lblCurrentQtyValue);
            this.pnlBody.Controls.Add(this.lblAdjustmentType);
            this.pnlBody.Controls.Add(this.rbStockIn);
            this.pnlBody.Controls.Add(this.rbStockOut);
            this.pnlBody.Controls.Add(this.lblAmount);
            this.pnlBody.Controls.Add(this.numAmount);
            this.pnlBody.Controls.Add(this.lblNewQty);
            this.pnlBody.Controls.Add(this.lblNewQtyValue);
            this.pnlBody.Controls.Add(this.lblNotes);
            this.pnlBody.Controls.Add(this.txtNotes);
            this.pnlBody.Controls.Add(this.lblError);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 70;
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Location = new System.Drawing.Point(210, 15);
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

            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnApply.Size = new System.Drawing.Size(120, 40);
            this.btnApply.Location = new System.Drawing.Point(340, 15);
            this.btnApply.Text = "Apply";
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.Name = "btnApply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnApply);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubheader;

        private System.Windows.Forms.Panel pnlBody;

        private System.Windows.Forms.Label lblCurrentQty;
        private System.Windows.Forms.Label lblCurrentQtyValue;

        private System.Windows.Forms.Label lblAdjustmentType;
        private System.Windows.Forms.RadioButton rbStockIn;
        private System.Windows.Forms.RadioButton rbStockOut;

        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.NumericUpDown numAmount;

        private System.Windows.Forms.Label lblNewQty;
        private System.Windows.Forms.Label lblNewQtyValue;

        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
    }
}