namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd866 : mtd708
    {
        private byte[] _var0;
        private ushort _var1;
        private ushort _var10;
        private ushort _var11;
        private ushort _var12;
        private ushort _var13;
        private ushort _var14;
        private ushort _var2;
        private ushort _var3;
        private ushort _var4;
        private ushort _var5;
        private ushort _var6;
        private ushort _var7;
        private ushort _var8;
        private ushort _var9;

        internal mtd866(mtd821 var15) : base(var15)
        {
        }

        protected override void mtd822(mtd708 var16, mtd823 var17)
        {
            mtd866 mtd = var16 as mtd866;
            this._var0 = new byte[mtd._var0.Length];
            mtd._var0.CopyTo(this._var0, 0);
            this._var1 = mtd._var1;
            this._var2 = mtd._var2;
            this._var3 = mtd._var3;
            this._var4 = mtd._var4;
            this._var5 = mtd._var5;
            this._var6 = mtd._var6;
            this._var7 = mtd._var7;
            this._var8 = mtd._var8;
            this._var9 = mtd._var9;
            this._var10 = mtd._var10;
            this._var11 = mtd._var11;
            this._var12 = mtd._var12;
            this._var13 = mtd._var13;
            this._var14 = mtd._var14;
        }

        protected override void mtd824(mtd825 var18)
        {
            this._var0 = var18.mtd826(4);
            this._var1 = var18.mtd835();
            this._var2 = var18.mtd835();
            this._var3 = var18.mtd835();
            this._var4 = var18.mtd835();
            this._var5 = var18.mtd835();
            this._var6 = var18.mtd835();
            this._var7 = var18.mtd835();
            this._var8 = var18.mtd835();
            this._var9 = var18.mtd835();
            this._var10 = var18.mtd835();
            this._var11 = var18.mtd835();
            this._var12 = var18.mtd835();
            this._var13 = var18.mtd835();
            this._var14 = var18.mtd835();
        }

        protected override void mtd828(mtd829 var18)
        {
            var18.mtd830(this._var0);
            var18.mtd838(this._var1);
            var18.mtd838(this._var2);
            var18.mtd838(this._var3);
            var18.mtd838(this._var4);
            var18.mtd838(this._var5);
            var18.mtd838(this._var6);
            var18.mtd838(this._var7);
            var18.mtd838(this._var8);
            var18.mtd838(this._var9);
            var18.mtd838(this._var10);
            var18.mtd838(this._var11);
            var18.mtd838(this._var12);
            var18.mtd838(this._var13);
            var18.mtd838(this._var14);
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
                return "maxp";
            }
        }

        internal static int mtd845
        {
            get
            {
                return 0x20;
            }
        }

        internal int mtd887
        {
            get
            {
                return Convert.ToInt32(this._var1);
            }
        }
    }
}

