namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A559
    {
        private A560[] _b0 = new A560[10];
        private int _b1;

        internal A559()
        {
        }

        internal void A3(A560 b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                A560[] destinationArray = new A560[2 * this._b0.Length];
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

        internal A560 this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

