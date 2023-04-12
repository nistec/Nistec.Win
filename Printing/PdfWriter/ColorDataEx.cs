namespace MControl.Printing.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ColorDataEx
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
}

