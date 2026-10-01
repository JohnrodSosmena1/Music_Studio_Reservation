using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Organizations
{
    public class OrganizationManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly SuperAdminService _service;

        private List<OrganizationListItemDto> _organizations = new();

        // UI Controls
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;

        private Panel pnlFilterBar = null!;
        private TextBox txtSearch = null!;
        private ComboBox cmbStatus = null!;
        private Button btnRefresh = null!;
        private Button btnAddOrg = null!;

        private Panel pnlGrid = null!;
        private DataGridView dgvOrgs = null!;

        public OrganizationManagementForm(AuthService auth, ApiClient api)
        {
            _auth = auth;
            _api = api;
            _service = new SuperAdminService(api);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(249, 250, 251);
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "OrganizationManagementForm";
            this.Text = "Studio Organizations";
            this.Load += async (s, e) => await LoadDataAsync();

            // Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(30, 20, 30, 10),
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "Studio Organizations",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = true,
                Location = new Point(30, 16),
                UseMnemonic = false
            };

            lblSubtitle = new Label
            {
                Text = "Manage platform multi-tenant organizations, subdomains, lifecycles, and subscriptions",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(30, 60),
                UseMnemonic = false
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            // Filter Bar
            pnlFilterBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.Transparent
            };

            txtSearch = new TextBox
            {
                Location = new Point(30, 15),
                Size = new Size(260, 32),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "🔍 Search organizations, codes, owners..."
            };
            txtSearch.TextChanged += async (s, e) => await FilterAsync();

            cmbStatus = new ComboBox
            {
                Location = new Point(305, 15),
                Size = new Size(160, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbStatus.Items.AddRange(new object[] { "All Statuses", "Active", "Inactive", "Suspended", "Trial", "Archived" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += async (s, e) => await FilterAsync();

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(480, 14),
                Size = new Size(100, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(55, 65, 81),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnRefresh.Click += async (s, e) => await LoadDataAsync();

            btnAddOrg = new Button
            {
                Text = "+ Add Organization",
                Location = new Point(595, 14),
                Size = new Size(160, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 92, 246),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddOrg.FlatAppearance.BorderSize = 0;
            btnAddOrg.Click += (s, e) => OpenAddModal();

            pnlFilterBar.Controls.Add(txtSearch);
            pnlFilterBar.Controls.Add(cmbStatus);
            pnlFilterBar.Controls.Add(btnRefresh);
            pnlFilterBar.Controls.Add(btnAddOrg);

            // Grid Container
            pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 10, 30, 30),
                BackColor = Color.Transparent
            };

            dgvOrgs = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(243, 244, 246),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowTemplate = { Height = 48 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            SetupColumns();
            dgvOrgs.CellContentClick += DgvOrgs_CellContentClick;

            pnlGrid.Controls.Add(dgvOrgs);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlFilterBar);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private void SetupColumns()
        {
            dgvOrgs.Columns.Clear();

            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 30 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Tenant Code", FillWeight = 70 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Organization Name", FillWeight = 130 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSubdomain", HeaderText = "Subdomain", FillWeight = 90 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOwner", HeaderText = "Owner / Admin", FillWeight = 100 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPlan", HeaderText = "Plan", FillWeight = 60 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", FillWeight = 65 });
            dgvOrgs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colExpires", HeaderText = "Sub. Expiry", FillWeight = 80 });

            var actionsCol = new DataGridViewButtonColumn
            {
                Name = "colActions",
                HeaderText = "Actions",
                Text = "⋯ Actions",
                UseColumnTextForButtonValue = true,
                FillWeight = 70,
                FlatStyle = FlatStyle.Flat
            };
            actionsCol.DefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            actionsCol.DefaultCellStyle.ForeColor = Color.FromArgb(139, 92, 246);
            actionsCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvOrgs.Columns.Add(actionsCol);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var res = await _service.GetOrganizationsAsync(pageSize: 100);
                _organizations = res?.Items ?? new List<OrganizationListItemDto>();
                RenderGrid(_organizations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load organizations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task FilterAsync()
        {
            var search = txtSearch.Text.Trim();
            var status = cmbStatus.SelectedItem?.ToString();
            if (status == "All Statuses") status = null;

            var res = await _service.GetOrganizationsAsync(search: search, status: status, pageSize: 100);
            _organizations = res?.Items ?? new List<OrganizationListItemDto>();
            RenderGrid(_organizations);
        }

        private void RenderGrid(List<OrganizationListItemDto> list)
        {
            dgvOrgs.Rows.Clear();
            foreach (var org in list)
            {
                var rowIdx = dgvOrgs.Rows.Add(
                    org.CompanyId,
                    org.CompanyCode,
                    org.CompanyName,
                    string.IsNullOrWhiteSpace(org.Subdomain) ? "—" : $"{org.Subdomain}.crmapp.com",
                    org.OwnerName ?? org.OwnerEmail ?? "—",
                    org.PlanName,
                    org.Status,
                    org.SubscriptionEnd.HasValue ? org.SubscriptionEnd.Value.ToString("MMM d, yyyy") : "—"
                );
                dgvOrgs.Rows[rowIdx].Tag = org;
            }
        }

        private void DgvOrgs_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvOrgs.Columns[e.ColumnIndex].Name == "colActions")
            {
                var org = dgvOrgs.Rows[e.RowIndex].Tag as OrganizationListItemDto;
                if (org == null) return;

                ShowActionMenu(org, e.ColumnIndex, e.RowIndex);
            }
        }

        private void ShowActionMenu(OrganizationListItemDto org, int colIndex, int rowIndex)
        {
            var menu = new ContextMenuStrip();

            menu.Items.Add("👁  View Details", null, (s, e) => OpenDetailModal(org));
            menu.Items.Add("✏  Edit Details", null, (s, e) => OpenEditModal(org));
            menu.Items.Add(new ToolStripSeparator());

            if (org.Status != "Active")
            {
                menu.Items.Add("▶  Activate", null, async (s, e) =>
                {
                    if (await _service.ActivateOrganizationAsync(org.CompanyId))
                    {
                        MessageBox.Show($"Organization '{org.CompanyName}' activated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                });
            }

            if (org.Status == "Active")
            {
                menu.Items.Add("⏸  Deactivate", null, async (s, e) =>
                {
                    if (await _service.DeactivateOrganizationAsync(org.CompanyId))
                    {
                        MessageBox.Show($"Organization '{org.CompanyName}' deactivated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                });
            }

            if (org.Status != "Suspended")
            {
                menu.Items.Add("⛔  Suspend (Block Logins)", null, async (s, e) =>
                {
                    var confirm = MessageBox.Show($"Suspend organization '{org.CompanyName}'?\n\nTenant users will be blocked from logging in until reactivated.",
                        "Confirm Suspend", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        if (await _service.SuspendOrganizationAsync(org.CompanyId))
                        {
                            MessageBox.Show($"Organization '{org.CompanyName}' suspended.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadDataAsync();
                        }
                    }
                });
            }

            if (org.Status != "Archived")
            {
                menu.Items.Add("📁  Archive (Read-Only)", null, async (s, e) =>
                {
                    if (await _service.ArchiveOrganizationAsync(org.CompanyId))
                    {
                        MessageBox.Show($"Organization '{org.CompanyName}' archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                });
            }

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("🗑  Soft Delete", null, async (s, e) =>
            {
                var confirm = MessageBox.Show($"Soft delete '{org.CompanyName}'?\n\nData will be retained but hidden from active platform operations.",
                    "Confirm Soft Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    if (await _service.SoftDeleteOrganizationAsync(org.CompanyId))
                    {
                        MessageBox.Show("Organization soft deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                }
            });

            menu.Items.Add("❌  Hard Delete (Permanent)", null, async (s, e) =>
            {
                using var confirmDialog = new HardDeleteConfirmModal(org.CompanyCode, org.CompanyName);
                if (confirmDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (await _service.HardDeleteOrganizationAsync(org.CompanyId, confirmDialog.TypedPhrase))
                    {
                        MessageBox.Show($"Organization '{org.CompanyName}' permanently deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                }
            });

            var cellRect = dgvOrgs.GetCellDisplayRectangle(colIndex, rowIndex, false);
            menu.Show(dgvOrgs, cellRect.Left, cellRect.Bottom);
        }

        private void OpenAddModal()
        {
            using var modal = new AddOrganizationModal(_service);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void OpenEditModal(OrganizationListItemDto org)
        {
            using var modal = new EditOrganizationModal(_service, org.CompanyId);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void OpenDetailModal(OrganizationListItemDto org)
        {
            using var modal = new OrganizationDetailModal(_service, org.CompanyId);
            modal.ShowDialog(this);
        }
    }

    // Modal to type "DELETE TEN-XXXXX" for hard delete
    public class HardDeleteConfirmModal : Form
    {
        public string TypedPhrase => txtPhrase.Text.Trim();
        private TextBox txtPhrase = null!;

        public HardDeleteConfirmModal(string code, string name)
        {
            this.Text = "Permanent Hard Delete Confirmation";
            this.Size = new Size(500, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lbl = new Label
            {
                Text = $"WARNING: You are about to permanently delete organization '{name}' ({code}).\n\n" +
                       $"All associated database records will be erased.\n\n" +
                       $"To confirm, please type 'DELETE {code}' below:",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(239, 68, 68),
                Location = new Point(25, 20),
                Size = new Size(430, 95)
            };

            txtPhrase = new TextBox
            {
                Location = new Point(25, 125),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold)
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(230, 175),
                Size = new Size(100, 35),
                DialogResult = DialogResult.Cancel
            };

            var btnConfirm = new Button
            {
                Text = "Hard Delete",
                Location = new Point(345, 175),
                Size = new Size(110, 35),
                BackColor = Color.FromArgb(239, 68, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += (s, e) =>
            {
                if (TypedPhrase == $"DELETE {code}")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Incorrect confirmation phrase. You must type 'DELETE {code}'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            this.Controls.Add(lbl);
            this.Controls.Add(txtPhrase);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnConfirm);
        }
    }
}
