namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd867 : mtd708
    {
        private sbyte[] _var0;
        private ushort _var1;
        private uint _var10;
        private uint _var11;
        private uint _var12;
        private uint _var13;
        private ushort _var14;
        private ushort _var15;
        private ushort _var16;
        private ushort _var17;
        private ushort _var18;
        private ushort _var19;
        private short _var2;
        private ushort _var20;
        private short _var21;
        private short _var22;
        private short _var23;
        private short _var24;
        private short _var25;
        private short _var26;
        private short _var27;
        private short _var28;
        private short _var29;
        private Nistec.Printing.View.Pdf.mtd903 _var3;
        private short _var30;
        private short _var31;
        private short _var4;
        private ushort _var5;
        private ushort _var6;
        private ushort _var7;
        private uint _var8;
        private uint _var9;

        internal mtd867(mtd821 var32) : base(var32)
        {
            this._var0 = new sbyte[4];
        }

        protected override void mtd822(mtd708 var33, mtd823 var34)
        {
            mtd867 mtd = var33 as mtd867;
            this._var20 = mtd._var20;
            this._var21 = mtd._var21;
            this._var16 = mtd._var16;
            this._var17 = mtd._var17;
            this._var2 = mtd._var2;
            this._var25 = mtd._var25;
            this._var27 = mtd._var27;
            this._var24 = mtd._var24;
            this._var26 = mtd._var26;
            this._var29 = mtd._var29;
            this._var31 = mtd._var31;
            this._var28 = mtd._var28;
            this._var30 = mtd._var30;
            this._var23 = mtd._var23;
            this._var22 = mtd._var22;
            this._var4 = mtd._var4;
            this._var3 = mtd._var3;
            this._var10 = mtd._var10;
            this._var11 = mtd._var11;
            this._var12 = mtd._var12;
            this._var13 = mtd._var13;
            this._var0[0] = mtd._var0[0];
            this._var0[1] = mtd._var0[1];
            this._var0[2] = mtd._var0[2];
            this._var0[3] = mtd._var0[3];
            this._var1 = mtd._var1;
            this._var14 = mtd._var14;
            this._var15 = mtd._var15;
            this._var5 = mtd._var5;
            this._var6 = mtd._var6;
            this._var7 = mtd._var7;
            this._var18 = mtd._var18;
            this._var19 = mtd._var19;
            this._var8 = mtd._var8;
            this._var9 = mtd._var9;
        }

        protected override void mtd824(mtd825 var35)
        {
            this._var20 = var35.mtd835();
            this._var21 = var35.mtd892();
            this._var16 = var35.mtd835();
            this._var17 = var35.mtd835();
            this._var2 = var35.mtd892();
            this._var25 = var35.mtd892();
            this._var27 = var35.mtd892();
            this._var24 = var35.mtd892();
            this._var26 = var35.mtd892();
            this._var29 = var35.mtd892();
            this._var31 = var35.mtd892();
            this._var28 = var35.mtd892();
            this._var30 = var35.mtd892();
            this._var23 = var35.mtd892();
            this._var22 = var35.mtd892();
            this._var4 = var35.mtd892();
            this._var3 = var35.mtd904();
            this._var10 = var35.mtd837();
            this._var11 = var35.mtd837();
            this._var12 = var35.mtd837();
            this._var13 = var35.mtd837();
            this._var0[0] = var35.mtd901();
            this._var0[1] = var35.mtd901();
            this._var0[2] = var35.mtd901();
            this._var0[3] = var35.mtd901();
            this._var1 = var35.mtd835();
            this._var14 = var35.mtd835();
            this._var15 = var35.mtd835();
            this._var5 = var35.mtd835();
            this._var6 = var35.mtd835();
            this._var7 = var35.mtd835();
            this._var18 = var35.mtd835();
            this._var19 = var35.mtd835();
            this._var8 = var35.mtd837();
            this._var9 = var35.mtd837();
        }

        protected override void mtd828(mtd829 var35)
        {
            var35.mtd838(this._var20);
            var35.mtd893(this._var21);
            var35.mtd838(this._var16);
            var35.mtd838(this._var17);
            var35.mtd893(this._var2);
            var35.mtd893(this._var25);
            var35.mtd893(this._var27);
            var35.mtd893(this._var24);
            var35.mtd893(this._var26);
            var35.mtd893(this._var29);
            var35.mtd893(this._var31);
            var35.mtd893(this._var28);
            var35.mtd893(this._var30);
            var35.mtd893(this._var23);
            var35.mtd893(this._var22);
            var35.mtd893(this._var4);
            var35.mtd923(this._var3);
            var35.mtd839(this._var10);
            var35.mtd839(this._var11);
            var35.mtd839(this._var12);
            var35.mtd839(this._var13);
            var35.mtd924(this._var0[0]);
            var35.mtd924(this._var0[1]);
            var35.mtd924(this._var0[2]);
            var35.mtd924(this._var0[3]);
            var35.mtd838(this._var1);
            var35.mtd838(this._var14);
            var35.mtd838(this._var15);
            var35.mtd838(this._var5);
            var35.mtd838(this._var6);
            var35.mtd838(this._var7);
            var35.mtd838(this._var18);
            var35.mtd838(this._var19);
            var35.mtd839(this._var8);
            var35.mtd839(this._var9);
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
                return "OS/2";
            }
        }

        internal static int mtd845
        {
            get
            {
                return 0x56;
            }
        }

        internal short mtd872
        {
            get
            {
                return this._var2;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd903 mtd903
        {
            get
            {
                return this._var3;
            }
        }
    }
}

