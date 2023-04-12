namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class GroupHeader : Section
    {
        private int _var0;

        public GroupHeader()
        {
            base._mtd66 = SectionType.GroupHeader;
        }

        public GroupHeader(string name)
        {
            base._mtd91 = name;
            base._mtd66 = SectionType.GroupHeader;
        }

        public bool ShouldSerializeGroupKeepTogether()
        {
            return (this.GroupKeepTogether != Nistec.Printing.View.GroupKeepTogether.None);
        }

        public bool ShouldSerializeNewPage()
        {
            return (this.NewPage != Nistec.Printing.View.NewPage.None);
        }

        [mtd85(mtd88.mtd86), Category("Appearance"), Description("Determines whether the section and it's footer will print as a single block on the same page")]
        public Nistec.Printing.View.GroupKeepTogether GroupKeepTogether
        {
            get
            {
                return base._mtd115;
            }
            set
            {
                base._mtd115 = value;
            }
        }

        [mtd85(mtd88.mtd86), Browsable(false)]
        public int Index
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        [Category("Appearance"), DefaultValue(false), mtd85(mtd88.mtd86), Description("Determines whether the section should be printed on a sinle page")]
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

        [mtd85(mtd88.mtd86), Category("Appearance"), Description("Determines whether a new page should be inserted before and/or after printing the section")]
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

