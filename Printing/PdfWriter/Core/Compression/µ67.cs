namespace MControl.Printing.Pdf.Core.Compression
{
    using System;

    internal class A67
    {
        private int _b0;
        private int _b1;
        private int _b2;
        private int _b3;

        internal A67(int b3, int b2, int b0, int b1)
        {
            this._b3 = b3;
            this._b2 = b2;
            this._b0 = b0;
            this._b1 = b1;
        }

        internal bool A68(int b4)
        {
            return ((b4 >= this._b3) && (b4 <= this._b2));
        }

        internal void A69(int b4, A70 b5)
        {
            int num = A35.A71(this._b1 + (b4 - this._b3), this._b0);
            b5.A72(num, this._b0);
        }
    }
}

