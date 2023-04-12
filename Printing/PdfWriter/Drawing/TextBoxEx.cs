namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using MControl.Printing.Pdf.Core.Text;
    using System;

    public class TextBoxEx : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private MControl.Printing.Pdf.Border _b10;
        private float _b2;
        private float _b3;
        private string _b4;
        private float _b5;
        private PdfColor _b6;
        private MControl.Printing.Pdf.Drawing.TextStyle _b7;
        private TextAlignment _b8;
        private bool _b9;

        public TextBoxEx(float x, float y, float width, float height, string text, MControl.Printing.Pdf.Drawing.TextStyle textstyle, TextAlignment textalign, bool rightToleft, float pad, PdfColor backcolor, MControl.Printing.Pdf.Border border, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b4 = text;
            this._b5 = pad;
            this._b6 = backcolor;
            this._b7 = textstyle;
            this._b8 = textalign;
            this._b9 = rightToleft;
            this._b10 = border;
            base._A154 = name;
        }

        internal override void A119(ref A120 b11, ref A112 b12)
        {
            GraphicsElement.A435(ref b11, ref b12);
            GraphicsElement.A446(b12, this._b0, b11.A98(this._b1), this._b2, this._b3);
            GraphicsElement.A175(this._b0, this._b1, this._b2, this._b3, this._b6, ref b11, ref b12);
            b13(this, ref b11, ref b12);
            GraphicsElement.A438(ref b11, ref b12);
            GraphicsElement.A156(this._b0, this._b1, this._b2, this._b3, this._b10, ref b11, ref b12);
        }

        private static void b13(TextBoxEx b14, ref A120 b11, ref A112 b12)
        {
            float num = b14._b5;
            A497 A = new A497();
            A.A3(new A498(b14._b4, b14._b7));
            A500 A2 = new A500(A, 0f, 0f, 0f, 0f, b14._b8, b14._b9);
            A2.A501(b14._b2 - (2f * num), b14._b7, false);
            A478 A3 = A2.A479;
            A500.A119(ref b11, ref b12, A3, 0, A3.A2, b14._b0, b14._b1, num, num, b14._b9);
        }

        public PdfColor BackColor
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

        public MControl.Printing.Pdf.Border Border
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

        public float Pad
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

        public bool RightToLeft
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

        public string Text
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

        public TextAlignment TextAlign
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

        public MControl.Printing.Pdf.Drawing.TextStyle TextStyle
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

