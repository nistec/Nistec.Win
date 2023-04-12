namespace MControl.GridView
{
    using System;

    /// <summary>Defines values for specifying the parts of a <see cref="T:MControl.GridView.GridCell"></see> that are to be painted.</summary>
    [Flags]
    public enum GridPaintParts
    {
        /// <summary>All parts of the cell should be painted.</summary>
        All = 0x7f,
        /// <summary>The background of the cell should be painted.</summary>
        Background = 1,
        /// <summary>The border of the cell should be painted.</summary>
        Border = 2,
        /// <summary>The background of the cell content should be painted.</summary>
        ContentBackground = 4,
        /// <summary>The foreground of the cell content should be painted.</summary>
        ContentForeground = 8,
        /// <summary>The cell error icon should be painted.</summary>
        ErrorIcon = 0x10,
        /// <summary>The focus rectangle should be painted around the cell.</summary>
        Focus = 0x20,
        /// <summary>Nothing should be painted.</summary>
        None = 0,
        /// <summary>The background of the cell should be painted when the cell is selected.</summary>
        SelectionBackground = 0x40
    }
}

