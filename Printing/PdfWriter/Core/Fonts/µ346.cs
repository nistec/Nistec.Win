namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class A346
    {
        private ArrayList _b0 = new ArrayList();

        internal A346()
        {
        }

        internal bool A3(ushort b2)
        {
            if (!this._b0.Contains(b2))
            {
                this._b0.Add(b2);
                return true;
            }
            return false;
        }

        internal void A4()
        {
            this._b0.Clear();
        }

        internal int A293
        {
            get
            {
                return this._b0.Count;
            }
        }

        internal ushort[] A351
        {
            get
            {
                this._b0.Sort();
                ushort[] numArray = new ushort[this.A293];
                for (int i = 0; i < this.A293; i++)
                {
                    numArray[i] = this[i];
                }
                return numArray;
            }
        }

        internal ushort this[int b1]
        {
            get
            {
                return (ushort) this._b0[b1];
            }
        }
    }
}

