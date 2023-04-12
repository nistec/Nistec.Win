namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd868 : mtd708
    {
        private byte[] _var0;
        private ushort[] _var1;
        private short _var10;
        private string[] _var2;
        private uint _var3;
        private byte[] _var4;
        private uint _var5;
        private uint _var6;
        private uint _var7;
        private uint _var8;
        private short _var9;

        internal mtd868(mtd821 var11) : base(var11)
        {
        }

        protected override void mtd822(mtd708 var14, mtd823 var15)
        {
            mtd868 mtd = var14 as mtd868;
            byte[] buffer = new byte[4];
            buffer[1] = 3;
            this._var0 = buffer;
            this._var4 = new byte[mtd._var4.Length];
            mtd._var4.CopyTo(this._var4, 0);
            this._var9 = mtd._var9;
            this._var10 = mtd._var10;
            this._var3 = mtd._var3;
            this._var8 = mtd._var8;
            this._var6 = mtd._var6;
            this._var7 = mtd._var7;
            this._var5 = mtd._var5;
        }

        protected override void mtd824(mtd825 var17)
        {
            this._var0 = var17.mtd826(4);
            this._var4 = var17.mtd826(4);
            this._var9 = var17.mtd892();
            this._var10 = var17.mtd892();
            this._var3 = var17.mtd837();
            this._var8 = var17.mtd837();
            this._var6 = var17.mtd837();
            this._var7 = var17.mtd837();
            this._var5 = var17.mtd837();
            this.var16(var17);
        }

        protected override void mtd828(mtd829 var17)
        {
            var17.mtd830(this._var0);
            var17.mtd830(this._var4);
            var17.mtd893(this._var9);
            var17.mtd893(this._var10);
            var17.mtd839(this._var3);
            var17.mtd839(this._var8);
            var17.mtd839(this._var6);
            var17.mtd839(this._var7);
            var17.mtd839(this._var5);
            this.var20(var17);
        }

        internal static float mtd928(byte[] var23)
        {
            if (var23.Length != 4)
            {
                return 0f;
            }
            byte[] buffer = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 4; i++)
                {
                    buffer[i] = var23[(4 - i) - 1];
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    buffer[j] = var23[j];
                }
            }
            short num3 = BitConverter.ToInt16(buffer, 2);
            ushort num4 = BitConverter.ToUInt16(buffer, 0);
            if (num4 != 0)
            {
                double num5 = Math.Pow(10.0, Math.Ceiling(Math.Log10((double) num4)));
                return Convert.ToSingle((double) (num3 + ((((double) num4) / num5) * Math.Sign(num3))));
            }
            return Convert.ToSingle(num3);
        }

        private int var12()
        {
            float num = this.mtd927;
            this.var13(num);
            if (num != 2f)
            {
                return 0;
            }
            int num2 = 2 + (this._var1.Length * 2);
            for (int i = 0; i < this._var2.Length; i++)
            {
                num2 += this._var2[i].Length + 1;
            }
            return num2;
        }

        private void var13(float var19)
        {
            if (((var19 != 1f) && (var19 != 2f)) && (var19 != 3f))
            {
                throw new Exception("Invalid Post format in Font File");
            }
        }

        private void var16(mtd825 var17)
        {
            float num = this.mtd927;
            this.var13(num);
            if (num == 2f)
            {
                ushort num2 = var17.mtd835();
                this._var1 = new ushort[num2];
                int num3 = 0;
                for (int i = 0; i < num2; i++)
                {
                    this._var1[i] = var17.mtd835();
                    if (this._var1[i] > 0x101)
                    {
                        num3++;
                    }
                }
                this._var2 = new string[num3];
                for (int j = 0; j < num3; j++)
                {
                    this._var2[j] = this.var18(var17);
                }
            }
        }

        private string var18(mtd825 var17)
        {
            string str = "";
            int num = var17.mtd901();
            for (int i = 0; i < num; i++)
            {
                str = str + ((char) var17.mtd900());
            }
            return str;
        }

        private void var20(mtd829 var17)
        {
            float num = this.mtd927;
            this.var13(num);
            if (num == 2f)
            {
                var17.mtd838((ushort) this._var1.Length);
                for (int i = 0; i < this._var1.Length; i++)
                {
                    var17.mtd838(this._var1[i]);
                }
                for (int j = 0; j < this._var2.Length; j++)
                {
                    this.var21(var17, this._var2[j]);
                }
            }
        }

        private void var21(mtd829 var17, string var22)
        {
            var17.mtd924((sbyte) var22.Length);
            for (int i = 0; i < var22.Length; i++)
            {
                var17.mtd925((byte) var22[i]);
            }
        }

        internal override int mtd736
        {
            get
            {
                return (0x20 + this.var12());
            }
        }

        protected internal override string mtd789
        {
            get
            {
                return "post";
            }
        }

        internal float mtd927
        {
            get
            {
                return mtd928(this._var0);
            }
        }

        internal float mtd929
        {
            get
            {
                return mtd928(this._var4);
            }
        }

        internal uint mtd930
        {
            get
            {
                return this._var3;
            }
        }
    }
}

