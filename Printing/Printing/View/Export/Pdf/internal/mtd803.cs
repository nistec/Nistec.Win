namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd803 : mtd712
    {
        internal mtd803(PDFDocument var0) : base(var0)
        {
        }

        internal override void mtd172()
        {
            byte[] sourceArray = base.mtd801(base._mtd790.UserPassword);
            byte[] buffer2 = base.mtd801(base._mtd790.OwnerPassword);
            byte[] buffer3 = sourceArray;
            byte[] destinationArray = new byte[0x10];
            byte[] buffer5 = buffer2;
            buffer5 = base._mtd797.mtd804(buffer5);
            for (int i = 0; i < 50; i++)
            {
                buffer5 = base._mtd797.mtd804(buffer5);
            }
            Array.Copy(buffer5, destinationArray, 0x10);
            buffer3 = base._mtd799.mtd713(buffer3, destinationArray);
            byte[] buffer6 = new byte[destinationArray.Length];
            for (int j = 1; j < 20; j++)
            {
                for (int n = 0; n < destinationArray.Length; n++)
                {
                    buffer6[n] = (byte) (destinationArray[n] ^ j);
                }
                buffer3 = base._mtd799.mtd713(buffer3, buffer6);
            }
            base._mtd792 = buffer3;
            base._mtd795 = this.mtd802();
            buffer3 = new byte[0x10];
            destinationArray = new byte[0x54];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            Array.Copy(base._mtd792, 0, destinationArray, 0x20, 0x20);
            Array.Copy(BitConverter.GetBytes(base._mtd795), 0, destinationArray, 0x40, 4);
            Array.Copy(base.mtd789, 0, destinationArray, 0x44, 0x10);
            buffer5 = base._mtd797.mtd804(destinationArray);
            for (int k = 0; k < 50; k++)
            {
                buffer5 = base._mtd797.mtd804(buffer5);
            }
            Array.Copy(buffer5, 0, buffer3, 0, 0x10);
            base._mtd793 = buffer3;
            int num5 = base._mtd793.Length + 5;
            if (num5 > 0x10)
            {
                num5 = 0x10;
            }
            base._mtd794 = new byte[num5];
            buffer3 = new byte[0x20];
            destinationArray = new byte[0x30];
            base.mtd800.CopyTo(destinationArray, 0);
            base.mtd789.CopyTo(destinationArray, 0x20);
            buffer5 = base._mtd797.mtd804(destinationArray);
            buffer5 = base._mtd799.mtd713(buffer5, base._mtd793);
            byte[] buffer7 = base._mtd793;
            buffer6 = new byte[buffer7.Length];
            for (int m = 1; m < 20; m++)
            {
                for (int num7 = 0; num7 < buffer7.Length; num7++)
                {
                    buffer6[num7] = (byte) (buffer7[num7] ^ m);
                }
                buffer5 = base._mtd799.mtd713(buffer5, buffer6);
            }
            Array.Copy(buffer5, 0, buffer3, 0, 0x10);
            Array.Copy(base.mtd800, 0, buffer3, 0x10, 0x10);
            base._mtd791 = buffer3;
        }

        internal override void mtd710(ref mtd711 var5)
        {
            base.mtd710(ref var5);
            var5.mtd715("/Filter /Standard");
            var5.mtd715("/Length 128");
            var5.mtd715("/V 2");
            var5.mtd715("/R 3");
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
            return base._mtd799.mtd713(var3, var4, base._mtd794, base._mtd794.Length);
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

        internal override int mtd802()
        {
            int num = base.mtd802();
            if (!base._mtd790.AllowFormFilling)
            {
                num -= 0x100;
            }
            if (!base._mtd790.AllowAccessibility)
            {
                num -= 0x200;
            }
            if (!base._mtd790.AllowDocumentAssembly)
            {
                num -= 0x400;
            }
            if (!base._mtd790.AllowHighQualityPrinting)
            {
                num -= 0x800;
            }
            return num;
        }
    }
}

