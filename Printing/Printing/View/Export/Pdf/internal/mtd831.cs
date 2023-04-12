namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd831 : mtd708
    {
        private ushort[] _var0;
        private ushort[] _var1;
        private ushort[] _var2;
        private ushort[] _var3;
        private int _var4;
        private ushort[] _var5;
        private ushort _var6;
        private ushort _var7;

        internal mtd831(mtd821 var8) : base(var8)
        {
            this._var2 = new ushort[0x10000];
        }

        protected override void mtd822(mtd708 var15, mtd823 var13)
        {
            mtd831 mtd = var15 as mtd831;
            this._var7 = mtd._var7;
            this._var6 = mtd._var6;
            for (int i = 0; i < var13.mtd836.Length; i++)
            {
                ushort index = var13.mtd836[i];
                this._var2[index] = mtd[index];
            }
            this.var12(var13);
        }

        protected override void mtd824(mtd825 var11)
        {
            int num = var11.mtd832;
            this._var7 = var11.mtd835();
            int num2 = Convert.ToInt32(var11.mtd835());
            int num3 = -1;
            for (int i = 0; i < num2; i++)
            {
                ushort num5 = var11.mtd835();
                ushort num6 = var11.mtd835();
                if ((num5 == 3) && (num6 == 1))
                {
                    num3 = Convert.ToInt32(var11.mtd837());
                    break;
                }
                var11.mtd834(4);
            }
            if (num3 == -1)
            {
                throw new Exception("Unicode CMap not found in font file");
            }
            var11.mtd833(num);
            var11.mtd834(num3);
            this.var16(var11);
        }

        protected override void mtd828(mtd829 var11)
        {
            int num = var11.mtd832;
            var11.mtd838(this._var7);
            var11.mtd838(1);
            var11.mtd838(3);
            var11.mtd838(1);
            var11.mtd839((uint) ((var11.mtd832 - num) + 4));
            this.var18(var11);
        }

        private void var10(mtd825 var11)
        {
            int num = var11.mtd832;
            for (int i = 0; i < this._var1.Length; i++)
            {
                for (int j = this._var5[i]; j <= this._var1[i]; j++)
                {
                    ushort num4 = 0;
                    if ((this._var3[i] != 0) && (j != 0xffff))
                    {
                        int num5 = (((((this._var3[i] / 2) + j) - this._var5[i]) + i) - this._var3.Length) * 2;
                        var11.mtd833(num);
                        var11.mtd834(num5);
                        num4 = var11.mtd835();
                        if (num4 != 0)
                        {
                            num4 = (ushort) ((num4 + this._var0[i]) & 0xffff);
                        }
                    }
                    else
                    {
                        num4 = (ushort) ((j + this._var0[i]) & 0xffff);
                    }
                    this._var2[j] = num4;
                }
            }
        }

        private void var12(mtd823 var13)
        {
            this._var4 = 0;
            for (int i = 0; i < (var13.mtd836.Length - 1); i++)
            {
                if (var13.mtd836[i + 1] != (var13.mtd836[i] + 1))
                {
                    this._var4++;
                }
            }
            this._var4 += 2;
            this._var1 = new ushort[this._var4];
            this._var5 = new ushort[this._var4];
            this._var0 = new ushort[this._var4];
            this._var3 = new ushort[this._var4];
            if (var13.mtd836.Length > 0)
            {
                int index = 0;
                this._var5[0] = var13.mtd836[0];
                for (int j = 0; j < (var13.mtd836.Length - 1); j++)
                {
                    if (var13.mtd836[j + 1] != (var13.mtd836[j] + 1))
                    {
                        this._var1[index] = var13.mtd836[j];
                        this._var5[index + 1] = var13.mtd836[j + 1];
                        index++;
                    }
                }
                this._var1[index] = var13.mtd836[var13.mtd836.Length - 1];
                this._var1[index + 1] = 0xffff;
                this._var5[index + 1] = 0xffff;
            }
            this.var14();
        }

        private void var14()
        {
            int num = 0;
            for (int i = 0; i < this._var4; i++)
            {
                bool flag = true;
                for (int j = this._var5[i]; j < this._var1[i]; j++)
                {
                    if (this._var2[j + 1] != (this._var2[j] + 1))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    this._var3[i] = 0;
                    int num4 = this._var2[this._var5[i]] - this._var5[i];
                    if (num4 < 0)
                    {
                        num4 += 0x10000;
                    }
                    this._var0[i] = (ushort) num4;
                }
                else
                {
                    this._var3[i] = (ushort) (num + ((this._var3.Length - i) * 2));
                    num += ((this._var1[i] - this._var5[i]) + 1) * 2;
                    this._var0[i] = 0;
                }
            }
        }

        private void var16(mtd825 var11)
        {
            if (var11.mtd835() != 4)
            {
                throw new Exception("Invalid CMap format");
            }
            var11.mtd835();
            this._var6 = var11.mtd835();
            this._var4 = Convert.ToInt32((int) (var11.mtd835() / 2));
            this._var1 = new ushort[this._var4];
            this._var5 = new ushort[this._var4];
            this._var0 = new ushort[this._var4];
            this._var3 = new ushort[this._var4];
            var11.mtd834(6);
            for (int i = 0; i < this._var4; i++)
            {
                this._var1[i] = var11.mtd835();
            }
            var11.mtd834(2);
            for (int j = 0; j < this._var4; j++)
            {
                this._var5[j] = var11.mtd835();
            }
            for (int k = 0; k < this._var4; k++)
            {
                this._var0[k] = var11.mtd835();
            }
            for (int m = 0; m < this._var4; m++)
            {
                this._var3[m] = var11.mtd835();
            }
            this.var10(var11);
        }

        private void var17(mtd829 var11)
        {
            for (int i = 0; i < this._var3.Length; i++)
            {
                if (this._var3[i] != 0)
                {
                    for (int j = this._var5[i]; j <= this._var1[i]; j++)
                    {
                        var11.mtd838(this._var2[j]);
                    }
                }
            }
        }

        private void var18(mtd829 var11)
        {
            int num = var11.mtd832;
            var11.mtd838(4);
            var11.mtd838(0);
            var11.mtd838(this._var6);
            var11.mtd838((ushort) (this._var4 * 2));
            double y = Math.Floor(Math.Log((double) this._var4, 2.0));
            double num3 = Math.Pow(2.0, y) * 2.0;
            var11.mtd838((ushort) num3);
            var11.mtd838((ushort) y);
            var11.mtd838((ushort) ((this._var4 * 2) - num3));
            for (int i = 0; i < this._var1.Length; i++)
            {
                var11.mtd838(this._var1[i]);
            }
            var11.mtd838(0);
            for (int j = 0; j < this._var5.Length; j++)
            {
                var11.mtd838(this._var5[j]);
            }
            for (int k = 0; k < this._var0.Length; k++)
            {
                var11.mtd838(this._var0[k]);
            }
            for (int m = 0; m < this._var3.Length; m++)
            {
                var11.mtd838(this._var3[m]);
            }
            this.var17(var11);
            int num8 = var11.mtd832 - num;
            var11.mtd833(num);
            var11.mtd834(2);
            var11.mtd838((ushort) num8);
            var11.mtd833(num);
            var11.mtd834(num8);
        }

        internal int mtd166
        {
            get
            {
                return this._var2.Length;
            }
        }

        internal override int mtd736
        {
            get
            {
                int num = ((14 + (2 * this._var4)) + 2) + (6 * this._var4);
                for (int i = 0; i < this._var4; i++)
                {
                    if (this._var3[i] != 0)
                    {
                        num += ((this._var1[i] - this._var5[i]) + 1) * 2;
                    }
                }
                return (12 + num);
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "cmap";
            }
        }

        internal ushort this[int var9]
        {
            get
            {
                return this._var2[var9];
            }
        }
    }
}

