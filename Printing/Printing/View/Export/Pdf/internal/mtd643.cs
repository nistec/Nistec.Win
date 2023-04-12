namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal class mtd643 : mtd747
    {
        private string _var0;
        private Image _var1;
        private int _var2;
        private int _var3;
        private float _var4;
        private float _var5;
        private mtd742 _var6;
        private ImageFormat _var7;

        internal mtd643(Image var1, int var8, PDFDocument var9)
        {
            base._mtd756 = var9;
            this._var1 = var1;
            this._var2 = this._var1.Width;
            this._var3 = this._var1.Height;
            this._var4 = this._var1.HorizontalResolution;
            this._var5 = this._var1.VerticalResolution;
            this._var0 = string.Format("I{0}", var8);
            this.var10();
        }

        internal override void mtd710(ref mtd711 var11)
        {
            base._mtd756.mtd757.mtd758(var11.mtd32, 0);
            var11.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var11.mtd715("<<");
            var11.mtd715("/Type /XObject ");
            var11.mtd715("/Subtype /Image ");
            var11.mtd715(string.Format("/Name /{0} ", this.mtd763));
            var11.mtd715(string.Format("/Length {0} ", this._var6.mtd32));
            var11.mtd715(string.Format("/Width {0} ", this._var2));
            var11.mtd715(string.Format("/Height {0} ", this._var3));
            if (this._var7 == ImageFormat.Tiff)
            {
                var11.mtd715("/Filter /CCITTFaxDecode ");
                var11.mtd715("/ColorSpace /DeviceGray ");
                var11.mtd715("/BitsPerComponent 1 ");
                var11.mtd715("/DecodeParms << ");
                var11.mtd715("/K 1");
                var11.mtd715(string.Format("/Columns /{0} ", this._var2));
                var11.mtd715(string.Format("/Rows /{0} ", this._var3));
                var11.mtd715("/EndOfLine false ");
                var11.mtd715("/EncodedByteAlign false ");
                var11.mtd715("/Blackls1 true ");
                var11.mtd715(">>");
            }
            else
            {
                if (this._var7 == ImageFormat.Jpeg)
                {
                    var11.mtd715("/Filter /DCTDecode ");
                }
                else
                {
                    var11.mtd715("/Filter /FlateDecode ");
                }
                var11.mtd715("/ColorSpace /DeviceRGB ");
                var11.mtd715("/BitsPerComponent 8");
            }
            var11.mtd715(">>");
            var11.mtd783("stream");
            if (this._var6 != null)
            {
                byte[] buffer = this._var6.mtd784;
                int num = this._var6.mtd32;
                if (base._mtd756.mtd712 != null)
                {
                    base._mtd756.mtd712.mtd775(this.mtd759, 0);
                    buffer = base._mtd756.mtd712.mtd713(buffer, num);
                    var11.mtd710(buffer, buffer.Length);
                }
                else
                {
                    var11.mtd710(buffer, num);
                }
            }
            var11.mtd710((byte) 13);
            var11.mtd715("endstream");
            var11.mtd715("endobj");
        }

        private void var10()
        {
            if (this._var1.RawFormat.Guid == ImageFormat.Jpeg.Guid)
            {
                this._var7 = ImageFormat.Jpeg;
                this._var6 = mtd741.mtd743(this._var1);
            }
            else
            {
                this._var7 = ImageFormat.Bmp;
                this._var6 = mtd741.mtd744(this._var1);
            }
        }

        internal int mtd30
        {
            get
            {
                return this._var2;
            }
        }

        internal int mtd31
        {
            get
            {
                return this._var3;
            }
        }

        internal override string mtd763
        {
            get
            {
                return this._var0;
            }
        }

        internal float mtd781
        {
            get
            {
                return this._var4;
            }
        }

        internal float mtd782
        {
            get
            {
                return this._var5;
            }
        }

        internal Image mtd9
        {
            get
            {
                return this._var1;
            }
        }
    }
}

