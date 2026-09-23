using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;
using CRM.winforms.Services;

namespace CRM_MusicStudioReservation.Forms.Bookings
{
    public partial class BookStudioForm : Form
    {
        private readonly AuthService _auth;
        private readonly ApiClient _api;
        private readonly DashboardService _dashService;

        private int _currentStep = 1;

        // Wizard state
        private int _selectedStudioId = 0;
        private string _selectedStudioName = "";
        private decimal _selectedStudioRate = 0;
        private DateTime _selectedDate = DateTime.Today;
        private string _selectedTimeSlot = "";
        private string _notes = "";

        // 👇 CACHE — so re-render doesn't re-fetch from API
        private List<StudioDto>? _cachedStudios = null;

        public BookStudioForm(AuthService auth, ApiClient api)
        {
            InitializeComponent();
            _auth = auth;
            _api = api;
            _dashService = new DashboardService(api);

            btnCancel.Click += (s, e) => this.Close();
        }

        private void BookStudioForm_Load(object sender, EventArgs e)
        {
            ShowStep(1);
        }

        // ==================== STEP NAVIGATION ====================

        private void ShowStep(int step)
        {
            _currentStep = step;

            var stepTitles = new[]
            {
                "Step 1 of 4 — Select a Studio",
                "Step 2 of 4 — Date & Time",
                "Step 3 of 4 — Details",
                "Step 4 of 4 — Confirm"
            };
            lblStepIndicator.Text = stepTitles[step - 1];

            btnNext.Text = step == 4 ? "Confirm Booking" : "Next →";
            btnBack.Visible = step > 1;

            pnlContent.Controls.Clear();

            switch (step)
            {
                case 1: RenderStep1(); break;
                case 2: RenderStep2(); break;
                case 3: RenderStep3(); break;
                case 4: RenderStep4(); break;
            }
        }

        // ==================== STEP 1: Select Studio ====================

