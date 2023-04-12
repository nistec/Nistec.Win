namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal class A14 : A91
    {
        private string _b0;
        private Image _b1;
        private A14 _b10;
        private int _b11;
        private int _b2;
        private int _b3;
        private A112 _b4;
        private PixelFormat _b5;
        private A210 _b6;
        private ColorSpace _b7;
        private bool _b8;
        private string _b9;

        internal A14(Image b1, int b12, Document b13, ColorSpace b14, bool b8, A14 b10, int b11)
        {
            base._A92 = b13;
            this._b1 = b1;
            this._b2 = this._b1.Width;
            this._b3 = this._b1.Height;
            this._b0 = string.Format("I{0}", b12);
            this._b7 = b14;
            this._b8 = b8;
            this._b10 = b10;
            this._b11 = b11;
            this.b15();
        }

        internal bool A198(Image b1, ColorSpace b14, bool b8, A14 b10, int b11)
        {
            return ((((this._b1 == b1) && (this._b7 == b14)) && ((this._b8 == b8) && (this._b10 == b10))) && (this._b11 == b11));
        }

        internal override void A54(ref A55 b16)
        {
            base._A92.A93.A94(b16.A2, 0);
            b16.A59(string.Format("{0} 0 obj", this.A95));
            b16.A59("<<");
            b16.A59("/Type /XObject ");
            b16.A59("/Subtype /Image ");
            b16.A59(string.Format("/Name /{0} ", this.A168));
            b16.A59(string.Format("/Length {0} ", this._b4.A2));
            b16.A59(string.Format("/Width {0} ", this._b2));
            b16.A59(string.Format("/Height {0} ", this._b3));
            if ((this._b6 == A210.Raw) && (this._b5 == PixelFormat.Format1bppIndexed))
            {
                b16.A59("/Filter /CCITTFaxDecode ");
                b16.A59("/ColorSpace /DeviceGray ");
                b16.A59("/BitsPerComponent 1 ");
                b16.A59("/Decode " + this._b9);
                b16.A59("/DecodeParms << ");
                b16.A59("/K -1");
                b16.A59(string.Format("/Columns {0} ", this._b2));
                b16.A59(string.Format("/Rows {0} ", this._b3));
                b16.A59("/BlackIs1 true ");
                b16.A59(">>");
            }
            else
            {
                if (this._b6 == A210.Jpeg)
                {
                    b16.A59("/Filter /DCTDecode ");
                }
                else
                {
                    b16.A59("/Filter /FlateDecode ");
                }
                if (this._b7 == ColorSpace.CMYK)
                {
                    b16.A59("/ColorSpace /DeviceCMYK ");
                }
                else if (this._b7 == ColorSpace.GrayScale)
                {
                    b16.A59("/ColorSpace /DeviceGray ");
                }
                else
                {
                    b16.A59("/ColorSpace /DeviceRGB ");
                }
                if (!this._b8)
                {
                    b16.A59("/Decode " + this._b9);
                    if (this._b10 != null)
                    {
                        b16.A59(string.Format("/SMask {0} 0 R ", this._b10.A95));
                    }
                }
                if (this._b6 == A210.Raw)
                {
                    b16.A59("/BitsPerComponent " + A15.A18(A215.A220(this._b5)));
                }
                else
                {
                    b16.A59("/BitsPerComponent 8");
                }
            }
            b16.A59(">>");
            b16.A181("stream");
            if (this._b4 != null)
            {
                byte[] data = this._b4.A221;
                int size = this._b4.A2;
                if (base._A92.A56 != null)
                {
                    base._A92.A56.A100(this.A95, 0);
                    data = base._A92.A56.A57(data, size);
                    b16.A54(data, data.Length);
                }
                else
                {
                    b16.A54(data, size);
                }
            }
            b16.A54((byte) 13);
            b16.A59("endstream");
            b16.A59("endobj");
        }

        private void b15()
        {
            this._b5 = this._b1.PixelFormat;
            this._b6 = A210.Raw;
            if (this._b7 == ColorSpace.RGB)
            {
                this._b9 = "[0.0 1.0 0.0 1.0 0.0 1.0]";
            }
            else if (this._b7 == ColorSpace.CMYK)
            {
                this._b9 = "[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]";
            }
            else if (this._b7 == ColorSpace.GrayScale)
            {
                this._b9 = "[0.0 1.0]";
            }
            if (A215.A216(this._b1.RawFormat, this._b5, this._b7))
            {
                this._b6 = A210.Jpeg;
                this._b4 = A215.A217(ref this._b1);
            }
            else
            {
                Bitmap bitmap = this._b1 as Bitmap;
                this._b4 = A215.A218(ref bitmap, this._b7, this._b11);
                if ((this._b4 == null) || (this._b4.A2 == 0))
                {
                    this._b6 = A210.Bmp;
                    this._b4 = A215.A219(ref this._b1);
                }
                else if (this._b5 == PixelFormat.Format1bppIndexed)
                {
                    Color color = bitmap.Palette.Entries[0];
                    Color color2 = bitmap.Palette.Entries[1];
                    float num = PdfColor.GetGray(color.R, color.G, color.B);
                    float num2 = PdfColor.GetGray(color2.R, color2.G, color2.B);
                    this._b9 = A15.A29(new float[] { num, num2 });
                }
            }
        }

        internal override string A168
        {
            get
            {
                return this._b0;
            }
        }

        internal int A211
        {
            get
            {
                return this._b2;
            }
        }

        internal int A212
        {
            get
            {
                return this._b3;
            }
        }

        internal ColorSpace A213
        {
            get
            {
                return this._b7;
            }
        }

        internal bool A214
        {
            get
            {
                return this._b8;
            }
        }

        internal Image A49
        {
            get
            {
                return this._b1;
            }
        }
    }
}

