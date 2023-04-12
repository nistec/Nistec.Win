namespace Nistec.Printing.Data
{
    using Nistec.Printing;
    using System;
    using System.Xml;
    using System.Data;

    public class AdoColumn:AdoField
    {
        //private Type _dataType;
        private bool _isKey;
        private int _length;
        //private string _name;
        //private int _ordinal;
        private object _tag;
        //private string _caption;

        internal AdoColumn()
        {
            this._name = "";
            this._type = typeof(string);
            this._length = 0xff;
            this._isKey = false;
            this._ordinal = 0;
            this._tag = null;
        }

        public AdoColumn(string name)
        {
            this._name = DataHelper.ValidName(name);
            this._type = typeof(string);
            this._length = 0xff;
            this._isKey = false;
            this._ordinal = 0;
            this._tag = null;
        }
        public AdoColumn(DataColumn column)
            :this(column.ColumnName,column.DataType,column.Unique)
        {
        }

        public AdoColumn(string name, Type dataType)
        {
            this._name = DataHelper.ValidName(name);
            this._type = dataType;
            if (dataType == typeof(string))
            {
                this._length = 0xff;
            }
            else
            {
                this._length = 0;
            }
            this._isKey = false;
            this._ordinal = 0;
            this._tag = null;
        }

        public AdoColumn(string name, Type dataType, bool isKey)
        {
            this._name = DataHelper.ValidName(name);
            this._type = dataType;
            if (dataType == typeof(string))
            {
                this._length = 0xff;
            }
            else
            {
                this._length = 0;
            }
            this._isKey = isKey;
            this._ordinal = 0;
            this._tag = null;
        }

        public AdoColumn(string name, Type dataType, int Length)
        {
            this._name = DataHelper.ValidName(name);
            this._type = dataType;
            if (dataType == typeof(string))
            {
                this._length = Length;
            }
            else
            {
                this._length = 0;
            }
            this._isKey = false;
            this._ordinal = 0;
            this._tag = null;
        }

        public AdoColumn(string name, Type dataType, int Length, bool isKey)
        {
            this._name = DataHelper.ValidName(name);
            this._type = dataType;
            if (dataType == typeof(string))
            {
                this._length = Length;
            }
            else
            {
                this._length = 0;
            }
            this._isKey = isKey;
            this._ordinal = 0;
            this._tag = null;
        }

        public AdoColumn Clone()
        {
            AdoColumn column = new AdoColumn(this._name, Type.GetType(this._type.FullName), this._length, this._isKey);
            column.SetOrdinal(this._ordinal);
            column.Tag = this._tag;
            return column;
        }

        internal void LoadFromXml(XmlNode node)
        {
            XmlNode node2 = node.SelectSingleNode("Name");
            if (node2 != null)
            {
                this._name = node2.FirstChild.Value;
            }
            XmlNode node3 = node.SelectSingleNode("DataType");
            if (node3 != null)
            {
                this._type = Type.GetType(node3.FirstChild.Value);
            }
            XmlNode node4 = node.SelectSingleNode("Length");
            if (node4 != null)
            {
                try
                {
                    this._length = Convert.ToInt32(node4.FirstChild.Value);
                }
                catch
                {
                }
            }
            XmlNode node5 = node.SelectSingleNode("IsKey");
            if (node5 != null)
            {
                try
                {
                    this._isKey = Convert.ToBoolean(node5.FirstChild.Value);
                }
                catch
                {
                }
            }
        }

        internal void SaveToXml(XmlNode node)
        {
            XmlNode newChild = node.OwnerDocument.CreateElement("Name");
            newChild.AppendChild(node.OwnerDocument.CreateTextNode(this._name));
            node.AppendChild(newChild);
            XmlNode node3 = node.OwnerDocument.CreateElement("DataType");
            node3.AppendChild(node.OwnerDocument.CreateTextNode(this._type.FullName));
            node.AppendChild(node3);
            if (this._length > 0)
            {
                XmlNode node4 = node.OwnerDocument.CreateElement("Length");
                node4.AppendChild(node.OwnerDocument.CreateTextNode(this._length.ToString()));
                node.AppendChild(node4);
            }
            if (this._isKey)
            {
                XmlNode node5 = node.OwnerDocument.CreateElement("IsKey");
                node5.AppendChild(node.OwnerDocument.CreateTextNode(this._isKey.ToString()));
                node.AppendChild(node5);
            }
        }

        internal void SetOrdinal(int ordinal)
        {
            this._ordinal = ordinal;
        }

        public override Type DataType
        {
            get
            {
                return base.DataType;
            }
            set
            {
                if (base.DataType != value)
                {
                    base.DataType = value;
                    if (base.DataType == typeof(string))
                    {
                        this._length = 0xff;
                    }
                    else
                    {
                        this._length = 0;
                    }
                }
            }
        }

        public bool IsKey
        {
            get
            {
                return this._isKey;
            }
            set
            {
                this._isKey = value;
            }
        }

        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        //public string ColumnName
        //{
        //    get
        //    {
        //        return this._name;
        //    }
        //    set
        //    {
        //        if ((value == null) || (value == ""))
        //        {
        //            throw new InvalidOperationException("Invalid value for Name property");
        //        }
        //        this._name = DataHelper.SafeName(value);
        //    }
        //}

        //public int Ordinal
        //{
        //    get
        //    {
        //        return this._ordinal;
        //    }
        //}

        public object Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }

        //public string Caption
        //{
        //    get
        //    {
        //        return this._caption;
        //    }
        //    set
        //    {
        //        if ((value == null) || (value == ""))
        //        {
        //            _caption=_name;
        //        }
        //        this._caption = value;
        //    }
        //}

        //public bool IsEmpty
        //{
        //    get { return string.IsNullOrEmpty(ColumnName); }
        //}

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Caption))
                return ColumnName;
            return Caption;
        }
    }
}

