namespace MControl.Printing.Pdf.Core.Encrypt
{
    using System;
    using System.Text;

    internal class A233
    {
        private byte[] _b0 = new byte[0x40];
        private uint[] _b1 = new uint[2];
        private uint[] _b2 = new uint[4];
        private static byte b10 = 20;
        private static byte b11 = 4;
        private static byte b12 = 11;
        private static byte b13 = 0x10;
        private static byte b14 = 0x17;
        private static byte b15 = 6;
        private static byte b16 = 10;
        private static byte b17 = 15;
        private static byte b18 = 0x15;
        private static byte[] b19 = new byte[0x40];
        private static byte b3 = 7;
        private static byte b4 = 12;
        private static byte b5 = 0x11;
        private static byte b6 = 0x16;
        private static byte b7 = 5;
        private static byte b8 = 9;
        private static byte b9 = 14;

        static A233()
        {
            b19[0] = 0x80;
        }

        internal A233()
        {
            this.b20();
        }

        internal byte[] A242(byte[] b0)
        {
            return this.A242(b0, 0, b0.Length);
        }

        internal byte[] A242(string b52)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(b52);
            return this.A242(bytes, 0, bytes.Length);
        }

        internal byte[] A242(byte[] b0, int b42, int b1)
        {
            this.b20();
            this.b50(b0, (uint) b42, (uint) b1);
            return this.b51();
        }

        private void b20()
        {
            this._b1[0] = 0;
            this._b1[1] = 0;
            this._b2[0] = 0x67452301;
            this._b2[1] = 0xefcdab89;
            this._b2[2] = 0x98badcfe;
            this._b2[3] = 0x10325476;
        }

        private static uint b21(uint b22, uint b23, uint b24)
        {
            return ((b22 & b23) | (~b22 & b24));
        }

        private static uint b25(uint b22, uint b23, uint b24)
        {
            return ((b22 & b24) | (b23 & ~b24));
        }

        private static uint b26(uint b22, uint b23, uint b24)
        {
            return ((b22 ^ b23) ^ b24);
        }

        private static uint b27(uint b22, uint b23, uint b24)
        {
            return (b23 ^ (b22 | ~b24));
        }

        private static uint b28(uint b22, byte b29)
        {
            return ((b22 << b29) | (b22 >> (0x20 - b29)));
        }

        private static void b30(ref uint b31, uint b32, uint b33, uint b34, uint b22, byte b35, uint b36)
        {
            b31 += (b21(b32, b33, b34) + b22) + b36;
            b31 = b28(b31, b35);
            b31 += b32;
        }

        private static void b37(ref uint b31, uint b32, uint b33, uint b34, uint b22, byte b35, uint b36)
        {
            b31 += (b25(b32, b33, b34) + b22) + b36;
            b31 = b28(b31, b35);
            b31 += b32;
        }

        private static void b38(ref uint b31, uint b32, uint b33, uint b34, uint b22, byte b35, uint b36)
        {
            b31 += (b26(b32, b33, b34) + b22) + b36;
            b31 = b28(b31, b35);
            b31 += b32;
        }

        private static void b39(ref uint b31, uint b32, uint b33, uint b34, uint b22, byte b35, uint b36)
        {
            b31 += (b27(b32, b33, b34) + b22) + b36;
            b31 = b28(b31, b35);
            b31 += b32;
        }

