using System;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class C
    {
        // Workbook records
        public const UInt32 BIFF12_DEFINEDNAME            = 0x27;
        public const UInt32 BIFF12_FILEVERSION            = 0x0180;
        public const UInt32 BIFF12_WORKBOOK               = 0x0183;
        public const UInt32 BIFF12_WORKBOOK_END           = 0x0184;
        public const UInt32 BIFF12_BOOKVIEWS              = 0x0187;
        public const UInt32 BIFF12_BOOKVIEWS_END          = 0x0188;
        public const UInt32 BIFF12_SHEETS                 = 0x018F;
        public const UInt32 BIFF12_SHEETS_END             = 0x0190;
        public const UInt32 BIFF12_WORKBOOKPR             = 0x0199;
        public const UInt32 BIFF12_SHEET                  = 0x019C;
        public const UInt32 BIFF12_CALCPR                 = 0x019D;
        public const UInt32 BIFF12_WORKBOOKVIEW           = 0x019E;
        public const UInt32 BIFF12_EXTERNALREFERENCES     = 0x02E1;
        public const UInt32 BIFF12_EXTERNALREFERENCES_END = 0x02E2;
        public const UInt32 BIFF12_EXTERNALREFERENCE      = 0x02E3;
        public const UInt32 BIFF12_WEBPUBLISHING          = 0x04A9;

        // Worksheet records
        public const UInt32 BIFF12_ROW             = 0x00; // row info
        public const UInt32 BIFF12_BLANK           = 0x01; // empty cell
        public const UInt32 BIFF12_NUM             = 0x02; // single-precision float
        public const UInt32 BIFF12_BOOLERR         = 0x03; // error identifier
        public const UInt32 BIFF12_BOOL            = 0x04; // boolean value
        public const UInt32 BIFF12_FLOAT           = 0x05; // double-precision float
        public const UInt32 BIFF12_STRING          = 0x07; // string (shared string index)
        public const UInt32 BIFF12_FORMULA_STRING  = 0x08; // formula returning a string (inline string)
        public const UInt32 BIFF12_FORMULA_FLOAT   = 0x09; // formula returning a double-precision float
        public const UInt32 BIFF12_FORMULA_BOOL    = 0x0A; // formula returning a boolean
        public const UInt32 BIFF12_FORMULA_BOOLERR = 0x0B; // formula returning an error identifier
        public const UInt32 BIFF12_COL             = 0x3C; // column info
        public const UInt32 BIFF12_WORKSHEET       = 0x0181;
        public const UInt32 BIFF12_WORKSHEET_END   = 0x0182;
        public const UInt32 BIFF12_SHEETVIEWS      = 0x0185;
        public const UInt32 BIFF12_SHEETVIEWS_END  = 0x0186;
        public const UInt32 BIFF12_SHEETVIEW       = 0x0189;
        public const UInt32 BIFF12_SHEETVIEW_END   = 0x018A;
        public const UInt32 BIFF12_SHEETDATA       = 0x0191;
        public const UInt32 BIFF12_SHEETDATA_END   = 0x0192;
        public const UInt32 BIFF12_SHEETPR         = 0x0193;
        public const UInt32 BIFF12_DIMENSION       = 0x0194;
        public const UInt32 BIFF12_SELECTION       = 0x0198;
        public const UInt32 BIFF12_COLS            = 0x0386;
        public const UInt32 BIFF12_COLS_END        = 0x0387;
        public const UInt32 BIFF12_CONDITIONALFORMATTING = 0x03CD;
        public const UInt32 BIFF12_CONDITIONALFORMATTING_END = 0x03CE;
        public const UInt32 BIFF12_CFRULE          = 0x03CF;
        public const UInt32 BIFF12_CFRULE_END      = 0x03D0;
        public const UInt32 BIFF12_ICONSET         = 0x03D1;
        public const UInt32 BIFF12_ICONSET_END     = 0x03D2;
        public const UInt32 BIFF12_DATABAR         = 0x03D3;
        public const UInt32 BIFF12_DATABAR_END     = 0x03D4;
        public const UInt32 BIFF12_COLORSCALE      = 0x03D5;
        public const UInt32 BIFF12_COLORSCALE_END  = 0x03D6;
        public const UInt32 BIFF12_CFVO            = 0x03D7;
        public const UInt32 BIFF12_PAGEMARGINS     = 0x03DC;
        public const UInt32 BIFF12_PRINTOPTIONS    = 0x03DD;
        public const UInt32 BIFF12_PAGESETUP       = 0x03DE;
        public const UInt32 BIFF12_HEADERFOOTER    = 0x03DF;
        public const UInt32 BIFF12_SHEETFORMATPR   = 0x03E5;
        public const UInt32 BIFF12_HYPERLINK       = 0x03EE;
        public const UInt32 BIFF12_DRAWING         = 0x04A6;
        public const UInt32 BIFF12_LEGACYDRAWING   = 0x04A7;
        public const UInt32 BIFF12_COLOR           = 0x04B4;
        public const UInt32 BIFF12_OLEOBJECTS      = 0x04FE;
        public const UInt32 BIFF12_OLEOBJECT       = 0x04FF;
        public const UInt32 BIFF12_OLEOBJECTS_END  = 0x0580;
        public const UInt32 BIFF12_TABLEPARTS      = 0x0594;
        public const UInt32 BIFF12_TABLEPART       = 0x0595;
        public const UInt32 BIFF12_TABLEPARTS_END  = 0x0596;

        //SharedStrings records
        public const UInt32 BIFF12_SI              = 0x13;
        public const UInt32 BIFF12_SST             = 0x019F;
        public const UInt32 BIFF12_SST_END         = 0x01A0;

        //Styles records
        public const UInt32 BIFF12_FONT            = 0x2B;
        public const UInt32 BIFF12_FILL            = 0x2D;
        public const UInt32 BIFF12_BORDER          = 0x2E;
        public const UInt32 BIFF12_XF              = 0x2F;
        public const UInt32 BIFF12_CELLSTYLE       = 0x30;
        public const UInt32 BIFF12_STYLESHEET      = 0x0296;
        public const UInt32 BIFF12_STYLESHEET_END  = 0x0297;
        public const UInt32 BIFF12_COLORS          = 0x03D9;
        public const UInt32 BIFF12_COLORS_END      = 0x03DA;
        public const UInt32 BIFF12_DXFS            = 0x03F9;
        public const UInt32 BIFF12_DXFS_END        = 0x03FA;
        public const UInt32 BIFF12_TABLESTYLES     = 0x03FC;
        public const UInt32 BIFF12_TABLESTYLES_END = 0x03FD;
        public const UInt32 BIFF12_FILLS           = 0x04DB;
        public const UInt32 BIFF12_FILLS_END       = 0x04DC;
        public const UInt32 BIFF12_FONTS           = 0x04E3;
        public const UInt32 BIFF12_FONTS_END       = 0x04E4;
        public const UInt32 BIFF12_BORDERS         = 0x04E5;
        public const UInt32 BIFF12_BORDERS_END     = 0x04E6;
        public const UInt32 BIFF12_CELLXFS         = 0x04E9;
        public const UInt32 BIFF12_CELLXFS_END     = 0x04EA;
        public const UInt32 BIFF12_CELLSTYLES      = 0x04EB;
        public const UInt32 BIFF12_CELLSTYLES_END  = 0x04EC;
        public const UInt32 BIFF12_CELLSTYLEXFS    = 0x04F2;
        public const UInt32 BIFF12_CELLSTYLEXFS_END = 0x04F3;

        //Comment records
        public const UInt32 BIFF12_COMMENTS        = 0x04F4;
        public const UInt32 BIFF12_COMMENTS_END    = 0x04F5;
        public const UInt32 BIFF12_AUTHORS         = 0x04F6;
        public const UInt32 BIFF12_AUTHORS_END     = 0x04F7;
        public const UInt32 BIFF12_AUTHOR          = 0x04F8;
        public const UInt32 BIFF12_COMMENTLIST     = 0x04F9;
        public const UInt32 BIFF12_COMMENTLIST_END = 0x04FA;
        public const UInt32 BIFF12_COMMENT         = 0x04FB;
        public const UInt32 BIFF12_COMMENT_END     = 0x04FC;
        public const UInt32 BIFF12_TEXT            = 0x04FD;

        //Table records
        public const UInt32 BIFF12_AUTOFILTER      = 0x01A1;
        public const UInt32 BIFF12_AUTOFILTER_END  = 0x01A2;
        public const UInt32 BIFF12_FILTERCOLUMN    = 0x01A3;
        public const UInt32 BIFF12_FILTERCOLUMN_END= 0x01A4;
        public const UInt32 BIFF12_FILTERS         = 0x01A5;
        public const UInt32 BIFF12_FILTERS_END     = 0x01A6;
        public const UInt32 BIFF12_FILTER          = 0x01A7;
        public const UInt32 BIFF12_TABLE           = 0x02D7;
        public const UInt32 BIFF12_TABLE_END       = 0x02D8;
        public const UInt32 BIFF12_TABLECOLUMNS    = 0x02D9;
        public const UInt32 BIFF12_TABLECOLUMNS_END= 0x02DA;
        public const UInt32 BIFF12_TABLECOLUMN     = 0x02DB;
        public const UInt32 BIFF12_TABLECOLUMN_END = 0x02DC;
        public const UInt32 BIFF12_TABLESTYLEINFO  = 0x0481;
        public const UInt32 BIFF12_SORTSTATE       = 0x0492;
        public const UInt32 BIFF12_SORTCONDITION   = 0x0494;
        public const UInt32 BIFF12_SORTSTATE_END   = 0x0495;

        //QueryTable records
        public const UInt32 BIFF12_QUERYTABLE            = 0x03BF;
        public const UInt32 BIFF12_QUERYTABLE_END        = 0x03C0;
        public const UInt32 BIFF12_QUERYTABLEREFRESH     = 0x03C1;
        public const UInt32 BIFF12_QUERYTABLEREFRESH_END = 0x03C2;
        public const UInt32 BIFF12_QUERYTABLEFIELDS      = 0x03C7;
        public const UInt32 BIFF12_QUERYTABLEFIELDS_END  = 0x03C8;
        public const UInt32 BIFF12_QUERYTABLEFIELD       = 0x03C9;
        public const UInt32 BIFF12_QUERYTABLEFIELD_END   = 0x03CA;

        //Connection records
        public const UInt32 BIFF12_CONNECTIONS           = 0x03AD;
        public const UInt32 BIFF12_CONNECTIONS_END       = 0x03AE;
        public const UInt32 BIFF12_CONNECTION            = 0x01C9;
        public const UInt32 BIFF12_CONNECTION_END        = 0x01CA;
        public const UInt32 BIFF12_DBPR                  = 0x01CB;
        public const UInt32 BIFF12_DBPR_END              = 0x01CC;

    }
}
