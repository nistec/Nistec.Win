namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd841
    {
        private byte[] _var0;

        internal mtd841()
        {
        }

        internal void mtd710(mtd829 var1)
        {
            var1.mtd830(this._var0);
        }

        internal virtual void mtd842(mtd825 var1, int var2)
        {
            this._var0 = var1.mtd826(var2);
        }

        internal byte[] mtd784
        {
            get
            {
                return this._var0;
            }
        }
    }
}

