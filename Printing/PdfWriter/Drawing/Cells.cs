namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Drawing;
    using System;
    using System.Reflection;

    public class Cells
    {
        private Cell[] _b0 = new Cell[10];
        private int _b1 = 0;
        private Row _b2;

        internal Cells(Row b2)
        {
            this._b2 = b2;
        }

        internal void A3(Cell b3)
        {
            this.b4();
            this._b0[this._b1] = b3;
            this._b2.Table.A473 = false;
            this._b1++;
        }

        public Cell Add(string text)
        {
            return this.Add(text, 1, this._b2.A405);
        }

        public Cell Add(string text, TableStyle style)
        {
            return this.Add(text, 1, style);
        }

        public Cell Add(string linktext, string uri)
        {
            return this.Add(linktext, uri, 1, this._b2.A405);
        }

        public Cell Add(string text, int colspan, TableStyle style)
        {
            b5(colspan, this._b2);
            Cell cell = new Cell(this._b2, this._b2.A474, colspan, new A475(text), style);
            this.A3(cell);
            this._b2.A474 += colspan;
            return cell;
        }

        public Cell Add(string linktext, string uri, TableStyle style)
        {
            return this.Add(linktext, uri, 1, style);
        }

        public Cell Add(string linktext, string uri, int colspan, TableStyle style)
        {
            b5(colspan, this._b2);
            Cell cell = new Cell(this._b2, this._b2.A474, colspan, new A476(linktext, uri), style);
            this.A3(cell);
            this._b2.A474 += colspan;
            return cell;
        }

        public int IndexOf(Cell cell)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (cell == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Remove(Cell cell)
        {
            int index = this.IndexOf(cell);
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
            this._b2.Table.A473 = false;
            if (index < this._b1)
            {
                Array.Copy(this._b0, index + 1, this._b0, index, this._b1 - index);
            }
            this._b0[this._b1] = null;
        }

        private void b4()
        {
            if (this._b1 >= this._b0.Length)
            {
                Cell[] destinationArray = new Cell[2 * this._b0.Length];
                Array.Copy(this._b0, destinationArray, this._b1);
                this._b0 = destinationArray;
            }
        }

        private static void b5(int b6, Row b2)
        {
            if (b6 < 1)
            {
                throw new PdfWriterException("Cell can not span less than one Column.");
            }
            if (((b2.A474 + b6) - 1) >= b2.Table.Columns.Count)
            {
                throw new PdfWriterException("Not enough columns in this row to have a column span of " + b6 + ".");
            }
        }

        public int Count
        {
            get
            {
                return this._b1;
            }
        }

        public Cell this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

