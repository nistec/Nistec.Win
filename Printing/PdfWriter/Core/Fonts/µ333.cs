namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A333 : A51
    {
        private byte[] _b0;
        private byte[] _b1;
        private short _b10;
        private short _b11;
        private ushort _b12;
        private ushort _b13;
        private short _b14;
        private short _b15;
        private short _b16;
        private uint _b2;
        private uint _b3;
        private ushort _b4;
        private ushort _b5;
        private byte[] _b6;
        private byte[] _b7;
        private short _b8;
        private short _b9;

        internal A333(A281 b17) : base(b17)
        {
        }

        protected override void A283(A51 b18, A284 b19)
        {
            A333 A = b18 as A333;
            this._b0 = new byte[A._b0.Length];
            A._b0.CopyTo(this._b0, 0);
            this._b1 = new byte[A._b1.Length];
            A._b1.CopyTo(this._b1, 0);
            this._b2 = 0;
            this._b3 = A._b3;
            this._b4 = A._b4;
            this._b5 = A.A365;
            this._b6 = new byte[A._b6.Length];
            A._b6.CopyTo(this._b6, 0);
            this._b7 = new byte[A._b7.Length];
            A._b7.CopyTo(this._b7, 0);
            this._b9 = A.A360;
            this._b11 = A.A362;
            this._b8 = A.A359;
            this._b10 = A.A361;
            this._b12 = A._b12;
            this._b13 = A._b13;
            this._b14 = A._b14;
            this._b15 = A._b15;
            this._b16 = A._b16;
        }

        protected override void A285(A286 b20)
        {
            this._b0 = b20.A287(4);
            this._b1 = b20.A287(4);
            this._b2 = b20.A303();
            this._b3 = b20.A303();
            this._b4 = b20.A302();
            this._b5 = b20.A302();
            this._b6 = b20.A287(8);
            this._b7 = b20.A287(8);
            this._b9 = b20.A305();
            this._b11 = b20.A305();
            this._b8 = b20.A305();
            this._b10 = b20.A305();
            this._b12 = b20.A302();
            this._b13 = b20.A302();
            this._b14 = b20.A305();
            this._b15 = b20.A305();
            this._b16 = b20.A305();
        }

        protected override void A289(A290 b20)
        {
            b20.A291(this._b0);
            b20.A291(this._b1);
            b20.A299(this._b2);
            b20.A299(this._b3);
            b20.A297(this._b4);
            b20.A297(this._b5);
            b20.A291(this._b6);
            b20.A291(this._b7);
            b20.A363(this._b9);
            b20.A363(this._b11);
            b20.A363(this._b8);
            b20.A363(this._b10);
            b20.A297(this._b12);
            b20.A297(this._b13);
            b20.A363(this._b14);
            b20.A363(this._b15);
            b20.A363(this._b16);
        }

        internal void A355(A290 b20)
        {
            if (base.A288 != null)
            {
                b20.A300(base.A288.A85);
                b20.A301(8);
                if (this._b2 == 0)
                {
                    this._b2 = b20.A322(0, b20.A84);
                    this._b2 = 0xb1b0afba - this._b2;
                }
                b20.A299(this._b2);
            }
        }

        protected internal override string A226
        {
            get
            {
                return "head";
            }
        }

        internal static int A316
        {
            get
            {
                return 0x36;
            }
        }

        internal short A359
        {
            get
            {
                return this._b8;
            }
        }

        internal short A360
        {
            get
            {
                return this._b9;
            }
        }

        internal short A361
        {
            get
            {
                return this._b10;
            }
        }

        internal short A362
        {
            get
            {
                return this._b11;
            }
        }

        internal short A364
        {
            get
            {
                return this._b15;
            }
        }

        internal ushort A365
        {
            get
            {
                return this._b5;
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

