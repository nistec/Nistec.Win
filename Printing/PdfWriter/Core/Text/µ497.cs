namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A497
    {
        private A523[] _b0 = new A523[10];
        private int _b1;

        internal A497()
        {
        }

        internal void A3(A523 b3)
        {
            if (b3 != null)
            {
                this.b4();
                this._b0[this._b1] = b3;
                this._b1++;
            }
        }

        internal void A4()
        {
            for (int i = 0; i < this._b1; i++)
            {
                this._b0[i] = null;
            }
            this._b1 = 0;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                A523[] destinationArray = new A523[2 * this._b0.Length];
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

        internal A523 this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

