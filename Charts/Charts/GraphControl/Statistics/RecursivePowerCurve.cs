namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class RecursivePowerCurve
    {
        public static void AddPowerCurveRecursion(DataTable dt, string colName, KeyItem li)
        {
            int f = 1;
            string str = GetRecursivePowerCurveBaseColumn(colName, out f);
            if (str != null)
            {
                if (dt.Columns[str] == null)
                {
                    throw new Exception("Invalid Column Name:" + str);
                }
                if (dt.Columns[colName] == null)
                {
                    dt.Columns.Add(colName, typeof(decimal));
                }
                List<Punct> al = new List<Punct>();
                int num2 = 1;
                foreach (DataRow row in dt.Rows)
                {
                    al.Add(new Punct((double) num2++, Convert.ToDouble(row[str])));
                }
                Recursion recursion = new Recursion(al);
                recursion.rezolva(Recursion.RecursiveType.PowerCurve, f);
                bool valid = recursion.valid;
                num2 = 0;
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = FixDecimal.fix(al[num2++].y_recalc);
                }
                li.hint = recursion.text;
            }
        }

        public static string GetName(string baseColumnName, float f)
        {
            return string.Concat(new object[] { "$Curve_", f, "_", baseColumnName });
        }

        internal static string GetRecursivePowerCurveBaseColumn(string colName, out int f)
        {
            if (!colName.StartsWith("$Curve_"))
            {
                f = 2;
                return null;
            }
            string str = colName.Replace("$Curve_", "");
            int index = str.IndexOf("_");
            try
            {
                f = int.Parse(str.Substring(0, index));
                str = str.Replace(((int) f).ToString() + "_", "");
            }
            catch
            {
                f = 2;
                return null;
            }
            return str;
        }
    }
}

