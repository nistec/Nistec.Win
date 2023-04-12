namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A478
    {
        private A526[] _b0;
        private int _b1;

        internal A478()
        {
            this._b1 = 0;
            this._b0 = new A526[10];
        }

        internal A478(A526[] b2)
        {
            this._b1 = b2.Length;
            this._b0 = b2;
        }

        internal void A3(A526 b2)
        {
            this.b4();
            this._b0[this._b1] = b2;
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

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                A526[] destinationArray = new A526[2 * this._b0.Length];
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

        internal A526 this[int b3]
        {
            get
            {
                return this._b0[b3];
            }
        }
    }
}

