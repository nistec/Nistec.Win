namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd746 : mtd747
    {
        internal PageMode mtd748;
        internal bool mtd749;
        internal bool mtd750;
        internal bool mtd751;
        internal bool mtd752;
        internal bool mtd753;
        internal bool mtd754;
        internal PageMode mtd755;

        internal mtd746(PDFDocument var0)
        {
            base._mtd756 = var0;
            this.mtd748 = PageMode.UseNone;
            this.mtd749 = false;
            this.mtd750 = false;
            this.mtd751 = false;
            this.mtd752 = false;
            this.mtd753 = false;
            this.mtd754 = false;
            this.mtd755 = PageMode.UseNone;
        }

        internal override void mtd710(ref mtd711 var1)
        {
            base._mtd756.mtd757.mtd758(var1.mtd32, 0);
            var1.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var1.mtd715("<<");
            var1.mtd715("/Type /Catalog ");
            var1.mtd715(string.Format("/Pages {0} 0 R ", base._mtd756.mtd760.mtd759));
            var1.mtd715(string.Format("/Outlines {0} 0 R ", base._mtd756.mtd761.mtd759));
            if (base._mtd756.mtd762.mtd166 > 0)
            {
                var1.mtd715(string.Format("/PageMode /{0} ", PageMode.UseOutlines));
            }
            else
            {
                var1.mtd715(string.Format("/PageMode /{0} ", this.mtd748));
            }
            var1.mtd715("/ViewerPreferences <<");
            var1.mtd715(string.Format("/HideToolbar {0} ", mtd620.mtd650(this.mtd749)));
            var1.mtd715(string.Format("/HideMenubar {0} ", mtd620.mtd650(this.mtd750)));
            var1.mtd715(string.Format("/HideWindowUI {0} ", mtd620.mtd650(this.mtd751)));
            var1.mtd715(string.Format("/FitWindow {0} ", mtd620.mtd650(this.mtd752)));
            var1.mtd715(string.Format("/CenterWindow {0} ", mtd620.mtd650(this.mtd753)));
            var1.mtd715(string.Format("/DisplayDocTitle {0} ", mtd620.mtd650(this.mtd754)));
            var1.mtd715(string.Format("/NonFullScreenPageMode /{0} ", this.mtd755));
            var1.mtd715(">>");
            var1.mtd715(">>");
            var1.mtd715("endobj");
        }
    }
}

