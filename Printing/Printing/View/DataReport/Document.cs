namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class Document
    {
        private string _var0;
        private float _var1;
        private mtd245 _var10;
        private Report _Report;
        private PagesCollection _Pages = new PagesCollection();
        private bool _var13;
        private int _var14;
        private DataFields _DataFields;
        private CodeProvider _CodeProvider = new CodeProvider();
        private int _var17;
        //private RectangleF _var18;
        private InternalPrintDocument _PrintDocument;
        private float _var2;
        private Font _var20;
        private float _var3;
        private float _var4;
        private float _var5;
        private float _var6;
        private float _var7;
        private bool _var70;
        private float _var8;
        private float _ReportWidth;
        internal SectionCollection _Sections = new SectionCollection();
        internal mtd266[] mtd267 = null;
        internal mtd270 mtd271;
        internal mtd272 mtd273;
        internal mtd272 mtd274;
        internal mtd272 mtd275;
        internal mtd272 mtd276;
        internal bool mtd370;
        internal bool mtd371;
        private System.Drawing.Graphics g;

        internal Document(Report var21)
        {
            this._Report = var21;
            //this._var18 = new RectangleF();
            this._PrintDocument = new InternalPrintDocument(this);
        }
        //mtd378
        internal void InitDocumentIntrenal ()
        {
            this._Pages.Clear();
            this.var23();
            if (this.g == null)
            {
                this.g = System.Drawing.Graphics.FromHwnd(new IntPtr(0));
            }
            GraphicsUnit pageUnit = this.g.PageUnit;
            this.g.PageUnit = GraphicsUnit.Inch;
            this._PrintDocument.mtd379();
            if (this._DataFields != null)
            {
                this._DataFields.mtd172();
                this.RenderDocument(this.PrintBound);
            }
            if (this._Pages.MaxPages == 0)
            {
                this.var25();
                this._Report.MaxPages = this._Pages.MaxPages;
            }
            int maxPages = (int) this._Report.MaxPages;
            this._PrintDocument.PrinterSettings.MaximumPage = maxPages;
            this._PrintDocument.PrinterSettings.MinimumPage = 1;
            this._PrintDocument.PrinterSettings.FromPage = 1;
            this._PrintDocument.PrinterSettings.ToPage = maxPages;
            this.g.PageUnit = pageUnit;
            if (!this._Report.mtd117(Msg.ReportEnd) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { this._Report, EventArgs.Empty };
                this._CodeProvider.mtd71("Report", Methods._End, objArray);
            }
            this.var26();
        }

        public bool Find(FindInfo FM)
        {
            this._var70 = false;
            int num = FM.mtd201.PageIndex - 1;
            int count = 0;
            int num3 = -1;
            int step = FM.Step;
            bool flag = false;
            if ((FM.mtd203.mtd205 != null) && (FM.mtd204 != null))
            {
                ControlType type = FM.mtd204.mtd199.ControlType;
                num3 = FM.mtd204.mtd200;
                if (type == ControlType.SubReport)
                {
                    bool flag2 = false;
                    mtd207 mtd = (mtd207) FM.mtd204;
                    RectangleF ef = FM.mtd57;
                    mtd198 mtd2 = this.var71(mtd, FM, ref ef, true);
                    if (mtd2 != null)
                    {
                        FM.mtd206(ef);
                        FM.mtd204 = mtd2;
                        return true;
                    }
                    if (mtd.mtd208 != null)
                    {
                        flag2 = true;
                    }
                    while (flag2)
                    {
                        mtd = mtd.mtd208;
                        mtd2 = this.var71(mtd, FM, ref ef, true);
                        if (mtd2 != null)
                        {
                            FM.mtd206(ef);
                            FM.mtd204 = mtd2;
                            return true;
                        }
                        if (mtd.mtd208 != null)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = false;
                        }
                    }
                }
                if (step == 1)
                {
                    num3++;
                }
                else
                {
                    num3--;
                }
                flag = true;
            }
            if (step == 1)
            {
                if (this.FindAtHeaderAndFooter(num3, ref flag, ref FM))
                {
                    return true;
                }
                num++;
                count = this.Pages.Count;
                num3 = -1;
                for (int i = num; i < count; i++)
                {
                    FM.mtd201 = this._Pages.GetPage(i);
                    if (this.FindAtHeaderAndFooter(num3, ref flag, ref FM))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (this.FindAtHeaderAndFooter(num3, ref flag, ref FM))
                {
                    return true;
                }
                num--;
                count = 0;
                num3 = -1;
                for (int j = num; j >= count; j--)
                {
                    FM.mtd201 = this._Pages.GetPage(j);
                    if (this.FindAtHeaderAndFooter(num3, ref flag, ref FM))
                    {
                        return true;
                    }
                }
            }
            FM.mtd201 = null;
            FM.mtd203.mtd205 = null;
            FM.mtd203.mtd200 = -1;
            FM.mtd204 = null;
            return false;
        }

        public bool Find(string text, ref int pageno)
        {
            FindInfo fM = new FindInfo();
            fM.Text = text;
            if (((long) (pageno + 1)) <= this._Report.MaxPages)
            {
                fM.mtd201 = this._Pages.GetPage(pageno);
                if (this.Find(fM))
                {
                    pageno = fM.mtd201.PageIndex;
                    return true;
                }
                if (pageno != 1)
                {
                    fM.mtd201 = this._Pages.GetPage(0);
                    if (this.Find(fM))
                    {
                        pageno = fM.mtd201.PageIndex;
                        return true;
                    }
                }
                return false;
            }
            fM.mtd201 = this._Pages.GetPage(0);
            if (this.Find(fM))
            {
                pageno = fM.mtd201.PageIndex;
                return true;
            }
            return false;
        }

        public void Print()
        {
            this.Print(null);
        }

        public void Print(PrintDialog dialog)
        {
            this._PrintDocument.mtd377();
            if ((this._Report.MaxPages > 0L) && this._PrintDocument.mtd380)
            {
                if (dialog != null)
                {
                    dialog.Document = this._PrintDocument;
                    if (this._Report.MaxPages > 1L)
                    {
                        dialog.AllowSomePages = true;
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this._PrintDocument.Print();
                    }
                }
                else
                {
                    this._PrintDocument.Print();
                }
            }
        }

        public void PrintPage(System.Drawing.Graphics g, int PageNo, float top)
        {
            this.PrintVersion(g);
            Page page = this._Pages.GetPage(PageNo);
            float num = this._var1;
            if (g.PageUnit == GraphicsUnit.Display)
            {
                num *= ReportUtil.Dpi;
            }
            if (page.mtd347 != null)
            {
                this.PrintControls(g, page.mtd347, top, num);
            }
            this.var61(g, page, top);
            if (page.mtd348 != null)
            {
                this.PrintControls(g, page.mtd348, top, num);
            }
        }

        private void var22()
        {
            this._CodeProvider.Text = this._Report.Script;
            if (this._CodeProvider.mtd282())
            {
                this._CodeProvider.ScriptLanguage = this._Report.ScriptLanguage;
                this._CodeProvider.mtd284(true);
            }
        }

        private void var23()
        {
            this._Sections = this._Report.Sections;
            this.var22();
            if (!this._Report.mtd117(Msg.ReportStart) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { this._Report, EventArgs.Empty };
                this._CodeProvider.mtd71("Report", Methods._Start, objArray);
            }
            DataFields.mtd194(this._Report, this._CodeProvider);
            this._DataFields = this._Report.DataFields;
        }
        //var24
        private void RenderDocument(RectangleF rect)
        {
            mtd163 mtd2;
            NewPage newPage;
            this._var10 = new mtd245();
            this._var1 = rect.Left;
            this._var2 = rect.Right;
            this._var3 = rect.Top;
            this._var4 = rect.Bottom;
            this._ReportWidth = this._Report.ReportWidth / ReportUtil.Dpi;
            this._var10.mtd277(this, null, false);
            if (this.mtd275 == null)
            {
                this._var5 = this._var3;
                this._var6 = this._var4;
            }
            else
            {
                this._var5 = this._var3 + this.mtd275.mtd290.Height;
                this._var6 = this._var4 - this.mtd276.mtd290.Height;
            }
            this._var7 = this._var6 - this._var5;
            this._var8 = this._var2 - this._var1;
            float num = this._var5;
            Page page = new Page(this._Pages.MaxPages + 1);
            if (this.mtd273 != null)
            {
                newPage = this.mtd273.mtd290.mtd242.reportHeader.NewPage;
                if ((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.Before)))
                {
                    this._Pages.AddPage(page);
                    page = new Page(this._Pages.MaxPages + 1);
                    num = this._var5;
                }
                page = this.var27(ref this.mtd273, ref page, ref num);
                if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.After))) && (page.mtd350 > 0))
                {
                    this._Pages.AddPage(page);
                    page = new Page(this._Pages.MaxPages + 1);
                    num = this._var5;
                }
            }
        Label_0383:
            if (this.mtd271.mtd166 > 0)
            {
                while (!this._DataFields.mtd189)
                {
                    for (int j = 0; j < this.mtd271.mtd166; j++)
                    {
                        mtd272 mtd = this.mtd271[j];
                        mtd2 = mtd.mtd290;
                        if (mtd2._SectionType == SectionType.GroupHeader)
                        {
                            newPage = mtd2.mtd242.groupHeader.NewPage;
                            if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.Before))) && (page.mtd350 > 0))
                            {
                                this._Pages.AddPage(page);
                                page = new Page(this._Pages.MaxPages + 1);
                                num = this._var5;
                            }
                            page = this.var28(ref mtd, ref num, ref page);
                            if (this._DataFields.mtd189)
                            {
                                goto Label_0383;
                            }
                            if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.After))) && (page.mtd350 > 0))
                            {
                                this._Pages.AddPage(page);
                                page = new Page(this._Pages.MaxPages + 1);
                                num = this._var5;
                            }
                        }
                        else if (mtd2._SectionType == SectionType.ReportDetail)
                        {
                            page = this.RenderReportDetail(ref mtd, ref num, ref page);
                        }
                        else if (mtd2._SectionType == SectionType.GroupFooter)
                        {
                            newPage = mtd2.mtd242.groupFooter.NewPage;
                            if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.Before))) && (page.mtd350 > 0))
                            {
                                this._Pages.AddPage(page);
                                page = new Page(this._Pages.MaxPages + 1);
                                num = this._var5;
                            }
                            page = this.RenderGroopFooter(ref mtd, ref num, ref page);
                            if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.After))) && (page.mtd350 > 0))
                            {
                                this._Pages.AddPage(page);
                                page = new Page(this._Pages.MaxPages + 1);
                                num = this._var5;
                            }
                        }
                    }
                }
            }
            
            if (((page != null) && (page.PageIndex == (this._Pages.MaxPages + 1))) && (page.mtd350 > 0))
            {
                this._Pages.AddPage(page);
            }
            int num3 = (this.mtd271.mtd166 - 1) / 2;
            for (int i = 0; i < num3; i++)
            {
                bool flag;
                mtd2 = this.mtd271[i].mtd290;
                Page page3 = this._Pages.GetPage(this._Pages.MaxPages - 1);
                page3 = this.RenderGroopHeader(ref mtd2.mtd242.mtd357, ref page3, ref num, out flag);
                if ((page3 != null) && flag)
                {
                    this._Pages.Remove((int) (this._Pages.MaxPages - 1));
                    this._Pages.AddPage(page3);
                }
            }
            if (this.mtd274 != null)
            {
                this.RenderReportFooter(ref this.mtd274);
            }
            this._Report.MaxPages = this._Pages.MaxPages;
            if (this.mtd275 != null)
            {
                this.RenderPageHeaderAndFooter();
            }
        }

        private void var25()
        {
            this._Pages.AddPage(new Page(this._Pages.MaxPages + 1));
        }

        private void var26()
        {
            this.mtd267 = null;
            this._CodeProvider.Clear();
        }

        private Page var27(ref mtd272 var34, ref Page var35, ref float var36)
        {
            Page page = var35;
            mtd289 mtd = var34.mtd290.mtd242;
            if (!mtd.reportHeader.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { mtd.reportHeader, EventArgs.Empty };
                this._CodeProvider.mtd71(mtd.reportHeader.Name, Methods._Initialize, objArray);
            }
            if (!mtd.reportHeader.Visible)
            {
                return page;
            }
            mtd209 mtd2 = new mtd209();
            mtd163 mtd3 = this._var10.mtd292(this._var14, ref var34, ref mtd2, var36, false);
            if (mtd2.mtd166 > 0)
            {
                return this.var37(ref mtd3, ref mtd2, ref page, mtd.mtd112, ref var36, false);
            }
            return this.var38(ref var36, ref mtd3, ref page);
        }

        private Page var28(ref mtd272 var34, ref float var36, ref Page var35)
        {
            Page page = var35;
            mtd289 mtd = var34.mtd290.mtd242;
            if (!this._var13)
            {
                this._DataFields.mtd174();
                if (this._DataFields.mtd189)
                {
                    return page;
                }
                this._var13 = true;
            }
            this._var14 = this._DataFields.mtd190;
            if (!mtd245.mtd294(ref mtd))
            {
                bool flag;
                mtd.mtd295();
                page = this.RenderGroopHeader(ref mtd.mtd357, ref page, ref var36, out flag);
                mtd.mtd356 = var36;
                if (!mtd.groupHeader.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
                {
                    object[] objArray = new object[] { mtd.groupHeader, EventArgs.Empty };
                    this._CodeProvider.mtd71(mtd.groupHeader.Name, Methods._Initialize, objArray);
                }
                bool visible = mtd.groupHeader.Visible;
                mtd209 mtd2 = new mtd209();
                mtd163 mtd3 = null;
                if (visible)
                {
                    mtd3 = this._var10.mtd292(this._var14, ref var34, ref mtd2, var36, true);
                }
                mtd.mtd357 = mtd3;
                this._var17 = page.PageIndex;
                if (visible)
                {
                    if (mtd2.mtd166 > 0)
                    {
                        page = this.var37(ref mtd3, ref mtd2, ref page, mtd.mtd112, ref var36, false);
                    }
                    else
                    {
                        page = this.var38(ref var36, ref mtd3, ref page);
                    }
                }
                mtd.mtd355 = this._var17;
                mtd.mtd297 = this._var14;
            }
            return page;
        }

        //var29
        private Page RenderReportDetail(ref mtd272 var34, ref float var36, ref Page var35)
        {
            bool flag;
            Page page2 = var35;
            ReportDetail detail = var34.mtd290.mtd242.reportDetail;
            var34.mtd298 = true;
            if ((this.mtd267 != null) && (this.mtd267.Length > 0))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            while (!this._DataFields.mtd189)
            {
                if (!this._var13)
                {
                    this._DataFields.mtd174();
                    if (this._DataFields.mtd189)
                    {
                        this._var13 = true;
                        return page2;
                    }
                }
                else
                {
                    this._var13 = false;
                }
                this._var14 = this._DataFields.mtd190;
                if (flag && !mtd245.mtd299(ref this.mtd267))
                {
                    this._var13 = false;
                    this._var14--;
                    this._DataFields.mtd192 = true;
                    return page2;
                }
                mtd245.mtd300(ref var34);
                if (!detail.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
                {
                    object[] objArray = new object[] { detail, EventArgs.Empty };
                    this._CodeProvider.mtd71(detail.Name, Methods._Initialize, objArray);
                }
                NewPage newPage = detail.NewPage;
                if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.Before))) && (page2.mtd350 > 0))
                {
                    this._Pages.AddPage(page2);
                    page2 = new Page(page2.PageIndex + 1);
                    var36 = this._var5;
                }
                mtd209 mtd2 = new mtd209();
                mtd163 mtd3 = null;
                if (detail.Visible)
                {
                    mtd3 = this._var10.mtd292(this._var14, ref var34, ref mtd2, var36, true);
                }
                var34.mtd298 = false;
                if (detail.Visible)
                {
                    if (mtd2.mtd166 > 0)
                    {
                        page2 = this.var37(ref mtd3, ref mtd2, ref page2, detail.KeepTogether, ref var36, false);
                    }
                    else
                    {
                        page2 = this.var38(ref var36, ref mtd3, ref page2);
                    }
                }
                if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.After))) && (page2.mtd350 > 0))
                {
                    this._Pages.AddPage(page2);
                    page2 = new Page(page2.PageIndex + 1);
                    var36 = this._var5;
                }
            }
            return page2;
        }

        //var30
        private Page RenderGroopFooter(ref mtd272 var34, ref float var36, ref Page var35)
        {
            Page page = var35;
            mtd289 mtd = var34.mtd290.mtd242;
            mtd289 mtd2 = mtd.mtd302;
            if ((mtd2 == null) || !(this._DataFields.mtd189 | !mtd245.mtd294(ref mtd2)))
            {
                return page;
            }
            this._var10.mtd303(ref var34.mtd304, mtd2.mtd297, this._var14);
            if (!mtd.groupFooter.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { mtd.groupFooter, EventArgs.Empty };
                this._CodeProvider.mtd71(mtd.groupFooter.Name, Methods._Initialize, objArray);
            }
            if (!mtd.groupFooter.Visible)
            {
                return page;
            }
            mtd209 mtd3 = new mtd209();
            mtd163 mtd4 = this._var10.mtd292(this._var14, ref var34, ref mtd3, var36, false);
            if (mtd3.mtd166 > 0)
            {
                return this.var37(ref mtd4, ref mtd3, ref page, mtd.mtd112, ref var36, mtd.groupFooter.PrintAtBottom);
            }
            if (mtd.groupFooter.PrintAtBottom)
            {
                return this.var47(ref var36, ref mtd4, ref page, mtd.mtd112);
            }
            return this.var38(ref var36, ref mtd4, ref page);
        }

        //var31
        private Page RenderGroopHeader(ref mtd163 var39, ref Page var40, ref float var36, out bool var41)
        {
            if (var39 != null)
            {
                mtd289 mtd = var39.mtd242;
                if ((((mtd.groupHeader.GroupKeepTogether == GroupKeepTogether.All) && !this.mtd370) && (!this.mtd371 && ((var40.PageIndex - mtd.mtd355) == 1))) && ((var40.mtd350 > 0) && (mtd.mtd356 > this._var5)))
                {
                    Page page = this._Pages.GetPage(mtd.mtd355 - 1);
                    int num = page.mtd349.mtd215(var39);
                    if ((num != -1) && (this.var42(num, ref page, ref var40) <= (this._var6 - this._var5)))
                    {
                        Page page2 = this.var43(ref var36, num, ref page, ref var40);
                        var41 = true;
                        return page2;
                    }
                }
            }
            var41 = false;
            return var40;
        }
        //RenderReportFooter
        private void RenderReportFooter(ref mtd272 var34)
        {
            mtd163 mtd;
            float num;
            mtd289 mtd2 = var34.mtd290.mtd242;
            Page page = this._Pages.GetPage(this._Pages.MaxPages - 1);
            NewPage newPage = mtd2.reportFooter.NewPage;
            if (((newPage != NewPage.None) && ((newPage == NewPage.BeforeAfter) | (newPage == NewPage.Before))) && (page.mtd350 > 0))
            {
                page = new Page(this._Pages.MaxPages + 1);
            }
            else
            {
                this._Pages.Remove((int) (this._Pages.MaxPages - 1));
            }
            if (page.mtd350 == 0)
            {
                num = this._var5;
            }
            else
            {
                mtd = page.mtd349.mtd143(page.mtd350 - 1);
                num = mtd.mtd29 + mtd.Height;
            }
            this._var10.mtd303(ref var34.mtd304, 0, this._var14);
            if (!mtd2.reportFooter.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { mtd2.reportFooter, EventArgs.Empty };
                this._CodeProvider.mtd71(mtd2.reportFooter.Name, Methods._Initialize, objArray);
            }
            mtd209 mtd3 = new mtd209();
            if (mtd2.reportFooter.Visible)
            {
                mtd = this._var10.mtd292(this._var14, ref var34, ref mtd3, num, false);
                if (mtd3.mtd166 > 0)
                {
                    page = this.var37(ref mtd, ref mtd3, ref page, mtd2.mtd112, ref num, mtd2.reportFooter.PrintAtBottom);
                }
                else if (mtd2.reportFooter.PrintAtBottom)
                {
                    page = this.var47(ref num, ref mtd, ref page, mtd2.mtd112);
                }
                else
                {
                    page = this.var38(ref num, ref mtd, ref page);
                }
            }
            this._Pages.AddPage(page);
        }
        //var33
        private void RenderPageHeaderAndFooter()
        {
            mtd163 mtd3 = this.mtd275.mtd290;
            mtd163 mtd4 = this.mtd276.mtd290;
            mtd3.mtd29 += this._var3;
            mtd4.mtd29 += this._var4 - mtd4.Height;
            mtd307[] mtdArray = this.mtd276.mtd308;
            mtd289 mtd5 = mtd3.mtd242;
            mtd289 mtd6 = mtd4.mtd242;
            int length = -1;
            if (mtdArray != null)
            {
                length = mtdArray.Length;
            }
            for (int i = 0; i < this._Pages.MaxPages; i++)
            {
                Page page = this._Pages.GetPage(i);
                if (!mtd5.pageHeader.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
                {
                    object[] objArray = new object[] { mtd5.pageHeader, EventArgs.Empty };
                    this._CodeProvider.mtd71(mtd5.pageHeader.Name, Methods._Initialize, objArray);
                }
                page.mtd347 = this.var48(ref this.mtd275);
                for (int j = 0; j < length; j++)
                {
                    mtd307 mtd = mtdArray[j];
                    mtd159 mtd2 = mtd.mtd309.mtd242;
                    McTextBox box = mtd2._McTextBox;
                    int num2 = this.var49(ref page, ref mtd, 0, page.mtd350 - 1, 1);
                    if (num2 != -1)
                    {
                        int num3 = this.var49(ref page, ref mtd, page.mtd350 - 1, num2 + 1, -1);
                        if (num3 < num2)
                        {
                            num3 = num2;
                        }
                        box.Value = mtd2._McField.mtd110(num2, num3, box.SummaryFunc);
                    }
                    else
                    {
                        box.Value = null;
                    }
                }
                if (!mtd6.pageFooter.mtd117(Msg.Initialize) && this._CodeProvider.mtd178)
                {
                    object[] objArray2 = new object[] { mtd6.pageFooter, EventArgs.Empty };
                    this._CodeProvider.mtd71(mtd6.pageFooter.Name, Methods._Initialize, objArray2);
                }
                page.mtd348 = this.var48(ref this.mtd276);
            }
        }

        private Page var37(ref mtd163 var54, ref mtd209 var55, ref Page pg, bool var56, ref float var36, bool var57)
        {
            float num = 0f;
            float num2 = 0f;
            Page page = pg;
            mtd163 mtd = var54;
            mtd163 mtd2 = null;
            foreach (float num3 in var55)
            {
                num2 = num3 - num;
                num = num3;
                this._var10.mtd311(ref mtd, out mtd2, this._var5, num2 + var36);
                if (var57)
                {
                    page = this.var47(ref var36, ref mtd, ref page, var56);
                }
                else
                {
                    page = this.var38(ref var36, ref mtd, ref page);
                }
                mtd = mtd2;
                this._Pages.AddPage(page);
                page = new Page(page.PageIndex + 1);
                var36 = this._var5;
            }
            var36 = this._var5;
            if (var57)
            {
                return this.var47(ref var36, ref mtd, ref page, var56);
            }
            return this.var38(ref var36, ref mtd, ref page);
        }

        private Page var38(ref float var36, ref mtd163 var54, ref Page pg)
        {
            Page page = pg;
            if ((var36 + var54.Height) > this._var6)
            {
                if (var54.mtd242.mtd112)
                {
                    if (var36 > this._var5)
                    {
                        if (var54.Height <= (this._var6 - this._var5))
                        {
                            this._Pages.AddPage(page);
                            page = new Page(page.PageIndex + 1);
                            var54.mtd29 = this._var5;
                            page.mtd351(var54);
                            var36 = this._var5 + var54.Height;
                            this._var17 = page.PageIndex;
                            return page;
                        }
                        this._var17 = page.PageIndex;
                        return this.var58(ref var36, ref var54, ref page, false);
                    }
                    this._var17 = page.PageIndex;
                    return this.var58(ref var36, ref var54, ref page, false);
                }
                this._var17 = page.PageIndex;
                return this.var58(ref var36, ref var54, ref page, false);
            }
            this._var17 = page.PageIndex;
            var36 += var54.Height;
            page.mtd351(var54);
            return page;
        }

        private float var42(int var44, ref Page var45, ref Page var46)
        {
            float num = 0f;
            Page[] pageArray = new Page[] { var45, var46 };
            for (int i = 0; i < 2; i++)
            {
                Page page = pageArray[i];
                for (int j = var44; j < page.mtd350; j++)
                {
                    mtd163 mtd = page.mtd349.mtd143(j);
                    num += mtd.Height;
                }
                var44 = 0;
            }
            return num;
        }

        private Page var43(ref float var36, int var44, ref Page var45, ref Page var46)
        {
            mtd163 oPRow = null;
            Page page = var45;
            Page page2 = new Page(var45.PageIndex + 1);
            var36 = this._var5;
            for (int i = 0; i < 2; i++)
            {
                int num2 = page.mtd350;
                for (int j = var44; j < num2; j++)
                {
                    mtd163 nPRow = page.mtd349.mtd143(var44);
                    if ((oPRow != null) && oPRow.mtd286)
                    {
                        this._var10.mtd313(ref oPRow, ref nPRow);
                        var36 += nPRow.Height;
                        var44++;
                    }
                    else
                    {
                        nPRow.mtd29 = var36;
                        page2.mtd351(nPRow);
                        if (nPRow._SectionType == SectionType.GroupHeader)
                        {
                            nPRow.mtd242.mtd355 = page2.PageIndex;
                            nPRow.mtd242.mtd356 = var36;
                        }
                        var36 += nPRow.Height;
                        page.mtd349.mtd217(nPRow);
                    }
                    oPRow = nPRow;
                }
                var44 = 0;
                page = var46;
            }
            return page2;
        }

        private Page var47(ref float var36, ref mtd163 var54, ref Page pg, bool var56)
        {
            Page page = pg;
            if ((var36 + var54.Height) > this._var6)
            {
                if (var56 && (var54.Height <= (this._var6 - this._var5)))
                {
                    this._Pages.AddPage(page);
                    page = new Page(page.PageIndex + 1);
                    var54.mtd29 = this._var6 - var54.Height;
                    page.mtd351(var54);
                    var36 = this._var6;
                    return page;
                }
                return this.var58(ref var36, ref var54, ref page, true);
            }
            var54.mtd29 = this._var6 - var54.Height;
            page.mtd351(var54);
            var36 = this._var6;
            return page;
        }

        private mtd163 var48(ref mtd272 var34)
        {
            mtd163 mtd = var34.mtd290;
            mtd314[] mtdArray = var34.mtd343;
            mtd163 mtd2 = mtd.mtd105(mtd.mtd29, false);
            mtd164 mtd3 = mtd2.mtd167;
            foreach (mtd314 mtd4 in mtdArray)
            {
                switch (mtd4._ControlType)
                {
                    case ControlType.TextBox:
                    {
                        mtd158 mtd5 = mtd158.mtd105(ref mtd4.mtd309, mtd4.mtd325);
                        mtd3.mtd2(mtd5);
                        break;
                    }
                    case ControlType.Label:
                    {
                        mtd148 mtd6 = mtd148.mtd105(ref mtd4.mtd317, mtd4.mtd325);
                        mtd3.mtd2(mtd6);
                        break;
                    }
                    case ControlType.Line:
                    {
                        mtd149 mtd7 = mtd149.mtd105(ref mtd4.mtd318, mtd4.mtd325);
                        mtd3.mtd2(mtd7);
                        break;
                    }
                    case ControlType.Picture:
                    {
                        mtd152 mtd8 = mtd152.mtd105(ref mtd4.mtd320, mtd4.mtd325);
                        mtd3.mtd2(mtd8);
                        break;
                    }
                    case ControlType.CheckBox:
                    {
                        mtd136 mtd9 = mtd136.mtd105(ref mtd4.mtd319, mtd4.mtd325);
                        mtd3.mtd2(mtd9);
                        break;
                    }
                    case ControlType.Shape:
                    {
                        mtd156 mtd10 = mtd156.mtd105(ref mtd4.mtd321, mtd4.mtd325);
                        mtd3.mtd2(mtd10);
                        break;
                    }
                    case ControlType.RichTextField:
                    {
                        mtd155 mtd11 = mtd155.mtd105(ref mtd4.mtd323, mtd4.mtd325);
                        mtd3.mtd2(mtd11);
                        break;
                    }
                }
            }
            return mtd2;
        }

        private int var49(ref Page pg, ref mtd307 var51, int var44, int var52, int var53)
        {
            bool flag = false;
            if ((var53 > 0) && (var44 <= var52))
            {
                flag = true;
            }
            else if ((var53 < 0) && (var44 >= var52))
            {
                flag = true;
            }
            while (flag)
            {
                mtd163 mtd = pg.mtd349.mtd143(var44);
                if (mtd._SectionType == SectionType.ReportDetail)
                {
                    if (!var51.mtd346)
                    {
                        if (!mtd.mtd286)
                        {
                            return mtd.Index;
                        }
                        foreach (mtd126 mtd2 in mtd.mtd167)
                        {
                            if ((mtd2.ControlType == ControlType.TextBox) && (mtd2.RptControl == var51.mtd345))
                            {
                                var51.mtd346 = true;
                                return mtd.Index;
                            }
                        }
                    }
                    else
                    {
                        var51.mtd346 = false;
                    }
                }
                if (var44 == var52)
                {
                    flag = false;
                }
                else
                {
                    var44 += var53;
                }
            }
            return -1;
        }

        private Page var58(ref float var36, ref mtd163 var54, ref Page pg, bool var57)
        {
            bool flag = false;
            mtd163 mtd = null;
            mtd163 mtd2 = var54;
            Page page = pg;
            while (!flag)
            {
                this._var10.mtd311(ref mtd2, out mtd, this._var5, this._var6);
                page.mtd351(mtd2);
                this._Pages.AddPage(page);
                page = new Page(page.PageIndex + 1);
                if ((mtd.mtd29 + mtd.Height) <= this._var6)
                {
                    flag = true;
                }
                else
                {
                    mtd2 = mtd;
                }
            }
            if (var57)
            {
                mtd.mtd29 = this._var6 - mtd.Height;
                var36 = this._var6;
            }
            else
            {
                var36 = this._var5 + mtd.Height;
            }
            page.mtd351(mtd);
            return page;
        }

        //var59
        private void PrintVersion(System.Drawing.Graphics g)
        {
            float x = 0.3f;
            float num2 = 1.02f;
            float y = 0.3f;
            float num4 = 0.45f;
            if (g.PageUnit == GraphicsUnit.Display)
            {
                x *= ReportUtil.Dpi;
                num2 *= ReportUtil.Dpi;
                y *= ReportUtil.Dpi;
                num4 *= ReportUtil.Dpi;
            }
            using (SolidBrush brush = new SolidBrush(Color.Gray))
            {
                g.DrawString("Nistec ReportView Version 4.0.1.0", this.var83(), brush, x, y);
            }
        }

        //var60
        private void PrintControls(System.Drawing.Graphics g, mtd163 var54, float var62, float var66)
        {
            Region clip = g.Clip;
            Section section = var54.mtd242._Section;
            section.mtd117(Msg.OnPrint);
            if (section.Visible && var54.mtd86)
            {
                RectangleF ef2;
                if (g.PageUnit == GraphicsUnit.Display)
                {
                    ef2 = new RectangleF(var66, var62 + (var54.mtd29 * ReportUtil.Dpi), this._ReportWidth * ReportUtil.Dpi, var54.Height * ReportUtil.Dpi);
                    RectangleF ef3 = ef2;
                    this.var67(ref ef3, 0.25f);
                    g.IntersectClip(new Region(ef3));
                }
                else
                {
                    ef2 = new RectangleF(var66, var62 + var54.mtd29, this._ReportWidth, var54.Height);
                    RectangleF ef4 = ef2;
                    this.var67(ref ef4, 0.01f);
                    g.IntersectClip(new Region(ef4));
                }
                if (var54.BackColor != Color.Transparent)
                {
                    using (SolidBrush brush = new SolidBrush(var54.BackColor))
                    {
                        g.FillRectangle(brush, ef2);
                    }
                }
                foreach (mtd126 mtd in var54.mtd167)
                {
                    RectangleF ef;
                    if (!mtd.mtd130)
                    {
                        continue;
                    }
                    ControlType type = mtd.ControlType;
                    if (type == ControlType.SubReport)
                    {
                        mtd161 mtd2 = (mtd161) mtd;
                        McSubReport report = mtd2.mtd264.mtd269;
                        if (g.PageUnit == GraphicsUnit.Display)
                        {
                            ef = new RectangleF(var66 + (mtd2.mtd128 * ReportUtil.Dpi), var62 + ((var54.mtd29 + mtd2.mtd129) * ReportUtil.Dpi), report.Width * ReportUtil.Dpi, mtd2.Height * ReportUtil.Dpi);
                        }
                        else
                        {
                            ef = new RectangleF(var66 + mtd2.mtd128, (var62 + var54.mtd29) + mtd2.mtd129, report.Width, mtd2.Height);
                        }
                        report.mtd22(g, ef);
                        this.var63(ref g, ref mtd2, ef);
                        continue;
                    }
                    if (g.PageUnit == GraphicsUnit.Display)
                    {
                        ef = new RectangleF(var66 + (mtd.mtd128 * ReportUtil.Dpi), var62 + ((var54.mtd29 + mtd.mtd129) * ReportUtil.Dpi), mtd.Width * ReportUtil.Dpi, mtd.Height * ReportUtil.Dpi);
                    }
                    else
                    {
                        ef = new RectangleF(var66 + mtd.mtd128, (var62 + var54.mtd29) + mtd.mtd129, mtd.Width, mtd.Height);
                    }
                    switch (type)
                    {
                        case ControlType.TextBox:
                        {
                            mtd158.mtd22(g, (mtd158) mtd, ef);
                            continue;
                        }
                        case ControlType.Label:
                        {
                            mtd148.mtd22(g, (mtd148) mtd, ef);
                            continue;
                        }
                        case ControlType.Line:
                        {
                            mtd149.mtd22(g, (mtd149) mtd, var62, var66, var54.mtd29);
                            continue;
                        }
                        case ControlType.Picture:
                        {
                            mtd152.mtd22(g, (mtd152) mtd, ef);
                            continue;
                        }
                        case ControlType.CheckBox:
                        {
                            mtd136.mtd22(g, (mtd136) mtd, ef);
                            continue;
                        }
                        case ControlType.Shape:
                        {
                            mtd156.mtd22(g, (mtd156) mtd, ef);
                            continue;
                        }
                        case ControlType.RichTextField:
                            mtd155.mtd22(g, (mtd155) mtd, ef);
                            break;
                    }
                }
                g.Clip = clip;
            }
        }

        private void var61(System.Drawing.Graphics g, Page pg, float var62)
        {
            float num = this._var1;
            if (g.PageUnit == GraphicsUnit.Display)
            {
                num *= ReportUtil.Dpi;
            }
            foreach (mtd163 mtd in pg.mtd349)
            {
                this.PrintControls(g, mtd, var62, num);
            }
        }

        private void var63(ref System.Drawing.Graphics g, ref mtd161 var64, RectangleF var65)
        {
            Region clip = g.Clip;
            g.IntersectClip(new Region(var65));
            foreach (mtd163 mtd in var64.mtd162)
            {
                this.PrintControls(g, mtd, var65.Top, var65.Left);
            }
            g.Clip = clip;
        }

        private void var67(ref RectangleF _var68, float var69)
        {
            _var68.X -= var69;
            _var68.Y -= 2f * var69;
            _var68.Width += 2f * var69;
            _var68.Height += 2f * var69;
        }

        private mtd198 var71(mtd207 var79, FindInfo var78, ref RectangleF var68, bool var82)
        {
            mtd198 mtd = null;
            int count = -1;
            int num2 = -1;
            int num3 = -1;
            bool flag = false;
            mtd161 mtd2 = (mtd161) var79.mtd199;
            mtd163 mtd3 = null;
            int step = var78.Step;
            if (var82)
            {
                num2 = var79.mtd203.mtd200;
            }
            else if (var78.Step == 1)
            {
                num2 = 0;
            }
            else
            {
                num2 = mtd2.mtd162.Count - 1;
            }
            if (var78.Step == 1)
            {
                count = mtd2.mtd162.Count;
                if (num2 < count)
                {
                    flag = true;
                    mtd3 = mtd2.mtd162.mtd143(num2);
                    if (var79.mtd204 != null)
                    {
                        num3 = var79.mtd204.mtd200 + 1;
                    }
                    else
                    {
                        num3 = 0;
                    }
                }
            }
            else
            {
                count = 0;
                if (num2 >= count)
                {
                    flag = true;
                    mtd3 = mtd2.mtd162.mtd143(num2);
                    if (var79.mtd204 != null)
                    {
                        num3 = var79.mtd204.mtd200 - 1;
                    }
                    else
                    {
                        num3 = mtd3.mtd167.mtd166 - 1;
                    }
                }
            }
            while (flag)
            {
                mtd = this.var76(var79.mtd29, var79.mtd28, mtd3, num3, var78, var79, ref var68);
                if (mtd != null)
                {
                    break;
                }
                num2 += var78.Step;
                if (var78.Step == 1)
                {
                    if (num2 < count)
                    {
                        flag = true;
                        mtd3 = mtd2.mtd162.mtd143(num2);
                        num3 = 0;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else if (num2 >= count)
                {
                    flag = true;
                    mtd3 = mtd2.mtd162.mtd143(num2);
                    num3 = mtd3.mtd167.mtd166 - 1;
                }
                else
                {
                    flag = false;
                }
            }
            if (mtd != null)
            {
                if (!this._var70)
                {
                    this._var70 = true;
                    var79.mtd203.mtd205 = mtd3;
                    var79.mtd203.mtd200 = num2;
                    var79.mtd204 = mtd;
                    McLocation mtd4 = mtd.mtd199._Location;
                    var68 = new RectangleF(var79.mtd28 + (mtd4.Left * ReportUtil.Dpi), var79.mtd29 + ((mtd4.Top + mtd3.mtd29) * ReportUtil.Dpi), mtd4.Width * ReportUtil.Dpi, mtd4.Height * ReportUtil.Dpi);
                    return var79;
                }
                var79.mtd203.mtd205 = mtd3;
                var79.mtd203.mtd200 = num2;
                var79.mtd204 = mtd;
            }
            return mtd;
        }
        //var72
        private bool FindAtHeaderAndFooter(int var73, ref bool var74, ref FindInfo var75)
        {
            mtd198 mtd = null;
            mtd163 mtd2 = null;
            SectionType pageHeader;
            int num = -1;
            RectangleF ef = new RectangleF();
            float num2 = this._var1 * ReportUtil.Dpi;
            int step = var75.Step;
            Page page = var75.mtd201;
            if (var75.mtd203.mtd205 != null)
            {
                pageHeader = var75.mtd203.mtd205._SectionType;
                if ((pageHeader != SectionType.PageHeader) && (pageHeader != SectionType.ReportFooter))
                {
                    num = var75.mtd203.mtd200;
                }
                else
                {
                    num = -1;
                }
            }
            else if (step == 1)
            {
                pageHeader = SectionType.PageHeader;
            }
            else
            {
                pageHeader = SectionType.PageFooter;
            }
            if (step == 1)
            {
                if (var73 == -1)
                {
                    var73 = 0;
                }
                if (pageHeader == SectionType.PageHeader)
                {
                    if (page.mtd347 != null)
                    {
                        mtd2 = page.mtd347;
                        mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                        if (mtd != null)
                        {
                            goto Label_0240;
                        }
                    }
                    var73 = 0;
                }
                if (pageHeader != SectionType.PageFooter)
                {
                    if (num == -1)
                    {
                        num = 0;
                    }
                    for (int i = num; i < page.mtd350; i++)
                    {
                        mtd2 = page.mtd349.mtd143(i);
                        mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                        if (mtd != null)
                        {
                            num = i;
                            goto Label_0240;
                        }
                        var73 = 0;
                    }
                    var73 = 0;
                }
                if (page.mtd348 != null)
                {
                    mtd2 = page.mtd348;
                    mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                }
            }
            else
            {
                if (pageHeader == SectionType.PageFooter)
                {
                    if (page.mtd348 != null)
                    {
                        mtd2 = page.mtd348;
                        if ((var73 == -1) && !var74)
                        {
                            var73 = page.mtd348.mtd167.mtd166 - 1;
                        }
                        var74 = false;
                        mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                        if (mtd != null)
                        {
                            goto Label_0240;
                        }
                    }
                    var73 = -1;
                }
                if (pageHeader != SectionType.PageHeader)
                {
                    if (num == -1)
                    {
                        num = page.mtd350 - 1;
                    }
                    for (int j = num; j >= 0; j--)
                    {
                        mtd2 = page.mtd349.mtd143(j);
                        if ((var73 == -1) && !var74)
                        {
                            var73 = mtd2.mtd167.mtd166 - 1;
                        }
                        var74 = false;
                        mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                        if (mtd != null)
                        {
                            num = j;
                            goto Label_0240;
                        }
                        var73 = -1;
                    }
                    var73 = -1;
                }
                if (page.mtd347 != null)
                {
                    if ((var73 == -1) && !var74)
                    {
                        var73 = page.mtd347.mtd167.mtd166 - 1;
                    }
                    var74 = false;
                    mtd2 = page.mtd347;
                    mtd = this.var76(0f, num2, mtd2, var73, var75, null, ref ef);
                }
            }
        Label_0240:
            if (mtd != null)
            {
                var75.mtd203.mtd205 = mtd2;
                if ((mtd2._SectionType != SectionType.PageHeader) && (mtd2._SectionType != SectionType.ReportFooter))
                {
                    var75.mtd203.mtd200 = num;
                }
                else
                {
                    var75.mtd203.mtd200 = -1;
                }
                var75.mtd204 = mtd;
                var75.mtd206(ef);
                return true;
            }
            var75.mtd203.mtd205 = null;
            var75.mtd203.mtd200 = -1;
            var75.mtd204 = null;
            var75.mtd57 = RectangleF.Empty;
            return false;
        }

        private mtd198 var76(float var62, float var66, mtd163 var77, int var73, FindInfo var78, mtd207 var79, ref RectangleF var68)
        {
            int num;
            bool flag = false;
            if (var78.Step == 1)
            {
                num = var77.mtd167.mtd166;
                if (var73 < num)
                {
                    flag = true;
                }
            }
            else
            {
                num = 0;
                if (var73 >= num)
                {
                    flag = true;
                }
            }
            while (flag)
            {
                mtd198 mtd2;
                mtd126 mtd3 = var77.mtd167[var73];
                if (mtd3.ControlType == ControlType.SubReport)
                {
                    mtd207 mtd4 = new mtd207(var79);
                    mtd4.mtd199 = mtd3;
                    mtd4.mtd200 = var73;
                    mtd4.mtd29 = var62 + ((var77.mtd29 + mtd3.mtd129) * ReportUtil.Dpi);
                    mtd4.mtd28 = var66 + (mtd3.mtd128 * ReportUtil.Dpi);
                    mtd2 = this.var71(mtd4, var78, ref var68, false);
                    if (mtd2 != null)
                    {
                        return mtd2;
                    }
                }
                else if (mtd3.ControlType != ControlType.RichTextField)
                {
                    string str;
                    if (mtd3.ControlType == ControlType.Label)
                    {
                        mtd148 mtd5 = (mtd148) mtd3;
                        str = Convert.ToString(mtd5.mtd137);
                    }
                    else if (mtd3.ControlType == ControlType.TextBox)
                    {
                        mtd158 mtd6 = (mtd158) mtd3;
                        str = mtd159.mtd160(mtd6.mtd137, mtd6.mtd242.OutputFormat);
                    }
                    else
                    {
                        str = "";
                    }
                    if ((str.Length > 0) && this.var80(str, var78))
                    {
                        McLocation mtd = mtd3._Location;
                        var68 = new RectangleF(var66 + (mtd.Left * ReportUtil.Dpi), var62 + ((mtd.Top + var77.mtd29) * ReportUtil.Dpi), mtd.Width * ReportUtil.Dpi, mtd.Height * ReportUtil.Dpi);
                        mtd2 = new mtd198();
                        mtd2.mtd200 = var73;
                        mtd2.mtd199 = mtd3;
                        return mtd2;
                    }
                }
                var73 += var78.Step;
                if (var78.Step == 1)
                {
                    if (var73 < num)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else if (var73 >= num)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            var68 = Rectangle.Empty;
            return null;
        }

        private bool var80(string var81, FindInfo var78)
        {
            RegexOptions none;
            if (var78.IsMatchWhole)
            {
                return (string.Compare(var81, var78.Text, var78.IsMatchCase) == 0);
            }
            if (var78.IsMatchCase)
            {
                none = RegexOptions.None;
            }
            else
            {
                none = RegexOptions.IgnoreCase;
            }
            return Regex.IsMatch(var81, var78.Text, none);
        }

        private Font var83()
        {
            if (this._var20 == null)
            {
                this._var20 = new Font("Tahoma", 8f);
            }
            return this._var20;
        }

        internal FieldsCollection Fields
        {
            get
            {
                return this._DataFields.Fields;
            }
        }

        internal string mtd91
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        public float BottomMargin
        {
            get
            {
                return this._Report.PageSetting.Margins.MarginBottom;
            }
        }

        public System.Drawing.Graphics Graphics
        {
            get
            {
                return this.g;
            }
            set
            {
                this.g = value;
            }
        }

        public float LeftMargin
        {
            get
            {
                return this._Report.PageSetting.Margins.MarginLeft;
            }
        }

        public long MaxPages
        {
            get
            {
                return this._Report.MaxPages;
            }
        }

        public float PageHeight
        {
            get
            {
                return this._PrintDocument.PageHeight / 100f;// ReportUtil.Dpi;
            }
        }

        public PagesCollection Pages
        {
            get
            {
                return this._Pages;
            }
        }

        public float PageWidth
        {
            get
            {
                return this._PrintDocument.PageWidth / 100f;// ReportUtil.Dpi;
            }
        }

        public RectangleF PrintBound
        {
            get
            {
                return this._PrintDocument.PrintBound;
            }
        }

        public System.Drawing.Printing.PrinterSettings PrinterSettings
        {
            get
            {
                this._PrintDocument.mtd377();
                return this._PrintDocument.PrinterSettings;
            }
        }

        public Report Report
        {
            get
            {
                return this._Report;
            }
        }

        public float ReportWidth
        {
            get
            {
                return this._ReportWidth;
            }
        }

        public float RightMargin
        {
            get
            {
                return this._Report.PageSetting.Margins.MarginRight;
            }
        }

        public float TopMargin
        {
            get
            {
                return this._Report.PageSetting.Margins.MarginTop;
            }
        }
    }
}

