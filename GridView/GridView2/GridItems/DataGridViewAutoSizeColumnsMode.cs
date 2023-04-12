namespace MControl.GridView
{
    using System;

    /// <summary>Defines values for specifying how the widths of columns are adjusted. </summary>
    public enum GridAutoSizeColumnsMode
    {
        /// <summary>The column widths adjust to fit the contents of all cells in the columns, including header cells. </summary>
        AllCells = 6,
        /// <summary>The column widths adjust to fit the contents of all cells in the columns, excluding header cells. </summary>
        AllCellsExceptHeader = 4,
        /// <summary>The column widths adjust to fit the contents of the column header cells. </summary>
        ColumnHeader = 2,
        /// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, including header cells. </summary>
        DisplayedCells = 10,
        /// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, excluding header cells. </summary>
        DisplayedCellsExceptHeader = 8,
        /// <summary>The column widths adjust so that the widths of all columns exactly fill the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:MControl.GridView.GridColumn.MinimumWidth"></see> property values. Relative column widths are determined by the relative <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> property values.</summary>
        Fill = 0x10,
        /// <summary>The column widths do not automatically adjust. </summary>
        None = 1
    }
}

