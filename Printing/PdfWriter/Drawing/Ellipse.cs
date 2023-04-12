namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Ellipse : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private PdfColor _b4;
        private PdfColor _b5;
        private MControl.Printing.Pdf.LineStyle _b6;
        private float _b7;
        private MControl.Printing.Pdf.GraphicsMode _b8;

        public Ellipse(float x, float y, float width, float height, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b7 = linewidth;
            this._b6 = linestyle;
            this._b4 = fillcolor;
            this._b5 = linecolor;
            this._b8 = mode;
            base._A154 = name;
        }

        internal override void A119(ref A120 b9, ref A112 b10)
        {
            if ((this._b8 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
            {
                GraphicsElement.A177(this._b7, this._b5, this._b6, ref b9, ref b10);
            }
            if ((this._b8 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
            {
                GraphicsElement.A160(this._b4, ref b9, ref b10);
            }
            A119(b10, this._b0, b9.A98(this._b1), this._b2, this._b3);
            GraphicsElement.A180(b10, this._b8, false);
        }

        internal static void A119(A55 b11, float b0, float b1, float b2, float b3)
        {
            bool flag = true;
            float num = 3.141593f;
            float num2 = b2 / 2f;
            float num3 = b3 / 2f;
            float num4 = b0 + num2;
            float num5 = b1 - num3;
            float num6 = 0f;
            float num7 = 360f;
            flag = true;
            while (num6 < num7)
            {
                float num8;
                float num9;
                if ((num7 - num6) < 90f)
                {
                    num8 = num6;
                    num9 = num7;
                }
                else
                {
                    num8 = num6;
                    num9 = num8 + 90f;
                }
                num8 = (num8 * num) / 180f;
                num9 = (num9 * num) / 180f;
                float num10 = (float) Math.Sin((double) num8);
                float num11 = (float) Math.Cos((double) num8);
                float num12 = (float) Math.Sin((double) num9);
                float num13 = (float) Math.Cos((double) num9);
                float num14 = ((4f * ((float) (1.0 - Math.Cos((double) ((num9 - num8) / 2f))))) / ((float) Math.Sin((double) ((num9 - num8) / 2f)))) / 3f;
                float num15 = num2 * num11;
                float num16 = num3 * num10;
                float num17 = num2 * (num11 - (num14 * num10));
                float num18 = num3 * (num10 + (num14 * num11));
                float num19 = num2 * (num13 + (num14 * num12));
                float num20 = num3 * (num12 - (num14 * num13));
                float num21 = num2 * num13;
                float num22 = num3 * num12;
                float num23 = num15 + num4;
                float num24 = num16 + num5;
                float num25 = num17 + num4;
                float num26 = num18 + num5;
                float num27 = num19 + num4;
                float num28 = num20 + num5;
                float num29 = num21 + num4;
                float num30 = num22 + num5;
                num15 = num23;
                num16 = num24;
                num17 = num25;
                num18 = num26;
                num19 = num27;
                num20 = num28;
                num21 = num29;
                num22 = num30;
                if (flag)
                {
                    flag = false;
                    GraphicsElement.A178(b11, num15, num16);
                }
                GraphicsElement.A472(b11, num17, num18, num19, num20, num21, num22);
                num6 += 90f;
            }
        }

        public PdfColor FillColor
        {
            get
            {
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
        {
            get
            {
                return this._b8;
            }
            set
            {
                this._b8 = value;
            }
        }

        public float Height
        {
            get
            {
                return this._b3;
            }
            set
            {
                this._b3 = value;
            }
        }

        public PdfColor LineColor
        {
            get
            {
                return this._b5;
            }
            set
            {
                this._b5 = value;
            }
        }

        public MControl.Printing.Pdf.LineStyle LineStyle
        {
            get
            {
                return this._b6;
            }
            set
            {
                this._b6 = value;
            }
        }

        public float LineWidth
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        public float Width
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

        public float X
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

        public float Y
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
    }
}

