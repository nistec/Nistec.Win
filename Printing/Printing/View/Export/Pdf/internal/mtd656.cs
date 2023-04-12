namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.IO;

    internal class mtd656
    {
        private mtd716 var0 = new mtd716();
        private mtd722 var1 = new mtd722();
        private static mtd719[] var2 = new mtd719[4];
        private static mtd725[] var3 = new mtd725[0x1d];
        private const int var4 = 11;
        private static mtd725[] var5 = new mtd725[30];
        private static readonly byte[] var6;

        static mtd656()
        {
            var7();
            var8();
            var9();
            var6 = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };
        }

        internal mtd656()
        {
        }

        internal byte[] mtd657(byte[] var10, int var11)
        {
            byte[] destinationArray = new byte[var11];
            Array.Copy(var10, 0, destinationArray, 0, var11);
            if (var10.Length != 0)
            {
                this.var12();
                this.var13();
                mtd726 mtd = new mtd726(destinationArray, 11);
                mtd727 mtd2 = new mtd727();
                while (mtd.mtd728(mtd2))
                {
                    this.var14(mtd2);
                }
                this.var15(0x100);
                this.var1.mtd729();
                int num = (int) this.var0.mtd717(destinationArray);
                this.var1.mtd730(num >> 0x10);
                this.var1.mtd730(num & 0xffff);
                MemoryStream stream = new MemoryStream();
                this.var1.mtd731(stream);
                if (stream.Length > 0L)
                {
                    return stream.ToArray();
                }
            }
            return null;
        }

        internal static short mtd723(int var25, int var11)
        {
            var25 = var25 << (0x10 - var11);
            return (short) ((((var6[var25 & 15] << 12) | (var6[(var25 >> 4) & 15] << 8)) | (var6[(var25 >> 8) & 15] << 4)) | var6[var25 >> 12]);
        }

        private void var12()
        {
            int num = 0x3800;
            num |= 0x80;
            num += 0x1f - (num % 0x1f);
            this.var1.mtd730(num);
        }

        private void var13()
        {
            this.var1.mtd724(1, 1);
            this.var1.mtd724(1, 2);
        }

        private bool var14(mtd727 var24)
        {
            if (var24 == null)
            {
                return false;
            }
            if (var24.mtd734)
            {
                this.var18(var24.mtd735);
            }
            else
            {
                this.var16(var24.mtd736);
                this.var20(var24.mtd737);
            }
            return true;
        }

        private void var15(int var17)
        {
            mtd719 mtd = null;
            for (int i = 0; i < var2.Length; i++)
            {
                if (var2[i].mtd720(var17))
                {
                    mtd = var2[i];
                    break;
                }
            }
            if (mtd == null)
            {
                throw new Exception("Invalid base of the length");
            }
            mtd.mtd721(var17, this.var1);
        }

        private void var16(int var11)
        {
            mtd725 mtd = null;
            for (int i = 0; i < var3.Length; i++)
            {
                if (var3[i].mtd720(var11))
                {
                    mtd = var3[i];
                    break;
                }
            }
            if (mtd == null)
            {
                throw new Exception("Invalid length");
            }
            this.var15(mtd.mtd732);
            mtd.mtd733(var11, this.var1);
        }

        private void var18(byte var19)
        {
            this.var15(var19);
        }

        private void var20(int var21)
        {
            mtd725 mtd = null;
            for (int i = 0; i < var5.Length; i++)
            {
                if (var5[i].mtd720(var21))
                {
                    mtd = var5[i];
                    break;
                }
            }
            if (mtd == null)
            {
                throw new Exception("Invalid offset");
            }
            this.var22(mtd.mtd732);
            mtd.mtd733(var21, this.var1);
        }

        private void var22(int var23)
        {
            int num = mtd723(var23, 5);
            this.var1.mtd724(num, 5);
        }

        private static void var7()
        {
            var3[0] = new mtd725(3, 3, 0x101, 0);
            var3[1] = new mtd725(4, 4, 0x102, 0);
            var3[2] = new mtd725(5, 5, 0x103, 0);
            var3[3] = new mtd725(6, 6, 260, 0);
            var3[4] = new mtd725(7, 7, 0x105, 0);
            var3[5] = new mtd725(8, 8, 0x106, 0);
            var3[6] = new mtd725(9, 9, 0x107, 0);
            var3[7] = new mtd725(10, 10, 0x108, 0);
            var3[8] = new mtd725(11, 12, 0x109, 1);
            var3[9] = new mtd725(13, 14, 0x10a, 1);
            var3[10] = new mtd725(15, 0x10, 0x10b, 1);
            var3[11] = new mtd725(0x11, 0x12, 0x10c, 1);
            var3[12] = new mtd725(0x13, 0x16, 0x10d, 2);
            var3[13] = new mtd725(0x17, 0x1a, 270, 2);
            var3[14] = new mtd725(0x1b, 30, 0x10f, 2);
            var3[15] = new mtd725(0x1f, 0x22, 0x110, 2);
            var3[0x10] = new mtd725(0x23, 0x2a, 0x111, 3);
            var3[0x11] = new mtd725(0x2b, 50, 0x112, 3);
            var3[0x12] = new mtd725(0x33, 0x3a, 0x113, 3);
            var3[0x13] = new mtd725(0x3b, 0x42, 0x114, 3);
            var3[20] = new mtd725(0x43, 0x52, 0x115, 4);
            var3[0x15] = new mtd725(0x53, 0x62, 0x116, 4);
            var3[0x16] = new mtd725(0x63, 0x72, 0x117, 4);
            var3[0x17] = new mtd725(0x73, 130, 280, 4);
            var3[0x18] = new mtd725(0x83, 0xa2, 0x119, 5);
            var3[0x19] = new mtd725(0xa3, 0xc2, 0x11a, 5);
            var3[0x1a] = new mtd725(0xc3, 0xe2, 0x11b, 5);
            var3[0x1b] = new mtd725(0xe3, 0x101, 0x11c, 5);
            var3[0x1c] = new mtd725(0x102, 0x102, 0x11d, 0);
        }

        private static void var8()
        {
            var5[0] = new mtd725(1, 1, 0, 0);
            var5[1] = new mtd725(2, 2, 1, 0);
            var5[2] = new mtd725(3, 3, 2, 0);
            var5[3] = new mtd725(4, 4, 3, 0);
            var5[4] = new mtd725(5, 6, 4, 1);
            var5[5] = new mtd725(7, 8, 5, 1);
            var5[6] = new mtd725(9, 12, 6, 2);
            var5[7] = new mtd725(13, 0x10, 7, 2);
            var5[8] = new mtd725(0x11, 0x18, 8, 3);
            var5[9] = new mtd725(0x19, 0x20, 9, 3);
            var5[10] = new mtd725(0x21, 0x30, 10, 4);
            var5[11] = new mtd725(0x31, 0x40, 11, 4);
            var5[12] = new mtd725(0x41, 0x60, 12, 5);
            var5[13] = new mtd725(0x61, 0x80, 13, 5);
            var5[14] = new mtd725(0x81, 0xc0, 14, 6);
            var5[15] = new mtd725(0xc1, 0x100, 15, 6);
            var5[0x10] = new mtd725(0x101, 0x180, 0x10, 7);
            var5[0x11] = new mtd725(0x181, 0x200, 0x11, 7);
            var5[0x12] = new mtd725(0x201, 0x300, 0x12, 8);
            var5[0x13] = new mtd725(0x301, 0x400, 0x13, 8);
            var5[20] = new mtd725(0x401, 0x600, 20, 9);
            var5[0x15] = new mtd725(0x601, 0x800, 0x15, 9);
            var5[0x16] = new mtd725(0x801, 0xc00, 0x16, 10);
            var5[0x17] = new mtd725(0xc01, 0x1000, 0x17, 10);
            var5[0x18] = new mtd725(0x1001, 0x1800, 0x18, 11);
            var5[0x19] = new mtd725(0x1801, 0x2000, 0x19, 11);
            var5[0x1a] = new mtd725(0x2001, 0x3000, 0x1a, 12);
            var5[0x1b] = new mtd725(0x3001, 0x4000, 0x1b, 12);
            var5[0x1c] = new mtd725(0x4001, 0x6000, 0x1c, 13);
            var5[0x1d] = new mtd725(0x6001, 0x8000, 0x1d, 13);
        }

        private static void var9()
        {
            var2[0] = new mtd719(0, 0x8f, 8, 0x30);
            var2[1] = new mtd719(0x90, 0xff, 9, 400);
            var2[2] = new mtd719(0x100, 0x117, 7, 0);
            var2[3] = new mtd719(280, 0x11f, 8, 0xc0);
        }
    }
}

