namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellValidating"></see> event of a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    public class GridCellValidatingEventArgs : CancelEventArgs
    {
        private int columnIndex;
        private object formattedValue;
        private int rowIndex;

        internal GridCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.formattedValue = formattedValue;
        }

        /// <summary>Gets the column index of the cell that needs to be validated.</summary>
        /// <returns>A zero-based integer that specifies the column index of the cell that needs to be validated.</returns>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets the formatted contents of the cell that needs to be validated.</summary>
        /// <returns>A reference to the formatted value.</returns>
        public object FormattedValue
        {
            get
            {
                return this.formattedValue;
            }
        }

        /// <summary>Gets the row index of the cell that needs to be validated.</summary>
        /// <returns>A zero-based integer that specifies the row index of the cell that needs to be validated.</returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
    }
}

