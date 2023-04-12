namespace MControl.Printing.Pdf.Core.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using System;
    using System.Drawing;

    internal class A529
    {
        internal float A172 = 8f;
        internal float A196 = 1f;
        internal float A197 = 1f;
        internal PdfFont A533 = null;
        internal PdfColor A534 = PdfColor.Black;
        internal LineStyle A535 = LineStyle.Solid;
        internal float A536 = 1f;
        internal PdfColor A537 = PdfColor.Black;
        internal RectangleF A539;

        internal A529()
        {
        }

        internal A529 A496()
        {
            A529 A = new A529();
            A.A536 = this.A536;
            A.A535 = this.A535;
            A.A537 = this.A537;
            A.A534 = this.A534;
            A.A539 = this.A539;
            A.A172 = this.A172;
            A.A533 = this.A533;
            A.A196 = this.A196;
            A.A197 = this.A197;
            return A;
        }

        internal PdfFont A403
        {
            get
            {
                return this.A533;
            }
        }

        internal PdfColor A540
        {
            get
            {
                return this.A534;
            }
        }

        internal float A541
        {
            get
            {
                return this.A536;
            }
        }

        internal PdfColor A542
        {
            get
            {
                return this.A537;
            }
        }

        internal LineStyle A543
        {
            get
            {
                return this.A535;
            }
        }

        internal float A544
        {
            get
            {
                return this.A172;
            }
        }

        internal float A545
        {
            get
            {
                return this.A196;
            }
        }

        internal float A546
        {
            get
            {
                return this.A197;
            }
        }
    }
}

