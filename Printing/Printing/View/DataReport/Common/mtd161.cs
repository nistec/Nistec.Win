namespace Nistec.Printing.View
{
    using System;

    internal class mtd161 : mtd126
    {
        internal mtd127 mtd162;
        internal mtd161 mtd208;
        internal Nistec.Printing.View.mtd264 mtd264;
        internal bool mtd286;

        internal mtd161()
        {
            this.mtd162 = new mtd127();
        }

        internal mtd161(ref McReportControl c) : base(ref c)
        {
            McSubReport sr = (McSubReport) c;
            this.mtd162 = new mtd127();
            this.mtd264 = new Nistec.Printing.View.mtd264(ref sr);
        }

        internal static mtd161 mtd105(ref mtd161 e)
        {
            McSubReport report = e.mtd264.mtd269;
            mtd161 mtd = new mtd161();
            mtd.RptControl = e.RptControl;
            mtd.mtd130 = report.Visible;
            mtd._Border = report.Border;
            mtd.mtd264 = e.mtd264;
            mtd._Location = new McLocation(e.mtd128, e.mtd129, e.Width, e.Height, true);
            return mtd;
        }

        internal static mtd161 mtd105(ref mtd161 e, mtd248 var0)
        {
            McSubReport report = e.mtd264.mtd269;
            mtd161 mtd = new mtd161();
            mtd.RptControl = e.RptControl;
            mtd.mtd130 = report.Visible;
            mtd._Border = report.Border;
            mtd.mtd264 = e.mtd264;
            if (var0.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(report.Left, report.Top, report.Width, report.Height, true);
                return mtd;
            }
            mtd._Location = e._Location;
            return mtd;
        }

        internal mtd163 mtd287(SectionType var1)
        {
            for (int i = 0; i < this.mtd162.Count; i++)
            {
                mtd163 mtd = this.mtd162.mtd143(i);
                if (var1 == mtd._SectionType)
                {
                    return mtd;
                }
            }
            return null;
        }
    }
}

