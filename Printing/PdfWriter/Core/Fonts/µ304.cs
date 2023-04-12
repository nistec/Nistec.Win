namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct A304
    {
        private int _b0;
        private ushort _b1;
        private ushort _b2;
        private short _b3;
        internal A304(ushort b1, ushort b2, short b3, int b0)
        {
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
            this._b0 = b0;
        }

        internal ushort A309
        {
            get
            {
                return this._b1;
            }
        }
        internal ushort A306
        {
            get
            {
                return this._b2;
            }
        }
        internal short A308
        {
            get
            {
                return this._b3;
            }
        }
        internal int A307
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }
    }
}

