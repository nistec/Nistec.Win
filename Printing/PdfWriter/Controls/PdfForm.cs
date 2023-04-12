namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class PdfForm : A91
    {
        private PdfFields _b0;

        internal PdfForm(Document b1)
        {
            base._A92 = b1;
            this._b0 = new PdfFields();
        }

        internal override void A54(ref A55 b2)
        {
            base._A92.A93.A94(b2.A2, 0);
            b2.A59(string.Format("{0} 0 obj", this.A95));
            b2.A59("<<");
            if (this._b0.Size > 0)
            {
                b2.A54("/Fields [ ");
                for (int i = 0; i < this._b0.Size; i++)
                {
                    A91 A2 = this._b0[i];
                    b2.A54(string.Format("{0} 0 R ", A2.A95));
                }
                b2.A59("]");
            }
            b2.A59("/NeedAppearances true");
            b2.A59(">>");
            b2.A59("endobj");
        }

        public PdfFields Fields
        {
            get
            {
                return this._b0;
            }
        }
    }
}

