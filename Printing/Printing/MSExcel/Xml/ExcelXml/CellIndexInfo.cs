namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class CellIndexInfo
    {
        [CompilerGenerated]
        private int _ColumnIndex;
        [CompilerGenerated]
        private string _ExcelColumnIndex;
        [CompilerGenerated]
        private int _RowIndex;

        internal CellIndexInfo(Cell cell)
        {
            this.ColumnIndex = cell.CellIndex;
            this.RowIndex = cell.ParentRow.RowIndex;
            this.SetExcelIndex();
        }

        private void SetExcelIndex()
        {
            this.ExcelColumnIndex = "";
            int num = (this.ColumnIndex / 0x1a) - 1;
            int num2 = this.ColumnIndex % 0x1a;
            if (num >= 0)
            {
                char ch = (char) (0x41 + num);
                this.ExcelColumnIndex = this.ExcelColumnIndex + ch;
            }
            char ch2 = (char) (0x41 + num2);
            this.ExcelColumnIndex = this.ExcelColumnIndex + ch2;
        }

        public int ColumnIndex
        {
            [CompilerGenerated]
            get
            {
                return this._ColumnIndex;
            }
            //private [CompilerGenerated]
            set
            {
                this._ColumnIndex = value;
            }
        }

        public string ExcelColumnIndex
        {
            [CompilerGenerated]
            get
            {
                return this._ExcelColumnIndex;
            }
            //private [CompilerGenerated]
            set
            {
                this._ExcelColumnIndex = value;
            }
        }

        public int RowIndex
        {
            [CompilerGenerated]
            get
            {
                return this._RowIndex;
            }
            //private [CompilerGenerated]
            set
            {
                this._RowIndex = value;
            }
        }
    }
}

