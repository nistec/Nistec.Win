namespace MControl.Printing.Pdf.Core.Drawing
{
    using System;
    using MControl.Printing.Pdf.Core;

    internal class A476 : A477
    {
        private string _b0;
        private string _b1;

        internal A476(string b0, string b1) : base(A47.A48)
        {
            this._b0 = b0;
            this._b1 = b1;
        }

        internal override string A38
        {
            get
            {
                return this._b0;
            }
        }

        internal override string A48
        {
            get
            {
                return this._b1;
            }
        }

        internal override object A480
        {
            get
            {
                return this._b0;
            }
        }
    }
}

