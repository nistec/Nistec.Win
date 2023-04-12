namespace MControl.Printing.Pdf.Core
{
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A554
    {
        private A112 _b21;
        private const int b0 = 0;
        private const int b1 = 1;
        private int[][] b10;
        private int[] b11;
        private static int[][] b12;
        private static int[][] b13;
        private static byte[] b14;
        private static byte[] b15;
        private int b16;
        private int b17;
        private int b18;
        private int b19;
        private const int b2 = 2;
        private byte[] b20;
        private byte[] b22;
        private int b23;
        private int b24;
        private const int b3 = 1;
        private const int b4 = -1;
        private const int b5 = -2;
        private const int b6 = -3;
        private const int b7 = -4;
        private int[] b8;
        private int[] b9;

        static A554()
        {
            int[][] numArray = new int[0x6d][];
            int[] numArray2 = new int[3];
            numArray2[0] = 8;
            numArray2[1] = 0x35;
            numArray[0] = numArray2;
            numArray[1] = new int[] { 6, 7, 1 };
            numArray[2] = new int[] { 4, 7, 2 };
            numArray[3] = new int[] { 4, 8, 3 };
            numArray[4] = new int[] { 4, 11, 4 };
            numArray[5] = new int[] { 4, 12, 5 };
            numArray[6] = new int[] { 4, 14, 6 };
            numArray[7] = new int[] { 4, 15, 7 };
            numArray[8] = new int[] { 5, 0x13, 8 };
            numArray[9] = new int[] { 5, 20, 9 };
            numArray[10] = new int[] { 5, 7, 10 };
            numArray[11] = new int[] { 5, 8, 11 };
            numArray[12] = new int[] { 6, 8, 12 };
            numArray[13] = new int[] { 6, 3, 13 };
            numArray[14] = new int[] { 6, 0x34, 14 };
            numArray[15] = new int[] { 6, 0x35, 15 };
            numArray[0x10] = new int[] { 6, 0x2a, 0x10 };
            numArray[0x11] = new int[] { 6, 0x2b, 0x11 };
            numArray[0x12] = new int[] { 7, 0x27, 0x12 };
            numArray[0x13] = new int[] { 7, 12, 0x13 };
            numArray[20] = new int[] { 7, 8, 20 };
            numArray[0x15] = new int[] { 7, 0x17, 0x15 };
            numArray[0x16] = new int[] { 7, 3, 0x16 };
            numArray[0x17] = new int[] { 7, 4, 0x17 };
            numArray[0x18] = new int[] { 7, 40, 0x18 };
            numArray[0x19] = new int[] { 7, 0x2b, 0x19 };
            numArray[0x1a] = new int[] { 7, 0x13, 0x1a };
            numArray[0x1b] = new int[] { 7, 0x24, 0x1b };
            numArray[0x1c] = new int[] { 7, 0x18, 0x1c };
            numArray[0x1d] = new int[] { 8, 2, 0x1d };
            numArray[30] = new int[] { 8, 3, 30 };
            numArray[0x1f] = new int[] { 8, 0x1a, 0x1f };
            numArray[0x20] = new int[] { 8, 0x1b, 0x20 };
            numArray[0x21] = new int[] { 8, 0x12, 0x21 };
            numArray[0x22] = new int[] { 8, 0x13, 0x22 };
            numArray[0x23] = new int[] { 8, 20, 0x23 };
            numArray[0x24] = new int[] { 8, 0x15, 0x24 };
            numArray[0x25] = new int[] { 8, 0x16, 0x25 };
            numArray[0x26] = new int[] { 8, 0x17, 0x26 };
            numArray[0x27] = new int[] { 8, 40, 0x27 };
            numArray[40] = new int[] { 8, 0x29, 40 };
            numArray[0x29] = new int[] { 8, 0x2a, 0x29 };
            numArray[0x2a] = new int[] { 8, 0x2b, 0x2a };
            numArray[0x2b] = new int[] { 8, 0x2c, 0x2b };
            numArray[0x2c] = new int[] { 8, 0x2d, 0x2c };
            numArray[0x2d] = new int[] { 8, 4, 0x2d };
            numArray[0x2e] = new int[] { 8, 5, 0x2e };
            numArray[0x2f] = new int[] { 8, 10, 0x2f };
            numArray[0x30] = new int[] { 8, 11, 0x30 };
            numArray[0x31] = new int[] { 8, 0x52, 0x31 };
            numArray[50] = new int[] { 8, 0x53, 50 };
            numArray[0x33] = new int[] { 8, 0x54, 0x33 };
            numArray[0x34] = new int[] { 8, 0x55, 0x34 };
            numArray[0x35] = new int[] { 8, 0x24, 0x35 };
            numArray[0x36] = new int[] { 8, 0x25, 0x36 };
            numArray[0x37] = new int[] { 8, 0x58, 0x37 };
            numArray[0x38] = new int[] { 8, 0x59, 0x38 };
            numArray[0x39] = new int[] { 8, 90, 0x39 };
            numArray[0x3a] = new int[] { 8, 0x5b, 0x3a };
            numArray[0x3b] = new int[] { 8, 0x4a, 0x3b };
            numArray[60] = new int[] { 8, 0x4b, 60 };
            numArray[0x3d] = new int[] { 8, 50, 0x3d };
            numArray[0x3e] = new int[] { 8, 0x33, 0x3e };
            numArray[0x3f] = new int[] { 8, 0x34, 0x3f };
            numArray[0x40] = new int[] { 5, 0x1b, 0x40 };
            numArray[0x41] = new int[] { 5, 0x12, 0x80 };
            numArray[0x42] = new int[] { 6, 0x17, 0xc0 };
            numArray[0x43] = new int[] { 7, 0x37, 0x100 };
            numArray[0x44] = new int[] { 8, 0x36, 320 };
            numArray[0x45] = new int[] { 8, 0x37, 0x180 };
            numArray[70] = new int[] { 8, 100, 0x1c0 };
            numArray[0x47] = new int[] { 8, 0x65, 0x200 };
            numArray[0x48] = new int[] { 8, 0x68, 0x240 };
            numArray[0x49] = new int[] { 8, 0x67, 640 };
            numArray[0x4a] = new int[] { 9, 0xcc, 0x2c0 };
            numArray[0x4b] = new int[] { 9, 0xcd, 0x300 };
            numArray[0x4c] = new int[] { 9, 210, 0x340 };
            numArray[0x4d] = new int[] { 9, 0xd3, 0x380 };
            numArray[0x4e] = new int[] { 9, 0xd4, 960 };
            numArray[0x4f] = new int[] { 9, 0xd5, 0x400 };
            numArray[80] = new int[] { 9, 0xd6, 0x440 };
            numArray[0x51] = new int[] { 9, 0xd7, 0x480 };
            numArray[0x52] = new int[] { 9, 0xd8, 0x4c0 };
            numArray[0x53] = new int[] { 9, 0xd9, 0x500 };
            numArray[0x54] = new int[] { 9, 0xda, 0x540 };
            numArray[0x55] = new int[] { 9, 0xdb, 0x580 };
            numArray[0x56] = new int[] { 9, 0x98, 0x5c0 };
            numArray[0x57] = new int[] { 9, 0x99, 0x600 };
            numArray[0x58] = new int[] { 9, 0x9a, 0x640 };
            numArray[0x59] = new int[] { 6, 0x18, 0x680 };
            numArray[90] = new int[] { 9, 0x9b, 0x6c0 };
            numArray[0x5b] = new int[] { 11, 8, 0x700 };
            numArray[0x5c] = new int[] { 11, 12, 0x740 };
            numArray[0x5d] = new int[] { 11, 13, 0x780 };
            numArray[0x5e] = new int[] { 12, 0x12, 0x7c0 };
            numArray[0x5f] = new int[] { 12, 0x13, 0x800 };
            numArray[0x60] = new int[] { 12, 20, 0x840 };
            numArray[0x61] = new int[] { 12, 0x15, 0x880 };
            numArray[0x62] = new int[] { 12, 0x16, 0x8c0 };
            numArray[0x63] = new int[] { 12, 0x17, 0x900 };
            numArray[100] = new int[] { 12, 0x1c, 0x940 };
            numArray[0x65] = new int[] { 12, 0x1d, 0x980 };
            numArray[0x66] = new int[] { 12, 30, 0x9c0 };
            numArray[0x67] = new int[] { 12, 0x1f, 0xa00 };
            numArray[0x68] = new int[] { 12, 1, -1 };
            numArray[0x69] = new int[] { 9, 1, -2 };
            numArray[0x6a] = new int[] { 10, 1, -2 };
            numArray[0x6b] = new int[] { 11, 1, -2 };
            int[] numArray3 = new int[3];
            numArray3[0] = 12;
            numArray3[2] = -2;
            numArray[0x6c] = numArray3;
            b12 = numArray;
            int[][] numArray4 = new int[0x6d][];
            int[] numArray5 = new int[3];
            numArray5[0] = 10;
            numArray5[1] = 0x37;
            numArray4[0] = numArray5;
            numArray4[1] = new int[] { 3, 2, 1 };
            numArray4[2] = new int[] { 2, 3, 2 };
            numArray4[3] = new int[] { 2, 2, 3 };
            numArray4[4] = new int[] { 3, 3, 4 };
            numArray4[5] = new int[] { 4, 3, 5 };
            numArray4[6] = new int[] { 4, 2, 6 };
            numArray4[7] = new int[] { 5, 3, 7 };
            numArray4[8] = new int[] { 6, 5, 8 };
            numArray4[9] = new int[] { 6, 4, 9 };
            numArray4[10] = new int[] { 7, 4, 10 };
            numArray4[11] = new int[] { 7, 5, 11 };
            numArray4[12] = new int[] { 7, 7, 12 };
            numArray4[13] = new int[] { 8, 4, 13 };
            numArray4[14] = new int[] { 8, 7, 14 };
            numArray4[15] = new int[] { 9, 0x18, 15 };
            numArray4[0x10] = new int[] { 10, 0x17, 0x10 };
            numArray4[0x11] = new int[] { 10, 0x18, 0x11 };
            numArray4[0x12] = new int[] { 10, 8, 0x12 };
            numArray4[0x13] = new int[] { 11, 0x67, 0x13 };
            numArray4[20] = new int[] { 11, 0x68, 20 };
            numArray4[0x15] = new int[] { 11, 0x6c, 0x15 };
            numArray4[0x16] = new int[] { 11, 0x37, 0x16 };
            numArray4[0x17] = new int[] { 11, 40, 0x17 };
            numArray4[0x18] = new int[] { 11, 0x17, 0x18 };
            numArray4[0x19] = new int[] { 11, 0x18, 0x19 };
            numArray4[0x1a] = new int[] { 12, 0xca, 0x1a };
            numArray4[0x1b] = new int[] { 12, 0xcb, 0x1b };
            numArray4[0x1c] = new int[] { 12, 0xcc, 0x1c };
            numArray4[0x1d] = new int[] { 12, 0xcd, 0x1d };
            numArray4[30] = new int[] { 12, 0x68, 30 };
            numArray4[0x1f] = new int[] { 12, 0x69, 0x1f };
            numArray4[0x20] = new int[] { 12, 0x6a, 0x20 };
            numArray4[0x21] = new int[] { 12, 0x6b, 0x21 };
            numArray4[0x22] = new int[] { 12, 210, 0x22 };
            numArray4[0x23] = new int[] { 12, 0xd3, 0x23 };
            numArray4[0x24] = new int[] { 12, 0xd4, 0x24 };
            numArray4[0x25] = new int[] { 12, 0xd5, 0x25 };
            numArray4[0x26] = new int[] { 12, 0xd6, 0x26 };
            numArray4[0x27] = new int[] { 12, 0xd7, 0x27 };
            numArray4[40] = new int[] { 12, 0x6c, 40 };
            numArray4[0x29] = new int[] { 12, 0x6d, 0x29 };
            numArray4[0x2a] = new int[] { 12, 0xda, 0x2a };
            numArray4[0x2b] = new int[] { 12, 0xdb, 0x2b };
            numArray4[0x2c] = new int[] { 12, 0x54, 0x2c };
            numArray4[0x2d] = new int[] { 12, 0x55, 0x2d };
            numArray4[0x2e] = new int[] { 12, 0x56, 0x2e };
            numArray4[0x2f] = new int[] { 12, 0x57, 0x2f };
            numArray4[0x30] = new int[] { 12, 100, 0x30 };
            numArray4[0x31] = new int[] { 12, 0x65, 0x31 };
            numArray4[50] = new int[] { 12, 0x52, 50 };
            numArray4[0x33] = new int[] { 12, 0x53, 0x33 };
            numArray4[0x34] = new int[] { 12, 0x24, 0x34 };
            numArray4[0x35] = new int[] { 12, 0x37, 0x35 };
            numArray4[0x36] = new int[] { 12, 0x38, 0x36 };
            numArray4[0x37] = new int[] { 12, 0x27, 0x37 };
            numArray4[0x38] = new int[] { 12, 40, 0x38 };
            numArray4[0x39] = new int[] { 12, 0x58, 0x39 };
            numArray4[0x3a] = new int[] { 12, 0x59, 0x3a };
            numArray4[0x3b] = new int[] { 12, 0x2b, 0x3b };
            numArray4[60] = new int[] { 12, 0x2c, 60 };
            numArray4[0x3d] = new int[] { 12, 90, 0x3d };
            numArray4[0x3e] = new int[] { 12, 0x66, 0x3e };
            numArray4[0x3f] = new int[] { 12, 0x67, 0x3f };
            numArray4[0x40] = new int[] { 10, 15, 0x40 };
            numArray4[0x41] = new int[] { 12, 200, 0x80 };
            numArray4[0x42] = new int[] { 12, 0xc9, 0xc0 };
            numArray4[0x43] = new int[] { 12, 0x5b, 0x100 };
            numArray4[0x44] = new int[] { 12, 0x33, 320 };
            numArray4[0x45] = new int[] { 12, 0x34, 0x180 };
            numArray4[70] = new int[] { 12, 0x35, 0x1c0 };
            numArray4[0x47] = new int[] { 13, 0x6c, 0x200 };
            numArray4[0x48] = new int[] { 13, 0x6d, 0x240 };
            numArray4[0x49] = new int[] { 13, 0x4a, 640 };
            numArray4[0x4a] = new int[] { 13, 0x4b, 0x2c0 };
            numArray4[0x4b] = new int[] { 13, 0x4c, 0x300 };
            numArray4[0x4c] = new int[] { 13, 0x4d, 0x340 };
            numArray4[0x4d] = new int[] { 13, 0x72, 0x380 };
            numArray4[0x4e] = new int[] { 13, 0x73, 960 };
            numArray4[0x4f] = new int[] { 13, 0x74, 0x400 };
            numArray4[80] = new int[] { 13, 0x75, 0x440 };
            numArray4[0x51] = new int[] { 13, 0x76, 0x480 };
            numArray4[0x52] = new int[] { 13, 0x77, 0x4c0 };
            numArray4[0x53] = new int[] { 13, 0x52, 0x500 };
            numArray4[0x54] = new int[] { 13, 0x53, 0x540 };
            numArray4[0x55] = new int[] { 13, 0x54, 0x580 };
            numArray4[0x56] = new int[] { 13, 0x55, 0x5c0 };
            numArray4[0x57] = new int[] { 13, 90, 0x600 };
            numArray4[0x58] = new int[] { 13, 0x5b, 0x640 };
            numArray4[0x59] = new int[] { 13, 100, 0x680 };
            numArray4[90] = new int[] { 13, 0x65, 0x6c0 };
            numArray4[0x5b] = new int[] { 11, 8, 0x700 };
            numArray4[0x5c] = new int[] { 11, 12, 0x740 };
            numArray4[0x5d] = new int[] { 11, 13, 0x780 };
            numArray4[0x5e] = new int[] { 12, 0x12, 0x7c0 };
            numArray4[0x5f] = new int[] { 12, 0x13, 0x800 };
            numArray4[0x60] = new int[] { 12, 20, 0x840 };
            numArray4[0x61] = new int[] { 12, 0x15, 0x880 };
            numArray4[0x62] = new int[] { 12, 0x16, 0x8c0 };
            numArray4[0x63] = new int[] { 12, 0x17, 0x900 };
            numArray4[100] = new int[] { 12, 0x1c, 0x940 };
            numArray4[0x65] = new int[] { 12, 0x1d, 0x980 };
            numArray4[0x66] = new int[] { 12, 30, 0x9c0 };
            numArray4[0x67] = new int[] { 12, 0x1f, 0xa00 };
            numArray4[0x68] = new int[] { 12, 1, -1 };
            numArray4[0x69] = new int[] { 9, 1, -2 };
            numArray4[0x6a] = new int[] { 10, 1, -2 };
            numArray4[0x6b] = new int[] { 11, 1, -2 };
            int[] numArray6 = new int[3];
            numArray6[0] = 12;
            numArray6[2] = -2;
            numArray4[0x6c] = numArray6;
            b13 = numArray4;
            b14 = new byte[] { 
                8, 7, 6, 6, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 
                3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
            b15 = new byte[] { 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 
                4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 7, 8
             };
        }

        internal A554(int b25)
        {
            int[] numArray = new int[3];
            numArray[0] = 3;
            numArray[1] = 1;
            this.b8 = numArray;
            int[] numArray2 = new int[3];
            numArray2[0] = 4;
            numArray2[1] = 1;
            this.b9 = numArray2;
            int[][] numArray3 = new int[7][];
            int[] numArray4 = new int[3];
            numArray4[0] = 7;
            numArray4[1] = 3;
            numArray3[0] = numArray4;
            int[] numArray5 = new int[3];
            numArray5[0] = 6;
            numArray5[1] = 3;
            numArray3[1] = numArray5;
            int[] numArray6 = new int[3];
            numArray6[0] = 3;
            numArray6[1] = 3;
            numArray3[2] = numArray6;
            int[] numArray7 = new int[3];
            numArray7[0] = 1;
            numArray7[1] = 1;
            numArray3[3] = numArray7;
            int[] numArray8 = new int[3];
            numArray8[0] = 3;
            numArray8[1] = 2;
            numArray3[4] = numArray8;
            int[] numArray9 = new int[3];
            numArray9[0] = 6;
            numArray9[1] = 2;
            numArray3[5] = numArray9;
            int[] numArray10 = new int[3];
            numArray10[0] = 7;
            numArray10[1] = 2;
            numArray3[6] = numArray10;
            this.b10 = numArray3;
            this.b11 = new int[] { 0, 1, 3, 7, 15, 0x1f, 0x3f, 0x7f, 0xff };
            this.b18 = 8;
            this._b21 = new A112();
            this.b17 = b25;
            this.b16 = (this.b17 + 7) / 8;
            this.b20 = new byte[this.b16];
        }

        internal static A112 A123(byte[] b19, int b25, int b30)
        {
            A554 A = new A554(b25);
            A.b26(b19, 0, A.b16 * b30);
            A.b31();
            return A._b21;
        }

        private void b26(byte[] b19, int b30)
        {
            this.b26(b19, 0, this.b16 * b30);
        }

        private void b26(byte[] b19, int b27, int b28)
        {
            this.b22 = b19;
            this.b23 = b27;
            this.b24 = b28;
            while (this.b24 > 0)
            {
                this.b29();
                Array.Copy(this.b22, this.b23, this.b20, 0, this.b16);
                this.b23 += this.b16;
                this.b24 -= this.b16;
            }
        }

        private void b29()
        {
            int num = 0;
            int num2 = (this.b40(this.b22, this.b23, 0) != 0) ? 0 : b41(this.b22, this.b23, 0, this.b17, 0);
            int num3 = (this.b40(this.b20, 0, 0) != 0) ? 0 : b41(this.b20, 0, 0, this.b17, 0);
            while (true)
            {
                int num5 = b42(this.b20, 0, num3, this.b17, this.b40(this.b20, 0, num3));
                if (num5 >= num2)
                {
                    int num6 = num3 - num2;
                    if ((-3 > num6) || (num6 > 3))
                    {
                        int num4 = b42(this.b22, this.b23, num2, this.b17, this.b40(this.b22, this.b23, num2));
                        this.b32(this.b8);
                        if (((num + num2) == 0) || (this.b40(this.b22, this.b23, num) == 0))
                        {
                            this.b35(num2 - num, b12);
                            this.b35(num4 - num2, b13);
                        }
                        else
                        {
                            this.b35(num2 - num, b13);
                            this.b35(num4 - num2, b12);
                        }
                        num = num4;
                    }
                    else
                    {
                        this.b32(this.b10[num6 + 3]);
                        num = num2;
                    }
                }
                else
                {
                    this.b32(this.b9);
                    num = num5;
                }
                if (num >= this.b17)
                {
                    return;
                }
                num2 = b41(this.b22, this.b23, num, this.b17, this.b40(this.b22, this.b23, num));
                num3 = b41(this.b20, 0, num, this.b17, this.b40(this.b22, this.b23, num) ^ 1);
                num3 = b41(this.b20, 0, num3, this.b17, this.b40(this.b22, this.b23, num));
            }
        }

        private void b31()
        {
            this.b34(1, 12);
            this.b34(1, 12);
            if (this.b18 != 8)
            {
                this._b21.A54((byte) this.b19);
                this.b19 = 0;
                this.b18 = 8;
            }
        }

        private void b32(int[] b33)
        {
            this.b34(b33[1], b33[0]);
        }

        private void b34(int b38, int b39)
        {
            while (b39 > this.b18)
            {
                this.b19 |= b38 >> (b39 - this.b18);
                b39 -= this.b18;
                this._b21.A54((byte) this.b19);
                this.b19 = 0;
                this.b18 = 8;
            }
            this.b19 |= (b38 & this.b11[b39]) << (this.b18 - b39);
            this.b18 -= b39;
            if (this.b18 == 0)
            {
                this._b21.A54((byte) this.b19);
                this.b19 = 0;
                this.b18 = 8;
            }
        }

        private void b35(int b36, int[][] b37)
        {
            int num;
            int num2;
            while (b36 >= 0xa40)
            {
                int[] numArray = b37[0x67];
                num = numArray[1];
                num2 = numArray[0];
                this.b34(num, num2);
                b36 -= numArray[2];
            }
            if (b36 >= 0x40)
            {
                int[] numArray2 = b37[0x3f + (b36 >> 6)];
                num = numArray2[1];
                num2 = numArray2[0];
                this.b34(num, num2);
                b36 -= numArray2[2];
            }
            num = b37[b36][1];
            num2 = b37[b36][0];
            this.b34(num, num2);
        }

        private int b40(byte[] b19, int b27, int b18)
        {
            if (b18 >= this.b17)
            {
                return 0;
            }
            return (((b19[b27 + (b18 >> 3)] & 0xff) >> (7 - (b18 & 7))) & 1);
        }

        private static int b41(byte[] b44, int b27, int b45, int b46, int b48)
        {
            return (b45 + ((b48 != 0) ? b43(b44, b27, b45, b46) : b47(b44, b27, b45, b46)));
        }

        private static int b42(byte[] b44, int b27, int b45, int b46, int b48)
        {
            if (b45 >= b46)
            {
                return b46;
            }
            return b41(b44, b27, b45, b46, b48);
        }

        private static int b43(byte[] b44, int b27, int b45, int b46)
        {
            int num2;
            int num3;
            int num = b46 - b45;
            int index = b27 + (b45 >> 3);
            if ((num > 0) && ((num2 = b45 & 7) != 0))
            {
                num3 = b15[(b44[index] << num2) & 0xff];
                if (num3 > (8 - num2))
                {
                    num3 = 8 - num2;
                }
                if (num3 > num)
                {
                    num3 = num;
                }
                if ((num2 + num3) < 8)
                {
                    return num3;
                }
                num -= num3;
                index++;
            }
            else
            {
                num3 = 0;
            }
            while (num >= 8)
            {
                if (b44[index] != 0xff)
                {
                    return (num3 + b15[b44[index] & 0xff]);
                }
                num3 += 8;
                num -= 8;
                index++;
            }
            if (num > 0)
            {
                num2 = b15[b44[index] & 0xff];
                num3 += (num2 > num) ? num : num2;
            }
            return num3;
        }

        private static int b47(byte[] b44, int b27, int b45, int b46)
        {
            int num2;
            int num3;
            int num = b46 - b45;
            int index = b27 + (b45 >> 3);
            if ((num > 0) && ((num2 = b45 & 7) != 0))
            {
                num3 = b14[(b44[index] << num2) & 0xff];
                if (num3 > (8 - num2))
                {
                    num3 = 8 - num2;
                }
                if (num3 > num)
                {
                    num3 = num;
                }
                if ((num2 + num3) < 8)
                {
                    return num3;
                }
                num -= num3;
                index++;
            }
            else
            {
                num3 = 0;
            }
            while (num >= 8)
            {
                if (b44[index] != 0)
                {
                    return (num3 + b14[b44[index] & 0xff]);
                }
                num3 += 8;
                num -= 8;
                index++;
            }
            if (num > 0)
            {
                num2 = b14[b44[index] & 0xff];
                num3 += (num2 > num) ? num : num2;
            }
            return num3;
        }
    }
}

