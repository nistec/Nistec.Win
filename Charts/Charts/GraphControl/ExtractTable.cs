namespace Nistec.Charts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Web.UI;
    using System.Windows.Forms;

    public static class ExtractTable
    {
        public static DataTable Extract(IEnumerable data, List<string> columns)
        {
            DataTable table = new DataTable();
            foreach (string str in columns)
            {
                if (table.Columns[str] != null)
                {
                    continue;
                }
                try
                {
                    if (data.GetType() == typeof(DataView))
                    {
                        table.Columns.Add(str, ((DataView) data).Table.Columns[str].DataType);
                    }
                    else
                    {
                        table.Columns.Add(str, typeof(object));
                    }
                    continue;
                }
                catch
                {
                    table.Columns.Add(str, typeof(object));
                    continue;
                }
            }
            try
            {

                IEnumerator enumerator = data.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataRow row = table.NewRow();
                    foreach (string str2 in columns)
                    {
                        try
                        {
                            row[str2] = DataBinder.GetPropertyValue(enumerator.Current, str2, null);
                            continue;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Data could not be extracted from dataSource", exception);
            }
            return table;
        }

        public static DataTable Extract(CurrencyManager dataManager, List<string> columns)
        {
            DataTable table = new DataTable();
            foreach (string str in columns)
            {
                if (table.Columns[str] == null)
                {
                    table.Columns.Add(str, typeof(object));
                }
            }
            try
            {
                for (int i = 0; i < dataManager.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    GetDatRow(dataManager, i, columns, dr);
                    table.Rows.Add(dr);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Data could not be extracted from dataSource", exception);
            }
            return table;
        }

        private static void GetDatRow(CurrencyManager dataManager, int index, List<string> columns, DataRow dr)
        {
            object component = dataManager.List[index];
            PropertyDescriptorCollection itemProperties = dataManager.GetItemProperties();
            foreach (string str in columns)
            {
                PropertyDescriptor descriptor = null;
                descriptor = itemProperties.Find(str, false);
                if (descriptor != null)
                {
                    dr[str] = descriptor.GetValue(component);
                }
            }
        }
    }
}

