namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class UPCAGraphics : EAN13Graphics
    {
        internal override void A430(ref A120 b0, ref A112 b1, float b2)
        {
            int num = 0;
            float textWidth = 0f;
            float num3 = 0f;
            float num4 = (int) (base.X + base._A413);
            float num5 = (int) b0.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b0, ref b1);
            if (base.CodeTextVisible)
            {
                char ch2 = base._A416[0];
                textWidth = base.CodeTextFont.GetTextWidth(ch2.ToString(), base.CodeTextFontSize);
                num3 = base.CodeTextFont.GetTextWidth(base.A455.ToString(), base.CodeTextFontSize);
                num4 += (int) textWidth;
            }
            num4 += base.A464(ref b0, ref b1, base.A453(base.A450[0], false), num4, num5, ref num, true);
            for (int i = 1; i < base.A450.Length; i++)
            {
                bool flag;
                char c = base.A450[i];
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
                num4 += base.A464(ref b0, ref b1, base.A453(c, false), num4, num5, ref num, flag);
            }
            float num7 = (b2 - base._A413) - base._A415;
            float num8 = num5 - base._A422;
            if (base.CodeTextVisible)
            {
                float num12 = base.CodeTextSpace / 2f;
                string str = base._A416 + base.A455;
                string str2 = str[0].ToString();
                float num9 = (int) (base.X + base._A413);
                float num10 = (num5 - base._A422) - base.CodeTextSpace;
                float num11 = textWidth;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num9, num10, num11, num12, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = str.Substring(1, 5);
                num9 += textWidth + (3f * base._A423);
                num11 = 42f * base._A423;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num9, num10, num11, num12, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = str.Substring(6, 5);
                num9 += 47f * base._A423;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num9, num10, num11, num12, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = base.A455.ToString();
                num9 += 45f * base._A423;
                num11 = num3;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num9, num10, num11, num12, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                num8 -= (base.CodeTextSpace + base.CodeTextFont.Height(base.CodeTextFontSize)) + base.CaptionSpace;
            }
            else
            {
                num8 += base.CaptionSpace + (base.CodeTextFont.Height(base.CodeTextFontSize) / 2f);
            }
            num4 += (int) num3;
            base.A465(ref b0, ref b1, num4, num5);
            base.A442(ref b0, ref b1, num8, num7);
        }

        internal override int A457()
        {
            return 6;
        }

        internal override int A458()
        {
            return 11;
        }

        internal override float A462(string c)
        {
            return base.CodeTextFont.GetTextWidth(c, base.CodeTextFontSize);
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.UPCA;
            }
        }
    }
}

