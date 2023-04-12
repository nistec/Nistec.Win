namespace MControl.Printing.Pdf.Core
{
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Controls;
    using MControl.Printing.Pdf.Drawing;
    using System;
    using System.Collections;
    using System.Drawing;

    internal class A120
    {
        private A599 _b0;
        private A599 _b1;
        private A599 _b2;
        private A599 _b3;
        private A598 _b4;
        private A529 _b5;
        private Stack _b6;
        private Page _b7;

        internal A120(Page b7, ref A598 b8)
        {
            this._b4 = b8;
            this._b7 = b7;
            this._b0 = new A599();
            this._b1 = new A599();
            this._b2 = new A599();
            this._b3 = new A599();
            this._b5 = new A529();
            this._b6 = new Stack();
        }

        internal A1 A121(A1 b17)
        {
            A1 A = this._b4.A604(b17);
            for (int i = 0; i < this._b3.A2; i++)
            {
                if (this._b3[i] == A)
                {
                    return A;
                }
            }
            this._b3.A3(A);
            return A;
        }

        internal void A164(PdfField b18, bool rootfield, bool lastfield)
        {
            this._b4.A164(b18, rootfield);
            if (lastfield)
            {
                this._b3.A3(b18);
            }
        }

        internal PdfFont A167(PdfFont b9)
        {
            PdfFont font = this._b4.A603(b9);
            for (int i = 0; i < this._b0.A2; i++)
            {
                if (this._b0[i] == font)
                {
                    return font;
                }
            }
            this._b0.A3(font);
            return font;
        }

        internal void A435()
        {
            this._b6.Push(this._b5.A496());
        }

        internal void A438()
        {
            this._b5 = (A529) this._b6.Pop();
        }

        internal A14 A520(Image b12, ColorSpace b13, bool b14, A14 b15, int b16)
        {
            A14 A = this._b4.A520(b12, b13, b14, b15, b16);
            for (int i = 0; i < this._b2.A2; i++)
            {
                if (this._b2[i] == A)
                {
                    return A;
                }
            }
            if (!b14)
            {
                this._b2.A3(A);
            }
            return A;
        }

        internal A6 A531(float b10, float b11)
        {
            A6 A = this._b4.A531(b10, b11);
            for (int i = 0; i < this._b1.A2; i++)
            {
                if (this._b1[i] == A)
                {
                    return A;
                }
            }
            this._b1.A3(A);
            return A;
        }

        internal float A98(float b19)
        {
            return (this._b7.Height - b19);
        }

        internal A599 A13
        {
            get
            {
                return this._b2;
            }
        }

        internal float A211
        {
            get
            {
                return this._b7.Width;
            }
        }

        internal float A212
        {
            get
            {
                return this._b7.Height;
            }
        }

        internal A599 A5
        {
            get
            {
                return this._b1;
            }
        }

        internal Stack A522
        {
            get
            {
                return this._b6;
            }
        }

        internal A529 A530
        {
            get
            {
                return this._b5;
            }
        }

        internal A599 A601
        {
            get
            {
                return this._b0;
            }
        }

        internal A599 A602
        {
            get
            {
                return this._b3;
            }
        }

        internal Document A97
        {
            get
            {
                return this._b7.A97;
            }
        }
    }
}

