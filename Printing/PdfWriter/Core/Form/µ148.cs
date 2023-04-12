namespace MControl.Printing.Pdf.Core.Controls
{
    using MControl.Printing.Pdf;
    using System;

    internal class A148 : A136
    {
        private Page _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        internal A148(Page b0, float b1, float b2, float b3) : base(A128.A131)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
        }

        internal override Page A141
        {
            get
            {
                return this._b0;
            }
        }

        internal override float A142
        {
            get
            {
                return this._b1;
            }
        }

        internal override float A143
        {
            get
            {
                return this._b2;
            }
        }

        internal override float A144
        {
            get
            {
                return this._b3;
            }
        }
    }
}

