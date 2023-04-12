namespace MControl.GridView
{
    using System;

    /// <summary>Defines constants that indicate the alignment of content within a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
    /// <filterpriority>2</filterpriority>
    public enum GridContentAlignment
    {
        /// <summary>The content is aligned vertically at the bottom and horizontally at the center of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        BottomCenter = 0x200,
        /// <summary>The content is aligned vertically at the bottom and horizontally at the left of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        BottomLeft = 0x100,
        /// <summary>The content is aligned vertically at the bottom and horizontally at the right of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        BottomRight = 0x400,
        /// <summary>The content is aligned at the vertical and horizontal center of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        MiddleCenter = 0x20,
        /// <summary>The content is aligned vertically at the middle and horizontally at the left of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        MiddleLeft = 0x10,
        /// <summary>The content is aligned vertically at the middle and horizontally at the right of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        MiddleRight = 0x40,
        /// <summary>The alignment is not set.</summary>
        /// <filterpriority>1</filterpriority>
        NotSet = 0,
        /// <summary>The content is aligned vertically at the top and horizontally at the center of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        TopCenter = 2,
        /// <summary>The content is aligned vertically at the top and horizontally at the left of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        TopLeft = 1,
        /// <summary>The content is aligned vertically at the top and horizontally at the right of a cell.</summary>
        /// <filterpriority>1</filterpriority>
        TopRight = 4
    }
}

