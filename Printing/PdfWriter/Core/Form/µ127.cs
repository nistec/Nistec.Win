namespace MControl.Printing.Pdf.Core.Controls
{
    using System;

    [Flags]
    internal enum A127
    {
        Comb = 0x1000000,
        Combo = 0x20000,
        CommitOnSelChange = 0x4000000,
        Default = 0,
        DoNotScroll = 0x800000,
        DoNotSpellCheck = 0x400000,
        Edit = 0x40000,
        FileSelect = 0x100000,
        Multiline = 0x1000,
        MultiSelect = 0x200000,
        NoExport = 4,
        NoToggleToOff = 0x4000,
        Password = 0x2000,
        PushButton = 0x10000,
        Radio = 0x8000,
        RadiosInUnion = 0x2000000,
        ReadOnly = 1,
        Required = 2,
        RichText = 0x2000000,
        Sort = 0x80000
    }
}

