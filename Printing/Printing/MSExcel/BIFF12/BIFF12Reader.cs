using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;


// The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

// developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

// It's a starting point for a general purpose read/write .bin library


namespace Nistec.Printing.MSExcel.Bin2007
{

   

    class BaseRecord
    {
        public virtual void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
            System.Console.WriteLine("OVERRIDE BaseRecord::Read()");
        }

        public static UInt32 GetDword(byte[] buffer, UInt32 offset)
        {
            return ((UInt32)(buffer[offset + 3]) << 24) +
                ((UInt32)(buffer[offset + 2]) << 16) +
                ((UInt32)(buffer[offset + 1]) << 8) +
                ((UInt32)(buffer[offset + 0]));
        }

        public static UInt16 GetWord(byte[] buffer, UInt32 offset)
        {
            UInt16 val = (UInt16) (buffer[offset + 1] << 8);
            val += (UInt16) (buffer[offset + 0]);
            return val;
        }

        public static byte GetByte(byte[] buffer, UInt32 offset)
        {
            return buffer[offset];
        }

        public static String GetString(byte[] buffer, UInt32 offset, UInt32 len)
        {
            StringBuilder sb = new StringBuilder((int)len);
            for (UInt32 i = offset; i < offset + 2 * len; i += 2)
                sb.Append((Char)GetWord(buffer, i));
            return sb.ToString();
        }

        public double GetFloat(byte[] buffer, UInt32 offset)
        {
            double d = 0;

            // When it's a simple precision float, Excel uses a special
            // encoding
            UInt32 rk = GetDword(buffer, offset);
            if ((rk & 0x02) != 0)
            {
                // int
                d = (double) (rk >> 2);
            }
            else
            {
                using (MemoryStream mem = new MemoryStream())
                {
                    BinaryWriter bw = new BinaryWriter(mem);

                    bw.Write(0);
                    bw.Write(rk & -4);

                    mem.Seek(0,SeekOrigin.Begin);

                    BinaryReader br = new BinaryReader(mem);
                    d = br.ReadDouble();
                    br.Close();
                    bw.Close();
                }
            }
            if ((rk & 0x01) != 0)
            {
                // divide by 100
                d /= 100;
            }
            
            return d;
        }

        public double GetDouble(byte[] buffer, UInt32 offset)
        {
            double d = 0;

            using (MemoryStream mem = new MemoryStream())
            {
                BinaryWriter bw = new BinaryWriter(mem);

                for (UInt32 i = 0 ; i < 8; i++)
                    bw.Write(buffer[offset + i]);

                mem.Seek(0,SeekOrigin.Begin);

                BinaryReader br = new BinaryReader(mem);
                d = br.ReadDouble();
                br.Close();
                bw.Close();
            }

            return d;
        }

        public static void Dump(byte[] buffer, UInt32 offset, UInt32 len)
        {
            Console.Write("\t\tinfo :");

            for (UInt32 i = 0; i < len; i++)
                Console.Write( String.Format(" {0:X2}", buffer[offset + i]) );    

            Console.WriteLine();
        }

        public virtual String GetTag()
        {
            return "";
        }

