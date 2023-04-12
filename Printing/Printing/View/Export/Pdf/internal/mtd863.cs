namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd863 : mtd708
    {
        private ushort _var0;
        private short _var1;
        private byte[] _var10;
        private byte[] _var11;
        private short _var12;
        private short _var2;
        private short _var3;
        private short _var4;
        private short _var5;
        private short _var6;
        private short _var7;
        private short _var8;
        private ushort _var9;

        internal mtd863(mtd821 var13) : base(var13)
        {
        }

        protected override void mtd822(mtd708 var14, mtd823 var15)
        {
            mtd863 mtd = var14 as mtd863;
            this._var11 = new byte[mtd._var11.Length];
            mtd._var11.CopyTo(this._var11, 0);
            this._var1 = mtd._var1;
            this._var4 = mtd._var4;
            this._var5 = mtd._var5;
            this._var0 = mtd._var0;
            this._var7 = mtd._var7;
            this._var8 = mtd._var8;
            this._var12 = mtd._var12;
            this._var2 = mtd._var2;
            this._var3 = mtd._var3;
            this._var10 = new byte[mtd._var10.Length];
            mtd._var10.CopyTo(this._var10, 0);
            this._var6 = mtd._var6;
            this._var9 = mtd._var9;
        }

        protected override void mtd824(mtd825 var16)
        {
            this._var11 = var16.mtd826(4);
            this._var1 = var16.mtd892();
            this._var4 = var16.mtd892();
            this._var5 = var16.mtd892();
            this._var0 = var16.mtd835();
            this._var7 = var16.mtd892();
            this._var8 = var16.mtd892();
            this._var12 = var16.mtd892();
            this._var2 = var16.mtd892();
            this._var3 = var16.mtd892();
            this._var10 = var16.mtd826(10);
            this._var6 = var16.mtd892();
            this._var9 = var16.mtd835();
        }

        protected override void mtd828(mtd829 var16)
        {
            var16.mtd830(this._var11);
            var16.mtd893(this._var1);
            var16.mtd893(this._var4);
            var16.mtd893(this._var5);
            var16.mtd838(this._var0);
            var16.mtd893(this._var7);
            var16.mtd893(this._var8);
            var16.mtd893(this._var12);
            var16.mtd893(this._var2);
            var16.mtd893(this._var3);
            var16.mtd830(this._var10);
            var16.mtd893(this._var6);
            var16.mtd838(this._var9);
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
                return "hhea";
            }
        }

        internal static int mtd845
        {
            get
            {
                return 0x24;
            }
        }

        internal short mtd896
        {
            get
            {
                return this._var1;
            }
        }

        internal short mtd897
        {
            get
            {
                return this._var4;
            }
        }

        internal int mtd898
        {
            get
            {
                return this._var9;
            }
        }
    }
}

