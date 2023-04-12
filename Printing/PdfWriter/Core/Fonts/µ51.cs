namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal abstract class A51
    {
        private A314 _b0;
        private MControl.Printing.Pdf.Core.Fonts.A281 _b1;

        internal A51(MControl.Printing.Pdf.Core.Fonts.A281 b1)
        {
            this._b1 = b1;
        }

        internal void A237(A51 b2)
        {
            this.A237(b2, null);
        }

        internal void A237(A51 b3, A284 b4)
        {
            if ((b3 != null) && base.GetType().Equals(b3.GetType()))
            {
                this.A283(b3, b4);
                if (this.A288 == null)
                {
                    this.A281.A345.A317(this);
                }
            }
        }

        protected virtual void A283(A51 b2, A284 b5)
        {
        }

        protected virtual void A285(A286 b6)
        {
        }

        protected virtual void A289(A290 b6)
        {
        }

        internal void A312(A286 b6)
        {
            if (this.A288 == null)
            {
                throw new Exception("The required Font table not found in font file");
            }
            b6.A300(this.A288.A85);
            this.A285(b6);
        }

        internal void A54(A290 b6)
        {
            b6.A238();
            if (this.A288 != null)
            {
                this.A288.A321(b6.A298);
            }
            this.A289(b6);
        }

        protected internal abstract string A226 { get; }

        internal MControl.Printing.Pdf.Core.Fonts.A281 A281
        {
            get
            {
                return this._b1;
            }
        }

        protected internal A314 A288
        {
            get
            {
                if (this._b0 == null)
                {
                    this._b0 = this.A281.A345[this.A226];
                }
                return this._b0;
            }
        }

        internal virtual int A84
        {
            get
            {
                return 0;
            }
        }
    }
}

