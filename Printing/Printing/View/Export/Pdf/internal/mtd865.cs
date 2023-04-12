namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd865 : mtd708
    {
        private uint[] _var0;

        internal mtd865(mtd821 var1) : base(var1)
        {
        }

        protected override void mtd822(mtd708 var2, mtd823 var3)
        {
            mtd865 mtd = var2 as mtd865;
            this._var0 = new uint[mtd.mtd166];
            uint num = 0;
            for (int i = 0; i < base.mtd821.mtd861.mtd877.Length; i++)
            {
                this._var0[i] = num;
                if (base.mtd821.mtd861.mtd877[i] != null)
                {
                    num += (uint) base.mtd821.mtd861.mtd877[i].mtd32;
                }
            }
            this._var0[base.mtd821.mtd861.mtd877.Length] = num;
        }

        protected override void mtd824(mtd825 var4)
        {
            this._var0 = new uint[base.mtd821.mtd866.mtd887 + 1];
            for (int i = 0; i < (base.mtd821.mtd866.mtd887 + 1); i++)
            {
                this._var0[i] = (base.mtd821.mtd862.mtd894 == 1) ? var4.mtd837() : (Convert.ToUInt32(var4.mtd835()) << 1);
            }
        }

        protected override void mtd828(mtd829 var4)
        {
            for (int i = 0; i < (base.mtd821.mtd866.mtd887 + 1); i++)
            {
                if (base.mtd821.mtd862.mtd894 == 1)
                {
                    var4.mtd839(this._var0[i]);
                }
                else
                {
                    var4.mtd838(Convert.ToUInt16((uint) (this._var0[i] >> 1)));
                }
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
                int num = (base.mtd821.mtd862.mtd894 == 1) ? 4 : 2;
                return (num * this.mtd166);
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "loca";
            }
        }

        internal uint this[ushort index]
        {
            get
            {
                return this._var0[index];
            }
        }
    }
}

