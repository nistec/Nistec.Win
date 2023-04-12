namespace Nistec.Printing.View
{
    using System;

    internal class mtd221 : mtd102
    {
        private float[] _var0;

        internal mtd221()
        {
            this._var0 = new float[500];
        }

        internal mtd221(int var1)
        {
            this._var0 = new float[var1];
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
            if (var6 == AggregateType.Sum)
            {
                return var7(this, var4, var5, false);
            }
            if (var6 == AggregateType.Avg)
            {
                return var7(this, var4, var5, true);
            }
            if (var6 == AggregateType.Count)
            {
                return (var4 + 1);
            }
            if (var6 == AggregateType.Max)
            {
                return var8(this, var4, var5);
            }
            if (var6 == AggregateType.Min)
            {
                return var9(this, var4, var5);
            }
            if (var6 == AggregateType.StdDev)
            {
                return var10(this, var4, var5, var6);
            }
            if (var6 == AggregateType.StdDevP)
            {
                return var10(this, var4, var5, var6);
            }
            if (var6 == AggregateType.Var)
            {
                return var10(this, var4, var5, var6);
            }
            if (var6 == AggregateType.VarP)
            {
                return var10(this, var4, var5, var6);
            }
            return null;
        }

        internal override void mtd111()
        {
            this._var0 = new float[500];
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
                float[] destinationArray = new float[this._var0.Length + 0x3e8];
                Array.Copy(this._var0, destinationArray, base._mtd218);
                this._var0 = destinationArray;
            }
        }

        private static object var10(mtd221 var11, int var4, int var5, AggregateType var6)
        {
            float num = var5 + 1;
            if (num > 1f)
            {
                try
                {
                    float num2 = 0f;
                    float num3 = 0f;
                    float num4 = 0f;
                    float[] numArray = var11.mtd225;
                    for (int i = var4; i <= var5; i++)
                    {
                        num2 = numArray[i];
                        num3 += num2 * num2;
                        num4 += num2;
                    }
                    if (var6 == AggregateType.StdDev)
                    {
                        return (float) Math.Sqrt((double) ((((num * num3) - (num4 * num4)) / num) * (num - 1f)));
                    }
                    if (var6 == AggregateType.StdDevP)
                    {
                        return (float) Math.Sqrt((double) (((num * num3) - (num4 * num4)) / (num * num)));
                    }
                    if (var6 == AggregateType.Var)
                    {
                        return ((((num * num3) - (num4 * num4)) / num) * (num - 1f));
                    }
                    return (((num * num3) - (num4 * num4)) / (num * num));
                }
                catch
                {
                }
            }
            return null;
        }

        private void var3(int var4, object var2)
        {
            if ((var2 == null) || (var2 == DBNull.Value))
            {
                this._var0[var4] = 0f;
            }
            else if (var2 is float)
            {
                this._var0.SetValue(var2, var4);
            }
            else if (var2 is string)
            {
                try
                {
                    float num = float.Parse((string) var2);
                    this._var0[var4] = num;
                }
                catch
                {
                    this._var0[var4] = 0f;
                }
            }
            else
            {
                try
                {
                    this._var0[var4] = (float) var2;
                }
                catch
                {
                    this._var0[var4] = 0f;
                }
            }
        }

        private static object var7(mtd221 var11, int var4, int var5, bool var12)
        {
            try
            {
                double num = 0.0;
                float[] numArray = var11.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num += numArray[i];
                }
                if (var12 && ((var5 + 1) > 0))
                {
                    num /= (double) (var5 + 1);
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        private static object var8(mtd221 var11, int var4, int var5)
        {
            try
            {
                double num = 0.0;
                float[] numArray = var11.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num = Math.Max(num, (double) numArray[i]);
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        private static object var9(mtd221 var11, int var4, int var5)
        {
            try
            {
                double num = 0.0;
                float[] numArray = var11.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num = Math.Min(num, (double) numArray[i]);
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        internal float[] mtd225
        {
            get
            {
                return this._var0;
            }
        }
    }
}

