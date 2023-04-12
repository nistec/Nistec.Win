namespace MControl.GridView
{
    using System;

    /// <summary>Defines values for specifying how the heights of rows are adjusted. </summary>
    /// <filterpriority>2</filterpriority>
    public enum GridAutoSizeRowsMode
    {
        /// <summary>The row heights adjust to fit the contents of all cells in the rows, including header cells. </summary>
        AllCells = 7,
        /// <summary>The row heights adjust to fit the contents of all cells in the rows, excluding header cells. </summary>
        AllCellsExceptHeaders = 6,
        /// <summary>The row heights adjust to fit the contents of the row header. </summary>
        AllHeaders = 5,
        /// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, including header cells. </summary>
        DisplayedCells = 11,
        /// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, excluding header cells. </summary>
        DisplayedCellsExceptHeaders = 10,
        /// <summary>The row heights adjust to fit the contents of the row headers currently displayed onscreen.</summary>
        DisplayedHeaders = 9,
        /// <summary>The row heights do not automatically adjust.</summary>
        /// <filterpriority>1</filterpriority>
        None = 0
    }
}

