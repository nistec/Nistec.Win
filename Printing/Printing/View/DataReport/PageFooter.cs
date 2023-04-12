namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class PageFooter : Section
    {
        public PageFooter()
        {
            base._mtd91 = "PageFooter";
            base._mtd66 = SectionType.PageFooter;
        }

        public PageFooter(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.PageFooter;
        }
    }
}

