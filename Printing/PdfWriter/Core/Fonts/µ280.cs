namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A280 : A51
    {
        private byte[] _b0;
        private string _b1;
        private bool _b2;

        internal A280(A281 b3, string b1) : base(b3)
        {
            this._b1 = b1;
            this._b2 = false;
        }

        protected override void A283(A51 b4, A284 b5)
        {
            if (!this._b2)
            {
                A280 A = b4 as A280;
                this._b1 = A.A282;
                this._b0 = new byte[A.A221.Length];
                A.A221.CopyTo(this._b0, 0);
            }
        }

        protected override void A285(A286 b6)
        {
            try
            {
                this._b0 = b6.A287(base.A288.A84);
            }
            catch
            {
                this._b2 = true;
            }
        }

        protected override void A289(A290 b6)
        {
            if (!this._b2)
            {
                b6.A291(this._b0);
            }
        }

        internal byte[] A221
        {
            get
            {
                return this._b0;
            }
        }

        protected internal override string A226
        {
            get
            {
                return this._b1;
            }
        }

        internal string A282
        {
            get
            {
                return this._b1;
            }
        }

        internal override int A84
        {
            get
            {
                return this._b0.Length;
            }
        }
    }
}

