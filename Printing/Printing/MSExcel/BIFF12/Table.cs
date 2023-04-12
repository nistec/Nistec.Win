using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    class AutoFilterRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 16)
            {
                Console.WriteLine("\tinfo : invalid autoFilter record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 r1 = GetDword(buffer, offset + 0);
            UInt32 r2 = GetDword(buffer, offset + 4);
            UInt32 c1 = GetDword(buffer, offset + 8);
            UInt32 c2 = GetDword(buffer, offset + 12);

            System.Console.WriteLine(String.Format("\tinfo : r1={0}, c1={1}, r2={2}, c2={3}", 
                r1, c1, r2, c2));
        }

        public override String GetTag()
        {
            return "autoFilter";
        }
    }

    class AutoFilterEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/autoFilter";
        }
    }

    class FilterColumnRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "filterColumn";
        }
    }

    class FilterColumnEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/filterColumn";
        }
    }

    class FiltersRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "filters";
        }
    }

    class FiltersEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/filters";
        }
    }

    class FilterRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "filter";
        }
    }

    class TableRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 16)
            {
                Console.WriteLine("\tinfo : invalid table record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 r1 = GetDword(buffer, offset + 0);
            UInt32 r2 = GetDword(buffer, offset + 4);
            UInt32 c1 = GetDword(buffer, offset + 8);
            UInt32 c2 = GetDword(buffer, offset + 12);

            System.Console.WriteLine(String.Format("\tinfo : r1={0}, c1={1}, r2={2}, c2={3}", 
                r1, c1, r2, c2));

        }

        public override String GetTag()
        {
            return "table";
        }
    }

    class TableEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/table";
        }
    }

    class TableColumnsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                Console.WriteLine("\tinfo : invalid tableColumns record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 columns = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : columns={0}", 
                columns));
        }

        public override String GetTag()
        {
            return "tableColumns";
        }
    }

    class TableColumnsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/tableColumns";
        }
    }

    class TableColumnRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 32)
            {
                Console.WriteLine("\tinfo : invalid tableColumn record length");
                offset = UInt32.MaxValue;
                return;
            }
            
            UInt32 columnId = GetDword(buffer, offset + 0);

            UInt32 len = GetDword(buffer, offset + 28);
            String s = GetString(buffer, offset + 32, len);

            System.Console.WriteLine(String.Format("\tinfo : columnId={0} columnName={1}", 
                columnId, s));
        }

        public override String GetTag()
        {
            return "tableColumn";
        }
    }

    class TableColumnEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/tableColumn";
        }
    }

    class TableStyleInfoRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 32)
            {
                Console.WriteLine("\tinfo : invalid tableStyleInfo record length");
                offset = UInt32.MaxValue;
                return;
            }
            
            UInt16 grbits = GetWord(buffer, offset + 0);

            UInt32 len = GetDword(buffer, offset + 2);
            String s = GetString(buffer, offset + 6, len);

            System.Console.WriteLine(String.Format("\tinfo : grbits={0} styleName={1}", 
                grbits, s));
        }

        public override String GetTag()
        {
            return "tableStyleInfo";
        }
    }

    class SortStateRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sortState";
        }
    }

    class SortConditionRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sortCondition";
        }
    }

    class SortStateEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sortState";
        }
    }

}
