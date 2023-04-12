namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;

    internal class mtd757 : mtd747
    {
        private int _var0;
        private ArrayList _var1;
        internal int mtd786;

        internal mtd757(PDFDocument var2)
        {
            base._mtd756 = var2;
            this.mtd759 = 1;
            this.mtd786 = 1;
            this._var1 = new ArrayList();
        }

        internal override void mtd710(ref mtd711 var5)
        {
            int num = this._var1.Count + 1;
            this._var0 = var5.mtd32;
            var5.mtd715("xref");
            var5.mtd715(string.Format("0 {0}", num));
            var5.mtd715("0000000000 65535 f ");
            for (int i = 0; i < this._var1.Count; i++)
            {
                mtd787 mtd = (mtd787) this._var1[i];
                var5.mtd715(string.Format("{0} {1} n ", mtd.mtd737.ToString("0000000000"), mtd.mtd788.ToString("00000")));
            }
            var5.mtd715("trailer");
            var5.mtd715("<<");
            var5.mtd715(string.Format("/Size {0} ", num));
            var5.mtd715(string.Format("/Root {0} 0 R ", base._mtd756.mtd746.mtd759));
            var5.mtd715(string.Format("/Info {0} 0 R ", base._mtd756.mtd242.mtd759));
            if (base._mtd756.mtd712 != null)
            {
                var5.mtd715(string.Format("/Encrypt {0} 0 R ", base._mtd756.mtd712.mtd759));
            }
            var5.mtd710("/ID[<");
            var5.mtd714(base._mtd756.mtd789);
            var5.mtd710("><");
            var5.mtd714(base._mtd756.mtd789);
            var5.mtd715(">]");
            var5.mtd715(">>");
            var5.mtd715("startxref");
            var5.mtd715(this._var0.ToString());
            var5.mtd715("%%EOF");
        }

        internal void mtd758(int var3, int var4)
        {
            this._var1.Add(new mtd787(var3, var4));
        }

        internal override int mtd759
        {
            get
            {
                return base._mtd759;
            }
            set
            {
                base._mtd759 = value;
            }
        }
    }
}

