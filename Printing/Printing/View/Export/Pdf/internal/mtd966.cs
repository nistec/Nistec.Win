namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class mtd966 : mtd942
    {
        internal mtd966()
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
                if (var2.SizeMode == SizeMode.Stretch)
                {
                    var4.mtd968(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { mtd620.mtd621(ef.Width), mtd620.mtd621(ef.Height), mtd620.mtd621(ef.X), mtd620.mtd621((float) (ef.Y - ef.Height)) }));
                    var4.mtd968(string.Format("/{0} Do", mtd.mtd763));
                }
                else
                {
                    float num5;
                    float num6;
                    float num7;
                    float num8;
                    if (var2.SizeMode == SizeMode.Zoom)
                    {
                        mtd969(out num5, out num6, out num7, out num8, ef, (float) mtd.mtd30, (float) mtd.mtd31);
                        var4.mtd968(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { mtd620.mtd621(num7), mtd620.mtd621(num8), mtd620.mtd621(num5), mtd620.mtd621(num6) }));
                        var4.mtd968(string.Format("/{0} Do", mtd.mtd763));
                    }
                    else
                    {
                        num7 = (mtd.mtd30 * 72f) / mtd.mtd781;
                        num8 = (mtd.mtd31 * 72f) / mtd.mtd782;
                        var5(out num5, out num6, ef, num7, num8, var2.PictureAlignment);
                        var4.mtd968(string.Format("{0} 0 0 {1} {2} {3} cm ", new object[] { mtd620.mtd621(num7), mtd620.mtd621(num8), mtd620.mtd621(num5), mtd620.mtd621(num6) }));
                        var4.mtd968(string.Format("/{0} Do", mtd.mtd763));
                    }
                }
                mtd942.mtd951(ref var3, ref var4);
                mtd952.mtd23(x, num2, num3, num4, var2.Border, ref var3, ref var4);
            }
        }

        internal static void mtd969(out float var6, out float var7, out float var12, out float var13, RectangleF var8, float var9, float var10)
        {
            var13 = var8.Height;
            var12 = (var8.Height * var9) / var10;
            if (var12 > var8.Width)
            {
                var12 = var8.Width;
                var13 = (var8.Width * var10) / var9;
                var6 = var8.X;
                var7 = (var8.Y - var8.Height) + ((var8.Height - var13) / 2f);
            }
            else
            {
                var6 = var8.X + ((var8.Width - var12) / 2f);
                var7 = var8.Y - var8.Height;
            }
        }

        private static void var5(out float var6, out float var7, RectangleF var8, float var9, float var10, PictureAlignment var11)
        {
            if (var11 == PictureAlignment.TopLeft)
            {
                var6 = var8.X;
                var7 = var8.Y - var10;
            }
            else if (var11 == PictureAlignment.TopRight)
            {
                var6 = var8.Right - var9;
                var7 = var8.Y - var10;
            }
            else if (var11 == PictureAlignment.BottomLeft)
            {
                var6 = var8.X;
                var7 = var8.Y - var8.Height;
            }
            else if (var11 == PictureAlignment.BottomRight)
            {
                var6 = var8.Right - var9;
                var7 = var8.Y - var8.Height;
            }
            else if (var11 == PictureAlignment.Center)
            {
                var6 = var8.X + ((var8.Width - var9) / 2f);
                var7 = (var8.Y - ((var8.Height - var10) / 2f)) - var10;
            }
            else
            {
                var6 = var8.X;
                var7 = var8.Y - var10;
            }
        }
    }
}