        private void b40(byte[] b41, uint b42)
        {
            uint num = this._b2[0];
            uint num2 = this._b2[1];
            uint num3 = this._b2[2];
            uint num4 = this._b2[3];
            uint[] numArray = new uint[0x10];
            b43(b41, b42, numArray, 0, 0x40);
            b30(ref num, num2, num3, num4, numArray[0], b3, 0xd76aa478);
            b30(ref num4, num, num2, num3, numArray[1], b4, 0xe8c7b756);
            b30(ref num3, num4, num, num2, numArray[2], b5, 0x242070db);
            b30(ref num2, num3, num4, num, numArray[3], b6, 0xc1bdceee);
            b30(ref num, num2, num3, num4, numArray[4], b3, 0xf57c0faf);
            b30(ref num4, num, num2, num3, numArray[5], b4, 0x4787c62a);
            b30(ref num3, num4, num, num2, numArray[6], b5, 0xa8304613);
            b30(ref num2, num3, num4, num, numArray[7], b6, 0xfd469501);
            b30(ref num, num2, num3, num4, numArray[8], b3, 0x698098d8);
            b30(ref num4, num, num2, num3, numArray[9], b4, 0x8b44f7af);
            b30(ref num3, num4, num, num2, numArray[10], b5, 0xffff5bb1);
            b30(ref num2, num3, num4, num, numArray[11], b6, 0x895cd7be);
            b30(ref num, num2, num3, num4, numArray[12], b3, 0x6b901122);
            b30(ref num4, num, num2, num3, numArray[13], b4, 0xfd987193);
            b30(ref num3, num4, num, num2, numArray[14], b5, 0xa679438e);
            b30(ref num2, num3, num4, num, numArray[15], b6, 0x49b40821);
            b37(ref num, num2, num3, num4, numArray[1], b7, 0xf61e2562);
            b37(ref num4, num, num2, num3, numArray[6], b8, 0xc040b340);
            b37(ref num3, num4, num, num2, numArray[11], b9, 0x265e5a51);
            b37(ref num2, num3, num4, num, numArray[0], b10, 0xe9b6c7aa);
            b37(ref num, num2, num3, num4, numArray[5], b7, 0xd62f105d);
            b37(ref num4, num, num2, num3, numArray[10], b8, 0x2441453);
            b37(ref num3, num4, num, num2, numArray[15], b9, 0xd8a1e681);
            b37(ref num2, num3, num4, num, numArray[4], b10, 0xe7d3fbc8);
            b37(ref num, num2, num3, num4, numArray[9], b7, 0x21e1cde6);
            b37(ref num4, num, num2, num3, numArray[14], b8, 0xc33707d6);
            b37(ref num3, num4, num, num2, numArray[3], b9, 0xf4d50d87);
            b37(ref num2, num3, num4, num, numArray[8], b10, 0x455a14ed);
            b37(ref num, num2, num3, num4, numArray[13], b7, 0xa9e3e905);
            b37(ref num4, num, num2, num3, numArray[2], b8, 0xfcefa3f8);
            b37(ref num3, num4, num, num2, numArray[7], b9, 0x676f02d9);
            b37(ref num2, num3, num4, num, numArray[12], b10, 0x8d2a4c8a);
            b38(ref num, num2, num3, num4, numArray[5], b11, 0xfffa3942);
            b38(ref num4, num, num2, num3, numArray[8], b12, 0x8771f681);
            b38(ref num3, num4, num, num2, numArray[11], b13, 0x6d9d6122);
            b38(ref num2, num3, num4, num, numArray[14], b14, 0xfde5380c);
            b38(ref num, num2, num3, num4, numArray[1], b11, 0xa4beea44);
            b38(ref num4, num, num2, num3, numArray[4], b12, 0x4bdecfa9);
            b38(ref num3, num4, num, num2, numArray[7], b13, 0xf6bb4b60);
            b38(ref num2, num3, num4, num, numArray[10], b14, 0xbebfbc70);
            b38(ref num, num2, num3, num4, numArray[13], b11, 0x289b7ec6);
            b38(ref num4, num, num2, num3, numArray[0], b12, 0xeaa127fa);
            b38(ref num3, num4, num, num2, numArray[3], b13, 0xd4ef3085);
            b38(ref num2, num3, num4, num, numArray[6], b14, 0x4881d05);
            b38(ref num, num2, num3, num4, numArray[9], b11, 0xd9d4d039);
            b38(ref num4, num, num2, num3, numArray[12], b12, 0xe6db99e5);
            b38(ref num3, num4, num, num2, numArray[15], b13, 0x1fa27cf8);
            b38(ref num2, num3, num4, num, numArray[2], b14, 0xc4ac5665);
            b39(ref num, num2, num3, num4, numArray[0], b15, 0xf4292244);
            b39(ref num4, num, num2, num3, numArray[7], b16, 0x432aff97);
            b39(ref num3, num4, num, num2, numArray[14], b17, 0xab9423a7);
            b39(ref num2, num3, num4, num, numArray[5], b18, 0xfc93a039);
            b39(ref num, num2, num3, num4, numArray[12], b15, 0x655b59c3);
            b39(ref num4, num, num2, num3, numArray[3], b16, 0x8f0ccc92);
            b39(ref num3, num4, num, num2, numArray[10], b17, 0xffeff47d);
            b39(ref num2, num3, num4, num, numArray[1], b18, 0x85845dd1);
            b39(ref num, num2, num3, num4, numArray[8], b15, 0x6fa87e4f);
            b39(ref num4, num, num2, num3, numArray[15], b16, 0xfe2ce6e0);
            b39(ref num3, num4, num, num2, numArray[6], b17, 0xa3014314);
            b39(ref num2, num3, num4, num, numArray[13], b18, 0x4e0811a1);
            b39(ref num, num2, num3, num4, numArray[4], b15, 0xf7537e82);
            b39(ref num4, num, num2, num3, numArray[11], b16, 0xbd3af235);
            b39(ref num3, num4, num, num2, numArray[2], b17, 0x2ad7d2bb);
            b39(ref num2, num3, num4, num, numArray[9], b18, 0xeb86d391);
            this._b2[0] += num;
            this._b2[1] += num2;
            this._b2[2] += num3;
            this._b2[3] += num4;
        }

