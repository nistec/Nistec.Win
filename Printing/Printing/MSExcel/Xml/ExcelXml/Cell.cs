namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Cell : Styles
    {
        private object _value;
        [CompilerGenerated]
        private string _Comment;
        internal int CellIndex;
        internal Nistec.Printing.ExcelXml.ContentType Content;
        private Formula formula;
        internal bool MergeStart;
        internal Row ParentRow;

        internal Cell(Row parent, int index)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.ParentRow = parent;
            this.Content = Nistec.Printing.ExcelXml.ContentType.None;
            this.CellIndex = index;
            if (parent.Style != null)
            {
                base.Style = parent.Style;
            }
            else if (parent.ParentSheet.Columns(this.CellIndex).Style != null)
            {
                base.Style = parent.ParentSheet.Columns(this.CellIndex).Style;
            }
            else if (parent.ParentSheet.Style != null)
            {
                base.Style = parent.ParentSheet.Columns(this.CellIndex).Style;
            }
        }

        public void Delete()
        {
            this.ParentRow.DeleteCell(this);
        }

        public void Empty()
        {
            this.Empty(true);
        }

        internal void Empty(bool removeContentOnly)
        {
            this.Content = Nistec.Printing.ExcelXml.ContentType.None;
            this._value = null;
            this.formula = null;
            if (!removeContentOnly)
            {
                this.ParentRow = null;
            }
        }

        internal void Write(XmlWriter writer, bool printIndex)
        {
            Predicate<Range> match = null;
            if (!this.IsEmpty() && (this.MergeStart || !this.ParentRow.ParentSheet.IsCellMerged(this)))
            {
                writer.WriteStartElement("Cell");
                if ((!string.IsNullOrEmpty(base.StyleID) && (this.ParentRow.StyleID != base.StyleID)) && (base.StyleID != "Default"))
                {
                    writer.WriteAttributeString("ss", "StyleID", null, base.StyleID);
                }
                if (printIndex)
                {
                    writer.WriteAttributeString("ss", "Index", null, (this.CellIndex + 1).ToString(CultureInfo.InvariantCulture));
                }
                if (this.MergeStart)
                {
                    if (match == null)
                    {
                        match = delegate (Range rangeToFind) {
                            return rangeToFind.CellFrom == this;
                        };
                    }
                    Range range = this.ParentRow.ParentSheet._MergedCells.Find(match);
                    if (range != null)
                    {
                        int num = range.ColumnCount - 1;
                        int num2 = range.RowCount - 1;
                        if (num > 0)
                        {
                            writer.WriteAttributeString("ss", "MergeAcross", null, num.ToString(CultureInfo.CurrentCulture));
                        }
                        if (num2 > 0)
                        {
                            writer.WriteAttributeString("ss", "MergeDown", null, num2.ToString(CultureInfo.CurrentCulture));
                        }
                    }
                }
                this.WriteContent(writer);
                this.WriteComment(writer);
                List<string> list = this.GetParentBook().CellInNamedRanges(this);
                foreach (string str in list)
                {
                    writer.WriteStartElement("NamedCell");
                    writer.WriteAttributeString("ss", "Name", null, str);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private void WriteComment(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Comment))
            {
                string author = this.GetParentBook().Properties.Author;
                writer.WriteStartElement("Comment");
                if (!string.IsNullOrEmpty(author))
                {
                    writer.WriteAttributeString("ss", "Author", null, author);
                }
                writer.WriteStartElement("ss", "Data", null);
                writer.WriteAttributeString("xmlns", "http://www.w3.org/TR/REC-html40");
                writer.WriteRaw(this.Comment);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private void WriteContent(XmlWriter writer)
        {
            //if (this.Content == Nistec.Printing.ExcelXml.ContentType.Formula)
            //{
            //    writer.WriteAttributeString("ss", "Formula", null, "=" + this.formula.ToString(this));
            //}
            if (this.Content == Nistec.Printing.ExcelXml.ContentType.UnresolvedValue)
            {
                writer.WriteAttributeString("ss", "Formula", null, (string) this._value);
            }
            else if (this.Content != Nistec.Printing.ExcelXml.ContentType.None)
            {
                if (this.formula != null)
                {
                    writer.WriteAttributeString("ss", "Formula", null, "=" + this.formula.ToString(this));
                }
                writer.WriteStartElement("Data");
                writer.WriteAttributeString("ss", "Type", null, this.Content.ToString());
                switch (this.Content)
                {
                    case Nistec.Printing.ExcelXml.ContentType.String:
                        writer.WriteValue(this._value==null?"":_value.ToString());
                        break;

                    case Nistec.Printing.ExcelXml.ContentType.Number:
                        writer.WriteValue(Convert.ToDecimal(this._value).ToString(new CultureInfo("en-US")));
                        break;

                    case Nistec.Printing.ExcelXml.ContentType.DateTime:
                        writer.WriteValue(((DateTime) this._value).ToString(@"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture));
                        break;

                    case Nistec.Printing.ExcelXml.ContentType.Boolean:
                        if (!((bool) this._value))
                        {
                            writer.WriteValue("0");
                            break;
                        }
                        writer.WriteValue("1");
                        break;
                }
                writer.WriteEndElement();
            }
        }

        internal override Cell FirstCell()
        {
            return null;
        }

        internal override Workbook GetParentBook()
        {
            return this.ParentRow.ParentSheet.ParentBook;
        }

        public T GetValue<T>()
        {
            string fullName = typeof(T).FullName;
            if (fullName == "System.Object")
            {
                return (T) this._value;
            }
            if (((!typeof(T).IsPrimitive && (fullName != "System.DateTime")) && (fullName != "System.String")) && (fullName != "Nistec.Printing.ExcelXml.Formula"))
            {
                throw new ArgumentException("T must be of a primitive or Formula type");
            }
            switch (this.Content)
            {
                case Nistec.Printing.ExcelXml.ContentType.String:
                case Nistec.Printing.ExcelXml.ContentType.UnresolvedValue:
                    if (!(fullName == "System.String"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this._value, typeof(T), CultureInfo.InvariantCulture);

                case Nistec.Printing.ExcelXml.ContentType.Number:
                    if (!IsNumericType(typeof(T)))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this._value, typeof(T), CultureInfo.InvariantCulture);

                case Nistec.Printing.ExcelXml.ContentType.DateTime:
                    if (!(fullName == "System.DateTime"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this._value, typeof(T), CultureInfo.InvariantCulture);

                case Nistec.Printing.ExcelXml.ContentType.Boolean:
                    if (!(fullName == "System.Boolean"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this._value, typeof(T), CultureInfo.InvariantCulture);

                //case Nistec.Printing.ExcelXml.ContentType.Formula:
                //    if (!(fullName == "Nistec.Printing.ExcelXml.Formula"))
                //    {
                //        return default(T);
                //    }
                //    return (T) this._value;
            }
            return default(T);
        }

        public bool IsEmpty()
        {
            return (((this.Content == Nistec.Printing.ExcelXml.ContentType.None) && string.IsNullOrEmpty(this.Comment)) && base.HasDefaultStyle());
        }

        internal override void IterateAndApply(Styles.IterateFunction ifFunc)
        {
        }

        internal void ResolveReferences()
        {
            if (this.formula!=null)// this.Content == Nistec.Printing.ExcelXml.ContentType.Formula)
            {
                foreach (Parameter parameter in this.formula.Parameters)
                {
                    if (parameter.ParameterType == ParameterType.Range)
                    {
                        Range range = parameter.Value as Range;
                        if (range != null)
                        {
                            range.ParseUnresolvedReference(this);
                        }
                    }
                }
            }
        }

        public void Unmerge()
        {
            if (this.MergeStart)
            {
                this.ParentRow.ParentSheet._MergedCells.RemoveAll(delegate (Range range) {
                    return range.CellFrom == this;
                });
                this.MergeStart = false;
            }
        }
        public static bool IsNumericType(Type type)
        {
            switch (type.FullName)
            {
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return true;
            }
            return false;
        }
        public string Comment
        {
            [CompilerGenerated]
            get
            {
                return this._Comment;
            }
            [CompilerGenerated]
            set
            {
                this._Comment = value;
            }
        }

        public Nistec.Printing.ExcelXml.ContentType ContentType
        {
            get
            {
                return this.Content;
            }
        }

        public CellIndexInfo Index
        {
            get
            {
                return new CellIndexInfo(this);
            }
        }
        
        public Formula Formula
        {
            get
            {
                return this.formula;
            }
        }

        public void SetValue(object value,ContentType type)
        {
            this._value = value;
            this.Content = type;
        }

        public void SetValue(object value, Type type)
        {
            this._value = value;
            switch (type.FullName)
            {
                case "System.DateTime":
                    {
                       this.Content = ContentType.DateTime;
                        break;
                    }
                case "System.Boolean":
                    {
                        this.Content =  ContentType.Boolean;
                        break;
                    }
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    {
                        this.Content =  ContentType.Number;
                        break;
                    }
                case "System.DBNull":
                    {
                        break;
                    }
                default:
                    this.Content =  ContentType.String;
                    break;
            }
        }

        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                switch (value.GetType().FullName)
                {
                    case "System.DateTime":
                        this._value = value;
                        this.Content = Nistec.Printing.ExcelXml.ContentType.DateTime;
                        break;

                    case "System.Byte":
                    case "System.SByte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        this._value = value;
                        this.Content = Nistec.Printing.ExcelXml.ContentType.Number;
                        break;

                    case "System.Boolean":
                        this._value = value;
                        this.Content = Nistec.Printing.ExcelXml.ContentType.Boolean;
                        break;

                    case "System.String":
                        this._value = value;
                        this.Content = Nistec.Printing.ExcelXml.ContentType.String;
                        break;

                    case "Nistec.Printing.ExcelXml.Cell":
                    {
                        Cell cell = value as Cell;
                        if (cell == null)
                        {
                            this.formula = null;
                            this._value = null;
                            this.Content = Nistec.Printing.ExcelXml.ContentType.None;
                            break;
                        }
                        if (this.formula != null)
                        {
                            this.formula = null;
                        }
                        this.formula = new Formula();
                        this._value = cell.Value;// null;
                        this.formula.Add(new Range(cell));
                        this.Content = cell.ContentType;// Nistec.Printing.ExcelXml.ContentType.Formula;
                        break;
                    }
                    case "Nistec.Printing.ExcelXml.Formula":
                    {
                        Formula formula = value as Formula;
                        if (formula == null)
                        {
                            this.formula = null;
                            this._value = null;
                            this.Content = Nistec.Printing.ExcelXml.ContentType.None;
                            break;
                        }
                        this.formula = formula;
                        this._value = formula.Value;// null;
                        this.Content = formula.ContentType;// Nistec.Printing.ExcelXml.ContentType.Formula;
                        break;
                    }
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}

