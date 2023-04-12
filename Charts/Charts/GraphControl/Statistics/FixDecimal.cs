namespace Nistec.Charts.Statistics
{
    using System;

    internal static class FixDecimal
    {
        public static decimal fix(double d)
        {
            try
            {
                decimal num = Convert.ToDecimal(d);
                return ((num > 0M) ? num : 0M);
            }
            catch
            {
                if (d > 0.0)
                {
                    return 79228162514264337593543950335M;
                }
                if (d < 0.0)
                {
                    return -79228162514264337593543950335M;
                }
                return 0M;
            }
        }
    }
}

