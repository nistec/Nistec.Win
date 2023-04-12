namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class Median
    {
        public static void AddMedianAverage(DataTable dt, string colName)
        {
            string str = GetMedianAverageBaseColumn(colName);
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
                List<double> list = new List<double>(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(Convert.ToDouble(row[colName]));
                }
                double num = 0.0;
                list.Sort();
                if ((list.Count % 2) == 1)
                {
                    num = list[(list.Count / 2) + 1];
                }
                else
                {
                    num = (list[list.Count / 2] + list[(list.Count / 2) + 1]) / 2.0;
                }
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = num;
                }
            }
        }

        internal static string GetMedianAverageBaseColumn(string colName)
        {
            if (!colName.StartsWith("$Median_"))
            {
                return null;
            }
            return colName.Replace("$Median_", "");
        }

        public static string GetName(string baseColumnName)
        {
            return ("$Median_" + baseColumnName);
        }
    }
}

