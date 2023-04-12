namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Reflection;

    public class Columns
    {
        private Column[] _b0 = new Column[10];
        private int _b1 = 0;

        internal Columns()
        {
        }

        internal void A3(Column b2)
        {
            this.b3();
            this._b0[this._b1] = b2;
            this._b1++;
        }

        internal void A8(Column b2, int b4)
        {
            if ((b4 < 0) || (b4 > this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this.b3();
            if (b4 < this._b1)
            {
                Array.Copy(this._b0, b4, this._b0, b4 + 1, this._b1 - b4);
            }
            this._b0[b4] = b2;
            this._b1++;
        }

        internal void A9(int b4)
        {
            if ((b4 < 0) || (b4 >= this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._b1--;
            if (b4 < this._b1)
            {
                Array.Copy(this._b0, b4 + 1, this._b0, b4, this._b1 - b4);
            }
            this._b0[this._b1] = null;
        }

        public Column Add(float width)
        {
            Column column = new Column(width);
            this.A3(column);
            return column;
        }

        public int IndexOf(Column column)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (column == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Remove(Column column)
        {
            int index = this.IndexOf(column);
            if (index != -1)
            {
                this.A9(index);
            }
        }

        private void b3()
        {
            if (this._b1 >= this._b0.Length)
            {
                Column[] destinationArray = new Column[2 * this._b0.Length];
                Array.Copy(this._b0, destinationArray, this._b1);
                this._b0 = destinationArray;
            }
        }

        public int Count
        {
            get
            {
                return this._b1;
            }
        }

        public Column this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

