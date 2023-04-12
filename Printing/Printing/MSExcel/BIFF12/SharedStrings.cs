using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class SIRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 5) 
            {
                byte nbFormattingRuns = GetByte(buffer, offset); // = 0 if regular string

                UInt32 lenStr = GetDword(buffer, offset + 1);

                String s = GetString(buffer, offset + 1 + 4, lenStr);

                int indexString = w._sharedStrings.Count;

                w._sharedStrings.Add(s);

                System.Console.WriteLine(String.Format("\tinfo : index={0}, v:string={1} v:lenStr={2} v:formattingRuns={3}", 
                    indexString, 
                    s, 
                    lenStr,
                    ((nbFormattingRuns & 0x01) == 0) ? "no" : "yes"));

            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid si record length");
            }

        }

        public override String GetTag()
        {
            return "si";
        }
    }

    class SSTRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 8)
            {
                System.Console.WriteLine("damaged shared strings buffer");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 countStringsInUse = GetDword(buffer, offset + 0);
            UInt32 uniqueStrings = GetDword(buffer, offset + 4);

            System.Console.WriteLine(String.Format("\tinfo : countStringsInUse={0}, uniqueStrings={1}", 
                countStringsInUse, 
                uniqueStrings));

        }

        public override String GetTag()
        {
            return "sst";
        }
    }

    class SSTEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sst";
        }
    }

}
