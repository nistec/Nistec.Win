namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    internal class A124 : A1
    {
        private A90 _b0;
        private HighlightMode _b1;
        private int _b2;

        internal A124(Document b3, RectangleF b4, RGBColor b5, int b2, A90 b0, HighlightMode b6) : base(b3, b4, b5)
        {
            this._b2 = b2;
            this._b0 = b0;
            this._b1 = b6;
        }

        internal override void A110()
        {
            base.A110();
            if (this._b0 != null)
            {
                this._b0.A110();
            }
        }

        internal override void A54(ref A55 b7)
        {
            base.A54(ref b7);
            b7.A59("/Subtype /Link");
            b7.A59(string.Format("/Rect {0}", A15.A21(this._A116.X, this._A116.Y - this._A116.Height, this._A116.Right, this._A116.Top)));
            b7.A59(string.Format("/C [{0}]", RGBColor.A122(base._A117)));
            b7.A59(string.Format("/Border [0 0 {0}]", this._b2));
            if (this._b1 == HighlightMode.None)
            {
                b7.A59("/H /N");
            }
            else if (this._b1 == HighlightMode.Invert)
            {
                b7.A59("/H /I");
            }
            else if (this._b1 == HighlightMode.Outline)
            {
                b7.A59("/H /O");
            }
            else if (this._b1 == HighlightMode.Push)
            {
                b7.A59("/H /P");
            }
            if (this._b0 != null)
            {
                b7.A59(string.Format("/A {0} 0 R ", this._b0.A95));
            }
            b7.A59(">>");
            b7.A59("endobj");
            if (this._b0 != null)
            {
                this._b0.A54(ref b7);
            }
        }
    }
}

