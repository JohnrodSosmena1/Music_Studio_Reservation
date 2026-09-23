using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM_MusicStudioReservation.api.DTOs;

namespace CRM.winforms.Pages
{
    public class StudiosPage : UserControl
    {
        private ListView _list;
        private Button _createBtn;
        private Button _deleteBtn;
        private Button _refreshBtn;
        private TextBox _searchBox;
        private Button _searchBtn;
        private Button _prevBtn;
        private Button _nextBtn;
        private Label _pageLabel;
        private NumericUpDown _pageEntry;
        private int _page = 1;
        private const int PageSize = 20;

        public StudiosPage()
        {
            Initialize();
            _ = LoadStudiosAsync();
        }

        private void Initialize()
        {
            BackColor = AppTheme.Background;
            var lbl = new Label
            {
                Text = "Studios",
                Font = AppTheme.SectionTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new System.Drawing.Point(24, 24),
                AutoSize = true
            };
            Controls.Add(lbl);

            _createBtn = new Button { Text = "New Studio", Location = new System.Drawing.Point(24, 64) };
            _createBtn.Click += (s, e) => ShowCreateDialog();
            Controls.Add(_createBtn);

            _searchBox = new TextBox { Location = new System.Drawing.Point(140, 64), Width = 220, PlaceholderText = "Search by name or code" };
            Controls.Add(_searchBox);
            _searchBtn = new Button { Text = "Search", Location = new System.Drawing.Point(370, 64) };
            _searchBtn.Click += (s, e) => { _page = 1; _ = LoadStudiosAsync(); };
            Controls.Add(_searchBtn);

            _list = new ListView
            {
                Location = new System.Drawing.Point(24, 110),
                Width = 900,
                Height = 400,
                View = View.Details,
                FullRowSelect = true
            };
            _list.Columns.AddRange(new[] {
                new ColumnHeader{ Text = "Id", Width = 60 },
                new ColumnHeader{ Text = "Code", Width = 120 },
                new ColumnHeader{ Text = "Name", Width = 200 },
                new ColumnHeader{ Text = "Type", Width = 120 },
                new ColumnHeader{ Text = "Rate", Width = 80 },
                new ColumnHeader{ Text = "Capacity", Width = 80 }
            });

            _list.DoubleClick += (s, e) => EditSelected();

            Controls.Add(_list);

            _prevBtn = new Button { Text = "Previous", Location = new System.Drawing.Point(24, 520) };
            _prevBtn.Click += (s, e) => { if (_page > 1) { _page--; _pageEntry.Value = _page; _ = LoadStudiosAsync(); } };
            Controls.Add(_prevBtn);
            _nextBtn = new Button { Text = "Next", Location = new System.Drawing.Point(120, 520) };
            _nextBtn.Click += (s, e) => { _page++; _pageEntry.Value = _page; _ = LoadStudiosAsync(); };
            Controls.Add(_nextBtn);

            _pageLabel = new Label { Text = $"Page {_page}", Location = new System.Drawing.Point(220, 524), AutoSize = true };
            Controls.Add(_pageLabel);

            _pageEntry = new NumericUpDown { Location = new System.Drawing.Point(300, 520), Width = 80, Minimum = 1, Maximum = 1000, Value = _page };
            _pageEntry.ValueChanged += (s, e) => { _page = (int)_pageEntry.Value; _ = LoadStudiosAsync(); };
            Controls.Add(_pageEntry);
        }

        private async Task LoadStudiosAsync()
        {
            try
            {
                var response = await ServiceLocator.ApiClient!.GetStudiosAsync(1, _page, PageSize, _searchBox.Text);
                var items = response.Items.ToList();
                _list.Items.Clear();
                foreach (var s in items.OrderBy(x => x.StudioId))
                {
                    var it = new ListViewItem(new[] {
                        s.StudioId.ToString(),
                        s.StudioCode ?? "",
                        s.StudioName ?? "",
                        s.StudioType.ToString(),
                        s.HourlyRate.ToString("C"),
                        s.Capacity.ToString()
                    });
                    _list.Items.Add(it);
                }

                // Paging UX: update controls using metadata from server
                _pageLabel.Text = $"Page {response.CurrentPage} of {response.TotalPages}";
                _prevBtn.Enabled = response.CurrentPage > 1;
                _nextBtn.Enabled = response.CurrentPage < response.TotalPages;
                _pageEntry.ValueChanged -= (s, e) => { _page = (int)_pageEntry.Value; _ = LoadStudiosAsync(); };
                _pageEntry.Value = _page;
                _pageEntry.ValueChanged += (s, e) => { _page = (int)_pageEntry.Value; _ = LoadStudiosAsync(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load studios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditSelected()
        {
            if (_list.SelectedItems.Count == 0) return;
            var id = int.Parse(_list.SelectedItems[0].Text);
            ShowEditDialog(id);
        }

        private async void ShowCreateDialog()
        {
            using var dlg = new StudioEditForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var dto = dlg.GetCreateDto();
                var created = await ServiceLocator.ApiClient!.CreateStudioAsync(1, dto);
                if (created != null) await LoadStudiosAsync();
                else MessageBox.Show("Create failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ShowEditDialog(int id)
        {
            var studio = await ServiceLocator.ApiClient!.GetStudioAsync(1, id);
            if (studio == null) { MessageBox.Show("Studio not found."); return; }
            using var dlg = new StudioEditForm(studio);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var upd = dlg.GetUpdateDto();
                var ok = await ServiceLocator.ApiClient.UpdateStudioAsync(1, id, upd);
                if (ok) await LoadStudiosAsync();
                else MessageBox.Show("Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
