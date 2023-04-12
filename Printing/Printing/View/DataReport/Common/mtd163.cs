namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    internal class mtd163
    {
        internal mtd164 mtd167;
        internal int Index;//mtd190;
        internal mtd289 mtd242;
        internal bool mtd286;
        internal float mtd29;
        internal float Height;//mtd31;
        internal Color BackColor;//mtd39;
        internal SectionType _SectionType;
        internal bool mtd86;

        internal mtd163(int var0)
        {
            this.mtd167 = new mtd164(var0);
        }

        internal mtd163(ref Section s)
        {
            this.BackColor = s.BackColor;
            this.Height = s.Height;
            this._SectionType = s.Type;
            this.mtd242 = new mtd289(ref s);
        }

        internal mtd163 mtd105(int var3)
        {
            mtd163 mtd = new mtd163(this.mtd167.mtd210);
            mtd.Index = var3;
            mtd.mtd29 = this.mtd29;
            mtd.Height = this.Height;
            mtd._SectionType = this._SectionType;
            mtd.mtd242 = this.mtd242;
            mtd.mtd86 = this.mtd242._Section.Visible;
            mtd.BackColor = this.mtd242._Section.BackColor;
            return mtd;
        }

        internal mtd163 mtd105(int var3, float var1)
        {
            mtd163 mtd = new mtd163(this.mtd167.mtd210);
            mtd.Index = var3;
            mtd.mtd29 = var1;
            mtd.Height = this.Height;
            mtd._SectionType = this._SectionType;
            mtd.mtd242 = this.mtd242;
            mtd.mtd86 = this.mtd242._Section.Visible;
            mtd.BackColor = this.mtd242._Section.BackColor;
            return mtd;
        }

        internal mtd163 mtd105(float var1, bool var2)
        {
            mtd163 mtd = new mtd163(this.mtd167.mtd210);
            mtd.Index = this.Index;
            mtd.mtd29 = var1;
            mtd.Height = this.Height;
            mtd._SectionType = this._SectionType;
            mtd.mtd242 = this.mtd242;
            if (var2)
            {
                mtd.mtd86 = this.mtd86;
                mtd.BackColor = this.BackColor;
                return mtd;
            }
            mtd.mtd86 = this.mtd242._Section.Visible;
            mtd.BackColor = this.mtd242._Section.BackColor;
            return mtd;
        }

        internal string mtd91
        {
            get
            {
                return string.Format("{0}{1}", this.mtd242.mtd91, this.Index);
            }
        }
    }
}

