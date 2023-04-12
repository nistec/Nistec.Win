namespace Nistec.Printing.MSExcel.Bin2003
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    using Nistec.Printing.Data;
    using System.IO;

    public class ExcelReader : AdoMap,IAdoReader
    {
        //private DataTable _tableSchema;


        public ExcelReader(ExcelReadProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelReadProperties();
            base.Output=new AdoOutput("Records Read", "Records successfully read from the configured MS Excel Workbook.");
        }


        //public override void ExecuteBegin(uint totalObjects)
        //{
        //    base.ExecuteBegin(totalObjects);
        //}

        //public override uint ExecuteCommit()
        //{
        //    return base.ExecuteCommit();
        //    //base.EndProcessing();
        //    //return  base.BatchRecordsRead;
        //}

        //public override bool Execute()
        //{
        //    this.ExecuteBegin(0);
        //    bool flag = this.ExecuteBatch(0);
        //    this.ExecuteCommit();
        //    return flag;
        //}

        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = false;
            ExcelReadProperties properties = this.Properties as ExcelReadProperties;
            //this.ExecuteBegin(0);
            try
            {

                if (properties.Workbook == "")
                {
                    base.CancelExecute("No Workbook specified");
                    return false;
                }
                if (base.Output == null)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }

                ExcelDataReader rd = new ExcelDataReader(properties.Workbook);
                DataTable table = rd.Read(properties.Worksheet, properties.FirstRowHeaders, batchSize);
                base.Output.Value = table;
                //bool flag = this.ExecuteBatch(0);//, output);
                //this.ExecuteCommit();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                base.CancelExecute(ex.Message.Replace("\r\n", " "));
            }
            return flag;
        }

        public static DataTable Import(string fileName,bool firstRowHeaders)
        {
            ExcelDataReader rd = new ExcelDataReader(fileName);
            return rd.Read(0, firstRowHeaders, 0);
        }

        public static DataTable Import(string fileName,bool firstRowHeaders,string worksheet)
        {
            ExcelDataReader rd = new ExcelDataReader(fileName);
            return rd.Read(worksheet, firstRowHeaders, 0);
        }
    }
}

