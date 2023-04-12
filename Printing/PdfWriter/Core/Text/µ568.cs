namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using System;
    using System.Reflection;

    internal class A568
    {
        private float _b0;
        private TextAlignment _b1;
        private float _b10;
        private int _b11;
        private bool _b12;
        private bool _b13;
        private float _b2;
        private float _b3;
        private float _b4;
        private float _b5;
        private float _b6;
        private MControl.Printing.Pdf.Core.Text.A565 _b7 = new MControl.Printing.Pdf.Core.Text.A565();
        private A563 _b8 = new A563();
        private float _b9;

        internal A568()
        {
        }

        internal void A237(float b0, float b9, TextAlignment b1, float b2, float b3, bool b13)
        {
            this._b1 = b1;
            this._b4 = 0f;
            this._b2 = b2;
            this._b5 = 0f;
            this._b0 = b0;
            this._b6 = 0f;
            this._b9 = b9;
            this._b11 = 0;
            this._b3 = b3;
            this._b13 = b13;
            this._b8.A4();
            this._b7.A4();
            this._b10 = 0f;
            this._b12 = false;
        }

        internal static void A552(A568 b15, bool b16, bool b17)
        {
            A564 A;
            A563 A2 = b15._b8;
            if (b15.A570 == TextAlignment.Left)
            {
                if (!b17)
                {
                    if (!b15.A571)
                    {
                        b18(b15);
                    }
                }
                else
                {
                    b19(b15);
                }
            }
            else if (b15.A570 == TextAlignment.Justify)
            {
                if (!b16)
                {
                    if (!b15.A571)
                    {
                        b18(b15);
                    }
                    b19(b15);
                    float num = (b15.A211 - b15.A573) / b15.A574;
                    for (int j = 0; j < A2.A2; j++)
                    {
                        A = A2[j];
                        if (A.A575)
                        {
                            A.A576 = num;
                            b15._b10 += num;
                            b15._b7.A3(new A45(A.A523, A.A211));
                        }
                        else
                        {
                            A555 A3 = A.A577;
                            A578 A4 = new A578(A3[0].A579, A.A523);
                            A4.A211 = A.A211;
                            A4.A293 = (A3[A3.A2 - 1].A579 - A3[0].A579) + 1;
                            b15._b7.A3(A4);
                        }
                    }
                    return;
                }
                if (!b17)
                {
                    if (!b15.A571)
                    {
                        b18(b15);
                    }
                }
                else
                {
                    b19(b15);
                    b15.A569 += b15.A211 - b15.A573;
                }
            }
            else if (b15.A570 == TextAlignment.Right)
            {
                if (!b17)
                {
                    b19(b15);
                }
                else if (!b15.A571)
                {
                    b18(b15);
                }
                b15._b0 += b15.A211 - b15.A573;
            }
            else if (b15.A570 == TextAlignment.Center)
            {
                if (!b15.A571)
                {
                    b18(b15);
                }
                b19(b15);
                b15._b0 += (b15.A211 - b15.A573) / 2f;
            }
            MControl.Printing.Pdf.Core.Text.A565 A5 = b15._b7;
            for (int i = 0; i < A2.A2; i++)
            {
                A = A2[i];
                if (A5.A2 > 0)
                {
                    A566 A6 = A5[A5.A2 - 1];
                    if (A6.A580 == A.A523)
                    {
                        A555 A7 = A.A577;
                        A6.A211 += A.A211;
                        A6.A293 += (A7[A7.A2 - 1].A579 - A7[0].A579) + 1;
                    }
                    else
                    {
                        A555 A8 = A.A577;
                        A578 A9 = new A578(A8[0].A579, A.A523);
                        A9.A211 = A.A211;
                        A9.A293 = (A8[A8.A2 - 1].A579 - A9.A579) + 1;
                        b15._b7.A3(A9);
                    }
                }
                else
                {
                    A555 A10 = A.A577;
                    A578 A11 = new A578(A10[0].A579, A.A523);
                    A11.A211 = A.A211;
                    A11.A293 = (A10[A10.A2 - 1].A579 - A11.A579) + 1;
                    b15._b7.A3(A11);
                }
            }
        }

        internal void A581(A564 b20)
        {
            if ((b20 != null) && !b20.A582)
            {
                if (b20.A575)
                {
                    this._b11++;
                }
                this._b10 += b20.A211;
                if (this._b4 < b20.A408)
                {
                    this._b4 = b20.A408;
                }
                if (this._b5 < b20.A409)
                {
                    this._b5 = b20.A409;
                }
                if (this._b6 < b20.A410)
                {
                    this._b6 = b20.A410;
                }
                if (!this._b12 && b20.A506)
                {
                    this._b12 = true;
                }
                this._b8.A3(b20);
            }
        }

        internal void SetDefault(StyleBase b21)
        {
            PdfFont font = b21.Font;
            float fontSize = b21.FontSize;
            this._b4 = font.A158(fontSize);
            this._b5 = font.A583(fontSize);
            this._b6 = font.A584(fontSize);
        }

        private static void b18(A568 b15)
        {
            if ((b15._b8.A2 > 0) && b15._b8[0].A575)
            {
                b15._b11--;
                b15._b10 -= b15._b8[0].A211;
                b15._b8.A9(0);
            }
        }

        private static void b19(A568 b15)
        {
            int num = b15._b8.A2;
            if ((num > 0) && b15._b8[num - 1].A575)
            {
                int num2 = num - 1;
                b15._b11--;
                b15._b10 -= b15._b8[num2].A211;
                b15._b8.A9(num2);
            }
        }

        internal int A2
        {
            get
            {
                return this._b8.A2;
            }
        }

        internal float A211
        {
            get
            {
                return this._b9;
            }
        }

        internal float A212
        {
            get
            {
                return ((this._b4 + this._b5) + this._b6);
            }
        }

        internal float A408
        {
            get
            {
                return this._b4;
            }
        }

        internal float A409
        {
            get
            {
                return this._b5;
            }
        }

        internal bool A506
        {
            get
            {
                return this._b12;
            }
        }

        internal float A527
        {
            get
            {
                return this._b3;
            }
        }

        internal float A528
        {
            get
            {
                return this._b2;
            }
        }

        internal MControl.Printing.Pdf.Core.Text.A565 A565
        {
            get
            {
                return this._b7;
            }
        }

        internal float A569
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

        internal TextAlignment A570
        {
            get
            {
                return this._b1;
            }
        }

        internal bool A571
        {
            get
            {
                return this._b13;
            }
        }

        internal float A572
        {
            get
            {
                return this._b4;
            }
        }

        internal float A573
        {
            get
            {
                return this._b10;
            }
        }

        internal float A574
        {
            get
            {
                return (float) this._b11;
            }
        }

        internal A566 this[int b14]
        {
            get
            {
                return this._b7[b14];
            }
        }
    }
}

