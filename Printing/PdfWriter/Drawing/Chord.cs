namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Chord : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private MControl.Printing.Pdf.GraphicsMode _b10;
        private float _b2;
        private float _b3;
        private float _b4;
        private float _b5;
        private PdfColor _b6;
        private PdfColor _b7;
        private MControl.Printing.Pdf.LineStyle _b8;
        private float _b9;

        public Chord(float x, float y, float width, float height, float startangle, float endangle, float linewidth, MControl.Printing.Pdf.LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, MControl.Printing.Pdf.GraphicsMode mode, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b4 = startangle;
            this._b5 = endangle;
            this._b9 = linewidth;
            this._b8 = linestyle;
            this._b7 = linecolor;
            this._b6 = fillcolor;
            this._b10 = mode;
            base._A154 = name;
        }

        internal override void A119(ref A120 b11, ref A112 b12)
        {
            if ((this._b10 & MControl.Printing.Pdf.GraphicsMode.stroke) == MControl.Printing.Pdf.GraphicsMode.stroke)
            {
                GraphicsElement.A177(this._b9, this._b7, this._b8, ref b11, ref b12);
            }
            if ((this._b10 & MControl.Printing.Pdf.GraphicsMode.fill) == MControl.Printing.Pdf.GraphicsMode.fill)
            {
                GraphicsElement.A160(this._b6, ref b11, ref b12);
            }
            A119(b12, this._b0, b11.A98(this._b1), this._b2, this._b3, this._b4, this._b5);
            GraphicsElement.A180(b12, this._b10, false);
        }

        internal static void A119(A55 b13, float b0, float b1, float b2, float b3, float b4, float b5)
        {
            float num20 = b4;
            float num21 = b5;
            bool flag = true;
            while (num21 < num20)
            {
                num21 += 360f;
            }
            if (num20 != num21)
            {
                float num = b2 / 2f;
                float num2 = b3 / 2f;
                float num3 = b0 + num;
                float num4 = b1 - num2;
                while (num20 < num21)
                {
                    float num5;
                    float num6;
                    if ((num21 - num20) < 90f)
                    {
                        num5 = num20;
                        num6 = num21;
                    }
                    else
                    {
                        num5 = num20;
                        num6 = num5 + 90f;
                    }
                    num5 = (float) ((num5 * 3.1415926535897931) / 180.0);
                    num6 = (float) ((num6 * 3.1415926535897931) / 180.0);
                    float num7 = (float) Math.Sin((double) num5);
                    float num8 = (float) Math.Cos((double) num5);
                    float num9 = (float) Math.Sin((double) num6);
                    float num10 = (float) Math.Cos((double) num6);
                    float num11 = ((4f * ((float) (1.0 - Math.Cos((double) ((num6 - num5) / 2f))))) / ((float) Math.Sin((double) ((num6 - num5) / 2f)))) / 3f;
                    float num12 = (num * num8) + num3;
                    float num13 = (num2 * num7) + num4;
                    float num14 = (num * (num8 - (num11 * num7))) + num3;
                    float num15 = (num2 * (num7 + (num11 * num8))) + num4;
                    float num16 = (num * (num10 + (num11 * num9))) + num3;
                    float num17 = (num2 * (num9 - (num11 * num10))) + num4;
                    float num18 = (num * num10) + num3;
                    float num19 = (num2 * num9) + num4;
                    if (flag)
                    {
                        flag = false;
                        GraphicsElement.A178(b13, num12, num13);
                    }
                    GraphicsElement.A472(b13, num14, num15, num16, num17, num18, num19);
                    num20 += 90f;
                }
            }
        }

        public float EndAngle
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

        public PdfColor FillColor
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

        public MControl.Printing.Pdf.GraphicsMode GraphicsMode
        {
            get
            {
                return this._b10;
            }
            set
            {
                this._b10 = value;
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
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        public MControl.Printing.Pdf.LineStyle LineStyle
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

        public float LineWidth
        {
            get
            {
                return this._b9;
            }
            set
            {
                this._b9 = value;
            }
        }

        public float StartAngle
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

