namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.CellPainting"></see> event. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridCellPaintingEventArgs : HandledEventArgs
    {
        private GridAdvancedBorderStyle advancedBorderStyle;
        private Rectangle cellBounds;
        private GridElementStates cellState;
        private GridCellStyle cellStyle;
        private Rectangle clipBounds;
        private int columnIndex;
        private Grid grid;
        private string errorText;
        private object formattedValue;
        private System.Drawing.Graphics graphics;
        private GridPaintParts paintParts;
        private int rowIndex;
        private object value;

        internal GridCellPaintingEventArgs(Grid grid)
        {
            this.grid = grid;
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellPaintingEventArgs"></see> class. </summary>
        /// <param name="formattedValue">The formatted data of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values specifying the parts to paint.</param>
        /// <param name="advancedBorderStyle">A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that contains border styles for the cell that is being painted.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridCell"></see>.</param>
        /// <param name="grid">The <see cref="T:MControl.GridView.Grid"></see> that contains the cell to be painted.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</param>
        /// <param name="columnIndex">The column index of the cell that is being painted.</param>
        /// <param name="cellState">A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the cell.</param>
        /// <param name="value">The data of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        /// <param name="cellStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains formatting and style information about the cell.</param>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridCell"></see> that is being painted.</param>
        /// <exception cref="T:System.ArgumentException">paintParts is not a valid bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values.</exception>
        /// <exception cref="T:System.ArgumentNullException">grid is null.-or-graphics is null.-or-cellStyle is null.</exception>
        public GridCellPaintingEventArgs(Grid grid, System.Drawing.Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if ((paintParts & ~GridPaintParts.All) != GridPaintParts.None)
            {
                throw new ArgumentException(MControl.GridView.RM.GetString("Grid_InvalidGridPaintPartsCombination", new object[] { "paintParts" }));
            }
            this.graphics = graphics;
            this.clipBounds = clipBounds;
            this.cellBounds = cellBounds;
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.cellState = cellState;
            this.value = value;
            this.formattedValue = formattedValue;
            this.errorText = errorText;
            this.cellStyle = cellStyle;
            this.advancedBorderStyle = advancedBorderStyle;
            this.paintParts = paintParts;
        }

        /// <summary>Paints the specified parts of the cell for the area in the specified bounds.</summary>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values specifying the parts to paint.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCellPaintingEventArgs.RowIndex"></see> is less than -1 or greater than or equal to the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control.-or-<see cref="P:MControl.GridView.GridCellPaintingEventArgs.ColumnIndex"></see> is less than -1 or greater than or equal to the number of columns in the <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        public void Paint(Rectangle clipBounds, GridPaintParts paintParts)
        {
            if ((this.rowIndex < -1) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            if ((this.columnIndex < -1) || (this.columnIndex >= this.grid.Columns.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_ColumnIndexOutOfRange"));
            }
            this.grid.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, paintParts);
        }

        /// <summary>Paints the cell background for the area in the specified bounds.</summary>
        /// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:MControl.GridView.GridCellStyle.SelectionBackColor"></see> property of the <see cref="P:MControl.GridView.GridCell.InheritedStyle"></see>; false to paint the background of the specified bounds with the color of the <see cref="P:MControl.GridView.GridCellStyle.BackColor"></see> property of the <see cref="P:MControl.GridView.GridCell.InheritedStyle"></see>.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCellPaintingEventArgs.RowIndex"></see> is less than -1 or greater than or equal to the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control.-or-<see cref="P:MControl.GridView.GridCellPaintingEventArgs.ColumnIndex"></see> is less than -1 or greater than or equal to the number of columns in the <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
        {
            if ((this.rowIndex < -1) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            if ((this.columnIndex < -1) || (this.columnIndex >= this.grid.Columns.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_ColumnIndexOutOfRange"));
            }
            GridPaintParts paintParts = GridPaintParts.Border | GridPaintParts.Background;
            if (cellsPaintSelectionBackground)
            {
                paintParts |= GridPaintParts.SelectionBackground;
            }
            this.grid.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, paintParts);
        }

        /// <summary>Paints the cell content for the area in the specified bounds.</summary>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridCellPaintingEventArgs.RowIndex"></see> is less than -1 or greater than or equal to the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control.-or-<see cref="P:MControl.GridView.GridCellPaintingEventArgs.ColumnIndex"></see> is less than -1 or greater than or equal to the number of columns in the <see cref="T:MControl.GridView.Grid"></see> control.</exception>
        public void PaintContent(Rectangle clipBounds)
        {
            if ((this.rowIndex < -1) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            if ((this.columnIndex < -1) || (this.columnIndex >= this.grid.Columns.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_ColumnIndexOutOfRange"));
            }
            this.grid.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, GridPaintParts.ErrorIcon | GridPaintParts.ContentForeground | GridPaintParts.ContentBackground);
        }

        internal void SetProperties(System.Drawing.Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, GridElementStates cellState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            this.graphics = graphics;
            this.clipBounds = clipBounds;
            this.cellBounds = cellBounds;
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.cellState = cellState;
            this.value = value;
            this.formattedValue = formattedValue;
            this.errorText = errorText;
            this.cellStyle = cellStyle;
            this.advancedBorderStyle = advancedBorderStyle;
            this.paintParts = paintParts;
            base.Handled = false;
        }

        /// <summary>Gets the border style of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> that represents the border style of the <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public GridAdvancedBorderStyle AdvancedBorderStyle
        {
            get
            {
                return this.advancedBorderStyle;
            }
        }

        /// <summary>Get the bounds of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the bounds of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public Rectangle CellBounds
        {
            get
            {
                return this.cellBounds;
            }
        }

        /// <summary>Gets the cell style of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains the cell style of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public GridCellStyle CellStyle
        {
            get
            {
                return this.cellStyle;
            }
        }

        /// <summary>Gets the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</returns>
        /// <filterpriority>1</filterpriority>
        public Rectangle ClipBounds
        {
            get
            {
                return this.clipBounds;
            }
        }

        /// <summary>Gets the column index of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The column index of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>Gets a string that represents an error message for the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>A string that represents an error message for the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public string ErrorText
        {
            get
            {
                return this.errorText;
            }
        }

        /// <summary>Gets the formatted value of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The formatted value of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public object FormattedValue
        {
            get
            {
                return this.formattedValue;
            }
        }

        /// <summary>Gets the <see cref="T:System.Drawing.Graphics"></see> used to paint the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The <see cref="T:System.Drawing.Graphics"></see> used to paint the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public System.Drawing.Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        /// <summary>The cell parts that are to be painted.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values specifying the parts to be painted.</returns>
        public GridPaintParts PaintParts
        {
            get
            {
                return this.paintParts;
            }
        }

        /// <summary>Gets the row index of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The row index of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>Gets the state of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the cell.</returns>
        /// <filterpriority>1</filterpriority>
        public GridElementStates State
        {
            get
            {
                return this.cellState;
            }
        }

        /// <summary>Gets the value of the current <see cref="T:MControl.GridView.GridCell"></see>.</summary>
        /// <returns>The value of the current <see cref="T:MControl.GridView.GridCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

