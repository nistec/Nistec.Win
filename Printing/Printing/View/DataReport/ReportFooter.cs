namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class ReportFooter : Section
    {
        public ReportFooter()
        {
            base._mtd91 = "ReportFooter";
            base._mtd66 = SectionType.ReportFooter;
        }

        public ReportFooter(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.ReportFooter;
        }

        public bool ShouldSerializeNewPage()
        {
            return (this.NewPage != Nistec.Printing.View.NewPage.None);
        }

        [Description("Determines whether the section should be printed on a single page"), Category("Apperarance"), mtd85(mtd88.mtd86), DefaultValue(false)]
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

        [Category("Appearance"), mtd85(mtd88.mtd86), Description("Determines whether a new page should be inserted before and/or after printing the section")]
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

        [mtd85(mtd88.mtd86), Category("Appearance"), Description("Determines whether the section should be printed at the bottom of the page immediately before any page footer section"), DefaultValue(false)]
        public bool PrintAtBottom
        {
            get
            {
                return base._mtd114;
            }
            set
            {
                base._mtd114 = value;
            }
        }
    }
}

