namespace MControl.Printing.Pdf.Core.Collection
{
    using MControl.Printing.Pdf;
    using System;
    using System.Reflection;

    internal class A12
    {
        private PdfFont[] _b0 = new PdfFont[10];
        private int _b1;

        internal A12()
        {
        }

        internal void A3(PdfFont b3)
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

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                PdfFont[] destinationArray = new PdfFont[2 * this._b0.Length];
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

        internal PdfFont this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

