namespace MControl.GridView
{
    using System;

    /// <summary>Defines values for specifying how the height of a row is adjusted. </summary>
    public enum GridAutoSizeRowMode
    {
        /// <summary>The row height adjusts to fit the contents of all cells in the row, including the header cell. </summary>
        AllCells = 3,
        /// <summary>The row height adjusts to fit the contents of all cells in the row, excluding the header cell. </summary>
        AllCellsExceptHeader = 2,
        /// <summary>The row height adjusts to fit the contents of the row header. </summary>
        RowHeader = 1
    }
}

