namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McLine : McReportControl
    {
        private Color _var0;
        private Nistec.Printing.View.LineStyle _var1;
        private float _var10;
        private float _var2;
        private float _var3;
        private float _var4;
        private float _var5;
        private float _var6;
        private float _var7;
        private float _var8;
        private float _var9;

        public McLine()
        {
            this.var11();
        }

        public McLine(string name)
        {
            base._mtd91 = name;
            this.var11();
        }

        public override void SetSize()
        {
            base.SetSize();
            this._var3 /= ReportUtil.Dpi;
            this._var4 /= ReportUtil.Dpi;
            this._var5 /= ReportUtil.Dpi;
            this._var6 /= ReportUtil.Dpi;
            this._var7 /= ReportUtil.Dpi;
            this._var8 /= ReportUtil.Dpi;
            this._var9 /= ReportUtil.Dpi;
            this._var10 /= ReportUtil.Dpi;
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            mtd10.mtd15(ref g, this._var3, this._var5, this._var4, this._var6, this._var0, this._var1, this._var2);
        }

        public bool ShouldSerializeLineColor()
        {
            return (this.LineColor != Color.Black);
        }

        public bool ShouldSerializeLineStyle()
        {
            return (this.LineStyle != Nistec.Printing.View.LineStyle.Solid);
        }

        private void var11()
        {
            base._mtd66 = ControlType.Line;
            this._var0 = Color.Black;
            this._var1 = Nistec.Printing.View.LineStyle.Solid;
            this._var2 = 1f;
        }

        private void var12()
        {
            if (this._var3 > this._var4)
            {
                this._mtd33.X = this._var4;
                this._mtd32.Width = this._var3 - this._var4;
            }
            else
            {
                this._mtd33.X = this._var3;
                this._mtd32.Width = this._var4 - this._var3;
            }
            if (this._var5 > this._var6)
            {
                this._mtd33.Y = this._var6;
                this._mtd32.Height = this._var5 - this._var6;
            }
            else
            {
                this._mtd33.Y = this._var5;
                this._mtd32.Height = this._var6 - this._var5;
            }
            this._var7 = this._var3 - this._mtd33.X;
            this._var8 = this._var4 - this._mtd33.X;
            this._var9 = this._var5 - this._mtd33.Y;
            this._var10 = this._var6 - this._mtd33.Y;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Nistec.Printing.View.Border Border
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86), Browsable(false)]
        public override Color BorderBottomColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override BorderLineStyle BorderBottomStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
        public override Color BorderLeftColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never)]
        public override BorderLineStyle BorderLeftStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
        public override Color BorderRightColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
        public override BorderLineStyle BorderRightStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [mtd85(mtd88.mtd86), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BorderTopColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never)]
        public override BorderLineStyle BorderTopStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override RectangleF Bounds
        {
            get
            {
                return base.Bounds;
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never)]
        public override string DataField
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float Height
        {
            get
            {
                return base.Height;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float Left
        {
            get
            {
                return base.Left;
            }
        }

        [Category("Appearance"), mtd85(mtd88.mtd86), Description("Color of the line control")]
        public Color LineColor
        {
            get
            {
                return this._var0;
            }
            set
            {
                if (this._var0 != value)
                {
                    mtd69.mtd70(mtd26.BorderColor, mtd56.mtd60, this._var0, value);
                    this._var0 = value;
                    base.mtd93(this);
                }
            }
        }

        [Category("Appearance"), mtd85(mtd88.mtd86), Description("Pen style used to draw the line")]
        public Nistec.Printing.View.LineStyle LineStyle
        {
            get
            {
                return this._var1;
            }
            set
            {
                if (this._var1 != value)
                {
                    mtd69.mtd70(mtd26.mtd42, mtd56.mtd60, this._var1, value);
                    this._var1 = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Pen width of the line in pixels"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public float LineWeight
        {
            get
            {
                return this._var2;
            }
            set
            {
                if (this._var2 != value)
                {
                    mtd69.mtd70(mtd26.mtd43, mtd56.mtd60, this._var2, value);
                    this._var2 = value;
                    base.mtd93(this);
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PointF Location
        {
            get
            {
                return base.Location;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override SizeF Size
        {
            get
            {
                return base.Size;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override float Top
        {
            get
            {
                return base.Top;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override float Width
        {
            get
            {
                return base.Width;
            }
        }

        [Description("Horizontal coordinate of the line's starting point"), TypeConverter(typeof(UISizeConverter)), mtd85(mtd88.mtd86), Category("Layout")]
        public float X1
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (this._var3 != value)
                {
                    mtd69.mtd70(mtd26.mtd44, mtd56.mtd61, this._var3, value);
                    this._var3 = value;
                    this.var12();
                    base.mtd93(this);
                }
            }
        }

        [Description("Horizontal coordinate of the line's end point"), mtd85(mtd88.mtd86), TypeConverter(typeof(UISizeConverter)), Category("Layout")]
        public float X2
        {
            get
            {
                return this._var4;
            }
            set
            {
                if (this._var4 != value)
                {
                    mtd69.mtd70(mtd26.mtd46, mtd56.mtd61, this._var4, value);
                    this._var4 = value;
                    this.var12();
                    base.mtd93(this);
                }
            }
        }

        [Description("Vertical coordinate of the line's starting point"), TypeConverter(typeof(UISizeConverter)), Category("Layout"), mtd85(mtd88.mtd86)]
        public float Y1
        {
            get
            {
                return this._var5;
            }
            set
            {
                if (this._var5 != value)
                {
                    mtd69.mtd70(mtd26.mtd45, mtd56.mtd61, this._var5, value);
                    this._var5 = value;
                    this.var12();
                    base.mtd93(this);
                }
            }
        }

        [mtd85(mtd88.mtd86), TypeConverter(typeof(UISizeConverter)), Category("Layout"), Description("Vertical coordinate of the line's end point")]
        public float Y2
        {
            get
            {
                return this._var6;
            }
            set
            {
                if (this._var6 != value)
                {
                    mtd69.mtd70(mtd26.mtd47, mtd56.mtd61, this._var6, value);
                    this._var6 = value;
                    this.var12();
                    base.mtd93(this);
                }
            }
        }
    }
}

