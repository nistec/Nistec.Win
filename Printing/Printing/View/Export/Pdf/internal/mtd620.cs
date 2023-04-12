namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    internal class mtd620
    {
        internal static char[] mtd644 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        internal static Encoding mtd645 = Encoding.GetEncoding(0x4e4);

        internal mtd620()
        {
        }

        internal static string mtd621(double var0)
        {
            if (!var1(var0))
            {
                return var0.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return var0.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string mtd621(float var0)
        {
            if (!var1((double) var0))
            {
                return var0.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return var0.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string mtd621(ushort var0)
        {
            return var0.ToString(NumberFormatInfo.InvariantInfo);
        }

        internal static string mtd646(DateTime var2)
        {
            string str = "D:";
            return ((((str + var2.Year.ToString("0000")) + var2.Month.ToString("00") + var2.Day.ToString("00")) + var2.Hour.ToString("00") + var2.Minute.ToString("00")) + var2.Second.ToString("00") + var3(var2));
        }

        internal static string mtd647(RectangleF var4)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { mtd621(var4.Left), mtd621(var4.Top), mtd621(var4.Right), mtd621(var4.Bottom) });
        }

        internal static string mtd647(int var9, int var10, int var11, int var12)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { var9, var10, var11, var12 });
        }

        internal static string mtd647(float var5, float var6, float var7, float var8)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { mtd621(var5), mtd621(var6), mtd621(var7), mtd621(var8) });
        }

        internal static string mtd648(RectangleF var4)
        {
            return string.Format("{0} {1} {2} {3}", new object[] { mtd621(var4.Left), mtd621((float) (var4.Y - var4.Height)), mtd621(var4.Width), mtd621(var4.Height) });
        }

        internal static string mtd648(float var13, float var14, float var15, float var16)
        {
            return string.Format("{0} {1} {2} {3}", new object[] { mtd621(var13), mtd621((float) (var14 - var16)), mtd621(var15), mtd621(var16) });
        }

        internal static string mtd649(PointF var17)
        {
            return string.Format("{0} {1}", mtd621(var17.X), mtd621(var17.Y));
        }

        internal static string mtd649(float var13, float var14)
        {
            return string.Format("{0} {1}", mtd621(var13), mtd621(var14));
        }

        internal static string mtd650(bool var18)
        {
            if (var18)
            {
                return "true";
            }
            return "false";
        }

        internal static string mtd651(char var19)
        {
            if (var19 == '(')
            {
                return @"\(";
            }
            if (var19 == ')')
            {
                return @"\)";
            }
            if (var19 == '\\')
            {
                return @"\\";
            }
            if (var19 == '\n')
            {
                return @"\n";
            }
            if (var19 == '\r')
            {
                return @"\r";
            }
            if (var19 == '\t')
            {
                return @"\t";
            }
            if (var19 == '\b')
            {
                return @"\b";
            }
            if (var19 == '\x0012')
            {
                return @"\f";
            }
            return Convert.ToString(var19);
        }

        internal static string mtd652(string var20)
        {
            string str = string.Empty;
            for (int i = 0; i < var20.Length; i++)
            {
                str = str + mtd651(var20[i]);
            }
            return str;
        }

        internal static string mtd653(string var20)
        {
            string str = string.Empty;
            byte[] bytes = mtd645.GetBytes(var20);
            for (int i = 0; i < bytes.Length; i++)
            {
                str = str + mtd644[bytes[i] >> 4] + mtd644[bytes[i] & 15];
            }
            return string.Format("{0}{1}{2}", "<", str, ">");
        }

        private static bool var1(double var0)
        {
            return ((Math.Round(var0, 0) - var0) == 0.0);
        }

        private static string var3(DateTime var2)
        {
            TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(var2);
            string str = "";
            if (utcOffset.Ticks == 0L)
            {
                return "Z";
            }
            if (utcOffset.Ticks > 0L)
            {
                str = str + "+";
            }
            return ((str + utcOffset.Hours.ToString("D2") + "'") + utcOffset.Minutes.ToString("D2") + "'");
        }
    }
}

