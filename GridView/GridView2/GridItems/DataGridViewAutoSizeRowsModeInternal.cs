namespace MControl.GridView
{
    using System;

    [Flags]
    internal enum GridAutoSizeRowsModeInternal
    {
        AllColumns = 2,
        AllRows = 4,
        DisplayedRows = 8,
        Header = 1,
        None = 0
    }
}

