namespace Nistec.Charts.Statistics
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class RandomData
    {
        public static void AddRandomData(DataTable dt, string colName)
        {
            int max = 0x29a;
            if (GetRandomDataBaseColumn(colName, out max) != null)
            {
                if (dt.Columns[colName] == null)
                {
                    dt.Columns.Add(colName, typeof(decimal));
                }
                Random random = new Random();
                foreach (DataRow row in dt.Rows)
                {
                    row[colName] = random.NextDouble() * max;
                }
            }
        }

        public static string GetName(int max)
        {
            return ("$Random_" + max);
        }

        internal static string GetRandomDataBaseColumn(string colName, out int max)
        {
            max = 0x29a;
            if (!colName.StartsWith("$Random_"))
            {
                return null;
            }
            string s = colName.Replace("$Random_", "");
            try
            {
                max = int.Parse(s);
            }
            catch
            {
                return null;
            }
            return "RandomData";
        }
    }
}

