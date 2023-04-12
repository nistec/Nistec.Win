namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Provides data for mouse events raised by a <see cref="T:MControl.GridView.Grid"></see> whenever the mouse is moved within a <see cref="T:MControl.GridView.GridCell"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellMouseEventArgs : MouseEventArgs
    {
        private int columnIndex;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellMouseEventArgs"></see> class.</summary>
        /// <param name="localY">The y-coordinate of the mouse, in pixels.</param>
        /// <param name="columnIndex">The cell's zero-based column index.</param>
        /// <param name="e">The originating <see cref="T:System.Windows.Forms.MouseEventArgs"></see>.</param>
        /// <param name="rowIndex">The cell's zero-based row index.</param>
        /// <param name="localX">The x-coordinate of the mouse, in pixels.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than -1.-or-rowIndex is less than -1.</exception>
        public GridCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e) : base(e.Button, e.Clicks, localX, localY, e.Delta)
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

        /// <summary>Gets the zero-based column index of the cell.</summary>
        /// <returns>An integer specifying the column index.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets the zero-based row index of the cell.</summary>
        /// <returns>An integer specifying the row index.</returns>
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