        private static void b43(byte[] b44, uint b45, uint[] b46, uint b47, uint b1)
        {
            uint index = b47;
            uint num2 = b45;
            uint num3 = b45 + b1;
            while (num2 < num3)
            {
                b46[index] = (uint) (((b44[num2] | (b44[(int) ((IntPtr) (num2 + 1))] << 8)) | (b44[(int) ((IntPtr) (num2 + 2))] << 0x10)) | (b44[(int) ((IntPtr) (num2 + 3))] << 0x18));
                index++;
                num2 += 4;
            }
        }

        private static void b48(uint[] b44, uint b45, byte[] b46, uint b47, uint b1)
        {
            uint index = b45;
            uint num2 = b47;
            uint num3 = b47 + b1;
            while (num2 < num3)
            {
                b46[num2] = (byte) (b44[index] & 0xff);
                b46[(int) ((IntPtr) (num2 + 1))] = (byte) ((b44[index] >> 8) & 0xff);
                b46[(int) ((IntPtr) (num2 + 2))] = (byte) ((b44[index] >> 0x10) & 0xff);
                b46[(int) ((IntPtr) (num2 + 3))] = (byte) ((b44[index] >> 0x18) & 0xff);
                index++;
                num2 += 4;
            }
        }

        private static void b49(byte[] b44, uint b45, byte[] b46, uint b47, uint b1)
        {
            uint index = b45;
            uint num2 = b47;
            uint num3 = b45 + b1;
            while (index < num3)
            {
                b46[num2] = b44[index];
                index++;
                num2++;
            }
        }

        private void b50(byte[] b0, uint b42, uint b1)
        {
            uint num;
            uint num2 = b1 - b42;
            uint num3 = (this._b1[0] >> 3) & 0x3f;
            this._b1[0] += num2 << 3;
            if (this._b1[0] < (num2 << 3))
            {
                this._b1[1]++;
            }
            this._b1[1] += num2 >> 0x1d;
            uint num4 = 0x40 - num3;
            if (num2 >= num4)
            {
                b49(b0, b42, this._b0, num3, num4);
                this.b40(this._b0, 0);
                for (num = num4; (num + 0x3f) < num2; num += 0x40)
                {
                    this.b40(b0, num);
                }
                num3 = 0;
            }
            else
            {
                num = 0;
            }
            b49(b0, num, this._b0, num3, num2 - num);
        }

        private byte[] b51()
        {
            byte[] buffer = new byte[0x10];
            byte[] buffer2 = new byte[8];
            b48(this._b1, 0, buffer2, 0, 8);
            uint num = (this._b1[0] >> 3) & 0x3f;
            uint num2 = (num < 0x38) ? (0x38 - num) : (120 - num);
            this.b50(b19, 0, num2);
            this.b50(buffer2, 0, 8);
            b48(this._b2, 0, buffer, 0, 0x10);
            return buffer;
        }
    }
}

