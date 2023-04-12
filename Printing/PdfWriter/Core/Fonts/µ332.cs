namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class A332 : A51
    {
        private A347[] _b0;
        private ArrayList _b1;

        internal A332(A281 b2) : base(b2)
        {
            this._b1 = new ArrayList();
        }

        protected override void A283(A51 b6, A284 b7)
        {
            A332 A = b6 as A332;
            this._b0 = new A347[A.A348.Length];
            this._b1.Clear();
            for (int i = 0; i < b7.A356.Length; i++)
            {
                this.b4(b7.A356[i], A.A348[b7.A356[i]]);
            }
        }

        protected override void A285(A286 b8)
        {
            this._b0 = new A347[base.A281.A337.A358];
            this._b1.Clear();
            int num = b8.A298;
            for (int i = 0; i < base.A281.A337.A358; i++)
            {
                uint num3 = base.A281.A336[(ushort) i];
                uint num4 = base.A281.A336[(ushort) (i + 1)];
                if (num4 != num3)
                {
                    this.b4(i, new A347((int) (num4 - num3)));
                    b8.A300(num);
                    b8.A301((int) num3);
                    this._b0[i].A312(b8);
                }
            }
        }

        protected override void A289(A290 b8)
        {
            for (int i = 0; i < this.A293; i++)
            {
                this[i].A54(b8);
            }
        }

        private void b4(int b3, A347 b5)
        {
            this._b0[b3] = b5;
            if (b5 != null)
            {
                this._b1.Add(b5);
            }
        }

        protected internal override string A226
        {
            get
            {
                return "glyf";
            }
        }

        internal int A293
        {
            get
            {
                return this._b1.Count;
            }
        }

        internal A347[] A348
        {
            get
            {
                return this._b0;
            }
        }

        internal override int A84
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.A293; i++)
                {
                    num += this[i].A2;
                }
                return num;
            }
        }

        internal A347 this[int b3]
        {
            get
            {
                return (this._b1[b3] as A347);
            }
        }
    }
}

