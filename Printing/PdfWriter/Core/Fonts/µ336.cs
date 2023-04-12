namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Reflection;

    internal class A336 : A51
    {
        private uint[] _b0;

        internal A336(A281 b1) : base(b1)
        {
        }

        protected override void A283(A51 b3, A284 b4)
        {
            A336 A = b3 as A336;
            this._b0 = new uint[A.A293];
            uint num = 0;
            for (int i = 0; i < base.A281.A332.A348.Length; i++)
            {
                this._b0[i] = num;
                if (base.A281.A332.A348[i] != null)
                {
                    num += (uint) base.A281.A332.A348[i].A2;
                }
            }
            this._b0[base.A281.A332.A348.Length] = num;
        }

        protected override void A285(A286 b5)
        {
            this._b0 = new uint[base.A281.A337.A358 + 1];
            for (int i = 0; i < (base.A281.A337.A358 + 1); i++)
            {
                this._b0[i] = (base.A281.A333.A364 == 1) ? b5.A303() : (Convert.ToUInt32(b5.A302()) << 1);
            }
        }

        protected override void A289(A290 b5)
        {
            for (int i = 0; i < (base.A281.A337.A358 + 1); i++)
            {
                if (base.A281.A333.A364 == 1)
                {
                    b5.A299(this._b0[i]);
                }
                else
                {
                    b5.A297(Convert.ToUInt16((uint) (this._b0[i] >> 1)));
                }
            }
        }

        protected internal override string A226
        {
            get
            {
                return "loca";
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
                int num = (base.A281.A333.A364 == 1) ? 4 : 2;
                return (num * this.A293);
            }
        }

        internal uint this[ushort b2]
        {
            get
            {
                return this._b0[b2];
            }
        }
    }
}

