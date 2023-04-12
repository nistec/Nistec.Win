namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class UPCEGraphics : UPCAGraphics
    {
        private int _b0;

        internal override void A430(ref A120 b6, ref A112 b7, float b8)
        {
            int num = 0;
            float textWidth = 0f;
            float num3 = 0f;
            float num4 = (int) (base.X + base._A413);
            float num5 = (int) b6.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b6, ref b7);
            if (base.CodeTextVisible)
            {
                textWidth = base.CodeTextFont.GetTextWidth(this._b0.ToString(), base.CodeTextFontSize);
                num3 = base.CodeTextFont.GetTextWidth(base.A455.ToString(), base.CodeTextFontSize);
                num4 += (int) textWidth;
            }
            string str = this.b5();
            num4 += base.A464(ref b6, ref b7, base.A453('*', false), num4, num5, ref num, true);
            int num6 = base.A450.Length - 1;
            for (int i = 1; i < num6; i++)
            {
                bool flag;
                char c = base.A450[i];
                if (str[i - 1] == 'e')
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                num4 += base.A464(ref b6, ref b7, base.A453(c, flag), num4, num5, ref num, false);
            }
            num4 += base.A464(ref b6, ref b7, base.A453('>', false), num4, num5, ref num, true);
            float num8 = (b8 - base._A413) - base._A415;
            float num9 = num5 - base._A422;
            if (base.CodeTextVisible)
            {
                float num13 = base.CodeTextSpace / 2f;
                string str2 = this._b0.ToString();
                float num10 = (int) (base.X + base._A413);
                float num11 = (num5 - base._A422) - base.CodeTextSpace;
                float num12 = textWidth;
                BarcodeGraphics.A443(ref b6, ref b7, str2, num10, num11, num12, num13, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = base.A450.Substring(1, 6);
                num10 += textWidth + (3f * base._A423);
                num12 = 42f * base._A423;
                BarcodeGraphics.A443(ref b6, ref b7, str2, num10, num11, num12, num13, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = base.A455;
                num10 += 48f * base._A423;
                num12 = base.CodeTextFont.GetTextWidth(str2, base.CodeTextFontSize);
                BarcodeGraphics.A443(ref b6, ref b7, str2, num10, num11, num12, num13, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                num9 -= (base.CodeTextSpace + base.CodeTextFont.Height(base.CodeTextFontSize)) + base.CaptionSpace;
            }
            else
            {
                num9 -= base.CodeTextSpace + (base.CodeTextFont.Height(base.CodeTextFontSize) / 2f);
            }
            num4 += (int) num3;
            base.A465(ref b6, ref b7, num4, num5);
            base.A442(ref b6, ref b7, num9, num8);
        }

        internal override void A432()
        {
            string str;
            int length = base._A416.Length;
            this._b0 = BarcodeGraphics.A439(base._A416[0]);
            switch (length)
            {
                case 11:
                case 12:
                    str = base._A416.Substring(0, 11);
                    break;

                default:
                    str = this.b1(base._A416.Substring(0, 7));
                    break;
            }
            base.A455 = Industrial2of5Graphics.A448(str).ToString();
            base.A450 = this.b2(str);
            base.A450 = '*' + base.A450 + '>';
        }

        internal override int A457()
        {
            return 6;
        }

        internal override int A458()
        {
            return 6;
        }

        internal override int A459()
        {
            return 0;
        }

        internal override int A460()
        {
            return 9;
        }

        public override bool IsValidData(string data)
        {
            if ((data != null) && (data.Length > 0))
            {
                switch (data[0])
                {
                    case '0':
                    case '1':
                        if ((data.Length == 7) || (data.Length == 8))
                        {
                            if (base.A447(data))
                            {
                                return true;
                            }
                        }
                        else if (b3(data) && base.A447(data))
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        private string b1(string b4)
        {
            if ((b4 == null) || (b4.Length != 7))
            {
                return "";
            }
            string str = string.Empty;
            string str2 = string.Empty;
            int num = BarcodeGraphics.A439(b4[b4.Length - 1]);
            switch (num)
            {
                case 0:
                case 1:
                case 2:
                    str = b4.Substring(1, 2) + num + "00";
                    str2 = "00" + b4.Substring(3, 3);
                    return (b4[0] + str + str2);

                case 3:
                    str = b4.Substring(1, 3) + "00";
                    str2 = "000" + b4.Substring(4, 2);
                    return (b4[0] + str + str2);

                case 4:
                    str = b4.Substring(1, 4) + "0";
                    str2 = "0000" + b4[6];
                    return (b4[0] + str + str2);
            }
            str = b4.Substring(1, 5);
            str2 = "0000" + b4[6];
            return (b4[0] + str + str2);
        }

        private string b2(string b4)
        {
            if (b4.Length == 11)
            {
                string str = b4.Substring(1, 5);
                string str2 = b4.Substring(6, 5);
                if (str.EndsWith("00"))
                {
                    char ch = str[2];
                    if ((((ch == '0') || (ch == '1')) || (ch == '2')) && str2.StartsWith("00"))
                    {
                        return (str.Substring(0, 2) + str2.Substring(2, 3) + ch);
                    }
                    if (str2.StartsWith("000"))
                    {
                        return (str.Substring(0, 3) + str2.Substring(3, 2) + '3');
                    }
                }
                int num = BarcodeGraphics.A439(str2[4]);
                if (str2.StartsWith("0000"))
                {
                    if (str[4] == '0')
                    {
                        return (str.Substring(0, 4) + str2[4] + '4');
                    }
                    if (num > 4)
                    {
                        return (str + str2[4]);
                    }
                }
            }
            return "";
        }

        private static bool b3(string b4)
        {
            if ((b4.Length == 11) || (b4.Length == 12))
            {
                string str = b4.Substring(3, 3);
                string str2 = b4.Substring(6, 2);
                if ((((str == "000") || (str == "100")) || (str == "200")) && (str2 == "00"))
                {
                    return true;
                }
                str = b4.Substring(4, 2);
                str2 = b4.Substring(6, 3);
                if ((str == "00") && (str2 == "000"))
                {
                    return true;
                }
                str = b4[5].ToString();
                str2 = b4.Substring(6, 4);
                if ((str == "0") && (str2 == "0000"))
                {
                    return true;
                }
            }
            return false;
        }

        private string b5()
        {
            if (this._b0 == 0)
            {
                switch (base.A455)
                {
                    case "0":
                        return "eeeooo";

                    case "1":
                        return "eeoeoo";

                    case "2":
                        return "eeooeo";

                    case "3":
                        return "eeoooe";

                    case "4":
                        return "eoeeoo";

                    case "5":
                        return "eooeeo";

                    case "6":
                        return "eoooee";

                    case "7":
                        return "eoeoeo";
                }
            }
            else if (this._b0 == 1)
            {
                switch (base.A455)
                {
                    case "0":
                        return "oooeee";

                    case "1":
                        return "ooeoee";

                    case "2":
                        return "ooeeoe";

                    case "3":
                        return "ooeeeo";

                    case "4":
                        return "oeooee";

                    case "5":
                        return "oeeooe";

                    case "6":
                        return "oeeeoo";

                    case "7":
                        return "oeoeoe";
                }
            }
            return "";
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.UPCE;
            }
        }
    }
}

