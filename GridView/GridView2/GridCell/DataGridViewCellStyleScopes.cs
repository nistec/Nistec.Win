namespace MControl.GridView
{
    using System;

    /// <summary>Specifies the <see cref="T:MControl.GridView.Grid"></see> entity that owns the cell style that was changed.</summary>
    [Flags]
    public enum GridCellStyleScopes
    {
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.Grid.AlternatingRowsDefaultCellStyle"></see> property changed.</summary>
        AlternatingRows = 0x80,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.GridCell.Style"></see> property changed.</summary>
        Cell = 1,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.GridColumn.DefaultCellStyle"></see> property changed.</summary>
        Column = 2,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.Grid.ColumnHeadersDefaultCellStyle"></see> property changed.</summary>
        ColumnHeaders = 0x10,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.Grid.DefaultCellStyle"></see> property changed.</summary>
        Grid = 8,
        /// <summary>The owning entity is unspecified.</summary>
        None = 0,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.GridRow.DefaultCellStyle"></see> property changed.</summary>
        Row = 4,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.Grid.RowHeadersDefaultCellStyle"></see> property changed.</summary>
        RowHeaders = 0x20,
        /// <summary>One or more values of the object returned by the <see cref="P:MControl.GridView.Grid.RowsDefaultCellStyle"></see> property changed.</summary>
        Rows = 0x40
    }
}

