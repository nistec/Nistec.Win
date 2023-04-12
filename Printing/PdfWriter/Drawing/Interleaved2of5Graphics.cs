namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Text;

    public class Interleaved2of5Graphics : BarcodeGraphics
    {
        private string _b1;
        private string _b2;
        private float _b3 = 3f;
        private float _b4;
        private float _b5;
        private static string b0 = BarcodeGraphics.A424;

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b12, ref A112 b13, float b14)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b12.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b12, ref b13);
            int length = this._b2.Length;
            float num4 = 0f;
            for (int i = 0; i < length; i++)
            {
                num4 = this.b7(this._b2[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b13, num, num2, num4, base._A422);
                    GraphicsElement.A180(b13, GraphicsMode.fill, false);
                }
                num += num4;
            }
            string str = base._A416;
            if (this.EnableCheckSum)
            {
                if (((str.Length + 1) % 2) != 0)
                {
                    str = '0' + str;
                }
            }
            else if ((str.Length % 2) != 0)
            {
                str = '0' + str;
            }
            base.A444(ref b12, ref b13, num2, b14, str, this._b1);
        }

        internal override float A431(bool b6)
        {
            float num = 0f;
            if (b6)
            {
                this.A432();
            }
            int length = this._b2.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b7(this._b2[i]);
            }
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._b2 = base._A416;
            if (this.EnableCheckSum)
            {
                this._b1 = Industrial2of5Graphics.A448(this._b2).ToString();
                this._b2 = this._b2 + this._b1;
            }
            else
            {
                this._b1 = "";
            }
            if ((this._b2.Length % 2) != 0)
            {
                this._b2 = '0' + this._b2;
            }
            string str3 = string.Empty;
            int length = this._b2.Length;
            for (int i = 0; i < length; i += 2)
            {
                string str = this.b8(this._b2[i]);
                if (str != null)
                {
                    string str2 = this.b8(this._b2[i + 1]);
                    if (str2 != null)
                    {
                        str3 = str3 + this.b9(str, str2);
                    }
                }
            }
            this._b2 = this.b8('S') + str3 + this.b8('F');
        }

        internal override void A433()
        {
            this._b4 = this._b3 * base._A423;
            this._b5 = base._A423;
        }

        internal override void A86()
        {
            this._b1 = string.Empty;
            this._b2 = string.Empty;
        }

        private float b7(char c)
        {
            if (c != 'n')
            {
                return this._b4;
            }
            return this._b5;
        }

        private string b8(char c)
        {
            switch (c)
            {
                case '0':
                    return "nnwwn";

                case '1':
                    return "wnnnw";

                case '2':
                    return "nwnnw";

                case '3':
                    return "wwnnn";

                case '4':
                    return "nnwnw";

                case '5':
                    return "wnwnn";

                case '6':
                    return "nwwnn";

                case '7':
                    return "nnnww";

                case '8':
                    return "wnnwn";

                case '9':
                    return "nwnwn";

                case 'F':
                    return "wnn";

                case 'S':
                    return "nnnn";
            }
            return "";
        }

        private string b9(string b10, string b11)
        {
            int length = b10.Length;
            StringBuilder builder = new StringBuilder(2 * length);
            for (int i = 0; i < length; i++)
            {
                builder.Append(b10[i]);
                builder.Append(b11[i]);
            }
            return builder.ToString();
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Interleaved2of5;
            }
        }

        public float WideNarrowRatio
        {
            get
            {
                return this._b3;
            }
            set
            {
                if (value < 2.5f)
                {
                    value = 2.5f;
                }
                this._b3 = value;
            }
        }
    }
}

