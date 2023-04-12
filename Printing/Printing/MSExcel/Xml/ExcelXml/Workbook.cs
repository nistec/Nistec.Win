namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Xml;
    using Nistec.Printing.Data;

    public class Workbook
    {
        #region members

        private List<Worksheet> _Worksheets;
        [CompilerGenerated]
        private DocumentProperties _Properties;
        internal List<NamedRange> NamedRanges;
        private List<XmlStyle> Styles;
        #endregion

        #region ctor
        public Workbook()
        {
            this.Initialize();
        }
        #endregion

        #region static methods

        public static bool Export(DataSet ds, string fileName, bool firstRowHeader)
        {
            Workbook workbook = new Workbook();
            foreach (DataTable table in ds.Tables)
            {
                Workbook.Export(workbook, table.Copy(), firstRowHeader, null);
            }
            return workbook.Write(fileName);
        }


        public static bool Export(DataTable table, string fileName, bool firstRowHeader, Nistec.Printing.Data.AdoField[] fields)
        {
            Workbook workbook = new Workbook();
            Workbook.Export(workbook, table, firstRowHeader, fields);
            return workbook.Write(fileName);
        }

        private static void Export(Workbook workbook, DataTable table, bool firstRowHeader, Nistec.Printing.Data.AdoField[] fields)
        {

            const int maxRows = 65000;
            string tableName = table.TableName;
            int k, iWorkSheet = 1, cols = 1, rows = 1, maxStep = 0, offset = 0;

            if (tableName == "") tableName = "Table";
            cols = table.Columns.Count; ;
            rows = table.Rows.Count;
            maxStep = rows;
            if (rows >= maxRows)
            {
                iWorkSheet = rows / maxRows;
                iWorkSheet++;
                maxStep = maxRows;
                //offset=iRow-maxRows;
            }

            string sheetName = tableName;
            if (fields == null || fields.Length == 0)
            {
                fields = Nistec.Printing.Data.AdoField.CreateFields(table);
            }

            for (int i = 0; i < iWorkSheet; i++)
            {
                if (i > 0)
                {
                    sheetName = tableName + i.ToString();
                }

                Worksheet worksheet = workbook.Add(sheetName);
                int index = 0;
                bool writeHeader = firstRowHeader;
                foreach (Nistec.Printing.Data.AdoField c in fields)
                {
                    if (writeHeader)
                    {
                        //Headers
                        worksheet[0, index].Value = c.ToString();
                        worksheet[0, index].Font.Bold = true;
                    }
                    worksheet.Columns(index);
                    index++;
                }

                if (iWorkSheet > 1 && i == iWorkSheet - 1)
                {
                    maxStep = rows - ((iWorkSheet - 1) * maxRows);
                }
                offset = i * maxRows;

                int iRow = 0;
                int iCol = 0;
                int wRow = 0;

                for (k = 0; k < maxStep; k++)
                {

                    iCol = 0;
                    iRow = k + offset;
                    wRow = k + (int)(writeHeader ? 1 : 0);
                    DataRow row = table.Rows[iRow];

                    foreach (Nistec.Printing.Data.AdoField c in fields)
                    {
                        worksheet[wRow, iCol].SetValue(row[c.ColumnName], c.DataType);

                        iCol++;
                    }
                    //base.UpdateProcessing();
                }

            }
        }


        //public static Workbook DataSetToWorkbook(DataTable source)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(source.Copy());
        //    return DataSetToWorkbook(ds);
        //}

        //public static Workbook Export(DataSet source, string fileName, bool firstRowHeader)
        //{
        //    Workbook workbook = new Workbook();

        //    for (int i = 0; i < source.Tables.Count; i++)
        //    {
        //        Worksheet worksheet = workbook[i];
        //        string tableName = source.Tables[i].TableName;
        //        if (tableName == "")
        //        {
        //            tableName = "Table" + i.ToString(CultureInfo.InvariantCulture);
        //        }
        //        worksheet.Name = tableName;// "Table" + i.ToString(CultureInfo.InvariantCulture);
        //        int count = source.Tables[i].Columns.Count;
        //        int iRow = 0;
        //        int iCol = 0;
        //        while (iCol < count)
        //        {
        //            if (firstRowHeader)
        //            {
        //                worksheet[0, iCol].Value = source.Tables[i].Columns[iRow].ColumnName;
        //                worksheet[0, iCol].Font.Bold = true;
        //            }
        //            worksheet.Columns(iCol);
        //            iCol++;
        //        }



        //        iRow = 0;
        //        foreach (DataRow row in source.Tables[i].Rows)
        //        {
        //            for (iCol = 0; iCol < count; iCol++)
        //            {
        //                switch (row[iRow].GetType().FullName)
        //                {
        //                    case "System.DateTime":
        //                        {
        //                            worksheet[iRow, iCol].Value = (DateTime)row[iRow];
        //                            continue;
        //                        }
        //                    case "System.Boolean":
        //                        {
        //                            worksheet[iRow, iCol].Value = (bool)row[iRow];
        //                            continue;
        //                        }
        //                    case "System.SByte":
        //                    case "System.Int16":
        //                    case "System.Int32":
        //                    case "System.Int64":
        //                    case "System.Byte":
        //                    case "System.UInt16":
        //                    case "System.UInt32":
        //                    case "System.UInt64":
        //                    case "System.Single":
        //                    case "System.Double":
        //                    case "System.Decimal":
        //                        {
        //                            worksheet[iRow, iCol].Value = Convert.ToDecimal(row[iRow], CultureInfo.InvariantCulture);
        //                            continue;
        //                        }
        //                    case "System.DBNull":
        //                        {
        //                            continue;
        //                        }
        //                }
        //                worksheet[iRow, iCol].Value = row[iRow].ToString();
        //            }
        //            iRow++;
        //        }
        //    }
        //    return workbook;
        //}

        public static DataSet Import(string fileName, bool firstRowHeader, uint maxRows)
        {

            if (!File.Exists(fileName))
            {
                return null;
            }
            Stream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return null;
            }
            catch (SecurityException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            if (!stream.CanRead)
            {
                return null;
            }
            XmlReader reader = XmlReader.Create(stream, settings);
            Workbook book = new Workbook();
            DataSet ds = new DataSet();
            book.Styles.Clear();
            int num = 0;

            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "DocumentProperties"))
                        {
                            if (name == "Worksheet")
                            {
                                if (!reader.IsEmptyElement)
                                {
                                    book[num].Read(reader, maxRows);
                                    ds.Tables.Add(book[num].GetDataTable(firstRowHeader, maxRows));
                                    num++;
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            stream.Close();
            stream.Dispose();
            return ds;
        }

        public static DataTable Import(string fileName, bool firstRowHeader, uint maxRows, string worksheet)
        {

            DataSet ds = Import(fileName, firstRowHeader, maxRows);
            return ds.Tables[worksheet];
        }
        public static DataTable Import(string fileName, bool firstRowHeader, uint maxRows, int worksheet)
        {

            DataSet ds = Import(fileName, firstRowHeader, maxRows);
            return ds.Tables[worksheet];
        }

        public static Workbook Import(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            if (!stream.CanRead)
            {
                return null;
            }
            XmlReader reader = XmlReader.Create(stream, settings);
            Workbook book = new Workbook();
            book.Styles.Clear();
            int num = 0;

            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    if (name != null)
                    {
                        switch (name)
                        {
                            case "Styles":
                                if (!reader.IsEmptyElement)
                                {
                                    book.ReadStyles(reader);
                                }
                                break;
                            case "Names":
                                ReadNamedRanges(reader, book, null);
                                break;
                            case "Worksheet":
                                if (!reader.IsEmptyElement)
                                {
                                    book[num++].Read(reader);
                                }
                                break;
                            case "DocumentProperties":
                                book.Properties.Read(reader);
                                break;

                        }
                    }
                }
            }
            book.ResolveNamedRangeReferences();
            book.ResolveCellReferences();
            reader.Close();
            stream.Close();
            stream.Dispose();
            return book;
        }

        public static Workbook Import(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            Stream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return null;
            }
            catch (SecurityException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            Workbook workbook = Import(stream);
            stream.Close();
            stream.Dispose();
            return workbook;
        }

        public static string[] GetWorksheets(string fileName)
        {

            if (!File.Exists(fileName))
            {
                return null;
            }
            Stream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return null;
            }
            catch (SecurityException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            if (!stream.CanRead)
            {
                return null;
            }
            XmlReader reader = XmlReader.Create(stream, settings);
            List<string> worksheets = new List<string>();
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "DocumentProperties"))
                        {
                            if (name == "Worksheet")
                            {
                                if (!reader.IsEmptyElement)
                                {
                                    worksheets.Add(reader.Value);
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            stream.Close();
            stream.Dispose();
            return worksheets.ToArray();
        }

        internal static void ReadNamedRanges(XmlReader reader, Workbook book, Worksheet ws)
        {
            if (!reader.IsEmptyElement)
            {
                while (reader.Read() && (!(reader.Name == "Names") || (reader.NodeType != XmlNodeType.EndElement)))
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "NamedRange"))
                    {
                        Nistec.Printing.ExcelXml.Range range = null;
                        string name = "";
                        //reader.MoveToFirstAttribute();

                        while (reader.MoveToNextAttribute()) //foreach (XmlReaderAttributeItem item in reader.GetAttributes())
                        {
                            if ((reader.LocalName == "Name") && reader.HasValue)
                            {
                                name = reader.Value;
                            }
                            if ((reader.LocalName == "RefersTo") && reader.HasValue)
                            {
                                range = new Nistec.Printing.ExcelXml.Range(reader.Value);
                            }
                        }
                        NamedRange range2 = new NamedRange(range, name, ws);
                        book.NamedRanges.Add(range2);
                    }
                }
            }
        }

        #endregion

        #region public methods

        public Worksheet Add()
        {
            return this[this._Worksheets.Count];
        }

        public Worksheet Add(string sheetName)
        {
            int index = this._Worksheets.Count;
            this[index].Name = sheetName;
            return this[index];
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
            this.AddNamedRange(range, name, null);
        }

        public void DeleteSheet(int index)
        {
            if ((index >= 0) && (index < this._Worksheets.Count))
            {
                this._Worksheets.RemoveAt(index);
            }
        }

        public void DeleteSheet(string sheetName)
        {
            this.DeleteSheet(this.GetSheetIDByName(sheetName));
        }

        public void DeleteSheet(Worksheet ws)
        {
            if (ws != null)
            {
                this.DeleteSheet(this._Worksheets.FindIndex(delegate(Worksheet s)
                {
                    return s == ws;
                }));
            }
        }

        public bool Write(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            if (!stream.CanWrite)
            {
                return false;
            }
            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            writer.WriteWhitespace("\n");
            writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
            writer.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
            writer.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
            writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
            writer.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");
            this.Properties.Write(writer);
            writer.WriteStartElement("Styles");
            foreach (XmlStyle style in this.Styles)
            {
                style.Write(writer);
            }
            writer.WriteEndElement();
            this.WriteNamedRanges(writer, null);
            foreach (Worksheet worksheet in this._Worksheets)
            {
                worksheet.Write(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            return true;
        }

        public bool Write(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Create);
            }
            catch
            {
                return false;
            }
            bool flag = this.Write(stream);
            stream.Close();
            stream.Dispose();
            return flag;
        }
        public Worksheet GetSheetByName(string sheetName)
        {
            foreach (Worksheet w in this._Worksheets)
            {
                if (w.Name == sheetName)
                    return w;
            }
            return null;
            //return this._Worksheets.Find(delegate (Worksheet sheet) {
            //    return sheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase);
            //});
        }

        public Worksheet Insert(int index, bool insertAfter)
        {
            if (index < 0)
            {
                return this.Add();
            }
            if (index >= this._Worksheets.Count)
            {
                return this[index];
            }
            Worksheet ws = new Worksheet(this);

            if (insertAfter)
            {
                this.SetSheetNameByIndex(ws, index + 1);
                this._Worksheets.Insert(index + 1, ws);
            }
            else
            {
                this.SetSheetNameByIndex(ws, index);
                this._Worksheets.Insert(index, ws);
            }
            return ws;
        }

        public Worksheet Insert(string sheetName, bool insertAfter)
        {
            return this.Insert(this.GetSheetIDByName(sheetName), insertAfter);
        }

        public Worksheet Insert(Worksheet ws, bool insertAfter)
        {
            return this.Insert(this._Worksheets.FindIndex(delegate(Worksheet s)
            {
                return s == ws;
            }), insertAfter);
        }
        //public Worksheet InsertAfter(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.Add();
        //    }
        //    if (index >= this._Worksheets.Count)
        //    {
        //        return this[index];
        //    }
        //    Worksheet ws = new Worksheet(this);
        //    this.SetSheetNameByIndex(ws, index + 1);
        //    this._Worksheets.Insert(index + 1, ws);
        //    return ws;
        //}

        //public Worksheet InsertAfter(string sheetName)
        //{
        //    return this.InsertAfter(this.GetSheetIDByName(sheetName));
        //}

        //public Worksheet InsertAfter(Worksheet ws)
        //{
        //    return this.InsertAfter(this._Worksheets.FindIndex(delegate (Worksheet s) {
        //        return s == ws;
        //    }));
        //}

        //public Worksheet InsertBefore(int index)
        //{
        //    if (index < 0)
        //    {
        //        return this.Add();
        //    }
        //    if (index >= this._Worksheets.Count)
        //    {
        //        return this[index];
        //    }
        //    Worksheet ws = new Worksheet(this);
        //    this.SetSheetNameByIndex(ws, index);
        //    this._Worksheets.Insert(index, ws);
        //    return ws;
        //}

        //public Worksheet InsertBefore(string sheetName)
        //{
        //    return this.InsertBefore(this.GetSheetIDByName(sheetName));
        //}

        //public Worksheet InsertBefore(Worksheet ws)
        //{
        //    return this.InsertBefore(this._Worksheets.FindIndex(delegate (Worksheet s) {
        //        return s == ws;
        //    }));
        //}


        #endregion 

        #region private methods

        internal void AddNamedRange(Nistec.Printing.ExcelXml.Range range, string name, Worksheet ws)
        {
            Predicate<NamedRange> match = null;
            if ((range.FirstCell() != null) && (range.FirstCell().GetParentBook() != this))
            {
                throw new InvalidOperationException("Named range parent book should be same");
            }
            NamedRange range2 = this.NamedRanges.Find(delegate (NamedRange nr) {
                return (nr.Name == name) && (nr.Worksheet == ws);
            });
            if (range2 == null)
            {
                if (match == null)
                {
                    match = delegate (NamedRange nr) {
                        return nr.Range.Match(range);
                    };
                }
                range2 = this.NamedRanges.Find(match);
                if (range2 == null)
                {
                    if (name == "_FilterDatabase")
                    {
                        this.NamedRanges.Insert(0, new NamedRange(range, name, ws));
                    }
                    else
                    {
                        this.NamedRanges.Add(new NamedRange(range, name, ws));
                    }
                }
                else
                {
                    range2.Name = name;
                }
            }
            else
            {
                range2.Range = range;
            }
        }

        internal string AddStyle(XmlStyle style)
        {
            XmlStyle style2 = this.FindStyle(style);
            if (style2 == null)
            {
                int count = this.Styles.Count;
                style.ID = string.Format(CultureInfo.InvariantCulture, "S{0:00}", new object[] { count++ });
                while (this.HasStyleID(style.ID))
                {
                    style.ID = string.Format(CultureInfo.InvariantCulture, "S{0:00}", new object[] { count++ });
                }
                this.Styles.Add(style);
                return style.ID;
            }
            return style2.ID;
        }

        internal List<string> CellInNamedRanges(Cell cell)
        {
            List<string> list = new List<string>();
            PrintOptions printOptions = cell.ParentRow.ParentSheet.PrintOptions;
            if (printOptions.PrintTitles)
            {
                int num = cell.ParentRow.RowIndex + 1;
                int num2 = cell.CellIndex + 1;
                if ((num >= printOptions.TopPrintRow) && (num <= printOptions.BottomPrintRow))
                {
                    list.Add("Print_Titles");
                }
                else if ((num2 >= printOptions.LeftPrintCol) && (num2 <= printOptions.RightPrintCol))
                {
                    list.Add("Print_Titles");
                }
            }
            foreach (NamedRange range in this.NamedRanges)
            {
                if (range.Range.Contains(cell))
                {
                    list.Add(range.Name);
                }
            }
            return list;
        }

        internal void WriteNamedRanges(XmlWriter writer, Worksheet ws)
        {
            bool flag = false;
            if ((ws != null) && ws.PrintOptions.PrintTitles)
            {
                flag = true;
                writer.WriteStartElement("Names");
                writer.WriteStartElement("NamedRange");
                writer.WriteAttributeString("ss", "Name", null, "Print_Titles");
                writer.WriteAttributeString("ss", "RefersTo", null, ws.PrintOptions.GetPrintTitleRange(ws.Name));
                writer.WriteEndElement();
            }
            foreach (NamedRange range in this.NamedRanges)
            {
                if (range.Worksheet == ws)
                {
                    if (!flag)
                    {
                        flag = true;
                        writer.WriteStartElement("Names");
                    }
                    writer.WriteStartElement("NamedRange");
                    writer.WriteAttributeString("ss", "Name", null, range.Name);
                    writer.WriteAttributeString("ss", "RefersTo", null, range.Range.NamedRangeReference(true));
                    if (range.Name == "_FilterDatabase")
                    {
                        writer.WriteAttributeString("ss", "Hidden", null, "1");
                    }
                    writer.WriteEndElement();
                }
            }
            if (flag)
            {
                writer.WriteEndElement();
            }
        }

        internal XmlStyle FindStyle(XmlStyle style)
        {
            return this.Styles.Find(delegate(XmlStyle xs)
            {
                return xs.CheckForMatch(style);
            });
        }

        internal string GetAutoFilterRange(Worksheet ws)
        {
            NamedRange range = this.NamedRanges.Find(delegate(NamedRange nr)
            {
                return (nr.Name == "_FilterDatabase") && (nr.Worksheet == ws);
            });
            if (range == null)
            {
                return "";
            }
            return range.Range.NamedRangeReference(false);
        }

        private int GetSheetIDByName(string sheetName)
        {
            return this._Worksheets.FindIndex(delegate(Worksheet sheet)
            {
                return sheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase);
            });
        }

        internal XmlStyle GetStyleByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                if (Styles.Count <= 0)
                    return new XmlStyle();
                return this.Styles[0];
            }
            return this.Styles.Find(delegate(XmlStyle xs)
            {
                return xs.ID == ID;
            });
        }

        internal bool HasStyleID(string ID)
        {
            return this.Styles.Exists(delegate(XmlStyle xs)
            {
                return xs.ID == ID;
            });
        }

        private void ReadStyles(XmlReader reader)
        {
            while (reader.Read() && (!(reader.Name == "Styles") || (reader.NodeType != XmlNodeType.EndElement)))
            {
                XmlStyle item = XmlStyle.Read(reader);
                if (item != null)
                {
                    this.Styles.Add(item);
                }
            }
        }

        private void Initialize()
        {
            this.Properties = new DocumentProperties();
            this.Styles = new List<XmlStyle>();
            this._Worksheets = new List<Worksheet>();
            this.NamedRanges = new List<NamedRange>();
            XmlStyle item = new XmlStyle();
            item.ID = "Default";
            item.Alignment.Vertical = VerticalAlignment.Bottom;
            this.Styles.Add(item);
        }

        private void ResolveCellReferences()
        {
            for (int i = 0; i < this._Worksheets.Count; i++)
            {
                Worksheet worksheet = this[i];
                foreach (Row row in worksheet._Rows)
                {
                    foreach (Cell cell in row._Cells)
                    {
                        cell.ResolveReferences();
                    }
                }
            }
        }

        private void ResolveNamedRangeReferences()
        {
            int index = -1;
            int num2 = -1;
            foreach (NamedRange range in this.NamedRanges)
            {
                num2++;
                if (range.Name == "Print_Titles")
                {
                    FormulaParser.ParsePrintHeaders(range.Worksheet, range.Range.UnresolvedRangeReference);
                    index = num2;
                }
                else
                {
                    Worksheet worksheet = range.Worksheet ?? this[0];
                    range.Range.ParseUnresolvedReference(worksheet[0, 0]);
                    if (range.Name == "_FilterDatabase")
                    {
                        worksheet.AutoFilter = true;
                    }
                }
            }
            if (index != -1)
            {
                this.NamedRanges.RemoveAt(index);
            }
        }

        private void SetSheetNameByIndex(Worksheet ws, int index)
        {
            index++;
            string sheetName = "Sheet" + index.ToString(CultureInfo.InvariantCulture);
            while (this.GetSheetIDByName(sheetName) != -1)
            {

                sheetName = string.Format("Sheet{0}", ++index);
                //sheetName = "Sheet" + ++index.ToString(CultureInfo.InvariantCulture);
            }
            ws.Name = sheetName;
        }

        #endregion

        #region properties

        public XmlStyle DefaultStyle
        {
            get
            {
                return this.Styles[0];
            }
            set
            {
                if ((value != null) && (value.ID == "Default"))
                {
                    this.Styles[0] = value;
                }
            }
        }

        public Worksheet this[string sheetName]
        {
            get
            {
                return this.GetSheetByName(sheetName);
            }
        }

        public Worksheet this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                if ((index + 1) > this._Worksheets.Count)
                {
                    for (int i = this._Worksheets.Count; i <= index; i++)
                    {
                        Worksheet ws = new Worksheet(this);
                        this.SetSheetNameByIndex(ws, i);
                        this._Worksheets.Add(ws);
                    }
                }
                return this._Worksheets[index];
            }
        }

        public DocumentProperties Properties
        {
            [CompilerGenerated]
            get
            {
                return this._Properties;
            }
            [CompilerGenerated]
            set
            {
                this._Properties = value;
            }
        }

        public int SheetCount
        {
            get
            {
                return this._Worksheets.Count;
            }
        }

        #endregion
    }
}

