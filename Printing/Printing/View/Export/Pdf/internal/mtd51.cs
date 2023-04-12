namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd51 : mtd942
    {
        internal static void mtd23(float var0, float var1, string var2, Color var3, mtd641 var4, float var5, bool var6, bool var7, ref mtd944 var8, ref mtd742 var9)
        {
            var4 = var8.mtd953(var4);
            var4 = mtd942.mtd963(var4, 8f, ref var8, ref var9);
            var4.mtd936(var2);
            mtd942.mtd964(var3, ref var8, ref var9);
            if (var7)
            {
                var2 = mtd942.mtd981(var2);
            }
            if (!var6)
            {
                mtd942.mtd965(var9, var0, var8.mtd947(var1), 0f, var4.mtd937(var2), false);
            }
            else
            {
                float num = 1f;
                if (var4.mtd934 == FontStyle.Bold)
                {
                    num = 1.25f;
                }
                mtd942.mtd975(num, var3, LineStyle.Solid, ref var8, ref var9);
                mtd942.mtd965(var9, var0, var8.mtd947(var1), var4.mtd817(var2, var5), var4.mtd937(var2), true);
            }
        }
    }
}

