namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd703 : mtd1022
    {
        private float _var0;

        internal mtd703(mtd1015 var1, float var0) : base(var1)
        {
            this._var0 = var0;
        }

        internal override bool mtd1030
        {
            get
            {
                return true;
            }
        }

        internal override float mtd1031
        {
            set
            {
                this._var0 += value;
            }
        }

        internal override float mtd30
        {
            get
            {
                return this._var0;
            }
        }
    }
}

