using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class QueryTableRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 14) 
            {
                UInt32 connectionId = GetDword(buffer, offset + 6);

                UInt32 lenName = GetDword(buffer, offset + 10);

                String s = GetString(buffer, offset + 10 + 4, lenName);

                System.Console.WriteLine(String.Format("\tinfo : connectionId={0}, name={1} lenName={2}", 
                    connectionId,
                    s,
                    lenName));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid queryTable record length");
            }

        }

        public override String GetTag()
        {
            return "queryTable";
        }
    }

    class QueryTableEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/queryTable";
        }
    }

    class QueryTableRefreshRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "queryTableRefresh";
        }
    }

    class QueryTableRefreshEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/queryTableRefresh";
        }
    }

    class QueryTableFieldsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "queryTableFields";
        }
    }

    class QueryTableFieldsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/queryTableFields";
        }
    }

    class QueryTableFieldRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 16) 
            {
                UInt32 fieldId = GetDword(buffer, offset + 4);
                UInt32 tableColumnId = GetDword(buffer, offset + 8);

                UInt32 lenfieldName = GetDword(buffer, offset + 12);

                String s = GetString(buffer, offset + 12 + 4, lenfieldName);

                System.Console.WriteLine(String.Format("\tinfo : fieldId={0}, fieldName={1} lenfieldName={2}, tableColumnId={3}", 
                    fieldId,
                    s,
                    lenfieldName,
                    tableColumnId));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid queryTableField record length");
            }
        }

        public override String GetTag()
        {
            return "queryTableField";
        }
    }

    class QueryTableFieldEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/queryTableField";
        }
    }

}
