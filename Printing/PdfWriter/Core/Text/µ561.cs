namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A561
    {
        private A562[] _b0 = new A562[10];
        private int _b1;

        internal A561()
        {
        }

        internal void A3(A562 b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                A562[] destinationArray = new A562[2 * this._b0.Length];
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

        internal A562 this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

