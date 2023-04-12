namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd716
    {
        private const uint base_ = 0xfff1;
        private uint var0 = 1;

        internal mtd716()
        {
        }

        internal long mtd717(byte[] var1)
        {
            return this.mtd717(var1, 0, var1.Length);
        }

        internal long mtd717(int[] var1)
        {
            for (int i = 0; i < var1.Length; i++)
            {
                this.mtd717(BitConverter.GetBytes(var1[i]));
            }
            return (long) this.var0;
        }

        internal long mtd717(byte[] var1, int var2, int var3)
        {
            uint num = this.var0 & 0xffff;
            uint num2 = this.var0 >> 0x10;
            while (var3 > 0)
            {
                int num3 = 0xed8;
                if (num3 > var3)
                {
                    num3 = var3;
                }
                var3 -= num3;
                while (--num3 >= 0)
                {
                    num += (uint) (var1[var2++] & 0xff);
                    num2 += num;
                }
                num = num % 0xfff1;
                num2 = num2 % 0xfff1;
            }
            this.var0 = (num2 << 0x10) | num;
            return (long) this.var0;
        }

        internal static long mtd718(byte[] var1)
        {
            return new mtd716().mtd717(var1, 0, var1.Length);
        }
    }
}

