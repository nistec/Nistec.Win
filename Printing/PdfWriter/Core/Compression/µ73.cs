namespace MControl.Printing.Pdf.Core.Compression
{
    using System;

    internal class A73
    {
        private int _b0;
        private int _b1;
        private int _b2;
        private int _b3;

        internal A73(int b3, int b1, int b4, int b2)
        {
            this._b3 = b3;
            this._b1 = b1;
            this._b0 = b4;
            this._b2 = b2;
        }

        internal bool A68(int b5)
        {
            return ((b5 >= this._b3) && (b5 <= this._b1));
        }

        internal void A81(int b5, A70 b6)
        {
            b6.A72(b5 - this._b3, this._b2);
        }

        internal int A80
        {
            get
            {
                return this._b0;
            }
        }

        internal int A87
        {
            get
            {
                return this._b1;
            }
        }

        internal int A88
        {
            get
            {
                return this._b2;
            }
        }

        internal int A89
        {
            get
            {
                return this._b3;
            }
        }
    }
}

