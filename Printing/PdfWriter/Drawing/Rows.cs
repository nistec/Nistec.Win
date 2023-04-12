namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Reflection;

    public class Rows
    {
        private Row[] _b0 = new Row[10];
        private int _b1 = 0;
        private Table _b2;

        internal Rows(Table table)
        {
            this._b2 = table;
        }

        public void Add(Row row)
        {
            this.b3();
            this._b0[this._b1] = row;
            this._b1++;
            this._b2.A473 = false;
        }

        public int IndexOf(Row row)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (row == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Remove(Row row)
        {
            int index = this.IndexOf(row);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._b1--;
            if (index < this._b1)
            {
                Array.Copy(this._b0, index + 1, this._b0, index, this._b1 - index);
            }
            this._b0[this._b1] = null;
            this._b2.A473 = false;
        }

        private void b3()
        {
            if (this._b1 >= this._b0.Length)
            {
                Row[] destinationArray = new Row[2 * this._b0.Length];
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

        public Row this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

