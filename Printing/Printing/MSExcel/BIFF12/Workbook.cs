using System;
using System.Collections;


namespace Nistec.Printing.MSExcel.Bin2007
{

    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library



    class DefinedNameRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 17) 
            {
                UInt16 grbits   = GetWord(buffer, offset + 0);
                UInt32 nametype = GetDword(buffer, offset + 5);
                UInt32 lenName  = GetDword(buffer, offset + 9);

                String name = GetString(buffer, offset + 13, lenName);

                UInt32 lenFormula = GetDword(buffer, offset + 13 + 2 * lenName);
                // formula ptg tokens follow between 
                //  offset + 13 + 2 * lenName + 4
                //   and
                //  offset + 13 + 2 * lenName + 4 + lenFormula

                bool bHidden  = (grbits & 0x0001) != 0;
                bool bFunc    = (grbits & 0x0002) != 0;
                bool bBuiltin = (grbits & 0x0020) != 0;

                String sNameType;
                switch (nametype)
                {
                    case 0 : sNameType = "Print_Area"; break; // or Print_Titles in fact
                    case 1 : sNameType = "Database"; break; 
                    //TODO (Consolidate_Area,Auto_Open,Auto_Close,Extract,Criteria,Recorder
                    //      Data_Form,Auto_Activate,Auto_Deactivate,Sheet_Title,FilterDatabase)
                    default : sNameType = "Normal"; break;
                }

                System.Console.WriteLine(String.Format("\tinfo : hidden={0}, addinfunc={1}, builtin={2}, type={3}, name={4}, lenName={5}, lenFormula={6}", 
                    bHidden ? "true" : "false",
                    bFunc ? "true" : "false",
                    bBuiltin ? "true" : "false",
                    sNameType,
                    name,
                    lenName,
                    lenFormula));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid defined name record length");
            }

        }

        public override String GetTag()
        {
            return "definedName";
        }
    }

    class FileVersionRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "fileVersion";
        }
    }

    class WorkbookRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "workbook";
        }

    }

    class WorkbookEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/workbook";
        }

    }

    class BookViewsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "bookViews";
        }

    }

    class BookViewsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/bookViews";
        }

    }

    class SheetsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sheets";
        }

    }

    class SheetsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sheets";
        }

    }

    class WorkbookPRRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "workbookPr";
        }

    }

    class SheetRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            UInt32 sheetid = GetDword(buffer, offset + 8);

            UInt32 sheet_opc_ref_len = GetDword(buffer, offset + 12);

            String sheet_opc_ref = GetString(buffer, offset + 16, sheet_opc_ref_len);

            UInt32 sheet_name_len = GetDword(buffer, offset + 16 + 2 * sheet_opc_ref_len);

            String sheet_name = GetString(buffer, offset + 16 + 2 * sheet_opc_ref_len + 4, sheet_name_len);

            System.Console.WriteLine(String.Format("\tinfo : id={0}, r:id={1}, name={2}", 
                sheetid, 
                sheet_opc_ref, 
                sheet_name));
        }

        public override String GetTag()
        {
            return "sheet";
        }

    }

    class CalcPRRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "calcPr";
        }

    }

    class WorkbookViewRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "workbookView";
        }

    }

    class ExternalReferencesRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "externalReferences";
        }
    }

    class ExternalReferencesEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/externalReferences";
        }
    }

    class ExternalReferenceRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                Console.WriteLine("\tinfo : invalid externalReference record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 len = GetDword(buffer, offset + 0);
            String s = GetString(buffer, offset + 4, len);

            System.Console.WriteLine(String.Format("\tinfo : r:id={0} r:id:lenStr={1}", 
                s, len));
        }

        public override String GetTag()
        {
            return "externalReference";
        }
    }

    class WebPublishingRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "webPublishing";
        }

    }


}
