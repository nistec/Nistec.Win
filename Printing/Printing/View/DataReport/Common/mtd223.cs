namespace Nistec.Printing.View
{
    using System;

    internal class mtd223 : mtd102
    {
        private decimal[] _var0;

        internal mtd223()
        {
            this._var0 = new decimal[500];
            base._mtd218 = 0;
        }

        internal mtd223(int var1)
        {
            this._var0 = new decimal[var1];
            base._mtd218 = 0;
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
            this._var0 = new decimal[500];
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
                decimal[] destinationArray = new decimal[this._var0.Length + 0x3e8];
                Array.Copy(this._var0, destinationArray, base._mtd218);
                this._var0 = destinationArray;
            }
        }

        private static object var10(mtd223 var12, int var4, int var5, AggregateType var6)
        {
            decimal d = var5 + 1;
            if (d > 1M)
            {
                try
                {
                    decimal num2 = 0M;
                    decimal num3 = 0M;
                    decimal num4 = 0M;
                    decimal[] numArray = var12.mtd225;
                    for (int i = var4; i <= var5; i++)
                    {
                        num2 = numArray[i];
                        num3 += num2 * num2;
                        num4 += num2;
                    }
                    if (var6 == AggregateType.StdDev)
                    {
                        decimal op = --d;
                        return (decimal)Math.Sqrt((double)((((d * num3) - (num4 * num4)) / d) * op));//decimal.op_Decrement(d)));
                    }
                    if (var6 == AggregateType.StdDevP)
                    {
                        return (decimal) Math.Sqrt((double) (((d * num3) - (num4 * num4)) / (d * d)));
                    }
                    if (var6 == AggregateType.Var)
                    {
                        decimal op = --d;
                        return ((((d * num3) - (num4 * num4)) / d) * op);// decimal.op_Decrement(d));
                    }
                    return (((d * num3) - (num4 * num4)) / (d * d));
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
                this._var0[var4] = 0M;
            }
            else if (var2 is decimal)
            {
                this._var0.SetValue(var2, var4);
            }
            else if (var2 is string)
            {
                try
                {
                    decimal num = decimal.Parse((string) var2);
                    this._var0[var4] = num;
                }
                catch
                {
                    this._var0[var4] = 0M;
                }
            }
            else
            {
                try
                {
                    this._var0[var4] = Convert.ToDecimal(var2);
                }
                catch
                {
                    this._var0[var4] = 0M;
                }
            }
        }

        private static object var7(mtd223 decimalStorage, int var4, int var5, bool var11)
        {
            try
            {
                decimal num = 0M;
                decimal[] numArray = decimalStorage.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num += numArray[i];
                }
                if (var11 && ((var5 + 1) > 0))
                {
                    num /= var5 + 1;
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        private static object var8(mtd223 var12, int var4, int var5)
        {
            try
            {
                decimal num = 0M;
                decimal[] numArray = var12.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num = Math.Max(num, numArray[i]);
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        private static object var9(mtd223 var12, int var4, int var5)
        {
            try
            {
                decimal num = 0M;
                decimal[] numArray = var12.mtd225;
                for (int i = var4; i <= var5; i++)
                {
                    num = Math.Min(num, numArray[i]);
                }
                return num;
            }
            catch
            {
            }
            return null;
        }

        internal decimal[] mtd225
        {
            get
            {
                return this._var0;
            }
        }
    }
}

