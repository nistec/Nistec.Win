namespace Nistec.Printing.View
{
    using System;

    internal class mtd69
    {
        private static mtd26 _var0;
        private static mtd56 _var1;
        private static object _var2;
        private static object _var3;

        internal static void mtd70(mtd26 var4, mtd56 var5, object var6, object var7)
        {
            _var0 = var4;
            _var1 = var5;
            _var2 = var6;
            _var3 = var7;
        }

        internal static void mtd71(object var8, mtd24 var9)
        {
            if (var9 != null)
            {
                var9(var8, new mtd25(_var0, _var1, _var2, _var3));
            }
        }
    }
}

