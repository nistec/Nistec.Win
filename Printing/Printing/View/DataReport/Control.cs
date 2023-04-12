namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McReportControl : Component
    {


        public static readonly float DefaultHeight = 24F;
        public static readonly float DefaultWidth = 100F;
        public static Font DefaultFont { get { return new Font("Microsoft Sans Serif", 8.25f); } }

        protected SizeF _mtd32 = new SizeF();
        protected PointF _mtd33 = new PointF();
        protected ControlType _mtd66;
        protected string _mtd91;
        protected bool _mtd92;
        protected Nistec.Printing.View.Border _mtd99 = new Nistec.Printing.View.Border();
        private object _var0;
        private object _var1;
        private bool _var2 = true;
        private string _var3;
        private int _var4;
        private bool _var7;

        [Browsable(false)]
        internal event mtd24 mtd101;

        [Description(""), Category("Events")]
        public event EventHandler ReSize;//_var6

        //mtd100
        protected void DrawRect (Graphics g)
        {
            using (Pen pen = new Pen(Color.LightGray, 1f))
            {
                g.DrawRectangle(pen, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            }
        }

        protected void mtd93(object sender)
        {
            mtd69.mtd71(this, this.mtd101);
        }
        //mtd94
        protected void OnReSize ()
        {
            if (this.ReSize != null)
            {
                this.ReSize(this, EventArgs.Empty);
            }
        }

        //mtd97
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void SetSize ()
        {
            this._mtd33 = new PointF(this._mtd33.X / ReportUtil.Dpi, this._mtd33.Y / ReportUtil.Dpi);
            this._mtd32 = new SizeF(this._mtd32.Width / ReportUtil.Dpi, this._mtd32.Height / ReportUtil.Dpi);
        }
        //mtd98
        public virtual void DrawCtl (object sender, PaintEventArgs e)
        {
            this.DrawRect(e.Graphics);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._var0 = null;
                this._var1 = null;
            }
            base.Dispose(disposing);
        }

        internal static bool IsDefaultFont(Font var8)
        {
            if ((!(var8.Name != "Microsoft Sans Serif") && (var8.Size == 8f)) && (var8.Style == FontStyle.Regular))
            {
                return false;
            }
            return true;
        }

        public bool ShouldSerializeBorder()
        {
            return (this.Border != null);
        }

        public bool ShouldSerializeBorderBottomColor()
        {
            return (this.BorderBottomColor != Color.Black);
        }

        public bool ShouldSerializeBorderBottomStyle()
        {
            return (this.BorderBottomStyle != BorderLineStyle.None);
        }

        public bool ShouldSerializeBorderLeftColor()
        {
            return (this.BorderLeftColor != Color.Black);
        }

        public bool ShouldSerializeBorderLeftStyle()
        {
            return (this.BorderLeftStyle != BorderLineStyle.None);
        }

        public bool ShouldSerializeBorderRightColor()
        {
            return (this.BorderRightColor != Color.Black);
        }

        public bool ShouldSerializeBorderRightStyle()
        {
            return (this.BorderRightStyle != BorderLineStyle.None);
        }

        public bool ShouldSerializeBorderTopColor()
        {
            return (this.BorderTopColor != Color.Black);
        }

        public bool ShouldSerializeBorderTopStyle()
        {
            return (this.BorderTopStyle != BorderLineStyle.None);
        }

        public bool ShouldSerializeDataField()
        {
            return (this._var3 != null);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual Nistec.Printing.View.Border Border
        {
            get
            {
                return this._mtd99;
            }
            set
            {
                this._mtd99 = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual Color BorderBottomColor
        {
            get
            {
                return this._mtd99.BorderBottomColor;
            }
            set
            {
                this._mtd99.BorderBottomColor = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual BorderLineStyle BorderBottomStyle
        {
            get
            {
                return this._mtd99.BorderBottomStyle;
            }
            set
            {
                this._mtd99.BorderBottomStyle = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual Color BorderLeftColor
        {
            get
            {
                return this._mtd99.BorderLeftColor;
            }
            set
            {
                this._mtd99.BorderLeftColor = value;
            }
        }

        [mtd85(mtd88.mtd86), Browsable(false)]
        public virtual BorderLineStyle BorderLeftStyle
        {
            get
            {
                return this._mtd99.BorderLeftStyle;
            }
            set
            {
                this._mtd99.BorderLeftStyle = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual Color BorderRightColor
        {
            get
            {
                return this._mtd99.BorderRightColor;
            }
            set
            {
                this._mtd99.BorderRightColor = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual BorderLineStyle BorderRightStyle
        {
            get
            {
                return this._mtd99.BorderRightStyle;
            }
            set
            {
                this._mtd99.BorderRightStyle = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual Color BorderTopColor
        {
            get
            {
                return this._mtd99.BorderTopColor;
            }
            set
            {
                this._mtd99.BorderTopColor = value;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86)]
        public virtual BorderLineStyle BorderTopStyle
        {
            get
            {
                return this._mtd99.BorderTopStyle;
            }
            set
            {
                this._mtd99.BorderTopStyle = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual RectangleF Bounds
        {
            get
            {
                return new RectangleF(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            }
            set
            {
                this.Location = value.Location;
                this.Size = value.Size;
            }
        }

        [Description("Indicates the datafield to which the control is bound"), mtd85(mtd88.mtd86), Category("Data"), Editor(typeof(mtd79), typeof(UITypeEditor))]
        public virtual string DataField
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

        [Category("Layout"), Description("Indicates Height of Control"), TypeConverter(typeof(UISizeConverter)), mtd85(mtd88.mtd86)]
        public virtual float Height
        {
            get
            {
                return this._mtd32.Height;
            }
            set
            {
                if (this._mtd32.Height != value)
                {
                    mtd69.mtd70(mtd26.mtd31, mtd56.mtd57, this._mtd32.Height, value);
                    this._mtd32.Height = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                    if (this.ReSize != null)
                    {
                        this.ReSize(this, EventArgs.Empty);
                    }
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Index
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                return this._mtd92;
            }
            set
            {
                this._mtd92 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsValidData
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
            }
        }

        [mtd85(mtd88.mtd86), TypeConverter(typeof(UISizeConverter)), Category("Layout"), Description("Indicates left of control")]
        public virtual float Left
        {
            get
            {
                return this._mtd33.X;
            }
            set
            {
                if (this._mtd33.X != value)
                {
                    mtd69.mtd70(mtd26.mtd28, mtd56.mtd57, this._mtd33.X, value);
                    this._mtd33.X = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual PointF Location
        {
            get
            {
                return this._mtd33;
            }
            set
            {
                if (this._mtd33 != value)
                {
                    mtd69.mtd70(mtd26.mtd33, mtd56.mtd57, this._mtd33, value);
                    this._mtd33 = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                }
            }
        }

        [ParenthesizePropertyName(true), MergableProperty(false), Category("Design"), Description("Indicates name used in code to identify the object")]
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

        [Browsable(false)]
        public object Parent
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SizeF Size
        {
            get
            {
                return this._mtd32;
            }
            set
            {
                if (this._mtd32 != value)
                {
                    mtd69.mtd70(mtd26.mtd32, mtd56.mtd57, this._mtd32, value);
                    this._mtd32 = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                    if (this.ReSize != null)
                    {
                        this.ReSize(this, EventArgs.Empty);
                    }
                }
            }
        }

        [MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Tag
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

        [Description("Indicates top of control"), TypeConverter(typeof(UISizeConverter)), Category("Layout"), mtd85(mtd88.mtd86)]
        public virtual float Top
        {
            get
            {
                return this._mtd33.Y;
            }
            set
            {
                if (this._mtd33.Y != value)
                {
                    mtd69.mtd70(mtd26.mtd29, mtd56.mtd57, this._mtd33.Y, value);
                    this._mtd33.Y = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                }
            }
        }

        [MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Design"), Description("Type of Control"), ReadOnly(true)]
        public ControlType Type
        {
            get
            {
                return this._mtd66;
            }
        }

        [mtd85(mtd88.mtd86), DefaultValue(true), Description("Determines whether the control is visible or hidden"), Category("Design")]
        public bool Visible
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

        [TypeConverter(typeof(UISizeConverter)), mtd85(mtd88.mtd86), Category("Layout"), Description("Indicates width of control")]
        public virtual float Width
        {
            get
            {
                return this._mtd32.Width;
            }
            set
            {
                if (this._mtd32.Width != value)
                {
                    mtd69.mtd70(mtd26.mtd30, mtd56.mtd57, this._mtd32.Width, value);
                    this._mtd32.Width = value;
                    this._mtd92 = true;
                    this.mtd93(this);
                    if (this.ReSize != null)
                    {
                        this.ReSize(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}

