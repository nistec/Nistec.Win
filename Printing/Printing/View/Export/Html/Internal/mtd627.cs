namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.IO;

    internal class mtd627
    {
        internal mtd627()
        {
        }

        internal static void mtd22(int var0, PropDoc var1, ref StreamWriter var2)
        {
            var2.Write("position: absolute; ");
            var2.Write(string.Format("left: {0}in; top: {1}in; width: {2}in; height: {3}in; ", new object[] { mtd620.mtd621(var1.Left), mtd620.mtd621(var1.Top), mtd620.mtd621(var1.Width), mtd620.mtd621(var1.Height) }));
            var2.Write("overflow: hidden; ");
            if (!var1.Visible)
            {
                var2.Write("visibility: hidden; ");
            }
            var2.Write(string.Format("z-index: {0}; ", mtd620.mtd621(var0)));
        }

        internal static void mtd22(float var5, float var6, ref StreamWriter var2)
        {
            var2.Write("position: relative; ");
            var2.Write(string.Format("width: {0}in; height: {1}in; ", mtd620.mtd621(var5), mtd620.mtd621(var6)));
            var2.Write("overflow: hidden; ");
        }

        internal static void mtd22(int var0, float var8, float var5, mtd141 var9, ref StreamWriter var2)
        {
            var2.Write("position: absolute; ");
            var2.Write(string.Format("top: {0}in; width: {1}in; height: {2}in; ", mtd620.mtd621((float)(var9.mtd29 - var8)), mtd620.mtd621(var5), mtd620.mtd621(var9.Height)));
            var2.Write("overflow: hidden; ");
            if (!var9.mtd86)
            {
                var2.Write("visibility: hidden; ");
            }
            var2.Write(string.Format("z-index: {0}; ", mtd620.mtd621(var0)));
        }

        internal static void mtd22(int var0, float var3, float var4, float var5, float var6, bool var7, ref StreamWriter var2)
        {
            var2.Write("position: absolute; ");
            var2.Write(string.Format("left: {0}in; top: {1}in; width: {2}in; height: {3}in; ", new object[] { mtd620.mtd621(var3), mtd620.mtd621(var4), mtd620.mtd621(var5), mtd620.mtd621(var6) }));
            var2.Write("overflow: hidden; ");
            if (!var7)
            {
                var2.Write("visibility: hidden; ");
            }
            var2.Write(string.Format("z-index: {0}; ", mtd620.mtd621(var0)));
        }
    }
}

