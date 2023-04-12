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

    public class ExcelReader : AdoMap,IAdoReader
    {

        public ExcelReader(ExcelReadProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelReadProperties();
            base.Output=new AdoOutput("Records Read", "Records successfully read from the configured MS Excel Workbook.");
        }

  
        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = false;
            ExcelReadProperties properties = this.Properties as ExcelReadProperties;
            //this.ExecuteBegin(0);
            //try
            //{
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
                ExcelXml.Workbook wb = ExcelXml.Workbook.Import(properties.Workbook);
                //wb[properties.Worksheet]
                Nistec.Printing.ExcelXml.Worksheet ws = wb[properties.Worksheet];
                if (ws == null)
                {
                    return false;
                }
                DataTable table = ws.GetDataTable(properties.FirstRowHeaders,0);//batchSize,properties.FirstRowHeaders);
                base.Output.Value = table;
                //this.ExecuteCommit();
                flag = true;
            //}
            //catch (Exception ex)
            //{
            //    flag = false;
            //    base.CancelExecute(ex.Message.Replace("\r\n", " "));
            //}
            return flag;
        }
  
    }
}

