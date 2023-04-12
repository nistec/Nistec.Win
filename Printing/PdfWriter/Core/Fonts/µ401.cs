namespace MControl.Printing.Pdf.Core.Fonts
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class A401 : A252
    {
        private bool _b0;
        private string _b2;
        private A281 _b3;
        private float _b4;
        private bool _b5;
        private Font _b6;
        private bool[] _b7;
        private static string b1 = "MCTLXX";

        internal A401(Font b6, bool b8)
        {
            base._A402 = b8;
            this._b5 = true;
            this._b0 = false;
            this._b6 = b6;
            base._A247 = b6.FontFamily.Name;
            base._A248 = b6.Style;
            this._b7 = null;
            this.b9();
        }

        internal A401(string b10, string b11, FontStyle b12, bool b8)
        {
            base._A402 = b8;
            this._b5 = true;
            this._b0 = false;
            base._A247 = b10;
            base._A248 = b12;
            this._b7 = null;
            this.b9(b11);
        }

        internal override void A110()
        {
            int num;
            A93 A1 = base._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            base._A95 = num;
            if (base._A402)
            {
                base._A92.A93.A95 = base._A95 + 5;
                this._b5 = false;
                this._b7 = new bool[0x10000];
            }
            else
            {
                base._A92.A93.A95 = base._A95 + 2;
            }
        }

        internal override void A159(string b19)
        {
            for (int i = 0; i < b19.Length; i++)
            {
                this.A406(b19[i]);
            }
        }

        internal override string A163(string b19)
        {
            string str = string.Empty;
            if (this._b5)
            {
                for (int j = 0; j < b19.Length; j++)
                {
                    str = str + A15.A25(b19[j]);
                }
                return string.Format("{0}{1}{2}", "(", str, ")");
            }
            if (!base._A402)
            {
                return A15.A27(b19);
            }
            for (int i = 0; i < b19.Length; i++)
            {
                str = str + this._b3.A354(b19[i]).ToString("X4");
            }
            return string.Format("{0}{1}{2}", "<", str, ">");
        }

        internal override float A256(char b16)
        {
            return (this._b3.A257(b16) * this._b4);
        }

        internal override float A256(ushort b17)
        {
            return (this._b3.A257(b17) * this._b4);
        }

        internal override float A257(char b16, float b18)
        {
            return ((this.A256(b16) / 1000f) * b18);
        }

        internal override float A258(string b19, float b18)
        {
            float num = 0f;
            for (int i = 0; i < b19.Length; i++)
            {
                num += this.A257(b19[i], b18);
            }
            return num;
        }

        internal override void A406(char b16)
        {
            if (base._A402)
            {
                this._b7[b16] = true;
            }
            if (this._b5 && (b16 > '\x007f'))
            {
                this._b5 = false;
            }
        }

        internal override void A54(ref A55 b20)
        {
            if (!this._b0)
            {
                this._b2 = base._A247.Replace(" ", "");
                if (base._A402)
                {
                    this.b21(b20);
                }
                else
                {
                    this.b22(b20);
                }
            }
            else
            {
                this._b0 = true;
            }
        }

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, byte[] lpvBuffer, uint cbData);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern int GetTextCharset(IntPtr hdc);
        [DllImport("gdi32")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);
        private void b13(byte[] b14)
        {
            this._b3 = new A281();
            this._b3.A312(b14);
            if ((this._b6 != null) && (this._b3.A292.A295 == 2))
            {
                this._b3.A292.A294 = b15(this._b6);
            }
            this._b4 = 1000f / ((float) this._b3.A333.A365);
            base._A253 = (int) Math.Round((double) (this._b3.A334.A366 * this._b4));
            base._A254 = (int) Math.Round((double) (this._b3.A334.A367 * this._b4));
        }

        private static Encoding b15(Font b6)
        {
            if (b6 == null)
            {
                return null;
            }
            Font font = (Font) b6.Clone();
            Encoding encoding = null;
            IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
            IntPtr hObject = font.ToHfont();
            IntPtr ptr3 = SelectObject(hdc, hObject);
            try
            {
                int textCharset = GetTextCharset(hdc);
                CHARSETINFO lpCs = new CHARSETINFO();
                TranslateCharsetInfo((uint) textCharset, ref lpCs, 1);
                encoding = Encoding.GetEncoding(lpCs.ciACP);
            }
            finally
            {
                Graphics graphics=null;
                font.Dispose();
                SelectObject(hdc, ptr3);
                DeleteObject(hObject);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
            return encoding;
        }

        private void b21(A55 b20)
        {
            ushort[] numArray = b26(this._b7);
            base._A92.A93.A94(b20.A2, 0);
            b20.A59(string.Format("{0} 0 obj", base.A95));
            b20.A59("<<");
            b20.A59("/Type /Font ");
            b20.A59("/Subtype /Type0 ");
            b20.A59(string.Format("/BaseFont /{0} ", this.A404));
            b20.A59(string.Format("/Name /{0} ", base._A259));
            b20.A59("/Encoding /Identity-H ");
            int num = base.A95 + 1;
            b20.A59(string.Format("/ToUnicode {0} 0 R ", num));
            int num2 = base.A95 + 2;
            b20.A59(string.Format("/DescendantFonts [{0} 0 R] ", num2));
            b20.A59(">>");
            b20.A59("endobj");
            this.b27(num, numArray, b20);
            this.b28(num2, numArray, b20);
        }

        private void b22(A55 b20)
        {
            base._A92.A93.A94(b20.A2, 0);
            b20.A59(string.Format("{0} 0 obj", base.A95));
            b20.A59("<<");
            b20.A59("/Type /Font ");
            b20.A59(string.Format("/BaseFont /{0} ", this.A404));
            b20.A59("/Subtype /TrueType ");
            b20.A59(string.Format("/Name /{0} ", base._A259));
            b20.A59(string.Format("/FirstChar {0} ", 0x20));
            b20.A59(string.Format("/LastChar {0} ", 0xff));
            b20.A54("/Widths [");
            this.b23(b20, null);
            b20.A59("]");
            b20.A59("/Encoding /PdfDocEncoding ");
            int num = base._A95 + 1;
            b20.A59(string.Format("/FontDescriptor {0} 0 R ", num));
            b20.A59(">>");
            b20.A59("endobj");
            this.b25(num, b20, null);
        }

        private void b23(A55 b20, ushort[] b24)
        {
            int num = 0;
            if (b24 == null)
            {
                for (int i = 0x20; i < 0x100; i++)
                {
                    if (num == 0x10)
                    {
                        num = 0;
                        b20.A59(string.Format("{0} ", (int) this.A256(Convert.ToChar(i))));
                    }
                    else
                    {
                        b20.A54(string.Format("{0} ", (int) this.A256(Convert.ToChar(i))));
                    }
                    num++;
                }
            }
            else
            {
                string str;
                ushort[] numArray = this._b3.A350(b24);
                ArrayList list = new ArrayList();
                for (int j = 0; j < numArray.Length; j++)
                {
                    ushort num3 = numArray[j];
                    if ((list.Count > 0) && ((((ushort) list[list.Count - 1]) + 1) != num3))
                    {
                        str = null;
                        for (int k = 0; k < list.Count; k++)
                        {
                            str = str + A15.A18((int) this.A256((ushort) list[k])) + " ";
                        }
                        b20.A54(A15.A18((ushort) list[0]));
                        if ((str != null) && (str.Length > 0))
                        {
                            b20.A59(string.Format("[ {0}]", str));
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
                        str = str + A15.A18((int) this.A256((ushort) list[m])) + " ";
                    }
                    b20.A54(A15.A18((ushort) list[0]));
                    if ((str != null) && (str.Length > 0))
                    {
                        b20.A59(string.Format("[ {0}]", str));
                    }
                }
            }
        }

        private void b25(int b29, A55 b20, ushort[] b24)
        {
            int num = 0;
            A93 A = base._A92.A93;
            A.A94(b20.A2, 0);
            b20.A59(string.Format("{0} 0 obj", b29));
            b20.A59("<<");
            b20.A59("/Type /FontDescriptor ");
            b20.A59(string.Format("/Ascent {0} ", base._A253));
            b20.A59(string.Format("/CapHeight {0} ", 500));
            b20.A59(string.Format("/Descent {0} ", base._A254));
            b20.A59(string.Format("/Flags {0} ", this.b31()));
            int num2 = (int) Math.Round((double) (this._b3.A333.A360 * this._b4));
            int num3 = (int) Math.Round((double) (this._b3.A333.A362 * this._b4));
            int num4 = (int) Math.Round((double) (this._b3.A333.A359 * this._b4));
            int num5 = (int) Math.Round((double) (this._b3.A333.A361 * this._b4));
            b20.A59(string.Format("/FontBBox {0} ", A15.A21(num2, num3, num4, num5)));
            b20.A59(string.Format("/FontName /{0} ", this.A404));
            b20.A59(string.Format("/ItalicAngle {0} ", (int) this._b3.A339.A393));
            b20.A59(string.Format("/StemV {0} ", 0));
            if (b24 != null)
            {
                num = base.A95 + 4;
                b20.A59(string.Format("/FontFile2 {0} 0 R ", num));
            }
            b20.A59(">>");
            b20.A59("endobj");
            if (b24 != null)
            {
                A112 A2 = new A112(this._b3.A54(b24, b1 + "+" + this._b2));
                A2.A123();
                A.A94(b20.A2, 0);
                A2.A54(b20, num, true, base._A92.A56);
            }
        }

        private static ushort[] b26(bool[] b30)
        {
            ushort[] destinationArray = new ushort[0x10000];
            int index = 1;
            ushort num2 = 0;
            destinationArray[0] = 0;
            for (int i = 1; i < 0x10000; i++)
            {
                num2 = (ushort) (num2 + 1);
                if (b30[i])
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

        private void b27(int b29, ushort[] b24, A55 b20)
        {
            string str = b1 + "+" + base.A168;
            A112 A = new A112();
            A.A59("/CIDInit /ProcSet findresource begin");
            A.A59("12 dict begin");
            A.A59("begincmap");
            A.A59("/CIDSystemInfo");
            A.A59("<<");
            A.A59("/Registry (Adobe)");
            A.A59("/Ordering (" + str + ")");
            A.A59("/Supplement 0");
            A.A59(">> def");
            A.A59("/CMapName /" + str + " def");
            A.A59("/CMapType 2 def");
            A.A59("1 begincodespacerange");
            A.A59("<0000> <FFFF>");
            A.A59("endcodespacerange");
            if (b24.Length > 0)
            {
                A.A59(Convert.ToString(b24.Length) + " beginbfchar");
                for (int i = 0; i < b24.Length; i++)
                {
                    string str2 = this._b3.A354(b24[i]).ToString("X4");
                    string str3 = b24[i].ToString("X4");
                    A.A59(string.Format("<{0}> <{1}>", str2, str3));
                }
                A.A59("endbfchar");
            }
            A.A59("endcmap");
            A.A59("CMapName currentdict /CMap defineresource pop");
            A.A59("end");
            A.A59("end");
            A.A123();
            base._A92.A93.A94(b20.A2, 0);
            A.A54(b20, b29, false, base._A92.A56);
        }

        private void b28(int b29, ushort[] b24, A55 b20)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(b29, 0);
            }
            base._A92.A93.A94(b20.A2, 0);
            b20.A59(string.Format("{0} 0 obj", b29));
            b20.A59("<<");
            b20.A59("/Type /Font ");
            b20.A59("/Subtype /CIDFontType2 ");
            b20.A59(string.Format("/BaseFont /{0} ", this.A404));
            b20.A54("/CIDSystemInfo << /Registry ");
            A26.A54(ref b20, "Adobe", A);
            b20.A54("/Ordering ");
            A26.A54(ref b20, "Identity", A);
            b20.A59("/Supplement 0 >>");
            int num = base.A95 + 3;
            b20.A59(string.Format("/FontDescriptor {0} 0 R ", num));
            b20.A54("/W [");
            this.b23(b20, b24);
            b20.A59("]");
            b20.A59(">>");
            b20.A59("endobj");
            this.b25(num, b20, b24);
        }

        private int b31()
        {
            byte[] buffer = new byte[4];
            buffer = this.b32(buffer, 6);
            if (!this._b5)
            {
                if (this._b3.A338.A373.A378 == 9)
                {
                    buffer = this.b32(buffer, 1);
                }
                if (((this._b3.A338.A373.A376 != 11) && (this._b3.A338.A373.A376 != 12)) && (this._b3.A338.A373.A376 != 13))
                {
                    buffer = this.b32(buffer, 2);
                }
                if (this._b3.A338.A373.A375 == 3)
                {
                    buffer = this.b32(buffer, 4);
                }
            }
            return BitConverter.ToInt32(buffer, 0);
        }

        private byte[] b32(byte[] value, int bitNumber)
        {
            bitNumber--;
            if ((bitNumber >= 0) && (bitNumber < (value.Length * 8)))
            {
                int index = bitNumber / 8;
                int num2 = bitNumber % 8;
                byte num3 = (byte) (((int) 1) << num2);
                value[index] = (byte) (value[index] | num3);
            }
            return value;
        }

        private void b9()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr hdc = graphics.GetHdc();
                try
                {
                    SelectObject(hdc, this._b6.ToHfont());
                    uint cbData = GetFontData(hdc, 0, 0, null, 0);
                    if (cbData < 0)
                    {
                        throw new Exception("Error when reading font file");
                    }
                    byte[] lpvBuffer = new byte[cbData];
                    if (GetFontData(hdc, 0, 0, lpvBuffer, cbData) != lpvBuffer.Length)
                    {
                        throw new Exception("Error when reading font file");
                    }
                    this.b13(lpvBuffer);
                }
                finally
                {
                    graphics.ReleaseHdc(hdc);
                }
            }
        }

        private void b9(string b11)
        {
            FileStream stream = new FileStream(b11, FileMode.Open, FileAccess.Read);
            if (stream.Length > 0L)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.b13(buffer);
            }
        }

        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern bool TranslateCharsetInfo(uint pSrc, [In, Out] ref CHARSETINFO lpCs, uint dwFlags);

        internal override bool A342
        {
            get
            {
                return ((this._b3 != null) && this._b3.A342);
            }
        }

        internal override Font A403
        {
            get
            {
                return this._b6;
            }
        }

        internal string A404
        {
            get
            {
                string str = this._b2;
                string str2 = string.Empty;
                if ((base.A405 & FontStyle.Bold) == FontStyle.Bold)
                {
                    str2 = str2 + "Bold";
                }
                if ((base.A405 & FontStyle.Italic) == FontStyle.Italic)
                {
                    str2 = str2 + "Italic";
                }
                if (str2.Length > 0)
                {
                    str = str + "," + str2;
                }
                if (!this._b5 && base._A402)
                {
                    return (b1 + "+" + str);
                }
                return str;
            }
        }
    }
}

