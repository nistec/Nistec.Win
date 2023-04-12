namespace Nistec.Printing.Data
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Xml;

    public class AdoColumns : CollectionBase
    {
        public AdoColumn Add(AdoColumn column)
        {
            if (column.ColumnName.Trim() == string.Empty)
            {
                column.ColumnName = "Column" + (base.InnerList.Count + 1);
            }
            base.List.Add(column);
            this.RefreshOrdinals();
            return column;
        }

        public AdoColumn Add(string name, Type dataType)
        {
            AdoColumn column = new AdoColumn(name, dataType);
            if (column.ColumnName.Trim() == string.Empty)
            {
                column.ColumnName = "Column" + (base.InnerList.Count + 1);
            }
            base.List.Add(column);
            this.RefreshOrdinals();
            return column;
        }

        public bool AreIdentical(AdoColumns columns)
        {
            return this.AreIdentical(columns, true);
        }

        public bool AreIdentical(AdoColumns columns, bool compareTypes)
        {
            if (columns != this)
            {
                if (columns.Count != base.Count)
                {
                    return false;
                }
                foreach (AdoColumn column in columns)
                {
                    if (!this.Contains(column.ColumnName))
                    {
                        return false;
                    }
                    if (compareTypes && (this[column.ColumnName].DataType.FullName != column.DataType.FullName))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool AreSubsetOf(AdoColumns columns)
        {
            return this.AreSubsetOf(columns, true);
        }

        public bool AreSubsetOf(AdoColumns columns, bool compareTypes)
        {
            return (this.MatchCount(columns, compareTypes) == base.Count);
        }

        public AdoColumns Clone()
        {
            AdoColumns columns = new AdoColumns();
            foreach (AdoColumn column in this)
            {
                columns.Add(column.Clone());
            }
            return columns;
        }

        public bool Contains(AdoColumn value)
        {
            return base.List.Contains(value);
        }

        public bool Contains(string name)
        {
            foreach (AdoColumn column in this)
            {
                if (column.ColumnName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(int index, AdoColumn column)
        {
            if ((column.ColumnName.Trim() == string.Empty) && !this.Contains("Column" + (index + 1)))
            {
                column.ColumnName = "Column" + (index + 1);
            }
            base.List.Insert(index, column);
            this.RefreshOrdinals();
        }

        internal void LoadFromXml(XmlNode node)
        {
            foreach (XmlNode node2 in node.SelectNodes("Column"))
            {
                AdoColumn column = new AdoColumn();
                column.LoadFromXml(node2);
                this.Add(column);
            }
        }

        public int MatchCount(AdoColumns columns)
        {
            return this.MatchCount(columns, true);
        }

        public int MatchCount(AdoColumns columns, bool compareTypes)
        {
            return this.MatchingColumns(columns, compareTypes).Count;
        }

        public AdoColumns MatchingColumns(AdoColumns columns)
        {
            return this.MatchingColumns(columns, true);
        }

        public AdoColumns MatchingColumns(AdoColumns columns, bool compareTypes)
        {
            if (columns == this)
            {
                return this;
            }
            AdoColumns columns2 = new AdoColumns();
            foreach (AdoColumn column in this)
            {
                if (!columns.Contains(column.ColumnName))
                {
                    continue;
                }
                if (compareTypes)
                {
                    if (this[column.ColumnName].DataType.FullName == column.DataType.FullName)
                    {
                        columns2.Add(column);
                    }
                    continue;
                }
                columns2.Add(column);
            }
            return columns2;
        }

        public void Merge(AdoColumns columns)
        {
            if (columns != null)
            {
                foreach (AdoColumn column in columns)
                {
                    if (!this.Contains(column.ColumnName))
                    {
                        this.Add(column.Clone());
                    }
                }
            }
        }

        public int MismatchCount(AdoColumns columns)
        {
            return this.MismatchCount(columns, true);
        }

        public int MismatchCount(AdoColumns columns, bool compareTypes)
        {
            int num = this.MatchCount(columns, compareTypes);
            return (columns.Count - num);
        }

        private void RefreshOrdinals()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this[i].SetOrdinal(i + 1);
            }
        }

        public void Remove(AdoColumn value)
        {
            base.List.Remove(value);
            this.RefreshOrdinals();
        }

        internal void SaveToXml(XmlNode node)
        {
            foreach (AdoColumn column in this)
            {
                XmlNode node2 = node.OwnerDocument.CreateElement("Column");
                column.SaveToXml(node2);
                node.AppendChild(node2);
            }
        }

        public AdoColumn this[string name]
        {
            get
            {
                foreach (AdoColumn column in this)
                {
                    if (column.ColumnName == name)
                    {
                        return column;
                    }
                }
                return null;
            }
        }

        public AdoColumn this[int index]
        {
            get
            {
                return (base.List[index] as AdoColumn);
            }
        }

        public string[] Names
        {
            get
            {
                string[] strArray = new string[base.Count];
                for (int i = 0; i < base.Count; i++)
                {
                    strArray[i] = this[i].ColumnName;
                }
                return strArray;
            }
        }

        public AdoColumn[] ToArray()
        {
            AdoColumn[] array = new AdoColumn[Count];
            int index = 0;
            foreach (AdoColumn c in this)
            {
                array[index] = c;
                index++;
            }
            return array;
        }

    }
}

