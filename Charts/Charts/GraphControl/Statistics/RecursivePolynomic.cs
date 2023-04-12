namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class RecursivePolynomic
    {
        public static void AddPolynomicRecursion(DataTable dt, string colName, KeyItem li)
        {
            int order = 1;
            string str = GetRecursivePolynomicBaseColumn(colName, out order);
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
                int num2 = 0;
                foreach (DataRow row in dt.Rows)
                {
                    al.Add(new Punct((double) num2++, Convert.ToDouble(row[str])));
                }
                Recursion recursion = new Recursion(al);
                recursion.rezolva(Recursion.RecursiveType.Polynomic, order);
                bool valid = recursion.valid;
                num2 = 0;
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = FixDecimal.fix(al[num2++].y_recalc);
                }
                li.hint = recursion.text;
            }
        }

        public static string GetName(string baseColumnName, int ord)
        {
            return string.Concat(new object[] { "$Polynomic_", ord, "_", baseColumnName });
        }

        internal static string GetRecursivePolynomicBaseColumn(string colName, out int order)
        {
            if (!colName.StartsWith("$Polynomic_"))
            {
                order = 1;
                return null;
            }
            string str = colName.Replace("$Polynomic_", "");
            try
            {
                int index = str.IndexOf("_");
                order = int.Parse(str.Substring(0, index));
                str = str.Replace(((int) order).ToString() + "_", "");
            }
            catch
            {
                order = 1;
                return null;
            }
            return str;
        }
    }
}

