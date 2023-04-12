namespace Nistec.Printing.View
{
    using System;

    public class Page
    {
        private int _var0;
        private mtd127 _var1 = new mtd127();
        internal mtd163 mtd347;
        internal mtd163 mtd348;

        internal Page(int var2)
        {
            this._var0 = var2;
        }

        internal bool mtd141(Nistec.Printing.View.mtd141 var4, int var5)
        {
            if ((var5 > -1) && (var5 < this.mtd349.Count))
            {
                var4.mtd142(this.mtd349.mtd143(var5));
                return true;
            }
            return false;
        }

        internal void mtd351(mtd163 var3)
        {
            this._var1.mtd2(var3);
        }

        internal bool mtd352(Nistec.Printing.View.mtd141 var4)
        {
            if (this.mtd347 != null)
            {
                var4.mtd142(this.mtd347);
                return true;
            }
            return false;
        }

        internal bool mtd353(Nistec.Printing.View.mtd141 var4)
        {
            if (this.mtd348 != null)
            {
                var4.mtd142(this.mtd348);
                return true;
            }
            return false;
        }

        internal mtd127 mtd349
        {
            get
            {
                return this._var1;
            }
        }

        internal int mtd350
        {
            get
            {
                return this._var1.Count;
            }
        }

        public int PageIndex
        {
            get
            {
                return this._var0;
            }
        }

        public int SectionCount
        {
            get
            {
                return this._var1.Count;
            }
        }
    }
}

