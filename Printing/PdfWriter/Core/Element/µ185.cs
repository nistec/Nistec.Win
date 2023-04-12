namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A185 : A91
    {
        internal PageMode A186;
        internal bool A187;
        internal bool A188;
        internal bool A189;
        internal bool A190;
        internal bool A191;
        internal bool A192;
        internal PageMode A193;
        internal string A194;

        internal A185(Document b0)
        {
            base._A92 = b0;
            this.A186 = PageMode.UseNone;
            this.A187 = false;
            this.A188 = false;
            this.A189 = false;
            this.A190 = false;
            this.A191 = false;
            this.A192 = false;
            this.A193 = PageMode.UseNone;
            this.A194 = null;
        }

        internal override void A54(ref A55 b1)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base._A92.A93.A94(b1.A2, 0);
            b1.A59(string.Format("{0} 0 obj", this.A95));
            b1.A59("<<");
            b1.A59("/Type /Catalog ");
            b1.A59(string.Format("/Pages {0} 0 R ", base._A92.Pages.A95));
            b1.A59(string.Format("/Outlines {0} 0 R ", base._A92.A195.A95));
            if (base._A92.Bookmarks.Count > 0)
            {
                b1.A59(string.Format("/PageMode /{0} ", PageMode.UseOutlines));
            }
            else
            {
                b1.A59(string.Format("/PageMode /{0} ", this.A186));
            }
            b1.A59("/ViewerPreferences <<");
            b1.A59(string.Format("/HideToolbar {0} ", A15.A24(this.A187)));
            b1.A59(string.Format("/HideMenubar {0} ", A15.A24(this.A188)));
            b1.A59(string.Format("/HideWindowUI {0} ", A15.A24(this.A189)));
            b1.A59(string.Format("/FitWindow {0} ", A15.A24(this.A190)));
            b1.A59(string.Format("/CenterWindow {0} ", A15.A24(this.A191)));
            b1.A59(string.Format("/DisplayDocTitle {0} ", A15.A24(this.A192)));
            b1.A59(string.Format("/NonFullScreenPageMode /{0} ", this.A193));
            b1.A59(">>");
            if ((this.A194 != null) && (this.A194.Length > 0))
            {
                b1.A59("/OpenAction <<");
                b1.A59("/S /JavaScript");
                b1.A59("/JS ");
                if (A != null)
                {
                    A26.A54(ref b1, this.A194, A);
                }
                else
                {
                    A26.A54(ref b1, A15.A26(this.A194), A);
                }
                b1.A59(">>");
            }
            if (base._A92.Form.Fields.Size > 0)
            {
                b1.A59(string.Format("/AcroForm {0} 0 R", base._A92.Form.A95));
            }
            b1.A59(">>");
            b1.A59("endobj");
        }
    }
}

