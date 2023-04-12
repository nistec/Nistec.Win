namespace Nistec.Printing.View
{
    using System;

    internal class mtd224 : mtd102
    {
        private object[] _var0;

        internal mtd224()
        {
            this._var0 = new object[500];
        }

        internal mtd224(int var1)
        {
            this._var0 = new object[var1];
        }

        internal override void mtd103(object var2)
        {
            int num;
            if (base._mtd218 > 0)
            {
                num = base._mtd218 - 1;
            }
            else
            {
                num = 0;
            }
            this.var3(num, var2);
        }

        internal override object mtd110(int var4, int var5, AggregateType var6)
        {
            if (var6 == AggregateType.Count)
            {
                return var7(this, var4, var5);
            }
            return null;
        }

        internal override void mtd111()
        {
            this._var0 = new object[500];
            base._mtd218 = 0;
        }

        internal override void mtd2(object var2)
        {
            this.mtd219();
            this.var3(base._mtd218, var2);
            base._mtd218++;
        }

        internal override void mtd219()
        {
            if (base._mtd218 >= this._var0.Length)
            {
                object[] destinationArray = new object[this._var0.Length + 0x3e8];
                Array.Copy(this._var0, destinationArray, base._mtd218);
                this._var0 = destinationArray;
            }
        }

        private void var3(int var4, object var2)
        {
            if ((var2 == null) || (var2 == DBNull.Value))
            {
                this._var0[var4] = null;
            }
            else
            {
                this._var0[var4] = var2;
            }
        }

        private static int var7(mtd224 var8, int var4, int var5)
        {
            int num = 0;
            try
            {
                object[] objArray = var8.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    object obj2 = objArray[i];
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num++;
                    }
                }
            }
            catch
            {
            }
            return num;
        }

        internal object[] mtd225
        {
            get
            {
                return this._var0;
            }
        }
    }
}

