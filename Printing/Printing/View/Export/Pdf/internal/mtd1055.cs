namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;

    internal class mtd1055
    {
        private float _var0;
        private float _var1;
        private PageRotate _var2;
        private PDFDocument _var3;
        private float _var4;
        private float _var5;
        private float _var6;

        internal mtd1055()
        {
        }

        internal void mtd172(PaperKind var7)
        {
            var8(var7, out this._var0, out this._var1);
            this._var2 = PageRotate.mtd677;
        }

        internal void mtd172(PaperKind var7, Nistec.Printing.View.Pdf.PageOrientation var9)
        {
            if (var9 == Nistec.Printing.View.Pdf.PageOrientation.mtd681)
            {
                var8(var7, out this._var0, out this._var1);
            }
            else
            {
                var8(var7, out this._var1, out this._var0);
            }
            this._var2 = PageRotate.mtd677;
        }

        internal void mtd172(float var10, float var11, float var12, float var13, float var14)
        {
            this._var0 = var10;
            this._var1 = var11;
            this._var5 = var12;
            this._var6 = var13;
            this._var2 = PageRotate.mtd677;
            this._var4 = (this._var0 - this._var5) - this._var6;
            if (var14 < this._var4)
            {
                this._var4 = var14;
            }
        }

        internal void mtd710(Page var15, int var16, ref mtd1061 var17, ref mtd711 var18)
        {
            mtd747 mtd;
            mtd742 mtd3 = new mtd742();
            mtd944 mtd4 = new mtd944(this, ref var17);
            var19(this._var5, 0f, this._var4, var15, ref mtd4, ref mtd3);
            mtd757 mtd5 = this._var3.mtd757;
            mtd5.mtd758(var18.mtd32, 0);
            var18.mtd715(string.Format("{0} 0 obj", var16));
            var18.mtd715("<<");
            var18.mtd715("/Type /Page ");
            var18.mtd715(string.Format("/Parent {0} 0 R ", this._var3.mtd760.mtd759));
            var18.mtd715(string.Format("/MediaBox {0} ", mtd620.mtd647(0f, 0f, this._var0, this._var1)));
            if (this._var2 == PageRotate.mtd677)
            {
                var18.mtd715(string.Format("/Rotate {0}", 0));
            }
            else if (this._var2 == PageRotate.mtd678)
            {
                var18.mtd715(string.Format("/Rotate {0}", 90));
            }
            else if (this._var2 == PageRotate.mtd679)
            {
                var18.mtd715(string.Format("/Rotate {0}", 180));
            }
            else if (this._var2 == PageRotate.mtd680)
            {
                var18.mtd715(string.Format("/Rotate {0}", 270));
            }
            int num = var16 + 1;
            if (mtd3.mtd32 > 0)
            {
                var18.mtd715(string.Format("/Contents {0} 0 R ", num));
            }
            var18.mtd715("/Resources <<");
            var18.mtd715(string.Format("/ProcSet {0} 0 R ", this._var3.mtd785.mtd759));
            mtd1062 mtd2 = mtd4.mtd638;
            if (mtd2.mtd32 > 0)
            {
                var18.mtd715("/ExtGState << ");
                for (int i = 0; i < mtd2.mtd32; i++)
                {
                    mtd = mtd2[i];
                    var18.mtd715(string.Format("/GS{0} {1} 0 R ", mtd.mtd763, mtd.mtd759));
                }
                var18.mtd715(">>");
            }
            mtd2 = mtd4.mtd642;
            if (mtd2.mtd32 > 0)
            {
                var18.mtd715("/XObject << ");
                for (int j = 0; j < mtd2.mtd32; j++)
                {
                    mtd = mtd2[j];
                    var18.mtd715(string.Format("/{0} {1} 0 R ", mtd.mtd763, mtd.mtd759));
                }
                var18.mtd715(">>");
            }
            mtd2 = mtd4.mtd1066;
            if (mtd2.mtd32 > 0)
            {
                var18.mtd715("/Font << ");
                for (int k = 0; k < mtd2.mtd32; k++)
                {
                    mtd = mtd2[k];
                    var18.mtd715(string.Format("/{0} {1} 0 R ", mtd.mtd763, mtd.mtd759));
                }
                var18.mtd715(">>");
            }
            var18.mtd715(">>");
            var18.mtd715(">>");
            var18.mtd715("endobj");
            if (mtd3.mtd32 > 0)
            {
                if (this._var3.Compress)
                {
                    mtd3.mtd745();
                }
                mtd5.mtd758(var18.mtd32, 0);
                mtd3.mtd710(var18, num, false, this._var3.mtd712);
            }
        }

        internal float mtd947(float y)
        {
            return (this._var1 - y);
        }

        private static void var19(float var20, float var21, float var0, Page var15, ref mtd944 var22, ref mtd742 var23)
        {
            mtd141 mtd = new mtd141();
            var15.mtd352(mtd);
            if (var15.mtd352(mtd) && mtd.mtd86)
            {
                var24(var20, var21, var0, mtd, ref var22, ref var23);
            }
            if (var15.mtd353(mtd) && mtd.mtd86)
            {
                var24(var20, var21, var0, mtd, ref var22, ref var23);
            }
            for (int i = 0; i < var15.SectionCount; i++)
            {
                if (var15.mtd141(mtd, i) && mtd.mtd86)
                {
                    var24(var20, var21, var0, mtd, ref var22, ref var23);
                }
            }
            var25(ref var22, ref var23);
        }

        private static void var24(float var20, float var21, float var0, mtd141 var26, ref mtd944 var22, ref mtd742 var23)
        {
            PropDoc mtd = new PropDoc();
            var21 += var26.mtd29 * 72f;
            float num = var26.Height * 72f;
            mtd942.mtd945(ref var22, ref var23);
            mtd942.mtd946(var23, var20, var22.mtd947(var21), var0, num);
            if (var26.BackColor != Color.Transparent)
            {
                mtd942.mtd948(var20, var21, var0, num, var26.BackColor, ref var22, ref var23);
            }
            for (int i = 0; i < var26.mtd166; i++)
            {
                if (var26.mtd168(mtd, i) && mtd.Visible)
                {
                    if ((mtd.ControlType == ControlType.TextBox) || (mtd.ControlType == ControlType.Label))
                    {
                        mtd238.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.Line)
                    {
                        mtd60.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.Shape)
                    {
                        mtd63.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.Picture)
                    {
                        mtd966.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.CheckBox)
                    {
                        mtd227.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.RichTextField)
                    {
                        mtd979.mtd23(var20, var21, mtd, ref var22, ref var23);
                    }
                    else if (mtd.ControlType == ControlType.SubReport)
                    {
                        var27(var20, var21, mtd, ref var22, ref var23);
                    }
                }
            }
            mtd942.mtd951(ref var22, ref var23);
        }

        private static void var25(ref mtd944 var22, ref mtd742 var23)
        {
            mtd51.mtd23(30f, 30f, "Nistec ReportView Version 4.0.1.0", Color.Gray, var22.mtd1064.mtd1003, 8f, false, false, ref var22, ref var23);
        }

        private static void var27(float var20, float var21, PropDoc var28, ref mtd944 var22, ref mtd742 var23)
        {
            mtd141 mtd = new mtd141();
            var20 += var28.Left * 72f;
            var21 += var28.Top * 72f;
            float num = var28.Width * 72f;
            float num2 = var28.Height * 72f;
            mtd942.mtd945(ref var22, ref var23);
            mtd942.mtd946(var23, var20, var22.mtd947(var21), num, num2);
            for (int i = 0; i < var28.Count; i++)
            {
                if (var28.mtd140(mtd, i) && mtd.mtd86)
                {
                    var24(var20, var21, num, mtd, ref var22, ref var23);
                }
            }
            mtd942.mtd951(ref var22, ref var23);
        }

        private static void var8(PaperKind var29, out float var0, out float var1)
        {
            SizeF sizeInInch = Nistec.Printing.View.PageSettings.PaperInfos[var29].SizeInInch;
            var0 = sizeInInch.Width * 72f;
            var1 = sizeInInch.Height * 72f;
        }

        internal PDFDocument mtd1064
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        internal PageRotate mtd1065
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var0;
            }
        }

        internal float mtd31
        {
            get
            {
                return this._var1;
            }
        }
    }
}

