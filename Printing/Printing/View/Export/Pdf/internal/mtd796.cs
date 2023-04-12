namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Text;

    internal class mtd796
    {
        private byte[] _var0 = new byte[0x40];
        private uint[] _var1 = new uint[2];
        private uint[] _var2 = new uint[4];
        private static byte var10 = 20;
        private static byte var11 = 4;
        private static byte var12 = 11;
        private static byte var13 = 0x10;
        private static byte var14 = 0x17;
        private static byte var15 = 6;
        private static byte var16 = 10;
        private static byte var17 = 15;
        private static byte var18 = 0x15;
        private static byte[] var19 = new byte[0x40];
        private static byte var3 = 7;
        private static byte var4 = 12;
        private static byte var5 = 0x11;
        private static byte var6 = 0x16;
        private static byte var7 = 5;
        private static byte var8 = 9;
        private static byte var9 = 14;

        static mtd796()
        {
            var19[0] = 0x80;
        }

        internal mtd796()
        {
            this.var20();
        }

        internal byte[] mtd804(string var52)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(var52);
            return this.mtd804(bytes, 0, bytes.Length);
        }

        internal byte[] mtd804(byte[] var0)
        {
            return this.mtd804(var0, 0, var0.Length);
        }

        internal byte[] mtd804(byte[] var0, int var42, int var1)
        {
            this.var20();
            this.var50(var0, (uint) var42, (uint) var1);
            return this.var51();
        }

        private void var20()
        {
            this._var1[0] = 0;
            this._var1[1] = 0;
            this._var2[0] = 0x67452301;
            this._var2[1] = 0xefcdab89;
            this._var2[2] = 0x98badcfe;
            this._var2[3] = 0x10325476;
        }

        private static uint var21(uint var22, uint var23, uint var24)
        {
            return ((var22 & var23) | (~var22 & var24));
        }

        private static uint var25(uint var22, uint var23, uint var24)
        {
            return ((var22 & var24) | (var23 & ~var24));
        }

        private static uint var26(uint var22, uint var23, uint var24)
        {
            return ((var22 ^ var23) ^ var24);
        }

        private static uint var27(uint var22, uint var23, uint var24)
        {
            return (var23 ^ (var22 | ~var24));
        }

        private static uint var28(uint var22, byte var29)
        {
            return ((var22 << var29) | (var22 >> (0x20 - var29)));
        }

        private static void var30(ref uint var31, uint var32, uint var33, uint var34, uint var22, byte var35, uint var36)
        {
            var31 += (var21(var32, var33, var34) + var22) + var36;
            var31 = var28(var31, var35);
            var31 += var32;
        }

        private static void var37(ref uint var31, uint var32, uint var33, uint var34, uint var22, byte var35, uint var36)
        {
            var31 += (var25(var32, var33, var34) + var22) + var36;
            var31 = var28(var31, var35);
            var31 += var32;
        }

        private static void var38(ref uint var31, uint var32, uint var33, uint var34, uint var22, byte var35, uint var36)
        {
            var31 += (var26(var32, var33, var34) + var22) + var36;
            var31 = var28(var31, var35);
            var31 += var32;
        }

        private static void var39(ref uint var31, uint var32, uint var33, uint var34, uint var22, byte var35, uint var36)
        {
            var31 += (var27(var32, var33, var34) + var22) + var36;
            var31 = var28(var31, var35);
            var31 += var32;
        }

        private void var40(byte[] var41, uint var42)
        {
            uint num = this._var2[0];
            uint num2 = this._var2[1];
            uint num3 = this._var2[2];
            uint num4 = this._var2[3];
            uint[] numArray = new uint[0x10];
            var43(var41, var42, numArray, 0, 0x40);
            var30(ref num, num2, num3, num4, numArray[0], var3, 0xd76aa478);
            var30(ref num4, num, num2, num3, numArray[1], var4, 0xe8c7b756);
            var30(ref num3, num4, num, num2, numArray[2], var5, 0x242070db);
            var30(ref num2, num3, num4, num, numArray[3], var6, 0xc1bdceee);
            var30(ref num, num2, num3, num4, numArray[4], var3, 0xf57c0faf);
            var30(ref num4, num, num2, num3, numArray[5], var4, 0x4787c62a);
            var30(ref num3, num4, num, num2, numArray[6], var5, 0xa8304613);
            var30(ref num2, num3, num4, num, numArray[7], var6, 0xfd469501);
            var30(ref num, num2, num3, num4, numArray[8], var3, 0x698098d8);
            var30(ref num4, num, num2, num3, numArray[9], var4, 0x8b44f7af);
            var30(ref num3, num4, num, num2, numArray[10], var5, 0xffff5bb1);
            var30(ref num2, num3, num4, num, numArray[11], var6, 0x895cd7be);
            var30(ref num, num2, num3, num4, numArray[12], var3, 0x6b901122);
            var30(ref num4, num, num2, num3, numArray[13], var4, 0xfd987193);
            var30(ref num3, num4, num, num2, numArray[14], var5, 0xa679438e);
            var30(ref num2, num3, num4, num, numArray[15], var6, 0x49b40821);
            var37(ref num, num2, num3, num4, numArray[1], var7, 0xf61e2562);
            var37(ref num4, num, num2, num3, numArray[6], var8, 0xc040b340);
            var37(ref num3, num4, num, num2, numArray[11], var9, 0x265e5a51);
            var37(ref num2, num3, num4, num, numArray[0], var10, 0xe9b6c7aa);
            var37(ref num, num2, num3, num4, numArray[5], var7, 0xd62f105d);
            var37(ref num4, num, num2, num3, numArray[10], var8, 0x2441453);
            var37(ref num3, num4, num, num2, numArray[15], var9, 0xd8a1e681);
            var37(ref num2, num3, num4, num, numArray[4], var10, 0xe7d3fbc8);
            var37(ref num, num2, num3, num4, numArray[9], var7, 0x21e1cde6);
            var37(ref num4, num, num2, num3, numArray[14], var8, 0xc33707d6);
            var37(ref num3, num4, num, num2, numArray[3], var9, 0xf4d50d87);
            var37(ref num2, num3, num4, num, numArray[8], var10, 0x455a14ed);
            var37(ref num, num2, num3, num4, numArray[13], var7, 0xa9e3e905);
            var37(ref num4, num, num2, num3, numArray[2], var8, 0xfcefa3f8);
            var37(ref num3, num4, num, num2, numArray[7], var9, 0x676f02d9);
            var37(ref num2, num3, num4, num, numArray[12], var10, 0x8d2a4c8a);
            var38(ref num, num2, num3, num4, numArray[5], var11, 0xfffa3942);
            var38(ref num4, num, num2, num3, numArray[8], var12, 0x8771f681);
            var38(ref num3, num4, num, num2, numArray[11], var13, 0x6d9d6122);
            var38(ref num2, num3, num4, num, numArray[14], var14, 0xfde5380c);
            var38(ref num, num2, num3, num4, numArray[1], var11, 0xa4beea44);
            var38(ref num4, num, num2, num3, numArray[4], var12, 0x4bdecfa9);
            var38(ref num3, num4, num, num2, numArray[7], var13, 0xf6bb4b60);
            var38(ref num2, num3, num4, num, numArray[10], var14, 0xbebfbc70);
            var38(ref num, num2, num3, num4, numArray[13], var11, 0x289b7ec6);
            var38(ref num4, num, num2, num3, numArray[0], var12, 0xeaa127fa);
            var38(ref num3, num4, num, num2, numArray[3], var13, 0xd4ef3085);
            var38(ref num2, num3, num4, num, numArray[6], var14, 0x4881d05);
            var38(ref num, num2, num3, num4, numArray[9], var11, 0xd9d4d039);
            var38(ref num4, num, num2, num3, numArray[12], var12, 0xe6db99e5);
            var38(ref num3, num4, num, num2, numArray[15], var13, 0x1fa27cf8);
            var38(ref num2, num3, num4, num, numArray[2], var14, 0xc4ac5665);
            var39(ref num, num2, num3, num4, numArray[0], var15, 0xf4292244);
            var39(ref num4, num, num2, num3, numArray[7], var16, 0x432aff97);
            var39(ref num3, num4, num, num2, numArray[14], var17, 0xab9423a7);
            var39(ref num2, num3, num4, num, numArray[5], var18, 0xfc93a039);
            var39(ref num, num2, num3, num4, numArray[12], var15, 0x655b59c3);
            var39(ref num4, num, num2, num3, numArray[3], var16, 0x8f0ccc92);
            var39(ref num3, num4, num, num2, numArray[10], var17, 0xffeff47d);
            var39(ref num2, num3, num4, num, numArray[1], var18, 0x85845dd1);
            var39(ref num, num2, num3, num4, numArray[8], var15, 0x6fa87e4f);
            var39(ref num4, num, num2, num3, numArray[15], var16, 0xfe2ce6e0);
            var39(ref num3, num4, num, num2, numArray[6], var17, 0xa3014314);
            var39(ref num2, num3, num4, num, numArray[13], var18, 0x4e0811a1);
            var39(ref num, num2, num3, num4, numArray[4], var15, 0xf7537e82);
            var39(ref num4, num, num2, num3, numArray[11], var16, 0xbd3af235);
            var39(ref num3, num4, num, num2, numArray[2], var17, 0x2ad7d2bb);
            var39(ref num2, num3, num4, num, numArray[9], var18, 0xeb86d391);
            this._var2[0] += num;
            this._var2[1] += num2;
            this._var2[2] += num3;
            this._var2[3] += num4;
        }

        private static void var43(byte[] var44, uint var45, uint[] var46, uint var47, uint var1)
        {
            uint index = var47;
            uint num2 = var45;
            uint num3 = var45 + var1;
            while (num2 < num3)
            {
                var46[index] = (uint) (((var44[num2] | (var44[(int) ((IntPtr) (num2 + 1))] << 8)) | (var44[(int) ((IntPtr) (num2 + 2))] << 0x10)) | (var44[(int) ((IntPtr) (num2 + 3))] << 0x18));
                index++;
                num2 += 4;
            }
        }

        private static void var48(uint[] var44, uint var45, byte[] var46, uint var47, uint var1)
        {
            uint index = var45;
            uint num2 = var47;
            uint num3 = var47 + var1;
            while (num2 < num3)
            {
                var46[num2] = (byte) (var44[index] & 0xff);
                var46[(int) ((IntPtr) (num2 + 1))] = (byte) ((var44[index] >> 8) & 0xff);
                var46[(int) ((IntPtr) (num2 + 2))] = (byte) ((var44[index] >> 0x10) & 0xff);
                var46[(int) ((IntPtr) (num2 + 3))] = (byte) ((var44[index] >> 0x18) & 0xff);
                index++;
                num2 += 4;
            }
        }

        private static void var49(byte[] var44, uint var45, byte[] var46, uint var47, uint var1)
        {
            uint index = var45;
            uint num2 = var47;
            uint num3 = var45 + var1;
            while (index < num3)
            {
                var46[num2] = var44[index];
                index++;
                num2++;
            }
        }

        private void var50(byte[] var0, uint var42, uint var1)
        {
            uint num;
            uint num2 = var1 - var42;
            uint num3 = (this._var1[0] >> 3) & 0x3f;
            this._var1[0] += num2 << 3;
            if (this._var1[0] < (num2 << 3))
            {
                this._var1[1]++;
            }
            this._var1[1] += num2 >> 0x1d;
            uint num4 = 0x40 - num3;
            if (num2 >= num4)
            {
                var49(var0, var42, this._var0, num3, num4);
                this.var40(this._var0, 0);
                for (num = num4; (num + 0x3f) < num2; num += 0x40)
                {
                    this.var40(var0, num);
                }
                num3 = 0;
            }
            else
            {
                num = 0;
            }
            var49(var0, num, this._var0, num3, num2 - num);
        }

        private byte[] var51()
        {
            byte[] buffer = new byte[0x10];
            byte[] buffer2 = new byte[8];
            var48(this._var1, 0, buffer2, 0, 8);
            uint num = (this._var1[0] >> 3) & 0x3f;
            uint num2 = (num < 0x38) ? (0x38 - num) : (120 - num);
            this.var50(var19, 0, num2);
            this.var50(buffer2, 0, 8);
            var48(this._var2, 0, buffer, 0, 0x10);
            return buffer;
        }
    }
}

