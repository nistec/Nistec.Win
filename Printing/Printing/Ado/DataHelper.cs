namespace Nistec.Printing.Data
{
    //using ADOX;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;

    public class DataHelper
    {
        public static void CreateDataColumnsFromFirstRow(DataTable dt)
        {

            if (dt == null)
            {
                return;
            }
            //dt.Columns.Clear();
            int index = 1;
            DataRow dr = dt.Rows[0];
            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                string colName = dr[i].ToString();
                if (!string.IsNullOrEmpty(colName))
                {
                    if (dt.Columns.Contains(colName))
                    {
                        colName += "_" + index.ToString();
                        index++;
                    }
                    dt.Columns[i].ColumnName = colName;
                }
            }
            dt.Rows[0].Delete();
            dt.AcceptChanges();
            //return dt;
        }

        //private static List<char> _validCharacters = new List<char>(new char[] { 
        //    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 
        //    'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
        //    'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 
        //    'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '\x00c0', '\x00c2', 
        //    '\x00c3', '\x00c4', '\x00c5', '\x00c6', '\x00c7', '\x00c8', '\x00c9', '\x00ca', '\x00cb', '\x00cc', '\x00cd', '\x00ce', '\x00cf', '\x00d0', '\x00d1', '\x00d2', 
        //    '\x00d3', '\x00d4', '\x00d5', '\x00d6', '\x00d7', '\x00d8', '\x00d9', '\x00da', '\x00db', '\x00dc', '\x00dd', '\x00de', '\x00df', '\x00e0', '\x00e1', '\x00e2', 
        //    '\x00e3', '\x00e4', '\x00e5', '\x00e6', '\x00e7', '\x00e8', '\x00e9', '\x00ea', '\x00eb', '\x00ec', '\x00ed', '\x00ee', '\x00ef', '\x00f0', '\x00f1', '\x00f2', 
        //    '\x00f3', '\x00f4', '\x00f5', '\x00f6', '\x00f8', '\x00f9', '\x00fa', '\x00fb', '\x00fc', '\x00fd', '\x00fe', '\x00ff', 'Ā', 'ā', 'Ă', 'ă', 
        //    'Ą', 'ą', 'Ć', 'ć', 'Č', 'č', 'Đ', 'đ', 'Ď', 'ď', 'Ē', 'ē', 'ĕ', 'Ė', 'ė', 'Ę', 
        //    'ę', 'Ě', 'ě', 'Ģ', 'ģ', 'Ī', 'ī', 'Į', 'į', 'Ķ', 'ķ', 'Ĺ', 'ĺ', 'Ļ', 'ļ', 'Ľ', 
        //    'ľ', 'Ł', 'ł', 'Ń', 'ń', 'Ņ', 'ņ', 'Ň', 'ň', 'Ō', 'ō', 'Ő', 'ő', '\x00d3', '\x00f3', 'Œ', 
        //    'œ', 'ŕ', 'Ŗ', 'ŗ', 'Ř', 'ř', 'Ş', 'ş', 'Ś', 'ś', 'Š', 'š', 'Ţ', 'ţ', 'ť', 'Ű', 
        //    'ű', 'Ų', 'ų', 'Ÿ', 'Ź', 'ź', 'Ż', 'ż', 'Ž', 'ž', '_', '#', '/'
        // });

        public static string[] AvailableDatabases(OleDbConnection connection)
        {
            ArrayList list = new ArrayList();
            bool flag = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    flag = true;
                    connection.Open();
                }
                foreach (DataRow row in connection.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, new object[1]).Rows)
                {
                    list.Add(row["CATALOG_NAME"].ToString());
                }
            }
            finally
            {
                if (flag && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return (string[]) list.ToArray(typeof(string));
        }

        public static string[] AvailableStoredProcedures(OleDbConnection connection)
        {
            ArrayList list = new ArrayList();
            bool flag = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    flag = true;
                    connection.Open();
                }
                object[] restrictions = new object[4];
                restrictions[3] = "VIEW";
                foreach (DataRow row in connection.GetOleDbSchemaTable(OleDbSchemaGuid.Procedures, restrictions).Rows)
                {
                    list.Add(row["TABLE_NAME"].ToString());
                }
            }
            finally
            {
                if (flag && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return (string[]) list.ToArray(typeof(string));
        }


        public static string[] AvailableTables(System.Data.SqlClient.SqlConnection connection)
        {
            OleDbConnection connection2 = new OleDbConnection();
            connection2.ConnectionString = "Provider=SQLOLEDB;" + connection.ConnectionString;
            return DataHelper.AvailableTables(connection2);
        }

        public static string[] AvailableTables(OleDbConnection connection)
        {
            ArrayList list = new ArrayList();
            bool flag = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    flag = true;
                    connection.Open();
                }
                object[] restrictions = new object[4];
                restrictions[3] = "TABLE";
                foreach (DataRow row in connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions).Rows)
                {
                    string str = row["TABLE_SCHEMA"].ToString();
                    list.Add(((str == string.Empty) ? "" : (str + ".")) + row["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                if (flag && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return (string[]) list.ToArray(typeof(string));
        }

        public static string[] AvailableViews(OleDbConnection connection)
        {
            ArrayList list = new ArrayList();
            bool flag = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    flag = true;
                    connection.Open();
                }
                object[] restrictions = new object[4];
                restrictions[3] = "VIEW";
                foreach (DataRow row in connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions).Rows)
                {
                    string str = row["TABLE_SCHEMA"].ToString();
                    list.Add(((str == string.Empty) ? "" : (str + ".")) + row["TABLE_NAME"].ToString());
                }
            }
            finally
            {
                if (flag && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return (string[]) list.ToArray(typeof(string));
        }

        public static string BracketName(string name)
        {
            string[] strArray = name.Split(new char[] { '.' });
            name = string.Empty;
            foreach (string str in strArray)
            {
                name = name + ((name == string.Empty) ? "" : ".") + "[" + str + "]";
            }
            return name;
        }

         
        //public static string BuildAlterTableSQL(DataTable schema, string tableName)
        //{
            
        //}

        public static string BuildCreateTableSQL(DataTable schema, string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("CREATE TABLE [{0}]", tableName + Environment.NewLine);
            builder.Append("(" + Environment.NewLine);
            int length = 0;
            foreach (DataColumn column in schema.Columns)
            {
                if (column.ColumnName.Length > length)
                {
                    length = column.ColumnName.Length;
                }
            }
            int num2 = 0;
            foreach (DataColumn column2 in schema.Columns)
            {
                num2++;
                builder.Append("   " + column2.ColumnName.PadRight(length + 3, ' '));
                string str = "VARCHAR(255)";
                switch (column2.DataType.Name)
                {
                    case "String":
                        if (column2.MaxLength > 0xff)
                        {
                            break;
                        }
                        str = "VARCHAR(" + column2.MaxLength + ")";
                        goto Label_024F;

                    case "Boolean":
                        str = "BIT";
                        goto Label_024F;

                    case "DateTime":
                        str = "DATETIME";
                        goto Label_024F;

                    case "Double":
                        str = "DOUBLE PRECISION";
                        goto Label_024F;

                    case "Single":
                        str = "REAL";
                        goto Label_024F;

                    case "Decimal":
                        str = "DECIMAL(18,2)";
                        goto Label_024F;

                    case "Int16":
                        str = "SMALLINT";
                        goto Label_024F;

                    case "Int32":
                        str = "INT";
                        goto Label_024F;

                    case "Int64":
                        str = "BIGINT";
                        goto Label_024F;

                    case "Byte[]":
                        str = "IMAGE";
                        goto Label_024F;

                    default:
                        goto Label_024F;
                }
                str = "TEXT";
            Label_024F:
                builder.Append(str.PadRight(0x10, ' '));
                if (column2.AllowDBNull)
                {
                    builder.Append(" NULL");
                }
                else
                {
                    builder.Append(" NOT NULL");
                }
                if (column2.Unique)
                {
                    builder.Append(" UNIQUE");
                }
                if (num2 == schema.Columns.Count)
                {
                    if (schema.PrimaryKey.Length > 0)
                    {
                        builder.Append(",");
                    }
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append(Environment.NewLine);
            }
            
            if (schema.PrimaryKey.Length > 0)
            {
                builder.Append("   PRIMARY KEY (");
                int num3 = 0;
                foreach (DataColumn column3 in schema.PrimaryKey)
                {
                    if (num3++ > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(column3.ColumnName);
                }
                builder.Append(")");
                builder.Append(Environment.NewLine);
            }
            builder.Append(")" + Environment.NewLine);
            return builder.ToString();
        }

        public static string BuildDeleteTableSQL(string tableName)
        {
            return ("DROP TABLE " + tableName + Environment.NewLine);
        }

        public static string BuildDeleteTableSQL(string tableName, bool bracketNames)
        {
            if (bracketNames)
            {
                return ("DROP TABLE " + BracketName(tableName) + Environment.NewLine);
            }
            return ("DROP TABLE " + tableName + Environment.NewLine);
        }

        public static OleDbCommand BuildInsertSQL(AdoTable table)
        {
            return BuildInsertSQL(table, null, true);
        }

        public static OleDbCommand BuildInsertSQL(AdoTable table, OleDbConnection connection)
        {
            return BuildInsertSQL(table, connection, true);
        }

        public static OleDbCommand BuildInsertSQL(AdoTable table, OleDbConnection connection, bool bracketNames)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            StringBuilder builder = new StringBuilder();
            if (bracketNames)
            {
                builder.Append("INSERT INTO " + BracketName(table.TableName) + " (");
            }
            else
            {
                builder.Append("INSERT INTO " + table.TableName + " (");
            }
            for (int i = 1; i <= table.Columns.Count; i++)
            {
                if (bracketNames)
                {
                    builder.Append("[" + table.Columns[i - 1].ColumnName + "]");
                }
                else
                {
                    builder.Append(table.Columns[i - 1].ColumnName);
                }
                if (i < table.Columns.Count)
                {
                    builder.Append(", ");
                }
            }
            builder.Append(Environment.NewLine);
            builder.Append(") VALUES (");
            for (int j = 1; j <= table.Columns.Count; j++)
            {
                builder.Append("?");
                if (j < table.Columns.Count)
                {
                    builder.Append(", ");
                }
            }
            builder.Append(")");
            command.CommandText = builder.ToString();
            foreach (AdoColumn column in table.Columns)
            {
                command.Parameters.AddWithValue("@" + ValidName(column.ColumnName), DBNull.Value);
            }
            return command;
        }

        public static OleDbCommand BuildSelectSQL(AdoTable table, bool allRows)
        {
            return BuildSelectSQL(table, allRows, null, true);
        }

        public static OleDbCommand BuildSelectSQL(AdoTable table, bool allRows, OleDbConnection connection)
        {
            return BuildSelectSQL(table, allRows, connection, true);
        }

        public static OleDbCommand BuildSelectSQL(AdoTable table, bool allRows, OleDbConnection connection, bool bracketNames)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (table.Columns.Count == 0)
            {
                if (builder.ToString() == "SELECT ")
                {
                    builder.Append("*");
                }
            }
            else
            {
                for (int i = 1; i <= table.Columns.Count; i++)
                {
                    if (bracketNames)
                    {
                        builder.Append("[" + table.Columns[i - 1].ColumnName + "]");
                    }
                    else
                    {
                        builder.Append(table.Columns[i - 1].ColumnName);
                    }
                    if (i < table.Columns.Count)
                    {
                        builder.Append(", ");
                    }
                }
            }
            builder.Append(Environment.NewLine);
            if (bracketNames)
            {
                builder.Append(" FROM " + BracketName(table.TableName));
            }
            else
            {
                builder.Append(" FROM " + table.TableName);
            }
            builder.Append(Environment.NewLine);
            if ((table.KeyColumns.Count > 0) && !allRows)
            {
                builder.Append(" WHERE ");
                for (int j = 1; j <= table.KeyColumns.Count; j++)
                {
                    if (bracketNames)
                    {
                        builder.Append("[" + table.KeyColumns[j - 1].ColumnName + "] = ?");
                    }
                    else
                    {
                        builder.Append(table.KeyColumns[j - 1].ColumnName + " = ?");
                    }
                    if (j < table.KeyColumns.Count)
                    {
                        builder.Append(" AND ");
                    }
                }
            }
            command.CommandText = builder.ToString();
            if ((table.KeyColumns.Count > 0) && !allRows)
            {
                foreach (AdoColumn column in table.KeyColumns)
                {
                    command.Parameters.AddWithValue("@" + ValidName(column.ColumnName), DBNull.Value);
                }
            }
            return command;
        }

        public static OleDbCommand BuildUpdateSQL(AdoTable table)
        {
            return BuildUpdateSQL(table, null, true);
        }

        public static OleDbCommand BuildUpdateSQL(AdoTable table, OleDbConnection connection)
        {
            return BuildUpdateSQL(table, connection, true);
        }

        public static OleDbCommand BuildUpdateSQL(AdoTable table, OleDbConnection connection, bool bracketNames)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            StringBuilder builder = new StringBuilder();
            if (bracketNames)
            {
                builder.Append("UPDATE " + BracketName(table.TableName) + " SET ");
            }
            else
            {
                builder.Append("UPDATE " + table.TableName + " SET ");
            }
            builder.Append(Environment.NewLine);
            for (int i = 1; i <= table.Columns.Count; i++)
            {
                if (!table.Columns[i - 1].IsKey)
                {
                    if (bracketNames)
                    {
                        builder.Append("[" + table.Columns[i - 1].ColumnName + "] = ?");
                    }
                    else
                    {
                        builder.Append(table.Columns[i - 1].ColumnName + " = ?");
                    }
                    if (i < table.Columns.Count)
                    {
                        builder.Append(", ");
                    }
                }
            }
            builder.Append(Environment.NewLine);
            if (table.KeyColumns.Count > 0)
            {
                builder.Append(" WHERE ");
                for (int k = 1; k <= table.KeyColumns.Count; k++)
                {
                    if (bracketNames)
                    {
                        builder.Append("[" + table.KeyColumns[k - 1].ColumnName + "] = ?");
                    }
                    else
                    {
                        builder.Append(table.KeyColumns[k - 1].ColumnName + " = ?");
                    }
                    if (k < table.KeyColumns.Count)
                    {
                        builder.Append(" AND ");
                    }
                }
            }
            command.CommandText = builder.ToString();
            for (int j = 1; j <= table.Columns.Count; j++)
            {
                if (!table.Columns[j - 1].IsKey)
                {
                    command.Parameters.AddWithValue("@" + ValidName(table.Columns[j - 1].ColumnName, false), DBNull.Value);
                }
            }
            foreach (AdoColumn column in table.KeyColumns)
            {
                command.Parameters.AddWithValue("@" + ValidName(column.ColumnName, false), DBNull.Value);
            }
            return command;
        }

        public static object CastValue(object value, Type dataType)
        {
            try
            {
                return Convert.ChangeType(value, dataType);
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static DataColumn CopyColumn(DataColumn column)
        {
            DataColumn column2 = new DataColumn();
            column2.ColumnName = column.ColumnName;
            column2.DataType = column.DataType;
            column2.AllowDBNull = column.AllowDBNull;
            column2.DefaultValue = column.DefaultValue;
            if (column.DataType.FullName == "System.String")
            {
                column2.MaxLength = column.MaxLength;
            }
            column2.Unique = column.Unique;
            return column2;
        }

        public static void CopyTableSchema(DataTable source, DataTable target)
        {
            foreach (DataColumn column in source.Columns)
            {
                target.Columns.Add(CopyColumn(column));
            }
            ArrayList list = new ArrayList();
            foreach (DataColumn column2 in source.PrimaryKey)
            {
                list.Add(target.Columns[column2.ColumnName]);
            }
            if (list.Count > 0)
            {
                target.PrimaryKey = (DataColumn[]) list.ToArray(typeof(DataColumn));
            }
        }

        public static int CountRows(OleDbCommand selectCmd)
        {
            if ((selectCmd == null) || (selectCmd.Connection == null))
            {
                return 0;
            }
            int startIndex = 0;
            int num2 = 0;
            string commandText = selectCmd.CommandText;
            if (commandText.Trim() == "")
            {
                return 0;
            }
            if (!commandText.Trim().ToLower().StartsWith("select"))
            {
                return 0;
            }
            startIndex = commandText.Trim().ToLower().IndexOf(" from ");
            if (startIndex == -1)
            {
                return 0;
            }
            string str2 = selectCmd.CommandText;
            selectCmd.CommandText = "SELECT Count(*) AS DATAROWCOUNT " + commandText.Substring(startIndex);
            bool flag = false;
            OleDbDataReader reader = null;
            try
            {
                if (selectCmd.Connection.State == ConnectionState.Closed)
                {
                    selectCmd.Connection.Open();
                    flag = true;
                }
                reader = selectCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    num2 = Convert.ToInt32(reader.GetValue(0));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (flag)
                {
                    selectCmd.Connection.Close();
                }
                selectCmd.CommandText = str2;
            }
            return num2;
        }

        public static DataTable CreateDataTable(OleDbCommand selectCmd, string name)
        {
            if ((name == null) || (name == ""))
            {
                name = "NewTable";
            }
            DataTable table = new DataTable(name);
            if (selectCmd != null)
            {
                OleDbDataReader reader = selectCmd.Clone().ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);
                DataTable schemaTable = reader.GetSchemaTable();
                int num = 0;
                string columnName = "";
                ArrayList list = new ArrayList();
                foreach (DataRow row in schemaTable.Rows)
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = ValidName(row["ColumnName"].ToString());
                    if (column.ColumnName != columnName)
                    {
                        num = 0;
                        columnName = column.ColumnName;
                    }
                    while (table.Columns.Contains(columnName + ((num == 0) ? "" : num.ToString())))
                    {
                        num++;
                    }
                    column.ColumnName = columnName + ((num == 0) ? "" : num.ToString());
                    column.DataType = (Type) row["DataType"];
                    if (column.DataType == typeof(string))
                    {
                        column.MaxLength = (int) row["ColumnSize"];
                    }
                    column.AllowDBNull = (bool) row["AllowDBNull"];
                    column.ReadOnly = (bool) row["IsReadOnly"];
                    column.Unique = (bool) row["IsUnique"];
                    column.AutoIncrement = (bool) row["IsAutoIncrement"];
                    if ((bool) row["IsKey"])
                    {
                        list.Add(column);
                    }
                    table.Columns.Add(column);
                }
                table.PrimaryKey = (DataColumn[]) list.ToArray(typeof(DataColumn));
                reader.Close();
            }
            return table;
        }

        public static AdoTable CreateSchema(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            AdoTable table2 = new AdoTable();
            foreach (DataColumn column in table.Columns)
            {
                AdoColumn column2 = new AdoColumn(column.ColumnName, column.DataType, column.MaxLength);
                table2.Columns.Add(column2);
            }
            foreach (DataColumn column3 in table.PrimaryKey)
            {
                table2.Columns[column3.ColumnName].IsKey = true;
            }
            return table2;
        }

        public static DataTable CreateTable(AdoTable schema, string name)
        {
            if (schema == null)
            {
                return null;
            }
            DataTable table = new DataTable(name);
            ArrayList list = new ArrayList();
            foreach (AdoColumn column in schema.Columns)
            {
                string str = column.ColumnName;
                if (table.Columns.Contains(str))
                {
                    int num = 1;
                    while (table.Columns.Contains(str + num))
                    {
                        num++;
                    }
                    str = str + num;
                }
                DataColumn column2 = new DataColumn(str, column.DataType);
                if (column.DataType == typeof(string))
                {
                    column2.MaxLength = column.Length;
                }
                table.Columns.Add(column2);
                if (column.IsKey)
                {
                    list.Add(column2);
                }
            }
            if (list.Count > 0)
            {
                table.PrimaryKey = (DataColumn[]) list.ToArray(typeof(DataColumn));
            }
            return table;
        }

        public static DataTable CreateTable(DataTable table, string name)
        {
            if (table == null)
            {
                return null;
            }
            DataTable target = new DataTable(name);
            CopyTableSchema(table, target);
            return target;
        }

        public static DataTable CreateTable(DataTable table, string name, DataColumn preceedingColumn)
        {
            if ((table == null) || (preceedingColumn == null))
            {
                return null;
            }
            DataTable target = new DataTable(name);
            if (table.Columns.Contains(preceedingColumn.ColumnName))
            {
                int num = 0;
                while (table.Columns.Contains(preceedingColumn.ColumnName + ++num))
                {
                }
                target.Columns.Add(preceedingColumn.ColumnName + num);
            }
            else
            {
                target.Columns.Add(preceedingColumn);
            }
            CopyTableSchema(table, target);
            return target;
        }

        public static string FormatRowError(DataRow row)
        {
            if (!row.HasErrors || (row.Table == null))
            {
                return string.Empty;
            }
            string str = string.Empty;
            string str2 = "record " + Convert.ToString((int) (row.Table.Rows.IndexOf(row) + 1));
            if (row.Table.PrimaryKey.Length > 0)
            {
                foreach (DataColumn column in row.Table.PrimaryKey)
                {
                    str = str + ((str == string.Empty) ? string.Empty : ", ") + ((row[column.ColumnName] == DBNull.Value) ? "(null)" : row[column.ColumnName].ToString());
                }
                str = "key " + str;
            }
            if (str == string.Empty)
            {
                return string.Format("   warning: {0} ({1})", row.RowError, str2);
            }
            return string.Format("   warning: {0} ({1}, {2})", row.RowError, str2, str);
        }

        public static DataTable GetPreviewRecords(OleDbCommand command)
        {
            DataSet dataSet = new DataSet();
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                adapter.FillSchema(dataSet, SchemaType.Source, "preview");
                adapter.Fill(dataSet, 0, 50, "preview");
            }
            catch (Exception)
            {
                return null;
            }
            if (dataSet.Tables.Contains("preview"))
            {
                return dataSet.Tables["preview"];
            }
            return null;
        }

        public static bool IsValidName(string name)
        {
            foreach (char ch in name)
            {
                if (!IsValidCharacter(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidCharacter(char ch)
        {
            if ((((ch < '0') || (ch > '9')) && ((ch < 'a') || (ch > 'z'))) && (((ch < 'A') || (ch > 'Z')) && (((ch != '_') && (ch != '-')) && (ch == ' '))))
            {
                return false;
            }
            return true;
        }

        public static string ValidName(string name)
        {
            return ValidName(name, true);
        }

        public static string ValidName(string name, bool allowSpaces)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in name)
            {
                if (allowSpaces)
                {
                    if (IsValidCharacter(ch) && !(ch == ' '))
                    {
                        builder.Append(ch);
                    }
                }
                else if (IsValidCharacter(ch))
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        //public static string SafeName(string name)
        //{
        //    return SafeName(name, true);
        //}

        //public static string SafeName(string name, bool allowSpaces)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    foreach (char ch in name)
        //    {
        //        if (allowSpaces)
        //        {
        //            if ((_validCharacters.Contains(ch) || (ch == '-')) || (ch == ' '))
        //            {
        //                builder.Append(ch);
        //            }
        //        }
        //        else if (_validCharacters.Contains(ch) || (ch == '-'))
        //        {
        //            builder.Append(ch);
        //        }
        //    }
        //    return builder.ToString();
        //}

        public static bool TableExists(System.Data.SqlClient.SqlConnection connection, string table)
        {

            foreach (object obj2 in AvailableTables(connection))
            {
                if (obj2.ToString() == table)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TableExists(OleDbConnection connection, string table)
        {
            foreach (object obj2 in AvailableTables(connection))
            {
                if (obj2.ToString() == table)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TableHasPrimaryKey(OleDbConnection connection, string table)
        {
            bool flag = false;
            bool flag2 = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    flag2 = true;
                    connection.Open();
                }
                DataSet dataSet = new DataSet();
                OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM [" + table + "]", connection);
                new OleDbDataAdapter(selectCommand).FillSchema(dataSet, SchemaType.Source);
                flag = dataSet.Tables[0].PrimaryKey.Length > 0;
                dataSet.Tables.Clear();
                dataSet.Dispose();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (((connection != null) && flag2) && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return flag;
        }

        public static bool TableIsSubset(DataTable fullTable, DataTable subTable)
        {
            int num = 0;
            foreach (DataColumn column in subTable.Columns)
            {
                foreach (DataColumn column2 in fullTable.Columns)
                {
                    if ((column2.ColumnName == column.ColumnName) && (column2.DataType == column.DataType))
                    {
                        num++;
                        break;
                    }
                }
            }
            return (num == subTable.Columns.Count);
        }

        public static bool TablesMatch(DataTable tableA, DataTable tableB)
        {
            if (tableA.Columns.Count != tableB.Columns.Count)
            {
                return false;
            }
            int num = 0;
            foreach (DataColumn column in tableA.Columns)
            {
                foreach (DataColumn column2 in tableB.Columns)
                {
                    if (((column.ColumnName == column2.ColumnName) && (column.DataType == column2.DataType)) && (column.AllowDBNull == column2.AllowDBNull))
                    {
                        num++;
                        break;
                    }
                }
            }
            return (num == tableB.Columns.Count);
        }

        public static void UpdateTableSchema(DataTable table, AdoTable schema)
        {
            for (int i = table.Columns.Count - 1; i > -1; i--)
            {
                if (!schema.Columns.Contains(table.Columns[i].ColumnName))
                {
                    table.Columns.RemoveAt(i);
                }
            }
            foreach (AdoColumn column in schema.Columns)
            {
                if (table.Columns.IndexOf(column.ColumnName) > -1)
                {
                    if (table.Columns[column.ColumnName].DataType != column.DataType)
                    {
                        ArrayList list = new ArrayList();
                        foreach (DataRow row in table.Rows)
                        {
                            list.Add(row[column.ColumnName]);
                        }
                        table.Columns.Remove(column.ColumnName);
                        DataColumn column2 = new DataColumn(column.ColumnName, Type.GetType(column.DataType.FullName));
                        if (column2.DataType == typeof(string))
                        {
                            column2.MaxLength = column.Length;
                        }
                        table.Columns.Add(column2);
                        for (int j = 0; j < list.Count; j++)
                        {
                            try
                            {
                                table.Rows[j][column.ColumnName] = CastValue(list[j], column.DataType);
                            }
                            catch
                            {
                                table.Rows[j][column.ColumnName] = DBNull.Value;
                            }
                        }
                    }
                    if (table.Columns[column.ColumnName].DataType == typeof(string))
                    {
                        foreach (DataRow row2 in table.Rows)
                        {
                            if (row2[column.ColumnName].ToString().Length > column.Length)
                            {
                                row2[column.ColumnName] = row2[column.ColumnName].ToString().Substring(0, column.Length);
                            }
                        }
                        table.Columns[column.ColumnName].MaxLength = column.Length;
                    }
                    continue;
                }
                DataColumn column3 = new DataColumn(column.ColumnName, Type.GetType(column.DataType.FullName));
                if (column3.DataType == typeof(string))
                {
                    column3.MaxLength = column.Length;
                }
                table.Columns.Add(column3);
            }
            ArrayList list2 = new ArrayList();
            foreach (AdoColumn column4 in schema.KeyColumns)
            {
                list2.Add(table.Columns[column4.ColumnName]);
            }
            if (list2.Count == 0)
            {
                table.PrimaryKey = null;
            }
            else
            {
                table.PrimaryKey = (DataColumn[]) list2.ToArray(typeof(DataColumn));
            }
        }

        public static string ValidateConnection(IDbConnection connection)
        {
            try
            {
                connection.Open();
                connection.Close();
                return string.Empty;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string ValidateRecordSource(IDbCommand command)
        {
            string message = string.Empty;
            try
            {
                command.Connection.Open();
                command.ExecuteReader().Close();
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            finally
            {
                command.Connection.Close();
            }
            return message;
        }

        //public static bool CanCreateDatabaseTable
        //{
        //    get
        //    {
        //        try
        //        {
        //            new CatalogClass();
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public static List<char> ValidCharacters
        //{
        //    get
        //    {
        //        return _validCharacters;
        //    }
        //}
    }
}

