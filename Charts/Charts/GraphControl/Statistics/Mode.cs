namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class Mode
    {
        public static void AddModeAverage(DataTable dt, string colName)
        {
            string str = GetModeAverageBaseColumn(colName);
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
                Dictionary<string, int> dictionary = new Dictionary<string, int>(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, int> dictionary2;
                    string str4;
                    if (!dictionary.ContainsKey(row[colName].ToString()))
                    {
                        dictionary.Add(row[colName].ToString(), 1);
                        continue;
                    }
                    (dictionary2 = dictionary)[str4 = row[colName].ToString()] = dictionary2[str4] + 1;
                }
                int num = dictionary[dt.Rows[0][colName].ToString()];
                foreach (string str2 in dictionary.Keys)
                {
                    if (num < dictionary[str2])
                    {
                        num = dictionary[str2];
                    }
                }
                double num2 = 0.0;
                int num3 = 0;
                foreach (string str3 in dictionary.Keys)
                {
                    if (dictionary[str3] == num)
                    {
                        num2 += Convert.ToDouble(str3);
                        num3++;
                    }
                }
                num2 /= (double) num3;
                foreach (DataRow row2 in dt.Rows)
                {
                    row2[colName] = num2;
                }
            }
        }

        internal static string GetModeAverageBaseColumn(string colName)
        {
            if (!colName.StartsWith("$Mode_"))
            {
                return null;
            }
            return colName.Replace("$Mode_", "");
        }

        public static string GetName(string baseColumnName)
        {
            return ("$Mode_" + baseColumnName);
        }
    }
}

