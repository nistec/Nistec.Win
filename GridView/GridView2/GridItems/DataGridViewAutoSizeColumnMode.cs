namespace MControl.GridView
{
    using System;

    /// <summary>Defines values for specifying how the width of a column is adjusted. </summary>
    public enum GridAutoSizeColumnMode
    {
        /// <summary>The column width adjusts to fit the contents of all cells in the column, including the header cell. </summary>
        AllCells = 6,
        /// <summary>The column width adjusts to fit the contents of all cells in the column, excluding the header cell. </summary>
        AllCellsExceptHeader = 4,
        /// <summary>The column width adjusts to fit the contents of the column header cell. </summary>
        ColumnHeader = 2,
        /// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, including the header cell. </summary>
        DisplayedCells = 10,
        /// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, excluding the header cell. </summary>
        DisplayedCellsExceptHeader = 8,
        /// <summary>The column width adjusts so that the widths of all columns exactly fills the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:MControl.GridView.GridColumn.MinimumWidth"></see> property values. Relative column widths are determined by the relative <see cref="P:MControl.GridView.GridColumn.FillWeight"></see> property values.</summary>
        Fill = 0x10,
        /// <summary>The column width does not automatically adjust.</summary>
        None = 1,
        /// <summary>The sizing behavior of the column is inherited from the <see cref="P:MControl.GridView.Grid.AutoSizeColumnsMode"></see> property.</summary>
        NotSet = 0
    }
}

