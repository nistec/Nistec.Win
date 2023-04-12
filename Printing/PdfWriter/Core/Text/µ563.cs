namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A563
    {
        private A564[] _b0 = new A564[10];
        private int _b1;

        internal A563()
        {
        }

        internal void A3(A564 b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        internal void A4()
        {
            for (int i = 0; i < this._b1; i++)
            {
                this._b0[i] = null;
            }
            this._b1 = 0;
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
                A564[] destinationArray = new A564[2 * this._b0.Length];
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

        internal A564 this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

