namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    //mtd373
    internal class InternalPrintDocument : PrintDocument
    {
        internal Document _mtd381;
        private int _var0;
        private int _var1;
        private float _var2;
        private float _var3;
        private RectangleF _var4;
        private bool _var5;

        internal InternalPrintDocument(Document var6)
        {
            this._mtd381 = var6;
            this._var4 = new RectangleF();
            this._var5 = false;
        }

        internal void mtd377()
        {
            if (!this._var5)
            {
                Nistec.Printing.View.PageSettings pageSetting = this._mtd381.Report.PageSetting;
                if (this.mtd380)
                {
                    try
                    {
                        base.DefaultPageSettings.Margins.Left = (int) (pageSetting.Margins.MarginLeft * 100f);
                        base.DefaultPageSettings.Margins.Bottom = (int) (pageSetting.Margins.MarginBottom * 100f);
                        base.DefaultPageSettings.Margins.Right = (int) (pageSetting.Margins.MarginRight * 100f);
                        base.DefaultPageSettings.Margins.Top = (int) (pageSetting.Margins.MarginTop * 100f);
                        if (pageSetting.Orientation == PageOrientation.Landscape)
                        {
                            base.DefaultPageSettings.Landscape = true;
                        }
                        if (pageSetting.Collate == PrinterCollate.Collate)
                        {
                            base.PrinterSettings.Collate = true;
                        }
                        else if (pageSetting.Collate == PrinterCollate.DontCollate)
                        {
                            base.PrinterSettings.Collate = false;
                        }
                        if (base.PrinterSettings.CanDuplex)
                        {
                            if (pageSetting.Duplex == PrinterDuplex.Default)
                            {
                                base.PrinterSettings.Duplex = Duplex.Default;
                            }
                            else if (pageSetting.Duplex == PrinterDuplex.Horizontal)
                            {
                                base.PrinterSettings.Duplex = Duplex.Horizontal;
                            }
                            else if (pageSetting.Duplex == PrinterDuplex.Simplex)
                            {
                                base.PrinterSettings.Duplex = Duplex.Simplex;
                            }
                            else if (pageSetting.Duplex == PrinterDuplex.Vertical)
                            {
                                base.PrinterSettings.Duplex = Duplex.Vertical;
                            }
                        }
                        this.var7(pageSetting);
                        this.var8(pageSetting);
                    }
                    catch
                    {
                    }
                }
                this._var5 = true;
            }
        }

        internal void mtd379()
        {
            Nistec.Printing.View.PageSettings pageSetting = this._mtd381.Report.PageSetting;
            if (pageSetting.PaperKind == PaperKind.Custom)
            {
                this._var2 = pageSetting.PaperHeight;
                this._var3 = pageSetting.PaperWidth;
            }
            else
            {
                SizeF sizeInInch = Nistec.Printing.View.PageSettings.PaperInfos[pageSetting.PaperKind].SizeInInch;
                this._var2 = sizeInInch.Height * 100f;
                this._var3 = sizeInInch.Width * 100f;
            }
            if (pageSetting.Orientation == PageOrientation.Landscape)
            {
                float num = this._var2;
                this._var2 = this._var3;
                this._var3 = num;
            }
            this._var4.X = pageSetting.Margins.MarginLeft;
            this._var4.Y = pageSetting.Margins.MarginTop;
            this._var4.Width = ((this._var3 / 100f) - pageSetting.Margins.MarginLeft) - pageSetting.Margins.MarginRight;
            this._var4.Height = ((this._var2 / 100f) - pageSetting.Margins.MarginTop) - pageSetting.Margins.MarginBottom;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            this._var0 = base.PrinterSettings.FromPage - 1;
            this._var1 = base.PrinterSettings.ToPage - 1;
            base.OnBeginPrint(e);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            this._var0 = 0;
            this._var1 = (int) this._mtd381.MaxPages;
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Inch;
            this._mtd381.PrintPage(g, this._var0, 0f);
            this._var0++;
            if (this._var0 > this._var1)
            {
                e.HasMorePages = false;
                this._var0 = 0;
            }
            base.OnPrintPage(e);
        }

        private void var7(Nistec.Printing.View.PageSettings ps)
        {
            if (ps.PaperKind == PaperKind.Custom)
            {
                base.DefaultPageSettings.PaperSize = new PaperSize("Custom", (int) this._var3, (int) this._var2);
            }
            else
            {
                bool flag = false;
                for (int i = 0; i < base.PrinterSettings.PaperSizes.Count; i++)
                {
                    if (base.PrinterSettings.PaperSizes[i].Kind == ps.PaperKind)
                    {
                        PaperSize size1 = base.PrinterSettings.PaperSizes[i];
                        base.DefaultPageSettings.PaperSize = base.PrinterSettings.PaperSizes[i];
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    base.DefaultPageSettings.PaperSize = new PaperSize("Custom", (int) this._var3, (int) this._var2);
                }
            }
        }

        private void var8(Nistec.Printing.View.PageSettings ps)
        {
            if (!ps.DefaultPaperSource)
            {
                for (int i = 0; i < base.DefaultPageSettings.PrinterSettings.PaperSources.Count; i++)
                {
                    if (base.PrinterSettings.PaperSources[i].Kind.Equals(ps.PaperSource))
                    {
                        base.DefaultPageSettings.PaperSource = base.DefaultPageSettings.PrinterSettings.PaperSources[i];
                        return;
                    }
                }
            }
        }

        internal RectangleF PrintBound//mtd374
        {
            get
            {
                return this._var4;
            }
        }

        internal float PageWidth//mtd375
        {
            get
            {
                return this._var3;
            }
        }

        internal float PageHeight//mtd376
        {
            get
            {
                return this._var2;
            }
        }

        internal bool mtd380
        {
            get
            {
                bool flag = false;
                try
                {
                    if (PrinterSettings.InstalledPrinters.Count > 0)
                    {
                        return true;
                    }
                    flag = false;
                }
                catch
                {
                }
                return flag;
            }
        }

        internal int mtd382
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        internal int mtd383
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
    }
}

