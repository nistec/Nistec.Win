namespace Nistec.Printing.Data
{
    using System;
    using System.Xml;
    using System.Data;

    public class AdoTable
    {
        private AdoColumns _columns;
        private string _tableName;

        public AdoTable()
        {
            this._tableName = "";
            this._columns = new AdoColumns();
        }

        public AdoTable(AdoColumns columns)
        {
            this._tableName = "";
            this._columns = columns.Clone();
        }

        public AdoTable(string tableName)
        {
            this._tableName = tableName;
            this._columns = new AdoColumns();
        }

        public AdoTable(string tableName, AdoColumns columns)
        {
            this._tableName = tableName;
            this._columns = columns.Clone();
        }

        public AdoTable Clone()
        {
            AdoTable table = new AdoTable();
            foreach (AdoColumn column in this._columns)
            {
                table.Columns.Add(column.Clone());
            }
            table.TableName = this._tableName;
            return table;
        }

        public void LoadFromXml(XmlNode node)
        {
            XmlNode node2 = node.SelectSingleNode("AdoTable");
            if (node2 != null)
            {
                XmlNode node3 = node2.SelectSingleNode("TableName");
                if (node3 != null)
                {
                    this._tableName = node3.FirstChild.Value;
                }
                XmlNode node4 = node2.SelectSingleNode("AdoColumns");
                if (node4 != null)
                {
                    this._columns.LoadFromXml(node4);
                }
            }
        }

        public void SaveToXml(XmlNode node)
        {
            XmlNode newChild = node.OwnerDocument.CreateElement("AdoTable");
            if (this._tableName != "")
            {
                XmlNode node3 = node.OwnerDocument.CreateElement("TableName");
                node3.AppendChild(node.OwnerDocument.CreateTextNode(this._tableName));
                newChild.AppendChild(node3);
            }
            XmlNode node4 = node.OwnerDocument.CreateElement("AdoColumns");
            this._columns.SaveToXml(node4);
            newChild.AppendChild(node4);
            node.AppendChild(newChild);
        }

        public AdoColumns Columns
        {
            get
            {
                return this._columns;
            }
            set
            {
                this._columns = value;
            }
        }

        public AdoColumns KeyColumns
        {
            get
            {
                AdoColumns columns = new AdoColumns();
                foreach (AdoColumn column in this._columns)
                {
                    if (column.IsKey)
                    {
                        columns.Add(column.Clone());
                    }
                }
                return columns;
            }
        }

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        public void CreateFields(string[] fields)
        {
            Columns.Clear();
            foreach (string s in fields)
            {
                Columns.Add(new AdoColumn(s));
            }
        }

        public void CreateFields(DataTable dt)
        {
            Columns.Clear();
            foreach (DataColumn c in dt.Columns)
            {
                Columns.Add(new AdoColumn(c));
            }
        }

        public void CreateFields(DataTable dt, string[] fields)
        {
            Columns.Clear();
            foreach (string s in fields)
            {
                Columns.Add(new AdoColumn(dt.Columns[s]));
            }
        }

        public void ValidateTableName(string name)
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                _tableName = string.IsNullOrEmpty(name) ? "AdoTable" : name;
            }
        }

        public static int[] CalcColumnsLength(DataTable table, AdoField[] fileds)
        {
            int[] colwidth = new int[table.Columns.Count];
            colwidth.Initialize();
            if (fileds == null)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colwidth[i] = string.IsNullOrEmpty(table.Columns[i].Caption) ? table.Columns[i].ColumnName.Length : table.Columns[i].Caption.Length;
                }
            }
            else
            {
                for (int i = 0; i < fileds.Length; i++)
                {
                    colwidth[i] = string.IsNullOrEmpty(fileds[i].Caption) ? fileds[i].ColumnName.Length : fileds[i].Caption.Length;
                }

            }
            foreach (DataRow dr in table.Rows)
            {
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    colwidth[c] = Math.Max(colwidth[c], string.Format("{0}", dr[c]).Length);
                }
            }
            return colwidth;
        }

        public static void SetColumnsLength(DataTable table, AdoTable source, AdoField[] fileds)
        {
            int[] colwidth = CalcColumnsLength(table, fileds);
           
            for (int i = 0; i < source.Columns.Count; i++)
            {
                source.Columns[i].Length = colwidth[i];
            }
        }
        public static float CalcWidth(float length, float totalLength, float fontWidth, float maxWidth)
        {

            float factor = 1f;
            float totalWidth = totalLength * fontWidth;
            if (totalWidth > 0)
            {
                factor = maxWidth / totalWidth;
            }
            return length * factor;

        }

        public static float GetTotalLength(AdoTable source)
        {
            float len = 0;
            for (int i = 0; i < source.Columns.Count; i++)
            {
                len += source.Columns[i].Length;
            }
            return len;
        }

        public static AdoTable CreateSchema(DataTable table)
        {
            return CreateSchema(table, null, false);
        }

        public static AdoTable CreateSchema(DataTable table, bool calcWidth)
        {
            return CreateSchema(table, null, calcWidth);
        }

        public static AdoTable CreateSchema(DataTable table, AdoField[] fileds)
        {
            return CreateSchema(table, fileds, false);
        }

        public static AdoTable CreateSchema(DataTable table, AdoField[] fileds, bool calcWidth)
        {
            AdoTable source = new AdoTable();
            source.ValidateTableName(table.TableName);

            if (fileds == null)
            {
                foreach (DataColumn column in table.Columns)
                {
                    AdoColumn col = new AdoColumn(column.ColumnName, column.DataType,column.MaxLength);
                    source.Columns.Add(col);
                }
                //for (int i = 0; i < table.Columns.Count; i++)
                //{
                //    source.Columns.Add(table.Columns[i].ColumnName, table.Columns[i].DataType);
                //}
            }
            else
            {
                for (int i = 0; i < fileds.Length; i++)
                {
                    AdoColumn col = new AdoColumn(fileds[i].ColumnName, fileds[i].DataType);
                    //col.Length = table.Columns[fileds[i]].MaxLength;
                    col.Caption = fileds[i].Caption;
                    source.Columns.Add(col);
                }

            } 
            
           
            foreach (DataColumn dc in table.PrimaryKey)
            {
                if (source.Columns.Contains(dc.ColumnName))
                {
                    source.Columns[dc.ColumnName].IsKey = true;
                }
            }

            if (calcWidth)
            {
                SetColumnsLength(table, source,fileds);
            }

            return source;
        }

    }
}

