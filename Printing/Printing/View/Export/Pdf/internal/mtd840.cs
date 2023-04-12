namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd840 : mtd841
    {
        private ArrayList var0 = new ArrayList();

        internal mtd840()
        {
        }

        internal override void mtd842(mtd825 var3, int var4)
        {
            int num = var3.mtd832;
            this.var2(var3);
            var3.mtd833(num);
            base.mtd842(var3, var4);
        }

        private void var2(mtd825 var3)
        {
            ushort num = 0;
            do
            {
                num = var3.mtd835();
                this.var0.Add(var3.mtd835());
                if ((num & 1) != 0)
                {
                    var3.mtd834(4);
                }
                else
                {
                    var3.mtd834(2);
                }
                if ((num & 8) != 0)
                {
                    var3.mtd834(2);
                }
                else if ((num & 0x40) != 0)
                {
                    var3.mtd834(4);
                }
                else if ((num & 0x80) != 0)
                {
                    var3.mtd834(8);
                }
            }
            while ((num & 0x20) != 0);
        }

        internal int mtd166
        {
            get
            {
                return this.var0.Count;
            }
        }

        internal ushort this[int var1]
        {
            get
            {
                return (ushort) this.var0[var1];
            }
        }
    }
}

