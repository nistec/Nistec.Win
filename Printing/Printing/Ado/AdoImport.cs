using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nistec.Win;

namespace Nistec.Printing.Data
{
    public static class AdoImport
    {

        public static DataTable Import(ImportType exType, string fileName, bool firstRowHeader)
        {
            switch (exType)
            {
                case ImportType.Excel:
                    return Nistec.Printing.MSExcel.Bin2003.ExcelReader.Import(fileName, firstRowHeader);
                case ImportType.ExcelXml:
                    return Nistec.Printing.ExcelXml.Workbook.Import(fileName, firstRowHeader,0,0);
                case ImportType.Csv:
                    return Nistec.Printing.Csv.CsvReader.Import(fileName, firstRowHeader);
                case ImportType.Xml:
                    return Nistec.Printing.Xml.XmlReader.Import(fileName, System.IO.Path.GetFileNameWithoutExtension(fileName).Replace(" ",""));
                case ImportType.OleDb:
                    return Nistec.Printing.MSExcel.OleDb.ExcelReader.ReadFirstWorksheet(fileName, firstRowHeader,65000,1);
                    //if (ds == null || ds.Tables.Count == 0)
                    //    return null;
                    //return ds.Tables[0];
                default:
                    return Nistec.Printing.MSExcel.Bin2003.ExcelReader.Import(fileName, firstRowHeader);
            }
        }
        public static string GetFileImportName()
        {
            string filter = "XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";//|HTML files (*.htm)|*.htm";
            return CommonDlg.FileDialog(filter);
        }

        public static DataTable Import()
        {
            string fileName = GetFileImportName();
            if (fileName != "")
            {
                if (fileName.ToLower().EndsWith("csv"))
                {
                    return Import(ImportType.Csv, fileName, false);
                  }
                else if (fileName.ToLower().EndsWith("xls"))
                {
                    return Import(ImportType.Excel, fileName, false);
                }
                else if (fileName.ToLower().EndsWith("xml"))
                {
                    return Import(ImportType.Xml, fileName, false);
                }
            }
            return null;
        }

    }
}
