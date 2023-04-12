namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A314
    {
        private uint _b0;
        private uint _b1;
        private uint _b2;
        private byte[] _b3;

        internal A314()
        {
        }

        internal void A237(A51 b4)
        {
            if (b4.A226.Length == 4)
            {
                this._b3 = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    this._b3[i] = Convert.ToByte(b4.A226[i]);
                }
                this._b0 = 0;
                this._b2 = 0;
                this._b1 = (uint) b4.A84;
            }
        }

        internal void A312(A286 b5)
        {
            this._b3 = b5.A287(4);
            this._b0 = b5.A303();
            this._b2 = b5.A303();
            this._b1 = b5.A303();
        }

        internal void A318(A290 b5)
        {
            if (this._b0 == 0)
            {
                this._b0 = b5.A322(this.A85, this.A84);
            }
            b5.A301(4);
            b5.A299(this._b0);
            b5.A301(8);
        }

        internal void A320(A290 b5)
        {
            b5.A301(8);
            b5.A299(this._b2);
            b5.A301(4);
        }

        internal void A321(int b2)
        {
            if (this._b2 == 0)
            {
                this._b2 = (uint) b2;
            }
        }

        internal void A54(A290 b5)
        {
            b5.A291(this._b3);
            b5.A299(this._b0);
            b5.A299(this._b2);
            b5.A299(this._b1);
        }

        internal string A315
        {
            get
            {
                if (this._b3 == null)
                {
                    return null;
                }
                string str = "";
                for (int i = 0; i < this._b3.Length; i++)
                {
                    str = str + Convert.ToChar(this._b3[i]);
                }
                return str;
            }
        }

        internal static int A316
        {
            get
            {
                return 0x10;
            }
        }

        internal int A84
        {
            get
            {
                return Convert.ToInt32(this._b1);
            }
        }

        internal int A85
        {
            get
            {
                return Convert.ToInt32(this._b2);
            }
        }
    }
}

