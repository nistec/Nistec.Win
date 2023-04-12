namespace Nistec.Printing.MSExcel.OleDb
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    using Nistec.Printing.Data;
    using System.IO;
    using System.Text;

    public class ExcelReader : AdoMap,IAdoReader
    {
        private OleDbCommand _cmdSelect;
        private OleDbConnection _connection;
        private OleDbDataReader _reader;
        private DataTable _tableSchema;


        public ExcelReader(ExcelReadProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelReadProperties();
            base.Output=new AdoOutput("Records Read", "Records successfully read from the configured MS Excel Workbook.");
        }

        public override uint ExecuteCommit()
        {
            //base.ExecuteCommit();
            //base.EndProcessing();
            if (this._reader != null)
            {
                this._reader.Close();
                this._reader = null;
            }
            if (this._cmdSelect != null)
            {
                this._cmdSelect.Cancel();
                this._cmdSelect = null;
            }
            if (this._connection != null)
            {
                this._connection.Close();
                this._connection = null;
            }
            //return  base.BatchRecordsRead;
            return base.ExecuteCommit();
        }

        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            ExcelReadProperties properties = this.Properties as ExcelReadProperties;
            try
            {
                if (properties.Workbook == "")
                {
                    base.CancelExecute("No Workbook specified");
                    return false;
                }
                if (base.Output==null)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                if (this._reader == null)
                {
                    OleDbConnection connection = ExcelHelper.GetConnection(properties);
                    connection.Open();
                    this._cmdSelect = new OleDbCommand(ExcelHelper.GetSelectSQL(properties), connection);
                    this._tableSchema = DataHelper.CreateDataTable(this._cmdSelect, "RecordsRead");
                    this._reader = this._cmdSelect.ExecuteReader();
                    base.ExecuteBegin(0);
                }
                DataTable table = this._tableSchema.Clone();
                object[] values = new object[this._reader.FieldCount];
                table.BeginLoadData();
                base.OnExecutionStarted(0);
                while (((batchSize == 0) || (table.Rows.Count < batchSize)) && !base.StopProcessing)
                {
                    if (!this._reader.Read())
                    {
                        base.BatchNotResponding = true;
                        break;
                    }
                    this._reader.GetValues(values);
                    table.LoadDataRow(values, true);
                    base.UpdateProcessing();
                }
                try
                {
                    table.EndLoadData();
                }
                catch (ConstraintException)
                {
                    //if (output != null)
                    //{
                    foreach (DataRow row in table.Rows)
                    {
                        if (row.HasErrors)
                        {
                            //output.WriteLine(DataHelper.FormatRowError(row));
                        }
                    }
                    //}
                }
                catch (Exception exception)
                {
                    //if (output != null)
                    //{
                    //    output.WriteLine("Warning: " + exception.Message.Replace("\r\n", " "));
                    //}
                    flag = false;
                    base.CancelExecute(exception.Message.Replace("\r\n", " "));
                }
                base.Output.Value = table;
            }
            catch (Exception exception2)
            {
                flag = false;
                base.CancelExecute(exception2.Message.Replace("\r\n", " "));
            }
            finally
            {
                //base._errorDescription = base.ObjectsProcessed.ToString();
                //if (output != null)
                //{
                //if (batchSize == 0)
                //{
                //    output.WriteLine(base.ObjectsProcessed + " records read.");
                //}
                //else
                //{
                //    output.WriteLine((((base.ObjectsProcessed % batchSize) == 0) ? batchSize : (base.ObjectsProcessed % batchSize)) + " records read.");
                //}
                //}
            }
            return flag;
        }

        //public override bool Execute()
        //{
        //    this.ExecuteBegin(0);
        //    bool flag = this.ExecuteBatch(0);
        //    this.ExecuteCommit();
        //    return flag;
        //}

        public static DataTable ReadFirstWorksheet(string fileName, bool firstRowHeader, uint maxRows, byte imex)
        {
            DataTable dataSet = new DataTable();
            OleDbConnection connection = new OleDbConnection(ExcelHelper.BuildConnectionString(fileName, firstRowHeader, imex));
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

            string worksheet = ExcelHelper.GetFirstWorksheet(connection);

            //OleDbCommand selectCommand = new OleDbCommand();
            try
            {
                    OleDbDataAdapter da = new OleDbDataAdapter(ExcelHelper.GetSelectSQL(worksheet, maxRows), connection);
                    da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dataSet;
        }

        public static DataSet Read(string fileName, bool firstRowHeader, uint maxRows)
        {
            return Read(fileName, firstRowHeader, maxRows, 1);
        }

        public static DataSet Read(string fileName, bool firstRowHeader, uint maxRows, byte imex)
        {
            DataSet dataSet = new DataSet();
            OleDbConnection connection = new OleDbConnection(ExcelHelper.BuildConnectionString(fileName, firstRowHeader,imex));
            try
            {
                connection.Open();
            }
            catch
            {
                return null;
            }

            string[] worksheets = ExcelHelper.GetWorksheets(connection, false);

            //OleDbCommand selectCommand = new OleDbCommand();
            try
            {
                //selectCommand.Connection = connection;
                //selectCommand.CommandType = CommandType.Text;
                //StringBuilder sb = new StringBuilder();
                foreach (string worksheet in worksheets)
                {
                    //sb.Append(ExcelHelper.GetSelectSQL(worksheet, maxRows));
                    //sb.Append(";");
                    OleDbDataAdapter da = new OleDbDataAdapter(ExcelHelper.GetSelectSQL(worksheet, maxRows), connection);
                    da.Fill(dataSet);
                }

                //selectCommand.CommandText = sb.ToString();
                //OleDbDataAdapter da = new OleDbDataAdapter(sb.ToString(), connection);
                //da.Fill(dataSet);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dataSet;
        }

    }
}

