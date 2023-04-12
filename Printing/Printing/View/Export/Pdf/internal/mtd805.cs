namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd805 : mtd711
    {
        protected byte[] _mtd1008;
        protected int _mtd218;

        internal mtd805()
        {
            this._mtd218 = 0;
            this._mtd1008 = new byte[0x400];
        }

        internal mtd805(long var0)
        {
            this._mtd218 = 0;
            if (var0 < 0x400L)
            {
                var0 = 0x400L;
            }
            this._mtd1008 = new byte[var0];
        }

        protected override void _mtd1006(byte var1)
        {
            this.var2();
            this._mtd1008[this._mtd218] = var1;
            this._mtd218++;
        }

        internal void mtd1009()
        {
            byte[] sourceArray = this._mtd1008;
            this._mtd1008 = new byte[this._mtd218];
            Array.Copy(sourceArray, 0, this._mtd1008, 0, this._mtd218);
        }

        private void var2()
        {
            if (this._mtd218 >= this._mtd1008.Length)
            {
                byte[] destinationArray = new byte[2 * this._mtd1008.Length];
                Array.Copy(this._mtd1008, destinationArray, this._mtd218);
                this._mtd1008 = destinationArray;
            }
        }

        internal override int mtd32
        {
            get
            {
                return this._mtd218;
            }
        }

        internal byte[] mtd784
        {
            get
            {
                return this._mtd1008;
            }
        }
    }
}

