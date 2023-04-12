namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd742 : mtd805
    {
        private int _var0;
        private bool _var1;

        internal mtd742()
        {
            this._var0 = 0;
            this._var1 = false;
        }

        internal mtd742(byte[] var2)
        {
            base._mtd1008 = var2;
            base._mtd218 = var2.Length;
        }

        internal mtd742(byte[] var2, int var3)
        {
            base._mtd1008 = var2;
            base._mtd218 = var3;
        }

        internal void mtd710(mtd711 var4, int var5, bool var6, mtd712 var7)
        {
            var4.mtd715(string.Format("{0} 0 obj", var5));
            if (this._var1)
            {
                var4.mtd710(string.Format("<< /Length {0} /Filter /FlateDecode ", base._mtd218));
            }
            else
            {
                var4.mtd710(string.Format("<< /Length {0} ", base._mtd218));
            }
            if (var6)
            {
                var4.mtd715(string.Format("/Length1 {0} >>", this._var0));
            }
            else
            {
                var4.mtd715(">>");
            }
            var4.mtd783("stream");
            byte[] buffer = base.mtd784;
            if (var7 != null)
            {
                var7.mtd775(var5, 0);
                buffer = var7.mtd713(buffer, base._mtd218);
                var4.mtd710(buffer, buffer.Length);
            }
            else
            {
                var4.mtd710(buffer, base._mtd218);
            }
            var4.mtd710((byte) 13);
            var4.mtd715("endstream");
            var4.mtd715("endobj");
        }

        internal void mtd745()
        {
            this._var0 = base._mtd218;
            if (this.mtd32 > 0x1d)
            {
                base._mtd1008 = mtd654.mtd655(base._mtd1008, base._mtd218);
                base._mtd218 = base._mtd1008.Length;
                this._var1 = true;
            }
        }
    }
}

