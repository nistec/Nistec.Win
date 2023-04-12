namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    internal class A125 : A1
    {
        private string _b0;
        private string _b1;
        private bool _b2;
        private AnnotIcon _b3;

        internal A125(Document b4, string b0, string b1, RectangleF b5, RGBColor b6, bool b2, AnnotIcon b3) : base(b4, b5, b6)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
        }

        internal override void A54(ref A55 b7)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base.A54(ref b7);
            b7.A59("/Subtype /Text");
            b7.A59(string.Format("/Rect {0}", A15.A21(this._A116.X, this._A116.Y - this._A116.Height, this._A116.Right, this._A116.Top)));
            b7.A59(string.Format("/C [{0}]", RGBColor.A122(base._A117)));
            if (this._b0.Length > 0)
            {
                b7.A59("/T ");
                if (A != null)
                {
                    A26.A54(ref b7, this._b0, A);
                }
                else
                {
                    A26.A54(ref b7, A15.A26(this._b0), A);
                }
            }
            if (this._b1.Length > 0)
            {
                b7.A59("/Contents ");
                if (A != null)
                {
                    A26.A54(ref b7, this._b1, A);
                }
                else
                {
                    A26.A54(ref b7, A15.A26(this._b1), A);
                }
            }
            b7.A59(string.Format("/Open {0}", A15.A24(this._b2)));
            if (this._b3 != AnnotIcon.Note)
            {
                b7.A59(string.Format("/Name ({0})", this._b3.ToString()));
            }
            b7.A59(">>");
            b7.A59("endobj");
        }
    }
}

