namespace Nistec.Printing.MSExcel.OleDb
{
    using Nistec.Printing;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;
    using Nistec.Printing.Data;

    public class ExcelHelper
    {
        private static string _lastLoadFolder = string.Empty;
        private static string _lastSaveFolder = string.Empty;

        private ExcelHelper()
        {
            
        }

        private static string OverrideIMEX(byte imex)
        {
            return imex > 0 ? "IMEX=" + imex.ToString() + ";" : "";
        }

        public static string BuildConnectionString(string fileName, bool firstRowHeaders, byte imex)
        {
            string fileEx = System.IO.Path.GetExtension(fileName).ToLower();

            //Use this one when you want to treat all data in the file as text, overriding Excels column type "General" to guess what type of data is in the column.
            //return ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0 Xml;IMEX=1;HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"");

            if (fileEx.EndsWith("xlsx"))
                return ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0 Xml;" + OverrideIMEX(imex) + "HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"");
            if (fileEx.EndsWith("xlsb"))
                return ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"");
            if (fileEx.EndsWith("xlsm"))
                return ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0 Macro;HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"");

            return ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;" + OverrideIMEX(imex) + "HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"");
        }

        private static string BuildConnectionString(ExcelReadProperties properties)
        {
            return BuildConnectionString(properties.Workbook, properties.FirstRowHeaders,properties.IMEX);

            //return ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + properties.Workbook + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=" + (properties.FirstRowHeaders ? "YES" : "NO") + "\"");
        }

        private static string CleanObjectName(string objectName)
        {
            return objectName.Replace("'", "").Replace("$", "");
        }

        //public static void TryConnection(string fileName,bool firstRowHeaders)
        //{
        //    string cnn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=" + (firstRowHeaders ? "YES" : "NO") + "\"";
        //    OleDbConnection connection = null;
        //    OleDbCommand command=new OleDbCommand();

        //    try
        //    {
        //        connection = new OleDbConnection(cnn);
        //        connection.Open();
        //                command.Connection = connection;
        //command.CommandText = "Create Table MySheet (F1 char(255), F2 char(255))";
        //                    command.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State== ConnectionState.Open)
        //            connection.Close();
        //    }

        //}

        private static string BuildConnectionString(ExcelWriteProperties properties)
        {
            //return ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + properties.Filename + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=" + (properties.FirstRowHeaders ? "YES" : "NO") + "\"");
            return ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + properties.Filename + ";Extended Properties=\"Excel 8.0;HDR=" + (properties.FirstRowHeaders ? "YES" : "NO") + ";\"");
        }
        public static OleDbConnection GetConnection(ExcelWriteProperties properties)
        {
            return new OleDbConnection(BuildConnectionString(properties));
        }

        public static bool CreateWorksheet(ExcelWriteProperties properties)
        {
            bool flag = false;
            OleDbConnection connection = GetConnection(properties);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = string.Format("Create Table {0} ({1})", properties.DataSource.TableName, properties.GetColumnRange());
                //command.CommandText = "Create Table NewWorksheet (ID char(255),Subject char(255),Status char(255),StartDate char(255),DueDate char(255),DateCompleted char(255),ActualWork char(255),Category char(255),Sender char(255),UserHandle char(255),F11 char(255))";
                command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        public static OleDbConnection GetConnection(ExcelReadProperties properties)
        {
            return new OleDbConnection(BuildConnectionString(properties));
        }

        public static string FormatRow(DataRow row, ExcelWriteProperties properties)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataColumn column in row.Table.Columns)
            {
                //DateTime time;
                if (column != row.Table.Columns[0])
                {
                    builder.Append(",");
                }
                if (row[column] == DBNull.Value)
                {
                    builder.Append("''");
                    continue;
                }

                string str2 = column.DataType.Name;
                if (str2 == null)
                {
                    builder.Append(row[column]);
                    continue;
                    //goto Label_0197;
                }
                if (WinHelp.IsNumber(row[column]))
                {
                    builder.Append(row[column]);
                    continue;
                    //goto Label_0197;
                }

                //////////////////////////

                switch (str2)
                {
                    case "DateTime":
                        builder.AppendFormat("#{0}#",((DateTime)row[column]).ToString(properties.DateFormat));
                        break;
                    case "Boolean":
                        builder.Append(((bool)row[column]) ? 1 : 0);
                        break;
                    case "Byte[]":
                        builder.Append(Encoding.ASCII.GetString((byte[])row[column]));
                        break;
                    case "String":
                        builder.AppendFormat("'{0}'", row[column].ToString().Replace("'", "''"));
                        break;
                    default:
                        builder.Append(row[column]);
                        break;
                }

                //////////////////////////////////


                //else if (!(str2 == "String"))
                //{
                //    if (str2 == "DateTime")
                //    {
                //        goto Label_0132;
                //    }
                //    if (str2 == "Boolean")
                //    {
                //        goto Label_0156;
                //    }
                //    if (str2 == "Byte[]")
                //    {
                //        goto Label_0178;
                //    }
                //    goto Label_0197;
                //}
                //builder.AppendFormat("'{0}'", row[column].ToString().Replace("'","''"));
                //    }
                //    else
                //    {
                //        builder.Append("''");
                //    }
                //    continue;
                //Label_0132:
                //    time = (DateTime)row[column];
                //    builder.Append(time.ToString(properties.DateFormat));
                //    continue;
                //Label_0156:
                //    if ((bool)row[column])
                //    {
                //        builder.Append(1);
                //    }
                //    else
                //    {
                //        builder.Append(0);
                //    }
                //    continue;
                //Label_0178:
                //    builder.Append(Encoding.ASCII.GetString((byte[])row[column]));
                //    continue;
                //Label_0197:
                //    builder.Append(row[column].ToString());
            }
            return builder.ToString();
        }

        public static string GetFirstWorksheet(ExcelReadProperties properties)
        {
            OleDbConnection connection = GetConnection(properties);
            try
            {
                connection.Open();
                return GetFirstWorksheet(connection);
            }
            catch
            {
                return "";
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        public static string GetFirstWorksheet(OleDbConnection connection)
        {
            return Types.NZ(connection.GetSchema("Tables").Rows[0]["TABLE_NAME"], "Sheet1$");
        }

        public static string[] GetNamedRanges(ExcelReadProperties properties)
        {
            OleDbConnection connection = GetConnection(properties);
            try
            {
                connection.Open();
            }
            catch
            {
                return new string[0];
            }
            ArrayList list = new ArrayList();
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                try
                {
                    DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                    {
                        string str = oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString();
                        if (!str.EndsWith("$'") && !str.EndsWith("$"))
                        {
                            list.Add(oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString());
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            connection.Close();
            return (string[]) list.ToArray(typeof(string));
        }

        public static DataTable GetPreviewRows(ExcelReadProperties properties)
        {
            OleDbConnection connection = GetConnection(properties);
            try
            {
                connection.Open();
            }
            catch
            {
                return null;
            }
            OleDbCommand command = new OleDbCommand(GetSelectSQL(properties), connection);
            DataTable previewRecords = DataHelper.GetPreviewRecords(command);
            connection.Close();
            return previewRecords;
        }

        public static DataTable GetRows(ExcelReadProperties properties)
        {
            OleDbConnection connection = GetConnection(properties);
            try
            {
                connection.Open();
            }
            catch
            {
                return null;
            }
            OleDbCommand selectCommand = new OleDbCommand(GetSelectSQL(properties), connection);
            DataTable table = null;
            try
            {
                DataSet dataSet = new DataSet();
                new OleDbDataAdapter(selectCommand).Fill(dataSet);
                if (dataSet.Tables.Count > 0)
                {
                    table = dataSet.Tables[0];
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return table;
        }
        
        public static DataSet Read(string fileName,bool firstRowHeader,uint maxRows,byte imex)
        {
            DataSet dataSet = new DataSet();
            OleDbConnection connection = new OleDbConnection(BuildConnectionString(fileName, firstRowHeader, imex));
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                return null;
            }

            string[] worksheets = GetWorksheets(connection, false);

            //OleDbCommand selectCommand = new OleDbCommand();
            try
            {
                //selectCommand.Connection = connection;
                //selectCommand.CommandType = CommandType.Text;
                //StringBuilder sb=new StringBuilder();
                foreach (string worksheet in worksheets)
                {
                    //sb.Append(GetSelectSQL(worksheet,maxRows));
                    //sb.Append(";");

                    string sql=GetSelectSQL(worksheet, maxRows)+";";
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);
                    da.Fill(dataSet, worksheet);
                }

                //selectCommand.CommandText = sb.ToString();
                //OleDbDataAdapter da = new OleDbDataAdapter(sb.ToString(),connection);
                //da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return dataSet;
        }

       

        public static string GetSelectSQL(ExcelReadProperties properties)
        {
            StringBuilder builder = new StringBuilder();
            if (properties.QueryType == QueryType.Worksheet)
            {
                builder.Append("SELECT * FROM ");
                builder.Append("[" + properties.Worksheet + "$");
                if (properties.IsRange)
                {
                    builder.Append(properties.Range);
                }
                builder.Append("]");
            }
            else if (properties.QueryType == QueryType.NamedRange)
            {
                builder.Append("SELECT * FROM [" + properties.NamedRange + "]");
            }
            else
            {
                builder.Append(properties.Query);
            }
            return builder.ToString();
        }

        public static string GetSelectSQL(string worksheet, uint maxRows)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT {0}* FROM ", maxRows == 0 ? "" : "TOP " + maxRows.ToString() + " ");
            if (worksheet.EndsWith("$"))
                builder.Append("[" + worksheet + "]");
            else
                builder.Append("[" + worksheet + "$]" );
            return builder.ToString();
        }

        public static string GetInsertSQL(DataRow row, ExcelWriteProperties properties)
        {
            StringBuilder builder = new StringBuilder();
            //if (properties.QueryType == QueryType.Worksheet)
            //{
                builder.Append("INSERT INTO ");
                builder.Append("[" + properties.DataSource.TableName + "$]");
                builder.AppendFormat("({0})",properties.GetFieldsRange());
                builder.AppendFormat("VALUES ({0})",ExcelHelper.FormatRow(row, properties));

            //    if (properties.IsRange)
            //    {
            //        builder.Append(properties.Range);
            //    }
            //    builder.Append("]");
            //}
            //else if (properties.QueryType == QueryType.NamedRange)
            //{
            //    builder.Append("SELECT * FROM [" + properties.NamedRange + "]");
            //}
            //else
            //{
            //    builder.Append(properties.Query);
            //}
            return builder.ToString();
        }

        public static string[] GetWorksheets(ExcelReadProperties properties)
        {
            return GetWorksheets(GetConnection(properties),true);
        }
        public static string[] GetWorksheets(ExcelWriteProperties properties)
        {
            return GetWorksheets(GetConnection(properties),true);
        }
        public static string[] GetWorksheets(OleDbConnection connection,bool closeConnectin)
        {
            try
            {
                if(closeConnectin)
                connection.Open();
            }
            catch
            {
                return new string[0];
            }
            ArrayList list = new ArrayList();
            if ((connection != null) && (connection.State == ConnectionState.Open))
            {
                try
                {
                    DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                    {
                        string str = oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString();
                        if (str.EndsWith("$'") || str.EndsWith("$"))
                        {
                            list.Add(CleanObjectName(oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString()));
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            if (closeConnectin)
                connection.Close();
            return (string[]) list.ToArray(typeof(string));
        }

        public static string ValidateQuery(ExcelReadProperties properties)
        {
            OleDbConnection connection = GetConnection(properties);
            OleDbCommand command = new OleDbCommand(GetSelectSQL(properties), connection);
            return DataHelper.ValidateRecordSource(command);
        }

        public static string LastLoadFolder
        {
            get
            {
                return _lastLoadFolder;
            }
            set
            {
                _lastLoadFolder = value;
            }
        }

        public static string LastSaveFolder
        {
            get
            {
                return _lastSaveFolder;
            }
            set
            {
                _lastSaveFolder = value;
            }
        }
    }
}

