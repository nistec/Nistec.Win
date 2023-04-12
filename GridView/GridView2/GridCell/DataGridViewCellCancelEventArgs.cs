namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for <see cref="E:MControl.GridView.Grid.CellBeginEdit"></see> and <see cref="E:MControl.GridView.Grid.RowValidating"></see> events.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellCancelEventArgs : CancelEventArgs
    {
        private int columnIndex;
        private int rowIndex;

        internal GridCellCancelEventArgs(GridCell gridCell) : this(gridCell.ColumnIndex, gridCell.RowIndex)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellCancelEventArgs"></see> class. </summary>
        /// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
        /// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than -1.-or-rowIndex is less than -1.</exception>
        public GridCellCancelEventArgs(int columnIndex, int rowIndex)
        {
            if (columnIndex < -1)
            {
                throw new ArgumentOutOfRangeException("columnIndex");
            }
            if (rowIndex < -1)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
        }

        /// <summary>Gets the column index of the cell that the event occurs for.</summary>
        /// <returns>The column index of the <see cref="T:MControl.GridView.GridCell"></see> that the event occurs for.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets the row index of the cell that the event occurs for.</summary>
        /// <returns>The row index of the <see cref="T:MControl.GridView.GridCell"></see> that the event occurs for.</returns>
        /// <filterpriority>1</filterpriority>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

