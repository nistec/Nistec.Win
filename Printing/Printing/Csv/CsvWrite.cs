namespace Nistec.Printing.Csv
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Nistec.Printing.Data;

    public class CsvWriter : AdoMap, IAdoWriter
    {


        public CsvWriter(CsvWriteProperties properties)
        {
            this.Properties = properties.Clone();// new CsvWriteProperties();
            base.Output = new AdoOutput("Records To Save", "Records successfully saved to the Text File.");//, true);
        }


        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            CsvWriteProperties properties = this.Properties as CsvWriteProperties;
            try
            {
                if (properties.Filename == "")
                {
                    base.CancelExecute("No filename specified");
                    return false;
                }
                if (base.Output == null)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                this.UpdateSchema();
                DataTable table = base.Output.Value as DataTable;
                //base.ExecuteBegin((uint)table.Rows.Count);
                long length = 0L;
                if (File.Exists(properties.Filename))
                {
                    FileInfo info = new FileInfo(properties.Filename);
                    length = info.Length;
                }
                bool append = properties.Append;
                //if (!append)
                //{
                //    append = (base.Parent as AdoMap).BatchCycle > 1;
                //}
                using (StreamWriter writer = new StreamWriter(properties.Filename, append, Encoding.GetEncoding(properties.Encoding)))
                {
                    if (((properties.FileType == FileTypes.Delimited) && properties.FirstRowHeaders) && (!append || (length == 0L)))
                    {
                        writer.Write(CsvHelper.CSVCreateHeader(table, properties));
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        if (base.StopProcessing)
                        {
                            return flag;
                        }
                        writer.Write(CsvHelper.FormatRow(row, properties));
                        writer.Write(CsvHelper.GetRecordSeparator(properties.RecordSeparator));
                        base.UpdateProcessing();
                    }
                    //base.ExecuteCommit();
                    return flag;
                }
            }
            catch (Exception exception)
            {
                flag = false;
                base.CancelExecute(exception.Message);
            }
            finally
            {
                //base.EndProcessing();
                //if (output != null)
                //{
                //    output.WriteLine(base.ObjectsProcessed + " records written.");
                //}
            }
            return flag;
        }

        public override AdoTable GetSchema()
        {
            CsvWriteProperties properties = this.Properties as CsvWriteProperties;
            if (properties.FileType == FileTypes.Fixed)
            {
                return properties.DataSource;
            }
            return null;
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader)
        {
            return Export(table, fileName, firstRowHeader, null);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            bool flag = true;
            CsvWriteProperties properties = new CsvWriteProperties();
            properties.Filename = fileName;
            properties.FirstRowHeaders = firstRowHeader;
            
            if (properties.Filename == "")
            {
                throw new ArgumentNullException("No filename specified");
            }
            if (table == null)
            {
                throw new ArgumentNullException("Inputs/Outputs are invalid");
            }
            properties.DataSource = AdoTable.CreateSchema(table,fields);
            bool append=properties.Append;
            using (StreamWriter writer = new StreamWriter(properties.Filename, properties.Append, Encoding.GetEncoding(properties.Encoding)))
            {
                if (((properties.FileType == FileTypes.Delimited) && properties.FirstRowHeaders))// && (!append || (length == 0L)))
                {
                    writer.Write(CsvHelper.CSVCreateHeader(table, properties));
                }
                foreach (DataRow row in table.Rows)
                {
                    writer.Write(CsvHelper.FormatRow(row, properties));
                    writer.Write(CsvHelper.GetRecordSeparator(properties.RecordSeparator));
                }
            }
            return flag;

        }

        #region write not used

        private static bool WriteCsv(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {

            if (fileName == "")
            {
                throw new ArgumentNullException("No filename specified");
            }
            if (table == null)
            {
                throw new ArgumentNullException("Inputs/Outputs are invalid");
            }

            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Default))
            {
                writer.Write(CSVCreateHeader(fields));
                foreach (DataRow row in table.Rows)
                {
                    writer.Write(CSVFormatRow(row, fields));
                    writer.Write("\r\n");
                }
            }
            return true;
        }

        private static string CSVCreateHeader(AdoField[] fields)
        {
            if ((fields == null) || (fields.Length == 0))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (AdoField column in fields)
            {
                if (builder.Length > 0)
                {
                    builder.Append(",");
                }
                builder.Append("\"");
                builder.Append(column.ToString());
                builder.Append("\"");
            }
            builder.Append("\r\n");
            return builder.ToString();
        }

        private static string CSVFormatRow(DataRow row, AdoField[] fields)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)//(DataColumn column in row.Table.Columns)
            {
                object val = row[fields[i].ColumnName];
                DataColumn col = row.Table.Columns[fields[i].ColumnName];

                if (i > 0)//(column != row.Table.Columns[0])
                {
                    builder.Append(",");
                }
                if (val != DBNull.Value)
                {
                    string str2 = col.DataType.Name;
                    if (str2 == null)
                    {
                        goto Label_0197;
                    }
                    if (!(str2 == "String"))
                    {
                        if (str2 == "DateTime")
                        {
                            goto Label_0132;
                        }
                        if (str2 == "Boolean")
                        {
                            goto Label_0156;
                        }
                        if (str2 == "Byte[]")
                        {
                            goto Label_0178;
                        }
                        goto Label_0197;
                    }
                    builder.Append("\"");
                    builder.Append(val.ToString().Replace("\"", "\"" + "\""));
                    builder.Append("\"");
                }
                continue;
            Label_0132:
                //DateTime time = (DateTime)val;// row[column];
                //builder.Append(time.ToString());
                builder.Append("\"");
                builder.Append(val.ToString().Replace("\"", "\"" + "\""));
                builder.Append("\"");
                continue;
            Label_0156:
                if ((bool)val)
                {
                    builder.Append(1);
                }
                else
                {
                    builder.Append(0);
                }
                continue;
            Label_0178:
                //builder.Append(Encoding.ASCII.GetString((byte[])val));
                builder.Append("\"");
                builder.Append(val.ToString().Replace("\"", "\"" + "\""));
                builder.Append("\"");
                continue;
            Label_0197:
                builder.Append(val.ToString());
            }
            return builder.ToString();
        }
        #endregion

    }
}

