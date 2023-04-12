namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A565
    {
        private A566[] _b0;
        private int _b1;

        internal A565()
        {
            this._b1 = 0;
            this._b0 = new A566[10];
        }

        internal A565(A565 b2)
        {
            this._b1 = b2._b1;

            this._b0 = new A566[this._b1];
            for (int i = 0; i < this._b1; i++)
            {
                this._b0[i] = b2[i];
            }
        }

        internal void A3(A566 b4)
        {
            this.b5();
            this._b0[this._b1] = b4;
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

        internal void A9(int b3)
        {
            if ((b3 < 0) || (b3 >= this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._b1--;
            if (b3 < this._b1)
            {
                Array.Copy(this._b0, b3 + 1, this._b0, b3, this._b1 - b3);
            }
            this._b0[this._b1] = null;
        }

        private void b5()
        {
            if (this._b1 >= this._b0.Length)
            {
                A566[] destinationArray = new A566[2 * this._b0.Length];
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

        internal A566 this[int b3]
        {
            get
            {
                return this._b0[b3];
            }
        }
    }
}

