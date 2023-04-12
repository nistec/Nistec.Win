namespace Nistec.Charts.Statistics
{
    using System;
    using System.Data;

    public static class RunningAverage
    {
        public static void AddRunningAverage(DataTable dt, string colName)
        {
            if (colName.StartsWith("$Avg_"))
            {
                int num;
                string str = colName.Replace("$Avg_", "");
                try
                {
                    int index = str.IndexOf("_");
                    num = int.Parse(str.Substring(0, index));
                }
                catch
                {
                    throw new Exception("Invalid Column Name." + str);
                }
                if (num <= 0)
                {
                    num = 1;
                }
                str = str.Replace(num + "_", "");
                if (str != null)
                {
                    if (dt.Columns[str] == null)
                    {
                        throw new Exception("Invalid Column Name.");
                    }
                    if (dt.Columns[colName] == null)
                    {
                        dt.Columns.Add(colName, typeof(decimal));
                    }
                    decimal num3 = 0M;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        num3 += Convert.ToDecimal(dt.Rows[i][str]);
                        if (i >= num)
                        {
                            num3 -= Convert.ToDecimal(dt.Rows[i - num][str]);
                        }
                        if (i >= num)
                        {
                            dt.Rows[i][colName] = num3 / num;
                        }
                        else
                        {
                            dt.Rows[i][colName] = 0;
                        }
                    }
                }
            }
        }

        public static string GetName(string baseColumnName, int n)
        {
            return string.Concat(new object[] { "$Avg_", n, "_", baseColumnName });
        }

        internal static string GetRunningAverageBaseColumn(string colName)
        {
            int num;
            if (!colName.StartsWith("$Avg_"))
            {
                return null;
            }
            string str = colName.Replace("$Avg_", "");
            try
            {
                int index = str.IndexOf("_");
                num = int.Parse(str.Substring(0, index));
            }
            catch
            {
                return null;
            }
            return (str = str.Replace(num + "_", ""));
        }
    }
}

