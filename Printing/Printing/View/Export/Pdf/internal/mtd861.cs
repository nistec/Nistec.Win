namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd861 : mtd708
    {
        private mtd876[] _var0;
        private ArrayList _var1;

        internal mtd861(mtd821 var2) : base(var2)
        {
            this._var1 = new ArrayList();
        }

        protected override void mtd822(mtd708 var6, mtd823 var7)
        {
            mtd861 mtd = var6 as mtd861;
            this._var0 = new mtd876[mtd.mtd877.Length];
            this._var1.Clear();
            for (int i = 0; i < var7.mtd885.Length; i++)
            {
                this.var4(var7.mtd885[i], mtd.mtd877[var7.mtd885[i]]);
            }
        }

        protected override void mtd824(mtd825 var8)
        {
            this._var0 = new mtd876[base.mtd821.mtd866.mtd887];
            this._var1.Clear();
            int num = var8.mtd832;
            for (int i = 0; i < base.mtd821.mtd866.mtd887; i++)
            {
                uint num3 = base.mtd821.mtd865[(ushort) i];
                uint num4 = base.mtd821.mtd865[(ushort) (i + 1)];
                if (num4 != num3)
                {
                    this.var4(i, new mtd876((int) (num4 - num3)));
                    var8.mtd833(num);
                    var8.mtd834((int) num3);
                    this._var0[i].mtd842(var8);
                }
            }
        }

        protected override void mtd828(mtd829 var8)
        {
            for (int i = 0; i < this.mtd166; i++)
            {
                this[i].mtd710(var8);
            }
        }

        private void var4(int var3, mtd876 var5)
        {
            this._var0[var3] = var5;
            if (var5 != null)
            {
                this._var1.Add(var5);
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var1.Count;
            }
        }

        internal override int mtd736
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.mtd166; i++)
                {
                    num += this[i].mtd32;
                }
                return num;
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "glyf";
            }
        }

        internal mtd876[] mtd877
        {
            get
            {
                return this._var0;
            }
        }

        internal mtd876 this[int var3]
        {
            get
            {
                return (this._var1[var3] as mtd876);
            }
        }
    }
}

