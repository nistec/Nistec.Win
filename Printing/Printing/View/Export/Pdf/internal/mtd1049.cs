namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1049 : mtd956
    {
        private string _var0;

        internal mtd1049(string var1, mtd1000 var2, string var0) : base(var1, var2)
        {
            this._var0 = var0;
        }

        internal override string mtd1041
        {
            get
            {
                return this._var0;
            }
        }
    }
}

