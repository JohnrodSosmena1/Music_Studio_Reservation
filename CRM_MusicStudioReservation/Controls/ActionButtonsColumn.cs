using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    /// <summary>
    /// A DataGridView column hosting ActionButtonsCell for multiple action buttons per row.
    /// </summary>
    public class ActionButtonsColumn : DataGridViewColumn
    {
        public ActionButtonsColumn() : base(new ActionButtonsCell())
        {
            this.HeaderText = "Actions";
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Resizable = DataGridViewTriState.False;
            this.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.FillWeight = 220;   // wider than other columns
        }

        public override object Clone() => new ActionButtonsColumn();
    }
}