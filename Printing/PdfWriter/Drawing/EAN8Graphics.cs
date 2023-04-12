namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class EAN8Graphics : EAN13Graphics
    {
        internal override void A430(ref A120 b0, ref A112 b1, float b2)
        {
            int num = 0;
            float num2 = (int) (base.X + base._A413);
            float num3 = (int) b0.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b0, ref b1);
            num2 += base.A464(ref b0, ref b1, base.A453(base.A450[0], false), num2, num3, ref num, true);
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
                num2 += base.A464(ref b0, ref b1, base.A453(c, false), num2, num3, ref num, flag);
            }
            float num5 = (b2 - base._A413) - base._A415;
            float num6 = num3 - base._A422;
            if (base.CodeTextVisible)
            {
                float num10 = base.CodeTextSpace / 2f;
                string str = base._A416 + base.A455;
                float num7 = (int) (base.X + base._A413);
                float num8 = (num3 - base._A422) - base.CodeTextSpace;
                float num9 = 28f * base._A423;
                string str2 = str.Substring(0, 4);
                num7 += 3f * base._A423;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num7, num8, num9, num10, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                str2 = str.Substring(4, str.Length - 4);
                num7 += 33f * base._A423;
                BarcodeGraphics.A443(ref b0, ref b1, str2, num7, num8, num9, num10, base.CodeTextFont, base.CodeTextFontSize, TextAlignment.Center);
                num6 -= (base.CodeTextSpace + base.CodeTextFont.Height(base.CodeTextFontSize)) + base.CaptionSpace;
            }
            else
            {
                num6 -= base.CaptionSpace + (base.CodeTextFont.Height(base.CodeTextFontSize) / 2f);
            }
            base.A465(ref b0, ref b1, num2, num3);
            base.A442(ref b0, ref b1, num6, num5);
        }

        internal override int A457()
        {
            return 4;
        }

        internal override int A458()
        {
            return 7;
        }

        internal override float A461(string c)
        {
            return 0f;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.EAN8;
            }
        }
    }
}

