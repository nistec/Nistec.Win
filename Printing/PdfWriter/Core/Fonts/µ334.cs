namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A334 : A51
    {
        private ushort _b0;
        private short _b1;
        private byte[] _b10;
        private byte[] _b11;
        private short _b12;
        private short _b2;
        private short _b3;
        private short _b4;
        private short _b5;
        private short _b6;
        private short _b7;
        private short _b8;
        private ushort _b9;

        internal A334(A281 b13) : base(b13)
        {
        }

        protected override void A283(A51 b14, A284 b15)
        {
            A334 A = b14 as A334;
            this._b11 = new byte[A._b11.Length];
            A._b11.CopyTo(this._b11, 0);
            this._b1 = A._b1;
            this._b4 = A._b4;
            this._b5 = A._b5;
            this._b0 = A._b0;
            this._b7 = A._b7;
            this._b8 = A._b8;
            this._b12 = A._b12;
            this._b2 = A._b2;
            this._b3 = A._b3;
            this._b10 = new byte[A._b10.Length];
            A._b10.CopyTo(this._b10, 0);
            this._b6 = A._b6;
            this._b9 = A._b9;
        }

        protected override void A285(A286 b16)
        {
            this._b11 = b16.A287(4);
            this._b1 = b16.A305();
            this._b4 = b16.A305();
            this._b5 = b16.A305();
            this._b0 = b16.A302();
            this._b7 = b16.A305();
            this._b8 = b16.A305();
            this._b12 = b16.A305();
            this._b2 = b16.A305();
            this._b3 = b16.A305();
            this._b10 = b16.A287(10);
            this._b6 = b16.A305();
            this._b9 = b16.A302();
        }

        protected override void A289(A290 b16)
        {
            b16.A291(this._b11);
            b16.A363(this._b1);
            b16.A363(this._b4);
            b16.A363(this._b5);
            b16.A297(this._b0);
            b16.A363(this._b7);
            b16.A363(this._b8);
            b16.A363(this._b12);
            b16.A363(this._b2);
            b16.A363(this._b3);
            b16.A291(this._b10);
            b16.A363(this._b6);
            b16.A297(this._b9);
        }

        protected internal override string A226
        {
            get
            {
                return "hhea";
            }
        }

        internal static int A316
        {
            get
            {
                return 0x24;
            }
        }

        internal short A366
        {
            get
            {
                return this._b1;
            }
        }

        internal short A367
        {
            get
            {
                return this._b4;
            }
        }

        internal int A368
        {
            get
            {
                return this._b9;
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

