namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class RecursiveLinear
    {
        public static void AddLinearRecursion(DataTable dt, string colName, KeyItem li)
        {
            string str = GetRecursiveLinearBaseColumn(colName);
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
                recursion.rezolva(Recursion.RecursiveType.Linear, 0);
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
            return ("$Linear_" + baseColumnName);
        }

        internal static string GetRecursiveLinearBaseColumn(string colName)
        {
            if (!colName.StartsWith("$Linear_"))
            {
                return null;
            }
            return colName.Replace("$Linear_", "");
        }
    }
}

