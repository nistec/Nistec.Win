namespace MControl.Printing.Pdf.Core.Text
{
    using System;

    internal class A45 : A566
    {
        private float _b0;

        internal A45(A523 b1, float b0) : base(b1)
        {
            this._b0 = b0;
        }

        internal override float A211
        {
            get
            {
                return this._b0;
            }
        }

        internal override bool A575
        {
            get
            {
                return true;
            }
        }

        internal override float A576
        {
            set
            {
                this._b0 += value;
            }
        }
    }
}

