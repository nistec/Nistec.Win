namespace MControl.Printing.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ColorData16
    {
        public short Blue;
        public short Green;
        public short Red;
    }
}

