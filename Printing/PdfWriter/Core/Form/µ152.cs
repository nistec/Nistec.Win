namespace MControl.Printing.Pdf.Core.Controls
{
    using System;
    using MControl.Printing.Pdf.Controls;

    internal class A152 : A136
    {
        private PdfField[] _b0;
        private FormSubmitFlags _b1;
        private string _b2;

        internal A152(string b2, FormSubmitFlags b3, PdfField[] b0) : base(A128.A129)
        {
            this._b2 = b2;
            this._b1 = b3;
            this._b0 = b0;
        }

        internal override PdfField[] A138
        {
            get
            {
                return this._b0;
            }
        }

        internal override FormSubmitFlags A139
        {
            get
            {
                return this._b1;
            }
        }

        internal override string A48
        {
            get
            {
                return this._b2;
            }
        }
    }
}

