namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd120
    {
        private static StringFormat _var0 = ((StringFormat) StringFormat.GenericTypographic.Clone());
        internal Font _Font;//mtd132;
        internal ContentAlignment _ContentAlignment;//mtd35;
        internal bool LineLimit;//mtd36
        internal string OutputFormat;//mtd37;
        internal bool RightToLeft;//mtd38

        internal mtd120(Font var1, ContentAlignment var2, bool var3, bool var5)
        {
            this._Font = var1;
            this._ContentAlignment = var2;
            this.LineLimit = var3;
            this.RightToLeft = var5;
        }

        internal mtd120(Font var1, ContentAlignment var2, bool var3, string var4, bool var5)
        {
            this._Font = var1;
            this._ContentAlignment = var2;
            this.LineLimit = var3;
            this.OutputFormat = var4;
            this.RightToLeft = var5;
        }

        internal StringFormat mtd244
        {
            get
            {
                if (this._ContentAlignment == ContentAlignment.TopLeft)
                {
                    _var0.LineAlignment = StringAlignment.Near;
                    _var0.Alignment = StringAlignment.Near;
                }
                else if (this._ContentAlignment == ContentAlignment.TopCenter)
                {
                    _var0.LineAlignment = StringAlignment.Near;
                    _var0.Alignment = StringAlignment.Center;
                }
                else if (this._ContentAlignment == ContentAlignment.TopRight)
                {
                    _var0.LineAlignment = StringAlignment.Near;
                    //_var0.Alignment = StringAlignment.Far;
                    _var0.Alignment = this.RightToLeft ? StringAlignment.Near : StringAlignment.Far;
                }
                else if (this._ContentAlignment == ContentAlignment.MiddleLeft)
                {
                    _var0.LineAlignment = StringAlignment.Center;
                    _var0.Alignment = StringAlignment.Near;
                }
                else if (this._ContentAlignment == ContentAlignment.MiddleCenter)
                {
                    _var0.LineAlignment = StringAlignment.Center;
                    _var0.Alignment = StringAlignment.Center;
                }
                else if (this._ContentAlignment == ContentAlignment.MiddleRight)
                {
                    _var0.LineAlignment = StringAlignment.Center;
                   // _var0.Alignment = StringAlignment.Far;
                    _var0.Alignment = this.RightToLeft ? StringAlignment.Near : StringAlignment.Far;
                }
                else if (this._ContentAlignment == ContentAlignment.BottomLeft)
                {
                    _var0.LineAlignment = StringAlignment.Far;
                    _var0.Alignment = StringAlignment.Near;
                }
                else if (this._ContentAlignment == ContentAlignment.BottomCenter)
                {
                    _var0.LineAlignment = StringAlignment.Far;
                    _var0.Alignment = StringAlignment.Center;
                }
                else if (this._ContentAlignment == ContentAlignment.BottomRight)
                {
                    _var0.LineAlignment = StringAlignment.Far;
                    //_var0.Alignment = StringAlignment.Far;
                    _var0.Alignment = this.RightToLeft ? StringAlignment.Near : StringAlignment.Far;
                }
                if (this.LineLimit)
                {
                    _var0.FormatFlags = StringFormatFlags.LineLimit;
                }
                else
                {
                    _var0.FormatFlags = StringFormatFlags.NoWrap;
                }
                if (this.RightToLeft)
                {
                    _var0.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                return _var0;
            }
        }
    }
}

