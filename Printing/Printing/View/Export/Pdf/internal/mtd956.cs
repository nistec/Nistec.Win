namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd956 : mtd1015
    {
        private string _var0;
        private mtd1000 _var1;

        internal mtd956(string var0, mtd1000 var1)
        {
            this._var0 = var0;
            this._var1 = var1;
        }

        internal override string mtd51
        {
            get
            {
                return this._var0;
            }
        }

        internal override mtd1000 mtd934
        {
            get
            {
                return this._var1;
            }
        }
    }
}

