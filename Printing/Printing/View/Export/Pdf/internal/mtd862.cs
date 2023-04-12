namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd862 : mtd708
    {
        private byte[] _var0;
        private byte[] _var1;
        private short _var10;
        private short _var11;
        private ushort _var12;
        private ushort _var13;
        private short _var14;
        private short _var15;
        private short _var16;
        private uint _var2;
        private uint _var3;
        private ushort _var4;
        private ushort _var5;
        private byte[] _var6;
        private byte[] _var7;
        private short _var8;
        private short _var9;

        internal mtd862(mtd821 var17) : base(var17)
        {
        }

        protected override void mtd822(mtd708 var18, mtd823 var19)
        {
            mtd862 mtd = var18 as mtd862;
            this._var0 = new byte[mtd._var0.Length];
            mtd._var0.CopyTo(this._var0, 0);
            this._var1 = new byte[mtd._var1.Length];
            mtd._var1.CopyTo(this._var1, 0);
            this._var2 = 0;
            this._var3 = mtd._var3;
            this._var4 = mtd._var4;
            this._var5 = mtd.mtd895;
            this._var6 = new byte[mtd._var6.Length];
            mtd._var6.CopyTo(this._var6, 0);
            this._var7 = new byte[mtd._var7.Length];
            mtd._var7.CopyTo(this._var7, 0);
            this._var9 = mtd.mtd889;
            this._var11 = mtd.mtd891;
            this._var8 = mtd.mtd888;
            this._var10 = mtd.mtd890;
            this._var12 = mtd._var12;
            this._var13 = mtd._var13;
            this._var14 = mtd._var14;
            this._var15 = mtd._var15;
            this._var16 = mtd._var16;
        }

        protected override void mtd824(mtd825 var20)
        {
            this._var0 = var20.mtd826(4);
            this._var1 = var20.mtd826(4);
            this._var2 = var20.mtd837();
            this._var3 = var20.mtd837();
            this._var4 = var20.mtd835();
            this._var5 = var20.mtd835();
            this._var6 = var20.mtd826(8);
            this._var7 = var20.mtd826(8);
            this._var9 = var20.mtd892();
            this._var11 = var20.mtd892();
            this._var8 = var20.mtd892();
            this._var10 = var20.mtd892();
            this._var12 = var20.mtd835();
            this._var13 = var20.mtd835();
            this._var14 = var20.mtd892();
            this._var15 = var20.mtd892();
            this._var16 = var20.mtd892();
        }

        protected override void mtd828(mtd829 var20)
        {
            var20.mtd830(this._var0);
            var20.mtd830(this._var1);
            var20.mtd839(this._var2);
            var20.mtd839(this._var3);
            var20.mtd838(this._var4);
            var20.mtd838(this._var5);
            var20.mtd830(this._var6);
            var20.mtd830(this._var7);
            var20.mtd893(this._var9);
            var20.mtd893(this._var11);
            var20.mtd893(this._var8);
            var20.mtd893(this._var10);
            var20.mtd838(this._var12);
            var20.mtd838(this._var13);
            var20.mtd893(this._var14);
            var20.mtd893(this._var15);
            var20.mtd893(this._var16);
        }

        internal void mtd884(mtd829 var20)
        {
            if (base.mtd827 != null)
            {
                var20.mtd833(base.mtd827.mtd737);
                var20.mtd834(8);
                if (this._var2 == 0)
                {
                    this._var2 = var20.mtd851(0, var20.mtd736);
                    this._var2 = 0xb1b0afba - this._var2;
                }
                var20.mtd839(this._var2);
            }
        }

        internal override int mtd736
        {
            get
            {
                return mtd845;
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "head";
            }
        }

        internal static int mtd845
        {
            get
            {
                return 0x36;
            }
        }

        internal short mtd888
        {
            get
            {
                return this._var8;
            }
        }

        internal short mtd889
        {
            get
            {
                return this._var9;
            }
        }

        internal short mtd890
        {
            get
            {
                return this._var10;
            }
        }

        internal short mtd891
        {
            get
            {
                return this._var11;
            }
        }

        internal short mtd894
        {
            get
            {
                return this._var15;
            }
        }

        internal ushort mtd895
        {
            get
            {
                return this._var5;
            }
        }
    }
}

