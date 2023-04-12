namespace MControl.Printing.Pdf.Core.Text
{
    using System;

    internal class A590 : A526
    {
        private float _b0;

        internal A590(A568 b1) : base(b1)
        {
            this._b0 = b1.A527;
        }

        internal override float A527
        {
            get
            {
                return this._b0;
            }
        }
    }
}

