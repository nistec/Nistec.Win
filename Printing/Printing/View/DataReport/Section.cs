namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class Section : Component
    {
        protected bool _mtd112 = false;
        protected NewPage _mtd113 = NewPage.None;
        protected bool _mtd114 = false;
        protected GroupKeepTogether _mtd115 = GroupKeepTogether.None;
        public object _mtd116;
        protected SectionType _mtd66;
        protected string _mtd91;
        private bool _var0 = true;
        private bool _var1 = true;
        private bool _var2 = false;
        private float _var3;
        private Color _var4 = Color.Transparent;
        private McControlCollection _var5 = new McControlCollection();
        private bool _var8;

        [Description(""), Category("Events")]
        public event EventHandler Initialize;

        [Description(""), Category("Events")]
        public event EventHandler OnPrint;

        internal bool mtd117(Msg msg)
        {
            switch (msg)
            {
                case Msg.Initialize:
                    if (this.Initialize == null)
                    {
                        break;
                    }
                    this.Initialize(this, EventArgs.Empty);
                    return true;

                case Msg.OnPrint:
                    if (this.OnPrint == null)
                    {
                        break;
                    }
                    this.OnPrint(this, EventArgs.Empty);
                    return true;
            }
            return false;
        }

        internal void mtd97()
        {
            this._var3 /= ReportUtil.Dpi;
        }

        public void DrawCtl(object sender, PaintEventArgs e)
        {
            foreach (McReportControl control in this._var5)
            {
                control.DrawCtl(this, e);
            }
        }

        public bool ShouldSerializeBackColor()
        {
            return (this.BackColor != Color.Transparent);
        }

        [Description("The backcolor used to display controls in the section"), mtd85(mtd88.mtd86), Category("Appearance")]
        public Color BackColor
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
            }
        }

        [Category("Appearance"), Description("Indicates whether or not the section can grow to accomodate control"), DefaultValue(true), mtd85(mtd88.mtd86)]
        public bool CanGrow
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

        [DefaultValue(false), Description("Indicates whether or not the control can shrink to accomodate control"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public bool CanShrink
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public McControlCollection Controls
        {
            get
            {
                return this._var5;
            }
        }

        [mtd85(mtd88.mtd86), Description("Indicates Height of section"), Category("Layout"), TypeConverter(typeof(UISizeConverter))]
        public float Height
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                this._var8 = value;
            }
        }

        [MergableProperty(false), Category("Design"), Description("Indicates name used in code to identify the object"), ParenthesizePropertyName(true)]
        public string Name
        {
            get
            {
                if (base.Site == null)
                {
                    return this._mtd91;
                }
                return base.Site.Name;
            }
            set
            {
                this._mtd91 = value;
                if (base.Site != null)
                {
                    base.Site.Name = value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Tag
        {
            get
            {
                return this._mtd116;
            }
            set
            {
                this._mtd116 = value;
            }
        }

        [ReadOnly(true), Category("Design"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SectionType Type
        {
            get
            {
                return this._mtd66;
            }
        }

        [Description("Determines whether the section is visible or hidden"), mtd85(mtd88.mtd86), Category("Design"), DefaultValue(true)]
        public bool Visible
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
    }
}

