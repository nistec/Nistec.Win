namespace MControl.Printing.Pdf.Controls
{
    using System;
    using System.Reflection;

    public class PdfFields
    {
        private PdfField[] _b0 = new PdfField[10];
        private int _b1 = 0;

        internal PdfFields()
        {
        }

        internal void A10(PdfField b3)
        {
            int num = this.A11(b3);
            if (num != -1)
            {
                this.A9(num);
            }
        }

        internal int A11(PdfField b3)
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

        internal void A3(PdfField b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b1++;
        }

        internal void A8(PdfField b3, int b2)
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
                PdfField[] destinationArray = new PdfField[2 * this._b0.Length];
                Array.Copy(this._b0, destinationArray, this._b1);
                this._b0 = destinationArray;
            }
        }

        public PdfField this[int b2]
        {
            get
            {
                return this._b0[b2];
            }
        }

        public int Size
        {
            get
            {
                return this._b1;
            }
        }
    }
}

