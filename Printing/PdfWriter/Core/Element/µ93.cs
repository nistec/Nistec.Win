namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;

    internal class A93 : A91
    {
        private int _b0;
        private ArrayList _b1;
        internal int A223;

        internal A93(Document b2)
        {
            base._A92 = b2;
            this.A95 = 1;
            this.A223 = 1;
            this._b1 = new ArrayList();
        }

        internal override void A54(ref A55 b5)
        {
            int num = this._b1.Count + 1;
            this._b0 = b5.A2;
            b5.A59("xref");
            b5.A59(string.Format("0 {0}", num));
            b5.A59("0000000000 65535 f ");
            for (int i = 0; i < this._b1.Count; i++)
            {
                A224 A = (A224) this._b1[i];
                b5.A59(string.Format("{0} {1} n ", A.A85.ToString("0000000000"), A.A225.ToString("00000")));
            }
            b5.A59("trailer");
            b5.A59("<<");
            b5.A59(string.Format("/Size {0} ", num));
            b5.A59(string.Format("/Root {0} 0 R ", base._A92.A185.A95));
            b5.A59(string.Format("/Info {0} 0 R ", base._A92.A199.A95));
            if (base._A92.A56 != null)
            {
                b5.A59(string.Format("/Encrypt {0} 0 R ", base._A92.A56.A95));
            }
            b5.A54("/ID[<");
            b5.A58(base._A92.A226);
            b5.A54("><");
            b5.A58(base._A92.A226);
            b5.A59(">]");
            b5.A59(">>");
            b5.A59("startxref");
            b5.A59(this._b0.ToString());
            b5.A59("%%EOF");
        }

        internal void A94(int b3, int b4)
        {
            this._b1.Add(new A224(b3, b4));
        }

        internal override int A95
        {
            get
            {
                return base._A95;
            }
            set
            {
                base._A95 = value;
            }
        }
    }
}

