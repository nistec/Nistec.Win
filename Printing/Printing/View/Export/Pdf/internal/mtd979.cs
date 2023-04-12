namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd979 : mtd942
    {
        internal mtd979()
        {
        }

        internal static void mtd23(float var0, float var1, PropDoc var2, ref mtd944 var3, ref mtd742 var4)
        {
            float x = (var2.Left * 72f) + var0;
            float num2 = (var2.Top * 72f) + var1;
            float num3 = var2.Width * 72f;
            float num4 = var2.Height * 72f;
            if (var2.Image != null)
            {
                mtd643 mtd = var3.mtd967(var2.Image);
                float width = num3;
                float height = num4;
                if (num3 <= 0f)
                {
                    width = var3.mtd30;
                }
                if (num4 <= 0f)
                {
                    height = var3.mtd31;
                }
                RectangleF ef = new RectangleF(x, var3.mtd947(num2), width, height);
                mtd942.mtd945(ref var3, ref var4);
                mtd942.mtd946(var4, ef.X, ef.Y, ef.Width, ef.Height);
                float num7 = (mtd.mtd30 * 72f) / mtd.mtd781;
                float num8 = (mtd.mtd31 * 72f) / mtd.mtd782;
                float num5 = ef.X;
                float num6 = ef.Y - num8;
                var4.mtd968(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { mtd620.mtd621(num7), mtd620.mtd621(num8), mtd620.mtd621(num5), mtd620.mtd621(num6) }));
                var4.mtd968(string.Format("/{0} Do", mtd.mtd763));
                mtd942.mtd951(ref var3, ref var4);
                mtd952.mtd23(x, num2, num3, num4, var2.Border, ref var3, ref var4);
            }
        }
    }
}