        public static bool GetRecordID(byte[] buffer, ref UInt32 offset, ref UInt32 recid)
        {
            recid = 0;

            if (offset >= buffer.Length)
                return false;
            byte b1 = buffer[offset++];
            recid = (UInt32)(b1);

            if ((b1 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b2 = buffer[offset++];
            recid = ((UInt32)(b2) << 8) | recid;

            if ((b2 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b3 = buffer[offset++];
            recid = ((UInt32)(b3) << 16) | recid;

            if ((b3 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b4 = buffer[offset++];
            recid = ((UInt32)(b4) << 24) | recid;

            return true;
        }

        public static bool GetRecordLen(byte[] buffer, ref UInt32 offset, ref UInt32 reclen)
        {
            reclen = 0;

            if (offset >= buffer.Length)
                return false;
            byte b1 = buffer[offset++];
            reclen = (UInt32)(b1 & 0x7F);

            if ((b1 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b2 = buffer[offset++];
            reclen = ((UInt32)(b2 & 0x7F) << 7) | reclen;

            if ((b2 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b3 = buffer[offset++];
            reclen = ((UInt32)(b3 & 0x7F) << 14) | reclen;

            if ((b3 & 0x80) == 0)
                return true;

            if (offset >= buffer.Length)
                return false;
            byte b4 = buffer[offset++];
            reclen = ((UInt32)(b4 & 0x7F) << 21) | reclen;

            return true;
        }

    }


   

    class Workbook
    {
        public StringCollection _sharedStrings = new StringCollection();
    }




	/// <summary>
	/// Summary description for BIFF12Reader.
	/// </summary>
	public class BIFF12Reader
	{

        static void Read(Workbook w, Hashtable h, byte[] buffer)
        {
            // A generic BIFF12 part is a sequence of BIFF12 records

            // A BIFF12 record is a record identifier, followed by a record length, followed by the content itself

            // The record identifier is stored in 2 bytes

            // The record length is stored in a single byte (which limits record lengths to 255 bytes)

            // The record content is arbitrary content whose underlying structure is associated to the
            // the record identifier, and is defined once for all by the implementers of the file format

            UInt32 offset = 0;

            while (offset < buffer.Length)
            {
                UInt32 recid = 0;
                UInt32 reclen = 0;

                if (!BaseRecord.GetRecordID(buffer, ref offset, ref recid) ||
                    !BaseRecord.GetRecordLen(buffer, ref offset, ref reclen))
                {
                    Console.WriteLine("***Damaged buffer***");
                    break;
                }

                if (recid == 0 && reclen == 0)
                    break;

                BaseRecord recHandler = (BaseRecord) h[recid];
                //BaseRecord rec = (BaseRecord) Activator.CreateInstance(h[recid].GetType());

                if (recHandler != null)
                {
                    Console.Write( String.Format("<{0}>\r\n[rec=0x{1:X} len=0x{2:X}]", recHandler.GetTag(), recid, reclen) );

                    for (int i = 0; i < reclen; i++)
                    {
                        Console.Write( String.Format(" {0:X2}", buffer[offset + i]) );    
                    }

                    Console.WriteLine();

                    recHandler.Read(buffer, ref offset, (int)recid, (int)reclen, h, w);

                    if (offset == UInt32.MaxValue)
                    {
                        Console.WriteLine("***Damaged buffer***");
                        break;
                    }

                }
                else
                {
                    Console.Write( String.Format("[rec=0x{0:X} len=0x{1:X}]", recid, reclen) );

                    for (int i = 0; i < reclen; i++)
                    {
                        Console.Write( String.Format(" {0:X2}", buffer[offset + i]) );    
                    }

                    Console.WriteLine();
                }

                offset += reclen;

                Console.WriteLine();
            }
        }

		public static void Read(string fileName)
		{

            Hashtable h = new Hashtable();
    
            // Workbook records
            h[C.BIFF12_DEFINEDNAME]            = new DefinedNameRecord();
            h[C.BIFF12_FILEVERSION]            = new FileVersionRecord();
            h[C.BIFF12_WORKBOOK]               = new WorkbookRecord();
            h[C.BIFF12_WORKBOOK_END]           = new WorkbookEndRecord();
            h[C.BIFF12_BOOKVIEWS]              = new BookViewsRecord();
            h[C.BIFF12_BOOKVIEWS_END]          = new BookViewsEndRecord();
            h[C.BIFF12_SHEETS]                 = new SheetsRecord();
            h[C.BIFF12_SHEETS_END]             = new SheetsEndRecord();
            h[C.BIFF12_WORKBOOKPR]             = new WorkbookPRRecord();
            h[C.BIFF12_SHEET]                  = new SheetRecord();
            h[C.BIFF12_CALCPR]                 = new CalcPRRecord();
            h[C.BIFF12_WORKBOOKVIEW]           = new WorkbookViewRecord();
            h[C.BIFF12_EXTERNALREFERENCES]     = new ExternalReferencesRecord();
            h[C.BIFF12_EXTERNALREFERENCES_END] = new ExternalReferencesEndRecord();
            h[C.BIFF12_EXTERNALREFERENCE]      = new ExternalReferenceRecord();
            h[C.BIFF12_WEBPUBLISHING]          = new WebPublishingRecord();

            // Worksheet records
            h[C.BIFF12_ROW]               = new RowRecord();
            h[C.BIFF12_BLANK]             = new BlankRecord();
            h[C.BIFF12_NUM]               = new NumRecord();
            h[C.BIFF12_BOOLERR]           = new BoolErrRecord();
            h[C.BIFF12_BOOL]              = new BoolRecord();
            h[C.BIFF12_FLOAT]             = new FloatRecord();
            h[C.BIFF12_STRING]            = new StringRecord();
            h[C.BIFF12_FORMULA_STRING]    = new FormulaStringRecord();
            h[C.BIFF12_FORMULA_FLOAT]     = new FormulaFloatRecord();
            h[C.BIFF12_FORMULA_BOOL]      = new FormulaBoolRecord();
            h[C.BIFF12_FORMULA_BOOLERR]   = new FormulaBoolErrRecord();
            h[C.BIFF12_COL]               = new ColRecord();
            h[C.BIFF12_WORKSHEET]         = new WorksheetRecord();
            h[C.BIFF12_WORKSHEET_END]     = new WorksheetEndRecord();
            h[C.BIFF12_SHEETVIEWS]        = new SheetViewsRecord();
            h[C.BIFF12_SHEETVIEWS_END]    = new SheetViewsEndRecord();
            h[C.BIFF12_SHEETVIEW]         = new SheetViewRecord();
            h[C.BIFF12_SHEETVIEW_END]     = new SheetViewEndRecord();
            h[C.BIFF12_SHEETDATA]         = new SheetDataRecord();
            h[C.BIFF12_SHEETDATA_END]     = new SheetDataEndRecord();
            h[C.BIFF12_SHEETPR]           = new SheetPRRecord();
            h[C.BIFF12_DIMENSION]         = new DimensionRecord();
            h[C.BIFF12_SELECTION]         = new SelectionRecord();
            h[C.BIFF12_COLS]              = new ColsRecord();
            h[C.BIFF12_COLS_END]          = new ColsEndRecord();
            h[C.BIFF12_PAGEMARGINS]       = new PageMarginsRecord();
            h[C.BIFF12_PRINTOPTIONS]      = new PrintOptionsRecord();
            h[C.BIFF12_PAGESETUP]         = new PageSetupRecord();
            h[C.BIFF12_HEADERFOOTER]      = new HeaderFooterRecord();
            h[C.BIFF12_SHEETFORMATPR]     = new SheetFormatPRRecord();
            h[C.BIFF12_DRAWING]           = new DrawingRecord();
            h[C.BIFF12_LEGACYDRAWING]     = new LegacyDrawingRecord();
            h[C.BIFF12_OLEOBJECTS]        = new OLEObjectsRecord();
            h[C.BIFF12_OLEOBJECT]         = new OLEObjectRecord();
            h[C.BIFF12_OLEOBJECTS_END]    = new OLEObjectsEndRecord();
            h[C.BIFF12_TABLEPARTS]        = new TablePartsRecord();
            h[C.BIFF12_TABLEPART]         = new TablePartRecord();
            h[C.BIFF12_TABLEPARTS_END]    = new TablePartsEndRecord();
            h[C.BIFF12_CONDITIONALFORMATTING] = new ConditionalFormattingRecord();
            h[C.BIFF12_CONDITIONALFORMATTING_END] = new ConditionalFormattingEndRecord();
            h[C.BIFF12_CFRULE]            = new CFRuleRecord();
            h[C.BIFF12_CFRULE_END]        = new CFRuleEndRecord();
            h[C.BIFF12_ICONSET]           = new IconSetRecord();
            h[C.BIFF12_ICONSET_END]       = new IconSetEndRecord();
            h[C.BIFF12_DATABAR]           = new DataBarRecord();
            h[C.BIFF12_DATABAR_END]       = new DataBarEndRecord();
            h[C.BIFF12_COLORSCALE]        = new ColorScaleRecord();
            h[C.BIFF12_COLORSCALE_END]    = new ColorScaleEndRecord();
            h[C.BIFF12_CFVO]              = new CFVORecord();
            h[C.BIFF12_HYPERLINK]         = new HyperlinkRecord();
            h[C.BIFF12_COLOR]             = new ColorRecord();

            // SharedStrings records
            h[C.BIFF12_SI]                = new SIRecord();
            h[C.BIFF12_SST]               = new SSTRecord();
            h[C.BIFF12_SST_END]           = new SSTEndRecord();

            // Styles records
            h[C.BIFF12_FONT]              = new FontRecord();
            h[C.BIFF12_FILL]              = new FillRecord();            
            h[C.BIFF12_BORDER]            = new BorderRecord();
            h[C.BIFF12_XF]                = new XFRecord();
            h[C.BIFF12_CELLSTYLE]         = new CellStyleRecord();
            h[C.BIFF12_STYLESHEET]        = new StyleSheetRecord();
            h[C.BIFF12_STYLESHEET_END]    = new StyleSheetEndRecord();
            h[C.BIFF12_COLORS]            = new ColorsRecord();
            h[C.BIFF12_COLORS_END]        = new ColorsEndRecord();
            h[C.BIFF12_DXFS]              = new DXFsRecord();
            h[C.BIFF12_DXFS_END]          = new DXFsEndRecord();
            h[C.BIFF12_TABLESTYLES]       = new TableStylesRecord();
            h[C.BIFF12_TABLESTYLES_END]   = new TableStylesEndRecord();
            h[C.BIFF12_FILLS]             = new FillsRecord();
            h[C.BIFF12_FILLS_END]         = new FillsEndRecord();
            h[C.BIFF12_FONTS]             = new FontsRecord();
            h[C.BIFF12_FONTS_END]         = new FontsEndRecord();
            h[C.BIFF12_BORDERS]           = new BordersRecord();
            h[C.BIFF12_BORDERS_END]       = new BordersEndRecord();
            h[C.BIFF12_CELLXFS]           = new CellXFsRecord();
            h[C.BIFF12_CELLXFS_END]       = new CellXFsEndRecord();
            h[C.BIFF12_CELLSTYLES]        = new CellStylesRecord();
            h[C.BIFF12_CELLSTYLES_END]    = new CellStylesEndRecord();
            h[C.BIFF12_CELLSTYLEXFS]      = new CellStyleXFsRecord();
            h[C.BIFF12_CELLSTYLEXFS_END]  = new CellStyleXFsEndRecord();

            // Comment records
            h[C.BIFF12_COMMENTS]          = new CommentsRecord();
            h[C.BIFF12_COMMENTS_END]      = new CommentsEndRecord();
            h[C.BIFF12_AUTHORS]           = new AuthorsRecord();
            h[C.BIFF12_AUTHORS_END]       = new AuthorsEndRecord();
            h[C.BIFF12_AUTHOR]            = new AuthorRecord();
            h[C.BIFF12_COMMENTLIST]       = new CommentListRecord();
            h[C.BIFF12_COMMENTLIST_END]   = new CommentListEndRecord();
            h[C.BIFF12_COMMENT]           = new CommentRecord();
            h[C.BIFF12_COMMENT_END]       = new CommentEndRecord();
            h[C.BIFF12_TEXT]              = new TextRecord();

            //Table records
            h[C.BIFF12_AUTOFILTER]        = new AutoFilterRecord();
            h[C.BIFF12_AUTOFILTER_END]    = new AutoFilterEndRecord();
            h[C.BIFF12_FILTERCOLUMN]      = new FilterColumnRecord();
            h[C.BIFF12_FILTERCOLUMN_END]  = new FilterColumnEndRecord();
            h[C.BIFF12_FILTERS]           = new FiltersRecord();
            h[C.BIFF12_FILTERS_END]       = new FiltersEndRecord();
            h[C.BIFF12_FILTER]            = new FilterRecord();
            h[C.BIFF12_TABLE]             = new TableRecord();
            h[C.BIFF12_TABLE_END]         = new TableEndRecord();
            h[C.BIFF12_TABLECOLUMNS]      = new TableColumnsRecord();
            h[C.BIFF12_TABLECOLUMNS_END]  = new TableColumnsEndRecord();
            h[C.BIFF12_TABLECOLUMN]       = new TableColumnRecord();
            h[C.BIFF12_TABLECOLUMN_END]   = new TableColumnEndRecord();
            h[C.BIFF12_TABLESTYLEINFO]    = new TableStyleInfoRecord();
            h[C.BIFF12_SORTSTATE]         = new SortStateRecord();
            h[C.BIFF12_SORTCONDITION]     = new SortConditionRecord();
            h[C.BIFF12_SORTSTATE_END]     = new SortStateEndRecord();

            //QueryTable records
            h[C.BIFF12_QUERYTABLE]        = new QueryTableRecord();
            h[C.BIFF12_QUERYTABLE_END]    = new QueryTableEndRecord();
            h[C.BIFF12_QUERYTABLEREFRESH] = new QueryTableRefreshRecord();
            h[C.BIFF12_QUERYTABLEREFRESH_END] = new QueryTableRefreshEndRecord();
            h[C.BIFF12_QUERYTABLEFIELDS]  = new QueryTableFieldsRecord();
            h[C.BIFF12_QUERYTABLEFIELDS_END] = new QueryTableFieldsEndRecord();
            h[C.BIFF12_QUERYTABLEFIELD]   = new QueryTableFieldRecord();
            h[C.BIFF12_QUERYTABLEFIELD_END] = new QueryTableFieldEndRecord();

            //Connection records
            h[C.BIFF12_CONNECTIONS]       = new ConnectionsRecord();
            h[C.BIFF12_CONNECTIONS_END]   = new ConnectionsEndRecord();
            h[C.BIFF12_CONNECTION]        = new ConnectionRecord();
            h[C.BIFF12_CONNECTION_END]    = new ConnectionEndRecord();
            h[C.BIFF12_DBPR]              = new DBPRRecord();
            h[C.BIFF12_DBPR_END]          = new DBPREndRecord();


            Workbook w = new Workbook();

            Console.WriteLine("*** Dumping a workbook part\r\n");


            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1.xlsb\xl\workbook.bin", 
                                                  FileMode.Open, FileAccess.Read))
            {
                byte[] bufferWorkbookPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferWorkbookPart);
            }
            


            Console.WriteLine("*** Dumping a shared strings part (one for the entire workbook)\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_strings.xlsb\xl\sharedStrings.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferSharedStringsPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferSharedStringsPart);
            }




            Console.WriteLine("*** Dumping a styles part (one for the entire workbook)\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_strings.xlsb\xl\styles.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferStylesPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferStylesPart);
            }




            Console.WriteLine("*** Dumping a printer settings part (optional, one per worksheet/chart)\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1.xlsb\xl\printerSettings\printerSettings1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferPrinterSettingsPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                PrinterSettingsReader.Read(w, h, bufferPrinterSettingsPart);
            }




            Console.WriteLine("*** Dumping a comments part (optional, one per workbook)\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_comment.xlsb\xl\comments1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferWorksheetPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferWorksheetPart);
            }




            Console.WriteLine("*** Dumping a table part\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_pivottable.xlsb\xl\tables\table1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferTablePart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferTablePart);
            }



            Console.WriteLine("*** Dumping a query table part\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_range.xlsb\xl\queryTables\queryTable1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferQueryTablePart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferQueryTablePart);
            }





            Console.WriteLine("*** Dumping a connections part\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_range.xlsb\xl\connections.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferConnectionsPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferConnectionsPart);
            }



            Console.WriteLine("*** Dumping a worksheet part\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_strings.xlsb\xl\worksheets\sheet1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferWorksheetPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(w, h, bufferWorksheetPart);
            }

            Console.WriteLine("press key to continue...");
            Console.ReadLine();

  		}

    }

    public class ExcelDataReader
    {
        private DataSet _DataSource;
        private Hashtable hash;
        private Workbook book;


        public DataSet DataSource
        {
            get { return _DataSource; }
        }

        void Read(string tableName, byte[] buffer)
        {
            // A generic BIFF12 part is a sequence of BIFF12 records

            // A BIFF12 record is a record identifier, followed by a record length, followed by the content itself

            // The record identifier is stored in 2 bytes

            // The record length is stored in a single byte (which limits record lengths to 255 bytes)

            // The record content is arbitrary content whose underlying structure is associated to the
            // the record identifier, and is defined once for all by the implementers of the file format

            UInt32 offset = 0;

            while (offset < buffer.Length)
            {
                UInt32 recid = 0;
                UInt32 reclen = 0;

                if (!BaseRecord.GetRecordID(buffer, ref offset, ref recid) ||
                    !BaseRecord.GetRecordLen(buffer, ref offset, ref reclen))
                {
                    Console.WriteLine("***Damaged buffer***");
                    break;
                }

                if (recid == 0 && reclen == 0)
                    break;

                BaseRecord recHandler = (BaseRecord)hash[recid];
                //BaseRecord rec = (BaseRecord) Activator.CreateInstance(h[recid].GetType());

                if (recHandler != null)
                {
                    Console.Write(String.Format("<{0}>\r\n[rec=0x{1:X} len=0x{2:X}]", recHandler.GetTag(), recid, reclen));

                    for (int i = 0; i < reclen; i++)
                    {
                        Console.Write(String.Format(" {0:X2}", buffer[offset + i]));
                    }

                    Console.WriteLine();

                    recHandler.Read(buffer, ref offset, (int)recid, (int)reclen, hash, book);

                    if (offset == UInt32.MaxValue)
                    {
                        Console.WriteLine("***Damaged buffer***");
                        break;
                    }

                }
                else
                {
                    Console.Write(String.Format("[rec=0x{0:X} len=0x{1:X}]", recid, reclen));

                    for (int i = 0; i < reclen; i++)
                    {
                        Console.Write(String.Format(" {0:X2}", buffer[offset + i]));
                    }

                    Console.WriteLine();
                }

                offset += reclen;

                Console.WriteLine();
            }
        }


        public ExcelDataReader()
        {
            _DataSource = new DataSet();
            hash = new Hashtable();

            // Workbook records
            hash[C.BIFF12_DEFINEDNAME] = new DefinedNameRecord();
            hash[C.BIFF12_FILEVERSION] = new FileVersionRecord();
            hash[C.BIFF12_WORKBOOK] = new WorkbookRecord();
            hash[C.BIFF12_WORKBOOK_END] = new WorkbookEndRecord();
            hash[C.BIFF12_BOOKVIEWS] = new BookViewsRecord();
            hash[C.BIFF12_BOOKVIEWS_END] = new BookViewsEndRecord();
            hash[C.BIFF12_SHEETS] = new SheetsRecord();
            hash[C.BIFF12_SHEETS_END] = new SheetsEndRecord();
            hash[C.BIFF12_WORKBOOKPR] = new WorkbookPRRecord();
            hash[C.BIFF12_SHEET] = new SheetRecord();
            hash[C.BIFF12_CALCPR] = new CalcPRRecord();
            hash[C.BIFF12_WORKBOOKVIEW] = new WorkbookViewRecord();
            hash[C.BIFF12_EXTERNALREFERENCES] = new ExternalReferencesRecord();
            hash[C.BIFF12_EXTERNALREFERENCES_END] = new ExternalReferencesEndRecord();
            hash[C.BIFF12_EXTERNALREFERENCE] = new ExternalReferenceRecord();
            hash[C.BIFF12_WEBPUBLISHING] = new WebPublishingRecord();

            // Worksheet records
            hash[C.BIFF12_ROW] = new RowRecord();
            hash[C.BIFF12_BLANK] = new BlankRecord();
            hash[C.BIFF12_NUM] = new NumRecord();
            hash[C.BIFF12_BOOLERR] = new BoolErrRecord();
            hash[C.BIFF12_BOOL] = new BoolRecord();
            hash[C.BIFF12_FLOAT] = new FloatRecord();
            hash[C.BIFF12_STRING] = new StringRecord();
            hash[C.BIFF12_FORMULA_STRING] = new FormulaStringRecord();
            hash[C.BIFF12_FORMULA_FLOAT] = new FormulaFloatRecord();
            hash[C.BIFF12_FORMULA_BOOL] = new FormulaBoolRecord();
            hash[C.BIFF12_FORMULA_BOOLERR] = new FormulaBoolErrRecord();
            hash[C.BIFF12_COL] = new ColRecord();
            hash[C.BIFF12_WORKSHEET] = new WorksheetRecord();
            hash[C.BIFF12_WORKSHEET_END] = new WorksheetEndRecord();
            hash[C.BIFF12_SHEETVIEWS] = new SheetViewsRecord();
            hash[C.BIFF12_SHEETVIEWS_END] = new SheetViewsEndRecord();
            hash[C.BIFF12_SHEETVIEW] = new SheetViewRecord();
            hash[C.BIFF12_SHEETVIEW_END] = new SheetViewEndRecord();
            hash[C.BIFF12_SHEETDATA] = new SheetDataRecord();
            hash[C.BIFF12_SHEETDATA_END] = new SheetDataEndRecord();
            hash[C.BIFF12_SHEETPR] = new SheetPRRecord();
            hash[C.BIFF12_DIMENSION] = new DimensionRecord();
            hash[C.BIFF12_SELECTION] = new SelectionRecord();
            hash[C.BIFF12_COLS] = new ColsRecord();
            hash[C.BIFF12_COLS_END] = new ColsEndRecord();
            hash[C.BIFF12_PAGEMARGINS] = new PageMarginsRecord();
            hash[C.BIFF12_PRINTOPTIONS] = new PrintOptionsRecord();
            hash[C.BIFF12_PAGESETUP] = new PageSetupRecord();
            hash[C.BIFF12_HEADERFOOTER] = new HeaderFooterRecord();
            hash[C.BIFF12_SHEETFORMATPR] = new SheetFormatPRRecord();
            hash[C.BIFF12_DRAWING] = new DrawingRecord();
            hash[C.BIFF12_LEGACYDRAWING] = new LegacyDrawingRecord();
            hash[C.BIFF12_OLEOBJECTS] = new OLEObjectsRecord();
            hash[C.BIFF12_OLEOBJECT] = new OLEObjectRecord();
            hash[C.BIFF12_OLEOBJECTS_END] = new OLEObjectsEndRecord();
            hash[C.BIFF12_TABLEPARTS] = new TablePartsRecord();
            hash[C.BIFF12_TABLEPART] = new TablePartRecord();
            hash[C.BIFF12_TABLEPARTS_END] = new TablePartsEndRecord();
            hash[C.BIFF12_CONDITIONALFORMATTING] = new ConditionalFormattingRecord();
            hash[C.BIFF12_CONDITIONALFORMATTING_END] = new ConditionalFormattingEndRecord();
            hash[C.BIFF12_CFRULE] = new CFRuleRecord();
            hash[C.BIFF12_CFRULE_END] = new CFRuleEndRecord();
            hash[C.BIFF12_ICONSET] = new IconSetRecord();
            hash[C.BIFF12_ICONSET_END] = new IconSetEndRecord();
            hash[C.BIFF12_DATABAR] = new DataBarRecord();
            hash[C.BIFF12_DATABAR_END] = new DataBarEndRecord();
            hash[C.BIFF12_COLORSCALE] = new ColorScaleRecord();
            hash[C.BIFF12_COLORSCALE_END] = new ColorScaleEndRecord();
            hash[C.BIFF12_CFVO] = new CFVORecord();
            hash[C.BIFF12_HYPERLINK] = new HyperlinkRecord();
            hash[C.BIFF12_COLOR] = new ColorRecord();

            // SharedStrings records
            hash[C.BIFF12_SI] = new SIRecord();
            hash[C.BIFF12_SST] = new SSTRecord();
            hash[C.BIFF12_SST_END] = new SSTEndRecord();

            // Styles records
            hash[C.BIFF12_FONT] = new FontRecord();
            hash[C.BIFF12_FILL] = new FillRecord();
            hash[C.BIFF12_BORDER] = new BorderRecord();
            hash[C.BIFF12_XF] = new XFRecord();
            hash[C.BIFF12_CELLSTYLE] = new CellStyleRecord();
            hash[C.BIFF12_STYLESHEET] = new StyleSheetRecord();
            hash[C.BIFF12_STYLESHEET_END] = new StyleSheetEndRecord();
            hash[C.BIFF12_COLORS] = new ColorsRecord();
            hash[C.BIFF12_COLORS_END] = new ColorsEndRecord();
            hash[C.BIFF12_DXFS] = new DXFsRecord();
            hash[C.BIFF12_DXFS_END] = new DXFsEndRecord();
            hash[C.BIFF12_TABLESTYLES] = new TableStylesRecord();
            hash[C.BIFF12_TABLESTYLES_END] = new TableStylesEndRecord();
            hash[C.BIFF12_FILLS] = new FillsRecord();
            hash[C.BIFF12_FILLS_END] = new FillsEndRecord();
            hash[C.BIFF12_FONTS] = new FontsRecord();
            hash[C.BIFF12_FONTS_END] = new FontsEndRecord();
            hash[C.BIFF12_BORDERS] = new BordersRecord();
            hash[C.BIFF12_BORDERS_END] = new BordersEndRecord();
            hash[C.BIFF12_CELLXFS] = new CellXFsRecord();
            hash[C.BIFF12_CELLXFS_END] = new CellXFsEndRecord();
            hash[C.BIFF12_CELLSTYLES] = new CellStylesRecord();
            hash[C.BIFF12_CELLSTYLES_END] = new CellStylesEndRecord();
            hash[C.BIFF12_CELLSTYLEXFS] = new CellStyleXFsRecord();
            hash[C.BIFF12_CELLSTYLEXFS_END] = new CellStyleXFsEndRecord();

            // Comment records
            hash[C.BIFF12_COMMENTS] = new CommentsRecord();
            hash[C.BIFF12_COMMENTS_END] = new CommentsEndRecord();
            hash[C.BIFF12_AUTHORS] = new AuthorsRecord();
            hash[C.BIFF12_AUTHORS_END] = new AuthorsEndRecord();
            hash[C.BIFF12_AUTHOR] = new AuthorRecord();
            hash[C.BIFF12_COMMENTLIST] = new CommentListRecord();
            hash[C.BIFF12_COMMENTLIST_END] = new CommentListEndRecord();
            hash[C.BIFF12_COMMENT] = new CommentRecord();
            hash[C.BIFF12_COMMENT_END] = new CommentEndRecord();
            hash[C.BIFF12_TEXT] = new TextRecord();

            //Table records
            hash[C.BIFF12_AUTOFILTER] = new AutoFilterRecord();
            hash[C.BIFF12_AUTOFILTER_END] = new AutoFilterEndRecord();
            hash[C.BIFF12_FILTERCOLUMN] = new FilterColumnRecord();
            hash[C.BIFF12_FILTERCOLUMN_END] = new FilterColumnEndRecord();
            hash[C.BIFF12_FILTERS] = new FiltersRecord();
            hash[C.BIFF12_FILTERS_END] = new FiltersEndRecord();
            hash[C.BIFF12_FILTER] = new FilterRecord();
            hash[C.BIFF12_TABLE] = new TableRecord();
            hash[C.BIFF12_TABLE_END] = new TableEndRecord();
            hash[C.BIFF12_TABLECOLUMNS] = new TableColumnsRecord();
            hash[C.BIFF12_TABLECOLUMNS_END] = new TableColumnsEndRecord();
            hash[C.BIFF12_TABLECOLUMN] = new TableColumnRecord();
            hash[C.BIFF12_TABLECOLUMN_END] = new TableColumnEndRecord();
            hash[C.BIFF12_TABLESTYLEINFO] = new TableStyleInfoRecord();
            hash[C.BIFF12_SORTSTATE] = new SortStateRecord();
            hash[C.BIFF12_SORTCONDITION] = new SortConditionRecord();
            hash[C.BIFF12_SORTSTATE_END] = new SortStateEndRecord();

            //QueryTable records
            hash[C.BIFF12_QUERYTABLE] = new QueryTableRecord();
            hash[C.BIFF12_QUERYTABLE_END] = new QueryTableEndRecord();
            hash[C.BIFF12_QUERYTABLEREFRESH] = new QueryTableRefreshRecord();
            hash[C.BIFF12_QUERYTABLEREFRESH_END] = new QueryTableRefreshEndRecord();
            hash[C.BIFF12_QUERYTABLEFIELDS] = new QueryTableFieldsRecord();
            hash[C.BIFF12_QUERYTABLEFIELDS_END] = new QueryTableFieldsEndRecord();
            hash[C.BIFF12_QUERYTABLEFIELD] = new QueryTableFieldRecord();
            hash[C.BIFF12_QUERYTABLEFIELD_END] = new QueryTableFieldEndRecord();

            //Connection records
            hash[C.BIFF12_CONNECTIONS] = new ConnectionsRecord();
            hash[C.BIFF12_CONNECTIONS_END] = new ConnectionsEndRecord();
            hash[C.BIFF12_CONNECTION] = new ConnectionRecord();
            hash[C.BIFF12_CONNECTION_END] = new ConnectionEndRecord();
            hash[C.BIFF12_DBPR] = new DBPRRecord();
            hash[C.BIFF12_DBPR_END] = new DBPREndRecord();


            book = new Workbook();

            Console.WriteLine("*** Dumping a workbook part\r\n");

        }

        public DataTable ReadWorksheet(string fileName,string worksheet)
        {

            Console.WriteLine("*** Dumping a worksheet part\r\n");

            using (FileStream fs = new FileStream(fileName,//@"..\..\Excel12_files\Book1_strings.xlsb\xl\worksheets\sheet1.bin", 
                       FileMode.Open, FileAccess.Read))
            {
                byte[] bufferWorksheetPart = new BinaryReader(fs).ReadBytes((int)fs.Length);

                Read(worksheet, bufferWorksheetPart);
            }

            Console.WriteLine("press key to continue...");
            Console.ReadLine();

            return _DataSource.Tables[worksheet];

        }

	}
}
