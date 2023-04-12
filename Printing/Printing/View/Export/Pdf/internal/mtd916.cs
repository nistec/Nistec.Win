namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct mtd916
    {
        internal ushort mtd918;
        internal ushort mtd919;
        internal ushort mtd852;
        internal ushort mtd917;
        internal ushort mtd920;
        internal ushort mtd921;
        internal static int mtd845
        {
            get
            {
                return 12;
            }
        }
    }
}

