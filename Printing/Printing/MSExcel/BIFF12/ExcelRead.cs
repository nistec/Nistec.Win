namespace Nistec.Printing.MSExcel.Bin2007
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


  
        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = false;
            ExcelReadProperties properties = this.Properties as ExcelReadProperties;
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
                BIFF12Reader.Read(properties.Workbook);

                ExcelDataReader rd = new ExcelDataReader();
                DataTable table = rd.ReadWorksheet(properties.Workbook,properties.Worksheet);
                base.Output.Value = table;
                //bool flag = this.ExecuteBatch(0);//, output);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                base.CancelExecute(ex.Message.Replace("\r\n", " "));
            }
            return flag;
        }
  
    }
}

