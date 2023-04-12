namespace MControl.Printing.Pdf.Core.Controls
{
    using System;

    internal class A150 : A136
    {
        private string _b0;

        internal A150(string b0) : base(A128.A134)
        {
            this._b0 = b0;
        }

        internal override string A145
        {
            get
            {
                return this._b0;
            }
        }
    }
}

