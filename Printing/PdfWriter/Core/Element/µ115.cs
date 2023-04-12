namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A115 : A90
    {
        private string _b0;

        internal A115(Document b1, string b0) : base(b1)
        {
            base._A92 = b1;
            this._b0 = b0;
        }

        internal override void A54(ref A55 b2)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base.A54(ref b2);
            b2.A59("/S /URI");
            b2.A54("/URI ");
            if (A != null)
            {
                A26.A54(ref b2, this._b0, A);
            }
            else
            {
                A26.A54(ref b2, A15.A26(this._b0), A);
            }
            b2.A59(">>");
            b2.A59("endobj");
        }
    }
}

