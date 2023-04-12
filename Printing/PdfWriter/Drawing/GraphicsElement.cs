namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Text;

    public abstract class GraphicsElement
    {
        internal string _A154;

        internal GraphicsElement()
        {
        }

        internal abstract void A119(ref A120 b0, ref A112 b1);
        internal static void A156(float b3, float b4, float b5, float b6, Border border, ref A120 info, ref A112 cs)
        {
            if (((border != null) && (border.LineColor != null)) && (border.LineColor != PdfColor.Transparent))
            {
                A177(border.LineWidth, border.LineColor, border.LineStyle, ref info, ref cs);
                A437(cs, b3, info.A98(b4), b5, b6);
                A180(cs, GraphicsMode.stroke, false);
            }
        }

        internal static PdfFont A157(PdfFont b17, float b18, ref A120 b0, ref A112 b1)
        {
            bool flag = false;
            A529 A = b0.A530;
            PdfFont font = b0.A167(b17);
            if (!PdfFont.A198(A.A533, font))
            {
                A.A533 = font;
                flag = true;
            }
            if (A.A172 != b18)
            {
                A.A172 = b18;
                flag = true;
            }
            if (flag)
            {
                b1.A176(string.Format("/{0} {1} Tf ", font.A168, A15.A18(b18)));
            }
            return font;
        }

        internal static PdfFont A157(PdfFont b17, float b18, ref A120 b0, ref StringBuilder b37)
        {
            PdfFont font = b0.A167(b17);
            if ((font != null) && (b18 > 0f))
            {
                b37.Append(string.Format("/{0} {1} Tf ", font.A168, A15.A18(b18)));
            }
            return font;
        }

        internal static void A160(PdfColor b19, ref A120 b0, ref A112 b1)
        {
            A529 A = b0.A530;
            if (b19 == null)
            {
                b19 = PdfColor.Transparent;
            }
            if (A.A534 != b19)
            {
                A.A534 = b19;
                b19.A468(ref b1, false);
            }
        }

        internal static string A161(string b25)
        {
            char[] chArray = new char[b25.Length];
            int index = 0;
            for (int i = b25.Length - 1; i > -1; i--)
            {
                chArray[index] = b25[i];
                index++;
            }
            return new string(chArray);
        }

        internal static void A162(A55 b2, float b3, float b4, float b14, string b15, bool b16)
        {
            b2.A176("BT");
            b2.A176(string.Format("{0} {1} Td", A15.A18(b3), A15.A18(b4)));
            b2.A176(string.Format("{0} Tj", b15));
            b2.A176("ET ");
            if (b16)
            {
                A532(b2, b3, b4 - 1.4f, b3 + b14, b4 - 1.4f);
            }
        }

        internal static void A170(float b30, float b31, ref A112 b1)
        {
            if ((b30 != 0f) || (b31 != 0f))
            {
                b1.A176(string.Format("1 0 0 1 {0} {1} cm", A15.A18(b30), A15.A18(b31)));
            }
        }

        internal static void A175(float b3, float b4, float b5, float b6, PdfColor b19, ref A120 b0, ref A112 b1)
        {
            if ((b19 != null) && (b19 != PdfColor.Transparent))
            {
                A160(b19, ref b0, ref b1);
                A437(b1, b3, b0.A98(b4), b5, b6);
                A180(b1, GraphicsMode.fill, false);
            }
        }

        internal static void A177(float b20, PdfColor b21, LineStyle b22, ref A120 b0, ref A112 b1)
        {
            A529 A = b0.A530;
            if (A.A535 != b22)
            {
                A.A535 = b22;
                if (b22 == LineStyle.Solid)
                {
                    b1.A176("[] 0 d");
                }
                else if (b22 == LineStyle.Dash)
                {
                    b1.A176("[6 2] 0 d");
                }
                else if (b22 == LineStyle.Dot)
                {
                    b1.A176("[2 2] 0 d");
                }
            }
            if (A.A536 != b20)
            {
                if (b20 <= 0f)
                {
                    b20 = 1f;
                }
                A.A536 = b20;
                b1.A176(string.Format("{0} w", A15.A18(b20)));
            }
            if (b21 == null)
            {
                b21 = PdfColor.Transparent;
            }
            if (A.A537 != b21)
            {
                A.A537 = b21;
                b21.A468(ref b1, true);
            }
        }

        internal static void A178(A55 b2, float b3, float b4)
        {
            b2.A176(string.Format("{0} {1} m", A15.A18(b3), A15.A18(b4)));
        }

        internal static void A179(A55 b2, float b3, float b4)
        {
            b2.A176(A15.A23(b3, b4) + " l");
        }

        internal static void A180(A55 b2, GraphicsMode b23, bool b24)
        {
            bool flag = false;
            bool flag2 = false;
            if ((b23 & GraphicsMode.stroke) == GraphicsMode.stroke)
            {
                flag2 = true;
            }
            if ((b23 & GraphicsMode.fill) == GraphicsMode.fill)
            {
                flag = true;
            }
            if ((b23 & GraphicsMode.alternate) == GraphicsMode.alternate)
            {
                if ((b23 & GraphicsMode.clip) == GraphicsMode.clip)
                {
                    b2.A176("W*");
                }
                if (flag && flag2)
                {
                    if (!b24)
                    {
                        b2.A176("B*");
                    }
                    else
                    {
                        b2.A176("b*");
                    }
                }
                else if (!flag && flag2)
                {
                    if (!b24)
                    {
                        b2.A176("S");
                    }
                    else
                    {
                        b2.A176("s");
                    }
                }
                else if (flag && !flag2)
                {
                    b2.A176("f*");
                }
                else if (!flag && !flag2)
                {
                    b2.A176("n*");
                }
            }
            else
            {
                if ((b23 & GraphicsMode.clip) == GraphicsMode.clip)
                {
                    b2.A176("W");
                }
                if (flag && flag2)
                {
                    if (!b24)
                    {
                        b2.A176("B");
                    }
                    else
                    {
                        b2.A176("b");
                    }
                }
                else if (!flag && flag2)
                {
                    if (!b24)
                    {
                        b2.A176("S");
                    }
                    else
                    {
                        b2.A176("s");
                    }
                }
                else if (flag && !flag2)
                {
                    b2.A176("f");
                }
                else if (!flag && !flag2)
                {
                    b2.A176("n");
                }
            }
        }

        internal static void A183(ref A112 b1, float b3, float b4, float b26, float b27, GraphicsMode b23)
        {
            A178(b1, b3, b4 - b27);
            A472(b1, b3 + (b26 * 1.3333f), b4 - b27, b3 + (b26 * 1.3333f), b4 + b27, b3, b4 + b27);
            A472(b1, b3 - (b26 * 1.3333f), b4 + b27, b3 - (b26 * 1.3333f), b4 - b27, b3, b4 - b27);
            A180(b1, b23, true);
        }

        internal static void A435(ref A120 b0, ref A112 b1)
        {
            b1.A176("q ");
            b0.A435();
        }

        internal static void A436(float b3, float b4, float b30, float b31, float b32, ref A120 b0, ref A112 b1)
        {
            if (b32 > 0f)
            {
                b32 = A15.A19(b32);
                float num = (float) Math.Sin((double) b32);
                float num2 = (float) Math.Cos((double) b32);
                if ((b3 != 0f) || (b4 != 0f))
                {
                    float num3 = 0f;
                    float num4 = 0f;
                    float num5 = 0f;
                    float num6 = 0f;
                    if (b4 != 0f)
                    {
                        num3 = b0.A98(b4);
                    }
                    float num7 = (float) Math.Sqrt((double) ((b3 * b3) + (num3 * num3)));
                    if (b3 == 0f)
                    {
                        num4 = A15.A19(90f);
                    }
                    else if (b4 == 0f)
                    {
                        num4 = 0f;
                    }
                    else
                    {
                        num4 = (float) Math.Atan2((double) num3, (double) b3);
                    }
                    float num8 = b32 + num4;
                    if (b3 != 0f)
                    {
                        num5 = b3 - (((float) Math.Cos((double) num8)) * num7);
                    }
                    if (b4 != 0f)
                    {
                        num6 = num3 - (((float) Math.Sin((double) num8)) * num7);
                    }
                    A170(num5, num6, ref b1);
                }
                A170(b30, b31, ref b1);
                b1.A176(string.Format("{0} {1} {2} {0} 0 0 cm", A15.A18(num2), A15.A18(num), A15.A18(-num)));
            }
        }

        internal static void A437(A55 b2, RectangleF b13)
        {
            b2.A176(A15.A22(b13) + " re");
        }

        internal static void A437(A55 b2, float b3, float b4, float b5, float b6)
        {
            b2.A176(A15.A22(b3, b4, b5, b6) + " re");
        }

        internal static void A438(ref A120 b0, ref A112 b1)
        {
            if (b0.A522.Count > 0)
            {
                b1.A176("Q ");
                b0.A438();
            }
        }

        internal static void A446(A55 b2, float b3, float b4, float b5, float b6)
        {
            b2.A176(A15.A22(b3, b4, b5, b6) + " re W n");
        }

        internal static void A472(A55 b2, float b7, float b8, float b9, float b10, float b11, float b12)
        {
            b2.A176(string.Format("{0} {1} {2} {3} {4} {5} c", new object[] { A15.A18(b7), A15.A18(b8), A15.A18(b9), A15.A18(b10), A15.A18(b11), A15.A18(b12) }));
        }

        internal static void A512(float b3, float b4, float b33, float b34, ref A120 b0, ref A112 b1)
        {
            if ((b33 != 1f) || (b34 != 1f))
            {
                float num = 0f;
                float num2 = 0f;
                if (b3 != 0f)
                {
                    num = b3 * (b33 - 1f);
                }
                if (b4 != 0f)
                {
                    num2 = b0.A98(b4) * (b34 - 1f);
                }
                A170(-num, -num2, ref b1);
                b1.A176(string.Format("{0} 0 0 {1} 0 0 cm", A15.A18(b33), A15.A18(b34)));
            }
        }

        internal static void A516(float b3, float b4, float b35, float b36, ref A120 b0, ref A112 b1)
        {
            if ((b35 > 0f) || (b36 > 0f))
            {
                float num = (float) Math.Tan((double) A15.A19(b35));
                float num2 = (float) Math.Tan((double) A15.A19(b36));
                float num3 = 0f;
                float num4 = 0f;
                if (b3 != 0f)
                {
                    num3 = num2 * b0.A98(b4);
                }
                if (b4 != 0f)
                {
                    num4 = num * b3;
                }
                A170(-num3, -num4, ref b1);
                b1.A176(string.Format("1 {0} {1} 1 0 0 cm", A15.A18(num), A15.A18(num2)));
            }
        }

        internal static void A521(ref A120 b0, ref A112 b1, Border b28, RectangleF b29)
        {
            if (((b28 != null) && (b28.LineColor != null)) && (b28.LineColor != PdfColor.Transparent))
            {
                A177(b28.LineWidth, b28.LineColor, b28.LineStyle, ref b0, ref b1);
                A437(b1, b29.X, b29.Y, b29.Width, b29.Height);
                A180(b1, GraphicsMode.stroke, false);
            }
        }

        internal static void A532(A55 b2, float b7, float b8, float b9, float b10)
        {
            b2.A176(A15.A23(b7, b8) + " m");
            b2.A176(A15.A23(b9, b10) + " l");
            b2.A176("S");
        }

        internal static void A538(PdfColor b38)
        {
        }

        public string Name
        {
            get
            {
                return this._A154;
            }
        }
    }
}

