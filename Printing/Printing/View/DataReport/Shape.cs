namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McShape : McReportControl
    {
        private Color _var0;
        private Color _var1;
        private Nistec.Printing.View.LineStyle _var2;
        private float _var3;
        private ShapeStyle _var4;

        public McShape()
        {
            this.var5();
        }

        public McShape(string name)
        {
            base._mtd91 = name;
            this.var5();
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            mtd10.mtd16(ref graphics, this.Bounds, this._var4, this._var0, this._var1, this._var2, this._var3);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this._var0 != Color.Transparent);
        }

        public bool ShouldSerializeLineColor()
        {
            return (this._var1 != Color.Black);
        }

        public bool ShouldSerializeLineStyle()
        {
            return (this._var2 != Nistec.Printing.View.LineStyle.Solid);
        }

        public bool ShouldSerializeStyle()
        {
            return (this._var4 != ShapeStyle.Rectangle);
        }

        private void var5()
        {
            base._mtd66 = ControlType.Shape;
            this._var0 = Color.Transparent;
            this._var1 = Color.Black;
            this._var2 = Nistec.Printing.View.LineStyle.Solid;
            this._var3 = 1f;
            this._var4 = ShapeStyle.Rectangle;
        }

        [mtd85(mtd88.mtd86), Category("Appearance"), Description("Indicates the background color of the control area")]
        public Color BackColor
        {
            get
            {
                return this._var0;
            }
            set
            {
                if (this._var0 != value)
                {
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd63, this._var0, value);
                    this._var0 = value;
                    base.mtd93(this);
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
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

        [EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
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

        [mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
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

        [Description("Indicates the color used to paint the control border"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public Color LineColor
        {
            get
            {
                return this._var1;
            }
            set
            {
                if (this._var1 != value)
                {
                    mtd69.mtd70(mtd26.BorderColor, mtd56.mtd63, this._var1, value);
                    this._var1 = value;
                    base.mtd93(this);
                }
            }
        }

        [Category("Appearance"), Description("Indicates the pen style used to paint the shape control border"), mtd85(mtd88.mtd86)]
        public Nistec.Printing.View.LineStyle LineStyle
        {
            get
            {
                return this._var2;
            }
            set
            {
                if (this._var2 != value)
                {
                    mtd69.mtd70(mtd26.mtd42, mtd56.mtd63, this._var2, value);
                    this._var2 = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Indicates the pen width of the line in pixels used to paint the shape control border"), Category("Appearance"), mtd85(mtd88.mtd86), DefaultValue(1)]
        public float LineWeight
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (this._var3 != value)
                {
                    mtd69.mtd70(mtd26.mtd43, mtd56.mtd63, this._var3, value);
                    this._var3 = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Indicates the shape type to be printed"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public ShapeStyle Style
        {
            get
            {
                return this._var4;
            }
            set
            {
                if (this._var4 != value)
                {
                    mtd69.mtd70(mtd26.mtd50, mtd56.mtd63, this._var4, value);
                    this._var4 = value;
                    base.mtd93(this);
                }
            }
        }
    }
}

