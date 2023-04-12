namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd761 : mtd747
    {
        private Nistec.Printing.View.Pdf.mtd762 _var0;

        internal mtd761(PDFDocument var1)
        {
            base._mtd756 = var1;
            this._var0 = new Nistec.Printing.View.Pdf.mtd762(null);
        }

        internal override void mtd710(ref mtd711 var2)
        {
            int num = this._var0.mtd166;
            base._mtd756.mtd757.mtd758(var2.mtd32, 0);
            var2.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var2.mtd715("<<");
            var2.mtd715("/Type /Outlines ");
            if (num > 0)
            {
                var2.mtd715(string.Format("/First {0} 0 R", this._var0[0].mtd759));
                var2.mtd715(string.Format("/Last {0} 0 R", this._var0[num - 1].mtd759));
            }
            var2.mtd715(string.Format("/Count {0}", num));
            var2.mtd715(">>");
            var2.mtd715("endobj");
            this.var3(ref var2, this._var0);
        }

        internal override void mtd780()
        {
            mtd757 mtd = base._mtd756.mtd757;
            if (base._mtd759 == 0)
            {
                int num2;
                mtd.mtd759 = (num2 = mtd.mtd759) + 1;
                base._mtd759 = num2;
            }
            for (int i = 0; i < this._var0.mtd166; i++)
            {
                this._var0[i].mtd780(mtd);
            }
        }

        private void var3(ref mtd711 var2, Nistec.Printing.View.Pdf.mtd762 var0)
        {
            int num = var0.mtd166;
            for (int i = 0; i < num; i++)
            {
                mtd776 mtd = var0[i];
                mtd712 mtd2 = base._mtd756.mtd712;
                base._mtd756.mtd757.mtd758(var2.mtd32, 0);
                var2.mtd715(string.Format("{0} 0 obj", mtd.mtd759));
                if (mtd2 != null)
                {
                    mtd2.mtd775(mtd.mtd759, 0);
                }
                var2.mtd715("<<");
                if ((mtd.mtd767 != null) && (mtd.mtd767.Length > 0))
                {
                    var2.mtd710("/Title ");
                    if (mtd2 != null)
                    {
                        mtd652.mtd710(ref var2, mtd.mtd767, mtd2);
                    }
                    else
                    {
                        mtd652.mtd710(ref var2, mtd620.mtd652(mtd.mtd767), mtd2);
                    }
                }
                if (mtd.mtd208 == null)
                {
                    var2.mtd715(string.Format("/Parent {0} 0 R", this.mtd759));
                }
                else
                {
                    var2.mtd715(string.Format("/Parent {0} 0 R", mtd.mtd208.mtd759));
                }
                mtd1055 mtd1 = mtd.mtd777;
                if (mtd.mtd778.mtd166 > 0)
                {
                    var2.mtd715(string.Format("/First {0} 0 R", mtd.mtd778[0].mtd759));
                    var2.mtd715(string.Format("/Last {0} 0 R", mtd.mtd778[mtd.mtd778.mtd166 - 1].mtd759));
                    if ((i + 1) == num)
                    {
                        if ((i - 1) > -1)
                        {
                            var2.mtd715(string.Format("/Prev {0} 0 R", var0[i - 1].mtd759));
                        }
                    }
                    else
                    {
                        var2.mtd715(string.Format("/Next {0} 0 R", var0[i + 1].mtd759));
                    }
                    if (mtd.mtd779)
                    {
                        var2.mtd715(string.Format("/Count {0}", mtd.mtd778.mtd166));
                    }
                    else
                    {
                        var2.mtd715(string.Format("/Count -{0}", mtd.mtd778.mtd166));
                    }
                }
                else if ((i + 1) == num)
                {
                    if ((i - 1) > -1)
                    {
                        var2.mtd715(string.Format("/Prev {0} 0 R", var0[i - 1].mtd759));
                    }
                }
                else
                {
                    var2.mtd715(string.Format("/Next {0} 0 R", var0[i + 1].mtd759));
                }
                var2.mtd715(">>");
                var2.mtd715("endobj");
                if (mtd.mtd778.mtd166 > 0)
                {
                    this.var3(ref var2, mtd.mtd778);
                }
            }
        }

        internal Nistec.Printing.View.Pdf.mtd762 mtd762
        {
            get
            {
                return this._var0;
            }
        }
    }
}

