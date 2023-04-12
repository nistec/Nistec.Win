namespace Nistec.Charts.Statistics
{
    using System;
    using System.Data;

    public static class ArithmeticAverage
    {
        public static void AddArithmeticAverage(DataTable dt, string colName)
        {
            string str = GetArithmeticAverageBaseColumn(colName);
            if (str != null)
            {
                if (dt.Columns[str] == null)
                {
                    throw new Exception("Invalid Column Name" + str);
                }
                if (dt.Columns[colName] == null)
                {
                    dt.Columns.Add(colName, typeof(decimal));
                }
                decimal num = 0M;
                foreach (DataRow row in dt.Rows)
                {
                    num += Convert.ToDecimal(row[str]);
                }
                num /= dt.Rows.Count;
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = num;
                }
            }
        }

        internal static string GetArithmeticAverageBaseColumn(string colName)
        {
            if (!colName.StartsWith("$ArithmeticAvg_"))
            {
                return null;
            }
            return colName.Replace("$ArithmeticAvg_", "");
        }

        public static string GetName(string baseColumnName)
        {
            return ("$ArithmeticAvg_" + baseColumnName);
        }
    }
}

