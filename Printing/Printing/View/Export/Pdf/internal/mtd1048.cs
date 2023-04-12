namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1048 : mtd1015
    {
        private float _var0;
        private TextAlignment _var1;

        internal mtd1048(TextAlignment var2, float var0)
        {
            this._var0 = var0;
            this._var1 = var2;
        }

        internal override TextAlignment mtd1024
        {
            get
            {
                return this._var1;
            }
        }

        internal override float mtd1046
        {
            get
            {
                return this._var0;
            }
        }

        internal override mtd696 mtd66
        {
            get
            {
                return mtd696.mtd698;
            }
        }
    }
}

