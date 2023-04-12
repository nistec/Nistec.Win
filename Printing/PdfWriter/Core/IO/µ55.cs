namespace MControl.Printing.Pdf.Core.IO
{
    using System;

    internal abstract class A55
    {
        internal static char[] A16 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        internal A55()
        {
        }

        protected abstract void _A548(byte b0);
        internal void A176(string b4)
        {
            this.A54(b4);
            this._A548(10);
        }

        internal void A181(string b4)
        {
            this.A54(b4);
            this._A548(13);
            this._A548(10);
        }

        internal void A54(byte b1)
        {
            this._A548(b1);
        }

        internal void A54(string b4)
        {
            int length = b4.Length;
            for (int i = 0; i < length; i++)
            {
                this._A548((byte) b4[i]);
            }
        }

        internal void A54(byte[] b2, int b3)
        {
            for (int i = 0; i < b3; i++)
            {
                this._A548(b2[i]);
            }
        }

        internal void A549(string b4)
        {
            this.A54(b4);
            this._A548(20);
        }

        internal void A58(byte[] b2)
        {
            for (int i = 0; i < b2.Length; i++)
            {
                byte num2 = b2[i];
                this._A548((byte) A16[num2 >> 4]);
                this._A548((byte) A16[num2 & 15]);
            }
        }

        internal void A59(string b4)
        {
            this.A54(b4);
            this._A548(13);
        }

        internal abstract int A2 { get; }
    }
}

