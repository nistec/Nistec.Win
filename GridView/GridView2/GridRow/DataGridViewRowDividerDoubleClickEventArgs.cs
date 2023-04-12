namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowDividerDoubleClick"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    public class GridRowDividerDoubleClickEventArgs : HandledMouseEventArgs
    {
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowDividerDoubleClickEventArgs"></see> class. </summary>
        /// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs"></see> containing the inherited event data.</param>
        /// <param name="rowIndex">The index of the row above the row divider that was double-clicked.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">rowIndex is less than -1.</exception>
        public GridRowDividerDoubleClickEventArgs(int rowIndex, HandledMouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
        {
            if (rowIndex < -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            this.rowIndex = rowIndex;
        }

        /// <summary>The index of the row above the row divider that was double-clicked.</summary>
        /// <returns>The index of the row above the divider.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

