namespace Nistec.Printing.View
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    internal class mtd85 : Attribute
    {
        public static readonly mtd85 mtd86 = new mtd85(mtd88.mtd86);
        public static readonly mtd85 mtd89 = new mtd85(mtd88.mtd89);
        private mtd88 var0;

        public mtd85(mtd88 var0)
        {
            this.var0 = var0;
        }

        public mtd88 mtd90
        {
            get
            {
                return this.var0;
            }
        }
    }
}

