namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class AdditionalAction
    {
        private AdditionalActionType _b0 = AdditionalActionType.Keystroke;
        private string _b1 = string.Empty;

        internal string A126()
        {
            if (this._b0 == AdditionalActionType.Keystroke)
            {
                return "K";
            }
            if (this._b0 == AdditionalActionType.Format)
            {
                return "F";
            }
            if (this._b0 == AdditionalActionType.ValueChange)
            {
                return "V";
            }
            return "C";
        }

        internal void A54(int b2, ref Document b3, ref A55 b4)
        {
            int num = b2 + 1;
            A56 A = b3.A56;
            if (A != null)
            {
                A.A100(b2, 0);
            }
            b3.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", b2));
            b4.A59("<<");
            b4.A59("/S /JavaScript");
            b4.A59(string.Format("/JS {0} 0 R", num));
            b4.A59(">>");
            b4.A59("endobj");
            A112 A2 = new A112();
            A2.A59(this._b1);
            if (b3.Compress)
            {
                A2.A123();
            }
            b3.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", num));
            b4.A59("<<");
            if (A2.Compressed)
            {
                b4.A59(string.Format("/Length {0} /Filter /FlateDecode ", A2.A2));
            }
            else
            {
                b4.A59(string.Format("/Length {0} ", A2.A2));
            }
            b4.A59(">>");
            A2.A114(b4, num, A);
            b4.A59("endobj");
        }

        public string ActionScript
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        public AdditionalActionType ActionType
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }
    }
}

