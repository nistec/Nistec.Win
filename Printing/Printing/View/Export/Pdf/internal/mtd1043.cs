namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1043 : mtd1016
    {
        private float _var0;

        internal mtd1043(mtd230 var1) : base(var1)
        {
            this._var0 = var1.mtd1026;
        }

        internal override float mtd1026
        {
            get
            {
                return this._var0;
            }
        }
    }
}

