namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd242 : mtd747
    {
        internal string mtd767;
        internal string mtd768;
        internal string mtd769;
        internal string mtd770;
        internal string mtd771;
        internal string mtd772;
        internal DateTime mtd773;
        internal DateTime mtd774;

        internal mtd242(PDFDocument var0)
        {
            base._mtd756 = var0;
            this.mtd767 = null;
            this.mtd769 = null;
            this.mtd770 = null;
            this.mtd771 = "Nistec PDFWriter for .NET";
            this.mtd772 = null;
            this.mtd768 = null;
            this.mtd772 = null;
            this.mtd773 = DateTime.Now;
            this.mtd774 = this.mtd773;
        }

        internal override void mtd710(ref mtd711 bs)
        {
            mtd757 mtd = base._mtd756.mtd757;
            mtd712 mtd2 = base._mtd756.mtd712;
            mtd.mtd758(bs.mtd32, 0);
            bs.mtd715(string.Format("{0} 0 obj", this.mtd759));
            if (mtd2 != null)
            {
                mtd2.mtd775(this.mtd759, 0);
            }
            bs.mtd715("<<");
            if ((this.mtd767 != null) && (this.mtd767.Length > 0))
            {
                bs.mtd710("/Title ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd767, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd767), mtd2);
                }
            }
            if ((this.mtd768 != null) && (this.mtd768.Length > 0))
            {
                bs.mtd710("/Author ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd768, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd768), mtd2);
                }
            }
            if ((this.mtd769 != null) && (this.mtd769.Length > 0))
            {
                bs.mtd710("/Subject ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd769, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd769), mtd2);
                }
            }
            if ((this.mtd770 != null) && (this.mtd770.Length > 0))
            {
                bs.mtd710("/Keywords ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd770, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd770), mtd2);
                }
            }
            if ((this.mtd771 != null) && (this.mtd771.Length > 0))
            {
                bs.mtd710("/Creator ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd771, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd771), mtd2);
                }
            }
            if ((this.mtd772 != null) && (this.mtd772.Length > 0))
            {
                bs.mtd710("/Producer ");
                if (mtd2 != null)
                {
                    mtd652.mtd710(ref bs, this.mtd772, mtd2);
                }
                else
                {
                    mtd652.mtd710(ref bs, mtd620.mtd652(this.mtd772), mtd2);
                }
            }
            bs.mtd710("/CreationDate ");
            mtd652.mtd710(ref bs, mtd620.mtd646(this.mtd773), mtd2);
            bs.mtd710("/ModDate ");
            mtd652.mtd710(ref bs, mtd620.mtd646(this.mtd774), mtd2);
            bs.mtd715(">>");
            bs.mtd715("endobj");
        }
    }
}

