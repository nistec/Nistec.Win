using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class FontRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("\tinfo : <font><sz val=.../><color theme=.../><name val=.../><family val=.../><scheme val=.../></font>");
        }

        public override String GetTag()
        {
            return "font";
        }
    }

    class FillRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("\tinfo : <fill><patternFill = .../><fgColor = .../><bgColor =.../></fill>");
        }

        public override String GetTag()
        {
            return "fill";
        }
    }

    class BorderRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("\tinfo : <border><left/><right/><top/><bottom/><diagonal/></border>");
        }

        public override String GetTag()
        {
            return "border";
        }
    }

    class XFRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("\tinfo : <xf numFmtId=... fontId=... fillId=... borderId=.../>");
        }

        public override String GetTag()
        {
            return "xf";
        }
    }

    class CellStyleRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("\tinfo : <cellStyle name=... xfId=... builtinId=.../>");
        }

        public override String GetTag()
        {
            return "cellStyle";
        }
    }

    class StyleSheetRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "styleSheet";
        }
    }

    class StyleSheetEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/styleSheet";
        }
    }

    class ColorsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "colors";
        }
    }

    class ColorsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/colors";
        }
    }

    class DXFsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "dxfs";
        }
    }

    class DXFsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/dxfs";
        }
    }

    class TableStylesRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "tableStyles";
        }
    }

    class TableStylesEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/tableStyles";
        }
    }

    class FillsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged fills buffer in styles");
                return;
            }

            UInt32 fills = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : fills={0}", 
                fills));
        }

        public override String GetTag()
        {
            return "fills";
        }
    }

    class FillsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/fills";
        }
    }

    class FontsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged fonts buffer in styles");
                return;
            }

            UInt32 fonts = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : fonts={0}", 
                fonts));
        }

        public override String GetTag()
        {
            return "fonts";
        }
    }

    class FontsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/fonts";
        }
    }

    class BordersRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged borders buffer in styles");
                return;
            }

            UInt32 borders = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : borders={0}", 
                borders));
        }

        public override String GetTag()
        {
            return "borders";
        }
    }

    class BordersEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/borders";
        }
    }
    
    class CellXFsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged cell XFs buffer in styles");
                return;
            }

            UInt32 cellXfs = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : cellXfs={0}", 
                cellXfs));
        }

        public override String GetTag()
        {
            return "cellXfs";
        }
    }

    class CellXFsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/cellXfs";
        }
    }

    class CellStylesRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged cell styles buffer in styles");
                return;
            }

            UInt32 cellStyles = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : cellStyles={0}", 
                cellStyles));
        }

        public override String GetTag()
        {
            return "cellStyles";
        }
    }

    class CellStylesEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/cellStyles";
        }
    }

    class CellStyleXFsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                System.Console.WriteLine("damaged cell styles XFs buffer in styles");
                return;
            }

            UInt32 cellStylesXfs = GetDword(buffer, offset + 0);

            System.Console.WriteLine(String.Format("\tinfo : cellStylesXfs={0}", 
                cellStylesXfs));
        }

        public override String GetTag()
        {
            return "cellStyleXfs";
        }
    }

    class CellStyleXFsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/cellStyleXfs";
        }
    }

}
