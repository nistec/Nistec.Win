namespace MControl.GridView
{
    using System;
    using System.Windows.Forms;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellParsing"></see> event of a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellParsingEventArgs : ConvertEventArgs
    {
        private int columnIndex;
        private GridCellStyle inheritedCellStyle;
        private bool parsingApplied;
        private int rowIndex;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellParsingEventArgs"></see> class. </summary>
        /// <param name="inheritedCellStyle">The style applied to the cell that was changed.</param>
        /// <param name="columnIndex">The column index of the cell that was changed.</param>
        /// <param name="rowIndex">The row index of the cell that was changed.</param>
        /// <param name="desiredType">The type of the new value.</param>
        /// <param name="value">The new value.</param>
        public GridCellParsingEventArgs(int rowIndex, int columnIndex, object value, System.Type desiredType, GridCellStyle inheritedCellStyle) : base(value, desiredType)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.inheritedCellStyle = inheritedCellStyle;
        }

        /// <summary>Gets the column index of the cell data that requires parsing.</summary>
        /// <returns>The column index of the cell that was changed.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets or sets the style applied to the edited cell.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents the current style of the cell being edited. The default value is the value of the cell <see cref="P:MControl.GridView.GridCell.InheritedStyle"></see> property.</returns>
        public GridCellStyle InheritedCellStyle
        {
            get
            {
                return this.inheritedCellStyle;
            }
            set
            {
                this.inheritedCellStyle = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether a cell's value has been successfully parsed.</summary>
        /// <returns>true if the cell's value has been successfully parsed; otherwise, false. The default is false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool ParsingApplied
        {
            get
            {
                return this.parsingApplied;
            }
            set
            {
                this.parsingApplied = value;
            }
        }

        /// <summary>Gets the row index of the cell that requires parsing.</summary>
        /// <returns>The row index of the cell that was changed.</returns>
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

