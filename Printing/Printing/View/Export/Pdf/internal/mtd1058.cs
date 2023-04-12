namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;

    internal class mtd1058
    {
        private PDFDocument _var0;
        private Document _var1;
        private int _var2;

        internal mtd1058(PDFDocument var0)
        {
            this._var0 = var0;
        }

        internal void mtd710(ref mtd1061 var3, ref mtd711 var4)
        {
            this._var0.mtd757.mtd758(var4.mtd32, 0);
            var4.mtd715(string.Format("{0} 0 obj", this._var2));
            var4.mtd715("<<");
            var4.mtd715("/Type /Pages");
            var4.mtd710("/Kids [ ");
            PagesCollection pages = this._var1.Pages;
            int count = pages.Count;
            int num2 = 4;
            for (int i = 0; i < count; i++)
            {
                var4.mtd710(string.Format("{0} 0 R ", num2));
                num2 += 2;
            }
            var4.mtd715("]");
            var4.mtd715(string.Format("/Count {0}", count));
            var4.mtd715(">>");
            var4.mtd715("endobj");
            if (count > 0)
            {
                mtd1055 mtd = new mtd1055();
                mtd.mtd172(this._var1.PageWidth * 72f, this._var1.PageHeight * 72f, this._var1.LeftMargin * 72f, this._var1.RightMargin * 72f, this._var1.ReportWidth * 72f);
                mtd.mtd1064 = this._var0;
                num2 = 4;
                for (int j = 0; j < count; j++)
                {
                    mtd.mtd710(pages.GetPage(j), num2, ref var3, ref var4);
                    num2 += 2;
                }
            }
        }

        internal void mtd780(Document var1)
        {
            int num;
            this._var1 = var1;
            mtd757 mtd1 = this._var0.mtd757;
            mtd1.mtd759 = (num = mtd1.mtd759) + 1;
            this._var2 = num;
            mtd757 mtd2 = this._var0.mtd757;
            mtd2.mtd759 += this._var1.Pages.Count * 2;
        }

        internal Document mtd1067
        {
            set
            {
                this._var1 = value;
            }
        }

        internal int mtd759
        {
            get
            {
                return this._var2;
            }
        }
    }
}

