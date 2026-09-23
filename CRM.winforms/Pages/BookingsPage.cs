using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.winforms.Helpers;
using CRM.winforms.Services;
using CRM_MusicStudioReservation.api.DTOs;

namespace CRM.winforms.Pages
{
    public class BookingsPage : UserControl
    {
        private ListView _list;
        private Button _prevBtn;
        private Button _nextBtn;
        private Label _pageLabel;
        private NumericUpDown _pageEntry;
        private int _page = 1;
        private const int PageSize = 20;

        public BookingsPage()
        {
            Initialize();
            _ = LoadBookingsAsync();
        }

        private void Initialize()
        {
            BackColor = AppTheme.Background;
            var lbl = new Label
            {
                Text = "Bookings",
                Font = AppTheme.SectionTitle,
                ForeColor = AppTheme.TextPrimary,
                Location = new System.Drawing.Point(24, 24),
                AutoSize = true
            };
            Controls.Add(lbl);

            _list = new ListView
            {
                Location = new System.Drawing.Point(24, 70),
                Width = 900,
                Height = 400,
                View = View.Details,
                FullRowSelect = true
            };
            _list.Columns.AddRange(new[] {
                new ColumnHeader{ Text = "BookingCode", Width = 120 },
                new ColumnHeader{ Text = "CustomerId", Width = 100 },
                new ColumnHeader{ Text = "StudioId", Width = 80 },
                new ColumnHeader{ Text = "Start", Width = 180 },
                new ColumnHeader{ Text = "End", Width = 180 },
                new ColumnHeader{ Text = "Amount", Width = 80 },
                new ColumnHeader{ Text = "Status", Width = 120 }
            });

            Controls.Add(_list);

            _prevBtn = new Button { Text = "Previous", Location = new System.Drawing.Point(24, 480) };
            _prevBtn.Click += (s, e) => { if (_page > 1) { _page--; _pageEntry.Value = _page; _ = LoadBookingsAsync(); } };
            Controls.Add(_prevBtn);
            _nextBtn = new Button { Text = "Next", Location = new System.Drawing.Point(120, 480) };
            _nextBtn.Click += (s, e) => { _page++; _pageEntry.Value = _page; _ = LoadBookingsAsync(); };
            Controls.Add(_nextBtn);

            _pageLabel = new Label { Text = $"Page {_page}", Location = new System.Drawing.Point(220, 484), AutoSize = true };
            Controls.Add(_pageLabel);

            _pageEntry = new NumericUpDown { Location = new System.Drawing.Point(300, 480), Width = 80, Minimum = 1, Maximum = 1000, Value = _page };
            _pageEntry.ValueChanged += (s, e) => { _page = (int)_pageEntry.Value; _ = LoadBookingsAsync(); };
            Controls.Add(_pageEntry);
        }

        private async Task LoadBookingsAsync()
        {
            try
            {
                var session = ServiceLocator.Session;
                if (session == null) return;

                // For mock, companyId 1 is used. Replace with tenant selection later.
                var response = await ServiceLocator.ApiClient!.GetBookingsAsync(1, _page, PageSize);
                var bookings = response.Items.ToList();
                _list.Items.Clear();
                foreach (var b in bookings.OrderByDescending(x => x.CreatedAt))
                {
                    var it = new ListViewItem(new[] {
                        b.BookingCode ?? "",
                        (b.CustomerId?.ToString() ?? ""),
                        (b.StudioId?.ToString() ?? ""),
                        b.StartTime.ToString("g"),
                        b.EndTime.ToString("g"),
                        b.TotalAmount.ToString("C"),
                        b.BookingStatus.ToString()
                    });
                    _list.Items.Add(it);
                }

                _pageLabel.Text = $"Page {response.CurrentPage} of {response.TotalPages}";
                _prevBtn.Enabled = response.CurrentPage > 1;
                _nextBtn.Enabled = response.CurrentPage < response.TotalPages;
                _pageEntry.ValueChanged -= (s, e) => { _page = (int)_pageEntry.Value; _ = LoadBookingsAsync(); };
                _pageEntry.Value = _page;
                _pageEntry.ValueChanged += (s, e) => { _page = (int)_pageEntry.Value; _ = LoadBookingsAsync(); };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load bookings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
