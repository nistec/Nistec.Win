namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;
    using System.IO;

    internal class mtd722
    {
        private int var0;
        private uint var1;
        private ArrayList var2 = new ArrayList();
        private int var3;
        private const int var4 = 0x1000;

        internal mtd722()
        {
            this.var5();
        }

        internal void mtd724(int var13, int var14)
        {
            this.var1 |= (uint) (var13 << this.var0);
            this.var0 += var14;
            if (this.var0 >= 0x10)
            {
                this.var6((byte) this.var1);
                this.var6((byte) (this.var1 >> 8));
                this.var1 = this.var1 >> 0x10;
                this.var0 -= 0x10;
            }
        }

        internal void mtd729()
        {
            if (this.var0 > 0)
            {
                this.var6((byte) this.var1);
                if (this.var0 > 8)
                {
                    this.var6((byte) (this.var1 >> 8));
                }
            }
            this.var1 = 0;
            this.var0 = 0;
        }

        internal void mtd730(int var15)
        {
            this.var6((byte) (var15 >> 8));
            this.var6((byte) var15);
        }

        internal void mtd731(Stream var11)
        {
            if (this.var12 != 0)
            {
                for (int i = 0; i < (this.var12 - 1); i++)
                {
                    byte[] buffer = this.var7(i);
                    var11.Write(buffer, 0, buffer.Length);
                }
                if (this.var3 > 0)
                {
                    var11.Write(this.var10, 0, this.var3);
                }
                if (this.var0 != 0)
                {
                    var11.WriteByte((byte) this.var1);
                    if (this.var0 > 8)
                    {
                        var11.WriteByte((byte) (this.var1 >> 8));
                    }
                }
            }
        }

        private void var5()
        {
            this.var2.Add(new byte[0x1000]);
            this.var3 = 0;
        }

        private void var6(byte var9)
        {
            this.var10[this.var3++] = var9;
            if (this.var3 >= this.var10.Length)
            {
                this.var5();
            }
        }

        private byte[] var7(int var8)
        {
            return (byte[]) this.var2[var8];
        }

        private byte[] var10
        {
            get
            {
                return (byte[]) this.var2[this.var12 - 1];
            }
        }

        private int var12
        {
            get
            {
                return this.var2.Count;
            }
        }
    }
}

