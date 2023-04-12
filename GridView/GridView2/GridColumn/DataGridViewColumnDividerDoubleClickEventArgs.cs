namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.ColumnDividerDoubleClick"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    public class GridColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
    {
        private int columnIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnDividerDoubleClickEventArgs"></see> class. </summary>
        /// <param name="columnIndex">The index of the column next to the column divider that was double-clicked. </param>
        /// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs"></see> containing the inherited event data. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than -1.</exception>
        public GridColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
        {
            if (columnIndex < -1)
            {
                throw new ArgumentOutOfRangeException("columnIndex");
            }
            this.columnIndex = columnIndex;
        }

        /// <summary>The index of the column next to the column divider that was double-clicked.</summary>
        /// <returns>The index of the column next to the divider. </returns>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }
    }
}

