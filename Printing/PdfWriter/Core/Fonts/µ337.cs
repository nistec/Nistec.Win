namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A337 : A51
    {
        private byte[] _b0;
        private ushort _b1;
        private ushort _b10;
        private ushort _b11;
        private ushort _b12;
        private ushort _b13;
        private ushort _b14;
        private ushort _b2;
        private ushort _b3;
        private ushort _b4;
        private ushort _b5;
        private ushort _b6;
        private ushort _b7;
        private ushort _b8;
        private ushort _b9;

        internal A337(A281 b15) : base(b15)
        {
        }

        protected override void A283(A51 b16, A284 b17)
        {
            A337 A = b16 as A337;
            this._b0 = new byte[A._b0.Length];
            A._b0.CopyTo(this._b0, 0);
            this._b1 = A._b1;
            this._b2 = A._b2;
            this._b3 = A._b3;
            this._b4 = A._b4;
            this._b5 = A._b5;
            this._b6 = A._b6;
            this._b7 = A._b7;
            this._b8 = A._b8;
            this._b9 = A._b9;
            this._b10 = A._b10;
            this._b11 = A._b11;
            this._b12 = A._b12;
            this._b13 = A._b13;
            this._b14 = A._b14;
        }

        protected override void A285(A286 b18)
        {
            this._b0 = b18.A287(4);
            this._b1 = b18.A302();
            this._b2 = b18.A302();
            this._b3 = b18.A302();
            this._b4 = b18.A302();
            this._b5 = b18.A302();
            this._b6 = b18.A302();
            this._b7 = b18.A302();
            this._b8 = b18.A302();
            this._b9 = b18.A302();
            this._b10 = b18.A302();
            this._b11 = b18.A302();
            this._b12 = b18.A302();
            this._b13 = b18.A302();
            this._b14 = b18.A302();
        }

        protected override void A289(A290 b18)
        {
            b18.A291(this._b0);
            b18.A297(this._b1);
            b18.A297(this._b2);
            b18.A297(this._b3);
            b18.A297(this._b4);
            b18.A297(this._b5);
            b18.A297(this._b6);
            b18.A297(this._b7);
            b18.A297(this._b8);
            b18.A297(this._b9);
            b18.A297(this._b10);
            b18.A297(this._b11);
            b18.A297(this._b12);
            b18.A297(this._b13);
            b18.A297(this._b14);
        }

        protected internal override string A226
        {
            get
            {
                return "maxp";
            }
        }

        internal static int A316
        {
            get
            {
                return 0x20;
            }
        }

        internal int A358
        {
            get
            {
                return Convert.ToInt32(this._b1);
            }
        }

        internal override int A84
        {
            get
            {
                return A316;
            }
        }
    }
}

