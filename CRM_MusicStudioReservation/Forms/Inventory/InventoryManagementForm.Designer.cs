namespace CRM.winforms.Forms.Inventory
{
    partial class InventoryManagementForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.cmbCondition = new System.Windows.Forms.ComboBox();
            this.chkLowStockOnly = new System.Windows.Forms.CheckBox();

            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAvailability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlHeaderActions.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ==== InventoryManagementForm ====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.ClientSize = new System.Drawing.Size(1040, 720);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InventoryManagementForm";
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.InventoryManagementForm_Load);

            // ==== Header ====
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 0);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Size = new System.Drawing.Size(600, 40);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Text = "Inventory";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Name = "lblTitle";

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Size = new System.Drawing.Size(600, 25);
            this.lblSubtitle.Location = new System.Drawing.Point(30, 57);
            this.lblSubtitle.Text = "Equipment && instrument tracking";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Name = "lblSubtitle";

            // ==== Header Actions (right-aligned via FlowLayoutPanel) ====
            this.pnlHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeaderActions.Width = 300;
            this.pnlHeaderActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlHeaderActions.WrapContents = false;
            this.pnlHeaderActions.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderActions.Padding = new System.Windows.Forms.Padding(0, 30, 30, 0);
            this.pnlHeaderActions.Name = "pnlHeaderActions";

            this.btnAdd.Size = new System.Drawing.Size(150, 42);
            this.btnAdd.Text = "＋  Add Item";
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnRefresh.Size = new System.Drawing.Size(120, 42);
            this.btnRefresh.Text = "↻  Refresh";
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.pnlHeaderActions.Controls.Add(this.btnAdd);
            this.pnlHeaderActions.Controls.Add(this.btnRefresh);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderActions);

            // ==== Filters ====
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Height = 70;
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);

            this.lblSearch.AutoSize = false;
            this.lblSearch.Size = new System.Drawing.Size(60, 40);
            this.lblSearch.Location = new System.Drawing.Point(30, 15);
            this.lblSearch.Text = "Search";
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearch.Name = "lblSearch";

            this.txtSearch.Size = new System.Drawing.Size(240, 30);
            this.txtSearch.Location = new System.Drawing.Point(95, 20);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lblCategory.AutoSize = false;
            this.lblCategory.Size = new System.Drawing.Size(70, 40);
            this.lblCategory.Location = new System.Drawing.Point(355, 15);
            this.lblCategory.Text = "Category";
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCategory.Name = "lblCategory";

            this.cmbCategory.Size = new System.Drawing.Size(180, 30);
            this.cmbCategory.Location = new System.Drawing.Point(430, 20);
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);

            this.lblCondition.AutoSize = false;
            this.lblCondition.Size = new System.Drawing.Size(80, 40);
            this.lblCondition.Location = new System.Drawing.Point(625, 15);
            this.lblCondition.Text = "Condition";
            this.lblCondition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCondition.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCondition.Name = "lblCondition";

            this.cmbCondition.Size = new System.Drawing.Size(140, 30);
            this.cmbCondition.Location = new System.Drawing.Point(710, 20);
            this.cmbCondition.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCondition.Name = "cmbCondition";
            this.cmbCondition.SelectedIndexChanged += new System.EventHandler(this.cmbCondition_SelectedIndexChanged);

            this.chkLowStockOnly.AutoSize = false;
            this.chkLowStockOnly.Size = new System.Drawing.Size(150, 30);
            this.chkLowStockOnly.Location = new System.Drawing.Point(870, 20);
            this.chkLowStockOnly.Text = "Low Stock Only";
            this.chkLowStockOnly.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkLowStockOnly.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.chkLowStockOnly.Name = "chkLowStockOnly";
            this.chkLowStockOnly.CheckedChanged += new System.EventHandler(this.chkLowStockOnly_CheckedChanged);

            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Controls.Add(this.lblCategory);
            this.pnlFilters.Controls.Add(this.cmbCategory);
            this.pnlFilters.Controls.Add(this.lblCondition);
            this.pnlFilters.Controls.Add(this.cmbCondition);
            this.pnlFilters.Controls.Add(this.chkLowStockOnly);

            // ==== Body ====
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlBody.Name = "pnlBody";

            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.MultiSelect = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.RowTemplate.Height = 40;
            this.dgvItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.dgvItems.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvItems.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvItems.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.dgvItems.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.dgvItems.GridColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellDoubleClick);
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);

            this.colId.HeaderText = "ID";
            this.colId.FillWeight = 30;
            this.colId.Name = "colId";

            this.colCode.HeaderText = "Code";
            this.colCode.FillWeight = 70;
            this.colCode.Name = "colCode";

            this.colName.HeaderText = "Name";
            this.colName.FillWeight = 130;
            this.colName.Name = "colName";

            this.colCategory.HeaderText = "Category";
            this.colCategory.FillWeight = 90;
            this.colCategory.Name = "colCategory";

            this.colQty.HeaderText = "Qty";
            this.colQty.FillWeight = 50;
            this.colQty.Name = "colQty";

            this.colCondition.HeaderText = "Condition";
            this.colCondition.FillWeight = 70;
            this.colCondition.Name = "colCondition";

            this.colAvailability.HeaderText = "Availability";
            this.colAvailability.FillWeight = 80;
            this.colAvailability.Name = "colAvailability";

            this.colLocation.HeaderText = "Location";
            this.colLocation.FillWeight = 90;
            this.colLocation.Name = "colLocation";

            this.colUnitCost.HeaderText = "Unit Cost";
            this.colUnitCost.FillWeight = 80;
            this.colUnitCost.Name = "colUnitCost";

            this.colTotalValue.HeaderText = "Total Value";
            this.colTotalValue.FillWeight = 90;
            this.colTotalValue.Name = "colTotalValue";

            this.colActions.HeaderText = "Actions";
            this.colActions.FillWeight = 100;
            this.colActions.Name = "colActions";
            this.colActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colActions.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.colActions.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            this.colActions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            this.colActions.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colCode,
                this.colName,
                this.colCategory,
                this.colQty,
                this.colCondition,
                this.colAvailability,
                this.colLocation,
                this.colUnitCost,
                this.colTotalValue,
                this.colActions
            });

            this.pnlBody.Controls.Add(this.dgvItems);

            // ==== Footer ====
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 50;
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);

            this.lblCount.AutoSize = false;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.Width = 400;
            this.lblCount.Text = "0 items";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Name = "lblCount";

            this.lblTotalValue.AutoSize = false;
            this.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalValue.Width = 300;
            this.lblTotalValue.Text = "Total: ₱0.00";
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalValue.Name = "lblTotalValue";

            this.pnlFooter.Controls.Add(this.lblCount);
            this.pnlFooter.Controls.Add(this.lblTotalValue);

            // ==== Form assembly ====
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeaderActions.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlHeaderActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.ComboBox cmbCondition;
        private System.Windows.Forms.CheckBox chkLowStockOnly;

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvailability;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalValue;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotalValue;
    }
}