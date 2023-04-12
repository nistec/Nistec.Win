namespace Nistec.Printing.View
{
    using System;

    internal class mtd272
    {
        internal mtd163 mtd290;
        internal bool mtd298;
        internal mtd158[] mtd304;
        internal mtd307[] mtd308;
        internal mtd314[] mtd343;

        internal mtd272(ref Section s, int var0)
        {
            this.mtd290 = new mtd163(ref s);
            this.mtd290.mtd167 = new mtd164(var0);
            this.mtd343 = new mtd314[var0];
            this.mtd304 = new mtd158[var0];
            this.mtd298 = false;
        }

        internal void mtd214(int var0)
        {
            this.mtd290.mtd167 = new mtd164(var0);
            this.var1(var0);
        }

        internal void mtd344(int var0)
        {
            if (var0 == 0)
            {
                this.mtd304 = new mtd158[0];
            }
            else
            {
                mtd158[] sourceArray = this.mtd304;
                this.mtd304 = new mtd158[var0];
                Array.Copy(sourceArray, this.mtd304, var0);
            }
        }

        private void var1(int var0)
        {
            mtd314[] sourceArray = this.mtd343;
            this.mtd343 = new mtd314[var0];
            Array.Copy(sourceArray, this.mtd343, var0);
        }
    }
}

