namespace MControl.GridView
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>Specifies the user interface (UI) state of a element within a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    [ComVisible(true), Flags]
    public enum GridElementStates
    {
        /// <summary>Indicates the an element is currently displayed onscreen.</summary>
        Displayed = 1,
        /// <summary>Indicates that an element cannot be scrolled through the UI.</summary>
        Frozen = 2,
        /// <summary>Indicates that an element is in its default state.</summary>
        None = 0,
        /// <summary>Indicates that an element will not accept user input to change its value.</summary>
        ReadOnly = 4,
        /// <summary>Indicates that an element can be resized through the UI. This value is ignored except when combined with the <see cref="F:MControl.GridView.GridElementStates.ResizableSet"></see> value.</summary>
        Resizable = 8,
        /// <summary>Indicates that an element does not inherit the resizable state of its parent.</summary>
        ResizableSet = 0x10,
        /// <summary>Indicates that an element is in a selected (highlighted) UI state.</summary>
        Selected = 0x20,
        /// <summary>Indicates that an element is visible (displayable).</summary>
        Visible = 0x40
    }
}

