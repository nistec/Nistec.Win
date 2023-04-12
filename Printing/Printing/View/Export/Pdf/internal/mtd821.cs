namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd821
    {
        private Nistec.Printing.View.Pdf.mtd831 _var0;
        private mtd820 _var1;
        private Nistec.Printing.View.Pdf.mtd867 _var10;
        private Nistec.Printing.View.Pdf.mtd868 _var11;
        private mtd820 _var12;
        private mtd843 _var13;
        private mtd820 _var2;
        private Nistec.Printing.View.Pdf.mtd861 _var3;
        private Nistec.Printing.View.Pdf.mtd862 _var4;
        private Nistec.Printing.View.Pdf.mtd863 _var5;
        private Nistec.Printing.View.Pdf.mtd864 _var6;
        private Nistec.Printing.View.Pdf.mtd865 _var7;
        private Nistec.Printing.View.Pdf.mtd866 _var8;
        private Nistec.Printing.View.Pdf.mtd91 _var9;

        internal mtd821()
        {
            this._var13 = new mtd843(this);
            this._var4 = new Nistec.Printing.View.Pdf.mtd862(this);
            this._var8 = new Nistec.Printing.View.Pdf.mtd866(this);
            this._var5 = new Nistec.Printing.View.Pdf.mtd863(this);
            this._var6 = new Nistec.Printing.View.Pdf.mtd864(this);
            this._var11 = new Nistec.Printing.View.Pdf.mtd868(this);
            this._var10 = new Nistec.Printing.View.Pdf.mtd867(this);
            this._var7 = new Nistec.Printing.View.Pdf.mtd865(this);
            this._var3 = new Nistec.Printing.View.Pdf.mtd861(this);
            this._var0 = new Nistec.Printing.View.Pdf.mtd831(this);
            this._var9 = new Nistec.Printing.View.Pdf.mtd91(this);
            this._var12 = new mtd820(this, "prep");
            this._var1 = new mtd820(this, "cvt ");
            this._var2 = new mtd820(this, "fpgm");
        }

        internal byte[] mtd710(ushort[] var17, string var24)
        {
            mtd821 mtd = new mtd821();
            mtd829 mtd2 = new mtd829();
            mtd823 mtd3 = new mtd823();
            mtd3.mtd836 = var17;
            mtd3.mtd885 = this.mtd879(var17);
            mtd3.mtd886 = var24;
            mtd.var19(this, mtd3);
            mtd.var22(mtd2);
            return mtd2.mtd784;
        }

        internal ushort mtd816(char var18)
        {
            return this.mtd816(this.mtd831[var18]);
        }

        internal ushort mtd816(ushort var15)
        {
            mtd881 mtd = this.mtd864[var15];
            return mtd.mtd882;
        }

        internal void mtd842(byte[] var21)
        {
            mtd825 mtd = new mtd825(var21);
            this._var13.mtd842(mtd);
            this._var4.mtd842(mtd);
            this._var5.mtd842(mtd);
            this._var8.mtd842(mtd);
            this._var6.mtd842(mtd);
            this._var7.mtd842(mtd);
            this._var3.mtd842(mtd);
            this._var12.mtd842(mtd);
            this._var1.mtd842(mtd);
            this._var2.mtd842(mtd);
            this._var10.mtd842(mtd);
            this._var11.mtd842(mtd);
            this._var9.mtd842(mtd);
            this._var0.mtd842(mtd);
        }

        internal ushort[] mtd879(ushort[] var17)
        {
            mtd875 mtd = new mtd875();
            this.var14(0, mtd);
            for (int i = 0; i < var17.Length; i++)
            {
                this.var14(this.mtd831[var17[i]], mtd);
            }
            return mtd.mtd880;
        }

        internal ushort mtd883(ushort var18)
        {
            return this.mtd831[var18];
        }

        private void var14(ushort var15, mtd875 var16)
        {
            if (var16.mtd2(var15))
            {
                mtd876 mtd = this.mtd861.mtd877[var15];
                if ((mtd != null) && (mtd.mtd878 is mtd840))
                {
                    mtd840 mtd2 = mtd.mtd878 as mtd840;
                    for (int i = 0; i < mtd2.mtd166; i++)
                    {
                        this.var14(mtd2[i], var16);
                    }
                }
            }
        }

        private void var19(mtd821 var20, mtd823 var21)
        {
            this.mtd861.mtd172(var20.mtd861, var21);
            this.mtd865.mtd172(var20.mtd865);
            this.mtd862.mtd172(var20.mtd862);
            this.mtd866.mtd172(var20.mtd866);
            this.mtd863.mtd172(var20.mtd863);
            this.mtd864.mtd172(var20.mtd864);
            this.mtd873.mtd172(var20.mtd873);
            this.mtd869.mtd172(var20.mtd869);
            this.mtd870.mtd172(var20.mtd870);
            this._var13.mtd172(var20.mtd874);
        }

        private void var22(mtd829 var23)
        {
            this._var13.mtd710(var23);
            this.mtd862.mtd710(var23);
            this.mtd863.mtd710(var23);
            this.mtd866.mtd710(var23);
            this.mtd864.mtd710(var23);
            this.mtd865.mtd710(var23);
            this.mtd861.mtd710(var23);
            this.mtd873.mtd710(var23);
            this.mtd869.mtd710(var23);
            this.mtd870.mtd710(var23);
            var23.mtd800();
            this._var13.mtd848(var23);
            this._var13.mtd847(var23);
            this.mtd862.mtd884(var23);
        }

        internal Nistec.Printing.View.Pdf.mtd831 mtd831
        {
            get
            {
                return this._var0;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd861 mtd861
        {
            get
            {
                return this._var3;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd862 mtd862
        {
            get
            {
                return this._var4;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd863 mtd863
        {
            get
            {
                return this._var5;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd864 mtd864
        {
            get
            {
                return this._var6;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd865 mtd865
        {
            get
            {
                return this._var7;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd866 mtd866
        {
            get
            {
                return this._var8;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd867 mtd867
        {
            get
            {
                return this._var10;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd868 mtd868
        {
            get
            {
                return this._var11;
            }
        }

        internal mtd820 mtd869
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd820 mtd870
        {
            get
            {
                return this._var2;
            }
        }

        internal bool mtd871
        {
            get
            {
                return (this.mtd867.mtd872 != 2);
            }
        }

        internal mtd820 mtd873
        {
            get
            {
                return this._var12;
            }
        }

        internal mtd843 mtd874
        {
            get
            {
                return this._var13;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd91 mtd91
        {
            get
            {
                return this._var9;
            }
        }
    }
}

