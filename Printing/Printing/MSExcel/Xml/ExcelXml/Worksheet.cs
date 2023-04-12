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
    using System.Data;

    public class Worksheet : Styles, IEnumerable<Cell>, IEnumerable
    {
        private List<Column> _Columns;
        internal List<Nistec.Printing.ExcelXml.Range> _MergedCells;
        internal List<Row> _Rows;
        [CompilerGenerated]
        private int _FreezeLeftColumns;
        [CompilerGenerated]
        private int _FreezeTopRows;
        [CompilerGenerated]
        private Nistec.Printing.ExcelXml.PrintOptions _PrintOptions;
        internal bool AutoFilter;
        internal int maxColumnAddressed;
        internal Workbook ParentBook;
        private string sheetName;

        private int _ExpandedColumnCount;

        public int ExpandedColumnCount
        {
            get { return _ExpandedColumnCount; }
        }
        private int _ExpandedRowCount;

        public int ExpandedRowCount
        {
            get { return _ExpandedRowCount; }
        }


        internal Worksheet(Workbook parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.ParentBook = parent;
            this.PrintOptions = new Nistec.Printing.ExcelXml.PrintOptions();
            this.PrintOptions.Layout = PageLayout.None;
            this.PrintOptions.Orientation = PageOrientation.None;
            this._Rows = new List<Row>();
            this._Columns = new List<Column>();
            this._MergedCells = new List<Nistec.Printing.ExcelXml.Range>();
            this.PrintOptions.FitHeight = 1;
            this.PrintOptions.FitWidth = 1;
            this.PrintOptions.Scale = 100;
            this.PrintOptions.ResetMargins();
        }

        public DataTable GetTableSchema(bool firstRowHeader)
        {
            try
            {
                DataTable dt = new DataTable(this.Name);
                if (firstRowHeader)
                {
                    for (int i = 0; i < _Rows[0].CellCount; i++)
                    {
                        dt.Columns.Add(string.Format("{0}", _Rows[0][i].Value));
                    }

                }
                else
                {
                    int i = 0;
                    foreach (Column col in _Columns)
                    {
                        dt.Columns.Add(string.Format("F{0}", ++i), col.Style.GetFormatType());
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when creating TableSchema, "+ex.Message);
            }
        }
 
        //public DataTable GetDataTable(bool firstRowHeader)
        //{

        //    int count = this.ExpandedColumnCount;// _Rows[0].CellCount;
            
        //    DataTable dt = GetTableSchema(firstRowHeader);

        //    int start = firstRowHeader ? 1 : 0;

        //    for (int r = start; r < _Rows.Count; r++)
        //    {
        //        object[] o = new object[count];
        //        int i = 0;
        //        Row row = _Rows[r];
        //        foreach (Cell cell in row._Cells)
        //        {
        //            o[i] = cell.Value;
        //            i++;
        //        }
        //        if (i <= 0)
        //            continue;
        //        for(int j= i-1;j< count;j++)
        //            o[j] = null;

        //        dt.Rows.Add(o);

        //    }

        //    return dt;
        //}

        public DataTable GetDataTable(bool firstRowHeader,uint maxRows)
        {

            int count = _Rows[0].ParentSheet._Columns.Count;// _Rows[0].CellCount;
            int rows =maxRows==0? _Rows.Count: Math.Min((int)maxRows, _Rows.Count);
            DataTable dt = GetTableSchema(firstRowHeader).Copy();

            int start = firstRowHeader ? 1 : 0;

            for (int r = start; r < rows; r++)
            {
                object[] o = new object[count];
                for (int c = 0; c < count; c++)
                {
                    o[c] = _Rows[r][c].Value;
                }
                dt.Rows.Add(o);

            }

            return dt;
        }

        public void AddNamedRange(Nistec.Printing.ExcelXml.Range range, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if ((name == "Print_Titles") || (name == "_FilterDatabase"))
            {
                throw new ArgumentException("name must not be equal to Print_Titles or _FilterDatabase");
            }
            this.GetParentBook().AddNamedRange(range, name, this);
        }

        public Row AddRow()
        {
            return this.GetRowByIndex(this._Rows.Count);//[this._Rows.Count];
        }

        internal Cell Cells(int rowIndex, int colIndex)
        {
            int num;
            if (colIndex < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if (rowIndex < 0)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((rowIndex + 1) > this._Rows.Count)
            {
                for (num = this._Rows.Count; num <= rowIndex; num++)
                {
                    this._Rows.Add(new Row(this, num));
                }
            }
            if ((colIndex + 1) > this._Rows[rowIndex]._Cells.Count)
            {
                for (num = this._Rows[rowIndex]._Cells.Count; num <= colIndex; num++)
                {
                    this._Rows[rowIndex]._Cells.Add(new Cell(this._Rows[rowIndex], num));
                }
            }
            this.maxColumnAddressed = Math.Max(colIndex, this.maxColumnAddressed);
            return this._Rows[rowIndex]._Cells[colIndex];
        }

        public Column Columns(int colIndex)
        {
            if (colIndex < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if ((colIndex + 1) > this._Columns.Count)
            {
                for (int i = this._Columns.Count; i <= colIndex; i++)
                {
                    this._Columns.Add(new Column(this));
                }
            }
            return this._Columns[colIndex];
        }

        public void Delete()
        {
            this.GetParentBook().DeleteSheet(this);
        }

        public void DeleteColumn(int index)
        {
            this.DeleteColumns(index, 1, true);
        }

        public void DeleteColumn(int index, bool cascade)
        {
            this.DeleteColumns(index, 1, cascade);
        }

        public void DeleteColumns(int index, int numberOfColumns)
        {
            this.DeleteColumns(index, numberOfColumns, true);
        }

        public void DeleteColumns(int index, int numberOfColumns, bool cascade)
        {
            if (index >= 0)
            {
                if (cascade && (index < this._Columns.Count))
                {
                    for (int i = index; i < (index + numberOfColumns); i++)
                    {
                        if (index >= this._Columns.Count)
                        {
                            break;
                        }
                        this._Columns.RemoveAt(index);
                    }
                }
                if (index <= this.maxColumnAddressed)
                {
                    foreach (Row row in this._Rows)
                    {
                        if (index < row._Cells.Count)
                        {
                            row.DeleteCells(index, numberOfColumns, cascade);
                        }
                    }
                }
            }
        }

        public void DeleteRow(int index)
        {
            this.DeleteRows(index, 1);
        }

        public void DeleteRow(Row row)
        {
            if (row != null)
            {
                this.DeleteRow(this._Rows.FindIndex(delegate (Row r) {
                    return r == row;
                }));
            }
        }

        public void DeleteRow(int index, bool cascade)
        {
            if (cascade)
            {
                this.DeleteRow(index);
            }
            else if ((index >= 0) && (index < this._Rows.Count))
            {
                foreach (Cell cell in this._Rows[index]._Cells)
                {
                    cell.Empty();
                }
            }
        }

        public void DeleteRow(Row row, bool cascade)
        {
            if (row != null)
            {
                this.DeleteRow(this._Rows.FindIndex(delegate (Row r) {
                    return r == row;
                }), cascade);
            }
        }

        public void DeleteRows(int index, int numberOfRows)
        {
            if ((numberOfRows >= 0) && ((index >= 0) && (index < this._Rows.Count)))
            {
                if ((index + numberOfRows) > this._Rows.Count)
                {
                    numberOfRows = this._Rows.Count - index;
                }
                for (int i = index; i < (index + numberOfRows); i++)
                {
                    this._Rows[index].Empty();
                    this._Rows.RemoveAt(index);
                }
                this.ResetRowNumbersFrom(index);
            }
        }

        public void DeleteRows(Row row, int numberOfRows)
        {
            if (row != null)
            {
                this.DeleteRows(this._Rows.FindIndex(delegate (Row r) {
                    return r == row;
                }), numberOfRows);
            }
        }

        public void DeleteRows(int index, int numberOfRows, bool cascade)
        {
            if (cascade)
            {
                this.DeleteRows(index, numberOfRows);
            }
            else if ((index >= 0) && (index < this._Rows.Count))
            {
                if ((index + numberOfRows) > this._Rows.Count)
                {
                    numberOfRows = this._Rows.Count - index;
                }
                for (int i = index; i < (index + numberOfRows); i++)
                {
                    foreach (Cell cell in this._Rows[i]._Cells)
                    {
                        cell.Empty();
                    }
                }
            }
        }

        public void DeleteRows(Row row, int numberOfRows, bool cascade)
        {
            if (row != null)
            {
                this.DeleteRows(this._Rows.FindIndex(delegate (Row r) {
                    return r == row;
                }), numberOfRows, cascade);
            }
        }

        internal void Write(XmlWriter writer)
        {
            _ExpandedColumnCount = _Columns.Count;
            _ExpandedRowCount = _Rows.Count;

            writer.WriteStartElement("Worksheet");
            writer.WriteAttributeString("ss", "Name", null, this.Name);
            this.ParentBook.WriteNamedRanges(writer, this);
            writer.WriteStartElement("Table");

            writer.WriteAttributeString("ss", "ExpandedColumnCount", null, _ExpandedColumnCount.ToString());
            writer.WriteAttributeString("ss", "ExpandedRowCount", null, _ExpandedRowCount.ToString());

            writer.WriteAttributeString("ss", "FullColumns", null, "1");
            writer.WriteAttributeString("ss", "FullRows", null, "1");
            if (!(string.IsNullOrEmpty(base.StyleID) || !(base.StyleID != "Default")))
            {
                writer.WriteAttributeString("ss", "StyleID", null, base.StyleID);
            }
            foreach (Column column in this._Columns)
            {
                column.Write(writer);
            }
            foreach (Row row in this._Rows)
            {
                row.Write(writer);
            }
            writer.WriteEndElement();
            this.WriteOptions(writer);
            if (this.AutoFilter)
            {
                string autoFilterRange = this.GetParentBook().GetAutoFilterRange(this);
                writer.WriteStartElement("", "AutoFilter", "urn:schemas-microsoft-com:office:excel");
                writer.WriteAttributeString("", "Range", null, autoFilterRange);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteOptions(XmlWriter writer)
        {
            writer.WriteStartElement("", "WorksheetOptions", "urn:schemas-microsoft-com:office:excel");
            this.PrintOptions.Write(writer);
            writer.WriteElementString("Selected", "");
            if ((this.FreezeLeftColumns > 0) || (this.FreezeTopRows > 0))
            {
                writer.WriteElementString("FreezePanes", "");
                writer.WriteElementString("FrozenNoSplit", "");
                if (this.FreezeTopRows > 0)
                {
                    writer.WriteElementString("SplitHorizontal", this.FreezeTopRows.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("TopRowBottomPane", this.FreezeTopRows.ToString(CultureInfo.InvariantCulture));
                }
                else if (this.FreezeLeftColumns > 0)
                {
                    writer.WriteElementString("SplitVertical", this.FreezeLeftColumns.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("LeftColumnRightPane", this.FreezeLeftColumns.ToString(CultureInfo.InvariantCulture));
                }
                this.WritePanes(writer);
            }
            writer.WriteEndElement();
        }

        internal override Cell FirstCell()
        {
            return null;
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            _GetEnumerator d__ = new _GetEnumerator(0);
            d__.__this = this;
            return d__;
        }

        internal override Workbook GetParentBook()
        {
            return this.ParentBook;
        }

        internal Row GetRowByIndex(int rowIndex)
        {
            if (rowIndex < 0)
            {
                throw new ArgumentOutOfRangeException("rowIndex");
            }
            if ((rowIndex + 1) > this._Rows.Count)
            {
                for (int i = this._Rows.Count; i <= rowIndex; i++)
                {
                    this._Rows.Add(new Row(this, i));
                }
            }
            return this._Rows[rowIndex];
        }

        internal void Read(XmlReader reader)
        {
            Read(reader, 0);
        }

        internal void Read(XmlReader reader,uint maxRows)
        {
            //reader.MoveToFirstAttribute();

            while (reader.MoveToNextAttribute())
            {
                if (reader.HasValue)
                {
                    switch (reader.LocalName)
                    {
                        case "Name":
                            this.Name = reader.Value;
                            break;
                        case "StyleID":
                            base.Style = this.ParentBook.GetStyleByID(reader.Value);
                            break;
                    }
                }
            }
            while (reader.Read() && (!(reader.Name == "Worksheet") || (reader.NodeType != XmlNodeType.EndElement)))
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;

                    switch(name)
                    {
                        case "Table":
                            this.ReadTable(reader, maxRows);
                            break;
                        case "WorksheetOptions":
                            this.ReadOptions(reader);
                            break;
                        case "Names":
                            Workbook.ReadNamedRanges(reader, this.GetParentBook(), this);
                            break;
                    }
                }
            }
        }

        private void ReadCell(XmlReader reader, Row row)
        {
            bool isEmptyElement = reader.IsEmptyElement;
            int count = row._Cells.Count;
            int rowIndex = 0;
            int colIndex = 0;
            XmlStyle styleByID = null;
            string formula = "";

            
            //reader.MoveToFirstAttribute();
            while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
            {
                if (!reader.HasValue)
                    continue;
                switch (reader.LocalName)
                {
                    case "Index":
                        int.TryParse(reader.Value, out count);// item.Value.ParseToInt<int>(out count);
                        count--;
                        break;
                    case "StyleID":
                        styleByID = this.ParentBook.GetStyleByID(reader.Value);
                        break;
                    case "Formula":
                        formula = reader.Value;
                        break;
                    case "MergeAcross":
                        int.TryParse(reader.Value, out colIndex);// item.Value.ParseToInt<int>(out num3);
                        break;
                    case "MergeDown":
                        int.TryParse(reader.Value, out rowIndex);// item.Value.ParseToInt<int>(out variable);
                        break;
                }
            }
            Cell cell = this.Cells(row.RowIndex, count);
            if (styleByID != null)
            {
                cell.Style = styleByID;
            }
            //if (!string.IsNullOrEmpty(formula))
            //{
            //    FormulaParser.Parse(cell, formula);
            //}
            if (!isEmptyElement)
            {
                if ((rowIndex > 0) || (colIndex > 0))
                {
                    cell.MergeStart = true;
                    Nistec.Printing.ExcelXml.Range range = new Nistec.Printing.ExcelXml.Range(cell, this.Cells(row.RowIndex + rowIndex, count + colIndex));
                    this._MergedCells.Add(range);
                }
                while (reader.Read() && (!(reader.Name == "Cell") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Data")
                        {
                            ReadCellData(reader, cell, formula);
                        }
                        else if (reader.Name == "Comment")
                        {
                            ReadCellComment(reader, cell);
                        }
                    }
                }
            }
        }

        internal static object GetCellValue(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return "";

            string singleAttribute = reader.GetAttribute("ss:Type");
            reader.Read();
            if (reader.NodeType != XmlNodeType.Text)
                return "";
            if (reader.Value == null)
                return "";

            switch (singleAttribute)
            {
                case "Number":
                    decimal num;
                    if (decimal.TryParse(reader.Value.ToString(), out num))
                        return num;
                    break;
                case "DateTime":
                    DateTime time;
                    if (DateTime.TryParseExact(reader.Value, @"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                        return time;
                    break;
                case "Boolean":
                    return reader.Value == "1";
            }


            return reader.Value; 
        }

        internal static void ReadCellComment(XmlReader reader, Cell cell)
        {
            reader.Read();
            if (reader.LocalName == "Data")
            {
                string text = reader.ReadInnerXml();
                if (!string.IsNullOrEmpty(text))
                {
                    cell.Comment = text;
                }
            }
        }
        internal static void ReadCellData(XmlReader reader, Cell cell, string formulaText)
        {
            if (reader.IsEmptyElement)
            {
                cell.Value = "";
            }
            else if (!string.IsNullOrEmpty(formulaText))
            {
                if (formulaText[0] != '=')
                {
                    cell.Value = formulaText;
                    cell.Content = ContentType.UnresolvedValue;
                }
                formulaText = formulaText.Substring(1);
                Formula formula = new Formula();
                formula.Value = GetCellValue(reader); 
                FormulaParser.ParseFormula(cell, formula, formulaText);
                if (string.IsNullOrEmpty(formula.Function) && (formula.parameters[0].ParameterType == ParameterType.String))
                {
                    cell.Value = formulaText;
                    cell.Content = ContentType.UnresolvedValue;
                }
                //else if (formula.parameters[0].ParameterType == ParameterType.Formula)
                //{
                //    cell.Value = formula.parameters[0].Value as Formula;
                //    cell.Content = ContentType.Formula;
                //}
                else
                {
                    cell.Value = formula;
                    //cell.Content = ContentType.Formula;
                }
            }
            else
            {
                cell.Value = GetCellValue(reader); 

            }

        }
        internal static void ReadCellData(XmlReader reader, Cell cell)
        {
            if (reader.IsEmptyElement)
            {
                cell.Value = "";
            }
            else
            {
                //XmlReaderAttributeItem singleAttribute = reader.GetSingleAttribute("Type");
                //string attr = reader.GetAttribute(0);

                string singleAttribute = reader.GetAttribute("ss:Type");
                if (singleAttribute != null)
                {
                    reader.Read();
                    if (reader.NodeType != XmlNodeType.Text)
                    {
                        cell.Value = "";
                    }
                    else
                    {
                        string str = singleAttribute;
                        if (str != null)
                        {

                            switch (str)
                            {
                                case "Number":
                                    decimal num;
                                    if (reader.Value != null && decimal.TryParse(reader.Value.ToString(), out num))//(reader.Value.ParseToInt<decimal>(out num))
                                    {
                                        cell.Value = num;
                                    }
                                    else
                                    {
                                        cell.Value = reader.Value;
                                    }
                                    break;
                                case "DateTime":
                                    DateTime time;
                                    if (reader.Value != null && DateTime.TryParseExact(reader.Value, @"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                                    {
                                        cell.Value = time;
                                    }
                                    else
                                    {
                                        cell.Value = reader.Value;
                                    }
                                    break;
                                case "Boolean":
                                    cell.Value = reader.Value == "1";
                                    break;
                                default://String
                                    cell.Value = reader.Value;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void ReadOptions(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read() && (!(reader.Name == "WorksheetOptions") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    int num;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        string name = reader.Name;
                        if (name != null)
                        {
                            switch(name)
                            {
                                case "FitToPage":
                                    this.PrintOptions.FitToPage = true; 
                                    break;
                                case "Print":
                                    this.ReadPrintOptions(reader);
                                    break;
                                case "SplitHorizontal":
                                    if (reader.IsEmptyElement)
                                    {
                                        continue;
                                    }
                                    reader.Read();
                                    if ((reader.NodeType == XmlNodeType.Text) && int.TryParse(reader.Value, out num))//reader.Value.ParseToInt<int>(out num))
                                    {
                                        this.FreezeTopRows = num;
                                    }
                                    break;
                                case "SplitVertical":
                                    if (reader.IsEmptyElement)
                                    {
                                        continue;
                                    }
                                    reader.Read();
                                    if ((reader.NodeType == XmlNodeType.Text) && int.TryParse(reader.Value, out num))//reader.Value.ParseToInt<int>(out num))
                                    {
                                        this.FreezeLeftColumns = num;
                                    }
                                    break;
                                case "PageSetup":
                                    this.ReadPageSetup(reader);
                                    break;
                            }
                         }
                    }
                }
            }
        }

        private void ReadPageSetup(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return;

            while (reader.Read() && (!(reader.Name == "PageSetup") || (reader.NodeType != XmlNodeType.EndElement)))
            {
                string item;// XmlReaderAttributeItem item;
                double num;
                string name;
                if (reader.NodeType == XmlNodeType.Element)
                {
                    name = reader.Name;
                    if (name != null)
                    {
                        switch (name)
                        {
                            case "Header":
                                item = reader.GetAttribute("ss:Margin");//, true);
                                if ((item != null) && double.TryParse(item, out num))//item.Value.ParseToInt<double>(out num))
                                {
                                    this.PrintOptions.HeaderMargin = num;
                                }
                                break;
                            case "Footer":
                                item = reader.GetAttribute("ss:Margin");//, true);
                                if ((item != null) && double.TryParse(item, out num))//item.Value.ParseToInt<double>(out num))
                                {
                                    this.PrintOptions.FooterMargin = num;
                                }
                                break;
                            case "PageMargins":
                                ReadPageMargins(reader);
                                break;
                            case "Layout":
                                item = reader.GetAttribute("ss:Orientation");
                                //item = reader.GetSingleAttribute("Orientation", true);
                                if (item != null)
                                {
                                    this.PrintOptions.Orientation = (PageOrientation)Enum.Parse(typeof(PageOrientation), item, true);// ObjectExtensions.ParseEnum<PageOrientation>(item);
                                }
                                break;
                        }
                    }
                }
            }

        }

        private void ReadPageMargins(XmlReader reader)
        {
            double num;

            while (reader.MoveToNextAttribute()) 
            {
                if (double.TryParse(reader.Value, out num))
                {
                    switch (reader.LocalName)
                    {
                        case "Left":
                            this.PrintOptions.LeftMargin = num;
                            break;
                        case "Right":
                            this.PrintOptions.RightMargin = num;
                            break;
                        case "Top":
                            this.PrintOptions.TopMargin = num;
                            break;
                        case "Bottom":
                            this.PrintOptions.BottomMargin = num;
                            break;

                    }
                }
            }
        }

        private void ReadPrintOptions(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read() && (!(reader.Name == "Print") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    int num;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        string name = reader.Name;
                        if (name != null)
                        {
                            if (!(name == "FitHeight"))
                            {
                                if (name == "FitWidth")
                                {
                                    goto Label_00BA;
                                }
                                if (name == "Scale")
                                {
                                    goto Label_0108;
                                }
                            }
                            else
                            {
                                if (reader.IsEmptyElement)
                                {
                                    continue;
                                }
                                reader.Read();
                                if ((reader.NodeType == XmlNodeType.Text) && int.TryParse(reader.Value, out num))//reader.Value.ParseToInt<int>(out num))
                                {
                                    this.PrintOptions.FitHeight = num;
                                }
                            }
                        }
                    }
                    goto Label_0154;
                Label_00BA:
                    if (reader.IsEmptyElement)
                    {
                        continue;
                    }
                    reader.Read();
                    if ((reader.NodeType == XmlNodeType.Text) && int.TryParse(reader.Value, out num))//reader.Value.ParseToInt<int>(out num))
                    {
                        this.PrintOptions.FitWidth = num;
                    }
                    goto Label_0154;
                Label_0108:
                    if (reader.IsEmptyElement)
                    {
                        continue;
                    }
                    reader.Read();
                    if ((reader.NodeType == XmlNodeType.Text) && int.TryParse(reader.Value, out num))//reader.Value.ParseToInt<int>(out num))
                    {
                        this.PrintOptions.Scale = num;
                    }
                Label_0154:;
                }
            }
        }

        private void ReadRow(XmlReader reader)
        {
            bool isEmptyElement = reader.IsEmptyElement;
            int count = this._Rows.Count;
            double variable = -1.0;
            XmlStyle styleByID = null;
            bool flag2 = false;
            //reader.MoveToFirstAttribute();

            while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
            {
                if (!reader.HasValue)
                    continue;
                switch (reader.LocalName)
                {
                    case "Height":
                        double.TryParse(reader.Value, out variable);// item.Value.ParseToInt<double>(out variable);
                        break;
                    case "Index":
                        int.TryParse(reader.Value, out count);// item.Value.ParseToInt<int>(out count);
                        count--;
                        break;
                    case "StyleID":
                        styleByID = this.ParentBook.GetStyleByID(reader.Value);
                        break;
                    case "Hidden":
                        flag2 = reader.Value == "1";
                        break;

                }
            }
            Row rowByIndex = this.GetRowByIndex(count);
            rowByIndex.Hidden = flag2;
            if (variable != -1.0)
            {
                rowByIndex.Height = variable;
            }
            if (styleByID != null)
            {
                rowByIndex.Style = styleByID;
            }
            if (!isEmptyElement)
            {
                while (reader.Read() && (!(reader.Name == "Row") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Cell"))
                    {
                        this.ReadCell(reader, rowByIndex);
                    }
                }
            }
        }

        private void ReadTable(XmlReader reader)
        {
            ReadTable(reader, 0);
        }


        private void ReadTable(XmlReader reader, uint maxRows)
        {
            int rowsRead = 0;

            int colCount = 0;
            int rowCount = 0;

            if (!reader.IsEmptyElement)
            {

                while (reader.MoveToNextAttribute())
                {
                    int tmp = 0;
                    if (reader.HasValue)
                    {
                        switch (reader.LocalName)
                        {
                            case "ExpandedColumnCount":
                                if (int.TryParse(reader.Value, out tmp))
                                    colCount = tmp;
                                break;
                            case "ExpandedRowCount":
                                if (int.TryParse(reader.Value, out tmp))
                                    rowCount = tmp;
                                break;
                        }
                    }
                }
                _ExpandedColumnCount = colCount;
                _ExpandedRowCount = rowCount;

                //if (rowCount == 0)
                //{
                //    return;
                //}
                //int colIndex = 0;
                while ((reader.Read() && (maxRows == 0 || rowsRead < maxRows)) && (!(reader.Name == "Table") || (reader.NodeType != XmlNodeType.EndElement)))
                {

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        string name = reader.Name;
                        if (name == "Row")
                        {
                            this.ReadRow(reader);
                            rowsRead++;
                        }
                        else if (name == "Column")
                        {
                            ReadColumns(reader, colCount);

                        }
                    }
                }
            }
        }

        private void ReadColumns(XmlReader reader,int colCount)
        {
            int colIndex = 0;
            while (reader.Read() && reader.Name == "Column" && reader.NodeType != XmlNodeType.EndElement)
            {

                double width = 0.0;
                bool hidden = false;
                int span = 1;
                XmlStyle styleByID = null;
                //reader.MoveToFirstAttribute();

                while (reader.MoveToNextAttribute())
                {
                    double tmpWidth;
                    if (reader.HasValue)
                    {
                        switch (reader.LocalName)
                        {
                            case "Width":
                                if (double.TryParse(reader.Value, out tmpWidth))
                                    width = tmpWidth;
                                break;
                            case "Hidden":
                                hidden = reader.Value == "1";
                                break;
                            case "Index":
                                int.TryParse(reader.Value, out colIndex);
                                break;
                            case "Span":
                                int.TryParse(reader.Value, out span);
                                break;
                            case "StyleID":
                                styleByID = this.ParentBook.GetStyleByID(reader.Value);
                                break;
                        }
                    }

                }
                for (int i = 1; i <= span; i++)
                {
                    this.Columns(colIndex).Width = width;
                    this.Columns(colIndex).Hidden = hidden;
                    if (styleByID != null)
                    {
                        this.Columns(colIndex).Style = styleByID;
                    }
                    colIndex++;
                }
            }
            int cols = this._Columns.Count;
            for (int i = cols; i < colCount; i++)
            {
                this.Columns(colIndex);
            }
        }

        //public void InsertColumnAfter(int index)
        //{
        //    this.InsertColumnsAfter(index, 1);
        //}

        //public void InsertColumnBefore(int index)
        //{
        //    this.InsertColumnsBefore(index, 1);
        //}

        //public void InsertColumnsAfter(int index, int numberOfColumns)
        //{
        //    if (index >= 0)
        //    {
        //        if (index < (this._Columns.Count - 1))
        //        {
        //            Column item = new Column(this);
        //            this._Columns.Insert(index + 1, item);
        //        }
        //        if (index <= (this.maxColumnAddressed - 1))
        //        {
        //            foreach (Row row in this._Rows)
        //            {
        //                if (index < row._Cells.Count)
        //                {
        //                    row.InsertCellsAfter(index, numberOfColumns);
        //                }
        //            }
        //        }
        //    }
        //}

        //public void InsertColumnsBefore(int index, int numberOfColumns)
        //{
        //    if (index >= 0)
        //    {
        //        if (index < this._Columns.Count)
        //        {
        //            Column item = new Column(this);
        //            this._Columns.Insert(index, item);
        //        }
        //        if (index <= this.maxColumnAddressed)
        //        {
        //            foreach (Row row in this._Rows)
        //            {
        //                if (index < row._Cells.Count)
        //                {
        //                    row.InsertCellsBefore(index, numberOfColumns);
        //                }
        //            }
        //        }
        //    }
        //}

        public void InsertColumns(int index, bool insertAfter)
        {
            InsertColumns(index,1, insertAfter);
        }

        public void InsertColumns(int index, int numberOfColumns, bool insertAfter)
        {
            int offset = insertAfter ? 1 : 0;
            
            if (index >= 0)
            {
                if (index < (this._Columns.Count - offset))
                {
                    Column item = new Column(this);
                    this._Columns.Insert(index + offset, item);
                }
                if (index <= (this.maxColumnAddressed - offset))
                {
                    foreach (Row row in this._Rows)
                    {
                        if (index < row._Cells.Count)
                        {
                            row.InsertCells(index, numberOfColumns,insertAfter); 
                        }
                    }
                }
            }
        }



        public Row InsertRow(int index, int numberOfRows, bool insertAfter)
        {
            int offset = insertAfter ? 1 : 0;

            if (index < 0)
            {
                return this.AddRow();
            }
            if (index >= (this._Rows.Count - offset))
            {
                return this.GetRowByIndex(index + offset);//this[index + offset];
            }

            if (((numberOfRows >= 0) && (index >= 0)) && (index < (this._Rows.Count - offset)))
            {
                for (int i = index; i < (index + numberOfRows); i++)
                {
                    Row item = new Row(this, index);
                    this._Rows.Insert(index + offset, item);
                }
                this.ResetRowNumbersFrom(index);
            }

            return this._Rows[index];
        }
        public Row InsertRow(int index, bool insertAfter)
        {
            return this.InsertRow(index,1, insertAfter);
        }
        public Row InsertRow(Row row, bool insertAfter)
        {
            return this.InsertRow(this._Rows.FindIndex(delegate(Row r)
            {
                return r == row;
            }),1,insertAfter);
        }

        //public Row InsertRowAfter(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.AddRow();
        //    }
        //    if (index >= (this._Rows.Count - 1))
        //    {
        //        return this[index + 1];
        //    }
        //    this.InsertRowsAfter(index, 1);
        //    return this._Rows[index];
        //}

        //public Row InsertRowAfter(Row row)
        //{
        //    return this.InsertRowAfter(this._Rows.FindIndex(delegate (Row r) {
        //        return r == row;
        //    }));
        //}

        //public Row InsertRowBefore(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.AddRow();
        //    }
        //    if (index >= this._Rows.Count)
        //    {
        //        return this[index];
        //    }
        //    this.InsertRowsBefore(index, 1);
        //    return this._Rows[index];
        //}

        //public Row InsertRowBefore(Row row)
        //{
        //    return this.InsertRowBefore(this._Rows.FindIndex(delegate (Row r) {
        //        return r == row;
        //    }));
        //}

        //public void InsertRowsAfter(int index, int rows)
        //{
        //    if (((rows >= 0) && (index >= 0)) && (index < (this._Rows.Count - 1)))
        //    {
        //        for (int i = index; i < (index + rows); i++)
        //        {
        //            Row item = new Row(this, index);
        //            this._Rows.Insert(index + 1, item);
        //        }
        //        this.ResetRowNumbersFrom(index);
        //    }
        //}

        //public void InsertRowsAfter(Row row, int rows)
        //{
        //    if (row != null)
        //    {
        //        this.InsertRowsAfter(this._Rows.FindIndex(delegate (Row r) {
        //            return r == row;
        //        }), rows);
        //    }
        //}

        //public void InsertRowsBefore(int index, int rows)
        //{
        //    if (((rows >= 0) && (index >= 0)) && (index < this._Rows.Count))
        //    {
        //        for (int i = index; i < (index + rows); i++)
        //        {
        //            Row item = new Row(this, index);
        //            this._Rows.Insert(index, item);
        //        }
        //        this.ResetRowNumbersFrom(index);
        //    }
        //}

        //public void InsertRowsBefore(Row row, int rows)
        //{
        //    this.InsertRowsBefore(this._Rows.FindIndex(delegate (Row r) {
        //        return r == row;
        //    }), rows);
        //}

        internal bool IsCellMerged(Cell cell)
        {
            foreach (Nistec.Printing.ExcelXml.Range range in this._MergedCells)
            {
                if (range.Contains(cell))
                {
                    return true;
                }
            }
            return false;
        }

        internal override void IterateAndApply(Styles.IterateFunction ifFunc)
        {
        }

        internal void ResetRowNumbersFrom(int index)
        {
            for (int i = index; i < this._Rows.Count; i++)
            {
                this._Rows[i].RowIndex = i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void WritePanes(XmlWriter writer)
        {
            string str = "";
            if ((this.FreezeLeftColumns > 0) && (this.FreezeTopRows > 0))
            {
                str = "3210";
            }
            else if (this.FreezeLeftColumns > 0)
            {
                str = "31";
            }
            else
            {
                str = "32";
            }
            writer.WriteElementString("ActivePane", str[str.Length - 1].ToString());
            writer.WriteStartElement("Panes");
            foreach (char ch in str)
            {
                writer.WriteStartElement("Pane");
                writer.WriteElementString("Number", ch.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public int ColumnCount
        {
            get
            {
                return this.maxColumnAddressed;
            }
        }

        public int FreezeLeftColumns
        {
            [CompilerGenerated]
            get
            {
                return this._FreezeLeftColumns;
            }
            [CompilerGenerated]
            set
            {
                this._FreezeLeftColumns = value;
            }
        }

        public int FreezeTopRows
        {
            [CompilerGenerated]
            get
            {
                return this._FreezeTopRows;
            }
            [CompilerGenerated]
            set
            {
                this._FreezeTopRows = value;
            }
        }

        public void SetValue(int rowIndex, int colIndex,object value, ContentType type)
        {
            this.Cells(rowIndex, colIndex).SetValue(value,type);
            
        }

        public Cell this[int rowIndex, int colIndex]
        {
            get
            {
                return this.Cells(rowIndex, colIndex);
            }
        }

        //public Row this[int rowIndex]
        //{
        //    get
        //    {
        //        return this.GetRowByIndex(rowIndex);
        //    }
        //}

        public string Name
        {
            get
            {
                return this.sheetName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Worksheet worksheet = this.GetParentBook()[this.sheetName];
                    if ((worksheet == null) || (worksheet == this))
                    {
                        this.sheetName = value.Trim();
                    }
                }
            }
        }

        public Nistec.Printing.ExcelXml.PrintOptions PrintOptions
        {
            [CompilerGenerated]
            get
            {
                return this._PrintOptions;
            }
            [CompilerGenerated]
            set
            {
                this._PrintOptions = value;
            }
        }

        public int RowCount
        {
            get
            {
                return this._Rows.Count;
            }
        }

        [CompilerGenerated]
        private sealed class _GetEnumerator : IEnumerator<Cell>, IEnumerator, IDisposable
        {
            private int __state;
            private Cell __current;
            public Worksheet __this;
            public int __1;
            public int __2;

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
                        while (this.__1 < this.__this._Rows.Count)
                        {
                            this.__2 = 0;
                            while (this.__2 <= this.__this.maxColumnAddressed)
                            {
                                this.__current = this.__this[this.__2, this.__1];
                                this.__state = 1;
                                return true;
                            //Label_0064:
                            //    this.__state = -1;
                            //    this.__2++;
                            }
                            this.__1++;
                        }
                        break;

                    case 1:
                        //goto Label_0064;
                        this.__state = -1;
                        this.__2++;
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

