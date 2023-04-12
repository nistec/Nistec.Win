namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd785 : mtd747
    {
        internal mtd785(PDFDocument var0)
        {
            base._mtd756 = var0;
        }

        internal override void mtd710(ref mtd711 var1)
        {
            base._mtd756.mtd757.mtd758(var1.mtd32, 0);
            var1.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var1.mtd715("[/PDF /Text /ImageC]");
            var1.mtd715("endobj");
        }
    }
}

