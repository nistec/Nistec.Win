namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class PageHeader : Section
    {
        public PageHeader()
        {
            base._mtd91 = "PageHeader";
            base._mtd66 = SectionType.PageHeader;
        }

        public PageHeader(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.PageHeader;
        }
    }
}

