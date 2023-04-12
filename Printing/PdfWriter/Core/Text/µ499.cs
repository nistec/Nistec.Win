namespace MControl.Printing.Pdf.Core.Text
{
    using MControl.Printing.Pdf.Drawing;
    using System;

    internal class A499 : A498
    {
        private string _b0;

        internal A499(string b1, StyleBase b2, string b0) : base(b1, b2)
        {
            this._b0 = b0;
        }

        internal override string A585
        {
            get
            {
                return this._b0;
            }
        }
    }
}

