namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Reflection;

    internal class A335 : A51
    {
        private A352[] _b0;

        internal A335(A281 b1) : base(b1)
        {
        }

        protected override void A283(A51 b3, A284 b4)
        {
            A335 A = b3 as A335;
            this._b0 = new A352[A._b0.Length];
            A._b0.CopyTo(this._b0, 0);
        }

        protected override void A285(A286 b5)
        {
            int num = Math.Max(base.A281.A337.A358, base.A281.A334.A368);
            this._b0 = new A352[num];
            for (int i = 0; i < base.A281.A334.A368; i++)
            {
                this._b0[i].A353 = b5.A302();
                this._b0[i].A369 = b5.A305();
            }
            ushort num3 = this._b0[base.A281.A334.A368 - 1].A353;
            for (int j = base.A281.A334.A368; j < num; j++)
            {
                this._b0[j].A353 = num3;
                this._b0[j].A369 = b5.A305();
            }
        }

        protected override void A289(A290 b5)
        {
            int num = Math.Max(base.A281.A337.A358, base.A281.A334.A368);
            for (int i = 0; i < base.A281.A334.A368; i++)
            {
                b5.A297(this._b0[i].A353);
                b5.A363(this._b0[i].A369);
            }
            for (int j = base.A281.A334.A368; j < num; j++)
            {
                b5.A363(this._b0[j].A369);
            }
        }

        protected internal override string A226
        {
            get
            {
                return "hmtx";
            }
        }

        internal int A293
        {
            get
            {
                return this._b0.Length;
            }
        }

        internal override int A84
        {
            get
            {
                return (A352.A316 * this.A293);
            }
        }

        internal A352 this[ushort b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

