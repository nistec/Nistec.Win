namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    internal class A1 : A91
    {
        internal RectangleF _A116;
        internal RGBColor _A117;

        internal A1(Document b0, RectangleF b1, RGBColor b2)
        {
            base._A92 = b0;
            this._A116 = b1;
            this._A117 = b2;
        }

        internal override void A54(ref A55 b3)
        {
            base._A92.A93.A94(b3.A2, 0);
            b3.A59(string.Format("{0} 0 obj", this.A95));
            b3.A59("<<");
            b3.A59("/Type /Annot ");
        }
    }
}

