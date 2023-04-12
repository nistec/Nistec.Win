namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class DocumentProperties
    {
        [CompilerGenerated]
        private string _Author;
        [CompilerGenerated]
        private string _Company;
        [CompilerGenerated]
        private string _LastAuthor;
        [CompilerGenerated]
        private string _Manager;
        [CompilerGenerated]
        private string _Subject;
        [CompilerGenerated]
        private string _Title;

        public DocumentProperties()
        {
            this.Author = "";
            this.LastAuthor = "";
            this.Manager = "";
            this.Company = "";
            this.Subject = "";
            this.Title = "";
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("", "DocumentProperties", "urn:schemas-microsoft-com:office:office");
            if (!string.IsNullOrEmpty(this.Author))
            {
                writer.WriteElementString("Author", this.Author);
            }
            if (!string.IsNullOrEmpty(this.LastAuthor))
            {
                writer.WriteElementString("LastAuthor", this.LastAuthor);
            }
            if (!string.IsNullOrEmpty(this.Manager))
            {
                writer.WriteElementString("Manager", this.Manager);
            }
            if (!string.IsNullOrEmpty(this.Company))
            {
                writer.WriteElementString("Company", this.Company);
            }
            if (!string.IsNullOrEmpty(this.Subject))
            {
                writer.WriteElementString("Subject", this.Subject);
            }
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.WriteElementString("Title", this.Title);
            }
            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            while (reader.Read() && ((reader.Name == "DocumentProperties") && (reader.NodeType != XmlNodeType.EndElement)))
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    if (name != null)
                    {
                        if (!(name == "Author"))
                        {
                            if (name == "LastAuthor")
                            {
                                goto Label_00C6;
                            }
                            if (name == "Manager")
                            {
                                goto Label_0102;
                            }
                            if (name == "Company")
                            {
                                goto Label_013E;
                            }
                            if (name == "Subject")
                            {
                                goto Label_0177;
                            }
                            if (name == "Title")
                            {
                                goto Label_01AD;
                            }
                        }
                        else
                        {
                            if (reader.IsEmptyElement)
                            {
                                continue;
                            }
                            reader.Read();
                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                this.Author = reader.Value;
                            }
                        }
                    }
                }
                goto Label_01E4;
            Label_00C6:
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    this.LastAuthor = reader.Value;
                }
                goto Label_01E4;
            Label_0102:
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    this.Manager = reader.Value;
                }
                goto Label_01E4;
            Label_013E:
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    this.Company = reader.Value;
                }
                goto Label_01E4;
            Label_0177:
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    this.Subject = reader.Value;
                }
                goto Label_01E4;
            Label_01AD:
                if (reader.IsEmptyElement)
                {
                    continue;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    this.Title = reader.Value;
                }
            Label_01E4:;
            }
        }

        public string Author
        {
            [CompilerGenerated]
            get
            {
                return this._Author;
            }
            [CompilerGenerated]
            set
            {
                this._Author = value;
            }
        }

        public string Company
        {
            [CompilerGenerated]
            get
            {
                return this._Company;
            }
            [CompilerGenerated]
            set
            {
                this._Company = value;
            }
        }

        public string LastAuthor
        {
            [CompilerGenerated]
            get
            {
                return this._LastAuthor;
            }
            [CompilerGenerated]
            set
            {
                this._LastAuthor = value;
            }
        }

        public string Manager
        {
            [CompilerGenerated]
            get
            {
                return this._Manager;
            }
            [CompilerGenerated]
            set
            {
                this._Manager = value;
            }
        }

        public string Subject
        {
            [CompilerGenerated]
            get
            {
                return this._Subject;
            }
            [CompilerGenerated]
            set
            {
                this._Subject = value;
            }
        }

        public string Title
        {
            [CompilerGenerated]
            get
            {
                return this._Title;
            }
            [CompilerGenerated]
            set
            {
                this._Title = value;
            }
        }
    }
}

