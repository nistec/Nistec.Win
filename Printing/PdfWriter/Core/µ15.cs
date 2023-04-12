namespace MControl.Printing.Pdf.Core
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.IO;
    using System.Text;

    internal class A15
    {
        private static float[] _b0;
        internal static char[] A16 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        internal static Encoding A17 = Encoding.GetEncoding(0x4e4);

        static A15()
        {
            Bitmap image = new Bitmap(1, 1);
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
            {
                PointF[] pts = new PointF[] { new PointF(1f, 1f) };
                GraphicsContainer container = graphics.BeginContainer(new Rectangle(0, 0, 1, 1), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
                graphics.PageUnit = GraphicsUnit.Inch;
                graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, pts);
                graphics.EndContainer(container);
                float x = pts[0].X;
                _b0 = new float[] { x / 75f, x / 300f, x, x / 25.4f, x / 2.54f, 1f, x / 72f };
            }
            image.Dispose();
        }

        internal A15()
        {
        }

        internal static string A18(double b1)
        {
            if (!b2(b1))
            {
                return b1.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return b1.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string A18(int b1)
        {
            return b1.ToString(NumberFormatInfo.InvariantInfo);
        }

        internal static string A18(float b1)
        {
            if (!b2((double) b1))
            {
                return b1.ToString("0.000", CultureInfo.InvariantCulture);
            }
            return b1.ToString("0", CultureInfo.InvariantCulture);
        }

        internal static string A18(ushort b1)
        {
            return b1.ToString(NumberFormatInfo.InvariantInfo);
        }

        internal static float A19(float b1)
        {
            return (b1 * 0.01745329f);
        }

        internal static string A20(DateTime b3)
        {
            string str = "D:";
            return ((((str + b3.Year.ToString("0000")) + b3.Month.ToString("00") + b3.Day.ToString("00")) + b3.Hour.ToString("00") + b3.Minute.ToString("00")) + b3.Second.ToString("00") + b4(b3));
        }

        internal static string A21(RectangleF b5)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { A18(b5.Left), A18(b5.Top), A18(b5.Right), A18(b5.Bottom) });
        }

        internal static string A21(int b10, int b11, int b12, int b13)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { b10, b11, b12, b13 });
        }

        internal static string A21(float b6, float b7, float b8, float b9)
        {
            return string.Format("[ {0} {1} {2} {3} ]", new object[] { A18(b6), A18(b7), A18(b8), A18(b9) });
        }

        internal static string A22(RectangleF b5)
        {
            return string.Format("{0} {1} {2} {3}", new object[] { A18(b5.Left), A18((float) (b5.Y - b5.Height)), A18(b5.Width), A18(b5.Height) });
        }

        internal static string A22(float b14, float b15, float b16, float b17)
        {
            return string.Format("{0} {1} {2} {3}", new object[] { A18(b14), A18((float) (b15 - b17)), A18(b16), A18(b17) });
        }

        internal static string A23(PointF b18)
        {
            return string.Format("{0} {1}", A18(b18.X), A18(b18.Y));
        }

        internal static string A23(float b14, float b15)
        {
            return string.Format("{0} {1}", A18(b14), A18(b15));
        }

        internal static string A24(bool b19)
        {
            if (b19)
            {
                return "true";
            }
            return "false";
        }

        internal static string A25(char b20)
        {
            if (b20 == '(')
            {
                return @"\(";
            }
            if (b20 == ')')
            {
                return @"\)";
            }
            if (b20 == '\\')
            {
                return @"\\";
            }
            if (b20 == '\n')
            {
                return @"\n";
            }
            if (b20 == '\r')
            {
                return @"\r";
            }
            if (b20 == '\t')
            {
                return @"\t";
            }
            if (b20 == '\b')
            {
                return @"\b";
            }
            if (b20 == '\x0012')
            {
                return @"\f";
            }
            return Convert.ToString(b20);
        }

        internal static string A26(string b21)
        {
            string str = string.Empty;
            for (int i = 0; i < b21.Length; i++)
            {
                str = str + A25(b21[i]);
            }
            return str;
        }

        internal static string A27(string b21)
        {
            string str = string.Empty;
            byte[] bytes = A17.GetBytes(b21);
            for (int i = 0; i < bytes.Length; i++)
            {
                str = str + A16[bytes[i] >> 4] + A16[bytes[i] & 15];
            }
            return string.Format("{0}{1}{2}", "<", str, ">");
        }

        internal static string A28(string b21)
        {
            StringBuilder builder = new StringBuilder("");
            byte[] bytes = Encoding.Unicode.GetBytes(b21);
            for (int i = 0; i < bytes.Length; i += 2)
            {
                builder.AppendFormat("{0:X2}{1:X2}", bytes[i + 1], bytes[i]);
            }
            return string.Format("{0}{1}{2}", "<", builder.ToString(), ">");
        }

        internal static string A29(float[] b1)
        {
            int length = b1.Length;
            string str = "[";
            for (int i = 0; i < length; i++)
            {
                if (i > 0)
                {
                    str = str + " ";
                }
                str = str + A18(b1[i]);
            }
            return (str + "]");
        }

        internal static float A30(float value, GraphicsUnits from, GraphicsUnits to)
        {
            return b22(b23(value, from), to);
        }

        internal static string A31(string b24)
        {
            if ((b24 != null) && (b24.Length > 0))
            {
                b24 = b24.Replace(@"\", "/").Replace(":", string.Empty);
                if (b24.Substring(0, 2) == "//")
                {
                    b24 = b24.Remove(1, 1);
                }
                if (b24.Substring(0, 1) != "/")
                {
                    b24 = "/" + b24;
                }
            }
            return b24;
        }

        internal static byte[] A32(Stream b25, bool b26)
        {
            byte[] buffer = null;
            if (b25 != null)
            {
                b25.Position = 0L;
                buffer = new byte[b25.Length];
                b25.Read(buffer, 0, (int) b25.Length);
                if (b26)
                {
                    buffer = Encoding.Convert(Encoding.Unicode, Encoding.BigEndianUnicode, buffer);
                }
            }
            return buffer;
        }

        private static bool b2(double b1)
        {
            return ((Math.Round(b1, 0) - b1) == 0.0);
        }

        private static float b22(float value, GraphicsUnits to)
        {
            return (value / _b0[(int) to]);
        }

        private static float b23(float value, GraphicsUnits from)
        {
            return (value * _b0[(int) from]);
        }

        private static string b4(DateTime b3)
        {
            TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(b3);
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

