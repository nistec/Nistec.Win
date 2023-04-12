namespace MControl.Printing.Pdf.Core.Drawing
{
    using System;
    using MControl.Printing.Pdf.Core;

    internal class A475 : A477
    {
        private string _b0;

        internal A475(string b0) : base(A47.A38)
        {
            this._b0 = b0;
        }

        internal override string A38
        {
            get
            {
                return this._b0;
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

