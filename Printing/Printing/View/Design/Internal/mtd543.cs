namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    //mtd543
    internal class GroupSectionDesigner
    {
        internal GroupHeader mtd296;
        internal GroupFooter mtd305;
        internal int mtd544;

        internal GroupSectionDesigner(GroupHeader var1, GroupFooter var2)
        {
            this.mtd305 = var2;
            this.mtd296 = var1;
        }

        internal GroupSectionDesigner(int var0, GroupHeader var1)
        {
            this.mtd296 = var1;
            this.mtd544 = var0;
        }
    }
}

