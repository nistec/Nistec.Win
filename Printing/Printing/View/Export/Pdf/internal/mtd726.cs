namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Collections;

    internal class mtd726
    {
        private int var0;
        private byte[] var1;
        private ArrayList var2;
        private const int var3 = 0x102;
        private const int var4 = 0x8000;
        private const int var5 = 3;
        private int var6;
        private int var7;

        internal mtd726(byte[] var1) : this(var1, 15)
        {
        }

        internal mtd726(byte[] var1, int var8)
        {
            this.var2 = new ArrayList();
            if ((var8 < 8) || (var8 > 15))
            {
                var8 = 15;
            }
            this.var7 = ((int) 1) << var8;
            this.var0 = this.var7 - 0x102;
            this.mtd111(var1);
        }

        internal void mtd111()
        {
            this.var6 = 0;
        }

        internal void mtd111(byte[] input)
        {
            this.var1 = input;
            this.mtd111();
        }

        internal bool mtd728(mtd727 var13)
        {
            if (var13 == null)
            {
                return false;
            }
            if (this.var6 >= this.var1.Length)
            {
                return false;
            }
            byte num = this.var1[this.var6];
            this.var9(num);
            int count = this.var2.Count;
            if (count <= 0)
            {
                var13.mtd735 = num;
                var13.mtd734 = true;
                this.var14(1);
            }
            else
            {
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < count; i++)
                {
                    int num6 = this.var11((int) this.var2[i]);
                    if (num6 > num4)
                    {
                        num3 = (int) this.var2[i];
                        num4 = num6;
                        if (num4 > 0x102)
                        {
                            num4 = 0x102;
                            break;
                        }
                    }
                }
                if (num4 < 3)
                {
                    num4 = 1;
                }
                if (num4 > 1)
                {
                    if (num3 > 0x8000)
                    {
                        throw new Exception("The phrase offset is out of bounds");
                    }
                    var13.mtd737 = num3;
                    var13.mtd736 = num4;
                    var13.mtd734 = false;
                }
                else
                {
                    var13.mtd735 = num;
                    var13.mtd734 = true;
                }
                this.var14(num4);
            }
            return true;
        }

        private int var11(int var12)
        {
            int num = this.var1.Length - this.var6;
            int num2 = 1;
            int num3 = (0x102 < num) ? 0x102 : num;
            for (int i = 1; i < num3; i++)
            {
                if (this.var1[(this.var6 - var12) + i] != this.var1[this.var6 + i])
                {
                    return num2;
                }
                num2++;
            }
            return num2;
        }

        private void var14(int var15)
        {
            this.var6 += var15;
        }

        private void var9(byte var10)
        {
            this.var2.Clear();
            int num = (this.var0 < this.var6) ? this.var0 : this.var6;
            for (int i = 1; i <= num; i++)
            {
                if (this.var1[this.var6 - i] == var10)
                {
                    this.var2.Add(i);
                }
            }
        }
    }
}

