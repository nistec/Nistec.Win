namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Code93Graphics : BarcodeGraphics
    {
        internal string _A450;
        private string _b2;
        private static string b0 = (BarcodeGraphics.A424 + BarcodeGraphics.A425 + "-. $/+%");
        private static string b1 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%?:!~*";

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b9, ref A112 b10, float b11)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b9.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b9, ref b10);
            int length = this._A450.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b12(ref b9, ref b10, this.A453(this._A450[i]), num, num2);
            }
            base.A444(ref b9, ref b10, num2, b11, this._b2);
        }

        internal override float A431(bool b3)
        {
            float num = 0f;
            if (b3)
            {
                this.A432();
            }
            num += this.A452() * this._A450.Length;
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._A450 = this.A451();
            if (this.EnableCheckSum)
            {
                char ch = b4(this._A450);
                this._A450 = this._A450 + ch;
                char ch2 = b5(this._A450);
                this._A450 = this._A450 + ch2;
                this._b2 = ch.ToString() + ch2.ToString();
            }
            else
            {
                this._b2 = "";
            }
            this._A450 = '*' + this._A450 + '|';
        }

        internal override void A433()
        {
        }

        internal static char A448(string b6, string b1, int b7, int b8)
        {
            int num = 0;
            int num2 = 1;
            for (int i = b6.Length - 1; i >= 0; i--)
            {
                num += b1.IndexOf(b6[i]) * num2;
                if (num2 == b7)
                {
                    num2 = 1;
                }
                else
                {
                    num2++;
                }
            }
            return b1[num % b8];
        }

        internal virtual string A451()
        {
            return base._A416;
        }

        internal float A452()
        {
            return (base._A423 * 9f);
        }

        internal string A453(char c)
        {
            switch (c)
            {
                case ' ':
                    return "311211";

                case '!':
                    return "311121";

                case '$':
                    return "321111";

                case '%':
                    return "211131";

                case '*':
                    return "111141";

                case '+':
                    return "113121";

                case '-':
                    return "121131";

                case '.':
                    return "311112";

                case '/':
                    return "112131";

                case '0':
                    return "131112";

                case '1':
                    return "111213";

                case '2':
                    return "111312";

                case '3':
                    return "111411";

                case '4':
                    return "121113";

                case '5':
                    return "121212";

                case '6':
                    return "121311";

                case '7':
                    return "111114";

                case '8':
                    return "131211";

                case '9':
                    return "141111";

                case ':':
                    return "312111";

                case '?':
                    return "121221";

                case 'A':
                    return "211113";

                case 'B':
                    return "211212";

                case 'C':
                    return "211311";

                case 'D':
                    return "221112";

                case 'E':
                    return "221211";

                case 'F':
                    return "231111";

                case 'G':
                    return "112113";

                case 'H':
                    return "112212";

                case 'I':
                    return "112311";

                case 'J':
                    return "122112";

                case 'K':
                    return "132111";

                case 'L':
                    return "111123";

                case 'M':
                    return "111222";

                case 'N':
                    return "111321";

                case 'O':
                    return "121122";

                case 'P':
                    return "131121";

                case 'Q':
                    return "212112";

                case 'R':
                    return "212211";

                case 'S':
                    return "211122";

                case 'T':
                    return "211221";

                case 'U':
                    return "221121";

                case 'V':
                    return "222111";

                case 'W':
                    return "112122";

                case 'X':
                    return "112221";

                case 'Y':
                    return "122121";

                case 'Z':
                    return "123111";

                case '|':
                    return "1111411";

                case '~':
                    return "122211";
            }
            return "";
        }

        internal float A454(char c)
        {
            return (base._A423 * (c - '0'));
        }

        internal override void A86()
        {
            this._A450 = string.Empty;
            this._b2 = string.Empty;
        }

        private float b12(ref A120 b9, ref A112 b10, string b13, float b14, float b15)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b13.Length; i++)
            {
                num2 = this.A454(b13[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b10, b14, b15, num2, base._A422);
                    GraphicsElement.A180(b10, GraphicsMode.fill, false);
                }
                b14 += num2;
                num += num2;
            }
            return num;
        }

        private static char b4(string b6)
        {
            return A448(b6, b1, 20, 0x2f);
        }

        private static char b5(string b6)
        {
            return A448(b6, b1, 15, 0x2f);
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Code93;
            }
        }
    }
}

