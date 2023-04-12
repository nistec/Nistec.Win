namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.IO;

    internal class mtd1010 : mtd711
    {
        private Stream _var0;

        internal mtd1010(Stream var0)
        {
            this._var0 = var0;
        }

        protected override void _mtd1006(byte var1)
        {
            this._var0.WriteByte(var1);
        }

        internal override int mtd32
        {
            get
            {
                return (int) this._var0.Length;
            }
        }
    }
}

