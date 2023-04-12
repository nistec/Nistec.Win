namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class EAN13Graphics : BarcodeGraphics
    {
        private float _A456;
        private int _b0;
        private MControl.Printing.Pdf.Drawing.UPCSupplement _b1;
        private string _b2;
        internal string A450;
        internal string A455;
        private static string b3 = BarcodeGraphics.A424;

        public EAN13Graphics()
        {
            base.CodeTextVisible = true;
            this.A455 = string.Empty;
            this._b0 = 5;
            this._b1 = MControl.Printing.Pdf.Drawing.UPCSupplement.None;
            this._b2 = string.Empty;
            base._A419 = TextAlignment.Center;
        }

        internal override string A429()
        {
            return b3;
        }

        internal override void A430(ref A120 b11, ref A112 b12, float b13)
        {
            float textWidth = 0f;
            GraphicsElement.A160(base._A421, ref b11, ref b12);
            int num4 = 0;
            if (base.CodeTextVisible)
            {
                char ch2 = base._A416[0];
                textWidth = base.CodeTextFont.GetTextWidth(ch2.ToString(), base.CodeTextFontSize);
            }
            float num = (int) ((base.X + base._A413) + textWidth);
            float num2 = (int) b11.A98(this.A440());
            string str = this.b6(this.A450[1]);
            num += this.A464(ref b11, ref b12, this.A453(this.A450[0], false), num, num2, ref num4, true);
            for (int i = 2; i < this.A450.Length; i++)
            {
                bool flag;
                bool flag2;
                char c = this.A450[i];
                switch (c)
                {
                    case '*':
                    case '|':
                        flag = true;
                        break;

                    default:
                        flag = false;
                        break;
                }
                if (((i > 2) && (i < 8)) && (str[i - 3] == 'e'))
                {
                    flag2 = true;
                }
                else
                {
                    flag2 = false;
                }
                num += this.A464(ref b11, ref b12, this.A453(c, flag2), num, num2, ref num4, flag);
            }
            float num6 = (b13 - base._A413) - base._A415;
            float num7 = num2 - base._A422;
            if (base.CodeTextVisible)
            {
                float num11 = base.CodeTextSpace / 2f;
                string str2 = base._A416 + this.A455;
                string str3 = str2[0].ToString();
                float num8 = (int) (base.X + base._A413);
                float num9 = (num2 - base._A422) - base.CodeTextSpace;
                float num10 = textWidth;
                BarcodeGraphics.A443(ref b11, ref b12, str3, num8, num9, num10, num11, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str3 = str2.Substring(1, 6);
                num8 += (3f * base._A423) + textWidth;
                num10 = 42f * base._A423;
                BarcodeGraphics.A443(ref b11, ref b12, str3, num8, num9, num10, num11, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str3 = str2.Substring(7, str2.Length - 7);
                num8 += 47f * base._A423;
                BarcodeGraphics.A443(ref b11, ref b12, str3, num8, num9, num10, num11, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                num7 -= (base.CodeTextSpace + base.CodeTextFont.Height(base.CodeTextFontSize)) + base.CaptionSpace;
            }
            else
            {
                num7 -= base.CaptionSpace;
            }
            this.A465(ref b11, ref b12, num, num2);
            base.A442(ref b11, ref b12, num7, num6);
        }

        internal override float A431(bool b4)
        {
            float num = 0f;
            if (b4)
            {
                this.A432();
            }
            num = (((this.A458() + this.A459()) * 7) + this.A460()) * base._A423;
            num += base._A413 + base._A415;
            if (base.CodeTextVisible)
            {
                char ch = this.A450[0];
                num += this.A461(ch.ToString()) + this.A462(this.A455.ToString());
            }
            if (this._b1 != MControl.Printing.Pdf.Drawing.UPCSupplement.None)
            {
                num += this._b0 + this.A463();
            }
            return num;
        }

        internal override void A432()
        {
            if (base._A416.Length == (this.A458() + 1))
            {
                base._A416 = base._A416.Substring(0, this.A458());
            }
            this.A450 = base._A416;
            if (this.EnableCheckSum)
            {
                this.A455 = Industrial2of5Graphics.A448(this.A450).ToString();
                this.A450 = this.A450 + this.A455;
            }
            else
            {
                this.A455 = "";
            }
            this.A450 = this.A450.Insert(this.A457(), '|'.ToString());
            this.A450 = '*' + this.A450 + '*';
        }

        internal override void A433()
        {
            this._A456 = base.CodeTextFont.Height(base.CodeTextFontSize);
        }

        internal override float A434()
        {
            float num = base.A434();
            if (!base.CodeTextVisible)
            {
                num += (int) (base.CodeTextFont.Height(base.CodeTextFontSize) / 2f);
            }
            return num;
        }

        internal override float A440()
        {
            float num = base.A440();
            if (this.UPCSupplement != MControl.Printing.Pdf.Drawing.UPCSupplement.None)
            {
                num += (int) (base.CodeTextFont.Height(base.CodeTextFontSize) / 2f);
            }
            return num;
        }

        internal string A453(char c, bool b5)
        {
            switch (c)
            {
                case '*':
                    return "111";

                case '+':
                    return "11";

                case '0':
                    if (!b5)
                    {
                        return "3211";
                    }
                    return "1123";

                case '1':
                    if (!b5)
                    {
                        return "2221";
                    }
                    return "1222";

                case '2':
                    if (!b5)
                    {
                        return "2122";
                    }
                    return "2212";

                case '3':
                    if (!b5)
                    {
                        return "1411";
                    }
                    return "1141";

                case '4':
                    if (!b5)
                    {
                        return "1132";
                    }
                    return "2311";

                case '5':
                    if (!b5)
                    {
                        return "1231";
                    }
                    return "1321";

                case '6':
                    if (!b5)
                    {
                        return "1114";
                    }
                    return "4111";

                case '7':
                    if (!b5)
                    {
                        return "1312";
                    }
                    return "2131";

                case '8':
                    if (!b5)
                    {
                        return "1213";
                    }
                    return "3121";

                case '9':
                    if (!b5)
                    {
                        return "3112";
                    }
                    return "2113";

                case '<':
                    return "112";

                case '>':
                    return "111111";

                case '|':
                    return "11111";
            }
            return "";
        }

        internal virtual int A457()
        {
            return 7;
        }

        internal virtual int A458()
        {
            return 12;
        }

        internal virtual int A459()
        {
            return 1;
        }

        internal virtual int A460()
        {
            return 11;
        }

        internal virtual float A461(string c)
        {
            return base.CodeTextFont.GetTextWidth(c, base.CodeTextFontSize);
        }

        internal virtual float A462(string c)
        {
            return 0f;
        }

        internal float A463()
        {
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_2_Digit)
            {
                return (20f * base._A423);
            }
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_5_Digit)
            {
                return (47f * base._A423);
            }
            return 0f;
        }

        internal float A464(ref A120 b11, ref A112 b12, string b16, float b14, float b15, ref int b17, bool b18)
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = base._A422;
            if (b18)
            {
                num3 += (int) (this._A456 / 2f);
            }
            for (int i = 0; i < b16.Length; i++)
            {
                num2 = base._A423 * BarcodeGraphics.A439(b16[i]);
                if ((b17 % 2) == 0)
                {
                    GraphicsElement.A437(b12, b14, b15, num2, num3);
                    GraphicsElement.A180(b12, GraphicsMode.fill, false);
                }
                b14 += num2;
                num += num2;
                b17++;
            }
            return num;
        }

        internal void A465(ref A120 b11, ref A112 b12, float b14, float b15)
        {
            if (IsValidSupplement(this._b1, this._b2))
            {
                float num = base.CodeTextFont.Height(base.CodeTextFontSize);
                float num2 = b15 - ((int) (num / 2f));
                float num3 = b14 + this._b0;
                float num4 = num3;
                float num5 = num2 - num;
                bool flag = false;
                bool flag2 = false;
                int num7 = 0;
                int num8 = this.b7();
                string str = this.b9(num8);
                num3 += this.A464(ref b11, ref b12, this.A453('<', false), num3, num2, ref num7, false);
                int length = this._b2.Length;
                for (int i = 0; i < length; i++)
                {
                    char c = this._b2[i];
                    if (flag)
                    {
                        num3 += this.A464(ref b11, ref b12, this.A453('+', false), num3, num2, ref num7, false);
                    }
                    if (str[i] == 'e')
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag2 = false;
                    }
                    num3 += this.A464(ref b11, ref b12, this.A453(c, flag2), num3, num2, ref num7, false);
                    flag = true;
                }
                if (base.CodeTextVisible)
                {
                    float num11 = base.CodeTextSpace / 2f;
                    float num6 = this.A463();
                    BarcodeGraphics.A443(ref b11, ref b12, this._b2, num4, num5, num6, num11, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                }
            }
        }

        internal override void A86()
        {
            this.A455 = string.Empty;
            this.A450 = string.Empty;
        }

        public override bool IsValidData(string data)
        {
            return (((data.Length == this.A458()) || (data.Length == (this.A458() + 1))) && base.A447(data));
        }

        public static bool IsValidSupplement(MControl.Printing.Pdf.Drawing.UPCSupplement supplement, string data)
        {
            if ((data != null) && (data.Length > 0))
            {
                if (supplement == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_2_Digit)
                {
                    if (data.Length != 2)
                    {
                        return false;
                    }
                    int length = data.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (b3.IndexOf(data[i]) < 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                if (supplement == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_5_Digit)
                {
                    if (data.Length != 5)
                    {
                        return false;
                    }
                    int num3 = data.Length;
                    for (int j = 0; j < num3; j++)
                    {
                        if (b3.IndexOf(data[j]) < 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private string b6(char c)
        {
            switch (c)
            {
                case '0':
                    return "ooooo";

                case '1':
                    return "oeoee";

                case '2':
                    return "oeeoe";

                case '3':
                    return "oeeeo";

                case '4':
                    return "eooee";

                case '5':
                    return "eeooe";

                case '6':
                    return "eeeoo";

                case '7':
                    return "eoeoe";

                case '8':
                    return "eoeeo";

                case '9':
                    return "eeoeo";
            }
            return "";
        }

        private int b7()
        {
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_2_Digit)
            {
                int num = (BarcodeGraphics.A439(this._b2[0]) * 10) + BarcodeGraphics.A439(this._b2[1]);
                return (num % 4);
            }
            if (this._b1 != MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_5_Digit)
            {
                return 0;
            }
            bool flag = false;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            for (int i = this._b2.Length - 1; i >= 0; i--)
            {
                if (flag)
                {
                    num2 += BarcodeGraphics.A439(this._b2[i]);
                }
                else
                {
                    num3 += BarcodeGraphics.A439(this._b2[i]);
                }
                flag = !flag;
            }
            num4 = (num3 * 3) + (num2 * 9);
            return (num4 % 10);
        }

        private int b8()
        {
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_2_Digit)
            {
                return 2;
            }
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_5_Digit)
            {
                return 5;
            }
            return 0;
        }

        private string b9(int b10)
        {
            if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_2_Digit)
            {
                switch (b10)
                {
                    case 0:
                        return "oo";

                    case 1:
                        return "oe";

                    case 2:
                        return "eo";

                    case 3:
                        return "ee";
                }
            }
            else if (this._b1 == MControl.Printing.Pdf.Drawing.UPCSupplement.Supplement_5_Digit)
            {
                switch (b10)
                {
                    case 0:
                        return "eeooo";

                    case 1:
                        return "eoeoo";

                    case 2:
                        return "eooeo";

                    case 3:
                        return "eoooe";

                    case 4:
                        return "oeeoo";

                    case 5:
                        return "ooeeo";

                    case 6:
                        return "oooee";

                    case 7:
                        return "oeoeo";

                    case 8:
                        return "oeooe";

                    case 9:
                        return "ooeoe";
                }
            }
            return "";
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.EAN13;
            }
        }

        public override TextAlignment CodeTextAlignment
        {
            get
            {
                return base._A419;
            }
            set
            {
            }
        }

        public override bool CodeTextAsSymbol
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override bool EnableCheckSum
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public int SupplementLeftPad
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

        public MControl.Printing.Pdf.Drawing.UPCSupplement UPCSupplement
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

        public string UPCSupplementText
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }
    }
}

