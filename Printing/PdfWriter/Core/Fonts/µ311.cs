namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A311
    {
        private byte[] _b0;

        internal A311()
        {
        }

        internal virtual void A312(A286 b1, int b2)
        {
            this._b0 = b1.A287(b2);
        }

        internal void A54(A290 b1)
        {
            b1.A291(this._b0);
        }

        internal byte[] A221
        {
            get
            {
                return this._b0;
            }
        }
    }
}

