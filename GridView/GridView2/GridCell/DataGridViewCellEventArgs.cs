namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for <see cref="T:MControl.GridView.Grid"></see> events related to cell and row operations.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellEventArgs : EventArgs
    {
        private int columnIndex;
        private int rowIndex;

        internal GridCellEventArgs(GridCell gridCell) : this(gridCell.ColumnIndex, gridCell.RowIndex)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellEventArgs"></see> class. </summary>
        /// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
        /// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than -1.-or-rowIndex is less than -1.</exception>
        public GridCellEventArgs(int columnIndex, int rowIndex)
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

        /// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
        /// <returns>The index of the column containing the cell that the event occurs for.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
        /// <returns>The index of the row containing the cell that the event occurs for.</returns>
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

