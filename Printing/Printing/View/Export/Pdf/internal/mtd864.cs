namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd864 : mtd708
    {
        private mtd881[] _var0;

        internal mtd864(mtd821 var1) : base(var1)
        {
        }

        protected override void mtd822(mtd708 var2, mtd823 var3)
        {
            mtd864 mtd = var2 as mtd864;
            this._var0 = new mtd881[mtd._var0.Length];
            mtd._var0.CopyTo(this._var0, 0);
        }

        protected override void mtd824(mtd825 var4)
        {
            int num = Math.Max(base.mtd821.mtd866.mtd887, base.mtd821.mtd863.mtd898);
            this._var0 = new mtd881[num];
            for (int i = 0; i < base.mtd821.mtd863.mtd898; i++)
            {
                this._var0[i].mtd882 = var4.mtd835();
                this._var0[i].mtd899 = var4.mtd892();
            }
            ushort num3 = this._var0[base.mtd821.mtd863.mtd898 - 1].mtd882;
            for (int j = base.mtd821.mtd863.mtd898; j < num; j++)
            {
                this._var0[j].mtd882 = num3;
                this._var0[j].mtd899 = var4.mtd892();
            }
        }

        protected override void mtd828(mtd829 var4)
        {
            int num = Math.Max(base.mtd821.mtd866.mtd887, base.mtd821.mtd863.mtd898);
            for (int i = 0; i < base.mtd821.mtd863.mtd898; i++)
            {
                var4.mtd838(this._var0[i].mtd882);
                var4.mtd893(this._var0[i].mtd899);
            }
            for (int j = base.mtd821.mtd863.mtd898; j < num; j++)
            {
                var4.mtd893(this._var0[j].mtd899);
            }
        }

        internal int mtd166
        {
            get
            {
                return this._var0.Length;
            }
        }

        internal override int mtd736
        {
            get
            {
                return (mtd881.mtd845 * this.mtd166);
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "hmtx";
            }
        }

        internal mtd881 this[ushort index]
        {
            get
            {
                return this._var0[index];
            }
        }
    }
}

