namespace MControl.Printing.Pdf.Core.Collection
{
    using MControl.Printing.Pdf;
    using System;
    using System.Reflection;

    internal class A7
    {
        private Page[] _b0 = new Page[10];
        private int _b1 = 0;

        internal A7()
        {
        }

        internal void A10(Page b3)
        {
            int num = this.A11(b3);
            if (num != -1)
            {
                this.A9(num);
            }
        }

        internal int A11(Page b3)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (b3 == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        internal void A3(Page b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        internal void A8(Page b3, int b2)
        {
            if ((b2 < 0) || (b2 > this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this.b4();
            if (b2 < this._b1)
            {
                Array.Copy(this._b0, b2, this._b0, b2 + 1, this._b1 - b2);
            }
            this._b0[b2] = b3;
            this._b1++;
        }

        internal void A9(int b2)
        {
            if ((b2 < 0) || (b2 >= this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._b1--;
            if (b2 < this._b1)
            {
                Array.Copy(this._b0, b2 + 1, this._b0, b2, this._b1 - b2);
            }
            this._b0[this._b1] = null;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                Page[] destinationArray = new Page[2 * this._b0.Length];
                Array.Copy(this._b0, destinationArray, this._b1);
                this._b0 = destinationArray;
            }
        }

        internal int A2
        {
            get
            {
                return this._b1;
            }
        }

        internal Page this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

