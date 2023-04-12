namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf.Drawing;
    using System;

    internal class A498 : A523
    {
        private string _b0;
        private StyleBase _b1;

        internal A498(string b0, StyleBase b1)
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

        internal override StyleBase A405
        {
            get
            {
                return this._b1;
            }
        }
    }
}

