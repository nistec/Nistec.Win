namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.IO;

    internal class A101 : A90
    {
        private string _b0;

        internal A101(Document b1, string b0) : base(b1)
        {
            base._A92 = b1;
            this._b0 = b0;
        }

        internal static void A102(ref A55 b2, Document b1, int b3, string b0)
        {
            if (File.Exists(b0))
            {
                b0 = Path.GetFullPath(b0);
            }
            A56 A = b1.A56;
            if (A != null)
            {
                A.A100(b3, 0);
            }
            b2.A59("/S /Launch");
            b2.A59("/Type /Filespec");
            b2.A54("/F ");
            if (A != null)
            {
                A26.A54(ref b2, A15.A31(b0), A);
            }
            else
            {
                A26.A54(ref b2, A15.A26(A15.A31(b0)), A);
            }
        }

        internal override void A54(ref A55 b2)
        {
            base.A54(ref b2);
            A102(ref b2, base._A92, this.A95, this._b0);
            b2.A59(">>");
            b2.A59("endobj");
        }
    }
}

