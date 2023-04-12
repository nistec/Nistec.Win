namespace MControl.Printing.Pdf.Core.IO
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using System;
    using System.IO;

    internal class A112 : A243
    {
        private int _b0;
        private bool _b1;

        internal A112()
        {
            this._b0 = 0;
            this._b1 = false;
        }

        internal A112(byte[] b2)
        {
            base._A550 = b2;
            base._A551 = b2.Length;
        }

        internal A112(byte[] b2, int b3)
        {
            base._A550 = b2;
            base._A551 = b3;
        }

        internal static A112 A113(Document b8, string b9, Stream b10, bool b11)
        {
            byte[] buffer = null;
            if (b10 != null)
            {
                buffer = A15.A32(b10, b11);
                b10.Close();
            }
            else if ((b9 != null) && File.Exists(b9))
            {
                using (FileStream stream = File.OpenRead(b9))
                {
                    buffer = A15.A32(stream, b11);
                }
            }
            if (buffer != null)
            {
                A112 A = new A112(buffer);
                if (b8.Compress)
                {
                    A.A123();
                }
                return A;
            }
            return new A112();
        }

        internal void A114(A55 b4, int b5, A56 b7)
        {
            b4.A181("stream");
            byte[] data = base.A221;
            if (base._A551 > 0)
            {
                if (b7 != null)
                {
                    b7.A100(b5, 0);
                    data = b7.A57(data, base._A551);
                    b4.A54(data, data.Length);
                }
                else
                {
                    b4.A54(data, base._A551);
                }
            }
            b4.A54((byte) 13);
            b4.A59("endstream");
        }

        internal void A123()
        {
            this._b0 = base._A551;
            if (this.A2 > 0x1d)
            {
                base._A550 = A33.A34(base._A550, base._A551);
                base._A551 = base._A550.Length;
                this._b1 = true;
            }
        }

        internal void A54(A55 b4, int b5, bool b6, A56 b7)
        {
            b4.A59(string.Format("{0} 0 obj", b5));
            if (this._b1)
            {
                b4.A54(string.Format("<< /Length {0} /Filter /FlateDecode ", base._A551));
            }
            else
            {
                b4.A54(string.Format("<< /Length {0} ", base._A551));
            }
            if (b6)
            {
                b4.A59(string.Format("/Length1 {0} >>", this._b0));
            }
            else
            {
                b4.A59(">>");
            }
            this.A114(b4, b5, b7);
            b4.A59("endobj");
        }

        internal bool Compressed
        {
            get
            {
                return this._b1;
            }
        }
    }
}

