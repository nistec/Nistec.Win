namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class Range : Styles, IEnumerable<Cell>, IEnumerable
    {
        [CompilerGenerated]
        private bool _Absolute;
        internal Cell CellFrom;
        internal Cell CellTo;
        private string name;
        internal string UnresolvedRangeReference;

        internal Range(string range)
        {
            if (range[0] == '=')
            {
                range = range.Substring(1);
            }
            this.UnresolvedRangeReference = range;
        }

        public Range(Cell cell)
        {
            this.CellFrom = cell;
            this.UnresolvedRangeReference = "";
        }

        public Range(Cell cellFrom, Cell cellTo)
        {
            this.UnresolvedRangeReference = "";
            if (cellTo == null)
            {
                this.CellFrom = cellFrom;
            }
            else
            {
                if (cellFrom.ParentRow.ParentSheet != cellTo.ParentRow.ParentSheet)
                {
                    throw new ArgumentException("cellFrom and cellTo's parent worksheets should be same");
                }
                if (cellFrom == cellTo)
                {
                    this.CellFrom = cellFrom;
                }
                else
                {
                    int rowIndex = cellFrom.ParentRow.RowIndex;
                    int num2 = cellTo.ParentRow.RowIndex;
                    int cellIndex = cellFrom.CellIndex;
                    int num4 = cellTo.CellIndex;
                    if ((rowIndex > num2) || (cellIndex > num4))
                    {
                        this.CellFrom = cellTo;
                        this.CellTo = cellFrom;
                    }
                    else
                    {
                        this.CellFrom = cellFrom;
                        this.CellTo = cellTo;
                    }
                }
            }
        }

        private string AbsoluteReference()
        {
            string str = string.Format(CultureInfo.InvariantCulture, "R{0}C{1}", new object[] { this.CellFrom.ParentRow.RowIndex + 1, this.CellFrom.CellIndex + 1 });
            if (this.CellFrom != null)
            {
                str = str + string.Format(CultureInfo.InvariantCulture, ":R{0}C{1}", new object[] { this.CellTo.ParentRow.RowIndex + 1, this.CellTo.CellIndex + 1 });
            }
            return str;
        }

        public void AutoFilter()
        {
            this.CellFrom.ParentRow.ParentSheet.AutoFilter = true;
            this.CellFrom.GetParentBook().AddNamedRange(this, "_FilterDatabase", this.CellFrom.ParentRow.ParentSheet);
        }

        public bool Contains(Cell cell)
        {
            if (this.CellFrom == null)
            {
                return false;
            }
            if (this.CellFrom.ParentRow.ParentSheet != cell.ParentRow.ParentSheet)
            {
                return false;
            }
            if (this.CellTo == null)
            {
                return (this.CellFrom == cell);
            }
            int rowIndex = this.CellFrom.ParentRow.RowIndex;
            int num2 = this.CellTo.ParentRow.RowIndex;
            int cellIndex = this.CellFrom.CellIndex;
            int num4 = this.CellTo.CellIndex;
            return ((((cell.ParentRow.RowIndex >= rowIndex) && (cell.ParentRow.RowIndex <= num2)) && (cell.CellIndex >= cellIndex)) && (cell.CellIndex <= num4));
        }

        internal override Cell FirstCell()
        {
            return this.CellFrom;
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            _GetEnumerator _d = new _GetEnumerator(0);
            _d.__this = this;
            return _d;
        }

        internal override Workbook GetParentBook()
        {
            return null;
        }

        internal override void IterateAndApply(Styles.IterateFunction applyStyleFunction)
        {
            if (this.CellFrom != null)
            {
                if (this.CellTo == null)
                {
                    applyStyleFunction(this.CellFrom);
                }
                else
                {
                    int rowIndex = this.CellFrom.ParentRow.RowIndex;
                    int num2 = this.CellTo.ParentRow.RowIndex;
                    int cellIndex = this.CellFrom.CellIndex;
                    int num4 = this.CellTo.CellIndex;
                    Worksheet parentSheet = this.CellFrom.ParentRow.ParentSheet;
                    for (int i = rowIndex; i <= num2; i++)
                    {
                        for (int j = cellIndex; j <= num4; j++)
                        {
                            applyStyleFunction(parentSheet[j, i]);
                        }
                    }
                }
            }
        }

        internal bool Match(Nistec.Printing.ExcelXml.Range range)
        {
            return ((range.CellFrom == this.CellFrom) && (range.CellTo == this.CellTo));
        }

        public bool Merge()
        {
            if (!this.CellFrom.MergeStart)
            {
                bool rangeHasMergedCells = false;
                this.IterateAndApply(delegate (Cell cell) {
                    rangeHasMergedCells = cell.MergeStart;
                });
                if (rangeHasMergedCells)
                {
                    return false;
                }
                this.CellFrom.ParentRow.ParentSheet._MergedCells.Add(this);
                this.CellFrom.MergeStart = true;
            }
            return true;
        }

        internal string NamedRangeReference(bool sheetReference)
        {
            if (this.CellFrom == null)
            {
                return this.UnresolvedRangeReference;
            }
            string str = "";
            if (sheetReference)
            {
                str = "'" + this.CellFrom.ParentRow.ParentSheet.Name + "'!";
            }
            return (str + this.AbsoluteReference());
        }

        internal void ParseUnresolvedReference(Cell cell)
        {
            if (!string.IsNullOrEmpty(this.UnresolvedRangeReference))
            {
                System.Text.RegularExpressions.Match match;
                ParseArgumentType argumentType = FormulaParser.GetArgumentType(this.UnresolvedRangeReference, out match);
                Nistec.Printing.ExcelXml.Range range = null;
                if (cell == null)
                {
                    throw new ArgumentNullException("cell");
                }
                if (FormulaParser.ParseRange(cell, match, out range, argumentType == ParseArgumentType.AbsoluteRange))
                {
                    this.UnresolvedRangeReference = "";
                    this.CellFrom = range.CellFrom;
                    this.CellTo = range.CellTo;
                }
            }
        }

        internal string RangeReference(Cell cell)
        {
            string str;
            if (this.CellFrom == null)
            {
                return this.UnresolvedRangeReference;
            }
            if (this.CellFrom.ParentRow == null)
            {
                return "#N/A";
            }
            if ((this.CellTo != null) && (this.CellTo.ParentRow == null))
            {
                return "#N/A";
            }
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            if (this.Absolute)
            {
                str = this.AbsoluteReference();
            }
            else if (this.CellTo != null)
            {
                str = string.Format(CultureInfo.InvariantCulture, "R[{0}]C[{1}]:R[{2}]C[{3}]", new object[] { this.CellFrom.ParentRow.RowIndex - cell.ParentRow.RowIndex, this.CellFrom.CellIndex - cell.CellIndex, this.CellTo.ParentRow.RowIndex - cell.ParentRow.RowIndex, this.CellTo.CellIndex - cell.CellIndex });
            }
            else
            {
                str = string.Format(CultureInfo.InvariantCulture, "R[{0}]C[{1}]", new object[] { this.CellFrom.ParentRow.RowIndex - cell.ParentRow.RowIndex, this.CellFrom.CellIndex - cell.CellIndex });
            }
            string text = "";
            if (this.CellFrom.ParentRow.ParentSheet != cell.ParentRow.ParentSheet)
            {
                text = this.CellFrom.ParentRow.ParentSheet.Name;
                if (this.CellFrom.GetParentBook() != cell.GetParentBook())
                {
                    throw new ArgumentException("External workbook references are not supported");
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                str = "'" + text + "'!" + str;
            }
            return str;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Unmerge()
        {
            if (this.CellFrom.MergeStart)
            {
                this.CellFrom.ParentRow.ParentSheet._MergedCells.Remove(this);
                this.CellFrom.MergeStart = false;
            }
        }

        public bool Absolute
        {
            [CompilerGenerated]
            get
            {
                return this._Absolute;
            }
            [CompilerGenerated]
            set
            {
                this._Absolute = value;
            }
        }

        public int ColumnCount
        {
            get
            {
                if (this.CellFrom == null)
                {
                    return 0;
                }
                if (this.CellTo == null)
                {
                    return 1;
                }
                int cellIndex = this.CellFrom.CellIndex;
                return ((this.CellTo.CellIndex - cellIndex) + 1);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    if ((string.IsNullOrEmpty(value) || (value == "Print_Titles")) || (value == "_FilterDatabase"))
                    {
                        throw new ArgumentException("name");
                    }
                    this.CellFrom.GetParentBook().AddNamedRange(this, this.name);
                    this.name = value;
                }
            }
        }

        public int RowCount
        {
            get
            {
                if (this.CellFrom == null)
                {
                    return 0;
                }
                if (this.CellTo == null)
                {
                    return 1;
                }
                int rowIndex = this.CellFrom.ParentRow.RowIndex;
                return ((this.CellTo.ParentRow.RowIndex - rowIndex) + 1);
            }
        }

        [CompilerGenerated]
        private sealed class _GetEnumerator : IEnumerator<Cell>, IEnumerator, IDisposable
        {
            private int __state;
            private Cell __current;
            public Nistec.Printing.ExcelXml.Range __this;
            public int _cellIndexFrom;//>5__10;
            public int _cellIndexTo;//>5__11;
            public int _i;//>5__13;
            public int _j;//>5__14;
            public int _rowFrom;//>5__e;
            public int _rowTo;//>5__f;
            public Worksheet _ws;//>5__12;

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
                        if (this.__this.CellFrom == null)
                        {
                            break;
                        }
                        if (this.__this.CellTo != null)
                        {
                            this._rowFrom = this.__this.CellFrom.ParentRow.RowIndex;
                            this._rowTo = this.__this.CellTo.ParentRow.RowIndex;
                            this._cellIndexFrom = this.__this.CellFrom.CellIndex;
                            this._cellIndexTo = this.__this.CellTo.CellIndex;
                            this._ws = this.__this.CellFrom.ParentRow.ParentSheet;
                            this._i = this._rowFrom;
                            while (this._i <= this._rowTo)
                            {
                                this._j = this._cellIndexFrom;
                                while (this._j <= this._cellIndexTo)
                                {
                                    this.__current = this._ws[this._j, this._i];
                                    this.__state = 2;
                                    return true;
                                //Label_014D:
                                //    this.__state = -1;
                                //    this._j++;
                                }
                                this._i++;
                            }
                            break;
                        }
                        this.__current = this.__this.CellFrom;
                        this.__state = 1;
                        return true;

                    case 1:
                        this.__state = -1;
                        break;

                    case 2:
                        //goto Label_014D;
                        this.__state = -1;
                        this._j++;
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

