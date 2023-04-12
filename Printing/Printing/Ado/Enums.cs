using System;
using System.Collections.Generic;
using System.Text;

namespace Nistec.Printing.Data
{
    public enum ReaderType
    {
        Excel2007,
        Excel2003,
        ExcelXml,
        ExcelOleDb,
        Csv,
        Xml,
        Clipboard,
        MSAccess,
        Odbc,
        OleDb,
        SqlServer,
        MySql,
        Oracle
    }

    public enum WriterType
    {
        ExcelXml,
        ExcelOleDb,
        Csv,
        Xml,
        Html,
        Pdf,
        MSAccess,
        Odbc,
        OleDb,
        SqlServer,
        MySql,
        Oracle
    }

    public enum ImportType
    {
        Excel,
        ExcelXml,
        Csv,
        Xml,
        OleDb
        //Clipboard,
    }

    public enum ExportType
    {
        Excel,
        Csv,
        Xml,
        Html,
        Pdf
    }
}
