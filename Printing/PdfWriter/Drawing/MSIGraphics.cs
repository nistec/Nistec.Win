namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Text;

    public class MSIGraphics : BarcodeGraphics
    {
        private MSICheckSum _b1 = MSICheckSum.Modulo10;
        private string _b2;
        private string _b3;
        private float _b4 = 2f;
        private float _b5;
        private float _b6;
        private static string b0 = BarcodeGraphics.A424;

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b16, ref A112 b17, float b18)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b16.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b16, ref b17);
            int length = this._b3.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b19(ref b16, ref b17, this.b14(this._b3[i]), num, num2);
            }
            base.A444(ref b16, ref b17, num2, b18, this._b2);
        }

        internal override float A431(bool b7)
        {
            float num = 0f;
            if (b7)
            {
                this.A432();
            }
            int length = this._b3.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b8(this._b3[i]);
            }
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._b3 = base._A416;
            if (this.EnableCheckSum)
            {
                this._b2 = this.b9(this._b3);
                this._b3 = this._b3 + this._b2;
            }
            else
            {
                this._b2 = "";
            }
            this._b3 = '*' + this._b3 + '|';
        }

        internal override void A433()
        {
            this._b5 = this._b4 * base._A423;
            this._b6 = base._A423;
        }

        internal override void A86()
        {
            this._b2 = string.Empty;
            this._b3 = string.Empty;
        }

        private static char b11(string b10)
        {
            int num = 0;
            int num2 = 0;
            string str = string.Empty;
            bool flag = true;
            for (int i = b10.Length - 1; i >= 0; i--)
            {
                if (flag)
                {
                    str = b10[i] + str;
                }
                else
                {
                    num += BarcodeGraphics.A439(b10[i]);
                }
                flag = !flag;
            }
            str = b12(str);
            int length = str.Length;
            for (int j = 0; j < length; j++)
            {
                num2 += BarcodeGraphics.A439(str[j]);
            }
            int num6 = num2 + num;
            num6 = num6 % 10;
            if (num6 != 0)
            {
                num6 = 10 - num6;
            }
            return (char) (0x30 + num6);
        }

        private static string b12(string b13)
        {
            int num2 = 0;
            int length = b13.Length;
            StringBuilder builder = new StringBuilder(length);
            for (int i = length - 1; i >= 0; i--)
            {
                int num = (BarcodeGraphics.A439(b13[i]) * 2) + num2;
                num2 = (num >= 10) ? 1 : 0;
                num = (num2 > 0) ? (num - 10) : num;
                builder.Insert(0, (char) (0x30 + num));
            }
            if (num2 > 0)
            {
                builder.Insert(0, '1');
            }
            return builder.ToString();
        }

        private string b14(char c)
        {
            switch (c)
            {
                case '*':
                    return "wn";

                case '0':
                    return "nwnwnwnw";

                case '1':
                    return "nwnwnwwn";

                case '2':
                    return "nwnwwnnw";

                case '3':
                    return "nwnwwnwn";

                case '4':
                    return "nwwnnwnw";

                case '5':
                    return "nwwnnwwn";

                case '6':
                    return "nwwnwnnw";

                case '7':
                    return "nwwnwnwn";

                case '8':
                    return "wnnwnwnw";

                case '9':
                    return "wnnwnwwn";

                case '|':
                    return "nwn";
            }
            return "";
        }

        private float b15(char c)
        {
            if (c != 'n')
            {
                return this._b5;
            }
            return this._b6;
        }

        private float b19(ref A120 b16, ref A112 b17, string b20, float b21, float b22)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b20.Length; i++)
            {
                num2 = this.b15(b20[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b17, b21, b22, num2, base._A422);
                    GraphicsElement.A180(b17, GraphicsMode.fill, false);
                }
                b21 += num2;
                num += num2;
            }
            return num;
        }

        private float b8(char c)
        {
            switch (c)
            {
                case '*':
                    return ((1f * this._b5) + (1f * this._b6));

                case '|':
                    return ((1f * this._b5) + (2f * this._b6));
            }
            return ((4f * this._b5) + (4f * this._b6));
        }

        private string b9(string b10)
        {
            string str = string.Empty;
            switch (this._b1)
            {
                case MSICheckSum.Modulo10:
                    return b11(b10).ToString();

                case MSICheckSum.DoubleModulo10:
                {
                    str = b11(b10).ToString();
                    char ch = b11(b10 + str);
                    return (str + ch.ToString());
                }
            }
            return str;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.MSI;
            }
        }

        public MSICheckSum CheckSumType
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

        public override bool EnableCheckSum
        {
            get
            {
                return (this._b1 != MSICheckSum.None);
            }
            set
            {
            }
        }
    }
}

