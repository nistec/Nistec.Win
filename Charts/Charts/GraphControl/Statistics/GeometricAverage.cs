namespace Nistec.Charts.Statistics
{
    using System;
    using System.Data;

    public static class GeometricAverage
    {
        public static void AddGeometricAverage(DataTable dt, string colName)
        {
            string str = GetGeometricAverageBaseColumn(colName);
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
                double x = 1.0;
                foreach (DataRow row in dt.Rows)
                {
                    x *= Convert.ToDouble(row[str]);
                }
                x = Math.Pow(x, 1.0 / ((double) dt.Rows.Count));
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = x;
                }
            }
        }

        internal static string GetGeometricAverageBaseColumn(string colName)
        {
            if (!colName.StartsWith("$GeometricAvg_"))
            {
                return null;
            }
            return colName.Replace("$GeometricAvg_", "");
        }

        public static string GetName(string baseColumnName)
        {
            return ("$GeometricAvg_" + baseColumnName);
        }
    }
}

