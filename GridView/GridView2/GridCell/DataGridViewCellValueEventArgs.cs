namespace MControl.GridView
{
    using System;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellValueNeeded"></see> and <see cref="E:MControl.GridView.Grid.CellValuePushed"></see> events of the <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellValueEventArgs : EventArgs
    {
        private int columnIndex;
        private int rowIndex;
        private object val;

        internal GridCellValueEventArgs()
        {
            this.columnIndex = this.rowIndex = -1;
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellValueEventArgs"></see> class. </summary>
        /// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
        /// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than 0.-or-rowIndex is less than 0.</exception>
        public GridCellValueEventArgs(int columnIndex, int rowIndex)
        {
            if (columnIndex < 0)
            {
                throw new ArgumentOutOfRangeException("columnIndex");
            }
            if (rowIndex < 0)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        internal void SetProperties(int columnIndex, int rowIndex, object value)
        {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
            this.val = value;
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

        /// <summary>Gets or sets the value of the cell that the event occurs for.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing the cell's value.</returns>
        /// <filterpriority>1</filterpriority>
        public object Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }
    }
}

