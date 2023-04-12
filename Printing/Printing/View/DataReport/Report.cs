namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data.Common;
    using System.IO;
    using System.Reflection;
    using System.Drawing;

    public class Report : Component, ISupportInitialize
    {

        public static readonly float DefaultHeight = 24F;
        public static readonly float DefaultWidth = 100F;
        public static Font DefaultFont { get { return new Font("Microsoft Sans Serif", 8.25f); } }

        private bool _RightToLeft;

        private Report _ParentReport;
        private SectionCollection _Sections;
        private string _Script;
        private Nistec.Printing.View.ScriptLanguage _ScriptLanguage = Nistec.Printing.View.ScriptLanguage.VB;
        private FieldsCollection _Fields = new FieldsCollection();
        private System.Data.Common.DataAdapter _DataAdapter;
        private object _DataSource;
        private bool _EOF = false;
        private Nistec.Printing.View.Document _Document;
        //private bool _var19 = false;
        private Nistec.Printing.View.DataFields _DataFields;
        private string _Version = "4.0.2.0";
        private float _ReportWidth;
        private bool _IsDirty;
        private long _MaxPages = 0L;
        protected PageSettings _PageSettings = new PageSettings();//mtd384

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler mtd385;//_var5

        [Description(""), Category("Events")]
        public event EventHandler DataFetch;

        [Category("Events"), Description("")]
        public event EventHandler DataInitialize;

        [Category("Events"), Description("")]
        public event EventHandler NoData;

        [Description(""), Category("Events")]
        public event EventHandler ReportEnd;//_var3

        [Category("Events"), Description("")]
        public event EventHandler ReportStart;//_var4

        internal bool mtd117(Msg var21)
        {
            switch (var21)
            {
                case Msg.ReportStart:
                    if (this.ReportStart == null)
                    {
                        break;
                    }
                    this.ReportStart(this, EventArgs.Empty);
                    return true;

                case Msg.ReportEnd:
                    if (this.ReportEnd == null)
                    {
                        break;
                    }
                    this.ReportEnd(this, EventArgs.Empty);
                    return true;

                case Msg.NoData:
                    if (this.NoData == null)
                    {
                        break;
                    }
                    this.NoData(this, EventArgs.Empty);
                    return true;

                case Msg.DataInitialize:
                    if (this.DataInitialize == null)
                    {
                        break;
                    }
                    this.DataInitialize(this, EventArgs.Empty);
                    return true;

                case Msg.DataFetch:
                    if (this.DataFetch == null)
                    {
                        break;
                    }
                    this.DataFetch(this, EventArgs.Empty);
                    return true;
            }
            return false;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public void Generate()
        {
            //if (!this._var19)
            //{
                this.Document.InitDocumentIntrenal();
            //}
        }

        public object GetCurrentDBValue(int columnindex)
        {
            if (this._DataFields != null)
            {
                return this._DataFields.mtd193(columnindex);
            }
            return DBNull.Value;
        }

        public object GetCurrentDBValue(string columnname)
        {
            if (this._DataFields != null)
            {
                return this._DataFields.mtd193(columnname);
            }
            return DBNull.Value;
        }

        public void LoadLayout(ref Stream stream)
        {
            StreamUtil.mtd87(stream, this);
            //this._var19 = false;
        }

        public void LoadLayout(string fileName)
        {
            StreamUtil.mtd87(fileName, this);
            //this._var19 = false;
        }

        public void LoadLayout(string fileName, IDesignerHost host)
        {
            StreamUtil.mtd87(fileName, this, host);
            //this._var19 = false;
        }

        public void LoadLayout(ref Stream stream, IDesignerHost host)
        {
            StreamUtil.mtd87(stream, this, host);
            //this._var19 = false;
        }

        public void LoadLayout(Type type, string Resource)
        {
            Stream manifestResourceStream = Assembly.GetAssembly(type).GetManifestResourceStream(Resource);
            this._Sections = new SectionCollection();
            StreamUtil.mtd87(manifestResourceStream, this);
            //this._var19 = false;
        }

        public void SaveLayout(string fileName)
        {
            StreamUtil.mtd84(fileName, this);
            //this._var19 = false;
        }

        public void SaveLayout(ref Stream stream)
        {
            StreamUtil.mtd84(ref stream, this);
            //this._var19 = false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Nistec.Printing.View.DataFields DataFields//mtd170
        {
            get
            {
                return this._DataFields;
            }
            set
            {
                this._DataFields = value;
            }
        }

        [Category("Data"), Description("")]
        public System.Data.Common.DataAdapter DataAdapter
        {
            get
            {
                return this._DataAdapter;
            }
            set
            {
                if (this._DataAdapter != value)
                {
                    this._DataAdapter = value;
                    if (this.mtd385 != null)
                    {
                        this.mtd385(this._DataAdapter, EventArgs.Empty);
                    }
                }
            }
        }

        [Description(""), Category("Data"), TypeConverter(typeof(mtd76))]
        public object DataSource
        {
            get
            {
                return this._DataSource;
            }
            set
            {
                if (this._DataSource != value)
                {
                    this._DataSource = value;
                    if (this.mtd385 != null)
                    {
                        this.mtd385(this._DataSource, EventArgs.Empty);
                    }
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Nistec.Printing.View.Document Document
        {
            get
            {
                if (this._Document == null)
                {
                    this._Document = new Nistec.Printing.View.Document(this);
                }
                return this._Document;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool EOF
        {
            get
            {
                return this._EOF;
            }
            set
            {
                this._EOF = value;
            }
        }

        [Browsable(false)]
        public FieldsCollection Fields
        {
            get
            {
                return this._Fields;
            }
        }

        [Browsable(false)]
        public bool IsDirty
        {
            get
            {
                return this._IsDirty;
            }
            set
            {
                this._IsDirty = value;
            }
        }

        [Browsable(false)]
        public long MaxPages
        {
            get
            {
                return this._MaxPages;
            }
            set
            {
                this._MaxPages = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PageSettings PageSetting
        {
            get
            {
                return this._PageSettings;
            }
            set
            {
                this._PageSettings = value;
            }
        }

        [Browsable(false)]
        public Report ParentReport
        {
            get
            {
                return this._ParentReport;
            }
            set
            {
                this._ParentReport = value;
            }
        }

        [Description("Indicates width of Report"), Category("Layout"), TypeConverter(typeof(UISizeConverter))]
        public float ReportWidth
        {
            get
            {
                return this._ReportWidth;
            }
            set
            {
                this._ReportWidth = value;
            }
        }

        [Browsable(false)]
        public string Script
        {
            get
            {
                return this._Script;
            }
            set
            {
                this._Script = value;
            }
        }

        [Category("Design"), Description("Indicates Script Language for Report")]
        public Nistec.Printing.View.ScriptLanguage ScriptLanguage
        {
            get
            {
                return this._ScriptLanguage;
            }
            set
            {
                this._ScriptLanguage = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SectionCollection Sections
        {
            get
            {
                if (this._Sections == null)
                {
                    this._Sections = new SectionCollection();
                }
                return this._Sections;
            }
        }

        [Description("Indicates Version of Report"), Category("Design")]
        public string Version
        {
            get
            {
                return this._Version;
            }
        }

        [Description("Indicates Direction of Report"), Category("Design")]
        public bool RightToLeft
        {
            get
            {
                return this._RightToLeft;
            }
            set
            {
                this._RightToLeft = value;
            }
        }
    }
}

