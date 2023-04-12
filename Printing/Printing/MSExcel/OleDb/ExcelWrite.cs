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

    public class ExcelWriter : AdoMap, IAdoWriter
    {


        public ExcelWriter(ExcelWriteProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelWriteProperties();
            base.Output=new AdoOutput("Records To Write", "Records successfully written to the configured MS Excel.");//, true);
        }

        public override bool ExecuteBatch(uint batchSize)
        {
            return Execute();
        }

        public override bool Execute()
        {
            bool flag = true;
            ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;
            OleDbConnection connection = null;
            try
            {
                if (properties.Filename == "")
                {
                    base.CancelExecute( "No filename specified");
                    return false;
                }
                if (base.Output==null)
                {
                    base.CancelExecute( "Inputs/Outputs are invalid");
                    return false;
                }
                if (base.Output.Value == null)
                {
                    base.CancelExecute("No input records to write");
                    return false;
                }
                this.UpdateSchema();
                DataTable table = base.Output.Value as DataTable;
                base.ExecuteBegin((uint)table.Rows.Count);
                long length = 0L;
                if (File.Exists(properties.Filename))
                {
                    FileInfo info = new FileInfo(properties.Filename);
                    length = info.Length;
                }
                //bool append = properties.Append;
                //if (!append)
                //{
                //    append = (base.Parent as AdoMap).BatchCycle > 1;
                //}
                properties.FirstRowHeaders = true;

                connection = ExcelHelper.GetConnection(properties);
                try
                {
                    connection.Open();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                string[] sheets = ExcelHelper.GetWorksheets(connection, false);
                string tableName = properties.DataSource.TableName;

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //if ((properties.FirstRowHeaders) && (!append || (length == 0L)))
                //{
                //    writer.Write(ExcelHelper.CreateHeader(table, properties));
                //}
                if (!ArrayContains(sheets, tableName))
                {
                    command.CommandText = string.Format("Create Table {0} ({1})", tableName, properties.GetColumnRange());
                    command.ExecuteNonQuery();
                }
                foreach (DataRow row in table.Rows)
                {
                    command.CommandText = (ExcelHelper.GetInsertSQL(row, properties));
                    command.ExecuteNonQuery();
                    base.UpdateProcessing();
                }
                return flag;
            }
            catch (Exception exception)
            {
                flag = false;
                base.CancelExecute(exception.Message);
            }
            finally
            {
                if ((connection != null) && (connection.State == ConnectionState.Open))
                {
                    connection.Close();
                }
                //base.EndProcessing();
                //if (output != null)
                //{
                //    output.WriteLine(base.ObjectsProcessed + " records written.");
                //}
            }
            return flag;
        }


         private bool ArrayContains(string[] array, string name)
        {
            foreach (string s in array)
            {
                if (s.Equals(name))
                    return true;
            }
            return false;
        }


        public override AdoTable GetSchema()
        {
            ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;
            return properties.DataSource;
        }

     }
}

