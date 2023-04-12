namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A222 : A91
    {
        internal A222(Document b0)
        {
            base._A92 = b0;
        }

        internal override void A54(ref A55 b1)
        {
            base._A92.A93.A94(b1.A2, 0);
            b1.A59(string.Format("{0} 0 obj", this.A95));
            b1.A59("[/Pdf /Text /ImageC]");
            b1.A59("endobj");
        }
    }
}

