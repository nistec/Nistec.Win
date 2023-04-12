namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class mtd931 : mtd811
    {
        private float _var10;
        private bool _var11;
        private Font _var12;
        private bool[] _var13;
        private bool _var6;
        private string _var8;
        private mtd821 _var9;
        private static string var7 = "RptXX";

        internal mtd931(Font var12, bool var14)
        {
            base._mtd932 = var14;
            this._var11 = true;
            this._var6 = false;
            this._var12 = var12;
            base._mtd808 = var12.FontFamily.Name;
            base._mtd809 = var12.Style;
            this._var13 = null;
            this.var15();
        }

        internal mtd931(string var16, string var17, FontStyle var18, bool var14)
        {
            base._mtd932 = var14;
            this._var11 = true;
            this._var6 = false;
            base._mtd808 = var16;
            base._mtd809 = var18;
            this._var13 = null;
            this.var15(var17);
        }

        internal override void mtd710(ref mtd711 var25)
        {
            if (!this._var6)
            {
                this._var8 = base._mtd808.Replace(" ", "");
                if (base._mtd932)
                {
                    this.var26(var25);
                }
                else
                {
                    this.var27(var25);
                }
            }
            else
            {
                this._var6 = true;
            }
        }

        internal override void mtd780()
        {
            int num;
            mtd757 mtd1 = base._mtd756.mtd757;
            mtd1.mtd759 = (num = mtd1.mtd759) + 1;
            base._mtd759 = num;
            if (base._mtd932)
            {
                base._mtd756.mtd757.mtd759 = base._mtd759 + 5;
                this._var11 = false;
                this._var13 = new bool[0x10000];
            }
            else
            {
                base._mtd756.mtd757.mtd759 = base._mtd759 + 2;
            }
        }

        internal override float mtd815(char var21)
        {
            return (this._var9.mtd816(var21) * this._var10);
        }

        internal override float mtd815(ushort var22)
        {
            return (this._var9.mtd816(var22) * this._var10);
        }

        internal override float mtd816(char var21, float var23)
        {
            return ((this.mtd815(var21) / 1000f) * var23);
        }

        internal override float mtd817(string var24, float var23)
        {
            float num = 0f;
            for (int i = 0; i < var24.Length; i++)
            {
                num += this.mtd816(var24[i], var23);
            }
            return num;
        }

        internal override void mtd935(char var21)
        {
            if (base._mtd932)
            {
                this._var13[var21] = true;
            }
            if (this._var11 && (var21 > '\x007f'))
            {
                this._var11 = false;
            }
        }

        internal override void mtd936(string var24)
        {
            for (int i = 0; i < var24.Length; i++)
            {
                this.mtd935(var24[i]);
            }
        }

        internal override string mtd937(string var24)
        {
            string str = string.Empty;
            if (this._var11)
            {
                for (int j = 0; j < var24.Length; j++)
                {
                    str = str + mtd620.mtd651(var24[j]);
                }
                return string.Format("{0}{1}{2}", "(", str, ")");
            }
            if (!base._mtd932)
            {
                return mtd620.mtd653(var24);
            }
            for (int i = 0; i < var24.Length; i++)
            {
                str = str + this._var9.mtd883(var24[i]).ToString("X4");
            }
            return string.Format("{0}{1}{2}", "<", str, ">");
        }

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr var1);
        [DllImport("gdi32.dll")]
        public static extern uint GetFontData(IntPtr var0, uint var2, uint var3, byte[] var4, uint var5);
        [DllImport("gdi32")]
        private static extern IntPtr SelectObject(IntPtr var0, IntPtr var1);
        private void var15()
        {
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            try
            {
                IntPtr hdc = graphics.GetHdc();
                try
                {
                    SelectObject(hdc, this._var12.ToHfont());
                    uint num = GetFontData(hdc, 0, 0, null, 0);
                    if (num < 0)
                    {
                        throw new Exception("Error when reading font file");
                    }
                    byte[] buffer = new byte[num];
                    if (GetFontData(hdc, 0, 0, buffer, num) != buffer.Length)
                    {
                        throw new Exception("Error when reading font file");
                    }
                    this.var19(buffer);
                }
                finally
                {
                    graphics.ReleaseHdc(hdc);
                }
            }
            finally
            {
                graphics.Dispose();
            }
        }

        private void var15(string var17)
        {
            FileStream stream = new FileStream(var17, FileMode.Open, FileAccess.Read);
            if (stream.Length > 0L)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.var19(buffer);
            }
        }

        private void var19(byte[] var20)
        {
            this._var9 = new mtd821();
            this._var9.mtd842(var20);
            this._var10 = 1000f / ((float) this._var9.mtd862.mtd895);
            base._mtd812 = (int) Math.Round((double) (this._var9.mtd863.mtd896 * this._var10));
            base._mtd813 = (int) Math.Round((double) (this._var9.mtd863.mtd897 * this._var10));
        }

        private void var26(mtd711 var25)
        {
            ushort[] numArray = var31(this._var13);
            base._mtd756.mtd757.mtd758(var25.mtd32, 0);
            var25.mtd715(string.Format("{0} 0 obj", base.mtd759));
            var25.mtd715("<<");
            var25.mtd715("/Type /Font ");
            var25.mtd715("/Subtype /Type0 ");
            var25.mtd715(string.Format("/BaseFont /{0} ", this.mtd933));
            var25.mtd715(string.Format("/Name /{0} ", base._mtd818));
            var25.mtd715("/Encoding /Identity-H ");
            int num = base.mtd759 + 1;
            var25.mtd715(string.Format("/ToUnicode {0} 0 R ", num));
            int num2 = base.mtd759 + 2;
            var25.mtd715(string.Format("/DescendantFonts [{0} 0 R] ", num2));
            var25.mtd715(">>");
            var25.mtd715("endobj");
            this.var32(num, numArray, var25);
            this.var33(num2, numArray, var25);
        }

        private void var27(mtd711 var25)
        {
            base._mtd756.mtd757.mtd758(var25.mtd32, 0);
            var25.mtd715(string.Format("{0} 0 obj", base.mtd759));
            var25.mtd715("<<");
            var25.mtd715("/Type /Font ");
            var25.mtd715(string.Format("/BaseFont /{0} ", this.mtd933));
            var25.mtd715("/Subtype /TrueType ");
            var25.mtd715(string.Format("/Name /{0} ", base._mtd818));
            var25.mtd715(string.Format("/FirstChar {0} ", 0x20));
            var25.mtd715(string.Format("/LastChar {0} ", 0xff));
            var25.mtd710("/Widths [");
            this.var28(var25, null);
            var25.mtd715("]");
            var25.mtd715("/Encoding /PDFDocEncoding ");
            int num = base._mtd759 + 1;
            var25.mtd715(string.Format("/FontDescriptor {0} 0 R ", num));
            var25.mtd715(">>");
            var25.mtd715("endobj");
            this.var30(num, var25, null);
        }

        private void var28(mtd711 var25, ushort[] var29)
        {
            int num = 0;
            if (var29 == null)
            {
                for (int i = 0x20; i < 0x100; i++)
                {
                    if (num == 0x10)
                    {
                        num = 0;
                        var25.mtd715(string.Format("{0} ", (int) this.mtd815(Convert.ToChar(i))));
                    }
                    else
                    {
                        var25.mtd710(string.Format("{0} ", (int) this.mtd815(Convert.ToChar(i))));
                    }
                    num++;
                }
            }
            else
            {
                string str;
                ushort[] numArray = this._var9.mtd879(var29);
                ArrayList list = new ArrayList();
                for (int j = 0; j < numArray.Length; j++)
                {
                    ushort num3 = numArray[j];
                    if ((list.Count > 0) && ((((ushort) list[list.Count - 1]) + 1) != num3))
                    {
                        str = null;
                        for (int k = 0; k < list.Count; k++)
                        {
                            str = str + mtd620.mtd621((float) ((int) this.mtd815((ushort) list[k]))) + " ";
                        }
                        var25.mtd710(mtd620.mtd621((ushort) list[0]));
                        if ((str != null) && (str.Length > 0))
                        {
                            var25.mtd715(string.Format("[ {0}]", str));
                        }
                        list.Clear();
                    }
                    list.Add(num3);
                }
                if (list.Count > 0)
                {
                    str = null;
                    for (int m = 0; m < list.Count; m++)
                    {
                        str = str + mtd620.mtd621((float) ((int) this.mtd815((ushort) list[m]))) + " ";
                    }
                    var25.mtd710(mtd620.mtd621((ushort) list[0]));
                    if ((str != null) && (str.Length > 0))
                    {
                        var25.mtd715(string.Format("[ {0}]", str));
                    }
                }
            }
        }

        private void var30(int var34, mtd711 var25, ushort[] var29)
        {
            int num = 0;
            mtd757 mtd = base._mtd756.mtd757;
            mtd.mtd758(var25.mtd32, 0);
            var25.mtd715(string.Format("{0} 0 obj", var34));
            var25.mtd715("<<");
            var25.mtd715("/Type /FontDescriptor ");
            var25.mtd715(string.Format("/Ascent {0} ", base._mtd812));
            var25.mtd715(string.Format("/CapHeight {0} ", 500));
            var25.mtd715(string.Format("/Descent {0} ", base._mtd813));
            var25.mtd715(string.Format("/Flags {0} ", this.var36()));
            int num2 = (int) Math.Round((double) (this._var9.mtd862.mtd889 * this._var10));
            int num3 = (int) Math.Round((double) (this._var9.mtd862.mtd891 * this._var10));
            int num4 = (int) Math.Round((double) (this._var9.mtd862.mtd888 * this._var10));
            int num5 = (int) Math.Round((double) (this._var9.mtd862.mtd890 * this._var10));
            var25.mtd715(string.Format("/FontBBox {0} ", mtd620.mtd647(num2, num3, num4, num5)));
            var25.mtd715(string.Format("/FontName /{0} ", this.mtd933));
            var25.mtd715(string.Format("/ItalicAngle {0} ", (int) this._var9.mtd868.mtd929));
            var25.mtd715(string.Format("/StemV {0} ", 0));
            if (var29 != null)
            {
                num = base.mtd759 + 4;
                var25.mtd715(string.Format("/FontFile2 {0} 0 R ", num));
            }
            var25.mtd715(">>");
            var25.mtd715("endobj");
            if (var29 != null)
            {
                mtd742 mtd2 = new mtd742(this._var9.mtd710(var29, var7 + "+" + this._var8));
                mtd2.mtd745();
                mtd.mtd758(var25.mtd32, 0);
                mtd2.mtd710(var25, num, true, base._mtd756.mtd712);
            }
        }

        private static ushort[] var31(bool[] var35)
        {
            ushort[] destinationArray = new ushort[0x10000];
            int index = 1;
            ushort num2 = 0;
            destinationArray[0] = 0;
            for (int i = 1; i < 0x10000; i++)
            {
                num2 = (ushort) (num2 + 1);
                if (var35[i])
                {
                    destinationArray[index] = num2;
                    index++;
                }
            }
            ushort[] sourceArray = destinationArray;
            destinationArray = new ushort[index];
            Array.Copy(sourceArray, destinationArray, index);
            return destinationArray;
        }

        private void var32(int var34, ushort[] var29, mtd711 var25)
        {
            string str = var7 + "+" + base.mtd763;
            mtd742 mtd = new mtd742();
            mtd.mtd715("/CIDInit /ProcSet findresource begin");
            mtd.mtd715("12 dict begin");
            mtd.mtd715("begincmap");
            mtd.mtd715("/CIDSystemInfo");
            mtd.mtd715("<<");
            mtd.mtd715("/Registry (Adobe)");
            mtd.mtd715("/Ordering (" + str + ")");
            mtd.mtd715("/Supplement 0");
            mtd.mtd715(">> def");
            mtd.mtd715("/CMapName /" + str + " def");
            mtd.mtd715("/CMapType 2 def");
            mtd.mtd715("1 begincodespacerange");
            mtd.mtd715("<0000> <FFFF>");
            mtd.mtd715("endcodespacerange");
            if (var29.Length > 0)
            {
                mtd.mtd715(Convert.ToString(var29.Length) + " beginbfchar");
                for (int i = 0; i < var29.Length; i++)
                {
                    string str2 = this._var9.mtd883(var29[i]).ToString("X4");
                    string str3 = var29[i].ToString("X4");
                    mtd.mtd715(string.Format("<{0}> <{1}>", str2, str3));
                }
                mtd.mtd715("endbfchar");
            }
            mtd.mtd715("endcmap");
            mtd.mtd715("CMapName currentdict /CMap defineresource pop");
            mtd.mtd715("end");
            mtd.mtd715("end");
            mtd.mtd745();
            base._mtd756.mtd757.mtd758(var25.mtd32, 0);
            mtd.mtd710(var25, var34, false, base._mtd756.mtd712);
        }

        private void var33(int var34, ushort[] var29, mtd711 var25)
        {
            mtd712 mtd = base._mtd756.mtd712;
            if (mtd != null)
            {
                mtd.mtd775(var34, 0);
            }
            base._mtd756.mtd757.mtd758(var25.mtd32, 0);
            var25.mtd715(string.Format("{0} 0 obj", var34));
            var25.mtd715("<<");
            var25.mtd715("/Type /Font ");
            var25.mtd715("/Subtype /CIDFontType2 ");
            var25.mtd715(string.Format("/BaseFont /{0} ", this.mtd933));
            var25.mtd710("/CIDSystemInfo << /Registry ");
            mtd652.mtd710(ref var25, "Adobe", mtd);
            var25.mtd710("/Ordering ");
            mtd652.mtd710(ref var25, "Identity", mtd);
            var25.mtd715("/Supplement 0 >>");
            int num = base.mtd759 + 3;
            var25.mtd715(string.Format("/FontDescriptor {0} 0 R ", num));
            var25.mtd710("/W [");
            this.var28(var25, var29);
            var25.mtd715("]");
            var25.mtd715(">>");
            var25.mtd715("endobj");
            this.var30(num, var25, var29);
        }

        private ulong var36()
        {
            return 0x20L;
        }

        private void var37()
        {
        }

        internal override Font Font//mtd132
        {
            get
            {
                return this._var12;
            }
        }

        internal string mtd933
        {
            get
            {
                string str = this._var8;
                string str2 = string.Empty;
                if ((base.mtd934 & FontStyle.Bold) == FontStyle.Bold)
                {
                    str2 = str2 + "Bold";
                }
                if ((base.mtd934 & FontStyle.Italic) == FontStyle.Italic)
                {
                    str2 = str2 + "Italic";
                }
                if (str2.Length > 0)
                {
                    str = str + "," + str2;
                }
                if (!this._var11 && base._mtd932)
                {
                    return (var7 + "+" + str);
                }
                return str;
            }
        }
    }
}

