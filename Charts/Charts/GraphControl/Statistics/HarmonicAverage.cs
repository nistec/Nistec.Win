namespace Nistec.Charts.Statistics
{
    using System;
    using System.Data;

    public static class HarmonicAverage
    {
        public static void AddHarmonicAverage(DataTable dt, string colName)
        {
            string str = GetHarmonicAverageBaseColumn(colName);
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
                double num = 0.0;
                foreach (DataRow row in dt.Rows)
                {
                    double num2 = Convert.ToDouble(row[str]);
                    num += (num2 == 0.0) ? 0.0 : (1.0 / num2);
                }
                if (dt.Rows.Count > 0)
                {
                    num *= 1.0 / ((double) dt.Rows.Count);
                }
                if (num > 0.0)
                {
                    num = 1.0 / num;
                }
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = num;
                }
            }
        }

        internal static string GetHarmonicAverageBaseColumn(string colName)
        {
            if (!colName.StartsWith("$HarmonicAvg_"))
            {
                return null;
            }
            return colName.Replace("$HarmonicAvg_", "");
        }

        public static string GetName(string baseColumnName)
        {
            return ("$HarmonicAvg_" + baseColumnName);
        }
    }
}

