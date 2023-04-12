namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class mtd10
    {
        private static Bitmap var0;
        private static Graphics var1;

        public static void mtd12(ref Graphics var1, RectangleF var3, Color var4, string var5, Color var6, ref Font var7, StringFormat var8, ref Border var9)
        {
            float num = 2f;
            RectangleF layoutRectangle = var3;
            using (SolidBrush brush = new SolidBrush(var4))
            {
                var1.FillRectangle(brush, var3);
            }
            if (var1.PageUnit == GraphicsUnit.Inch)
            {
                Region clip = var1.Clip;
                GraphicsUnit pageUnit = var1.PageUnit;
                var1.PageUnit = GraphicsUnit.Point;
                layoutRectangle.X = (var3.X * 72f) + num;
                layoutRectangle.Y = (var3.Y * 72f) + num;
                layoutRectangle.Width = (var3.Width * 72f) - (2f * num);
                layoutRectangle.Height = var3.Height * 72f;
                using (SolidBrush brush2 = new SolidBrush(var6))
                {
                    var1.DrawString(var5, var7, brush2, layoutRectangle, var8);
                }
                var1.PageUnit = pageUnit;
                var1.Clip = clip;
            }
            else
            {
                layoutRectangle.X = var3.X + num;
                layoutRectangle.Y = var3.Y + num;
                layoutRectangle.Width = var3.Width - (2f * num);
                layoutRectangle.Height = var3.Height;
                using (SolidBrush brush3 = new SolidBrush(var6))
                {
                    var1.DrawString(var5, var7, brush3, layoutRectangle, var8);
                }
            }
            var9.Render(var1, var3);
        }

        internal static void mtd13(Graphics var1, Image var10, RectangleF var3)
        {
            if (var1.PageUnit == GraphicsUnit.Inch)
            {
                mtd14(ref var3, var1.DpiX, var1.DpiY);
            }
            else
            {
                mtd14(ref var3, var1.DpiX / ReportUtil.Dpi, var1.DpiY / ReportUtil.Dpi);
            }
            Region clip = var1.Clip;
            GraphicsUnit pageUnit = var1.PageUnit;
            var1.PageUnit = GraphicsUnit.Pixel;
            var1.DrawImage(var10, var3.X, var3.Y);
            var1.PageUnit = pageUnit;
            var1.Clip = clip;
        }

        internal static void mtd14(ref RectangleF var11, float var12, float var13)
        {
            var11.X *= var12;
            var11.Y *= var13;
            var11.Width *= var12;
            var11.Height *= var13;
        }

        public static void mtd15(ref Graphics var1, float var14, float var15, float var16, float var17, Color var18, LineStyle var19, float var20)
        {
            using (Pen pen = new Pen(var18, var21(ref var1, var20)))
            {
                pen.DashStyle = var22(var19);
                var1.DrawLine(pen, var14, var15, var16, var17);
            }
        }

        public static void mtd16(ref Graphics var1, RectangleF var3, ShapeStyle var23, Color var4, Color var18, LineStyle var19, float var20)
        {
            using (Pen pen = new Pen(var18, var21(ref var1, var20)))
            {
                pen.DashStyle = var22(var19);
                if (var23 == ShapeStyle.Rectangle)
                {
                    using (Brush brush = new SolidBrush(var4))
                    {
                        var1.FillRectangle(brush, var3);
                    }
                    var1.DrawRectangle(pen, var3.X, var3.Y, var3.Width, var3.Height);
                }
                else if (var23 == ShapeStyle.Ellipse)
                {
                    using (Brush brush2 = new SolidBrush(var4))
                    {
                        var1.FillEllipse(brush2, var3);
                    }
                    var1.DrawEllipse(pen, var3);
                }
            }
        }

        public static void mtd17(ref Graphics var1, RectangleF var3, ref Font var7, StringFormat var8, Color var4, Color var6, string var5, bool var24, RectangleF var25, RectangleF var26, ref Border var9, bool var27)
        {
            RectangleF ef;
            RectangleF ef2;
            using (SolidBrush brush = new SolidBrush(var4))
            {
                var1.FillRectangle(brush, var3);
            }
            if (var1.PageUnit == GraphicsUnit.Inch)
            {
                ef = new RectangleF(var26.X + var3.X, var26.Y + var3.Y, 0.125f, 0.125f);
                ef2 = new RectangleF((var25.X + var3.X) * 72f, (var25.Y + var3.Y) * 72f, var25.Width * 72f, var25.Height * 72f);
            }
            else if (!var27)
            {
                ef = new RectangleF((var26.X * ReportUtil.Dpi) + var3.X, (var26.Y * ReportUtil.Dpi) + var3.Y, 12f, 12f);
                ef2 = new RectangleF((var25.X * ReportUtil.Dpi) + var3.X, (var25.Y * ReportUtil.Dpi) + var3.Y, var25.Width * ReportUtil.Dpi, var25.Height * ReportUtil.Dpi);
            }
            else
            {
                ef = new RectangleF(var26.X + var3.X, var26.Y + var3.Y, 12f, 12f);
                ef2 = new RectangleF(var25.X + var3.X, var25.Y + var3.Y, var25.Width, var25.Height);
            }
            if (var24)
            {
                if (var1.PageUnit == GraphicsUnit.Inch)
                {
                    Region clip = var1.Clip;
                    var1.PageUnit = GraphicsUnit.Pixel;
                    var1.DrawImage(mtd11, (float) (ef.X * var1.DpiX), (float) (ef.Y * var1.DpiX));
                    var1.PageUnit = GraphicsUnit.Inch;
                    var1.Clip = clip;
                }
                else
                {
                    var1.DrawImage(mtd11, ef.X, ef.Y);
                }
            }
            using (Pen pen = new Pen(Color.Black, 1f / var1.DpiX))
            {
                var1.DrawRectangle(pen, ef.X, ef.Y, ef.Width, ef.Height);
            }
            using (SolidBrush brush2 = new SolidBrush(var6))
            {
                if (var1.PageUnit == GraphicsUnit.Inch)
                {
                    Region region2 = var1.Clip;
                    var1.PageUnit = GraphicsUnit.Point;
                    var1.DrawString(var5, var7, brush2, ef2, var8);
                    var1.PageUnit = GraphicsUnit.Inch;
                    var1.Clip = region2;
                }
                else
                {
                    var1.DrawString(var5, var7, brush2, ef2, var8);
                }
            }
            var9.Render(var1, var3);
        }

        public static void mtd18(Graphics var1, IntPtr var28, Color var4, Border var9, RectangleF var3, bool var29, bool var30)
        {
            Image image = null;
            var31 var;
            var31 var2;
            var32 var5;
            float width = var3.Width;
            float height = var3.Height;
            if (var1.PageUnit == GraphicsUnit.Inch)
            {
                width *= var1.DpiX;
                height *= var1.DpiY;
            }
            var.var34 = 0;
            var.var35 = 0;
            var.var36 = (int) ((width / var1.DpiX) * 1440f);
            var.var37 = (int) ((height / var1.DpiY) * 1440f);
            var2.var34 = 0;
            var2.var35 = 0;
            var2.var36 = var.var36;
            var2.var37 = var.var37;
            var5.var38 = 0;
            var5.var39 = -1;
            if (var29 || var30)
            {
                image = new Bitmap((int) width, (int) height);
            }
            else
            {
                image = mtd19(var1, width, height);
            }
            IntPtr hdc = Graphics.FromImage(image).GetHdc();
            try
            {
                var33 var6;
                var6.var40 = hdc;
                var6.var41 = hdc;
                var6.var42 = var;
                var6.var43 = var2;
                var6.var44 = var5;
                IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(var6));
                Marshal.StructureToPtr(var6, ptr, false);
                SendMessage(var28, 0x439, 1, ptr);
                Marshal.FreeCoTaskMem(ptr);
            }
            finally
            {
                //???
                Graphics graphics=var1;
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
            using (Brush brush = new SolidBrush(var4))
            {
                var1.FillRectangle(brush, var3);
            }
            if (image != null)
            {
                mtd13(var1, image, var3);
            }
            var9.Render(var1, var3);
        }

        public static Metafile mtd19(Graphics var1, float var53, float var54)
        {
            IntPtr hdc = var1.GetHdc();
            Metafile metafile = new Metafile(hdc, new RectangleF(0f, 0f, var53, var54), MetafileFrameUnit.Pixel);
            var1.ReleaseHdc(hdc);
            return metafile;
        }

        public static void mtd20(ContentAlignment var45, out StringAlignment var46, out StringAlignment var47)
        {
            if (var45 == ContentAlignment.TopLeft)
            {
                var47 = StringAlignment.Near;
                var46 = StringAlignment.Near;
            }
            else if (var45 == ContentAlignment.TopCenter)
            {
                var47 = StringAlignment.Near;
                var46 = StringAlignment.Center;
            }
            else if (var45 == ContentAlignment.TopRight)
            {
                var47 = StringAlignment.Near;
                var46 = StringAlignment.Far;
            }
            else if (var45 == ContentAlignment.MiddleLeft)
            {
                var47 = StringAlignment.Center;
                var46 = StringAlignment.Near;
            }
            else if (var45 == ContentAlignment.MiddleCenter)
            {
                var47 = StringAlignment.Center;
                var46 = StringAlignment.Center;
            }
            else if (var45 == ContentAlignment.MiddleRight)
            {
                var47 = StringAlignment.Center;
                var46 = StringAlignment.Far;
            }
            else if (var45 == ContentAlignment.BottomLeft)
            {
                var47 = StringAlignment.Far;
                var46 = StringAlignment.Near;
            }
            else if (var45 == ContentAlignment.BottomCenter)
            {
                var47 = StringAlignment.Far;
                var46 = StringAlignment.Center;
            }
            else
            {
                var47 = StringAlignment.Far;
                var46 = StringAlignment.Far;
            }
        }

        public static Image mtd21(byte[] var50)
        {
            Image image = null;
            if (var50 == null)
            {
                return null;
            }
            if (((var50.Length > 0x4e) && (var50[0] == 0x15)) && (var50[1] == 0x1c))
            {
                image = var51(var50, 0x4e);
            }
            if (image == null)
            {
                image = var51(var50, 0);
            }
            return image;
        }

        private static Graphics GetGraphics()
        {
            if (var1 == null)
            {
                var1 = Graphics.FromHwnd(IntPtr.Zero);
            }
            return var1;
        }

        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        private static void var2(ref Bitmap var49)
        {
            var49 = new Bitmap(12, 12, PixelFormat.Format24bppRgb);
            var49.MakeTransparent();
            var49.SetPixel(8, 2, Color.Black);
            var49.SetPixel(9, 2, Color.Black);
            var49.SetPixel(8, 3, Color.Black);
            var49.SetPixel(9, 3, Color.Black);
            var49.SetPixel(7, 4, Color.Black);
            var49.SetPixel(8, 4, Color.Black);
            var49.SetPixel(7, 5, Color.Black);
            var49.SetPixel(8, 5, Color.Black);
            var49.SetPixel(3, 6, Color.Black);
            var49.SetPixel(4, 6, Color.Black);
            var49.SetPixel(6, 6, Color.Black);
            var49.SetPixel(7, 6, Color.Black);
            var49.SetPixel(3, 7, Color.Black);
            var49.SetPixel(4, 7, Color.Black);
            var49.SetPixel(6, 7, Color.Black);
            var49.SetPixel(7, 7, Color.Black);
            var49.SetPixel(4, 8, Color.Black);
            var49.SetPixel(5, 8, Color.Black);
            var49.SetPixel(6, 8, Color.Black);
            var49.SetPixel(4, 9, Color.Black);
            var49.SetPixel(5, 9, Color.Black);
            var49.SetPixel(6, 9, Color.Black);
            var49.SetPixel(5, 10, Color.Black);
        }

        private static float var21(ref Graphics var1, float var48)
        {
            if (var1.PageUnit == GraphicsUnit.Inch)
            {
                return (var48 * 0.0138f);
            }
            return (var48 * 1.035f);
        }

        private static DashStyle var22(LineStyle var19)
        {
            if (var19 != LineStyle.Solid)
            {
                if (var19 == LineStyle.Dash)
                {
                    return DashStyle.Dash;
                }
                if (var19 == LineStyle.DashDot)
                {
                    return DashStyle.DashDot;
                }
                if (var19 == LineStyle.DashDotDot)
                {
                    return DashStyle.DashDotDot;
                }
                if (var19 == LineStyle.Dot)
                {
                    return DashStyle.Dot;
                }
            }
            return DashStyle.Solid;
        }

        private static Image var51(byte[] var50, int var52)
        {
            MemoryStream stream = null;
            if (var50 == null)
            {
                return null;
            }
            try
            {
                stream = new MemoryStream(var50, var52, var50.Length - var52);
                return Image.FromStream(stream);
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap mtd11
        {
            get
            {
                if (var0 == null)
                {
                    var2(ref var0);
                }
                return var0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct var31
        {
            public int var35;
            public int var34;
            public int var36;
            public int var37;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct var32
        {
            public int var38;
            public int var39;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct var33
        {
            public IntPtr var40;
            public IntPtr var41;
            public mtd10.var31 var42;
            public mtd10.var31 var43;
            public mtd10.var32 var44;
        }
    }
}

