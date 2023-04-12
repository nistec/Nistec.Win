namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd806 : mtd712
    {
        internal mtd806(PDFDocument var0) : base(var0)
        {
            base._mtd793 = new byte[5];
        }

        internal override void mtd172()
        {
            byte[] buffer = base.mtd801(base._mtd790.UserPassword);
            byte[] buffer2 = base.mtd801(base._mtd790.OwnerPassword);
            byte[] buffer3 = base._mtd797.mtd804(buffer2);
            base._mtd792 = base._mtd799.mtd713(buffer, buffer3, 5);
            base._mtd795 = this.mtd802();
            byte[] destinationArray = new byte[0x54];
            Array.Copy(buffer, 0, destinationArray, 0, 0x20);
            Array.Copy(base._mtd792, 0, destinationArray, 0x20, 0x20);
            Array.Copy(BitConverter.GetBytes(base._mtd795), 0, destinationArray, 0x40, 4);
            Array.Copy(base.mtd789, 0, destinationArray, 0x44, 0x10);
            Array.Copy(base._mtd797.mtd804(destinationArray), 0, base._mtd793, 0, 5);
            base._mtd791 = base._mtd799.mtd713(base.mtd800, base._mtd793);
        }

        internal override void mtd710(ref mtd711 var5)
        {
            base.mtd710(ref var5);
            var5.mtd715("/Filter /Standard");
            var5.mtd715("/Length 40");
            var5.mtd715("/V 1");
            var5.mtd715("/R 2");
            var5.mtd710("/O <");
            var5.mtd714(base._mtd792);
            var5.mtd715(">");
            var5.mtd710("/U <");
            var5.mtd714(base._mtd791);
            var5.mtd715(">");
            var5.mtd715(string.Format("/P {0} ", this.mtd802()));
            var5.mtd715(">>");
            var5.mtd715("endobj");
        }

        internal override byte[] mtd713(byte[] var3, int var4)
        {
            return base._mtd799.mtd713(var3, var4, base._mtd794, 10);
        }

        internal override void mtd775(int var1, int var2)
        {
            mtd805 mtd = new mtd805();
            mtd.mtd710(base._mtd793, base._mtd793.Length);
            mtd.mtd710((byte) var1);
            mtd.mtd710((byte) (var1 >> 8));
            mtd.mtd710((byte) (var1 >> 0x10));
            mtd.mtd710((byte) var2);
            mtd.mtd710((byte) (var2 >> 8));
            base._mtd794 = base._mtd797.mtd804(mtd.mtd784, 0, mtd.mtd32);
        }
    }
}

