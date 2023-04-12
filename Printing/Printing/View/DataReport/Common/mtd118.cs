namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    public class PropDoc//mtd118
    {
        private McLocation _Location;//_var0;
        private mtd120 _var1;
        private object _var10;
        private Color _var11;
        private LineStyle _LineStyle;//_var12;
        private float _var13;
        private Image _Image;//_var14;
        private mtd127 _var15;
        private string _var16;
        private mtd121 _var2;
        private McLineStyle _McLineStyle;//_var3;
        private LinePosition _LinePosition;//_var4
        private mtd124 _var5;
        private mtd125 _var6;
        private mtd126 _var7;
        private Color _var8;
        private Color _var9;

        internal bool mtd140(mtd141 var17, int var18)
        {
            if (((this.Count != -1) && (var18 > -1)) && (var18 < this.Count))
            {
                var17.mtd142(this._var15.mtd143(var18));
                return true;
            }
            return false;
        }

        internal void mtd142(mtd136 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = var19.mtd145;
            this._var2 = var19.mtd146;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = null;
            this._var8 = var19.mtd147.ForeColor;
            this._var9 = var19.mtd147.BackColor;
            this._var10 = null;
            this._var16 = this._var2.mtd51;
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = null;
        }

        internal void mtd142(mtd148 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = var19.mtd145;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = null;
            this._var8 = var19.mtd147.ForeColor;
            this._var9 = var19.mtd147.BackColor;
            this._var10 = var19.mtd137;
            if ((this._var10 != null) && (this._var10 != DBNull.Value))
            {
                this._var16 = this._var10.ToString();
            }
            else
            {
                this._var16 = string.Empty;
            }
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = null;
        }

        internal void mtd142(mtd149 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = null;
            this._McLineStyle = var19.mtd150;
            this._LinePosition = var19.mtd151;
            this._var2 = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = null;
            this._var8 = Color.Transparent;
            this._var9 = Color.Transparent;
            this._var10 = null;
            this._var16 = null;
            this._var11 = this._McLineStyle._Color;
            this._LineStyle = this._McLineStyle._LineStyle;
            this._var13 = this._McLineStyle._LineWeight;
            this._Image = null;
        }

        internal void mtd142(mtd152 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = null;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = var19.mtd153;
            this._var6 = null;
            this._var15 = null;
            this._var8 = Color.Transparent;
            this._var9 = this._var5.BackColor;
            this._var10 = null;
            this._var16 = null;
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = var19.mtd154;
        }

        internal void mtd142(mtd155 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = null;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = null;
            this._var8 = var19.mtd147.ForeColor;
            this._var9 = var19.mtd147.BackColor;
            this._var10 = null;
            this._var16 = null;
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = var20(ref var19);
        }

        internal void mtd142(mtd156 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = null;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = var19.mtd157;
            this._var15 = null;
            this._var8 = Color.Transparent;
            this._var9 = this._var6.BackColor;
            this._var10 = null;
            this._var16 = null;
            this._var11 = this._var6.BorderColor;
            this._LineStyle = this._var6.mtd42;
            this._var13 = this._var6.mtd43;
            this._Image = null;
        }

        internal void mtd142(mtd158 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = var19.mtd145;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = null;
            this._var8 = var19.mtd147.ForeColor;
            this._var9 = var19.mtd147.BackColor;
            this._var10 = var19.mtd137;
            this._var16 = mtd159.mtd160(this._var10, this._var1.OutputFormat);
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = null;
        }

        internal void mtd142(mtd161 var19)
        {
            this._var7 = var19;
            this._Location = var19._Location;
            this._var1 = null;
            this._var2 = null;
            this._McLineStyle = null;
            this._LinePosition = null;
            this._var5 = null;
            this._var6 = null;
            this._var15 = var19.mtd162;
            this._var8 = Color.Transparent;
            this._var9 = Color.Transparent;
            this._var10 = null;
            this._var16 = null;
            this._var11 = Color.Transparent;
            this._LineStyle = LineStyle.Solid;
            this._var13 = 0f;
            this._Image = null;
        }

        private static Image var20(ref mtd155 var21)
        {
            float width = var21.Width * ReportUtil.Dpi;
            float height = var21.Height * ReportUtil.Dpi;
            Image image = new Bitmap((int) Math.Ceiling((double) width), (int) Math.Ceiling((double) height));
            mtd155.mtd22(Graphics.FromImage(image), var21, new RectangleF(0f, 0f, width, height));
            return image;
        }

        internal float Left//mtd128
        {
            get
            {
                return this._Location.Left;
            }
        }

        internal float Top//mtd129
        {
            get
            {
                return this._Location.Top;
            }
        }

        public Font Font//mtd132
        {
            get
            {
                if (this._var1 != null)
                {
                    return this._var1._Font;
                }
                return null;
            }
        }

        internal RectangleF mtd133
        {
            get
            {
                if (this._var2 != null)
                {
                    return this._var2.mtd133;
                }
                return RectangleF.Empty;
            }
        }

        internal RectangleF mtd134
        {
            get
            {
                if (this._var2 != null)
                {
                    return this._var2.mtd134;
                }
                return RectangleF.Empty;
            }
        }

        public bool IsChecked //mtd135
        {
            get
            {
                return ((this._var7.ControlType == ControlType.CheckBox) && ((mtd136)this._var7).mtd135);
            }
        }

        internal object mtd137
        {
            get
            {
                return this._var10;
            }
        }

        public ControlType ControlType//mtd138
        {
            get
            {
                return this._var7.ControlType;
            }
        }

        public int Count //mtd139
        {
            get
            {
                if ((this.ControlType == ControlType.SubReport) && (this._var15 != null))
                {
                    return this._var15.Count;
                }
                return -1;
            }
        }

        internal float Width//mtd30
        {
            get
            {
                return this._Location.Width;
            }
        }

        internal float Height//mtd31
        {
            get
            {
                return this._Location.Height;
            }
        }

        public ContentAlignment ContentAlignment//mtd35
        {
            get
            {
                if (this._var1 != null)
                {
                    return this._var1._ContentAlignment;
                }
                return ContentAlignment.TopLeft;
            }
        }

        internal bool LineLimit
        {
            get
            {
                return ((this._var1 != null) && this._var1.LineLimit);
            }
        }

        internal string OutputFormat
        {
            get
            {
                if (this._var1 != null)
                {
                    return this._var1.OutputFormat;
                }
                return null;
            }
        }

        internal bool RightToLeft
        {
            get
            {
                return ((this._var1 != null) && this._var1.RightToLeft);
            }
        }

        internal Color BackColor// mtd39
        {
            get
            {
                return this._var9;
            }
        }

        internal Color ForeColor //mtd40
        {
            get
            {
                return this._var8;
            }
        }

        internal Color BorderColor //mtd41
        {
            get
            {
                return this._var11;
            }
        }

        public LineStyle LineStyle//mtd42
        {
            get
            {
                return this._LineStyle;
            }
        }

        internal float mtd43
        {
            get
            {
                return this._var13;
            }
        }

        internal float LinePositionX1//mtd44
        {
            get
            {
                if (this._LinePosition != null)
                {
                    return this._LinePosition.X1;
                }
                return 0f;
            }
        }

        internal float LinePositionY1//mtd45
        {
            get
            {
                if (this._LinePosition != null)
                {
                    return this._LinePosition.Y1;
                }
                return 0f;
            }
        }

        internal float LinePositionX2//mtd46
        {
            get
            {
                if (this._LinePosition != null)
                {
                    return this._LinePosition.X2;
                }
                return 0f;
            }
        }

        internal float LinePositionY2//mtd47
        {
            get
            {
                if (this._LinePosition != null)
                {
                    return this._LinePosition.Y2;
                }
                return 0f;
            }
        }

        public PictureAlignment PictureAlignment//mtd48
        {
            get
            {
                if (this._var5 != null)
                {
                    return this._var5.mtd48;
                }
                return PictureAlignment.TopLeft;
            }
        }

        public SizeMode SizeMode//mtd49
        {
            get
            {
                if (this._var5 != null)
                {
                    return this._var5.mtd49;
                }
                return SizeMode.Clip;
            }
        }

        public ShapeStyle ShapeStyle//mtd50
        {
            get
            {
                if (this._var6 != null)
                {
                    return this._var6.mtd50;
                }
                return ShapeStyle.Rectangle;
            }
        }

        public string Text//mtd51
        {
            get
            {
                return this._var16;
            }
        }

        public bool Visible//mtd86
        {
            get
            {
                return this._var7.mtd130;
            }
        }

        public Image Image//mtd9
        {
            get
            {
                return this._Image;
            }
        }

        public Border Border//mtd99
        {
            get
            {
                return this._var7._Border;
            }
        }
    }
}

