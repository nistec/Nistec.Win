namespace Nistec.Printing.MSExcel.Xml
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    using Nistec.Printing.Data;
    using System.IO;
    using System.Globalization;
    using Nistec.Printing.ExcelXml;

    public class ExcelWriter : AdoMap, IAdoWriter
    {

        public ExcelWriter(ExcelWriteProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelWriteProperties();
            base.Output = new AdoOutput("Records To Write", "Records successfully written to the configured MS Excel.");//, true);
        }

        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;

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
                //DataTable table = base.Output.Value as DataTable;
                //flag = Workbook.Export(table, properties.Filename, properties.FirstRowHeaders, properties.DataSource.Columns.ToArray());

                flag = WriteWorkbook();

                //DataTable table = base.Output.Value as DataTable;
                //base.ExecuteBegin((uint)table.Rows.Count);
                //long length = 0L;
                //if (File.Exists(properties.Filename))
                //{
                //    FileInfo info = new FileInfo(properties.Filename);
                //    length = info.Length;
                //}
                //ExcelXml.Workbook wb = ExcelXml.Workbook.DataSetToWorkbook(table);
                //base.UpdateProcessing();
                //return flag;
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
            ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;
            return properties.DataSource;
        }

  
        private bool WriteWorkbook()
        {

            ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;
            DataTable table = base.Output.Value as DataTable;

            if (properties.DataSource.Columns.Count == 0)
            {
                properties.DataSource.CreateFields(table);
            }
            properties.DataSource.ValidateTableName(table.TableName);
            string sheet = properties.DataSource.TableName;

            Workbook workbook = new Workbook();

            const int maxRows = 65000;

            int k, iWorkSheet = 1, cols = 1, rows = 1, maxStep = 0, offset = 0;
            System.Text.StringBuilder strExcelXml = new System.Text.StringBuilder();
            cols = table.Columns.Count; ;
            rows = table.Rows.Count;
            maxStep = rows;
            if (rows >= maxRows)
            {
                iWorkSheet = rows / maxRows;
                iWorkSheet++;
                maxStep = maxRows;
                //offset=iRow-maxRows;
            }

            string sheetName = sheet;

            for (int i = 0; i < iWorkSheet; i++)
            {
                if (i > 0)
                {
                    sheetName = sheet + i.ToString();
                }

                Worksheet worksheet = workbook.Add(sheetName);
                int index = 0;
                bool writeHeader = properties.FirstRowHeaders;
                foreach (AdoColumn c in properties.DataSource.Columns)
                {
                    if (writeHeader)
                    {
                        //Headers
                        worksheet[0, index].Value = c.ToString();
                        worksheet[0, index].Font.Bold = true;
                    }
                    worksheet.Columns(index);
                    index++;
                }

                if (iWorkSheet > 1 && i == iWorkSheet - 1)
                {
                    maxStep = rows - ((iWorkSheet - 1) * maxRows);
                }
                offset = i * maxRows;

                int iRow = 0;
                int iCol = 0;
                int wRow =0;
                for (k = 0; k < maxStep; k++)
                {

                    iCol = 0;
                    iRow = k + offset;
                    wRow =k+ (int)(writeHeader ? 1 : 0);
                    DataRow row = table.Rows[iRow];

                    foreach (AdoColumn c in properties.DataSource.Columns)
                    {
                        worksheet[wRow, iCol].SetValue(row[c.ColumnName], c.DataType);
                        iCol++;
                    }
                    base.UpdateProcessing();
                }

            }
            return workbook.Write(properties.Filename);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader)
        {
            return Workbook.Export(table, fileName, firstRowHeader, null);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            return Workbook.Export(table, fileName, firstRowHeader, fields);
        }


    }
}

