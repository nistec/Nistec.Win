namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CHARSETINFO
    {
        public int ciCharset;
        public int ciACP;
        public tagFONTSIGNATURE fs;
    }
}

