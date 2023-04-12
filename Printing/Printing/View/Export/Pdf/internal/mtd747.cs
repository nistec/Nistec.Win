namespace Nistec.Printing.View.Pdf
{
    using System;

    internal abstract class mtd747
    {
        protected PDFDocument _mtd756;
        protected int _mtd759;

        internal mtd747()
        {
        }

        internal virtual void mtd710(ref mtd711 var0)
        {
        }

        internal virtual void mtd780()
        {
            int num;
            mtd757 mtd1 = this._mtd756.mtd757;
            mtd1.mtd759 = (num = mtd1.mtd759) + 1;
            this._mtd759 = num;
        }

        internal virtual int mtd759
        {
            get
            {
                return this._mtd759;
            }
            set
            {
            }
        }

        internal virtual string mtd763
        {
            get
            {
                return null;
            }
        }
    }
}

