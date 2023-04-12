namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd820 : mtd708
    {
        private byte[] _var0;
        private string _var1;

        internal mtd820(mtd821 var2, string var1) : base(var2)
        {
            this._var1 = var1;
        }

        protected override void mtd822(mtd708 var3, mtd823 var4)
        {
            mtd820 mtd = var3 as mtd820;
            this._var1 = mtd.mtd91;
            this._var0 = new byte[mtd.mtd784.Length];
            mtd.mtd784.CopyTo(this._var0, 0);
        }

        protected override void mtd824(mtd825 var5)
        {
            this._var0 = var5.mtd826(base.mtd827.mtd736);
        }

        protected override void mtd828(mtd829 var5)
        {
            var5.mtd830(this._var0);
        }

        internal override int mtd736
        {
            get
            {
                return this._var0.Length;
            }
        }

        internal byte[] mtd784
        {
            get
            {
                return this._var0;
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return this._var1;
            }
        }

        internal string mtd91
        {
            get
            {
                return this._var1;
            }
        }
    }
}

