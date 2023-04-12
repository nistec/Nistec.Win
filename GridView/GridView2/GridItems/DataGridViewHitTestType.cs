namespace MControl.GridView
{
    using System;

    /// <summary>Specifies a location in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public enum GridHitTestType
    {
        None,
        Cell,
        ColumnHeader,
        RowHeader,
        TopLeftHeader,
        HorizontalScrollBar,
        VerticalScrollBar
    }
}

