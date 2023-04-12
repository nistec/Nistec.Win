namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McPicture : McReportControl
    {
        private Color _var0;
        private System.Drawing.Image _var1;
        private Nistec.Printing.View.PictureAlignment _var2;
        private Nistec.Printing.View.SizeMode _var3;
        private McField _var4;

        public McPicture()
        {
            this.var5();
        }

        public McPicture(string name)
        {
            base._mtd91 = name;
            this.var5();
        }

        //mtd22
        public void PaintCtl (Graphics var6, RectangleF var7, ref System.Drawing.Image var8, Nistec.Printing.View.PictureAlignment var9, Nistec.Printing.View.SizeMode var10, Color var11)
        {
            using (SolidBrush brush = new SolidBrush(var11))
            {
                var6.FillRectangle(brush, var7);
            }
            if (var8 != null)
            {
                this.var12(var6, var7, ref var8, var9, var10);
            }
            this.Border.Render(var6, var7);
        }

        //mtd98
        public override void DrawCtl (object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            this.PaintCtl(graphics, this.Bounds, ref this._var1, this.PictureAlignment, this.SizeMode, this.BackColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._var1 != null))
            {
                this._var1.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this.BackColor != Color.Transparent);
        }

        public bool ShouldSerializeImage()
        {
            return (this.Image != null);
        }

        public bool ShouldSerializePictureAlignment()
        {
            return (this.PictureAlignment != Nistec.Printing.View.PictureAlignment.TopLeft);
        }

        public bool ShouldSerializeSizeMode()
        {
            return (this.SizeMode != Nistec.Printing.View.SizeMode.Clip);
        }

        private void var12(Graphics var6, RectangleF var7, ref System.Drawing.Image var13, Nistec.Printing.View.PictureAlignment var9, Nistec.Printing.View.SizeMode var10)
        {
            if (var10 == Nistec.Printing.View.SizeMode.Clip)
            {
                PointF tf = this.var14(var6.PageUnit, var13, var7, var9);
                Region clip = var6.Clip;
                var6.Clip = new Region(var7);
                var6.DrawImage(var13, tf.X, tf.Y);
                var6.Clip = clip;
            }
            else if (var10 == Nistec.Printing.View.SizeMode.Stretch)
            {
                var6.DrawImage(var13, var7);
            }
            else if (var10 == Nistec.Printing.View.SizeMode.Zoom)
            {
                RectangleF rect = this.var15(var13, var7);
                rect.X += var7.X;
                rect.Y += var7.Y;
                var6.DrawImage(var13, rect);
            }
        }

        private PointF var14(GraphicsUnit var16, System.Drawing.Image var8, RectangleF var7, Nistec.Printing.View.PictureAlignment var9)
        {
            float num = ((float) var8.Width) / var8.HorizontalResolution;
            float num2 = ((float) var8.Height) / var8.VerticalResolution;
            PointF tf = new PointF(var7.X, var7.Y);
            if (var16 == GraphicsUnit.Display)
            {
                num *= ReportUtil.Dpi;
                num2 *= ReportUtil.Dpi;
            }
            if (var9 != Nistec.Printing.View.PictureAlignment.TopLeft)
            {
                if (var9 == Nistec.Printing.View.PictureAlignment.TopRight)
                {
                    tf.X += var7.Width - num;
                    return tf;
                }
                if (var9 == Nistec.Printing.View.PictureAlignment.Center)
                {
                    tf.X += (var7.Width - num) / 2f;
                    tf.Y += (var7.Height - num2) / 2f;
                    return tf;
                }
                if (var9 == Nistec.Printing.View.PictureAlignment.BottomLeft)
                {
                    tf.Y += var7.Height - num2;
                    return tf;
                }
                if (var9 == Nistec.Printing.View.PictureAlignment.BottomRight)
                {
                    tf.X += var7.Width - num;
                    tf.Y += var7.Height - num2;
                }
            }
            return tf;
        }

        private RectangleF var15(System.Drawing.Image var8, RectangleF var7)
        {
            RectangleF ef = new RectangleF();
            ef.Height = var7.Height;
            ef.Width = (var7.Height * var8.Width) / ((float) var8.Height);
            if (ef.Width > var7.Width)
            {
                ef.Width = var7.Width;
                ef.Height = (var7.Width * var8.Height) / ((float) var8.Width);
                ef.Y = (var7.Height - ef.Height) / 2f;
                ef.X = 0f;
                return ef;
            }
            ef.X = (var7.Width - ef.Width) / 2f;
            ef.Y = 0f;
            return ef;
        }

        private void var5()
        {
            base._mtd66 = ControlType.Picture;
            this._var0 = Color.Transparent;
        }

        [Description("The backcolor used to display text and graphics in the control"), mtd85(mtd88.mtd86), Category("Appearance")]
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
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd9, this._var0, value);
                    this._var0 = value;
                    base.mtd93(this);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public McField Field
        {
            set
            {
                this._var4 = value;
            }
        }

        [mtd85(mtd88.mtd86), TypeConverter(typeof(mtd77)), Description("Represents the image to be printed with the Picture"), Category("Image")]
        public System.Drawing.Image Image
        {
            get
            {
                if (this._var4 == null)
                {
                    return this._var1;
                }
                if (this._var4.Value is System.Drawing.Image)
                {
                    return (System.Drawing.Image) this._var4.Value;
                }
                if (this._var4.Value is byte[])
                {
                    return mtd10.mtd21((byte[]) this._var4.Value);
                }
                return null;
            }
            set
            {
                if (this._var4 != null)
                {
                    this._var4.Value = value;
                }
                else
                {
                    this._var1 = value;
                }
            }
        }

        [mtd85(mtd88.mtd86), Category("Image"), Description("Indicates the position of the image within th control area")]
        public Nistec.Printing.View.PictureAlignment PictureAlignment
        {
            get
            {
                return this._var2;
            }
            set
            {
                if ((this._var2 != value) && (this._var2 != value))
                {
                    mtd69.mtd70(mtd26.mtd48, mtd56.mtd9, this._var2, value);
                    this._var2 = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Determines how the image is sized to fit the Picture control"), mtd85(mtd88.mtd86), Category("Image")]
        public Nistec.Printing.View.SizeMode SizeMode
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (this._var3 != value)
                {
                    mtd69.mtd70(mtd26.mtd49, mtd56.mtd9, this._var3, value);
                    this._var3 = value;
                    base.IsDirty = true;
                }
            }
        }
    }
}

