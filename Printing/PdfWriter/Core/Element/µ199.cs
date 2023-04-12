namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A199 : A91
    {
        internal string A200;
        internal string A201;
        internal string A202;
        internal string A203;
        internal string A204;
        internal string A205;
        internal DateTime A206;
        internal DateTime A207;

        internal A199(Document b0)
        {
            base._A92 = b0;
            this.A200 = null;
            this.A202 = null;
            this.A203 = null;
            this.A204 = "MControl PdfWriter.";
            this.A205 = null;
            this.A201 = null;
            this.A205 = null;
            this.A206 = DateTime.Now;
            this.A207 = this.A206;
        }

        internal override void A54(ref A55 b1)
        {
            A93 A = base._A92.A93;
            A56 A2 = base._A92.A56;
            A.A94(b1.A2, 0);
            b1.A59(string.Format("{0} 0 obj", this.A95));
            if (A2 != null)
            {
                A2.A100(this.A95, 0);
            }
            b1.A59("<<");
            if ((this.A200 != null) && (this.A200.Length > 0))
            {
                b1.A54("/Title ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A200, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A200), A2);
                }
            }
            if ((this.A201 != null) && (this.A201.Length > 0))
            {
                b1.A54("/Author ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A201, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A201), A2);
                }
            }
            if ((this.A202 != null) && (this.A202.Length > 0))
            {
                b1.A54("/Subject ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A202, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A202), A2);
                }
            }
            if ((this.A203 != null) && (this.A203.Length > 0))
            {
                b1.A54("/Keywords ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A203, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A203), A2);
                }
            }
            if ((this.A204 != null) && (this.A204.Length > 0))
            {
                b1.A54("/Creator ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A204, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A204), A2);
                }
            }
            if ((this.A205 != null) && (this.A205.Length > 0))
            {
                b1.A54("/Producer ");
                if (A2 != null)
                {
                    A26.A54(ref b1, this.A205, A2);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A205), A2);
                }
            }
            b1.A54("/CreationDate ");
            A26.A54(ref b1, A15.A20(this.A206), A2);
            b1.A54("/ModDate ");
            A26.A54(ref b1, A15.A20(this.A207), A2);
            b1.A59(">>");
            b1.A59("endobj");
        }
    }
}

