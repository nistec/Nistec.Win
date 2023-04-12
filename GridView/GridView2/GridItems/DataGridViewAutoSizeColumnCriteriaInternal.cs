namespace MControl.GridView
{
    using System;

    [Flags]
    internal enum GridAutoSizeColumnCriteriaInternal
    {
        AllRows = 4,
        DisplayedRows = 8,
        Fill = 0x10,
        Header = 2,
        None = 1,
        NotSet = 0
    }
}

