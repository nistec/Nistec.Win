using System;
using System.Collections;


namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class RowRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 12) 
            {
                UInt32 row     = GetDword(buffer, offset + 0);
                UInt32 style   = GetDword(buffer, offset + 4);
                UInt16 height  = GetWord(buffer, offset + 8);
                UInt16 grbits  = GetWord(buffer, offset + 10);

                bool outline = (grbits & 0x0700) != 0; // 8 levels of outline
                bool resized = (grbits & 0x2000) != 0;
                bool hidden  = (grbits & 0x1000) != 0;

                System.Console.WriteLine(String.Format("\tinfo : row={0}, height={1}, style={2}, outline={3}, resize={4}, hidden={5}", 
                    row,
                    (float)(height) / 20,
                    style,
                    outline ? "true":"false",
                    resized ? "true":"false",
                    hidden ?  "true":"false"));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid row record length");
            }
        }

        public override String GetTag()
        {
            return "row";
        }
    }

    
    class BlankRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 8) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:blank", 
                    col, 
                    style));

            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid blank record length");
            }
        }

        public override String GetTag()
        {
            return "num";
        }
    }

    class NumRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 12) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                double f      = GetFloat(buffer, offset + 8);

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:float={2}", 
                    col, 
                    style, 
                    f));

            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid num record length");
            }
        }

        public override String GetTag()
        {
            return "num";
        }
    }

    class BoolErrRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 9) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                byte b        = GetByte(buffer, offset + 8);

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:boolerr={2}", 
                    col, 
                    style, 
                    b));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid boolerr record length");
            }

        }

        public override String GetTag()
        {
            return "boolErr";
        }
    }

    class BoolRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 9) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                byte b        = GetByte(buffer, offset + 8);

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:bool={2}", 
                    col, 
                    style, 
                    (b != 0) ?"true":"false"));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid bool record length");
            }
        }

        public override String GetTag()
        {
            return "bool";
        }
    }

    class FloatRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 16) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                double f      = GetDouble(buffer, offset + 8);

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:float={2}", 
                    col, 
                    style, 
                    f));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid string record length");
            }
        }

        public override String GetTag()
        {
            return "float";
        }
    }

    class StringRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 12) 
            {
                UInt32 col                 = GetDword(buffer, offset + 0);
                UInt32 style               = GetDword(buffer, offset + 4);
                UInt32 sharedstring_index  = GetDword(buffer, offset + 8);

                if (sharedstring_index < w._sharedStrings.Count) 
                {
                    System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:stringindex={2} v:string={3}",
                        col,
                        style,
                        sharedstring_index,
                        w._sharedStrings[(int)sharedstring_index]));
                }
                else 
                {
                    System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, v:stringindex={2} unknown",
                        col,
                        style,
                        sharedstring_index));
                }
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid string record length");
            }

        }

        public override String GetTag()
        {
            return "string";
        }
    }

    class FormulaStringRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 22) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                UInt32 lenStr = GetDword(buffer, offset + 8);

                String s = GetString(buffer, offset + 12, lenStr);

                UInt16 grbits    = GetWord(buffer, offset + 12 + 2 * lenStr);
                bool bCalcOnOpen = (grbits & 0x0010) != 0;

                UInt32 lenFormula = GetDword(buffer, offset + 12 + 2 * lenStr + 2/*grbits*/);

                // formula ptg tokens follow between 
                //  offset + 12 + 2 * lenStr + 2/*grbits*/ + 4
                //   and
                //  offset + 12 + 2 * lenStr + 2/*grbits*/ + 4 + lenFormula

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, formula v:calcOnOpen={2} v:string={3} v:lenStr={4}, v:lenFormula={5}",
                    col,
                    style,
                    bCalcOnOpen ? "true" : "false",
                    s,
                    lenStr,
                    lenFormula));
                }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid string formula record length");
            }

        }

        public override String GetTag()
        {
            return "formulaString";
        }
    }

    class FormulaFloatRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 24) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                double d      = GetDouble(buffer, offset + 8);

                UInt16 grbits    = GetWord(buffer, offset + 16);
                bool bCalcOnOpen = (grbits & 0x0010) != 0;

                UInt32 lenFormula = GetDword(buffer, offset + 16 + 2/*grbits*/);

                // formula ptg tokens follow between 
                //  offset + 16 + 2/*grbits*/ + 4
                //   and
                //  offset + 16 + 2/*grbits*/ + 4 + lenFormula

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, formula v:calcOnOpen={2} v:float={3} v:lenFormula={4}",
                    col,
                    style,
                    bCalcOnOpen ? "true" : "false",
                    d,
                    lenFormula));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid float formula record length");
            }

        }

        public override String GetTag()
        {
            return "formulaFloat";
        }
    }

    class FormulaBoolRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 19) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                byte b        = GetByte(buffer, offset + 8);

                UInt16 grbits    = GetWord(buffer, offset + 9);
                bool bCalcOnOpen = (grbits & 0x0010) != 0;

                UInt32 lenFormula = GetDword(buffer, offset + 9 + 2/*grbits*/);

                // formula ptg tokens follow between 
                //  offset + 9 + 2/*grbits*/ + 4
                //   and
                //  offset + 9 + 2/*grbits*/ + 4 + lenFormula

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, formula v:calcOnOpen={2} v:bool={3} v:lenFormula={4}",
                    col,
                    style,
                    bCalcOnOpen ? "true" : "false",
                    (b != 0)?"true":"false",
                    lenFormula));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid formula bool record length");
            }
        }

        public override String GetTag()
        {
            return "formulaBool";
        }
    }

    class FormulaBoolErrRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 19) 
            {
                UInt32 col    = GetDword(buffer, offset + 0);
                UInt32 style  = GetDword(buffer, offset + 4);
                byte b        = GetByte(buffer, offset + 8);

                UInt16 grbits    = GetWord(buffer, offset + 9);
                bool bCalcOnOpen = (grbits & 0x0010) != 0;

                UInt32 lenFormula = GetDword(buffer, offset + 9 + 2/*flags*/);

                // formula ptg tokens follow between 
                //  offset + 9 + 2/*flags*/ + 4
                //   and
                //  offset + 9 + 2/*flags*/ + 4 + lenFormula

                System.Console.WriteLine(String.Format("\tinfo : col={0}, style={1}, formula v:calcOnOpen={2} v:boolerr={3} v:lenFormula={4}",
                    col,
                    style,
                    bCalcOnOpen ? "true" : "false",
                    b,
                    lenFormula));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid formula boolerr record length");
            }

        }

        public override String GetTag()
        {
            return "formulaBoolErr";
        }
    }

    class ColRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen >= 18) 
            {
                UInt32 colmin = GetDword(buffer, offset + 0);
                UInt32 colmax = GetDword(buffer, offset + 4);
                UInt32 width  = GetDword(buffer, offset + 8);
                UInt32 style  = GetDword(buffer, offset + 12);
                            
                UInt16 grbits  = GetWord(buffer, offset + 16);
                bool outline = (grbits & 0x0700) != 0; // 8 levels of outline
                bool resized = (grbits & 0x0002) != 0;
                bool hidden  = (grbits & 0x0001) != 0;


                System.Console.WriteLine(String.Format("\tinfo : colmin={0}, colmax={1}, width={2}, style={3}, outline={4}, resize={5}, hidden={6}", 
                    colmin,
                    colmax,
                    (float)(width) / 256,
                    style,
                    outline ? "true":"false",
                    resized ? "true":"false",
                    hidden ?  "true":"false"));
            }
            else 
            {
                System.Console.WriteLine("\tinfo : invalid col record length");
            }

        }

        public override String GetTag()
        {
            return "col";
        }
    }

    class WorksheetRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "worksheet";
        }
    }

    class WorksheetEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/worksheet";
        }
    }

    class SheetViewsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sheetViews";
        }
    }

    class SheetViewsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sheetViews";
        }
    }

    class SheetViewRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sheetView";
        }
    }

    class SheetViewEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sheetView";
        }
    }
    
    class SheetDataRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sheetData";
        }
    }

    class SheetDataEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/sheetData";
        }
    }

    class SheetPRRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            Console.WriteLine("\tinfo :   <tabColor rgb=.../>\r\n\tinfo :   <outlinePr showOutlineSymbols=.../>\r\n\tinfo :   <pageSetUpPr .../>\r\n\tinfo : </sheetPr>");
        }

        public override String GetTag()
        {
            return "sheetPr";
        }
    }

    class DimensionRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 16)
            {
                Console.WriteLine("\tinfo : invalid dimension record length");
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
            return "dimension";
        }
    }

    class SelectionRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "selection";
        }
    }
    
    class ColsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "cols";
        }
    }

    class ColsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/cols";
        }
    }

    class PageMarginsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "pageMargins";
        }
    }

    class PrintOptionsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "printOptions";
        }
    }

    class PageSetupRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "pageSetup";
        }
    }

    class HeaderFooterRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "headerFooter";
        }
    }

    class SheetFormatPRRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "sheetFormatPr";
        }
    }

    class DrawingRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                Console.WriteLine("\tinfo : invalid drawing record length");
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
            return "drawing";
        }
    }

    class LegacyDrawingRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                Console.WriteLine("\tinfo : invalid legacy drawing record length");
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
            return "legacyDrawing";
        }
    }

    class OLEObjectsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "oleObjects";
        }
    }

    class OLEObjectRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 18)
            {
                Console.WriteLine("\tinfo : invalid oleobject record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 shapeid = GetDword(buffer, offset + 8);
            UInt32 lenProgId = GetDword(buffer, offset + 14);
            String sProgId = GetString(buffer, offset + 14 + 4, lenProgId);
            UInt32 lenRId = GetDword(buffer, offset + 14 + 4 + 2 * lenProgId);
            String sRId = GetString(buffer, offset + 14 + 4 + 2 * lenProgId + 4, lenRId);

            System.Console.WriteLine(String.Format("\tinfo : shapeId={0} progId={1} progId:lenStr={2} r:id={3} r:id:lenStr={4}", 
                shapeid,
                sProgId,
                lenProgId,
                sRId,
                lenRId));

        }

        public override String GetTag()
        {
            return "oleObject";
        }
    }

    class OLEObjectsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/oleObjects";
        }
    }


    class TablePartsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "tableParts";
        }
    }

    class TablePartRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 4)
            {
                Console.WriteLine("\tinfo : invalid tablePart record length");
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
            return "tablePart";
        }
    }

    class TablePartsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/tableParts";
        }
    }

    class ConditionalFormattingRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "conditionalFormatting";
        }
    }

    class ConditionalFormattingEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/conditionalFormatting";
        }
    }

    class CFRuleRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "cfRule";
        }
    }

    class CFRuleEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/cfRule";
        }
    }

    class IconSetRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "iconSet";
        }
    }

    class IconSetEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/iconSet";
        }
    }

    class DataBarRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "dataBar";
        }
    }

    class DataBarEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/dataBar";
        }
    }

    class ColorScaleRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "colorScale";
        }
    }

    class ColorScaleEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/colorScale";
        }
    }

    class CFVORecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "cfvo";
        }
    }

    class HyperlinkRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            if (reclen < 20)
            {
                Console.WriteLine("\tinfo : invalid hyperlink record length");
                offset = UInt32.MaxValue;
                return;
            }

            UInt32 r1 = GetDword(buffer, offset + 0);
            UInt32 r2 = GetDword(buffer, offset + 4);
            UInt32 c1 = GetDword(buffer, offset + 8);
            UInt32 c2 = GetDword(buffer, offset + 12);

            UInt32 len = GetDword(buffer, offset + 16);
            String s = GetString(buffer, offset + 20, len);

            System.Console.WriteLine(String.Format("\tinfo : r1={0}, c1={1}, r2={2}, c2={3}, rId={4}", 
                r1, c1, r2, c2, s));

        }

        public override String GetTag()
        {
            return "hyperlink";
        }
    }

    class ColorRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "color";
        }
    }




}
