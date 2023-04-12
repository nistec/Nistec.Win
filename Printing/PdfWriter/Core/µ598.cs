namespace MControl.Printing.Pdf.Core
{
    using MControl.Printing.Pdf.Core.Collection;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Controls;
    using System;
    using System.Drawing;

    internal class A598
    {
        private A12 _b0;
        private A5 _b1;
        private A13 _b2;
        private A0 _b3;
        private PdfFields _b4;
        private A599 _b5;
        private Document _b6;

        internal A598(Document b6)
        {
            this._b6 = b6;
            this._b0 = new A12();
            this._b1 = new A5();
            this._b2 = new A13();
            this._b3 = new A0();
            this._b5 = new A599();
            this._b4 = b6.Form.Fields;
        }

        internal void A164(PdfField b16, bool parentfield)
        {
            if (b16 != null)
            {
                b16.A110();
                if (parentfield)
                {
                    this._b4.A3(b16);
                }
                this._b5.A3(b16);
            }
        }

        internal void A4()
        {
            this._b0.A4();
            this._b1.A4();
            this._b2.A4();
            this._b3.A4();
            this._b5.A4();
        }

        internal A14 A520(Image b10, ColorSpace b11, bool b12, A14 b13, int b14)
        {
            if (b10 == null)
            {
                return null;
            }
            for (int i = 0; i < this._b2.A2; i++)
            {
                if (this._b2[i].A198(b10, b11, b12, b13, b14))
                {
                    return this._b2[i];
                }
            }
            A14 A = new A14(b10, this._b2.A2, this._b6, b11, b12, b13, b14);
            A.A110();
            this._b2.A3(A);
            this._b5.A3(A);
            return A;
        }

        internal A6 A531(float b8, float b9)
        {
            A6 A = new A6(b8, b9, this._b1.A2, this._b6);
            if (A != null)
            {
                for (int i = 0; i < this._b1.A2; i++)
                {
                    if (A6.A198(this._b1[i], A))
                    {
                        return this._b1[i];
                    }
                }
                A.A110();
                this._b1.A3(A);
                this._b5.A3(A);
            }
            return A;
        }

        internal PdfFont A603(PdfFont b7)
        {
            PdfFont font = b7;
            if (font == null)
            {
                font = this._b6.A428;
            }
            for (int i = 0; i < this._b0.A2; i++)
            {
                if (PdfFont.A198(this._b0[i], font))
                {
                    return this._b0[i];
                }
            }
            font.A237(this._b6, this._b0.A2);
            font.A110();
            this._b0.A3(font);
            this._b5.A3(font);
            return font;
        }

        internal A1 A604(A1 b15)
        {
            if (b15 != null)
            {
                for (int i = 0; i < this._b3.A2; i++)
                {
                    if (this._b3[i] == b15)
                    {
                        return this._b3[i];
                    }
                }
                b15.A110();
                this._b3.A3(b15);
                this._b5.A3(b15);
            }
            return b15;
        }

        internal A599 A600
        {
            get
            {
                return this._b5;
            }
        }
    }
}

