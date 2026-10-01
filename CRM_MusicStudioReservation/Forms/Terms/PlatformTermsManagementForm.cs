using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Terms
{
    public class PlatformTermsManagementForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly SuperAdminService _service;

        private List<PlatformTandCDto> _terms = new();

        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;

        private Panel pnlFilterBar = null!;
        private ComboBox cmbType = null!;
        private ComboBox cmbStatus = null!;
        private Button btnRefresh = null!;
        private Button btnNewDraft = null!;

        private Panel pnlGrid = null!;
        private DataGridView dgvTerms = null!;

        public PlatformTermsManagementForm(AuthService auth, ApiClient api)
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
            this.Name = "PlatformTermsManagementForm";
            this.Text = "Platform Terms & Conditions";
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
                Text = "Platform Terms & Conditions",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = true,
                Location = new Point(30, 16),
                UseMnemonic = false
            };

            lblSubtitle = new Label
            {
                Text = "Manage platform-level agreements, privacy policies, version increments, and tenant acknowledgment compliance",
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

            cmbType = new ComboBox
            {
                Location = new Point(30, 15),
                Size = new Size(200, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbType.Items.AddRange(new object[] { "All Types", "PlatformTerms", "PrivacyPolicy", "DataProcessingAgreement", "TenantAgreement" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += async (s, e) => await LoadDataAsync();

            cmbStatus = new ComboBox
            {
                Location = new Point(245, 15),
                Size = new Size(140, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbStatus.Items.AddRange(new object[] { "All Statuses", "Draft", "Published", "Archived" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += async (s, e) => await LoadDataAsync();

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(400, 14),
                Size = new Size(100, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(55, 65, 81),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnRefresh.Click += async (s, e) => await LoadDataAsync();

            btnNewDraft = new Button
            {
                Text = "+ New Agreement Draft",
                Location = new Point(515, 14),
                Size = new Size(180, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 92, 246),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnNewDraft.FlatAppearance.BorderSize = 0;
            btnNewDraft.Click += (s, e) => OpenCreateDraftModal();

            pnlFilterBar.Controls.Add(cmbType);
            pnlFilterBar.Controls.Add(cmbStatus);
            pnlFilterBar.Controls.Add(btnRefresh);
            pnlFilterBar.Controls.Add(btnNewDraft);

            // Grid Container
            pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 10, 30, 30),
                BackColor = Color.Transparent
            };

            dgvTerms = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(243, 244, 246),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 48 }
            };

            SetupColumns();
            dgvTerms.CellContentClick += DgvTerms_CellContentClick;

            pnlGrid.Controls.Add(dgvTerms);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlFilterBar);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private void SetupColumns()
        {
            dgvTerms.Columns.Clear();
            dgvTerms.Columns.Add("colCode", "Code");
            dgvTerms.Columns.Add("colType", "Type");
            dgvTerms.Columns.Add("colTitle", "Title");
            dgvTerms.Columns.Add("colVersion", "Version");
            dgvTerms.Columns.Add("colStatus", "Status");
            dgvTerms.Columns.Add("colPublished", "Published At");
            dgvTerms.Columns.Add("colAcks", "Tenant Acks");

            var actionsCol = new DataGridViewButtonColumn
            {
                Name = "colActions",
                HeaderText = "Actions",
                Text = "⋯ Actions",
                UseColumnTextForButtonValue = true,
                FillWeight = 60,
                FlatStyle = FlatStyle.Flat
            };
            actionsCol.DefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            actionsCol.DefaultCellStyle.ForeColor = Color.FromArgb(139, 92, 246);
            actionsCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTerms.Columns.Add(actionsCol);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var type = cmbType.SelectedItem?.ToString();
                if (type == "All Types") type = null;

                var status = cmbStatus.SelectedItem?.ToString();
                if (status == "All Statuses") status = null;

                _terms = await _service.GetTermsAsync(type, status);
                dgvTerms.Rows.Clear();
                foreach (var t in _terms)
                {
                    var rIdx = dgvTerms.Rows.Add(
                        t.TandCCode,
                        t.TandCType,
                        t.Title,
                        t.Version,
                        t.Status,
                        t.PublishedAt.HasValue ? t.PublishedAt.Value.ToString("MMM d, yyyy") : "—",
                        t.AcknowledgedCount
                    );
                    dgvTerms.Rows[rIdx].Tag = t;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load platform terms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTerms_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTerms.Columns[e.ColumnIndex].Name == "colActions")
            {
                if (dgvTerms.Rows[e.RowIndex].Tag is PlatformTandCDto item)
                {
                    ShowActionMenu(item, e.ColumnIndex, e.RowIndex);
                }
            }
        }

        private void ShowActionMenu(PlatformTandCDto item, int colIdx, int rowIdx)
        {
            var menu = new ContextMenuStrip();

            if (item.Status == "Draft")
            {
                menu.Items.Add("✏  Edit Draft", null, (s, e) => OpenEditDraftModal(item));
                menu.Items.Add("🚀  Publish to Platform", null, async (s, e) =>
                {
                    var confirm = MessageBox.Show($"Publish '{item.Title}' ({item.Version})?\n\nAny previous published version of {item.TandCType} will be archived.",
                        "Confirm Publish", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        if (await _service.PublishTermsAsync(item.PlatformTandCId))
                        {
                            MessageBox.Show("Agreement published successfully to all tenant organizations.", "Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadDataAsync();
                        }
                    }
                });
            }

            if (item.Status == "Published")
            {
                menu.Items.Add("📁  Archive", null, async (s, e) =>
                {
                    if (await _service.ArchiveTermsAsync(item.PlatformTandCId))
                    {
                        MessageBox.Show("Agreement archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                });
            }

            var cellRect = dgvTerms.GetCellDisplayRectangle(colIdx, rowIdx, false);
            menu.Show(dgvTerms, cellRect.Left, cellRect.Bottom);
        }

        private void OpenCreateDraftModal()
        {
            using var modal = new AddEditPlatformTermsModal(_service, null);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void OpenEditDraftModal(PlatformTandCDto item)
        {
            using var modal = new AddEditPlatformTermsModal(_service, item);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }
    }

    // Modal to create or edit platform terms draft
    public class AddEditPlatformTermsModal : Form
    {
        private readonly SuperAdminService _service;
        private readonly PlatformTandCDto? _existing;

        private ComboBox cmbType = null!;
        private TextBox txtTitle = null!;
        private TextBox txtContent = null!;
        private TextBox txtNotes = null!;
        private CheckBox chkReAccept = null!;
        private Button btnSave = null!;

        public AddEditPlatformTermsModal(SuperAdminService service, PlatformTandCDto? existing)
        {
            _service = service;
            _existing = existing;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = _existing == null ? "Create Platform Agreement Draft" : $"Edit Draft ({_existing.Version})";
            this.Size = new Size(600, 620);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = this.Text, Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };

            int y = 65;
            this.Controls.Add(new Label { Text = "Agreement Type", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            cmbType = new ComboBox
            {
                Location = new Point(25, y + 22),
                Size = new Size(530, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F),
                Enabled = _existing == null
            };
            cmbType.Items.AddRange(new object[] { "PlatformTerms", "PrivacyPolicy", "DataProcessingAgreement", "TenantAgreement" });
            cmbType.SelectedIndex = 0;
            this.Controls.Add(cmbType);
            y += 60;

            this.Controls.Add(new Label { Text = "Title *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtTitle = new TextBox { Location = new Point(25, y + 22), Size = new Size(530, 28), Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtTitle);
            y += 60;

            this.Controls.Add(new Label { Text = "Agreement Body / Terms Content *", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtContent = new TextBox { Location = new Point(25, y + 22), Size = new Size(530, 180), Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtContent);
            y += 215;

            this.Controls.Add(new Label { Text = "Change Notes / Summary", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(25, y), AutoSize = true });
            txtNotes = new TextBox { Location = new Point(25, y + 22), Size = new Size(530, 28), Font = new Font("Segoe UI", 9.5F) };
            this.Controls.Add(txtNotes);
            y += 60;

            chkReAccept = new CheckBox { Text = "Require re-acceptance by all tenant organization owners", Location = new Point(25, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.Controls.Add(chkReAccept);
            y += 35;

            btnSave = new Button { Text = "Save Draft", Location = new Point(435, y), Size = new Size(120, 36), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(139, 92, 246), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += async (s, e) => await SaveAsync();

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnSave);

            if (_existing != null)
            {
                txtTitle.Text = _existing.Title;
                txtContent.Text = _existing.Content;
                txtNotes.Text = _existing.ChangeNotes ?? "";
                chkReAccept.Checked = _existing.RequiresReAcceptance;
                var tIdx = cmbType.FindStringExact(_existing.TandCType);
                if (tIdx >= 0) cmbType.SelectedIndex = tIdx;
            }
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Please fill in Title and Content.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_existing == null)
            {
                var dto = new PlatformTandCCreateDto
                {
                    TandCType = cmbType.SelectedItem?.ToString() ?? "PlatformTerms",
                    Title = txtTitle.Text.Trim(),
                    Content = txtContent.Text.Trim(),
                    ChangeNotes = txtNotes.Text.Trim(),
                    RequiresReAcceptance = chkReAccept.Checked
                };

                if (await _service.CreateDraftTermsAsync(dto))
                {
                    MessageBox.Show("Draft created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                var dto = new PlatformTandCUpdateDto
                {
                    Title = txtTitle.Text.Trim(),
                    Content = txtContent.Text.Trim(),
                    ChangeNotes = txtNotes.Text.Trim(),
                    RequiresReAcceptance = chkReAccept.Checked
                };

                if (await _service.UpdateDraftTermsAsync(_existing.PlatformTandCId, dto))
                {
                    MessageBox.Show("Draft updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
