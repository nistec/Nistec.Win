namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class ReportDetail : Section
    {
        public ReportDetail()
        {
            base._mtd91 = "ReportDetail";
            base._mtd66 = SectionType.ReportDetail;
        }

        public ReportDetail(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.ReportDetail;
        }

        public bool ShouldSerializeNewPage()
        {
            return (this.NewPage != Nistec.Printing.View.NewPage.None);
        }

        [mtd85(mtd88.mtd86), Category("Apperance"), DefaultValue(false), Description("Determines whether the section should be printed on a single page")]
        public bool KeepTogether
        {
            get
            {
                return base._mtd112;
            }
            set
            {
                base._mtd112 = value;
            }
        }

        [Category("Apperance"), mtd85(mtd88.mtd86), Description("Determines whether a new page should be inserted befor and/or after printing the section")]
        public Nistec.Printing.View.NewPage NewPage
        {
            get
            {
                return base._mtd113;
            }
            set
            {
                base._mtd113 = value;
            }
        }
    }
}

