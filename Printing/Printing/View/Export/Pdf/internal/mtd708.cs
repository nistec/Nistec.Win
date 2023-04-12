namespace Nistec.Printing.View.Pdf
{
    using System;

    internal abstract class mtd708
    {
        private mtd844 _var0;
        private Nistec.Printing.View.Pdf.mtd821 _var1;

        internal mtd708(Nistec.Printing.View.Pdf.mtd821 var1)
        {
            this._var1 = var1;
        }

        internal void mtd172(mtd708 var2)
        {
            this.mtd172(var2, null);
        }

        internal void mtd172(mtd708 var3, mtd823 var4)
        {
            if ((var3 != null) && base.GetType().Equals(var3.GetType()))
            {
                this.mtd822(var3, var4);
                if (this.mtd827 == null)
                {
                    this.mtd821.mtd874.mtd846(this);
                }
            }
        }

        internal void mtd710(mtd829 var6)
        {
            var6.mtd800();
            if (this.mtd827 != null)
            {
                this.mtd827.mtd850(var6.mtd832);
            }
            this.mtd828(var6);
        }

        protected virtual void mtd822(mtd708 var2, mtd823 var5)
        {
        }

        protected virtual void mtd824(mtd825 var6)
        {
        }

        protected virtual void mtd828(mtd829 var6)
        {
        }

        internal void mtd842(mtd825 var6)
        {
            if (this.mtd827 == null)
            {
                throw new Exception("The required Font table not found in font file");
            }
            var6.mtd833(this.mtd827.mtd737);
            this.mtd824(var6);
        }

        internal virtual int mtd736
        {
            get
            {
                return 0;
            }
        }

        protected internal abstract string mtd789 { get; }

        internal Nistec.Printing.View.Pdf.mtd821 mtd821
        {
            get
            {
                return this._var1;
            }
        }

        protected internal mtd844 mtd827
        {
            get
            {
                if (this._var0 == null)
                {
                    this._var0 = this.mtd821.mtd874[this.mtd789];
                }
                return this._var0;
            }
        }
    }
}

