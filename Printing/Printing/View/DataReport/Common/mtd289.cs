namespace Nistec.Printing.View
{
    using System;
    using System.Collections;

    internal class mtd289
    {
        internal PageFooter pageFooter;
        internal PageHeader pageHeader;
        internal ReportHeader reportHeader;
        internal GroupHeader groupHeader;
        internal int mtd297;
        internal ReportDetail reportDetail;
        internal mtd289 mtd302;
        internal GroupFooter groupFooter;
        internal ReportFooter reportFooter;
        internal Section _Section;
        internal int mtd355;
        internal float mtd356;
        internal mtd163 mtd357;
        internal mtd266[] mtd358;
        internal ArrayList mtd359 = new ArrayList();
        internal SectionType mtd66;

        internal mtd289(ref Section s)
        {
            this.mtd66 = s.Type;
            this._Section = s;
            if (this.mtd66 == SectionType.ReportDetail)
            {
                this.reportDetail = (ReportDetail) s;
            }
            else if (this.mtd66 == SectionType.GroupHeader)
            {
                this.groupHeader = (GroupHeader) s;
            }
            else if (this.mtd66 == SectionType.GroupFooter)
            {
                this.groupFooter = (GroupFooter) s;
            }
            else if (this.mtd66 == SectionType.PageHeader)
            {
                this.pageHeader = (PageHeader) s;
            }
            else if (this.mtd66 == SectionType.PageFooter)
            {
                this.pageFooter = (PageFooter) s;
            }
            else if (this.mtd66 == SectionType.ReportHeader)
            {
                this.reportHeader = (ReportHeader) s;
            }
            else if (this.mtd66 == SectionType.ReportFooter)
            {
                this.reportFooter = (ReportFooter) s;
            }
        }

        internal void mtd295()
        {
            if (this.mtd358 != null)
            {
                for (int i = 0; i < this.mtd358.Length; i++)
                {
                    this.mtd358[i].mtd70();
                }
            }
        }

        internal void mtd360(ArrayList var0, int var1)
        {
            if ((var0 != null) && (var0.Count > 0))
            {
                this.mtd358 = new mtd266[var0.Count];
                int index = 0;
                for (int i = var0.Count - 1; i > -1; i--)
                {
                    if (i > var1)
                    {
                        this.mtd358[index] = (mtd266) var0[i];
                    }
                    else
                    {
                        this.mtd358[index] = ((mtd266) var0[i]).mtd253();
                    }
                    index++;
                }
            }
        }

        internal bool mtd112
        {
            get
            {
                if (this.mtd66 == SectionType.ReportDetail)
                {
                    return this.reportDetail.KeepTogether;
                }
                if (this.mtd66 == SectionType.GroupHeader)
                {
                    return this.groupHeader.KeepTogether;
                }
                if (this.mtd66 == SectionType.GroupFooter)
                {
                    return this.groupFooter.KeepTogether;
                }
                return ((this.mtd66 == SectionType.ReportFooter) && this.reportFooter.KeepTogether);
            }
        }

        internal object mtd361
        {
            get
            {
                if (this.mtd66 == SectionType.ReportDetail)
                {
                    return this.reportDetail;
                }
                if (this.mtd66 == SectionType.GroupHeader)
                {
                    return this.groupHeader;
                }
                if (this.mtd66 == SectionType.GroupFooter)
                {
                    return this.groupFooter;
                }
                if (this.mtd66 == SectionType.PageFooter)
                {
                    return this.pageFooter;
                }
                if (this.mtd66 == SectionType.ReportFooter)
                {
                    return this.reportFooter;
                }
                return null;
            }
        }

        internal string mtd91
        {
            get
            {
                return this._Section.Name;
            }
        }
    }
}

