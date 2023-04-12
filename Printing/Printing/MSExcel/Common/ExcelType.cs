using System;
using System.Collections.Generic;
using System.Text;

namespace Nistec.Printing.MSExcel
{
    public enum ExcelReaderType
    {
        Excel2003,
        Excel2007,
        ExcelOleDb,
        ExcelXml
    }

    public enum ExcelWriterType
    {
        ExcelOleDb,
        ExcelXml
    }
}
