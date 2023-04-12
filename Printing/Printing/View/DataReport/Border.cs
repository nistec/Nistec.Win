namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Border
    {
        private Color _var0 = Color.Black;
        private BorderLineStyle _var1 = BorderLineStyle.None;
        private Color _var2 = Color.Black;
        private BorderLineStyle _var3 = BorderLineStyle.None;
        private Color _var4 = Color.Black;
        private BorderLineStyle _var5 = BorderLineStyle.None;
        private Color _var6 = Color.Black;
        private BorderLineStyle _var7 = BorderLineStyle.None;

        public void Render(Graphics g, RectangleF border)
        {
            PointF tf = new PointF(border.X, border.Y);
            PointF tf2 = new PointF(border.X, border.Y + border.Height);
            PointF tf3 = new PointF(border.X + border.Width, border.Y + border.Height);
            PointF tf4 = new PointF(border.X + border.Width, border.Y);
            if (this._var5 != BorderLineStyle.None)
            {
                this.var8(g, this._var5, this._var4, tf, tf2, 1);
            }
            if (this._var3 != BorderLineStyle.None)
            {
                this.var8(g, this._var3, this._var2, tf2, tf3, 2);
            }
            if (this._var7 != BorderLineStyle.None)
            {
                this.var8(g, this._var7, this._var6, tf4, tf3, 3);
            }
            if (this._var1 != BorderLineStyle.None)
            {
                this.var8(g, this._var1, this._var0, tf, tf4, 4);
            }
        }

        public bool ShouldSeializeBorderRightStyle()
        {
            return (this.BorderRightStyle != BorderLineStyle.None);
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

        public bool ShouldSerializeBorderTopColor()
        {
            return (this.BorderTopColor != Color.Black);
        }

        public bool ShouldSerializeBorderTopStyle()
        {
            return (this.BorderTopStyle != BorderLineStyle.None);
        }

        private void var15(Graphics var9, Pen var17, PointF var12, PointF var13, int var14)
        {
            float num = 0f;
            if (var9.PageUnit == GraphicsUnit.Display)
            {
                float num2 = ((float) var9.Transform.Elements.GetValue(0)) * var17.Width;
                if (num2 > 1.5f)
                {
                    num = var17.Width / 2f;
                }
                else if (num2 < 0.55f)
                {
                    var17.DashStyle = DashStyle.Solid;
                }
            }
            else if (var9.PageUnit == GraphicsUnit.Inch)
            {
                num = var17.Width / 2f;
            }
            if ((var14 == 1) || (var14 == 3))
            {
                var12 = new PointF(var12.X, var12.Y - num);
                var13 = new PointF(var13.X, var13.Y + num);
            }
            else
            {
                var12 = new PointF(var12.X - num, var12.Y);
                var13 = new PointF(var13.X + num, var13.Y);
            }
            var9.DrawLine(var17, var12, var13);
        }

        private PointF[] var16(PointF var12, PointF var13, int var14, float var18)
        {
            if (var14 == 1)
            {
                return new PointF[] { new PointF(var12.X - var18, var12.Y - var18), new PointF(var13.X - var18, var13.Y + var18) };
            }
            if (var14 == 2)
            {
                return new PointF[] { new PointF(var12.X - var18, var12.Y + var18), new PointF(var13.X + var18, var13.Y + var18) };
            }
            if (var14 == 3)
            {
                return new PointF[] { new PointF(var12.X + var18, var12.Y - var18), new PointF(var13.X + var18, var13.Y + var18) };
            }
            if (var14 == 4)
            {
                return new PointF[] { new PointF(var12.X - var18, var12.Y - var18), new PointF(var13.X + var18, var13.Y - var18) };
            }
            return null;
        }

        private void var8(Graphics var9, BorderLineStyle var10, Color var11, PointF var12, PointF var13, int var14)
        {
            float width = 1f;
            if (var10 == BorderLineStyle.Solid)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                }
                using (Pen pen = new Pen(var11, width))
                {
                    pen.DashStyle = DashStyle.Solid;
                    this.var15(var9, pen, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.Dash)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                }
                using (Pen pen2 = new Pen(var11, width))
                {
                    pen2.DashStyle = DashStyle.Dash;
                    this.var15(var9, pen2, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.DashDot)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                }
                using (Pen pen3 = new Pen(var11, width))
                {
                    pen3.DashStyle = DashStyle.DashDot;
                    this.var15(var9, pen3, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.DashDotDot)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                }
                using (Pen pen4 = new Pen(var11, width))
                {
                    pen4.DashStyle = DashStyle.DashDotDot;
                    this.var15(var9, pen4, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.Dot)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                }
                using (Pen pen5 = new Pen(var11, width))
                {
                    pen5.DashStyle = DashStyle.Dot;
                    this.var15(var9, pen5, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.Double)
            {
                float num2;
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 1f / var9.DpiX;
                    num2 = 0.02f;
                }
                else
                {
                    num2 = 2f * width;
                }
                using (Pen pen6 = new Pen(var11, width))
                {
                    pen6.DashStyle = DashStyle.Solid;
                    PointF[] tfArray = this.var16(var12, var13, var14, num2);
                    this.var15(var9, pen6, var12, var13, var14);
                    this.var15(var9, pen6, tfArray[0], tfArray[1], var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ThickSolid)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen7 = new Pen(var11, 2f * width))
                {
                    pen7.DashStyle = DashStyle.Solid;
                    this.var15(var9, pen7, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ThickDash)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen8 = new Pen(var11, 2f * width))
                {
                    pen8.DashStyle = DashStyle.Dash;
                    this.var15(var9, pen8, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ThickDashDot)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen9 = new Pen(var11, 2f * width))
                {
                    pen9.DashStyle = DashStyle.DashDot;
                    this.var15(var9, pen9, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ThickDashDotDot)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen10 = new Pen(var11, 2f * width))
                {
                    pen10.DashStyle = DashStyle.Dot;
                    this.var15(var9, pen10, var12, var13, var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ThickDouble)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen11 = new Pen(var11, 2f * width))
                {
                    pen11.DashStyle = DashStyle.Solid;
                    this.var15(var9, pen11, var12, var13, var14);
                    PointF[] tfArray2 = this.var16(var12, var13, var14, 4f * width);
                    this.var15(var9, pen11, var12, var13, var14);
                    this.var15(var9, pen11, tfArray2[0], tfArray2[1], var14);
                    return;
                }
            }
            if (var10 == BorderLineStyle.ExtraThickSolid)
            {
                if (var9.PageUnit == GraphicsUnit.Inch)
                {
                    width = 0.01f;
                }
                using (Pen pen12 = new Pen(var11, 3f * width))
                {
                    pen12.DashStyle = DashStyle.Solid;
                    this.var15(var9, pen12, var12, var13, var14);
                }
            }
        }

        public Color BorderBottomColor
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

        public BorderLineStyle BorderBottomStyle
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

        public Color BorderLeftColor
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

        public BorderLineStyle BorderLeftStyle
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }

        public Color BorderRightColor
        {
            get
            {
                return this._var6;
            }
            set
            {
                this._var6 = value;
            }
        }

        public BorderLineStyle BorderRightStyle
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

        public Color BorderTopColor
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

        public BorderLineStyle BorderTopStyle
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
    }
}

