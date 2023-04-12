namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Code39Graphics : BarcodeGraphics
    {
        internal string _A450;
        private float _b1 = 3f;
        private float _b2;
        private float _b3;
        private char _b4;
        private static string b0 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b10, ref A112 b11, float b12)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b10.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b10, ref b11);
            int length = this._A450.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b13(ref b10, ref b11, this.b8(this._A450[i]), num, num2);
            }
            base.A444(ref b10, ref b11, num2, b12, this._b4.ToString());
        }

        internal override float A431(bool b5)
        {
            float num = 0f;
            if (b5)
            {
                this.A432();
            }
            num += this.b6() * this._A450.Length;
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._A450 = base._A416;
            if (this.EnableCheckSum)
            {
                this._b4 = A448(base._A416);
                this._A450 = this._A450 + this._b4;
            }
            this._A450 = '*' + this._A450 + '*';
        }

        internal override void A433()
        {
            this._b2 = this._b1 * base._A423;
            this._b3 = base._A423;
        }

        internal static char A448(string b7)
        {
            int num = 0;
            int length = b7.Length;
            for (int i = 0; i < length; i++)
            {
                num += b0.IndexOf(b7[i]);
            }
            return b0[num % 0x2b];
        }

        internal override void A86()
        {
            this._A450 = string.Empty;
        }

        private float b13(ref A120 b10, ref A112 b11, string b14, float b15, float b16)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b14.Length; i++)
            {
                num2 = this.b9(b14[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b11, b15, b16, num2, base._A422);
                    GraphicsElement.A180(b11, GraphicsMode.fill, false);
                }
                b15 += num2;
                num += num2;
            }
            return num;
        }

        private float b6()
        {
            return ((3f * this._b2) + (7f * this._b3));
        }

        private string b8(char c)
        {
            switch (c)
            {
                case ' ':
                    return "nwwnnnwnnn";

                case '$':
                    return "nwnwnwnnnn";

                case '%':
                    return "nnnwnwnwnn";

                case '*':
                    return "nwnnwnwnnn";

                case '+':
                    return "nwnnnwnwnn";

                case '-':
                    return "nwnnnnwnwn";

                case '.':
                    return "wwnnnnwnnn";

                case '/':
                    return "nwnwnnnwnn";

                case '0':
                    return "nnnwwnwnnn";

                case '1':
                    return "wnnwnnnnwn";

                case '2':
                    return "nnwwnnnnwn";

                case '3':
                    return "wnwwnnnnnn";

                case '4':
                    return "nnnwwnnnwn";

                case '5':
                    return "wnnwwnnnnn";

                case '6':
                    return "nnwwwnnnnn";

                case '7':
                    return "nnnwnnwnwn";

                case '8':
                    return "wnnwnnwnnn";

                case '9':
                    return "nnwwnnwnnn";

                case 'A':
                    return "wnnnnwnnwn";

                case 'B':
                    return "nnwnnwnnwn";

                case 'C':
                    return "wnwnnwnnnn";

                case 'D':
                    return "nnnnwwnnwn";

                case 'E':
                    return "wnnnwwnnnn";

                case 'F':
                    return "nnwnwwnnnn";

                case 'G':
                    return "nnnnnwwnwn";

                case 'H':
                    return "wnnnnwwnnn";

                case 'I':
                    return "nnwnnwwnnn";

                case 'J':
                    return "nnnnwwwnnn";

                case 'K':
                    return "wnnnnnnwwn";

                case 'L':
                    return "nnwnnnnwwn";

                case 'M':
                    return "wnwnnnnwnn";

                case 'N':
                    return "nnnnwnnwwn";

                case 'O':
                    return "wnnnwnnwnn";

                case 'P':
                    return "nnwnwnnwnn";

                case 'Q':
                    return "nnnnnnwwwn";

                case 'R':
                    return "wnnnnnwwnn";

                case 'S':
                    return "nnwnnnwwnn";

                case 'T':
                    return "nnnnwnwwnn";

                case 'U':
                    return "wwnnnnnnwn";

                case 'V':
                    return "nwwnnnnnwn";

                case 'W':
                    return "wwwnnnnnnn";

                case 'X':
                    return "nwnnwnnnwn";

                case 'Y':
                    return "wwnnwnnnnn";

                case 'Z':
                    return "nwwnwnnnnn";
            }
            return "";
        }

        private float b9(char c)
        {
            if (c != 'n')
            {
                return this._b2;
            }
            return this._b3;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Code39;
            }
        }

        public float WideNarrowRatio
        {
            get
            {
                return this._b1;
            }
            set
            {
                if (value < 2.2f)
                {
                    value = 2.2f;
                }
                if (value > 3f)
                {
                    value = 3f;
                }
                this._b1 = value;
            }
        }
    }
}

