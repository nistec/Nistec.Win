namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class RecursiveExponential
    {
        public static void AddExponentialRecursion(DataTable dt, string colName, KeyItem li)
        {
            string str = GetRecursiveExponentialBaseColumn(colName);
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
                int num = 0;
                foreach (DataRow row in dt.Rows)
                {
                    al.Add(new Punct((double) num++, Convert.ToDouble(row[str])));
                }
                Recursion recursion = new Recursion(al);
                recursion.rezolva(Recursion.RecursiveType.Exponential, 0);
                bool valid = recursion.valid;
                num = 0;
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = FixDecimal.fix(al[num++].y_recalc);
                }
                li.hint = recursion.text;
            }
        }

        public static string GetName(string baseColumnName)
        {
            return ("$Exponential_" + baseColumnName);
        }

        internal static string GetRecursiveExponentialBaseColumn(string colName)
        {
            if (!colName.StartsWith("$Exponential_"))
            {
                return null;
            }
            return colName.Replace("$Exponential_", "");
        }
    }
}

