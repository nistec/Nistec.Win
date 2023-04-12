namespace MControl.GridView
{
    using System;
    using System.Drawing;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.RowPostPaint"></see> event. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridRowPostPaintEventArgs : EventArgs
    {
        private Rectangle clipBounds;
        private Grid grid;
        private string errorText;
        private System.Drawing.Graphics graphics;
        private GridCellStyle inheritedRowStyle;
        private bool isFirstDisplayedRow;
        private bool isLastVisibleRow;
        private Rectangle rowBounds;
        private int rowIndex;
        private GridElementStates rowState;

        internal GridRowPostPaintEventArgs(Grid grid)
        {
            this.grid = grid;
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridRowPostPaintEventArgs"></see> class. </summary>
        /// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"></see> that contains the bounds of the <see cref="T:MControl.GridView.GridRow"></see> that is being painted.</param>
        /// <param name="rowState">A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</param>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"></see> used to paint the <see cref="T:MControl.GridView.GridRow"></see>.</param>
        /// <param name="grid">The <see cref="T:MControl.GridView.Grid"></see> that owns the row that is being painted.</param>
        /// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has the <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false.</param>
        /// <param name="inheritedRowStyle">A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains formatting and style information about the row.</param>
        /// <param name="errorText">An error message that is associated with the row.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be painted.</param>
        /// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row currently displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false.</param>
        /// <exception cref="T:System.ArgumentNullException">grid is null.-or-graphics is null.-or-inheritedRowStyle is null.</exception>
        public GridRowPostPaintEventArgs(Grid grid, System.Drawing.Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, GridElementStates rowState, string errorText, GridCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            if (inheritedRowStyle == null)
            {
                throw new ArgumentNullException("inheritedRowStyle");
            }
            this.grid = grid;
            this.graphics = graphics;
            this.clipBounds = clipBounds;
            this.rowBounds = rowBounds;
            this.rowIndex = rowIndex;
            this.rowState = rowState;
            this.errorText = errorText;
            this.inheritedRowStyle = inheritedRowStyle;
            this.isFirstDisplayedRow = isFirstDisplayedRow;
            this.isLastVisibleRow = isLastVisibleRow;
        }

        /// <summary>Draws the focus rectangle around the specified bounds.</summary>
        /// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:MControl.GridView.GridCellStyle.SelectionBackColor"></see> property of the <see cref="P:MControl.GridView.GridRow.InheritedStyle"></see> property to determine the color of the focus rectangle; false to use the <see cref="P:MControl.GridView.GridCellStyle.BackColor"></see> property of the <see cref="P:MControl.GridView.GridRow.InheritedStyle"></see> property.</param>
        /// <param name="bounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the focus area.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
        {
            if ((this.rowIndex < 0) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            this.grid.Rows.SharedRow(this.rowIndex).DrawFocus(this.graphics, this.clipBounds, bounds, this.rowIndex, this.rowState, this.inheritedRowStyle, cellsPaintSelectionBackground);
        }

        /// <summary>Paints the specified cell parts for the area in the specified bounds.</summary>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values specifying the parts to paint.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        public void PaintCells(Rectangle clipBounds, GridPaintParts paintParts)
        {
            if ((this.rowIndex < 0) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            this.grid.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
        }

        /// <summary>Paints the cell backgrounds for the area in the specified bounds.</summary>
        /// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:MControl.GridView.GridCellStyle.SelectionBackColor"></see> property of the <see cref="P:MControl.GridView.GridRow.InheritedStyle"></see>; false to paint the background of the specified bounds with the color of the <see cref="P:MControl.GridView.GridCellStyle.BackColor"></see> property of the <see cref="P:MControl.GridView.GridRow.InheritedStyle"></see>.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        public void PaintCellsBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
        {
            if ((this.rowIndex < 0) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            GridPaintParts paintParts = GridPaintParts.Border | GridPaintParts.Background;
            if (cellsPaintSelectionBackground)
            {
                paintParts |= GridPaintParts.SelectionBackground;
            }
            this.grid.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
        }

        /// <summary>Paints the cell contents for the area in the specified bounds.</summary>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"></see> that specifies the area of the <see cref="T:MControl.GridView.Grid"></see> to be painted.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        public void PaintCellsContent(Rectangle clipBounds)
        {
            if ((this.rowIndex < 0) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            this.grid.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, GridPaintParts.ErrorIcon | GridPaintParts.ContentForeground | GridPaintParts.ContentBackground);
        }

        /// <summary>Paints the entire row header of the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <param name="paintSelectionBackground">true to paint the row header with the color of the <see cref="P:MControl.GridView.GridCellStyle.SelectionBackColor"></see> property of the <see cref="P:MControl.GridView.GridRow.InheritedStyle"></see>; false to paint the row header with the <see cref="P:MControl.GridView.GridCellStyle.BackColor"></see> of the <see cref="P:MControl.GridView.Grid.RowHeadersDefaultCellStyle"></see> property.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public void PaintHeader(bool paintSelectionBackground)
        {
            GridPaintParts paintParts = GridPaintParts.ErrorIcon | GridPaintParts.ContentForeground | GridPaintParts.ContentBackground | GridPaintParts.Border | GridPaintParts.Background;
            if (paintSelectionBackground)
            {
                paintParts |= GridPaintParts.SelectionBackground;
            }
            this.PaintHeader(paintParts);
        }

        /// <summary>Paints the specified parts of the row header of the current row.</summary>
        /// <param name="paintParts">A bitwise combination of <see cref="T:MControl.GridView.GridPaintParts"></see> values specifying the parts to paint.</param>
        /// <exception cref="T:System.InvalidOperationException"><see cref="P:MControl.GridView.GridRowPostPaintEventArgs.RowIndex"></see> is less than zero or greater than the number of rows in the <see cref="T:MControl.GridView.Grid"></see> control minus one.</exception>
        public void PaintHeader(GridPaintParts paintParts)
        {
            if ((this.rowIndex < 0) || (this.rowIndex >= this.grid.Rows.Count))
            {
                throw new InvalidOperationException(MControl.GridView.RM.GetString("GridElementPaintingEventArgs_RowIndexOutOfRange"));
            }
            this.grid.Rows.SharedRow(this.rowIndex).PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
        }

        internal void SetProperties(System.Drawing.Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, GridElementStates rowState, string errorText, GridCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            this.graphics = graphics;
            this.clipBounds = clipBounds;
            this.rowBounds = rowBounds;
            this.rowIndex = rowIndex;
            this.rowState = rowState;
            this.errorText = errorText;
            this.inheritedRowStyle = inheritedRowStyle;
            this.isFirstDisplayedRow = isFirstDisplayedRow;
            this.isLastVisibleRow = isLastVisibleRow;
        }

        /// <summary>Gets or sets the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the area of the <see cref="T:MControl.GridView.Grid"></see> that needs to be repainted.</returns>
        /// <filterpriority>1</filterpriority>
        public Rectangle ClipBounds
        {
            get
            {
                return this.clipBounds;
            }
            set
            {
                this.clipBounds = value;
            }
        }

        /// <summary>Gets a string that represents an error message for the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>A string that represents an error message for the current <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public string ErrorText
        {
            get
            {
                return this.errorText;
            }
        }

        /// <summary>Gets the <see cref="T:System.Drawing.Graphics"></see> used to paint the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>The <see cref="T:System.Drawing.Graphics"></see> used to paint the current <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public System.Drawing.Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        /// <summary>Gets the cell style applied to the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that contains the cell style applied to the current <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        public GridCellStyle InheritedRowStyle
        {
            get
            {
                return this.inheritedRowStyle;
            }
        }

        /// <summary>Gets a value indicating whether the current row is the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>.</summary>
        /// <returns>true if the row being painted is currently the first row displayed in the <see cref="T:MControl.GridView.Grid"></see>; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsFirstDisplayedRow
        {
            get
            {
                return this.isFirstDisplayedRow;
            }
        }

        /// <summary>Gets a value indicating whether the current row is the last visible row displayed in the <see cref="T:MControl.GridView.Grid"></see>.</summary>
        /// <returns>true if the current row is the last row in the <see cref="T:MControl.GridView.Grid"></see> that has the <see cref="P:MControl.GridView.GridRow.Visible"></see> property set to true; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsLastVisibleRow
        {
            get
            {
                return this.isLastVisibleRow;
            }
        }

        /// <summary>Get the bounds of the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>A <see cref="T:System.Drawing.Rectangle"></see> that represents the bounds of the current <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public Rectangle RowBounds
        {
            get
            {
                return this.rowBounds;
            }
        }

        /// <summary>Gets the index of the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>The index of the current <see cref="T:MControl.GridView.GridRow"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>Gets the state of the current <see cref="T:MControl.GridView.GridRow"></see>.</summary>
        /// <returns>A bitwise combination of <see cref="T:MControl.GridView.GridElementStates"></see> values that specifies the state of the row.</returns>
        /// <filterpriority>1</filterpriority>
        public GridElementStates State
        {
            get
            {
                return this.rowState;
            }
        }
    }
}

