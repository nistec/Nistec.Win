namespace Nistec.Printing.View
{
    using System;
    using System.Xml;

    public class XmlDataSource
    {
        internal XmlNode _mtd187 = null;
        private string _var0;
        private bool _var1;
        private string _var2;
        private Nistec.Printing.View.DataFieldSchemaList _var3 = new Nistec.Printing.View.DataFieldSchemaList();
        private bool _var4 = true;
        private XmlDocument _var5;
        private XmlNamespaceManager _var6;
        private System.Xml.XmlNodeList _var7 = null;
        private int _var8;

        internal void mtd111()
        {
            this._var5 = null;
            this._var6 = null;
            this._var7 = null;
            this._mtd187 = null;
            this._var8 = 0;
        }

        internal void mtd172()
        {
            if ((this._var4 && (this._var2 != null)) && (this._var2.Length != 0))
            {
                XmlReader reader;
                this._var5 = new XmlDocument();
                this._mtd187 = null;
                this._var7 = null;
                this._var6 = null;
                XmlReader reader2 = new XmlTextReader(this._var2);
                if (this._var1)
                {
                    reader = XmlReader.Create(reader2, new XmlReaderSettings());// new XmlValidatingReader(reader2);
                }
                else
                {
                    reader = reader2;
                }
                this._var5.Load(reader);
                reader.Close();
                if (reader != null)
                {
                    ((IDisposable) reader).Dispose();
                }
                if (this._var1 && (reader2 != null))
                {
                    reader2.Close();
                }
                this._var6 = new XmlNamespaceManager(this._var5.NameTable);
                try
                {
                    if ((this._var5.DocumentElement != null) && (this._var5.DocumentElement.Attributes != null))
                    {
                        XmlAttributeCollection attributes = this._var5.DocumentElement.Attributes;
                        int count = attributes.Count;
                        for (int i = 0; i < count; i++)
                        {
                            XmlAttribute attribute = attributes[i];
                            if (attribute.Prefix == "xmlns")
                            {
                                this._var6.AddNamespace(attribute.LocalName, attribute.Value);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                this._var4 = false;
                this.var9();
            }
        }

        internal bool mtd184(int var11)
        {
            if (var11 < this._var8)
            {
                this._mtd187 = this._var7[var11];
                return true;
            }
            this._mtd187 = null;
            return false;
        }

        internal object mtd185(string var10)
        {
            if ((this._mtd187 != null) && (var10 != null))
            {
                XmlNode node;
                if (this._var6 != null)
                {
                    node = this._mtd187.SelectSingleNode(var10, this._var6);
                }
                else
                {
                    node = this._mtd187.SelectSingleNode(var10);
                }
                if (node != null)
                {
                    return node.InnerText;
                }
                System.Xml.XmlNodeList childNodes = this._mtd187.ChildNodes;
                if ((childNodes != null) && (childNodes.Count != 0))
                {
                    int count = childNodes.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (childNodes[i].Name == var10)
                        {
                            return childNodes[i].InnerText;
                        }
                    }
                }
            }
            return DBNull.Value;
        }

        public void AddDataFieldSchema(string fieldname, TypeCode typecode)
        {
            this._var3.Add(new DataFieldSchema(fieldname, typecode));
        }

        public System.Xml.XmlNodeList SelectNodes(string xpath)
        {
            if ((this._mtd187 == null) || (xpath == null))
            {
                return null;
            }
            if (this._var6 != null)
            {
                return this._mtd187.SelectNodes(xpath, this._var6);
            }
            return this._mtd187.SelectNodes(xpath);
        }

        private void var9()
        {
            this._var7 = null;
            string xpath = this._var0;
            if ((xpath == null) || (xpath.Length == 0))
            {
                XmlElement documentElement = this._var5.DocumentElement;
                if (documentElement == null)
                {
                    throw new ArgumentNullException("xmlDataSource", "XML document element not found");
                }
                this._var7 = documentElement.ChildNodes;
            }
            else
            {
                try
                {
                    if (this._var6 != null)
                    {
                        this._var7 = this._var5.SelectNodes(xpath, this._var6);
                    }
                    else
                    {
                        this._var7 = this._var5.SelectNodes(xpath);
                    }
                }
                catch (Exception exception)
                {
                    try
                    {
                        if (this._var6 != null)
                        {
                            if (xpath.EndsWith("/") || xpath.EndsWith(@"\"))
                            {
                                this._var7 = this._var5.SelectNodes(xpath + "*", this._var6);
                            }
                            else
                            {
                                this._var7 = this._var5.SelectNodes(xpath + "/*", this._var6);
                            }
                        }
                        else if (xpath.EndsWith("/") || xpath.EndsWith(@"\"))
                        {
                            this._var7 = this._var5.SelectNodes(xpath + "*");
                        }
                        else
                        {
                            this._var7 = this._var5.SelectNodes(xpath + "/*");
                        }
                        return;
                    }
                    catch (Exception)
                    {
                        throw exception;
                    }
                }
            }
            if (this._var7 != null)
            {
                this._var8 = this._var7.Count;
            }
            else
            {
                this._var8 = 0;
            }
        }

        internal XmlNode mtd188
        {
            get
            {
                return this._mtd187;
            }
        }

        public int Count
        {
            get
            {
                this.mtd172();
                return this._var8;
            }
        }

        public Nistec.Printing.View.DataFieldSchemaList DataFieldSchemaList
        {
            get
            {
                return this._var3;
            }
        }

        public string FileUrl
        {
            get
            {
                return this._var2;
            }
            set
            {
                if (this._var2 != value)
                {
                    this._var2 = value;
                    this._var4 = true;
                }
            }
        }

        public string RecordPattern
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        public bool ValidateOnPrase
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        public System.Xml.XmlNodeList XmlNodeList
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
                if (this._var7 != null)
                {
                    this._var4 = false;
                    this._var8 = this._var7.Count;
                }
                else
                {
                    this._var8 = 0;
                }
            }
        }
    }
}

