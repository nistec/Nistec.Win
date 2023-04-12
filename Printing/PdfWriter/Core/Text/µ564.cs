namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using System;

    internal class A564
    {
        private float _b0;
        private MControl.Printing.Pdf.Core.Text.A523 _b1;
        private A44 _b2;
        private StyleBase _b3;
        private A555 _b4;

        internal A564(A44 b2, MControl.Printing.Pdf.Core.Text.A523 b1)
        {
            this._b0 = 0f;
            this._b2 = b2;
            this._b1 = b1;
            this._b3 = b1.A405;
            this._b4 = new A555();
        }

        internal A564(A44 b2, MControl.Printing.Pdf.Core.Text.A523 b1, A556[] b5, float b0)
        {
            this._b0 = b0;
            this._b2 = b2;
            this._b1 = b1;
            this._b3 = b1.A405;
            this._b4 = new A555(b5);
        }

        internal void A589(int b6, char b7)
        {
            float charWidth = this._b3.Font.GetCharWidth(b7, this._b3.FontSize);
            this._b0 += charWidth;
            this._b4.A3(new A556(b6, b7, charWidth));
        }

        internal float A211
        {
            get
            {
                return this._b0;
            }
        }

        internal float A408
        {
            get
            {
                return this._b3.Font.A158(this._b3.FontSize);
            }
        }

        internal float A409
        {
            get
            {
                return Math.Abs(this._b3.Font.A583(this._b3.FontSize));
            }
        }

        internal float A410
        {
            get
            {
                return this._b3.Font.A584(this._b3.FontSize);
            }
        }

        internal bool A506
        {
            get
            {
                if (!this._b3.A506 && (this._b1.A585 == null))
                {
                    return false;
                }
                return true;
            }
        }

        internal MControl.Printing.Pdf.Core.Text.A523 A523
        {
            get
            {
                return this._b1;
            }
        }

        internal bool A575
        {
            get
            {
                return (this._b2 == A44.A45);
            }
        }

        internal float A576
        {
            set
            {
                this._b0 += value;
            }
        }

        internal A555 A577
        {
            get
            {
                return this._b4;
            }
        }

        internal bool A582
        {
            get
            {
                if (this._b4.A2 > 0)
                {
                    return false;
                }
                return true;
            }
        }

        internal A44 A587
        {
            get
            {
                return this._b2;
            }
        }

        internal bool A588
        {
            get
            {
                return (this._b1.A405.BestWordFit && (this._b4.A2 > 4));
            }
        }
    }
}