        private async void RenderStep1()
        {
            pnlContent.Controls.Clear();

            var lblIntro = new Label
            {
                Text = "Choose a studio for your session:",
                Font = new Font("Segoe UI", 11F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblIntro);

            // Load from API ONLY on first render — after that, use cache
            if (_cachedStudios == null)
            {
                var lblLoading = new Label
                {
                    Text = "Loading studios...",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = AppTheme.TextMuted,
                    Location = new Point(30, 60),
                    AutoSize = true
                };
                pnlContent.Controls.Add(lblLoading);

                try
                {
                    var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                    var fetched = await _dashService.GetStudiosAsync(companyId);

                    if (fetched.Count == 0)
                    {
                        // API returned empty — use fallback
                        _cachedStudios = new List<StudioDto>
                        {
                            new() { StudioId = 1, StudioCode = "STD001", StudioName = "Studio 1", HourlyRate = 300m, Capacity = 8 },
                            new() { StudioId = 2, StudioCode = "STD002", StudioName = "Studio 2", HourlyRate = 500m, Capacity = 10 },
                            new() { StudioId = 3, StudioCode = "STD003", StudioName = "Studio 3", HourlyRate = 300m, Capacity = 8 },
                            new() { StudioId = 4, StudioCode = "STD004", StudioName = "Studio 4", HourlyRate = 200m, Capacity = 6 },
                            new() { StudioId = 5, StudioCode = "STD005", StudioName = "Studio 5", HourlyRate = 400m, Capacity = 12 }
                        };
                    }
                    else
                    {
                        _cachedStudios = fetched;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to load studios:\n\n{ex.GetType().Name}\n{ex.Message}\n\nUsing sample data.",
                        "Load Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    _cachedStudios = new List<StudioDto>
                    {
                        new() { StudioId = 1, StudioCode = "STD001", StudioName = "Studio 1", HourlyRate = 300m, Capacity = 8 },
                        new() { StudioId = 2, StudioCode = "STD002", StudioName = "Studio 2", HourlyRate = 500m, Capacity = 10 },
                        new() { StudioId = 3, StudioCode = "STD003", StudioName = "Studio 3", HourlyRate = 300m, Capacity = 8 }
                    };
                }

                lblLoading.Visible = false;
            }

            var studios = _cachedStudios;

            int x = 30, y = 60, count = 0;
            foreach (var s in studios)
            {
                var card = BuildStudioCard(s.StudioId, s.StudioCode, s.StudioName,
                    $"Capacity {s.Capacity}", s.HourlyRate);
                card.Location = new Point(x, y);
                pnlContent.Controls.Add(card);

                x += 310;
                count++;
                if (count % 3 == 0)
                {
                    x = 30;
                    y += 200;
                }
            }
        }

        private Panel BuildStudioCard(int id, string code, string name, string type, decimal rate)
        {
            var card = new Panel
            {
                Size = new Size(280, 180),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = id
            };
            RoundedCorners.Apply(card, 10);

            var isSelected = _selectedStudioId == id;

            card.Paint += (s, e) =>
            {
                using var pen = new Pen(isSelected ? AppTheme.Primary : AppTheme.Border, isSelected ? 3 : 1);
                using var path = RoundedCorners.CreateRoundedPath(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 10);
                e.Graphics.DrawPath(pen, path);
            };

            var pnlIcon = new Panel
            {
                Size = new Size(60, 60),
                Location = new Point(20, 20),
                BackColor = AppTheme.Primary
            };
            RoundedCorners.Apply(pnlIcon, 30);

            var lblIcon = new Label
            {
                Text = "🎸",
                Font = new Font("Segoe UI", 24F),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            pnlIcon.Controls.Add(lblIcon);

            var lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(90, 25),
                AutoSize = true
            };

            var lblType = new Label
            {
                Text = type,
                Font = new Font("Segoe UI", 9F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(90, 52),
                AutoSize = true
            };

            var lblRate = new Label
            {
                Text = $"₱{rate:N0} / Hour",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = AppTheme.Primary,
                Location = new Point(20, 100),
                AutoSize = true
            };

            var lblCode = new Label
            {
                Text = code,
                Font = new Font("Segoe UI", 8F),
                ForeColor = AppTheme.TextMuted,
                Location = new Point(20, 140),
                AutoSize = true
            };

            card.Controls.Add(pnlIcon);
            card.Controls.Add(lblName);
            card.Controls.Add(lblType);
            card.Controls.Add(lblRate);
            card.Controls.Add(lblCode);

            // ✅ Wire click on card AND all child controls (fixes "clicking text does nothing")
            EventHandler selectHandler = (s, e) =>
            {
                _selectedStudioId = id;
                _selectedStudioName = name;
                _selectedStudioRate = rate;
                ShowStep(1);  // re-render → uses cache → instant highlight
            };

            card.Click += selectHandler;
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += selectHandler;
                foreach (Control sub in ctrl.Controls)
                {
                    sub.Click += selectHandler;
                }
            }

            return card;
        }

        // ==================== STEP 2: Date & Time ====================

        private void RenderStep2()
        {
            var lblIntro = new Label
            {
                Text = "When would you like to book?",
                Font = new Font("Segoe UI", 11F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblIntro);

            var lblDate = new Label
            {
                Text = "Date:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(30, 70),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblDate);

            var dtp = new DateTimePicker
            {
                Location = new Point(30, 95),
                Size = new Size(300, 32),
                Font = new Font("Segoe UI", 11F),
                Format = DateTimePickerFormat.Long,
                Value = _selectedDate,
                MinDate = DateTime.Today
            };
            dtp.ValueChanged += (s, e) => _selectedDate = dtp.Value;
            pnlContent.Controls.Add(dtp);

            var lblTime = new Label
            {
                Text = "Available Time Slots:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(30, 150),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblTime);

            var slots = new[]
            {
                "09:00 AM - 10:00 AM", "10:00 AM - 11:00 AM", "11:00 AM - 12:00 PM",
                "12:00 PM - 01:00 PM", "01:00 PM - 02:00 PM", "02:00 PM - 03:00 PM",
                "03:00 PM - 04:00 PM", "04:00 PM - 05:00 PM", "05:00 PM - 06:00 PM",
                "06:00 PM - 07:00 PM", "07:00 PM - 08:00 PM", "08:00 PM - 09:00 PM"
            };

            int x = 30, y = 180, count = 0;
            foreach (var slot in slots)
            {
                var slotBtn = BuildSlotButton(slot);
                slotBtn.Location = new Point(x, y);
                pnlContent.Controls.Add(slotBtn);

                x += 160;
                count++;
                if (count % 6 == 0)
                {
                    x = 30;
                    y += 55;
                }
            }
        }

        private Button BuildSlotButton(string slot)
        {
            var isSelected = _selectedTimeSlot == slot;

            var btn = new Button
            {
                Size = new Size(150, 45),
                Text = slot,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, isSelected ? FontStyle.Bold : FontStyle.Regular),
                BackColor = isSelected ? AppTheme.Primary : Color.White,
                ForeColor = isSelected ? Color.White : AppTheme.TextPrimary,
                Cursor = Cursors.Hand,
                Tag = slot
            };
            btn.FlatAppearance.BorderColor = isSelected ? AppTheme.Primary : AppTheme.Border;
            btn.FlatAppearance.BorderSize = 1;

            btn.Click += (s, e) =>
            {
                _selectedTimeSlot = slot;
                ShowStep(2);
            };

            return btn;
        }

        // ==================== STEP 3: Details ====================

        private void RenderStep3()
        {
            var lblIntro = new Label
            {
                Text = "Add any notes or special requests:",
                Font = new Font("Segoe UI", 11F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblIntro);

            var lblNotes = new Label
            {
                Text = "Notes (optional):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(30, 70),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblNotes);

            var txt = new TextBox
            {
                Location = new Point(30, 95),
                Size = new Size(900, 200),
                Multiline = true,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.FixedSingle,
                Text = _notes,
                ScrollBars = ScrollBars.Vertical
            };
            txt.TextChanged += (s, e) => _notes = txt.Text;
            pnlContent.Controls.Add(txt);

            var lblHint = new Label
            {
                Text = "Examples: bring extra microphones, need recording engineer, etc.",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = AppTheme.TextMuted,
                Location = new Point(30, 305),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblHint);
        }

        // ==================== STEP 4: Confirm ====================

        private void RenderStep4()
        {
            var lblIntro = new Label
            {
                Text = "Review your booking:",
                Font = new Font("Segoe UI", 11F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblIntro);

            var summary = new Panel
            {
                Location = new Point(30, 60),
                Size = new Size(900, 300),
                BackColor = Color.White
            };
            RoundedCorners.Apply(summary, 10);

            int y = 25;
            AddSummaryRow(summary, "Studio:", _selectedStudioName, ref y);
            AddSummaryRow(summary, "Date:", _selectedDate.ToString("dddd, MMMM d, yyyy"), ref y);
            AddSummaryRow(summary, "Time:", _selectedTimeSlot, ref y);
            AddSummaryRow(summary, "Hourly Rate:", $"₱{_selectedStudioRate:N2}", ref y);
            AddSummaryRow(summary, "Notes:", string.IsNullOrEmpty(_notes) ? "(none)" : _notes, ref y);

            var lblTotal = new Label
            {
                Text = $"Total (1 hour):  ₱{_selectedStudioRate:N2}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = AppTheme.Primary,
                Location = new Point(30, 260),
                AutoSize = true
            };
            summary.Controls.Add(lblTotal);

            pnlContent.Controls.Add(summary);
        }

        private void AddSummaryRow(Panel parent, string label, string value, ref int y)
        {
            var lbl1 = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 10F),
                ForeColor = AppTheme.TextSecondary,
                Location = new Point(30, y),
                Size = new Size(200, 25)
            };

            var lbl2 = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = AppTheme.TextPrimary,
                Location = new Point(240, y),
                Size = new Size(600, 25)
            };

            parent.Controls.Add(lbl1);
            parent.Controls.Add(lbl2);
            y += 40;
        }

        // ==================== BUTTONS ====================

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_currentStep > 1)
                ShowStep(_currentStep - 1);
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentStep == 1 && _selectedStudioId == 0)
            {
                MessageBox.Show("Please select a studio.", "Select Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_currentStep == 2 && string.IsNullOrEmpty(_selectedTimeSlot))
            {
                MessageBox.Show("Please select a time slot.", "Select Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentStep < 4)
            {
                ShowStep(_currentStep + 1);
                return;
            }

            await SubmitBookingAsync();
        }

        // ==================== SUBMIT BOOKING ====================

        private async Task SubmitBookingAsync()
        {
            btnNext.Enabled = false;
            btnNext.Text = "Submitting...";

            try
            {
                var companyId = _auth.CurrentUser?.CompanyId ?? 1;
                var customerId = 1;   // TODO: map AppUser.UserId → Customer.CustomerId

                var (start, end) = ParseTimeSlot(_selectedTimeSlot);
                if (start == null || end == null)
                {
                    MessageBox.Show("Invalid time slot.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var request = new BookingCreateRequest
                {
                    CustomerId = customerId,
                    StudioId = _selectedStudioId,
                    StartTime = DateTime.SpecifyKind(_selectedDate.Date + start.Value, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(_selectedDate.Date + end.Value, DateTimeKind.Utc),
                    Notes = string.IsNullOrWhiteSpace(_notes) ? null : _notes
                };

                var response = await _dashService.CreateBookingAsync(companyId, request);

                if (response == null)
                {
                    MessageBox.Show(
                        "Booking failed. The server rejected the request.\n\n" +
                        "Possible reasons:\n" +
                        "• Customer ID does not exist in the tenant DB\n" +
                        "• Studio ID is invalid\n" +
                        "• Time slot is in the past\n" +
                        "• API is not reachable",
                        "Booking Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    $"Booking confirmed!\n\n" +
                    $"Booking Code: {response.BookingCode}\n" +
                    $"Studio: {_selectedStudioName}\n" +
                    $"Date: {response.StartTime:MMM d, yyyy}\n" +
                    $"Time: {response.StartTime:hh:mm tt} - {response.EndTime:hh:mm tt}\n" +
                    $"Total: ₱{response.TotalAmount:N2}",
                    "Booking Confirmed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Booking failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNext.Enabled = true;
                btnNext.Text = "Confirm Booking";
            }
        }

        private (TimeSpan? start, TimeSpan? end) ParseTimeSlot(string slot)
        {
            try
            {
                var parts = slot.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) return (null, null);

                var start = DateTime.Parse(parts[0].Trim()).TimeOfDay;
                var end = DateTime.Parse(parts[1].Trim()).TimeOfDay;
                return (start, end);
            }
            catch
            {
                return (null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}