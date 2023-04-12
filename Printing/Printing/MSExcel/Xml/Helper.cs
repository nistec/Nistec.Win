namespace Nistec.Printing.MSExcel.Xml
{
    using Nistec.Printing;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;

    public static class ExcelHelper
    {

        public static string[] GetGetWorksheets(ExcelReadProperties properties)
        {

           return ExcelXml.Workbook.GetWorksheets(properties.Workbook);
            
        }


    }
}

