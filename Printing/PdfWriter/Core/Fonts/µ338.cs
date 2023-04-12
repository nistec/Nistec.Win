namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A338 : A51
    {
        private sbyte[] _b0;
        private ushort _b1;
        private uint _b10;
        private uint _b11;
        private uint _b12;
        private uint _b13;
        private ushort _b14;
        private ushort _b15;
        private ushort _b16;
        private ushort _b17;
        private ushort _b18;
        private ushort _b19;
        private short _b2;
        private ushort _b20;
        private short _b21;
        private short _b22;
        private short _b23;
        private short _b24;
        private short _b25;
        private short _b26;
        private short _b27;
        private short _b28;
        private short _b29;
        private MControl.Printing.Pdf.Core.Fonts.A373 _b3;
        private short _b30;
        private short _b31;
        private short _b4;
        private ushort _b5;
        private ushort _b6;
        private ushort _b7;
        private uint _b8;
        private uint _b9;

        internal A338(A281 b32) : base(b32)
        {
            this._b0 = new sbyte[4];
        }

        protected override void A283(A51 b33, A284 b34)
        {
            A338 A = b33 as A338;
            this._b20 = A._b20;
            this._b21 = A._b21;
            this._b16 = A._b16;
            this._b17 = A._b17;
            this._b2 = A._b2;
            this._b25 = A._b25;
            this._b27 = A._b27;
            this._b24 = A._b24;
            this._b26 = A._b26;
            this._b29 = A._b29;
            this._b31 = A._b31;
            this._b28 = A._b28;
            this._b30 = A._b30;
            this._b23 = A._b23;
            this._b22 = A._b22;
            this._b4 = A._b4;
            this._b3 = A._b3;
            this._b10 = A._b10;
            this._b11 = A._b11;
            this._b12 = A._b12;
            this._b13 = A._b13;
            this._b0[0] = A._b0[0];
            this._b0[1] = A._b0[1];
            this._b0[2] = A._b0[2];
            this._b0[3] = A._b0[3];
            this._b1 = A._b1;
            this._b14 = A._b14;
            this._b15 = A._b15;
            this._b5 = A._b5;
            this._b6 = A._b6;
            this._b7 = A._b7;
            this._b18 = A._b18;
            this._b19 = A._b19;
            this._b8 = A._b8;
            this._b9 = A._b9;
        }

        protected override void A285(A286 b35)
        {
            this._b20 = b35.A302();
            this._b21 = b35.A305();
            this._b16 = b35.A302();
            this._b17 = b35.A302();
            this._b2 = b35.A305();
            this._b25 = b35.A305();
            this._b27 = b35.A305();
            this._b24 = b35.A305();
            this._b26 = b35.A305();
            this._b29 = b35.A305();
            this._b31 = b35.A305();
            this._b28 = b35.A305();
            this._b30 = b35.A305();
            this._b23 = b35.A305();
            this._b22 = b35.A305();
            this._b4 = b35.A305();
            this._b3 = b35.A374();
            this._b10 = b35.A303();
            this._b11 = b35.A303();
            this._b12 = b35.A303();
            this._b13 = b35.A303();
            this._b0[0] = b35.A371();
            this._b0[1] = b35.A371();
            this._b0[2] = b35.A371();
            this._b0[3] = b35.A371();
            this._b1 = b35.A302();
            this._b14 = b35.A302();
            this._b15 = b35.A302();
            this._b5 = b35.A302();
            this._b6 = b35.A302();
            this._b7 = b35.A302();
            this._b18 = b35.A302();
            this._b19 = b35.A302();
            this._b8 = b35.A303();
            this._b9 = b35.A303();
        }

        protected override void A289(A290 b35)
        {
            b35.A297(this._b20);
            b35.A363(this._b21);
            b35.A297(this._b16);
            b35.A297(this._b17);
            b35.A363(this._b2);
            b35.A363(this._b25);
            b35.A363(this._b27);
            b35.A363(this._b24);
            b35.A363(this._b26);
            b35.A363(this._b29);
            b35.A363(this._b31);
            b35.A363(this._b28);
            b35.A363(this._b30);
            b35.A363(this._b23);
            b35.A363(this._b22);
            b35.A363(this._b4);
            b35.A386(this._b3);
            b35.A299(this._b10);
            b35.A299(this._b11);
            b35.A299(this._b12);
            b35.A299(this._b13);
            b35.A387(this._b0[0]);
            b35.A387(this._b0[1]);
            b35.A387(this._b0[2]);
            b35.A387(this._b0[3]);
            b35.A297(this._b1);
            b35.A297(this._b14);
            b35.A297(this._b15);
            b35.A297(this._b5);
            b35.A297(this._b6);
            b35.A297(this._b7);
            b35.A297(this._b18);
            b35.A297(this._b19);
            b35.A299(this._b8);
            b35.A299(this._b9);
        }

        protected internal override string A226
        {
            get
            {
                return "OS/2";
            }
        }

        internal static int A316
        {
            get
            {
                return 0x56;
            }
        }

        internal short A343
        {
            get
            {
                return this._b2;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A373 A373
        {
            get
            {
                return this._b3;
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

