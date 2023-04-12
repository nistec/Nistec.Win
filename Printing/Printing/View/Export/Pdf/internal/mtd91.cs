namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd91 : mtd708
    {
        private ushort _var0;
        private ushort _var1;
        private mtd916[] _var2;
        private int _var3;
        private string[] _var4;

        internal mtd91(mtd821 var5) : base(var5)
        {
            this._var4 = new string[8];
        }

        protected override void mtd822(mtd708 var7, mtd823 var8)
        {
            mtd91 mtd = var7 as mtd91;
            this._var4[1] = var8.mtd886;
            this._var4[2] = mtd._var4[2];
            this._var4[3] = var8.mtd886;
            this._var4[4] = var8.mtd886;
            this._var4[6] = var8.mtd886;
            this.var6();
        }

        protected override void mtd824(mtd825 var10)
        {
            this._var3 = var10.mtd832;
            var10.mtd834(2);
            this._var0 = var10.mtd835();
            this._var1 = var10.mtd835();
            this.var9(var10);
            this.var11(var10);
        }

        protected override void mtd828(mtd829 var10)
        {
            this._var3 = var10.mtd832;
            var10.mtd838(0);
            var10.mtd838(this._var0);
            var10.mtd838(this._var1);
            this.var12(var10);
            this.var13(var10);
        }

        private void var11(mtd825 var10)
        {
            for (int i = 0; i < this._var2.Length; i++)
            {
                if (((this._var2[i].mtd917 == 3) && (this._var2[i].mtd918 == 1)) && (this._var2[i].mtd919 == 0x409))
                {
                    var10.mtd833(this._var3);
                    var10.mtd834(this._var1);
                    var10.mtd834(this._var2[i].mtd921);
                    if ((this._var2[i].mtd852 >= 0) && (this._var2[i].mtd852 <= 7))
                    {
                        this._var4[this._var2[i].mtd852] = var10.mtd915(this._var2[i].mtd920);
                    }
                }
            }
        }

        private void var12(mtd829 var10)
        {
            for (int i = 0; i < this._var2.Length; i++)
            {
                var10.mtd838(this._var2[i].mtd917);
                var10.mtd838(this._var2[i].mtd918);
                var10.mtd838(this._var2[i].mtd919);
                var10.mtd838(this._var2[i].mtd852);
                var10.mtd838(this._var2[i].mtd920);
                var10.mtd838(this._var2[i].mtd921);
            }
        }

        private void var13(mtd829 var10)
        {
            for (int i = 0; i < this._var2.Length; i++)
            {
                var10.mtd833(this._var3);
                var10.mtd834(this._var1);
                var10.mtd834(this._var2[i].mtd921);
                var10.mtd922(this._var4[this._var2[i].mtd852]);
            }
        }

        private void var6()
        {
            int num = 0;
            for (int i = 0; i < 8; i++)
            {
                if (this._var4[i] != null)
                {
                    num++;
                }
            }
            this._var2 = new mtd916[num];
            int index = 0;
            int num4 = 0;
            for (int j = 0; j < 8; j++)
            {
                if (this._var4[j] != null)
                {
                    this._var2[index].mtd917 = 3;
                    this._var2[index].mtd918 = 1;
                    this._var2[index].mtd919 = 0x409;
                    this._var2[index].mtd852 = (ushort) j;
                    this._var2[index].mtd920 = (ushort) (this._var4[j].Length * 2);
                    this._var2[index].mtd921 = (ushort) num4;
                    num4 += this._var2[index].mtd920;
                    index++;
                }
            }
            this._var0 = (ushort) this._var2.Length;
            this._var1 = (ushort) (6 + (mtd916.mtd845 * this._var0));
        }

        private void var9(mtd825 var10)
        {
            this._var2 = new mtd916[this._var0];
            for (int i = 0; i < this._var0; i++)
            {
                this._var2[i].mtd917 = var10.mtd835();
                this._var2[i].mtd918 = var10.mtd835();
                this._var2[i].mtd919 = var10.mtd835();
                this._var2[i].mtd852 = var10.mtd835();
                this._var2[i].mtd920 = var10.mtd835();
                this._var2[i].mtd921 = var10.mtd835();
            }
        }

        internal override int mtd736
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (this._var4[i] != null)
                    {
                        num += this._var4[i].Length * 2;
                    }
                }
                return ((6 + (mtd916.mtd845 * this._var2.Length)) + num);
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "name";
            }
        }
    }
}

