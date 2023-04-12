namespace Nistec.Printing.View.Html
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;

    public class HTMLDocument
    {
        private string _var14;
        private string _EndScriptBlock;
        private bool _HtmlFragment;
        private string var0;
        private string var1;
        private mtd632 var10 = null;
        private HtmlCharacterSet _CharSet = HtmlCharacterSet.utf_8;
        private Encoding _Encoding = Encoding.UTF8;
        private string _CodePage = "utf-8";
        private string var2;
        private string var3;
        private Document var4;
        private bool var5 = true;
        private string _Title;
        private System.Drawing.Printing.Margins _Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
        private mtd630 var8 = null;
        private mtd628 var9 = null;

        public void Export(FileStream stream, Document document)
        {
            this.var18(stream.Name);
            this.Export(stream, document, new FileImageSource(this.var3), 1, document.Pages.Count, false);
        }

        public void Export(string filepath, Document document, bool multipage)
        {
            if (((filepath != null) && (document != null)) && (document.Pages.Count != 0))
            {
                this.Export(filepath, document, 1, document.Pages.Count, multipage);
            }
        }

        public void Export(string filepath, Document document, int fromPageNo, int toPageNo, bool multipage)
        {
            if ((((filepath != null) && (document != null)) && ((fromPageNo <= toPageNo) && (fromPageNo >= 1))) && (((toPageNo >= 1) && (fromPageNo <= document.Pages.Count)) && (toPageNo <= document.Pages.Count)))
            {
                float num;
                float num2;
                float num3;
                float num4;
                this.var18(filepath);
                this.var5 = multipage;
                this.var4 = document;
                FileImageSource source = new FileImageSource(this.var3);
                this.var8 = new mtd630(source);
                this.var9 = new mtd628(source);
                this.var10 = new mtd632(source);
                this.var19(out num, out num2, out num3, out num4);
                this.var20(this.var4.TopMargin, num3, num4, 1, document.Pages.Count);
            }
        }

        public void Export(Stream stream, Document document, ImageSource imagesource, int fromPageNo, int toPageNo, bool multipage)
        {
            if ((((stream != null) && (document != null)) && ((imagesource != null) && (fromPageNo <= toPageNo))) && (((fromPageNo >= 1) && (toPageNo >= 1)) && ((fromPageNo <= document.Pages.Count) && (toPageNo <= document.Pages.Count))))
            {
                float num;
                float num2;
                float num3;
                float num4;
                this.var5 = false;
                this.var4 = document;
                this.var8 = new mtd630(imagesource);
                this.var9 = new mtd628(imagesource);
                this.var10 = new mtd632(imagesource);
                this.var19(out num, out num2, out num3, out num4);
                StreamWriter writer = new StreamWriter(stream, this._Encoding);
                this.var20(ref writer, this.var4.TopMargin, num3, num4, fromPageNo, toPageNo);
                writer.Flush();
            }
        }

        public void Export(StreamWriter writer, Document document, ImageSource imagesource, int fromPageNo, int toPageNo, bool multipage)
        {
            if ((((writer != null) && (document != null)) && ((imagesource != null) && (fromPageNo <= toPageNo))) && (((fromPageNo >= 1) && (toPageNo >= 1)) && ((fromPageNo <= document.Pages.Count) && (toPageNo <= document.Pages.Count))))
            {
                float num;
                float num2;
                float num3;
                float num4;
                this.var5 = false;
                this.var4 = document;
                this.var8 = new mtd630(imagesource);
                this.var9 = new mtd628(imagesource);
                this.var10 = new mtd632(imagesource);
                this.var19(out num, out num2, out num3, out num4);
                this.var20(ref writer, this.var4.TopMargin, num3, num4, fromPageNo, toPageNo);
            }
        }
        //SetCharSet
        private void SetCharSet()
        {
            if (this._CharSet == HtmlCharacterSet.big5)
            {
                this.SetCharSet(950, "big5");
            }
            else if (this._CharSet == HtmlCharacterSet.csISO2022JP)
            {
                this.SetCharSet(0xc42d, "csISO2022JP");
            }
            else if (this._CharSet == HtmlCharacterSet.gb2312)
            {
                this.SetCharSet(0x3a8, "gb2312");
            }
            else if (this._CharSet == HtmlCharacterSet.hz_gb_2312)
            {
                this.SetCharSet(0xcec8, "hz-gb-2312");
            }
            else if (this._CharSet == HtmlCharacterSet.ibm852)
            {
                this.SetCharSet(0x354, "ibm852");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_2022_jp)
            {
                this.SetCharSet(0xc42c, "iso-2022-jp");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_2022_kr)
            {
                this.SetCharSet(0xc431, "iso-2022-kr");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_1)
            {
                this.SetCharSet(0x6faf, "iso-8859-1");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_2)
            {
                this.SetCharSet(0x6fb0, "iso-8859-2");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_3)
            {
                this.SetCharSet(0x6fb1, "iso-8859-3");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_4)
            {
                this.SetCharSet(0x6fb2, "iso-8859-4");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_5)
            {
                this.SetCharSet(0x6fb3, "iso-8859-5");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_6)
            {
                this.SetCharSet(0x6fb4, "iso-8859-6");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_7)
            {
                this.SetCharSet(0x6fb5, "iso-8859-7");
            }
            else if (this._CharSet == HtmlCharacterSet.iso_8859_8)
            {
                this.SetCharSet(0x6fb6, "iso-8859-8");
            }
            else if (this._CharSet == HtmlCharacterSet.koi8_r)
            {
                this.SetCharSet(0x5182, "koi8-r");
            }
            else if (this._CharSet == HtmlCharacterSet.ks_c_5601)
            {
                this.SetCharSet(0x3b5, "ks_c_5601");
            }
            else if (this._CharSet == HtmlCharacterSet.shift_jis)
            {
                this.SetCharSet(0x3a4, "shift-jis");
            }
            else if (this._CharSet == HtmlCharacterSet.unicode)
            {
                this._CodePage = "unicode";
                this._Encoding = Encoding.Unicode;
            }
            else if (this._CharSet == HtmlCharacterSet.utf_7)
            {
                this._CodePage = "utf-7";
                this._Encoding = Encoding.UTF7;
            }
            else if (this._CharSet == HtmlCharacterSet.utf_8)
            {
                this._Encoding = Encoding.UTF8;
                this._CodePage = "utf-8";
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1250)
            {
                this.SetCharSet(0x4e2, "windows-1250");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1251)
            {
                this.SetCharSet(0x4e3, "windows-1251");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1252)
            {
                this.SetCharSet(0x4e4, "windows-1252");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1253)
            {
                this.SetCharSet(0x4e5, "windows-1253");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1254)
            {
                this.SetCharSet(0x4e6, "windows-1254");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1255)
            {
                this.SetCharSet(0x4e7, "windows-1255");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1256)
            {
                this.SetCharSet(0x4e8, "windows-1256");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1257)
            {
                this.SetCharSet(0x4e9, "windows-1257");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_1258)
            {
                this.SetCharSet(0x4ea, "windows-1258");
            }
            else if (this._CharSet == HtmlCharacterSet.windows_874)
            {
                this.SetCharSet(0x36a, "windows-874");
            }
        }
        //SetCharSet
        private void SetCharSet(int _CodePage, string codePage)
        {
            try
            {
                this._Encoding = Encoding.GetEncoding(_CodePage);
                this._CodePage = codePage;
            }
            catch
            {
                this._Encoding = Encoding.UTF8;
                this._CodePage = "utf-8";
            }
        }

        private bool var18(string var46)
        {
            try
            {
                FileInfo info = new FileInfo(var46);
                this.var0 = info.DirectoryName;
                if (!Directory.Exists(this.var0))
                {
                    Directory.CreateDirectory(this.var0);
                }
                this.var0 = this.var0 + @"\";
                this.var1 = Path.GetExtension(info.Name);
                this.var2 = Path.GetFileNameWithoutExtension(info.Name);
                new FileIOPermission(FileIOPermissionAccess.NoAccess, this.var0).Demand();
                this.var3 = string.Format("{0}{1}_files", this.var0, this.var2);
                if (!Directory.Exists(this.var3))
                {
                    Directory.CreateDirectory(this.var3);
                }
                File.SetAttributes(this.var3, FileAttributes.Archive | FileAttributes.Hidden);
                return true;
            }
            catch
            {
            }
            return false;
        }

        private void var19(out float var21, out float var22, out float var23, out float var24)
        {
            float reportWidth = this.var4.ReportWidth;
            var21 = this.var4.PageWidth;
            var22 = this.var4.PageHeight;
            var23 = var21 - (this.var4.LeftMargin + this.var4.RightMargin);
            if (reportWidth < var23)
            {
                var23 = reportWidth;
            }
            var24 = var22 - (this.var4.TopMargin + this.var4.BottomMargin);
        }

        private void var20(float var26, float var27, float var28, int var29, int var30)
        {
            int num = var29 - 1;
            if (this.var5)
            {
                while (num < var30)
                {
                    using (FileStream stream = new FileStream(this.FormatFileName(/*mtd620.mtd621(num)*/), FileMode.Create, FileAccess.ReadWrite))
                    {
                        StreamWriter writer = new StreamWriter(stream, this._Encoding);
                        this.var31(ref writer);
                        this.var32(this.var4.Pages.GetPage(num), var26, var27, var28, false, ref writer);
                        this.var33(ref writer);
                        writer.Flush();
                    }
                    num++;
                }
            }
            else
            {
                using (FileStream stream2 = new FileStream(this.FormatFileName(/*string.Format("{0}-{1}", var29, var30)*/), FileMode.Create, FileAccess.ReadWrite))
                {
                    StreamWriter writer2 = new StreamWriter(stream2, this._Encoding);
                    this.var31(ref writer2);
                    bool flag = true;
                    while (num < var30)
                    {
                        if (num == (var30 - 1))
                        {
                            flag = false;
                        }
                        this.var32(this.var4.Pages.GetPage(num), var26, var27, var28, flag, ref writer2);
                        num++;
                    }
                    this.var33(ref writer2);
                    writer2.Flush();
                }
            }
        }

        private void var20(ref StreamWriter var25, float var26, float var27, float var28, int var29, int var30)
        {
            int index = var29 - 1;
            if (!this._HtmlFragment)
            {
                this.var31(ref var25);
            }
            bool flag = true;
            while (index < var30)
            {
                if (index == (var30 - 1))
                {
                    flag = false;
                }
                this.var32(this.var4.Pages.GetPage(index), var26, var27, var28, flag, ref var25);
                index++;
            }
            if (!this._HtmlFragment)
            {
                this.var33(ref var25);
            }
        }

        private void var31(ref StreamWriter var25)
        {
            if ((this._Title == null) || (this._Title.Length == 0))
            {
                this._Title = "mCobtrol.ReportView";
            }
            var25.WriteLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
            var25.WriteLine("<html>");
            var25.WriteLine("<head>");
            var25.WriteLine(string.Format("<title>{0}</title>", this._Title));
            var25.WriteLine(string.Format("<meta HTTP-EQUIV='Content-Type' CONTENT='text/html; charset={0}'>", this._CodePage));
            var25.WriteLine("</head>");
            var25.WriteLine(string.Format("<body leftMargin={0} topMargin={1} rightMargin={2} bottomMargin={3}>", new object[] { this._Margins.Left, this._Margins.Top, this._Margins.Right, this._Margins.Bottom }));
            if ((this._var14 != null) && (this._var14.Length > 0))
            {
                var25.Write(this._var14);
            }
        }

        private void var32(Page var35, float var26, float var27, float var28, bool var36, ref StreamWriter var37)
        {
            mtd141 mtd = new mtd141();
            var35.mtd352(mtd);
            int num = 0;
            var37.Write("<div style=" + '"');
            mtd627.mtd22(var27, var28, ref var37);
            if (var36)
            {
                var37.Write("page-break-inside:avoid; page-break-after:always;");
            }
            else
            {
                var37.Write("page-break-inside:avoid;");
            }
            var37.WriteLine('"' + ">");
            if (var35.mtd352(mtd) && mtd.mtd86)
            {
                this.var38(mtd, ref num, var26, var27, ref var37);
                num++;
            }
            for (int i = 0; i < var35.SectionCount; i++)
            {
                if (var35.mtd141(mtd, i) && mtd.mtd86)
                {
                    this.var38(mtd, ref num, var26, var27, ref var37);
                    num++;
                }
            }
            if (var35.mtd353(mtd) && mtd.mtd86)
            {
                this.var38(mtd, ref num, var26, var27, ref var37);
                num++;
            }
            var39(num, ref var37);
            var37.WriteLine("</div>");
        }

        private void var33(ref StreamWriter var37)
        {
            var37.WriteLine();
            if ((this._EndScriptBlock != null) && (this._EndScriptBlock.Length > 0))
            {
                var37.Write(this._EndScriptBlock);
            }
            var37.WriteLine("</body>");
            var37.WriteLine("</html>");
        }

        private string FormatFileName(string pages)//var34
        {
            return string.Format("{0}{1}[{2}]{3}", new object[] { this.var0, this.var2, pages, this.var1 });
        }
        private string FormatFileName()//var34
        {
            return string.Format("{0}{1}{2}", new object[] { this.var0, this.var2, this.var1 });
        }

        private void var38(mtd141 var40, ref int var41, float var26, float var42, ref StreamWriter var37)
        {
            PropDoc mtd = new PropDoc();
            var37.Write("<div style=" + '"');
            mtd627.mtd22(var41, var26, var42, var40, ref var37);
            mtd624.mtd22(var40.BackColor, ref var37);
            var37.WriteLine('"' + ">");
            for (int i = 0; i < var40.mtd166; i++)
            {
                if (var40.mtd168(mtd, i))
                {
                    if ((mtd.ControlType == ControlType.TextBox) || (mtd.ControlType == ControlType.Label))
                    {
                        mtd634.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.Line)
                    {
                        mtd631.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.Shape)
                    {
                        mtd633.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.Picture)
                    {
                        this.var8.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.CheckBox)
                    {
                        this.var9.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.RichTextField)
                    {
                        this.var10.mtd22(var41++, mtd, ref var37);
                    }
                    else if (mtd.ControlType == ControlType.SubReport)
                    {
                        this.var43(var41++, mtd, ref var37);
                    }
                }
            }
            var37.WriteLine("</div>");
        }

        private static void var39(int var41, ref StreamWriter var37)
        {
            var37.Write("<span style=" + '"');
            var37.Write("position: absolute; ");
            var37.Write(string.Format("left: {0}px; top: {1}px; width: {2}px; height: {3}px; ", new object[] { 0, 10, 0xe1, 0x18 }));
            var37.Write("overflow: hidden; ");
            var37.Write(string.Format("z-index: {0}; ", mtd620.mtd621(var41)));
            var37.Write("font-family: Arial; ");
            var37.Write("font-size: 8pt; ");
            var37.Write("font-weight: normal; ");
            var37.Write("color: Gray; ");
            var37.WriteLine('"' + string.Format(">{0}</span>", "Nistec ReportView Version 4.0.1.0"));
        }

        private void var43(int var41, PropDoc var44, ref StreamWriter var37)
        {
            float num = var44.Width;
            mtd141 mtd = new mtd141();
            var37.Write("<div style=" + '"');
            mtd627.mtd22(var41, var44.Left, var44.Top, num, var44.Height, var44.Visible, ref var37);
            mtd624.mtd22(var44.BackColor, ref var37);
            var37.WriteLine('"' + ">");
            for (int i = 0; i < var44.Count; i++)
            {
                if (var44.mtd140(mtd, i) && mtd.mtd86)
                {
                    this.var38(mtd, ref var41, 0f, num, ref var37);
                }
            }
            var37.WriteLine("</div>");
        }

        public string BeginScriptBlock
        {
            get
            {
                return this._var14;
            }
            set
            {
                this._var14 = value;
            }
        }

        public HtmlCharacterSet CharSet
        {
            get
            {
                return this._CharSet;
            }
            set
            {
                this._CharSet = value;
                this.SetCharSet();
            }
        }

        public string CodePage
        {
            get
            {
                return this._CodePage;
            }
        }

        public string EndScriptBlock
        {
            get
            {
                return this._EndScriptBlock;
            }
            set
            {
                this._EndScriptBlock = value;
            }
        }

        public bool HtmlFragment
        {
            get
            {
                return this._HtmlFragment;
            }
            set
            {
                this._HtmlFragment = value;
            }
        }

        public System.Drawing.Printing.Margins Margins
        {
            get
            {
                return this._Margins;
            }
            set
            {
                this._Margins = value;
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }
    }
}

