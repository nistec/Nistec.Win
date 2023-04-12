namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class Text : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private string _b2;
        private PdfFont _b3;
        private float _b4;
        private PdfColor _b5;
        private bool _b6;
        private bool _b7;

        public Text(float x, float y, string text, PdfColor textcolor, PdfFont font, float fontsize, bool underline, bool rightToleft, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = text;
            this._b3 = font;
            this._b4 = fontsize;
            this._b5 = textcolor;
            this._b6 = underline;
            this._b7 = rightToleft;
            base._A154 = name;
        }

        internal override void A119(ref A120 b8, ref A112 b9)
        {
            this._b3 = GraphicsElement.A157(this._b3, this._b4, ref b8, ref b9);
            this._b3.A159(this._b2);
            GraphicsElement.A160(this._b5, ref b8, ref b9);
            string str = this._b2;
            if (this._b7)
            {
                str = GraphicsElement.A161(str);
            }
            if (!this._b6)
            {
                GraphicsElement.A162(b9, this._b0, b8.A98(this._b1), 0f, this._b3.A163(str), false);
            }
            else
            {
                float num = 1f;
                if (this._b3.A405 == FontStyle.Bold)
                {
                    num = 1.25f;
                }
                GraphicsElement.A177(num, this._b5, LineStyle.Solid, ref b8, ref b9);
                GraphicsElement.A162(b9, this._b0, b8.A98(this._b1), this._b3.GetTextWidth(str, this._b4), this._b3.A163(str), true);
            }
        }

        public PdfFont Font
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

        public float FontSize
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

        public bool RightToLeft
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

        public PdfColor TextColor
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

        public string TextValue
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

        public bool UnderLine
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

