namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellFormatting"></see> event of a <see cref="T:MControl.GridView.Grid"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellFormattingEventArgs : ConvertEventArgs
    {
        private GridCellStyle cellStyle;
        private int columnIndex;
        private bool formattingApplied;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellFormattingEventArgs"></see> class.</summary>
        /// <param name="cellStyle">The style of the cell that caused the event.</param>
        /// <param name="columnIndex">The column index of the cell that caused the event.</param>
        /// <param name="rowIndex">The row index of the cell that caused the event.</param>
        /// <param name="desiredType">The type to convert value to. </param>
        /// <param name="value">The cell's contents.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">columnIndex is less than -1-or-rowIndex is less than -1.</exception>
        public GridCellFormattingEventArgs(int columnIndex, int rowIndex, object value, System.Type desiredType, GridCellStyle cellStyle) : base(value, desiredType)
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
            this.cellStyle = cellStyle;
        }

        /// <summary>Gets or sets the style of the cell that is being formatted.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the display style of the cell being formatted. The default is the value of the cell's <see cref="P:MControl.GridView.GridCell.InheritedStyle"></see> property. </returns>
        /// <filterpriority>1</filterpriority>
        public GridCellStyle CellStyle
        {
            get
            {
                return this.cellStyle;
            }
            set
            {
                this.cellStyle = value;
            }
        }

        /// <summary>Gets the column index of the cell that is being formatted.</summary>
        /// <returns>The column index of the cell that is being formatted.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets or sets a value indicating whether the cell value has been successfully formatted.</summary>
        /// <returns>true if the formatting for the cell value has been handled; otherwise, false. The default value is false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool FormattingApplied
        {
            get
            {
                return this.formattingApplied;
            }
            set
            {
                this.formattingApplied = value;
            }
        }

        /// <summary>Gets the row index of the cell that is being formatted.</summary>
        /// <returns>The row index of the cell that is being formatted.</returns>
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

