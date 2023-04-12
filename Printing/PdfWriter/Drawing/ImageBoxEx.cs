namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class ImageBoxEx : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private int _b10;
        private float _b2;
        private float _b3;
        private Image _b4;
        private PictureAlignment _b5;
        private MControl.Printing.Pdf.SizeMode _b6;
        private MControl.Printing.Pdf.Border _b7;
        private ColorSpace _b8;
        private Image _b9;

        public ImageBoxEx(Image image, int frameindex, ColorSpace colorspace, Image imageMask, float x, float y, float width, float height, PictureAlignment align, MControl.Printing.Pdf.SizeMode sizemode, MControl.Printing.Pdf.Border border, string name)
        {
            this._b4 = image;
            this._b10 = frameindex;
            this._b8 = colorspace;
            this._b9 = imageMask;
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b5 = align;
            this._b6 = sizemode;
            this._b7 = border;
            base._A154 = name;
        }

        internal override void A119(ref A120 b11, ref A112 b12)
        {
            if (this._b4 != null)
            {
                A14 A = null;
                if (this._b9 != null)
                {
                    A14 A2 = b11.A520(this._b9, ColorSpace.GrayScale, true, null, this._b10);
                    A = b11.A520(this._b4, this._b8, false, A2, this._b10);
                }
                else
                {
                    A = b11.A520(this._b4, this._b8, false, null, this._b10);
                }
                float width = this._b2;
                float height = this._b3;
                if (this._b2 <= 0f)
                {
                    width = b11.A211;
                }
                if (this._b3 <= 0f)
                {
                    height = b11.A212;
                }
                RectangleF ef = new RectangleF(this._b0, b11.A98(this._b1), width, height);
                GraphicsElement.A435(ref b11, ref b12);
                GraphicsElement.A446(b12, ef.X, ef.Y, ef.Width, ef.Height);
                if (this._b6 == MControl.Printing.Pdf.SizeMode.Stretch)
                {
                    b12.A176(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { A15.A18(ef.Width), A15.A18(ef.Height), A15.A18(ef.X), A15.A18((float) (ef.Y - ef.Height)) }));
                    b12.A176(string.Format("/{0} Do", A.A168));
                }
                else
                {
                    float num;
                    float num2;
                    float num3;
                    float num4;
                    if (this._b6 == MControl.Printing.Pdf.SizeMode.Zoom)
                    {
                        b13(out num, out num2, out num3, out num4, ef, (float) A.A211, (float) A.A212, this._b5);
                        b12.A176(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { A15.A18(num3), A15.A18(num4), A15.A18(num), A15.A18(num2) }));
                        b12.A176(string.Format("/{0} Do", A.A168));
                    }
                    else
                    {
                        num3 = A15.A30((float) A.A211, GraphicsUnits.Pixel, GraphicsUnits.Point);
                        num4 = A15.A30((float) A.A212, GraphicsUnits.Pixel, GraphicsUnits.Point);
                        b14(out num, out num2, ef, num3, num4, this._b5);
                        b12.A176(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { A15.A18(num3), A15.A18(num4), A15.A18(num), A15.A18(num2) }));
                        b12.A176(string.Format("/{0} Do", A.A168));
                    }
                }
                GraphicsElement.A438(ref b11, ref b12);
                GraphicsElement.A521(ref b11, ref b12, this._b7, ef);
            }
        }

        private static void b13(out float b0, out float b1, out float b18, out float b19, RectangleF b15, float b16, float b17, PictureAlignment b5)
        {
            b19 = b15.Height;
            b18 = (b15.Height * b16) / b17;
            if (b18 > b15.Width)
            {
                b18 = b15.Width;
                b19 = (b15.Width * b17) / b16;
                b0 = b15.X;
                if ((b5 == PictureAlignment.TopLeft) || (b5 == PictureAlignment.TopRight))
                {
                    b1 = b15.Y - b19;
                }
                else if ((b5 == PictureAlignment.BottomLeft) || (b5 == PictureAlignment.BottomRight))
                {
                    b1 = b15.Y - b15.Height;
                }
                else
                {
                    b1 = (b15.Y - b15.Height) + ((b15.Height - b19) / 2f);
                }
            }
            else
            {
                b1 = b15.Y - b15.Height;
                if ((b5 == PictureAlignment.TopLeft) || (b5 == PictureAlignment.BottomLeft))
                {
                    b0 = b15.X;
                }
                else if ((b5 == PictureAlignment.TopRight) || (b5 == PictureAlignment.BottomRight))
                {
                    b0 = (b15.X + b15.Width) - b18;
                }
                else
                {
                    b0 = b15.X + ((b15.Width - b18) / 2f);
                }
            }
        }

        private static void b14(out float b0, out float b1, RectangleF b15, float b16, float b17, PictureAlignment b5)
        {
            if (b5 == PictureAlignment.TopLeft)
            {
                b0 = b15.X;
                b1 = b15.Y - b17;
            }
            else if (b5 == PictureAlignment.TopRight)
            {
                b0 = b15.Right - b16;
                b1 = b15.Y - b17;
            }
            else if (b5 == PictureAlignment.BottomLeft)
            {
                b0 = b15.X;
                b1 = b15.Y - b15.Height;
            }
            else if (b5 == PictureAlignment.BottomRight)
            {
                b0 = b15.Right - b16;
                b1 = b15.Y - b15.Height;
            }
            else if (b5 == PictureAlignment.Center)
            {
                b0 = b15.X + ((b15.Width - b16) / 2f);
                b1 = (b15.Y - ((b15.Height - b17) / 2f)) - b17;
            }
            else
            {
                b0 = b15.X;
                b1 = b15.Y - b17;
            }
        }

        public PictureAlignment Align
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

        public MControl.Printing.Pdf.Border Border
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

        public MControl.Printing.Pdf.SizeMode SizeMode
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

