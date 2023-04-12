namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Globalization;
    using System.IO;

    internal class A103 : A90
    {
        private int _b0;
        internal int A104;
        internal int A105;
        internal SoundChannels A106;
        internal SoundIconType A107;
        internal Stream A108;
        internal string A109;
        internal SoundEncoding A17;

        internal A103(Document b1) : base(b1)
        {
            this.A104 = 8;
            this.A105 = 0x5622;
            this.A106 = SoundChannels.Mono;
            this._b0 = 1;
        }

        internal override void A110()
        {
            int num;
            A93 A1 = base._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            base._A95 = num;
            base._A92.A93.A95 = base._A95 + 2;
        }

        internal override void A54(ref A55 b2)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base.A54(ref b2);
            this.A54(ref b2, A);
        }

        internal void A54(ref A55 b2, A56 b3)
        {
            int num = this.A95 + 1;
            b2.A59("/S /Sound");
            b2.A59(string.Format("Sound {0} 0 obj", num));
            b2.A59(string.Format("/Volume {0}", this.A111));
            b2.A59(">>");
            b2.A59("endobj");
            A112 A = A112.A113(base._A92, this.A109, this.A108, true);
            base._A92.A93.A94(b2.A2, 0);
            b2.A59(string.Format("{0} 0 obj", num));
            b2.A59("<<");
            b2.A59(string.Format("/C {0}", (int) this.A106));
            b2.A59(string.Format("/R {0}", this.A105));
            b2.A59(string.Format("/E /{0}", this.A17.ToString(CultureInfo.InvariantCulture.ToString())));
            b2.A59(string.Format("/B {0}", this.A104));
            if (A.Compressed)
            {
                b2.A59(string.Format("/Length {0} /Filter /FlateDecode ", A.A2));
            }
            else
            {
                b2.A59(string.Format("/Length {0} ", A.A2));
            }
            b2.A59(">>");
            A.A114(b2, num, b3);
            b2.A59("endobj");
        }

        internal int A111
        {
            get
            {
                return this._b0;
            }
            set
            {
                if (this._b0 != value)
                {
                    this._b0 = value;
                    if (value > 1)
                    {
                        this._b0 = 1;
                    }
                    if (value < -1)
                    {
                        this._b0 = -1;
                    }
                }
            }
        }
    }
}

