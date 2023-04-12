namespace MControl.Printing.Pdf.Core.Controls
{
    using System;
    using MControl.Printing.Pdf.Controls;

    internal class A151 : A136
    {
        private PdfField[] _b0;
        private bool _b1;

        internal A151(bool b1, PdfField[] b0) : base(A128.A86)
        {
            this._b1 = b1;
            this._b0 = b0;
        }

        internal override PdfField[] A138
        {
            get
            {
                return this._b0;
            }
        }

        internal override bool A140
        {
            get
            {
                return this._b1;
            }
        }
    }
}

