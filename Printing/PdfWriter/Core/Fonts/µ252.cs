namespace MControl.Printing.Pdf.Core.Fonts
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    internal class A252
    {
        protected string _A247;
        protected FontStyle _A248;
        protected int _A253;
        protected int _A254;
        protected int _A255 = 0;
        protected string _A259;
        protected bool _A402;
        protected Document _A92;
        protected int _A95;

        internal A252()
        {
        }

        internal virtual void A110()
        {
            int num;
            A93 A1 = this._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            this._A95 = num;
        }

        internal virtual void A159(string b5)
        {
        }

        internal virtual string A163(string b5)
        {
            string str = string.Empty;
            for (int i = 0; i < b5.Length; i++)
            {
                char ch = b5[i];
                if (ch > '\x00ff')
                {
                    return A15.A27(b5);
                }
                str = str + A15.A25(ch);
            }
            return string.Format("{0}{1}{2}", "(", str, ")");
        }

        internal void A237(Document b0, int b1)
        {
            this._A92 = b0;
            this._A259 = string.Format("F{0}", b1);
        }

        internal virtual float A256(char ch)
        {
            return 0f;
        }

        internal virtual float A256(ushort b2)
        {
            return 0f;
        }

        internal virtual float A257(char b3, float b4)
        {
            return 0f;
        }

        internal virtual float A258(string b5, float b4)
        {
            return 0f;
        }

        internal virtual void A406(char b3)
        {
        }

        internal virtual void A54(ref A55 b6)
        {
        }

        internal string A168
        {
            get
            {
                return this._A259;
            }
        }

        internal virtual bool A342
        {
            get
            {
                return false;
            }
        }

        internal string A357
        {
            get
            {
                return this._A247;
            }
        }

        internal virtual Font A403
        {
            get
            {
                return null;
            }
        }

        internal FontStyle A405
        {
            get
            {
                return this._A248;
            }
        }

        internal bool A407
        {
            get
            {
                return this._A402;
            }
        }

        internal int A408
        {
            get
            {
                return this._A253;
            }
        }

        internal int A409
        {
            get
            {
                return this._A254;
            }
        }

        internal int A410
        {
            get
            {
                return this._A255;
            }
        }

        internal int A95
        {
            get
            {
                return this._A95;
            }
        }
    }
}

