namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A555
    {
        private A556[] _b0;
        private int _b1;

        internal A555()
        {
            this._b0 = new A556[10];
        }

        internal A555(A556[] b0)
        {
            this._b0 = b0;
            this._b1 = b0.Length;
        }

        internal void A3(A556 b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                A556[] destinationArray = new A556[2 * this._b0.Length];
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

        internal A556 this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

