namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd843
    {
        private ArrayList _var0 = new ArrayList();
        private ushort _var1;
        private ushort _var2;
        private Nistec.Printing.View.Pdf.mtd821 _var3;
        private ushort _var4;
        private ushort _var5;
        private byte[] _var6;

        internal mtd843(Nistec.Printing.View.Pdf.mtd821 var7)
        {
            this._var3 = var7;
        }

        internal void mtd172(mtd843 var11)
        {
            this._var6 = new byte[var11._var6.Length];
            var11._var6.CopyTo(this._var6, 0);
            this._var2 = (ushort) this.mtd166;
            double y = Math.Floor(Math.Log((double) this._var2, 2.0));
            double num2 = Math.Pow(2.0, y);
            this._var5 = (ushort) ((int) (num2 * 16.0));
            this._var1 = (ushort) ((int) y);
            this._var4 = (ushort) ((this._var2 * 0x10) - this._var5);
        }

        internal void mtd710(mtd829 var12)
        {
            var12.mtd833(0);
            var12.mtd830(this._var6);
            var12.mtd838(this._var2);
            var12.mtd838(this._var5);
            var12.mtd838(this._var1);
            var12.mtd838(this._var4);
            this.var15(var12);
        }

        internal void mtd842(mtd825 var12)
        {
            var12.mtd833(0);
            this._var6 = var12.mtd826(4);
            this._var2 = var12.mtd835();
            this._var5 = var12.mtd835();
            this._var1 = var12.mtd835();
            this._var4 = var12.mtd835();
            this.var13(var12);
        }

        internal void mtd846(mtd708 var14)
        {
            mtd844 mtd = new mtd844();
            mtd.mtd172(var14);
            this._var0.Add(mtd);
        }

        internal void mtd847(mtd829 var12)
        {
            var12.mtd833(0);
            var12.mtd834(mtd845);
            for (int i = 0; i < this.mtd166; i++)
            {
                this[i].mtd847(var12);
            }
        }

        internal void mtd848(mtd829 var12)
        {
            var12.mtd833(0);
            var12.mtd834(mtd845);
            for (int i = 0; i < this.mtd166; i++)
            {
                this[i].mtd849(var12);
            }
        }

        private void var13(mtd825 var12)
        {
            this._var0.Clear();
            for (int i = 0; i < this.var10; i++)
            {
                mtd844 mtd = new mtd844();
                mtd.mtd842(var12);
                this._var0.Add(mtd);
            }
        }

        private void var15(mtd829 var12)
        {
            for (int i = 0; i < this.mtd166; i++)
            {
                this[i].mtd710(var12);
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var0.Count;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd821 mtd821
        {
            get
            {
                return this._var3;
            }
        }

        internal static int mtd845
        {
            get
            {
                return 12;
            }
        }

        internal mtd844 this[int var8]
        {
            get
            {
                return (this._var0[var8] as mtd844);
            }
        }

        internal mtd844 this[string var9]
        {
            get
            {
                for (int i = 0; i < this.mtd166; i++)
                {
                    if (this[i].mtd116 == var9)
                    {
                        return this[i];
                    }
                }
                return null;
            }
        }

        private int var10
        {
            get
            {
                return Convert.ToInt32(this._var2);
            }
        }
    }
}

