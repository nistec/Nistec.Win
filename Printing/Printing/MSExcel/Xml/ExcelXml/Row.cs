namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Row : Styles, IEnumerable<Cell>, IEnumerable
    {
        internal List<Cell> _Cells;
        [CompilerGenerated]
        private double _Height;
        [CompilerGenerated]
        private bool _Hidden;
        internal Worksheet ParentSheet;
        internal int RowIndex;

        internal Row(Worksheet parent, int row)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this._Cells = new List<Cell>();
            this.ParentSheet = parent;
            this.Height = 0.0;
            this.RowIndex = row;
            if (parent.Style != null)
            {
                base.Style = parent.Style;
            }
        }

        public Cell AddCell()
        {
            return this[this._Cells.Count];
        }

        private Cell Cells(int colIndex)
        {
            if (colIndex < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if ((colIndex + 1) > this._Cells.Count)
            {
                for (int i = this._Cells.Count; i <= colIndex; i++)
                {
                    this._Cells.Add(new Cell(this, i));
                }
            }
            this.ParentSheet.maxColumnAddressed = Math.Max(colIndex, this.ParentSheet.maxColumnAddressed);
            return this._Cells[colIndex];
        }

        public void Delete()
        {
            this.ParentSheet.DeleteRow(this);
        }

        public void DeleteCell(int index)
        {
            this.DeleteCells(index, 1, true);
        }

        public void DeleteCell(Cell cell)
        {
            if (cell != null)
            {
                this.DeleteCells(this._Cells.FindIndex(delegate (Cell r) {
                    return r == cell;
                }), 1, true);
            }
        }

        public void DeleteCell(int index, bool cascade)
        {
            this.DeleteCells(index, 1, cascade);
        }

        public void DeleteCell(Cell cell, bool cascade)
        {
            if (cell != null)
            {
                this.DeleteCells(this._Cells.FindIndex(delegate (Cell r) {
                    return r == cell;
                }), 1, cascade);
            }
        }

        public void DeleteCells(int index, int numberOfCells)
        {
            this.DeleteCells(index, numberOfCells, true);
        }

        public void DeleteCells(Cell cell, int numberOfCells)
        {
            if (cell != null)
            {
                this.DeleteCells(this._Cells.FindIndex(delegate (Cell r) {
                    return r == cell;
                }), numberOfCells, true);
            }
        }

        public void DeleteCells(int index, int numberOfCells, bool cascade)
        {
            if ((numberOfCells >= 0) && ((index >= 0) && (index < this._Cells.Count)))
            {
                if ((index + numberOfCells) > this._Cells.Count)
                {
                    numberOfCells = this._Cells.Count - index;
                }
                for (int i = index; i < (index + numberOfCells); i++)
                {
                    this._Cells[index].Empty(!cascade);
                    if (cascade)
                    {
                        this._Cells.RemoveAt(index);
                    }
                }
                if (cascade)
                {
                    this.ResetCellNumbersFrom(index);
                }
            }
        }

        public void DeleteCells(Cell cell, int numberOfCells, bool cascade)
        {
            if (cell != null)
            {
                this.DeleteCells(this._Cells.FindIndex(delegate (Cell r) {
                    return r == cell;
                }), numberOfCells, cascade);
            }
        }

        internal void Empty()
        {
            this.ParentSheet = null;
            this._Cells.Clear();
            this._Cells = null;
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("Row");
            if ((!string.IsNullOrEmpty(base.StyleID) && (this.ParentSheet.StyleID != base.StyleID)) && (base.StyleID != "Default"))
            {
                writer.WriteAttributeString("ss", "StyleID", null, base.StyleID);
            }
            if (this.Height != 0.0)
            {
                writer.WriteAttributeString("ss", "AutoFitHeight", null, "0");
                writer.WriteAttributeString("ss", "Height", null, this.Height.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Hidden)
            {
                writer.WriteAttributeString("ss", "Hidden", null, "1");
            }
            bool printIndex = false;
            foreach (Cell cell in this._Cells)
            {
                if (!(!cell.IsEmpty() || cell.MergeStart))
                {
                    printIndex = true;
                }
                else
                {
                    cell.Write(writer, printIndex);
                    printIndex = false;
                }
            }
            writer.WriteEndElement();
        }

        internal override Cell FirstCell()
        {
            return null;
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            _GetEnumerator d__ = new _GetEnumerator (0);
            d__.__this = this;
            return d__;
        }

        internal override Workbook GetParentBook()
        {
            return this.ParentSheet.ParentBook;
        }

        //public Cell InsertCellAfter(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.AddCell();
        //    }
        //    if (index >= (this._Cells.Count - 1))
        //    {
        //        return this[index + 1];
        //    }
        //    this.InsertCellsAfter(index, 1);
        //    return this._Cells[index];
        //}

        //public Cell InsertCellAfter(Cell cell)
        //{
        //    return this.InsertCellAfter(this._Cells.FindIndex(delegate (Cell r) {
        //        return r == cell;
        //    }));
        //}

        //public Cell InsertCellBefore(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.AddCell();
        //    }
        //    if (index >= this._Cells.Count)
        //    {
        //        return this[index];
        //    }
        //    this.InsertCellsBefore(index, 1);
        //    return this._Cells[index];
        //}

        //public Cell InsertCellBefore(Cell cell)
        //{
        //    return this.InsertCellBefore(this._Cells.FindIndex(delegate (Cell r) {
        //        return r == cell;
        //    }));
        //}

        //public void InsertCellsAfter(int index, int cells)
        //{
        //    if (((cells >= 0) && (index >= 0)) && (index < (this._Cells.Count - 1)))
        //    {
        //        for (int i = index; i < (index + cells); i++)
        //        {
        //            Cell item = new Cell(this, index);
        //            this._Cells.Insert(index + 1, item);
        //        }
        //        this.ResetCellNumbersFrom(index);
        //    }
        //}

        //public void InsertCellsAfter(Cell cell, int cells)
        //{
        //    if (cell != null)
        //    {
        //        this.InsertCellsAfter(this._Cells.FindIndex(delegate (Cell r) {
        //            return r == cell;
        //        }), cells);
        //    }
        //}

        //public void InsertCellsBefore(int index, int cells)
        //{
        //    if (((cells >= 0) && (index >= 0)) && (index < this._Cells.Count))
        //    {
        //        for (int i = index; i < (index + cells); i++)
        //        {
        //            Cell item = new Cell(this, index);
        //            this._Cells.Insert(index, item);
        //        }
        //        this.ResetCellNumbersFrom(index);
        //    }
        //}

        //public void InsertCellsBefore(Cell cell, int cells)
        //{
        //    this.InsertCellsBefore(this._Cells.FindIndex(delegate (Cell r) {
        //        return r == cell;
        //    }), cells);
        //}


        public void InsertCells(int index, int numberOfCells, bool insertAfter)
        {
            int offset = insertAfter ? 1 : 0;

            if (((numberOfCells >= 0) && (index >= 0)) && (index < (this._Cells.Count - offset)))
            {
                for (int i = index; i < (index + numberOfCells); i++)
                {
                    Cell item = new Cell(this, index);
                    this._Cells.Insert(index + offset, item);
                }
                this.ResetCellNumbersFrom(index);
            }

        }
        public Cell InsertCell(int index, bool insertAfter)
        {
            int offset = insertAfter ? 1 : 0;
            if (index < 0)
            {
                return this.AddCell();
            }
            if (index >= (this._Cells.Count - offset))
            {
                return this[index + offset];
            }
            this.InsertCells(index, 1,insertAfter);
            return this._Cells[index];
        }
        public Cell InsertCell(Cell cell, bool insertAfter)
        {
            return this.InsertCell(this._Cells.FindIndex(delegate(Cell r)
            {
                return r == cell;
            }),insertAfter);
        }



        internal override void IterateAndApply(Styles.IterateFunction ifFunc)
        {
        }

        internal void ResetCellNumbersFrom(int index)
        {
            for (int i = index; i < this._Cells.Count; i++)
            {
                this._Cells[i].CellIndex = i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int CellCount
        {
            get
            {
                return this._Cells.Count;
            }
        }

        public double Height
        {
            [CompilerGenerated]
            get
            {
                return this._Height;
            }
            [CompilerGenerated]
            set
            {
                this._Height = value;
            }
        }

        public bool Hidden
        {
            [CompilerGenerated]
            get
            {
                return this._Hidden;
            }
            [CompilerGenerated]
            set
            {
                this._Hidden = value;
            }
        }

        public Cell this[int colIndex]
        {
            get
            {
                return this.Cells(colIndex);
            }
        }

        [CompilerGenerated]
        private sealed class _GetEnumerator : IEnumerator<Cell>, IEnumerator, IDisposable
        {
            private int __state;
            private Cell __current;
            public Row __this;
            public int __1;

            [DebuggerHidden]
            public _GetEnumerator(int __state)
            {
                this.__state = __state;
            }

            public bool MoveNext()
            {
                switch (this.__state)
                {
                    case 0:
                        this.__state = -1;
                        this.__1 = 0;
                        while (this.__1 <= this.__this.ParentSheet.maxColumnAddressed)
                        {
                            this.__current = this.__this[this.__1];
                            this.__state = 1;
                            return true;
                        //Label_0051:
                        //    this.__state = -1;
                        //    this.__1++;
                        }
                        break;

                    case 1:
                        //goto Label_0051;
                        this.__state = -1;
                        this.__1++;
                        break;
                }
                return false;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }

            Cell IEnumerator<Cell>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.__current;
                }
            }
        }
    }
}

