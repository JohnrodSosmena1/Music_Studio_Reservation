using System;
using System.Windows.Forms;
using CRM.winforms.Helpers;
using CRM_MusicStudioReservation.api.DTOs;

namespace CRM.winforms.Pages
{
    public class StudioEditForm : Form
    {
        private RoundedTextBox _code;
        private RoundedTextBox _name;
        private NumericUpDown _capacity;
        private NumericUpDown _rate;
        private ComboBox _type;
        private CheckBox _isActive;
        private Button _save;

        private StudioResponseDto? _existing;

        public StudioEditForm()
        {
            Initialize();
        }

        public StudioEditForm(StudioResponseDto existing) : this()
        {
            _existing = existing;
            _code.Text = existing.StudioCode ?? "";
            _name.Text = existing.StudioName ?? "";
            _capacity.Value = existing.Capacity;
            _rate.Value = (decimal)existing.HourlyRate;
            _type.SelectedItem = existing.StudioType.ToString();
            _isActive.Checked = existing.IsActive;
        }

        private void Initialize()
        {
            Text = "Studio";
            Width = 480;
            Height = 380;
            StartPosition = FormStartPosition.CenterParent;

            _code = new RoundedTextBox { Location = new System.Drawing.Point(24, 24), Width = 400 }; _code.InnerTextBox.PlaceholderText = "Code";
            _name = new RoundedTextBox { Location = new System.Drawing.Point(24, 80), Width = 400 }; _name.InnerTextBox.PlaceholderText = "Name";
            _type = new ComboBox { Location = new System.Drawing.Point(24, 136), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            _type.Items.AddRange(Enum.GetNames(typeof(CRM_MusicStudioReservation.domain.entities.Enums.StudioType)));
            _capacity = new NumericUpDown { Location = new System.Drawing.Point(24, 180), Width = 120, Minimum = 1, Maximum = 100 };
            _rate = new NumericUpDown { Location = new System.Drawing.Point(160, 180), Width = 120, DecimalPlaces = 2, Minimum = 0, Maximum = 1000 };
            _isActive = new CheckBox { Location = new System.Drawing.Point(24, 220), Text = "Is Active", Checked = true };

            _save = new Button { Text = "Save", Location = new System.Drawing.Point(24, 260), Width = 160 };
            _save.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

            Controls.Add(_code);
            Controls.Add(_name);
            Controls.Add(_type);
            Controls.Add(_capacity);
            Controls.Add(_rate);
            Controls.Add(_isActive);
            Controls.Add(_save);
        }

        public StudioCreateDto GetCreateDto()
        {
            return new StudioCreateDto
            {
                StudioCode = _code.Text,
                StudioName = _name.Text,
                StudioType = Enum.TryParse<CRM_MusicStudioReservation.domain.entities.Enums.StudioType>(_type.SelectedItem?.ToString(), out var st) ? st : CRM_MusicStudioReservation.domain.entities.Enums.StudioType.Record,
                Capacity = (int)_capacity.Value,
                HourlyRate = (double)_rate.Value,
                Description = "",
                IsActive = _isActive.Checked
            };
        }

        public StudioUpdateDto GetUpdateDto()
        {
            return new StudioUpdateDto
            {
                StudioCode = _code.Text,
                StudioName = _name.Text,
                StudioType = Enum.TryParse<CRM_MusicStudioReservation.domain.entities.Enums.StudioType>(_type.SelectedItem?.ToString(), out var st) ? st : (CRM_MusicStudioReservation.domain.entities.Enums.StudioType?)null,
                Capacity = (int?)_capacity.Value,
                HourlyRate = (double?)_rate.Value,
                Description = "",
                IsActive = _isActive.Checked
            };
        }
    }
}
