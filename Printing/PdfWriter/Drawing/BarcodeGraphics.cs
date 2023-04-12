namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.Text;

    public abstract class BarcodeGraphics : GraphicsElement
    {
        private float _A411;
        internal float _A412 = 4f;
        internal float _A413 = 4f;
        internal float _A414 = 4f;
        internal float _A415 = 4f;
        internal string _A416;
        internal PdfFont _A417 = A428();
        internal bool _A418 = false;
        internal TextAlignment _A419 = TextAlignment.Center;
        internal PdfColor _A420 = PdfColor.White;
        internal PdfColor _A421 = PdfColor.Black;
        internal float _A422 = 20f;
        internal float _A423 = 0.75f;
        private float _b0;
        private float _b1;
        private PdfFont _b10 = A428();
        private LineStyle _b11 = LineStyle.Solid;
        private float _b12 = 1f;
        private PdfColor _b13 = PdfColor.Transparent;
        private float _b14 = 0f;
        private float _b15;
        private bool _b3 = false;
        private bool _b4 = true;
        private float _b5 = 3f;
        private string _b6 = string.Empty;
        private float _b7 = 3f;
        private TextAlignment _b8 = TextAlignment.Center;
        private MControl.Printing.Pdf.Drawing.CaptionType _b9 = MControl.Printing.Pdf.Drawing.CaptionType.None;
        internal static string A424 = "0123456789";
        internal static string A425 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        internal static string A426 = "abcdefghijklmnopqrstuvwxyz";
        internal static string A427 = b16("", 0x80);
        private static PdfFont b2;

        internal override void A119(ref A120 b17, ref A112 b18)
        {
            this.A86();
            if (this.IsValidData(this._A416))
            {
                try
                {
                    this.A433();
                    this.A432();
                    float num = b17.A98(this._b1);
                    float num2 = this.A431(false);
                    float num3 = this.A434();
                    GraphicsElement.A435(ref b17, ref b18);
                    if (this._b14 > 0f)
                    {
                        GraphicsElement.A436(this._b0, this._b1, 0f, 0f, this._b14, ref b17, ref b18);
                    }
                    if (this._A420 != PdfColor.Transparent)
                    {
                        GraphicsElement.A160(this._A420, ref b17, ref b18);
                        GraphicsElement.A437(b18, this._b0, num, num2, num3);
                        GraphicsElement.A180(b18, GraphicsMode.fill, false);
                    }
                    this.A430(ref b17, ref b18, num2);
                    if (this._b13 != PdfColor.Transparent)
                    {
                        GraphicsElement.A177(this._b12, this._b13, this._b11, ref b17, ref b18);
                        GraphicsElement.A437(b18, this._b0, num, num2, num3);
                        GraphicsElement.A180(b18, GraphicsMode.stroke, false);
                    }
                    GraphicsElement.A438(ref b17, ref b18);
                }
                catch
                {
                }
            }
        }

        internal static PdfFont A428()
        {
            if (b2 == null)
            {
                lock (typeof(BarcodeGraphics))
                {
                    b2 = new PdfFont(new Font("Courier New", 3f, GraphicsUnit.Millimeter), true);
                }
            }
            return b2;
        }

        internal abstract string A429();
        internal abstract void A430(ref A120 b17, ref A112 b18, float b19);
        internal abstract float A431(bool b20);
        internal abstract void A432();
        internal abstract void A433();
        internal virtual float A434()
        {
            float num = (this._A412 + this._A414) + this.A445();
            float num2 = this.CodeTextFont.Height(this.CodeTextFontSize);
            float num3 = this.CaptionFont.Height(this.CaptionFontSize);
            if (this._b3)
            {
                num += num2 + this._b5;
            }
            if (this.A441())
            {
                if ((this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.Top) || (this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.Bottom))
                {
                    return (num + (num3 + this._b7));
                }
                if (this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.TopAndBottom)
                {
                    num += 2f * (num3 + this._b7);
                }
            }
            return num;
        }

        internal static int A439(char c)
        {
            return (c - '0');
        }

        internal virtual float A440()
        {
            float num = this._A412 + this._b1;
            if (this.A441() && (this.CaptionType != MControl.Printing.Pdf.Drawing.CaptionType.Bottom))
            {
                num += this.CaptionFont.Height(this.CaptionFontSize) + this._b7;
            }
            return num;
        }

        internal bool A441()
        {
            return (((this._b9 != MControl.Printing.Pdf.Drawing.CaptionType.None) && (this._b6 != null)) && (this._b6.Length > 0));
        }

        internal void A442(ref A120 b17, ref A112 b18, float b23, float b19)
        {
            if (this.A441())
            {
                float num = this._b0 + this._A413;
                float num2 = b17.A98(this._b1 + this._A412);
                this.CaptionFont.Height(this.CaptionFontSize);
                float num3 = this._b7 / 2f;
                if ((this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.Top) || (this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.TopAndBottom))
                {
                    A443(ref b17, ref b18, this._b6, num, num2, b19, num3, this.CaptionFont, this.CaptionFontSize, this._b8);
                }
                if ((this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.Bottom) || (this._b9 == MControl.Printing.Pdf.Drawing.CaptionType.TopAndBottom))
                {
                    num2 = b23;
                    A443(ref b17, ref b18, this._b6, num, num2, b19, num3, this.CaptionFont, this.CaptionFontSize, this._b8);
                }
            }
        }

        internal static void A443(ref A120 b17, ref A112 b18, string b27, float b28, float b29, float b30, float b31, PdfFont b32, float b33, TextAlignment b34)
        {
            float textWidth = b32.GetTextWidth(b27, b33);
            float num2 = b32.Height(b33);
            float num3 = b28;
            float num4 = b29 - num2;
            if (textWidth < b30)
            {
                if (b34 == TextAlignment.Left)
                {
                    num3 = b28;
                }
                else if (b34 == TextAlignment.Center)
                {
                    num3 = b28 + ((b30 - textWidth) / 2f);
                }
                else
                {
                    num3 = b28 + (b30 - textWidth);
                }
            }
            GraphicsElement.A435(ref b17, ref b18);
            GraphicsElement.A446(b18, b28, b29 + b31, b30, num2 + b31);
            b32 = GraphicsElement.A157(b32, b33, ref b17, ref b18);
            b32.A159(b27);
            GraphicsElement.A162(b18, num3, num4, 0f, b32.A163(b27), false);
            GraphicsElement.A438(ref b17, ref b18);
        }

        internal void A444(ref A120 b17, ref A112 b18, float b24, float b19, string b25)
        {
            this.A444(ref b17, ref b18, b24, b19, this._A416, b25);
        }

        internal void A444(ref A120 b17, ref A112 b18, float b24, float b19, string b26, string b25)
        {
            float num = (b19 - this._A413) - this._A415;
            float num2 = b24 - this.A445();
            if (this.CodeTextVisible)
            {
                if ((this.CodeTextAsSymbol && this.EnableCheckSum) && (b25 != null))
                {
                    b26 = b26 + b25;
                }
                float num3 = this._A413 + this._b0;
                float num4 = (b24 - this.A445()) - this._b5;
                float num5 = this._b5 / 2f;
                A443(ref b17, ref b18, b26, num3, num4, num, num5, this.CodeTextFont, this.CodeTextFontSize, this._A419);
                num2 -= (this._b5 + this.CodeTextFont.Height(this.CodeTextFontSize)) + this._b7;
            }
            else
            {
                num2 -= this._b7;
            }
            this.A442(ref b17, ref b18, num2, num);
        }

        internal virtual float A445()
        {
            return this._A422;
        }

        internal virtual bool A447(string b35)
        {
            if ((b35 == null) || (b35.Length == 0))
            {
                return false;
            }
            string str = this.A429();
            int length = b35.Length;
            for (int i = 0; i < length; i++)
            {
                if (str.IndexOf(b35[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal abstract void A86();
        public virtual bool IsValidData(string data)
        {
            return this.A447(data);
        }

        public SizeF MeasureSize()
        {
            return this.MeasureSize(this._A416);
        }

        public SizeF MeasureSize(string data)
        {
            string str = this._A416;
            this.A86();
            this._A416 = data;
            float width = 0f;
            float height = 0f;
            if (this.IsValidData(this._A416))
            {
                width = this.A431(true);
                height = this.A434();
            }
            this.A86();
            this._A416 = str;
            return new SizeF(width, height);
        }

        private static string b16(string b21, int b22)
        {
            StringBuilder builder = new StringBuilder(b22);
            for (int i = 1; i < b22; i++)
            {
                char ch = (char) i;
                if (b21.IndexOf(ch) < 0)
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public PdfColor BackColor
        {
            get
            {
                return this._A420;
            }
            set
            {
                this._A420 = value;
            }
        }

        public virtual BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Codabar;
            }
        }

        public virtual float BarHeight
        {
            get
            {
                return this._A422;
            }
            set
            {
                this._A422 = value;
            }
        }

        public PdfColor BorderColor
        {
            get
            {
                return this._b13;
            }
            set
            {
                this._b13 = value;
            }
        }

        public LineStyle BorderStyle
        {
            get
            {
                return this._b11;
            }
            set
            {
                this._b11 = value;
            }
        }

        public float BorderWidth
        {
            get
            {
                return this._b12;
            }
            set
            {
                this._b12 = value;
            }
        }

        public float BottomMargin
        {
            get
            {
                return this._A414;
            }
            set
            {
                this._A414 = value;
            }
        }

        public TextAlignment CaptionAlignment
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

        public PdfFont CaptionFont
        {
            get
            {
                return this._b10;
            }
            set
            {
                if (value != null)
                {
                    this._b10 = value;
                }
                else
                {
                    this._b10 = A428();
                }
            }
        }

        public float CaptionFontSize
        {
            get
            {
                if (this._A411 > 0f)
                {
                    return this._b15;
                }
                if (this.CaptionFont.A403 != null)
                {
                    return this.CaptionFont.A403.SizeInPoints;
                }
                return 6f;
            }
            set
            {
                this._A411 = value;
            }
        }

        public float CaptionSpace
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

        public string CaptionText
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

        public MControl.Printing.Pdf.Drawing.CaptionType CaptionType
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

        public string CodeText
        {
            get
            {
                return this._A416;
            }
            set
            {
                this._A416 = value;
            }
        }

        public virtual TextAlignment CodeTextAlignment
        {
            get
            {
                return this._A419;
            }
            set
            {
                this._A419 = value;
            }
        }

        public virtual bool CodeTextAsSymbol
        {
            get
            {
                return this._A418;
            }
            set
            {
                this._A418 = value;
            }
        }

        public PdfFont CodeTextFont
        {
            get
            {
                return this._A417;
            }
            set
            {
                if (value != null)
                {
                    this._A417 = value;
                }
                else
                {
                    this._A417 = A428();
                }
            }
        }

        public float CodeTextFontSize
        {
            get
            {
                if (this._b15 > 0f)
                {
                    return this._b15;
                }
                if (this.CodeTextFont.A403 != null)
                {
                    return this.CodeTextFont.A403.SizeInPoints;
                }
                return 6f;
            }
            set
            {
                this._b15 = value;
            }
        }

        public float CodeTextSpace
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

        public bool CodeTextVisible
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

        public virtual bool EnableCheckSum
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

        public PdfColor ForeColor
        {
            get
            {
                return this._A421;
            }
            set
            {
                this._A421 = value;
            }
        }

        public float LeftMargin
        {
            get
            {
                return this._A413;
            }
            set
            {
                this._A413 = value;
            }
        }

        public virtual float Module
        {
            get
            {
                return this._A423;
            }
            set
            {
                if (value > 0f)
                {
                    this._A423 = value;
                }
            }
        }

        public float RightMargin
        {
            get
            {
                return this._A415;
            }
            set
            {
                this._A415 = value;
            }
        }

        public float Rotate
        {
            get
            {
                return this._b14;
            }
            set
            {
                this._b14 = value;
            }
        }

        public float TopMargin
        {
            get
            {
                return this._A412;
            }
            set
            {
                this._A412 = value;
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

