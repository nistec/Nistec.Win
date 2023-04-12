namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class GroupFooter : Section
    {
        public GroupFooter()
        {
            base._mtd66 = SectionType.GroupFooter;
        }

        public GroupFooter(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.GroupFooter;
        }

        public bool ShouldSerializeNewPage()
        {
            return (this.NewPage != Nistec.Printing.View.NewPage.None);
        }

        [mtd85(mtd88.mtd86), DefaultValue(false), Category("Appearance"), Description("Determines whether the section should be printed on a single page")]
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

        [Description("Determines whether a new page should be inserted before and/or after printing the section"), mtd85(mtd88.mtd86), Category("Appearance")]
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

        [Description("Determines whether the section should be printed at the bottom of the page immediately before any page footer section"), DefaultValue(false), mtd85(mtd88.mtd86), Category("Appearance")]
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

