namespace MControl.GridView
{
    using System;

    /// <summary>Specifies how a user starts cell editing in the <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public enum GridEditMode
    {
        EditOnEnter,
        EditOnKeystroke,
        EditOnKeystrokeOrF2,
        EditOnF2,
        EditProgrammatically
    }
}

