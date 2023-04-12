namespace Nistec.Printing.View
{
    using System;

    internal class mtd266
    {
        private McField _var0;
        internal object mtd137;

        internal mtd266(ref McField var1)
        {
            this._var0 = var1;
        }

        internal mtd266 mtd253()
        {
            return new mtd266(ref this._var0);
        }

        internal void mtd70()
        {
            this.mtd137 = this._var0.Value;
        }

        internal McField mtd204
        {
            get
            {
                return this._var0;
            }
        }

        internal bool mtd285
        {
            get
            {
                return (string.Compare(Convert.ToString(this.mtd137), Convert.ToString(this._var0.Value), true) == 0);
            }
        }
    }
}

