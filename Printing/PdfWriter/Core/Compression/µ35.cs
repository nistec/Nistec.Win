namespace MControl.Printing.Pdf.Core.Compression
{
    using System;
    using System.IO;

    internal class A35
    {
        private A64 b0 = new A64();
        private A70 b1 = new A70();
        private static A67[] b2 = new A67[4];
        private static A73[] b3 = new A73[0x1d];
        private const int b4 = 11;
        private static A73[] b5 = new A73[30];
        private static readonly byte[] b6;

        static A35()
        {
            b7();
            b8();
            b9();
            b6 = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };
        }

        internal A35()
        {
        }

        internal byte[] A36(byte[] b10, int b11)
        {
            byte[] destinationArray = new byte[b11];
            Array.Copy(b10, 0, destinationArray, 0, b11);
            if (b10.Length != 0)
            {
                this.b12();
                this.b13();
                A74 A = new A74(destinationArray, 11);
                A75 A2 = new A75();
                while (A.A76(A2))
                {
                    this.b14(A2);
                }
                this.b15(0x100);
                this.b1.A77();
                int num = (int) this.b0.A65(destinationArray);
                this.b1.A78(num >> 0x10);
                this.b1.A78(num & 0xffff);
                MemoryStream stream = new MemoryStream();
                this.b1.A79(stream);
                if (stream.Length > 0L)
                {
                    return stream.ToArray();
                }
            }
            return null;
        }

        internal static short A71(int b25, int b11)
        {
            b25 = b25 << (0x10 - b11);
            return (short) ((((b6[b25 & 15] << 12) | (b6[(b25 >> 4) & 15] << 8)) | (b6[(b25 >> 8) & 15] << 4)) | b6[b25 >> 12]);
        }

        private void b12()
        {
            int num = 0x3800;
            num |= 0x80;
            num += 0x1f - (num % 0x1f);
            this.b1.A78(num);
        }

        private void b13()
        {
            this.b1.A72(1, 1);
            this.b1.A72(1, 2);
        }

        private bool b14(A75 b24)
        {
            if (b24 == null)
            {
                return false;
            }
            if (b24.A82)
            {
                this.b18(b24.A83);
            }
            else
            {
                this.b16(b24.A84);
                this.b20(b24.A85);
            }
            return true;
        }

        private void b15(int b17)
        {
            A67 A = null;
            for (int i = 0; i < b2.Length; i++)
            {
                if (b2[i].A68(b17))
                {
                    A = b2[i];
                    break;
                }
            }
            if (A == null)
            {
                throw new Exception("Invalid base of the length");
            }
            A.A69(b17, this.b1);
        }

        private void b16(int b11)
        {
            A73 A = null;
            for (int i = 0; i < b3.Length; i++)
            {
                if (b3[i].A68(b11))
                {
                    A = b3[i];
                    break;
                }
            }
            if (A == null)
            {
                throw new Exception("Invalid length");
            }
            this.b15(A.A80);
            A.A81(b11, this.b1);
        }

        private void b18(byte b19)
        {
            this.b15(b19);
        }

        private void b20(int b21)
        {
            A73 A = null;
            for (int i = 0; i < b5.Length; i++)
            {
                if (b5[i].A68(b21))
                {
                    A = b5[i];
                    break;
                }
            }
            if (A == null)
            {
                throw new Exception("Invalid offset");
            }
            this.b22(A.A80);
            A.A81(b21, this.b1);
        }

        private void b22(int b23)
        {
            int num = A71(b23, 5);
            this.b1.A72(num, 5);
        }

        private static void b7()
        {
            b3[0] = new A73(3, 3, 0x101, 0);
            b3[1] = new A73(4, 4, 0x102, 0);
            b3[2] = new A73(5, 5, 0x103, 0);
            b3[3] = new A73(6, 6, 260, 0);
            b3[4] = new A73(7, 7, 0x105, 0);
            b3[5] = new A73(8, 8, 0x106, 0);
            b3[6] = new A73(9, 9, 0x107, 0);
            b3[7] = new A73(10, 10, 0x108, 0);
            b3[8] = new A73(11, 12, 0x109, 1);
            b3[9] = new A73(13, 14, 0x10a, 1);
            b3[10] = new A73(15, 0x10, 0x10b, 1);
            b3[11] = new A73(0x11, 0x12, 0x10c, 1);
            b3[12] = new A73(0x13, 0x16, 0x10d, 2);
            b3[13] = new A73(0x17, 0x1a, 270, 2);
            b3[14] = new A73(0x1b, 30, 0x10f, 2);
            b3[15] = new A73(0x1f, 0x22, 0x110, 2);
            b3[0x10] = new A73(0x23, 0x2a, 0x111, 3);
            b3[0x11] = new A73(0x2b, 50, 0x112, 3);
            b3[0x12] = new A73(0x33, 0x3a, 0x113, 3);
            b3[0x13] = new A73(0x3b, 0x42, 0x114, 3);
            b3[20] = new A73(0x43, 0x52, 0x115, 4);
            b3[0x15] = new A73(0x53, 0x62, 0x116, 4);
            b3[0x16] = new A73(0x63, 0x72, 0x117, 4);
            b3[0x17] = new A73(0x73, 130, 280, 4);
            b3[0x18] = new A73(0x83, 0xa2, 0x119, 5);
            b3[0x19] = new A73(0xa3, 0xc2, 0x11a, 5);
            b3[0x1a] = new A73(0xc3, 0xe2, 0x11b, 5);
            b3[0x1b] = new A73(0xe3, 0x101, 0x11c, 5);
            b3[0x1c] = new A73(0x102, 0x102, 0x11d, 0);
        }

        private static void b8()
        {
            b5[0] = new A73(1, 1, 0, 0);
            b5[1] = new A73(2, 2, 1, 0);
            b5[2] = new A73(3, 3, 2, 0);
            b5[3] = new A73(4, 4, 3, 0);
            b5[4] = new A73(5, 6, 4, 1);
            b5[5] = new A73(7, 8, 5, 1);
            b5[6] = new A73(9, 12, 6, 2);
            b5[7] = new A73(13, 0x10, 7, 2);
            b5[8] = new A73(0x11, 0x18, 8, 3);
            b5[9] = new A73(0x19, 0x20, 9, 3);
            b5[10] = new A73(0x21, 0x30, 10, 4);
            b5[11] = new A73(0x31, 0x40, 11, 4);
            b5[12] = new A73(0x41, 0x60, 12, 5);
            b5[13] = new A73(0x61, 0x80, 13, 5);
            b5[14] = new A73(0x81, 0xc0, 14, 6);
            b5[15] = new A73(0xc1, 0x100, 15, 6);
            b5[0x10] = new A73(0x101, 0x180, 0x10, 7);
            b5[0x11] = new A73(0x181, 0x200, 0x11, 7);
            b5[0x12] = new A73(0x201, 0x300, 0x12, 8);
            b5[0x13] = new A73(0x301, 0x400, 0x13, 8);
            b5[20] = new A73(0x401, 0x600, 20, 9);
            b5[0x15] = new A73(0x601, 0x800, 0x15, 9);
            b5[0x16] = new A73(0x801, 0xc00, 0x16, 10);
            b5[0x17] = new A73(0xc01, 0x1000, 0x17, 10);
            b5[0x18] = new A73(0x1001, 0x1800, 0x18, 11);
            b5[0x19] = new A73(0x1801, 0x2000, 0x19, 11);
            b5[0x1a] = new A73(0x2001, 0x3000, 0x1a, 12);
            b5[0x1b] = new A73(0x3001, 0x4000, 0x1b, 12);
            b5[0x1c] = new A73(0x4001, 0x6000, 0x1c, 13);
            b5[0x1d] = new A73(0x6001, 0x8000, 0x1d, 13);
        }

        private static void b9()
        {
            b2[0] = new A67(0, 0x8f, 8, 0x30);
            b2[1] = new A67(0x90, 0xff, 9, 400);
            b2[2] = new A67(0x100, 0x117, 7, 0);
            b2[3] = new A67(280, 0x11f, 8, 0xc0);
        }
    }
}

