namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(McCheckBox), "CheckBox.bmp"), ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McCheckBox : McReportControl
    {
        private string _var0;
        private Color _var1;
        private McField _var10;
        private bool _var11;
        private Color _var2;
        private Font _var3;
        private ContentAlignment _var4;
        private StringFormat _var5;
        private bool _var6;
        private RectangleF _var7;
        private RectangleF _var8;
        private float _var9;

        public McCheckBox()
        {
            this.var12();
        }

        public McCheckBox(string name)
        {
            base._mtd91 = name;
            this.var12();
        }

        public override void SetSize()
        {
            base.SetSize();
            this._var9 /= ReportUtil.Dpi;
            this._var7.Width = 0.125f;
            this._var7.Height = 0.125f;
            this.var13(this.Height);
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            mtd10.mtd17(ref graphics, this.Bounds, ref this._var3, this._var5, this._var1, this._var2, this._var0, this._var6, this._var8, this._var7, ref this._mtd99, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._var3 != null)
                {
                    this._var3.Dispose();
                }
                if (this._var5 != null)
                {
                    this._var5.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this._var1 != Color.Transparent);
        }

        public bool ShouldSerializeCheckAlignment()
        {
            return (this._var4 != ContentAlignment.MiddleLeft);
        }

        public bool ShouldSerializeForeColor()
        {
            return (this.ForeColor != Color.Black);
        }

        public bool ShouldSerializeText()
        {
            return (this.Text != null);
        }

        public bool ShouldSerializeTextFont()
        {
            return McReportControl.IsDefaultFont(this.TextFont);
        }

        private void var12()
        {
            base._mtd66 = ControlType.CheckBox;
            this._var11 = false;
            this._var1 = Color.Transparent;
            this._var2 = Color.Black;
            this._var3 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            this._var4 = ContentAlignment.MiddleLeft;
            this._var5 = (StringFormat) StringFormat.GenericTypographic.Clone();
            this._var5.LineAlignment = StringAlignment.Center;
            this._var5.Alignment = StringAlignment.Near;
            this._var7 = new RectangleF(0f, 0f, 12f, 12f);
            this._var8 = new RectangleF();
            this._var9 = 4f;
        }

        private void var13(float var15)
        {
            if (this._var4 == ContentAlignment.TopLeft)
            {
                this._var7.Location = new PointF(this._var9, this._var9);
                this._var8.Location = new PointF(this._var7.Width + (this._var9 * 2f), this._var9);
                this._var8.Width = (this.Width - this._var7.Width) - (this._var9 * 2f);
                this._var8.Height = var15;
            }
            else if (this._var4 == ContentAlignment.TopCenter)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) / 2f, this._var9);
                this._var8.Location = new PointF(0f, this._var7.Height + (this._var9 * 2f));
                this._var8.Width = this.Width;
                this._var8.Height = var15 - this._var7.Height;
            }
            else if (this._var4 == ContentAlignment.TopRight)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) - this._var9, this._var9);
                this._var8.Location = new PointF(0f, this._var9);
                this._var8.Width = (this.Width - this._var7.Width) - (this._var9 * 2f);
                this._var8.Height = var15;
            }
            else if (this._var4 == ContentAlignment.MiddleLeft)
            {
                this._var7.Location = new PointF(this._var9, (var15 - this._var7.Height) / 2f);
                this._var8.Location = new PointF(this._var7.Width + (2f * this._var9), 0f);
                this._var8.Width = (this.Width - this._var7.Width) - (2f * this._var9);
                this._var8.Height = var15;
            }
            else if (this._var4 == ContentAlignment.MiddleCenter)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) / 2f, (var15 - this._var7.Height) / 2f);
                this._var8.Location = new PointF(0f, (this._var7.Y + this._var7.Height) + this._var9);
                this._var8.Width = this.Width;
                this._var8.Height = ((var15 - this._var7.Height) / 2f) - this._var9;
            }
            else if (this._var4 == ContentAlignment.MiddleRight)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) - this._var9, (var15 - this._var7.Height) / 2f);
                this._var8.Location = new PointF(0f, 0f);
                this._var8.Width = (this.Width - this._var7.Width) - (2f * this._var9);
                this._var8.Height = var15;
            }
            else if (this._var4 == ContentAlignment.BottomLeft)
            {
                this._var7.Location = new PointF(this._var9, (var15 - this._var7.Height) - this._var9);
                this._var8.Location = new PointF(this._var7.Width + (2f * this._var9), 0f);
                this._var8.Width = (this.Width - this._var7.Width) - (2f * this._var9);
                this._var8.Height = var15 - this._var9;
            }
            else if (this._var4 == ContentAlignment.BottomCenter)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) / 2f, (var15 - this._var7.Height) - this._var9);
                this._var8.Location = new PointF(0f, 0f);
                this._var8.Width = this.Width;
                this._var8.Height = (var15 - this._var7.Height) - (2f * this._var9);
            }
            else if (this._var4 == ContentAlignment.BottomRight)
            {
                this._var7.Location = new PointF((this.Width - this._var7.Width) - this._var9, (var15 - this._var7.Height) - this._var9);
                this._var8.Location = new PointF(0f, 0f);
                this._var8.Width = (this.Width - this._var7.Width) - (2f * this._var9);
                this._var8.Height = var15 - this._var9;
            }
        }

        private void var14()
        {
            this._var5.FormatFlags = 0;
            if (this._var4 == ContentAlignment.TopLeft)
            {
                this._var5.LineAlignment = StringAlignment.Near;
                this._var5.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.TopCenter)
            {
                this._var5.LineAlignment = StringAlignment.Near;
                this._var5.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.TopRight)
            {
                this._var5.LineAlignment = StringAlignment.Near;
                this._var5.Alignment = StringAlignment.Far;
            }
            else if (this._var4 == ContentAlignment.MiddleLeft)
            {
                this._var5.LineAlignment = StringAlignment.Center;
                this._var5.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.MiddleCenter)
            {
                this._var5.LineAlignment = StringAlignment.Near;
                this._var5.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.MiddleRight)
            {
                this._var5.LineAlignment = StringAlignment.Center;
                this._var5.Alignment = StringAlignment.Far;
            }
            else if (this._var4 == ContentAlignment.BottomLeft)
            {
                this._var5.LineAlignment = StringAlignment.Far;
                this._var5.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.BottomCenter)
            {
                this._var5.LineAlignment = StringAlignment.Far;
                this._var5.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.BottomRight)
            {
                this._var5.LineAlignment = StringAlignment.Far;
                this._var5.Alignment = StringAlignment.Far;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public RectangleF mtd95
        {
            get
            {
                return this._var7;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RectangleF mtd96
        {
            get
            {
                return this._var8;
            }
        }

        [Description("The backcolor used to display text and graphics in the control"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public Color BackColor
        {
            get
            {
                return this._var1;
            }
            set
            {
                if (this._var1 != value)
                {
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd59, this._var1, value);
                    this._var1 = value;
                    base.mtd93(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override RectangleF Bounds
        {
            get
            {
                return base.Bounds;
            }
            set
            {
                this.Location = value.Location;
                this.Size = value.Size;
            }
        }

        [mtd85(mtd88.mtd86), Category("Format"), Description("Indicates alignment of the Checkbox and Text within the control drawing area")]
        public ContentAlignment CheckAlignment
        {
            get
            {
                return this._var4;
            }
            set
            {
                if (this._var4 != value)
                {
                    mtd69.mtd70(mtd26.mtd53, mtd56.mtd62, this._var4, value);
                    this._var4 = value;
                    this.var14();
                    this.var13(this.Height);
                    base.mtd93(this);
                }
            }
        }

        [DefaultValue(false), mtd85(mtd88.mtd86), Category("Data"), Description("Indicates whether the check box is in the checked state")]
        public bool Checked
        {
            get
            {
                if (this._var10 == null)
                {
                    return this._var6;
                }
                return ((this._var10.Value is bool) && ((bool) this._var10.Value));
            }
            set
            {
                if (this._var10 != null)
                {
                    this._var10.Value = value;
                }
                else
                {
                    this._var6 = value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public McField Field
        {
            set
            {
                this._var10 = value;
            }
        }

        [Description("The forecolor used to display text and graphics in the control"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public Color ForeColor
        {
            get
            {
                return this._var2;
            }
            set
            {
                if (this._var2 != value)
                {
                    mtd69.mtd70(mtd26.ForeColor, mtd56.mtd59, this._var2, value);
                    this._var2 = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Indicates Height of Control"), Category("Layout"), mtd85(mtd88.mtd86), TypeConverter(typeof(UISizeConverter))]
        public override float Height
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
                    base._mtd92 = true;
                    this.var13(this.Height);
                    base.mtd93(this);
                    base.OnReSize();
                }
            }
        }

        [mtd85(mtd88.mtd86), DefaultValue(false), Category("Format Text"), Description("Indicates whether or not the text is rendered right to left.")]
        public bool RightToLeft
        {
            get
            {
                return this._var11;
            }
            set
            {
                if (this._var11 != value)
                {
                    mtd69.mtd70(mtd26.RightToLeft, mtd56.mtd58, this._var11, value);
                    this._var11 = value;
                    base.mtd93(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override SizeF Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (base._mtd32 != value)
                {
                    mtd69.mtd70(mtd26.mtd32, mtd56.mtd57, base._mtd32, value);
                    base._mtd32 = value;
                    base._mtd92 = true;
                    this.var13(this.Height);
                    base.mtd93(this);
                    base.OnReSize();
                }
            }
        }

        [Description("Text value to be rendered in the control"), mtd85(mtd88.mtd86), Category("Data")]
        public string Text
        {
            get
            {
                return this._var0;
            }
            set
            {
                if (this._var0 != value)
                {
                    mtd69.mtd70(mtd26.mtd51, mtd56.mtd62, this._var0, value);
                    this._var0 = value;
                    base.mtd93(this);
                }
            }
        }

        [Category("Format"), Description("Font used to display the text in the control."), mtd85(mtd88.mtd86)]
        public Font TextFont
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (this._var3 != value)
                {
                    mtd69.mtd70(mtd26.mtd34, mtd56.mtd58, this._var3, value);
                    this._var3 = value;
                    base.mtd93(this);
                }
            }
        }

        [Category("Layout"), mtd85(mtd88.mtd86), Description("Indicates width of control"), TypeConverter(typeof(UISizeConverter))]
        public override float Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                if (this._mtd32.Width != value)
                {
                    mtd69.mtd70(mtd26.mtd30, mtd56.mtd57, this._mtd32.Width, value);
                    this._mtd32.Width = value;
                    base._mtd92 = true;
                    this.var13(this.Height);
                    base.mtd93(this);
                    base.OnReSize();
                }
            }
        }
    }
}

