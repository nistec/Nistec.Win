namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class A500
    {
        private float _b0;
        private float _b1;
        private float _b10;
        private A568 _b11;
        private A478 _b12;
        private StyleBase _b13;
        private float _b2;
        private float _b3;
        private TextAlignment _b4;
        private bool _b5;
        private A497 _b6;
        private bool _b7;
        private bool _b8;
        private float _b9;

        internal A500(A497 b6, float b0, float b1, float b2, float b3, TextAlignment b4, bool b5)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
            this._b4 = b4;
            this._b5 = b5;
            this._b6 = b6;
            this._b7 = false;
            this._b8 = true;
            this._b10 = 0f;
            this._b9 = 0f;
            this._b11 = new A568();
            this._b12 = new A478();
        }

        internal static void A119(ref A120 b14, ref A112 b23, A478 b12, int b24, int b25, float b26, float b27, float b28, float b29, bool b5)
        {
            A561 A = null;
            A557 A2 = null;
            A559 A3 = null;
            b26 += b29;
            b27 += b28;
            for (int i = b24; i < b25; i++)
            {
                A526 A4 = b12[i];
                if (A4.A506)
                {
                    A595(A4, out A, out A2, out A3);
                }
                else
                {
                    A = null;
                    A2 = null;
                    A3 = null;
                }
                A566 A5 = null;
                A523 A6 = null;
                StyleBase base2 = null;
                PdfFont font = null;
                float num2 = b26 + A4.A569;
                float num3 = num2;
                b27 += A4.A527;
                float num4 = b27 + A4.A572;
                if (A2 != null)
                {
                    A558 A7;
                    if (!b5)
                    {
                        for (int j = 0; j < A2.A2; j++)
                        {
                            A7 = A2[j];
                            GraphicsElement.A175(num2 + A7.A142, b27, A7.A211, A4.A212, A7.A567, ref b14, ref b23);
                        }
                    }
                    else
                    {
                        for (int k = A2.A2 - 1; k > -1; k--)
                        {
                            A7 = A2[k];
                            GraphicsElement.A175(b26 + (A4.A211 - ((A4.A569 + A7.A142) + A7.A211)), b27, A7.A211, A4.A212, A7.A567, ref b14, ref b23);
                        }
                    }
                }
                if (!b5)
                {
                    for (int m = 0; m < A4.A2; m++)
                    {
                        A5 = A4[m];
                        A6 = A5.A580;
                        if (A6.A405 != base2)
                        {
                            base2 = A6.A405;
                            font = GraphicsElement.A157(base2.Font, base2.FontSize, ref b14, ref b23);
                            base2.A157(font);
                            GraphicsElement.A160(base2.TextColor, ref b14, ref b23);
                        }
                        if (!A5.A575)
                        {
                            A443(num3, num4, A578.A591(A5), base2, ref b14, ref b23, false);
                        }
                        num3 += A5.A211;
                    }
                }
                else
                {
                    for (int n = A4.A2 - 1; n > -1; n--)
                    {
                        A5 = A4[n];
                        A6 = A5.A580;
                        if (A6.A405 != base2)
                        {
                            base2 = A6.A405;
                            font = GraphicsElement.A157(base2.Font, base2.FontSize, ref b14, ref b23);
                            base2.A157(font);
                            GraphicsElement.A160(base2.TextColor, ref b14, ref b23);
                        }
                        if (!A5.A575)
                        {
                            A443(num3, num4, A578.A591(A5), base2, ref b14, ref b23, true);
                        }
                        num3 += A5.A211;
                    }
                }
                if (A != null)
                {
                    A562 A8;
                    float num9;
                    float num10 = num4 + 2f;
                    float num11 = 1f;
                    if (!b5)
                    {
                        for (int num12 = 0; num12 < A.A2; num12++)
                        {
                            A8 = A[num12];
                            if (A8.A586)
                            {
                                num11 = 1.25f;
                            }
                            else
                            {
                                num11 = 1f;
                            }
                            num9 = num2 + A8.A142;
                            A596(num9, num10, num9 + A8.A211, num10, A8.A567, num11, ref b14, ref b23);
                        }
                    }
                    else
                    {
                        for (int num13 = A.A2 - 1; num13 > -1; num13--)
                        {
                            A8 = A[num13];
                            if (A8.A586)
                            {
                                num11 = 1.5f;
                            }
                            else
                            {
                                num11 = 1f;
                            }
                            num9 = b26 + (A4.A211 - ((A4.A569 + A8.A142) + A8.A211));
                            A596(num9, num10, num9 + A8.A211, num10, A8.A567, num11, ref b14, ref b23);
                        }
                    }
                }
                if (A3 != null)
                {
                    A560 A9;
                    if (!b5)
                    {
                        for (int num14 = 0; num14 < A3.A2; num14++)
                        {
                            A9 = A3[num14];
                            A597(num2 + A9.A142, b27, A9.A211, A4.A212, A9.A585, ref b14, ref b23);
                        }
                    }
                    else
                    {
                        for (int num15 = A3.A2 - 1; num15 > -1; num15--)
                        {
                            A9 = A3[num15];
                            A597(b26 + (A4.A211 - ((A4.A569 + A9.A142) + A9.A211)), b27, A9.A211, A4.A212, A9.A585, ref b14, ref b23);
                        }
                    }
                }
                b27 += A4.A212 + A4.A528;
            }
        }

        internal void A39(A523 b14)
        {
            TextAlignment alignment = this._b4;
            float num = this._b2;
            this._b4 = b14.A570;
            this._b2 = b14.A593;
            this.A594(this._b11, false);
            this._b4 = alignment;
            this._b2 = num;
        }

        internal void A40(A523 b14)
        {
            this._b4 = b14.A570;
            this._b3 = b14.A593;
            this._b7 = true;
            this.A594(this._b11, true);
        }

        internal static void A443(float b26, float b27, string b30, StyleBase b31, ref A120 b14, ref A112 b23, bool b5)
        {
            PdfFont font = b31.Font;
            font.A159(b30);
            string str = b30;
            if (b5)
            {
                str = GraphicsElement.A161(b30);
            }
            GraphicsElement.A162(b23, b26, b14.A98(b27), 0f, font.A163(str), false);
        }

        internal void A501(float b9, StyleBase b13, bool b16)
        {
            A523 A = null;
            this._b9 = b9;
            this._b8 = b16;
            this._b10 = 0f;
            this._b7 = true;
            this._b12.A4();
            this._b13 = b13;
            this.b15();
            for (int i = 0; i < this._b6.A2; i++)
            {
                A = this._b6[i];
                if (A.A587 == A37.A38)
                {
                    this.b17(A);
                }
                else if (A.A587 == A37.A40)
                {
                    this.A40(A);
                }
                else if (A.A587 == A37.A39)
                {
                    this.A39(A);
                }
            }
            this.A594(this._b11, true);
        }

        internal static float A503(A478 b12, int b24, int b25, float b28, float b45)
        {
            float num = b28 + b45;
            A526 A = null;
            for (int i = b24; i < b25; i++)
            {
                A = b12[i];
                num += (A.A527 + A.A212) + A.A528;
            }
            if (A != null)
            {
                num -= A.A528;
            }
            return num;
        }

        internal void A594(A568 b11, bool b22)
        {
            if (!this._b8)
            {
                if (b11.A2 > 0)
                {
                    A568.A552(b11, b22, this._b5);
                }
                if (b11.A527 != 0f)
                {
                    this._b12.A3(new A590(b11));
                }
                else
                {
                    this._b12.A3(new A526(b11));
                }
            }
            this._b10 += (b11.A527 + b11.A212) + b11.A528;
            this.b15();
        }

        internal static void A595(A526 b41, out A561 b42, out A557 b43, out A559 b44)
        {
            b42 = new A561();
            b43 = new A557();
            b44 = new A559();
            float num = 0f;
            float num2 = 0f;
            StyleBase base2 = null;
            float num3 = 0f;
            float num4 = 0f;
            string str = string.Empty;
            A566 A = null;
            A523 A2 = null;
            for (int i = 0; i < b41.A2; i++)
            {
                A = b41[i];
                A2 = A.A580;
                if (str != A2.A585)
                {
                    if ((str != string.Empty) || (str != null))
                    {
                        b44.A3(new A560(num3, num4, str));
                        num3 += num4;
                    }
                    str = A2.A585;
                    num4 = A.A211;
                }
                else
                {
                    switch (str)
                    {
                        case ""://string.Empty:
                        case null:
                            num3 += A.A211;
                            num4 = 0f;
                            goto Label_00D5;
                    }
                    num4 += A.A211;
                }
            Label_00D5:
                if (base2 != A2.A405)
                {
                    if (base2 == null)
                    {
                        base2 = A2.A405;
                        num2 += A.A211;
                    }
                    else
                    {
                        if (base2.Highlight != PdfColor.Transparent)
                        {
                            b43.A3(new A558(num, num2, base2.Highlight));
                        }
                        bool flag = false;
                        if ((base2.Font.A405 & FontStyle.Bold) == FontStyle.Bold)
                        {
                            flag = true;
                        }
                        if (base2.Underline)
                        {
                            b42.A3(new A562(num, num2, base2.TextColor, flag));
                        }
                        base2 = A2.A405;
                        num += num2;
                        num2 = A.A211;
                    }
                }
                else
                {
                    num2 += A.A211;
                }
            }
            if (A != null)
            {
                if ((str != string.Empty) && (str != null))
                {
                    b44.A3(new A560(num3, num4, str));
                }
                if (base2.Highlight != PdfColor.Transparent)
                {
                    b43.A3(new A558(num, num2, base2.Highlight));
                }
                if (base2.Underline)
                {
                    bool flag2 = false;
                    if ((base2.Font.A405 & FontStyle.Bold) == FontStyle.Bold)
                    {
                        flag2 = true;
                    }
                    b42.A3(new A562(num, num2, base2.TextColor, flag2));
                }
            }
        }

        internal static void A596(float b32, float b33, float b34, float b35, PdfColor b36, float b37, ref A120 b14, ref A112 b23)
        {
            b33 = b14.A98(b33);
            b35 = b14.A98(b35);
            GraphicsElement.A177(b37, b36, LineStyle.Solid, ref b14, ref b23);
            GraphicsElement.A178(b23, b32, b33);
            GraphicsElement.A179(b23, b34, b35);
            GraphicsElement.A180(b23, GraphicsMode.stroke, false);
        }

        internal static void A597(float b26, float b27, float b38, float b39, string b40, ref A120 b14, ref A112 b23)
        {
            if (b38 <= 0f)
            {
                b38 = b14.A211;
            }
            if (b39 <= 0f)
            {
                b39 = b14.A212;
            }
            A115 A = new A115(b14.A97, b40);
            b14.A121(new A124(b14.A97, new RectangleF(b26, b14.A98(b27), b38, b39), PdfColor.Black, 0, A, HighlightMode.None));
        }

        private void b15()
        {
            float num;
            float num2 = 0f;
            float num3 = 0f;
            bool flag = false;
            if (this._b7)
            {
                num3 = this._b9 - this._b0;
                num = this._b0 + this._b1;
                this._b7 = false;
                flag = true;
                num2 = this._b3;
            }
            else
            {
                num3 = this._b9;
                num = this._b1;
            }
            this._b11.A237(num, num3, this._b4, this._b2, num2, flag);
        }

        private void b17(A523 b18)
        {
            A564 A = null;
            string str = b18.A38;
            StyleBase base2 = b18.A405;
            string text1 = b18.A585;
            if (str != null)
            {
                if (base2 == null)
                {
                    base2 = this._b13;
                }
                for (int i = 0; i < str.Length; i++)
                {
                    char ch = str[i];
                    switch (ch)
                    {
                        case ' ':
                            if (A == null)
                            {
                                A = new A564(A44.A45, b18);
                                A.A589(i, ch);
                            }
                            else if (A.A575)
                            {
                                A.A589(i, ch);
                            }
                            else
                            {
                                this.b19(A);
                                A = new A564(A44.A45, b18);
                                A.A589(i, ch);
                            }
                            break;

                        case '\t':
                            if (A == null)
                            {
                                A = new A564(A44.A46, b18);
                                A.A589(i, ch);
                            }
                            else if (A.A587 == A44.A46)
                            {
                                A.A589(i, ch);
                            }
                            else
                            {
                                this.b19(A);
                                A = new A564(A44.A46, b18);
                                A.A589(i, ch);
                            }
                            break;

                        case '\r':
                            if (((i + 1) < str.Length) && (str[i + 1] == '\n'))
                            {
                                i++;
                            }
                            if (A != null)
                            {
                                this.b19(A);
                                A = null;
                            }
                            if (this._b11.A2 == 0)
                            {
                                this._b11.SetDefault(base2);
                            }
                            this.A594(this._b11, false);
                            break;

                        case '\n':
                            if (A != null)
                            {
                                this.b19(A);
                                A = null;
                            }
                            if (this._b11.A2 == 0)
                            {
                                this._b11.SetDefault(base2);
                            }
                            this.A594(this._b11, false);
                            break;

                        default:
                            if (A == null)
                            {
                                A = new A564(A44.A38, b18);
                                A.A589(i, ch);
                            }
                            else if (A.A587 == A44.A38)
                            {
                                A.A589(i, ch);
                            }
                            else
                            {
                                this.b19(A);
                                A = new A564(A44.A38, b18);
                                A.A589(i, ch);
                            }
                            break;
                    }
                }
                if (A != null)
                {
                    this.b19(A);
                }
            }
        }

        private void b19(A564 b20)
        {
            float num = b20.A211 + this._b11.A573;
            if (num < this._b11.A211)
            {
                this._b11.A581(b20);
            }
            else if (num == this._b11.A211)
            {
                this._b11.A581(b20);
                this.A594(this._b11, false);
            }
            else if (num > this._b11.A211)
            {
                if (b20.A587 == A44.A38)
                {
                    if ((b20.A211 > this._b11.A211) | b20.A588)
                    {
                        this.b21(b20);
                    }
                    else
                    {
                        this.A594(this._b11, false);
                        this._b11.A581(b20);
                    }
                }
                else
                {
                    this.A594(this._b11, false);
                }
            }
        }

        private void b21(A564 b20)
        {
            float num = 0f;
            A556[] AArray = null;
            A556[] AArray2 = null;
            float num2 = 0f;
            float num3 = 0f;
            A555 A2 = b20.A577;
            float num4 = this._b11.A211 - this._b11.A573;
            if (num4 > 0f)
            {
                for (int i = 0; i < A2.A2; i++)
                {
                    num += A2[i].A211;
                    if (num >= num4)
                    {
                        A556 A;
                        if (num == num4)
                        {
                            if ((i + 1) < A2.A2)
                            {
                                AArray = new A556[i + 1];
                                AArray2 = new A556[(A2.A2 - 1) - i];
                                num2 = 0f;
                                num3 = 0f;
                                for (int j = 0; j < (i + 1); j++)
                                {
                                    A = A2[j];
                                    AArray[j] = A;
                                    num2 += A.A211;
                                }
                                int index = 0;
                                for (int k = i + 1; k < A2.A2; k++)
                                {
                                    A = A2[k];
                                    AArray2[index] = A;
                                    num3 += A.A211;
                                    index++;
                                }
                                this._b11.A581(new A564(b20.A587, b20.A523, AArray, num2));
                                this.A594(this._b11, false);
                                this.b19(new A564(b20.A587, b20.A523, AArray2, num3));
                                b20 = null;
                                return;
                            }
                            this._b11.A581(b20);
                            this.A594(this._b11, false);
                            return;
                        }
                        if (num > num4)
                        {
                            if ((i - 1) > -1)
                            {
                                AArray = new A556[i];
                                AArray2 = new A556[A2.A2 - i];
                                num2 = 0f;
                                num3 = 0f;
                                for (int m = 0; m < i; m++)
                                {
                                    A = A2[m];
                                    AArray[m] = A;
                                    num2 += A.A211;
                                }
                                int num10 = 0;
                                for (int n = i; n < A2.A2; n++)
                                {
                                    A = A2[n];
                                    AArray2[num10] = A;
                                    num3 += A.A211;
                                    num10++;
                                }
                                this._b11.A581(new A564(b20.A587, b20.A523, AArray, num2));
                                this.A594(this._b11, false);
                                this.b19(new A564(b20.A587, b20.A523, AArray2, num3));
                                b20 = null;
                                return;
                            }
                            if (this._b11.A2 == 0)
                            {
                                this._b11.A581(b20);
                                this.A594(this._b11, false);
                                return;
                            }
                            this.A594(this._b11, false);
                            this.b19(b20);
                            return;
                        }
                    }
                }
            }
            else
            {
               this.A594(this._b11, false);
               this.b19(b20);
            }
        }

        internal A478 A479
        {
            get
            {
                return this._b12;
            }
        }

        internal float A502
        {
            get
            {
                return this._b10;
            }
        }
    }
}

