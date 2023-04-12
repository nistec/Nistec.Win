namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;

    internal class A118 : A1
    {
        private string _b0;
        private byte[] _b1;
        private FileAttachmentIcon _b2;
        private int _b3;

        internal A118(Document b4, string b0, Stream b5, RectangleF b6, RGBColor b7, int b3, FileAttachmentIcon b2) : base(b4, b6, b7)
        {
            this._b0 = b0;
            this._b3 = b3;
            this._b2 = b2;
            this._b1 = A15.A32(b5, false);
        }

        internal override void A110()
        {
            int num;
            A93 A1 = base._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            base._A95 = num;
            base._A92.A93.A95 = base._A95 + 3;
        }

        internal override void A119(ref A120 b8, ref A112 b9)
        {
            base._A92 = b8.A97;
            this._A116.Y = b8.A98(this._A116.Y);
            b8.A121(this);
        }

        internal override void A54(ref A55 b10)
        {
            A112 A2;
            string path = this._b0;
            if (File.Exists(path))
            {
                path = Path.GetFullPath(path);
            }
            base.A54(ref b10);
            b10.A59("/Subtype /FileAttachment");
            b10.A59(string.Format("/Rect {0}", A15.A21(this._A116.X, this._A116.Y - this._A116.Height, this._A116.Right, this._A116.Top)));
            b10.A59(string.Format("/C [{0}]", RGBColor.A122(base._A117)));
            b10.A59(string.Format("/Border [0 0 {0}]", this._b3));
            b10.A59(string.Format("/Name /{0}", this._b2.ToString(CultureInfo.InvariantCulture.ToString())));
            int num = this.A95 + 1;
            b10.A59(string.Format("/FS {0} 0 R", num));
            b10.A59(">>");
            b10.A59("endobj");
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(num, 0);
            }
            base._A92.A93.A94(b10.A2, 0);
            b10.A59(string.Format("{0} 0 obj", num));
            b10.A59("<<");
            b10.A59("/Type /Filespec");
            b10.A54("/F ");
            if (A != null)
            {
                A26.A54(ref b10, A15.A31(path), A);
            }
            else
            {
                A26.A54(ref b10, A15.A26(A15.A31(path)), A);
            }
            int num2 = base._A95 + 2;
            b10.A59(string.Format("/EF << /F {0} 0 R >>", num2));
            b10.A59(">>");
            b10.A59("endobj");
            if (this._b1 != null)
            {
                A2 = new A112(this._b1);
                if (base._A92.Compress)
                {
                    A2.A123();
                }
            }
            else
            {
                A2 = A112.A113(base._A92, this._b0, null, false);
            }
            base._A92.A93.A94(b10.A2, 0);
            b10.A59(string.Format("{0} 0 obj", num2));
            b10.A59("<<");
            b10.A59("/Type /EmbeddedFile ");
            if (A2.Compressed)
            {
                b10.A59(string.Format("/Length {0} /Filter /FlateDecode ", A2.A2));
            }
            else
            {
                b10.A59(string.Format("/Length {0} ", A2.A2));
            }
            b10.A59(">>");
            A2.A114(b10, num2, A);
            b10.A59("endobj");
        }
    }
}

