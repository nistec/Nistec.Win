namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class ReportHeader : Section
    {
        public ReportHeader()
        {
            base._mtd91 = "ReportHeader";
            base._mtd66 = SectionType.ReportHeader;
        }

        public ReportHeader(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.ReportHeader;
        }

        public bool ShouldSerializeNewPage()
        {
            return (this.NewPage != Nistec.Printing.View.NewPage.None);
        }

        [mtd85(mtd88.mtd86), Description("Determines whether a new page should be inserted before and/or after printing the section"), Category("Appearance")]
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

