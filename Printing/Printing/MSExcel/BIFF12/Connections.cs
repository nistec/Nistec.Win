using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class ConnectionsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "connections";
        }
    }

    class ConnectionsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/connections";
        }
    }

    class ConnectionRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 16) 
            {
                UInt32 lensourceFile = GetDword(buffer, offset + 23);
                String sourceFile = GetString(buffer, offset + 23 + 4, lensourceFile);

                UInt32 lenName = GetDword(buffer, offset + 23 + 4 + 2 * lensourceFile);
                String name = GetString(buffer, offset + 23 + 4 + 2 * lensourceFile + 4, lenName);

                System.Console.WriteLine(String.Format("\tinfo : sourceFile={0} lensourceFile={1}, name={2} lenName={3}", 
                    sourceFile,
                    lensourceFile,
                    name,
                    lenName));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid connection record length");
            }
        }

        public override String GetTag()
        {
            return "connection";
        }
    }

    class ConnectionEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/connection";
        }
    }

    class DBPRRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 16) 
            {
                UInt32 lenConnection = GetDword(buffer, offset + 5);
                String connection = GetString(buffer, offset + 5 + 4, lenConnection);

                System.Console.WriteLine(String.Format("\tinfo : connection={0} lenConnection={1}", 
                    connection,
                    lenConnection));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid dbPr record length");
            }
        }

        public override String GetTag()
        {
            return "dbPr";
        }
    }

    class DBPREndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/dbPr";
        }
    }



}
