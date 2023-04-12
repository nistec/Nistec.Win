using System;
using System.Runtime.InteropServices;

namespace MControl.Win32
{

	#region WinMsgs

	public sealed class WinMsgs
	{

		public  const int WM_NULL				    = 0x0000;
		public  const int WM_CREATE                 = 0x0001;
		public  const int WM_DESTROY                = 0x0002;
		public  const int WM_MOVE                   = 0x0003;
		public  const int WM_SIZE                   = 0x0005;
		public  const int WM_ACTIVATE               = 0x0006;
		public  const int WM_SETFOCUS               = 0x0007;
		public  const int WM_KILLFOCUS              = 0x0008;
		public  const int WM_ENABLE                 = 0x000A;
		public  const int WM_SETREDRAW              = 0x000B;
		public  const int WM_SETTEXT                = 0x000C;
		public  const int WM_GETTEXT                = 0x000D;
		public  const int WM_GETTEXTLENGTH          = 0x000E;
		public  const int WM_PAINT                  = 0x000F;
		public  const int WM_CLOSE                  = 0x0010;
		public  const int WM_QUERYENDSESSION        = 0x0011;
		public  const int WM_QUIT                   = 0x0012;
		public  const int WM_QUERYOPEN              = 0x0013;
		public  const int WM_ERASEBKGND             = 0x0014;
		public  const int WM_SYSCOLORCHANGE         = 0x0015;
		public  const int WM_ENDSESSION             = 0x0016;
		public  const int WM_SHOWWINDOW             = 0x0018;
		public  const int WM_WININICHANGE           = 0x001A;
		public  const int WM_SETTINGCHANGE          = 0x001A;
		public  const int WM_DEVMODECHANGE          = 0x001B;
		public  const int WM_ACTIVATEAPP            = 0x001C;
		public  const int WM_FONTCHANGE             = 0x001D;
		public  const int WM_TIMECHANGE             = 0x001E;
		public  const int WM_CANCELMODE             = 0x001F;
		public  const int WM_SETCURSOR              = 0x0020;
		public  const int WM_MOUSEACTIVATE          = 0x0021;
		public  const int WM_CHILDACTIVATE          = 0x0022;
		public  const int WM_QUEUESYNC              = 0x0023;
		public  const int WM_GETMINMAXINFO          = 0x0024;
		public  const int WM_PAINTICON              = 0x0026;
		public  const int WM_ICONERASEBKGND         = 0x0027;
		public  const int WM_NEXTDLGCTL             = 0x0028;
		public  const int WM_SPOOLERSTATUS          = 0x002A;
		public  const int WM_DRAWITEM               = 0x002B;
		public  const int WM_MEASUREITEM            = 0x002C;
		public  const int WM_DELETEITEM             = 0x002D;
		public  const int WM_VKEYTOITEM             = 0x002E;
		public  const int WM_CHARTOITEM             = 0x002F;
		public  const int WM_SETFONT                = 0x0030;
		public  const int WM_GETFONT                = 0x0031;
		public  const int WM_SETHOTKEY              = 0x0032;
		public  const int WM_GETHOTKEY              = 0x0033;
		public  const int WM_QUERYDRAGICON          = 0x0037;
		public  const int WM_COMPAREITEM            = 0x0039;
		public  const int WM_GETOBJECT              = 0x003D;
		public  const int WM_COMPACTING             = 0x0041;
		public  const int WM_COMMNOTIFY             = 0x0044 ;
		public  const int WM_WINDOWPOSCHANGING      = 0x0046;
		public  const int WM_WINDOWPOSCHANGED       = 0x0047;
		public  const int WM_POWER                  = 0x0048;
		public  const int WM_COPYDATA               = 0x004A;
		public  const int WM_CANCELJOURNAL          = 0x004B;
		public  const int WM_NOTIFY                 = 0x004E;
		public  const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
		public  const int WM_INPUTLANGCHANGE        = 0x0051;
		public  const int WM_TCARD                  = 0x0052;
		public  const int WM_HELP                   = 0x0053;
		public  const int WM_USERCHANGED            = 0x0054;
		public  const int WM_NOTIFYFORMAT           = 0x0055;
		public  const int WM_CONTEXTMENU            = 0x007B;
		public  const int WM_STYLECHANGING          = 0x007C;
		public  const int WM_STYLECHANGED           = 0x007D;
		public  const int WM_DISPLAYCHANGE          = 0x007E;
		public  const int WM_GETICON                = 0x007F;
		public  const int WM_SETICON                = 0x0080;
		public  const int WM_NCCREATE               = 0x0081;
		public  const int WM_NCDESTROY              = 0x0082;
		public  const int WM_NCCALCSIZE             = 0x0083;
		public  const int WM_NCHITTEST              = 0x0084;
		public  const int WM_NCPAINT                = 0x0085;
		public  const int WM_NCACTIVATE             = 0x0086;
		public  const int WM_GETDLGCODE             = 0x0087;
		public  const int WM_SYNCPAINT              = 0x0088;
		public  const int WM_NCMOUSEMOVE            = 0x00A0;
		public  const int WM_NCLBUTTONDOWN          = 0x00A1;
		public  const int WM_NCLBUTTONUP            = 0x00A2;
		public  const int WM_NCLBUTTONDBLCLK        = 0x00A3;
		public  const int WM_NCRBUTTONDOWN          = 0x00A4;
		public  const int WM_NCRBUTTONUP            = 0x00A5;
		public  const int WM_NCRBUTTONDBLCLK        = 0x00A6;
		public  const int WM_NCMBUTTONDOWN          = 0x00A7;
		public  const int WM_NCMBUTTONUP            = 0x00A8;
		public  const int WM_NCMBUTTONDBLCLK        = 0x00A9;
		public  const int WM_NCXBUTTONDOWN          = 0x00AB;
		public  const int WM_NCXBUTTONUP            = 0x00AC;
		public  const int WM_KEYDOWN                = 0x0100;
		public  const int WM_KEYUP                  = 0x0101;
		public  const int WM_CHAR                   = 0x0102;
		public  const int WM_DEADCHAR               = 0x0103;
		public  const int WM_SYSKEYDOWN             = 0x0104;
		public  const int WM_SYSKEYUP               = 0x0105;
		public  const int WM_SYSCHAR                = 0x0106;
		public  const int WM_SYSDEADCHAR            = 0x0107;
		public  const int WM_KEYLAST                = 0x0108;
		public  const int WM_IME_STARTCOMPOSITION   = 0x010D;
		public  const int WM_IME_ENDCOMPOSITION     = 0x010E;
		public  const int WM_IME_COMPOSITION        = 0x010F;
		public  const int WM_IME_KEYLAST            = 0x010F;
		public  const int WM_INITDIALOG             = 0x0110;
		public  const int WM_COMMAND                = 0x0111;
		public  const int WM_SYSCOMMAND             = 0x0112;
		public  const int WM_TIMER                  = 0x0113;
		public  const int WM_HSCROLL                = 0x0114;
		public  const int WM_VSCROLL                = 0x0115;
		public  const int WM_INITMENU               = 0x0116;
		public  const int WM_INITMENUPOPUP          = 0x0117;
		public  const int WM_MENUSELECT             = 0x011F;
		public  const int WM_MENUCHAR               = 0x0120;
		public  const int WM_ENTERIDLE              = 0x0121;
		public  const int WM_MENURBUTTONUP          = 0x0122;
		public  const int WM_MENUDRAG               = 0x0123;
		public  const int WM_MENUGETOBJECT          = 0x0124;
		public  const int WM_UNINITMENUPOPUP        = 0x0125;
		public  const int WM_MENUCOMMAND            = 0x0126;
		public  const int WM_CTLCOLORMSGBOX         = 0x0132;
		public  const int WM_CTLCOLOREDIT           = 0x0133;
		public  const int WM_CTLCOLORLISTBOX        = 0x0134;
		public  const int WM_CTLCOLORBTN            = 0x0135;
		public  const int WM_CTLCOLORDLG            = 0x0136;
		public  const int WM_CTLCOLORSCROLLBAR      = 0x0137;
		public  const int WM_CTLCOLOR         = 0x0138;
		public  const int WM_MOUSEMOVE              = 0x0200;
		public  const int WM_LBUTTONDOWN            = 0x0201;
		public  const int WM_LBUTTONUP              = 0x0202;
		public  const int WM_LBUTTONDBLCLK          = 0x0203;
		public  const int WM_RBUTTONDOWN            = 0x0204;
		public  const int WM_RBUTTONUP              = 0x0205;
		public  const int WM_RBUTTONDBLCLK          = 0x0206;
		public  const int WM_MBUTTONDOWN            = 0x0207;
		public  const int WM_MBUTTONUP              = 0x0208;
		public  const int WM_MBUTTONDBLCLK          = 0x0209;
		public  const int WM_MOUSEWHEEL             = 0x020A;
		public  const int WM_XBUTTONDOWN            = 0x020B;
		public  const int WM_XBUTTONUP              = 0x020C;
		public  const int WM_XBUTTONDBLCLK          = 0x020D;
		public  const int WM_PARENTNOTIFY           = 0x0210;
		public  const int WM_ENTERMENULOOP          = 0x0211;
		public  const int WM_EXITMENULOOP           = 0x0212;
		public  const int WM_NEXTMENU               = 0x0213;
		public  const int WM_SIZING                 = 0x0214;
		public  const int WM_CAPTURECHANGED         = 0x0215;
		public  const int WM_MOVING                 = 0x0216;
		public  const int WM_DEVICECHANGE           = 0x0219;
		public  const int WM_MDICREATE              = 0x0220;
		public  const int WM_MDIDESTROY             = 0x0221;
		public  const int WM_MDIACTIVATE            = 0x0222;
		public  const int WM_MDIRESTORE             = 0x0223;
		public  const int WM_MDINEXT                = 0x0224;
		public  const int WM_MDIMAXIMIZE            = 0x0225;
		public  const int WM_MDITILE                = 0x0226;
		public  const int WM_MDICASCADE             = 0x0227;
		public  const int WM_MDIICONARRANGE         = 0x0228;
		public  const int WM_MDIGETACTIVE           = 0x0229;
		public  const int WM_MDISETMENU             = 0x0230;
		public  const int WM_ENTERSIZEMOVE          = 0x0231;
		public  const int WM_EXITSIZEMOVE           = 0x0232;
		public  const int WM_DROPFILES              = 0x0233;
		public  const int WM_MDIREFRESHMENU         = 0x0234;
		public  const int WM_IME_SETCONTEXT         = 0x0281;
		public  const int WM_IME_NOTIFY             = 0x0282;
		public  const int WM_IME_CONTROL            = 0x0283;
		public  const int WM_IME_COMPOSITIONFULL    = 0x0284;
		public  const int WM_IME_SELECT             = 0x0285;
		public  const int WM_IME_CHAR               = 0x0286;
		public  const int WM_IME_REQUEST            = 0x0288;
		public  const int WM_IME_KEYDOWN            = 0x0290;
		public  const int WM_IME_KEYUP              = 0x0291;
		public  const int WM_MOUSEHOVER             = 0x02A1;
		public  const int WM_MOUSELEAVE             = 0x02A3;
		public  const int WM_CUT                    = 0x0300;
		public  const int WM_COPY                   = 0x0301;
		public  const int WM_PASTE                  = 0x0302;
		public  const int WM_CLEAR                  = 0x0303;
		public  const int WM_UNDO                   = 0x0304;
		public  const int WM_RENDERFORMAT           = 0x0305;
		public  const int WM_RENDERALLFORMATS       = 0x0306;
		public  const int WM_DESTROYCLIPBOARD       = 0x0307;
		public  const int WM_DRAWCLIPBOARD          = 0x0308;
		public  const int WM_PAINTCLIPBOARD         = 0x0309;
		public  const int WM_VSCROLLCLIPBOARD       = 0x030A;
		public  const int WM_SIZECLIPBOARD          = 0x030B;
		public  const int WM_ASKCBFORMATNAME        = 0x030C;
		public  const int WM_CHANGECBCHAIN          = 0x030D;
		public  const int WM_HSCROLLCLIPBOARD       = 0x030E;
		public  const int WM_QUERYNEWPALETTE        = 0x030F;
		public  const int WM_PALETTEISCHANGING      = 0x0310;
		public  const int WM_PALETTECHANGED         = 0x0311;
		public  const int WM_HOTKEY                 = 0x0312;
		public  const int WM_PRINT                  = 0x0317;
		public  const int WM_PRINTCLIENT            = 0x0318;
		public  const int WM_HANDHELDFIRST          = 0x0358;
		public  const int WM_HANDHELDLAST           = 0x035F;
		public  const int WM_AFXFIRST               = 0x0360;
		public  const int WM_AFXLAST                = 0x037F;
		public  const int WM_PENWINFIRST            = 0x0380;
		public  const int WM_PENWINLAST             = 0x038F;
		public  const int WM_APP                    = 0x8000;
		public  const int WM_USER                   = 0x0400;
   	
	}
	#endregion

	#region ScrollBarTypes

	public enum ScrollBarTypes
	{
		SB_HORZ = 0,
		SB_VERT = 1,
		SB_CTL = 2,
		SB_BOTH = 3
	}

	#endregion

	#region MouseActivateFlags	
	
	public enum MouseActivateFlags
	{
		MA_ACTIVATE			= 1,
		MA_ACTIVATEANDEAT   = 2,
		MA_NOACTIVATE       = 3,
		MA_NOACTIVATEANDEAT = 4
	}

	#endregion

	#region Msg

    //[CLSCompliantAttribute(false)]
    public enum Msg
	{
		WmMouseActivate = 0x0021,
		WmUser = 0x0400,
		WmMouseWhell = 0x020A,
		WmKeyDown = 0x0100,
		WmKeyUp = 0x0101,
		WmChar = 0x0102,
		WmlButtonUp = 0x0202,
		WmlButtonDown = 0x0201,
		WmmButtonDown = 0x0207,
		WmmButtonUp = 0x0208,
		WmlButtonDblClk = 0x0203,
		WmReflect = WmUser + 0x1c00
	}

	#endregion

	#region WindowExStyles
	
	public enum WindowExStyles
	{
		WS_EX_DLGMODALFRAME     = 0x00000001,
		WS_EX_NOPARENTNOTIFY    = 0x00000004,
		WS_EX_TOPMOST           = 0x00000008,
		WS_EX_ACCEPTFILES       = 0x00000010,
		WS_EX_TRANSPARENT       = 0x00000020,
		WS_EX_MDICHILD          = 0x00000040,
		WS_EX_TOOLWINDOW        = 0x00000080,
		WS_EX_WINDOWEDGE        = 0x00000100,
		WS_EX_CLIENTEDGE        = 0x00000200,
		WS_EX_CONTEXTHELP       = 0x00000400,
		WS_EX_RIGHT             = 0x00001000,
		WS_EX_LEFT              = 0x00000000,
		WS_EX_RTLREADING        = 0x00002000,
		WS_EX_LTRREADING        = 0x00000000,
		WS_EX_LEFTSCROLLBAR     = 0x00004000,
		WS_EX_RIGHTSCROLLBAR    = 0x00000000,
		WS_EX_CONTROLPARENT     = 0x00010000,
		WS_EX_STATICEDGE        = 0x00020000,
		WS_EX_APPWINDOW         = 0x00040000,
		WS_EX_OVERLAPPEDWINDOW  = 0x00000300,
		WS_EX_PALETTEWINDOW     = 0x00000188,
		WS_EX_LAYERED			= 0x00080000
	}

	#endregion

	#region CombineFlags
	

	public enum CombineFlags
	{
		RGN_AND		= 1,
		RGN_OR		= 2,
		RGN_XOR		= 3,
		RGN_DIFF	= 4,
		RGN_COPY	= 5
	}

	#endregion

	#region RasterOperations

	//[CLSCompliantAttribute(false)]
	public enum RasterOperations : uint
	{
		SRCCOPY		= 0x00CC0020,
		SRCPAINT	= 0x00EE0086,
		SRCAND		= 0x008800C6,
		SRCINVERT	= 0x00660046,
		SRCERASE	= 0x00440328,
		NOTSRCCOPY	= 0x00330008,
		NOTSRCERASE = 0x001100A6,
		MERGECOPY	= 0x00C000CA,
		MERGEPAINT	= 0x00BB0226,
		PATCOPY		= 0x00F00021,
		PATPAINT	= 0x00FB0A09,
		PATINVERT	= 0x005A0049,
		DSTINVERT	= 0x00550009,
		BLACKNESS	= 0x00000042,
		WHITENESS	= 0x00FF0062
	}

	#endregion

	#region BrushStyles
	
	public enum BrushStyles
	{
		BS_SOLID			= 0,
		BS_NULL             = 1,
		BS_HOLLOW           = 1,
		BS_HATCHED          = 2,
		BS_PATTERN          = 3,
		BS_INDEXED          = 4,
		BS_DIBPATTERN       = 5,
		BS_DIBPATTERNPT     = 6,
		BS_PATTERN8X8       = 7,
		BS_DIBPATTERN8X8    = 8,
		BS_MONOPATTERN      = 9
	}

	#endregion
	
	#region Edit control Styles
	public enum EditControlStyles
	{
		ES_LEFT             = 0x0000,
		ES_CENTER           = 0x0001,
		ES_RIGHT            = 0x0002,
		ES_MULTILINE        = 0x0004,
		ES_UPPERCASE        = 0x0008,
		ES_LOWERCASE        = 0x0010,
		ES_PASSWORD         = 0x0020,
		ES_AUTOVSCROLL      = 0x0040,
		ES_AUTOHSCROLL      = 0x0080,
		ES_NOHIDESEL        = 0x0100,
		ES_OEMCONVERT       = 0x0400,
		ES_READONLY         = 0x0800,
		ES_WANTRETURN       = 0x1000,
		ES_NUMBER           = 0x2000
	}
	#endregion

	#region Spin Control styles
	public enum SpinControlStyles
	{
		UDS_WRAP                = 0x0001,
		UDS_SETBUDDYINT         = 0x0002,
		UDS_ALIGNRIGHT          = 0x0004,
		UDS_ALIGNLEFT           = 0x0008,
		UDS_AUTOBUDDY           = 0x0010,
		UDS_ARROWKEYS           = 0x0020,
		UDS_HORZ                = 0x0040,
		UDS_NOTHOUSANDS         = 0x0080,
		UDS_HOTTRACK            = 0x0100
	}
	#endregion

	#region Edit Control Messages
	public enum EditConrolNotifyMsg
	{
		EN_SETFOCUS         = 0x0100,
		EN_KILLFOCUS        = 0x0200,
		EN_CHANGE           = 0x0300,
		EN_UPDATE           = 0x0400,
		EN_ERRSPACE         = 0x0500,
		EN_MAXTEXT          = 0x0501,
		EN_HSCROLL          = 0x0601,
		EN_VSCROLL          = 0x0602
	}

	public enum EditControlSetMargin
	{
		EC_LEFTMARGIN       = 0x0001,
		EC_RIGHTMARGIN      = 0x0002,
		EC_USEFONTINFO      = 0xffff
	}

	public enum EditControlMsg
	{
		EM_GETSEL               = 0x00B0,
		EM_SETSEL               = 0x00B1,
		EM_GETRECT              = 0x00B2,
		EM_SETRECT              = 0x00B3,
		EM_SETRECTNP            = 0x00B4,
		EM_SCROLL               = 0x00B5,
		EM_LINESCROLL           = 0x00B6,
		EM_SCROLLCARET          = 0x00B7,
		EM_GETMODIFY            = 0x00B8,
		EM_SETMODIFY            = 0x00B9,
		EM_GETLINECOUNT         = 0x00BA,
		EM_LINEINDEX            = 0x00BB,
		EM_SETHANDLE            = 0x00BC,
		EM_GETHANDLE            = 0x00BD,
		EM_GETTHUMB             = 0x00BE,
		EM_LINELENGTH           = 0x00C1,
		EM_REPLACESEL           = 0x00C2,
		EM_GETLINE              = 0x00C4,
		EM_LIMITTEXT            = 0x00C5,
		EM_CANUNDO              = 0x00C6,
		EM_UNDO                 = 0x00C7,
		EM_FMTLINES             = 0x00C8,
		EM_LINEFROMCHAR         = 0x00C9,
		EM_SETTABSTOPS          = 0x00CB,
		EM_SETPASSWORDCHAR      = 0x00CC,
		EM_EMPTYUNDOBUFFER      = 0x00CD,
		EM_GETFIRSTVISIBLELINE  = 0x00CE,
		EM_SETREADONLY          = 0x00CF,
		EM_SETWORDBREAKPROC     = 0x00D0,
		EM_GETWORDBREAKPROC     = 0x00D1,
		EM_GETPASSWORDCHAR      = 0x00D2,
		EM_SETMARGINS           = 0x00D3,
		EM_GETMARGINS           = 0x00D4,
		EM_SETLIMITTEXT         = EM_LIMITTEXT,   /* ;win40 Name change */
		EM_GETLIMITTEXT         = 0x00D5,
		EM_POSFROMCHAR          = 0x00D6,
		EM_CHARFROMPOS          = 0x00D7,
		EM_SETIMESTATUS         = 0x00D8,
		EM_GETIMESTATUS         = 0x00D9
	}
	#endregion

	#region Spin Control Messages
	public enum SpinControlMsg : int
	{
		WM_USER                 = 0x0400,
		CCM_FIRST               = 0x2000,
		CCM_SETUNICODEFORMAT    = (CCM_FIRST + 5),
		CCM_GETUNICODEFORMAT    = (CCM_FIRST + 6),
		UDM_SETRANGE            = (WM_USER+101),
		UDM_GETRANGE            = (WM_USER+102),
		UDM_SETPOS              = (WM_USER+103),
		UDM_GETPOS              = (WM_USER+104),
		UDM_SETBUDDY            = (WM_USER+105),
		UDM_GETBUDDY            = (WM_USER+106),
		UDM_SETACCEL            = (WM_USER+107),
		UDM_GETACCEL            = (WM_USER+108),
		UDM_SETBASE             = (WM_USER+109),
		UDM_GETBASE             = (WM_USER+110),
		UDM_SETRANGE32          = (WM_USER+111),
		UDM_GETRANGE32          = (WM_USER+112), // wParam & lParam are LPINT
		UDM_SETUNICODEFORMAT    = CCM_SETUNICODEFORMAT,
		UDM_GETUNICODEFORMAT    = CCM_GETUNICODEFORMAT,
		UDM_SETPOS32            = (WM_USER+113),
		UDM_GETPOS32            = (WM_USER+114),
	}
	#endregion

	#region SetWindowPos Z Order
	public enum SetWindowPosZOrder
	{
		HWND_TOP        = 0,
		HWND_BOTTOM     = 1,
		HWND_TOPMOST    = -1,
		HWND_NOTOPMOST  = -2
	}
	#endregion

	#region PatBlt Types
	public enum PatBltTypes
	{
		SRCCOPY          =   0x00CC0020,
		SRCPAINT         =   0x00EE0086,
		SRCAND           =   0x008800C6,
		SRCINVERT        =   0x00660046,
		SRCERASE         =   0x00440328,
		NOTSRCCOPY       =   0x00330008,
		NOTSRCERASE      =   0x001100A6,
		MERGECOPY        =   0x00C000CA,
		MERGEPAINT       =   0x00BB0226,
		PATCOPY          =   0x00F00021,
		PATPAINT         =   0x00FB0A09,
		PATINVERT        =   0x005A0049,
		DSTINVERT        =   0x00550009,
		BLACKNESS        =   0x00000042,
		WHITENESS        =   0x00FF0062
	}
	#endregion
	
	#region Clipboard Formats
	
    //[CLSCompliantAttribute(false)]
	public enum ClipboardFormats : uint
	{			
		CF_TEXT             = 1,
		CF_BITMAP           = 2,
		CF_METAFILEPICT     = 3,
		CF_SYLK             = 4,
		CF_DIF              = 5,
		CF_TIFF             = 6,
		CF_OEMTEXT          = 7,
		CF_DIB              = 8,
		CF_PALETTE          = 9,
		CF_PENDATA          = 10,
		CF_RIFF             = 11,
		CF_WAVE             = 12,
		CF_UNICODETEXT      = 13,
		CF_ENHMETAFILE      = 14,
		CF_HDROP            = 15,
		CF_LOCALE           = 16,
		CF_MAX              = 17,
		CF_OWNERDISPLAY     = 0x0080,
		CF_DSPTEXT          = 0x0081,
		CF_DSPBITMAP        = 0x0082,
		CF_DSPMETAFILEPICT  = 0x0083,
		CF_DSPENHMETAFILE   = 0x008E,
		CF_PRIVATEFIRST     = 0x0200,
		CF_PRIVATELAST      = 0x02FF,
		CF_GDIOBJFIRST      = 0x0300,
		CF_GDIOBJLAST       = 0x03FF
	}
	#endregion

	#region Common Controls Initialization flags
	public enum CommonControlInitFlags
	{
		ICC_LISTVIEW_CLASSES   = 0x00000001, 
		ICC_TREEVIEW_CLASSES   = 0x00000002, 
		ICC_BAR_CLASSES        = 0x00000004, 
		ICC_TAB_CLASSES        = 0x00000008, 
		ICC_UPDOWN_CLASS       = 0x00000010, 
		ICC_PROGRESS_CLASS     = 0x00000020, 
		ICC_HOTKEY_CLASS       = 0x00000040, 
		ICC_ANIMATE_CLASS      = 0x00000080, 
		ICC_WIN95_CLASSES      = 0x000000FF,
		ICC_DATE_CLASSES       = 0x00000100, 
		ICC_USEREX_CLASSES     = 0x00000200,
		ICC_COOL_CLASSES       = 0x00000400, 
		ICC_INTERNET_CLASSES   = 0x00000800,
		ICC_PAGESCROLLER_CLASS = 0x00001000, 
		ICC_NATIVEFNTCTL_CLASS = 0x00002000  
	}
	#endregion

	#region Common Controls Styles
	public  enum CommonControlStyles
	{
		CCS_TOP                 = 0x00000001,
		CCS_NOMOVEY             = 0x00000002,
		CCS_BOTTOM              = 0x00000003,
		CCS_NORESIZE            = 0x00000004,
		CCS_NOPARENTALIGN       = 0x00000008,
		CCS_ADJUSTABLE          = 0x00000020,
		CCS_NODIVIDER           = 0x00000040,
		CCS_VERT                = 0x00000080,
		CCS_LEFT                = (CCS_VERT | CCS_TOP),
		CCS_RIGHT               = (CCS_VERT | CCS_BOTTOM),
		CCS_NOMOVEX             = (CCS_VERT | CCS_NOMOVEY)
	}
	#endregion

	#region ToolBar Styles
	public enum ToolBarStyles
	{
		TBSTYLE_BUTTON          = 0x0000,
		TBSTYLE_SEP             = 0x0001,
		TBSTYLE_CHECK           = 0x0002,
		TBSTYLE_GROUP           = 0x0004,
		TBSTYLE_CHECKGROUP      = (TBSTYLE_GROUP | TBSTYLE_CHECK),
		TBSTYLE_DROPDOWN        = 0x0008,
		TBSTYLE_AUTOSIZE        = 0x0010,
		TBSTYLE_NOPREFIX        = 0x0020, 
		TBSTYLE_TOOLTIPS        = 0x0100,
		TBSTYLE_WRAPABLE        = 0x0200,
		TBSTYLE_ALTDRAG         = 0x0400,
		TBSTYLE_FLAT            = 0x0800,
		TBSTYLE_LIST            = 0x1000,
		TBSTYLE_CUSTOMERASE     = 0x2000,
		TBSTYLE_REGISTERDROP    = 0x4000,
		TBSTYLE_TRANSPARENT     = 0x8000,
		TBSTYLE_EX_DRAWDDARROWS = 0x00000001
	}
	#endregion

	#region ToolBar Ex Styles
	public enum ToolBarExStyles
	{
		TBSTYLE_EX_DRAWDDARROWS			= 0x1,
		TBSTYLE_EX_HIDECLIPPEDBUTTONS	= 0x10,
		TBSTYLE_EX_DOUBLEBUFFER			= 0x80
	}
	#endregion

	#region ToolBar Messages
	public enum ToolBarMessages
	{
		WM_USER                 =  0x0400,
		TB_ENABLEBUTTON         = (WM_USER + 1),
		TB_CHECKBUTTON          = (WM_USER + 2),
		TB_PRESSBUTTON          = (WM_USER + 3),
		TB_HIDEBUTTON           = (WM_USER + 4),
		TB_INDETERMINATE        = (WM_USER + 5),
		TB_MARKBUTTON           = (WM_USER + 6),
		TB_ISBUTTONENABLED      = (WM_USER + 9),
		TB_ISBUTTONCHECKED      = (WM_USER + 10),
		TB_ISBUTTONPRESSED      = (WM_USER + 11),
		TB_ISBUTTONHIDDEN       = (WM_USER + 12),
		TB_ISBUTTONINDETERMINATE= (WM_USER + 13),
		TB_ISBUTTONHIGHLIGHTED  = (WM_USER + 14),
		TB_SETSTATE             = (WM_USER + 17),
		TB_GETSTATE             = (WM_USER + 18),
		TB_ADDBITMAP            = (WM_USER + 19),
		TB_ADDBUTTONSA          = (WM_USER + 20),
		TB_INSERTBUTTONA        = (WM_USER + 21),
		TB_ADDBUTTONS           = (WM_USER + 20),
		TB_INSERTBUTTON         = (WM_USER + 21),
		TB_DELETEBUTTON         = (WM_USER + 22),
		TB_GETBUTTON            = (WM_USER + 23),
		TB_BUTTONCOUNT          = (WM_USER + 24),
		TB_COMMANDTOINDEX       = (WM_USER + 25),
		TB_SAVERESTOREA         = (WM_USER + 26),
		TB_CUSTOMIZE            = (WM_USER + 27),
		TB_ADDSTRINGA           = (WM_USER + 28),
		TB_GETITEMRECT          = (WM_USER + 29),
		TB_BUTTONSTRUCTSIZE     = (WM_USER + 30),
		TB_SETBUTTONSIZE        = (WM_USER + 31),
		TB_SETBITMAPSIZE        = (WM_USER + 32),
		TB_AUTOSIZE             = (WM_USER + 33),
		TB_GETTOOLTIPS          = (WM_USER + 35),
		TB_SETTOOLTIPS          = (WM_USER + 36),
		TB_SETPARENT            = (WM_USER + 37),
		TB_SETROWS              = (WM_USER + 39),
		TB_GETROWS              = (WM_USER + 40),
		TB_GETBITMAPFLAGS       = (WM_USER + 41),
		TB_SETCMDID             = (WM_USER + 42),
		TB_CHANGEBITMAP         = (WM_USER + 43),
		TB_GETBITMAP            = (WM_USER + 44),
		TB_GETBUTTONTEXTA       = (WM_USER + 45),
		TB_GETBUTTONTEXTW       = (WM_USER + 75),
		TB_REPLACEBITMAP        = (WM_USER + 46),
		TB_SETINDENT            = (WM_USER + 47),
		TB_SETIMAGELIST         = (WM_USER + 48),
		TB_GETIMAGELIST         = (WM_USER + 49),
		TB_LOADIMAGES           = (WM_USER + 50),
		TB_GETRECT              = (WM_USER + 51),
		TB_SETHOTIMAGELIST      = (WM_USER + 52),
		TB_GETHOTIMAGELIST      = (WM_USER + 53),
		TB_SETDISABLEDIMAGELIST = (WM_USER + 54),
		TB_GETDISABLEDIMAGELIST = (WM_USER + 55),
		TB_SETSTYLE             = (WM_USER + 56),
		TB_GETSTYLE             = (WM_USER + 57),
		TB_GETBUTTONSIZE        = (WM_USER + 58),
		TB_SETBUTTONWIDTH       = (WM_USER + 59),
		TB_SETMAXTEXTROWS       = (WM_USER + 60),
		TB_GETTEXTROWS          = (WM_USER + 61),
		TB_GETOBJECT            = (WM_USER + 62), 
		TB_GETBUTTONINFOW       = (WM_USER + 63),
		TB_SETBUTTONINFOW       = (WM_USER + 64),
		TB_GETBUTTONINFOA       = (WM_USER + 65),
		TB_SETBUTTONINFOA       = (WM_USER + 66),
		TB_INSERTBUTTONW        = (WM_USER + 67),
		TB_ADDBUTTONSW          = (WM_USER + 68),
		TB_HITTEST              = (WM_USER + 69),
		TB_SETDRAWTEXTFLAGS     = (WM_USER + 70),
		TB_GETHOTITEM           = (WM_USER + 71),
		TB_SETHOTITEM           = (WM_USER + 72), 
		TB_SETANCHORHIGHLIGHT   = (WM_USER + 73),  
		TB_GETANCHORHIGHLIGHT   = (WM_USER + 74),
		TB_SAVERESTOREW         = (WM_USER + 76),
		TB_ADDSTRINGW           = (WM_USER + 77),
		TB_MAPACCELERATORA      = (WM_USER + 78), 
		TB_GETINSERTMARK        = (WM_USER + 79), 
		TB_SETINSERTMARK        = (WM_USER + 80), 
		TB_INSERTMARKHITTEST    = (WM_USER + 81),  
		TB_MOVEBUTTON           = (WM_USER + 82),
		TB_GETMAXSIZE           = (WM_USER + 83),  
		TB_SETEXTENDEDSTYLE     = (WM_USER + 84),  
		TB_GETEXTENDEDSTYLE     = (WM_USER + 85),  
		TB_GETPADDING           = (WM_USER + 86),
		TB_SETPADDING           = (WM_USER + 87),
		TB_SETINSERTMARKCOLOR   = (WM_USER + 88),
		TB_GETINSERTMARKCOLOR   = (WM_USER + 89)
	}
	#endregion

	#region ToolBar Notifications
	public enum ToolBarNotifications
	{
		TTN_NEEDTEXTA		  = ((0-520)-0),
		TTN_NEEDTEXTW		  = ((0-520)-10),
		TBN_QUERYINSERT		= ((0-700)-6),
		TBN_DROPDOWN		  = ((0-700)-10),
		TBN_HOTITEMCHANGE	= ((0 - 700) - 13)
	}
	#endregion

	#region Reflected Messages
	public enum ReflectedMessages
	{
		OCM__BASE				      = (Msgs.WM_USER+0x1c00),
		OCM_COMMAND				    = (OCM__BASE + Msgs.WM_COMMAND),
		OCM_CTLCOLORBTN			  = (OCM__BASE + Msgs.WM_CTLCOLORBTN),
		OCM_CTLCOLOREDIT		  = (OCM__BASE + Msgs.WM_CTLCOLOREDIT),
		OCM_CTLCOLORDLG			  = (OCM__BASE + Msgs.WM_CTLCOLORDLG),
		OCM_CTLCOLORLISTBOX		= (OCM__BASE + Msgs.WM_CTLCOLORLISTBOX),
		OCM_CTLCOLORMSGBOX		= (OCM__BASE + Msgs.WM_CTLCOLORMSGBOX),
		OCM_CTLCOLORSCROLLBAR = (OCM__BASE + Msgs.WM_CTLCOLORSCROLLBAR),
		OCM_CTLCOLORSTATIC		= (OCM__BASE + Msgs.WM_CTLCOLORSTATIC),
		OCM_CTLCOLOR			    = (OCM__BASE + Msgs.WM_CTLCOLOR),
		OCM_DRAWITEM			    = (OCM__BASE + Msgs.WM_DRAWITEM),
		OCM_MEASUREITEM			  = (OCM__BASE + Msgs.WM_MEASUREITEM),
		OCM_DELETEITEM        = (OCM__BASE + Msgs.WM_DELETEITEM),
		OCM_VKEYTOITEM        = (OCM__BASE + Msgs.WM_VKEYTOITEM),
		OCM_CHARTOITEM        = (OCM__BASE + Msgs.WM_CHARTOITEM),
		OCM_COMPAREITEM       = (OCM__BASE + Msgs.WM_COMPAREITEM),
		OCM_HSCROLL           = (OCM__BASE + Msgs.WM_HSCROLL),
		OCM_VSCROLL           = (OCM__BASE + Msgs.WM_VSCROLL),
		OCM_PARENTNOTIFY      = (OCM__BASE + Msgs.WM_PARENTNOTIFY),
		OCM_NOTIFY            = (OCM__BASE + Msgs.WM_NOTIFY)
	}
	#endregion

	#region Notification Messages
	public enum NotificationMessages
	{
		NM_FIRST      = (0-0),
		NM_CUSTOMDRAW = (NM_FIRST-12),
		NM_NCHITTEST  = (NM_FIRST-14) 
	}
	#endregion

	#region ToolTip Flags
	public enum ToolTipFlags
	{
		TTF_CENTERTIP           = 0x0002,
		TTF_RTLREADING          = 0x0004,
		TTF_SUBCLASS            = 0x0010,
		TTF_TRACK               = 0x0020,
		TTF_ABSOLUTE            = 0x0080,
		TTF_TRANSPARENT         = 0x0100,
		TTF_DI_SETITEM          = 0x8000   
	}
	#endregion

	#region Custom Draw Return Flags
	public enum CustomDrawReturnFlags
	{
		CDRF_DODEFAULT          = 0x00000000,
		CDRF_NEWFONT            = 0x00000002,
		CDRF_SKIPDEFAULT        = 0x00000004,
		CDRF_NOTIFYPOSTPAINT    = 0x00000010,
		CDRF_NOTIFYITEMDRAW     = 0x00000020,
		CDRF_NOTIFYSUBITEMDRAW  = 0x00000020, 
		CDRF_NOTIFYPOSTERASE    = 0x00000040
	}
	#endregion

	#region Custom Draw Item State Flags
	public enum CustomDrawItemStateFlags
	{
		CDIS_SELECTED       = 0x0001,
		CDIS_GRAYED         = 0x0002,
		CDIS_DISABLED       = 0x0004,
		CDIS_CHECKED        = 0x0008,
		CDIS_FOCUS          = 0x0010,
		CDIS_DEFAULT        = 0x0020,
		CDIS_HOT            = 0x0040,
		CDIS_MARKED         = 0x0080,
		CDIS_INDETERMINATE  = 0x0100
	}
	#endregion

	#region Custom Draw Draw State Flags
	public enum CustomDrawDrawStateFlags
	{
		CDDS_PREPAINT           = 0x00000001,
		CDDS_POSTPAINT          = 0x00000002,
		CDDS_PREERASE           = 0x00000003,
		CDDS_POSTERASE          = 0x00000004,
		CDDS_ITEM               = 0x00010000,
		CDDS_ITEMPREPAINT       = (CDDS_ITEM | CDDS_PREPAINT),
		CDDS_ITEMPOSTPAINT      = (CDDS_ITEM | CDDS_POSTPAINT),
		CDDS_ITEMPREERASE       = (CDDS_ITEM | CDDS_PREERASE),
		CDDS_ITEMPOSTERASE      = (CDDS_ITEM | CDDS_POSTERASE),
		CDDS_SUBITEM            = 0x00020000
	}
	#endregion

	#region Toolbar button info flags
	public enum ToolBarButtonInfoFlags
	{
		TBIF_IMAGE             = 0x00000001,
		TBIF_TEXT              = 0x00000002,
		TBIF_STATE             = 0x00000004,
		TBIF_STYLE             = 0x00000008,
		TBIF_LPARAM            = 0x00000010,
		TBIF_COMMAND           = 0x00000020,
		TBIF_SIZE              = 0x00000040,
		I_IMAGECALLBACK        = -1,
		I_IMAGENONE            = -2
	}
	#endregion

	#region Toolbar button styles
	public enum ToolBarButtonStyles
	{
		TBSTYLE_BUTTON          = 0x0000,
		TBSTYLE_SEP             = 0x0001,
		TBSTYLE_CHECK           = 0x0002,
		TBSTYLE_GROUP           = 0x0004,
		TBSTYLE_CHECKGROUP      = (TBSTYLE_GROUP | TBSTYLE_CHECK),
		TBSTYLE_DROPDOWN        = 0x0008,
		TBSTYLE_AUTOSIZE        = 0x0010,
		TBSTYLE_NOPREFIX        = 0x0020, 
		TBSTYLE_TOOLTIPS        = 0x0100,
		TBSTYLE_WRAPABLE        = 0x0200,
		TBSTYLE_ALTDRAG         = 0x0400,
		TBSTYLE_FLAT            = 0x0800,
		TBSTYLE_LIST            = 0x1000,
		TBSTYLE_CUSTOMERASE     = 0x2000,
		TBSTYLE_REGISTERDROP    = 0x4000,
		TBSTYLE_TRANSPARENT     = 0x8000,
		TBSTYLE_EX_DRAWDDARROWS = 0x00000001
	}
	#endregion

	#region Toolbar button state
	public enum ToolBarButtonStates
	{
		TBSTATE_CHECKED         = 0x01,
		TBSTATE_PRESSED         = 0x02,
		TBSTATE_ENABLED         = 0x04,
		TBSTATE_HIDDEN          = 0x08,
		TBSTATE_INDETERMINATE   = 0x10,
		TBSTATE_WRAP            = 0x20,
		TBSTATE_ELLIPSES        = 0x40,
		TBSTATE_MARKED          = 0x80
	}
	#endregion

	#region Mouse Hook Filters
	public enum MouseHookFilters
	{
		MSGF_DIALOGBOX      = 0,
		MSGF_MESSAGEBOX     = 1,
		MSGF_MENU           = 2,
		MSGF_SCROLLBAR      = 5,
		MSGF_NEXTWINDOW     = 6
	}

	#endregion

	#region Draw Text format flags
	public enum DrawTextFormatFlags
	{
		DT_TOP              = 0x00000000,
		DT_LEFT             = 0x00000000,
		DT_CENTER           = 0x00000001,
		DT_RIGHT            = 0x00000002,
		DT_VCENTER          = 0x00000004,
		DT_BOTTOM           = 0x00000008,
		DT_WORDBREAK        = 0x00000010,
		DT_SINGLELINE       = 0x00000020,
		DT_EXPANDTABS       = 0x00000040,
		DT_TABSTOP          = 0x00000080,
		DT_NOCLIP           = 0x00000100,
		DT_EXTERNALLEADING  = 0x00000200,
		DT_CALCRECT         = 0x00000400,
		DT_NOPREFIX         = 0x00000800,
		DT_INTERNAL         = 0x00001000,
		DT_EDITCONTROL      = 0x00002000,
		DT_PATH_ELLIPSIS    = 0x00004000,
		DT_END_ELLIPSIS     = 0x00008000,
		DT_MODIFYSTRING     = 0x00010000,
		DT_RTLREADING       = 0x00020000,
		DT_WORD_ELLIPSIS    = 0x00040000
	}

	#endregion

	#region Rebar Styles
	public enum RebarStyles
	{
		RBS_TOOLTIPS        = 0x0100,
		RBS_VARHEIGHT       = 0x0200,
		RBS_BANDBORDERS     = 0x0400,
		RBS_FIXEDORDER      = 0x0800,
		RBS_REGISTERDROP    = 0x1000,
		RBS_AUTOSIZE        = 0x2000,
		RBS_VERTICALGRIPPER = 0x4000, 
		RBS_DBLCLKTOGGLE    = 0x8000,
	}
	#endregion

	#region Rebar Notifications
	public enum RebarNotifications 
	{
		RBN_FIRST           = (0-831),
		RBN_HEIGHTCHANGE    = (RBN_FIRST - 0),
		RBN_GETOBJECT       = (RBN_FIRST - 1),
		RBN_LAYOUTCHANGED   = (RBN_FIRST - 2),
		RBN_AUTOSIZE        = (RBN_FIRST - 3),
		RBN_BEGINDRAG       = (RBN_FIRST - 4),
		RBN_ENDDRAG         = (RBN_FIRST - 5),
		RBN_DELETINGBAND    = (RBN_FIRST - 6),   
		RBN_DELETEDBAND     = (RBN_FIRST - 7),    
		RBN_CHILDSIZE       = (RBN_FIRST - 8),
		RBN_CHEVRONPUSHED   = (RBN_FIRST - 10)
	}
	#endregion

	#region Rebar Messages
	public enum RebarMessages
	{
		CCM_FIRST           =    0x2000,
		WM_USER             =    0x0400,
		RB_INSERTBANDA		=	(WM_USER +  1),
		RB_DELETEBAND       =	(WM_USER +  2),
		RB_GETBARINFO		=	(WM_USER +  3),
		RB_SETBARINFO		=	(WM_USER +  4),
		RB_GETBANDINFO		=	(WM_USER +  5),
		RB_SETBANDINFOA		=	(WM_USER +  6),
		RB_SETPARENT		=	(WM_USER +  7),
		RB_HITTEST			=	(WM_USER +  8),
		RB_GETRECT			=	(WM_USER +  9),
		RB_INSERTBANDW		=	(WM_USER +  10),
		RB_SETBANDINFOW		=	(WM_USER +  11),
		RB_GETBANDCOUNT		=	(WM_USER +  12),
		RB_GETROWCOUNT		=	(WM_USER +  13),
		RB_GETROWHEIGHT		=	(WM_USER +  14),
		RB_IDTOINDEX		=	(WM_USER +  16),
		RB_GETTOOLTIPS		=	(WM_USER +  17),
		RB_SETTOOLTIPS		=	(WM_USER +  18),
		RB_SETBKCOLOR		=	(WM_USER +  19),
		RB_GETBKCOLOR		=	(WM_USER +  20), 
		RB_SETTEXTCOLOR		=	(WM_USER +  21),
		RB_GETTEXTCOLOR		=	(WM_USER +  22),
		RB_SIZETORECT		=	(WM_USER +  23), 
		RB_SETCOLORSCHEME	=	(CCM_FIRST + 2),  
		RB_GETCOLORSCHEME	=	(CCM_FIRST + 3), 
		RB_BEGINDRAG		=	(WM_USER + 24),
		RB_ENDDRAG			=	(WM_USER + 25),
		RB_DRAGMOVE			=	(WM_USER + 26),
		RB_GETBARHEIGHT		=	(WM_USER + 27),
		RB_GETBANDINFOW		=	(WM_USER + 28),
		RB_GETBANDINFOA		=	(WM_USER + 29),
		RB_MINIMIZEBAND		=	(WM_USER + 30),
		RB_MAXIMIZEBAND		=	(WM_USER + 31),
		RB_GETDROPTARGET	=	(CCM_FIRST + 4),
		RB_GETBANDBORDERS	=	(WM_USER + 34),  
		RB_SHOWBAND			=	(WM_USER + 35),      
		RB_SETPALETTE		=	(WM_USER + 37),
		RB_GETPALETTE		=	(WM_USER + 38),
		RB_MOVEBAND			=	(WM_USER + 39),
		RB_SETUNICODEFORMAT =   (CCM_FIRST + 5),
		RB_GETUNICODEFORMAT =   (CCM_FIRST + 6)
	}
	#endregion

	#region Rebar Info Mask
	public enum RebarInfoMask
	{
		RBBIM_STYLE         = 0x00000001,
		RBBIM_COLORS        = 0x00000002,
		RBBIM_TEXT          = 0x00000004,
		RBBIM_IMAGE         = 0x00000008,
		RBBIM_CHILD         = 0x00000010,
		RBBIM_CHILDSIZE     = 0x00000020,
		RBBIM_SIZE          = 0x00000040,
		RBBIM_BACKGROUND    = 0x00000080,
		RBBIM_ID            = 0x00000100,
		RBBIM_IDEALSIZE     = 0x00000200,
		RBBIM_LPARAM        = 0x00000400,
		BBIM_HEADERSIZE     = 0x00000800  
	}
	#endregion

	#region Rebar Styles
	public enum RebarStylesEx
	{
		RBBS_BREAK			=	0x1,
		RBBS_CHILDEDGE		=	0x4,
		RBBS_FIXEDBMP		=	0x20,
		RBBS_GRIPPERALWAYS	=	0x80,
		RBBS_USECHEVRON		=	0x200
	}
	#endregion

	#region Object types
	public enum ObjectTypes
	{
		OBJ_PEN             = 1,
		OBJ_BRUSH           = 2,
		OBJ_DC              = 3,
		OBJ_METADC          = 4,
		OBJ_PAL             = 5,
		OBJ_FONT            = 6,
		OBJ_BITMAP          = 7,
		OBJ_REGION          = 8,
		OBJ_METAFILE        = 9,
		OBJ_MEMDC           = 10,
		OBJ_EXTPEN          = 11,
		OBJ_ENHMETADC       = 12,
		OBJ_ENHMETAFILE     = 13
	}
	#endregion

	#region WM_MENUCHAR return values
	public enum MenuCharReturnValues
	{
		MNC_IGNORE  = 0,
		MNC_CLOSE   = 1,
		MNC_EXECUTE = 2,
		MNC_SELECT  = 3
	}
	#endregion

	#region Background Mode
	public enum BackgroundMode
	{
		TRANSPARENT = 1,
		OPAQUE = 2
	}
	#endregion

	#region ListView Messages
	public enum ListViewMessages
	{
		LVM_FIRST           =    0x1000,
		LVM_GETSUBITEMRECT  = (LVM_FIRST + 56),
		LVM_GETITEMSTATE    = (LVM_FIRST + 44),
		LVM_GETITEMTEXTW    = (LVM_FIRST + 115)
	}
	#endregion

	#region Header Control Messages
	public enum HeaderControlMessages : int
	{
		HDM_FIRST        =  0x1200,
		HDM_GETITEMRECT  = (HDM_FIRST + 7),
		HDM_HITTEST      = (HDM_FIRST + 6),
		HDM_SETIMAGELIST = (HDM_FIRST + 8),
		HDM_GETITEMW     = (HDM_FIRST + 11),
		HDM_ORDERTOINDEX = (HDM_FIRST + 15)
	}
	#endregion

	#region Header Control Notifications
	public enum HeaderControlNotifications
	{
		HDN_FIRST       = (0-300),
		HDN_BEGINTRACKW = (HDN_FIRST-26),
		HDN_ENDTRACKW   = (HDN_FIRST-27),
		HDN_ITEMCLICKW  = (HDN_FIRST-22),
	}
	#endregion

	#region Header Control HitTest Flags
	//[CLSCompliantAttribute(false)]
	public enum HeaderControlHitTestFlags : uint
	{
		HHT_NOWHERE             = 0x0001,
		HHT_ONHEADER            = 0x0002,
		HHT_ONDIVIDER           = 0x0004,
		HHT_ONDIVOPEN           = 0x0008,
		HHT_ABOVE               = 0x0100,
		HHT_BELOW               = 0x0200,
		HHT_TORIGHT             = 0x0400,
		HHT_TOLEFT              = 0x0800
	}
	#endregion

	#region List View sub item portion
	public enum SubItemPortion
	{
		LVIR_BOUNDS = 0,
		LVIR_ICON   = 1,
		LVIR_LABEL  = 2
	}
	#endregion

	#region Cursor Type
	//[CLSCompliantAttribute(false)]
	public enum CursorType : uint
	{
		IDC_ARROW		= 32512U,
		IDC_IBEAM       = 32513U,
		IDC_WAIT        = 32514U,
		IDC_CROSS       = 32515U,
		IDC_UPARROW     = 32516U,
		IDC_SIZE        = 32640U,
		IDC_ICON        = 32641U,
		IDC_SIZENWSE    = 32642U,
		IDC_SIZENESW    = 32643U,
		IDC_SIZEWE      = 32644U,
		IDC_SIZENS      = 32645U,
		IDC_SIZEALL     = 32646U,
		IDC_NO          = 32648U,
		IDC_HAND        = 32649U,
		IDC_APPSTARTING = 32650U,
		IDC_HELP        = 32651U
	}
	#endregion

	#region ComboBox messages
	public enum ComboBoxMessages
	{
		CB_GETDROPPEDSTATE = 0x0157
	}
	#endregion

	#region SetWindowLong indexes
	public enum SetWindowLongOffsets
	{
		GWL_WNDPROC     = (-4),
		GWL_HINSTANCE   = (-6),
		GWL_HWNDPARENT  = (-8),
		GWL_STYLE       = (-16),
		GWL_EXSTYLE     = (-20),
		GWL_USERDATA    = (-21),
		GWL_ID          = (-12)
	}
	#endregion

	#region TreeView Messages
	public enum TreeViewMessages
	{
		TV_FIRST        =  0x1100,
		TVM_GETITEMRECT = (TV_FIRST + 4),
		TVM_GETITEMW    = (TV_FIRST + 62)
	}
	#endregion

	#region TreeViewItem Flags
	public enum TreeViewItemFlags
	{
		TVIF_TEXT               = 0x0001,
		TVIF_IMAGE              = 0x0002,
		TVIF_PARAM              = 0x0004,
		TVIF_STATE              = 0x0008,
		TVIF_HANDLE             = 0x0010,
		TVIF_SELECTEDIMAGE      = 0x0020,
		TVIF_CHILDREN           = 0x0040,
		TVIF_INTEGRAL           = 0x0080
	}
	#endregion

	#region ListViewItem flags
	public enum ListViewItemFlags
	{
		LVIF_TEXT               = 0x0001,
		LVIF_IMAGE              = 0x0002,
		LVIF_PARAM              = 0x0004,
		LVIF_STATE              = 0x0008,
		LVIF_INDENT             = 0x0010,
		LVIF_NORECOMPUTE        = 0x0800
	}
	#endregion

	#region HeaderItem flags
	public enum HeaderItemFlags
	{
		HDI_WIDTH               = 0x0001,
		HDI_HEIGHT              = HDI_WIDTH,
		HDI_TEXT                = 0x0002,
		HDI_FORMAT              = 0x0004,
		HDI_LPARAM              = 0x0008,
		HDI_BITMAP              = 0x0010,
		HDI_IMAGE               = 0x0020,
		HDI_DI_SETITEM          = 0x0040,
		HDI_ORDER               = 0x0080
	}
	#endregion

	#region GetDCExFlags
	public enum GetDCExFlags
	{
		DCX_WINDOW           = 0x00000001,
		DCX_CACHE            = 0x00000002,
		DCX_NORESETATTRS     = 0x00000004,
		DCX_CLIPCHILDREN     = 0x00000008,
		DCX_CLIPSIBLINGS     = 0x00000010,
		DCX_PARENTCLIP       = 0x00000020,
		DCX_EXCLUDERGN       = 0x00000040,
		DCX_INTERSECTRGN     = 0x00000080,
		DCX_EXCLUDEUPDATE    = 0x00000100,
		DCX_INTERSECTUPDATE  = 0x00000200,
		DCX_LOCKWINDOWUPDATE = 0x00000400,
		DCX_VALIDATE         = 0x00200000
	}
	#endregion

	#region ActivateFlags
	public enum ActivateState
	{
		WA_INACTIVE     = 0,
		WA_ACTIVE       = 1,
		WA_CLICKACTIVE  = 2
	}
	#endregion

	#region StrechModeFlags
	public enum StrechModeFlags
	{
		BLACKONWHITE		= 1,
		WHITEONBLACK        = 2,
		COLORONCOLOR        = 3,
		HALFTONE            = 4,
		MAXSTRETCHBLTMODE   = 4
	}
	#endregion

	#region ScrollBarFlags
	public enum ScrollBarFlags
	{
		SBS_HORZ                    = 0x0000,
		SBS_VERT                    = 0x0001,
		SBS_TOPALIGN                = 0x0002,
		SBS_LEFTALIGN               = 0x0002,
		SBS_BOTTOMALIGN             = 0x0004,
		SBS_RIGHTALIGN              = 0x0004,
		SBS_SIZEBOXTOPLEFTALIGN     = 0x0002,
		SBS_SIZEBOXBOTTOMRIGHTALIGN = 0x0004,
		SBS_SIZEBOX                 = 0x0008,
		SBS_SIZEGRIP                = 0x0010
	}
	#endregion

	#region System Metrics Codes
	public enum SystemMetricsCodes
	{
		SM_CXSCREEN             = 0,
		SM_CYSCREEN             = 1,
		SM_CXVSCROLL            = 2,
		SM_CYHSCROLL            = 3,
		SM_CYCAPTION            = 4,
		SM_CXBORDER             = 5,
		SM_CYBORDER             = 6,
		SM_CXDLGFRAME           = 7,
		SM_CYDLGFRAME           = 8,
		SM_CYVTHUMB             = 9,
		SM_CXHTHUMB             = 10,
		SM_CXICON               = 11,
		SM_CYICON               = 12,
		SM_CXCURSOR             = 13,
		SM_CYCURSOR             = 14,
		SM_CYMENU               = 15,
		SM_CXFULLSCREEN         = 16,
		SM_CYFULLSCREEN         = 17,
		SM_CYKANJIWINDOW        = 18,
		SM_MOUSEPRESENT         = 19,
		SM_CYVSCROLL            = 20,
		SM_CXHSCROLL            = 21,
		SM_DEBUG                = 22,
		SM_SWAPBUTTON           = 23,
		SM_RESERVED1            = 24,
		SM_RESERVED2            = 25,
		SM_RESERVED3            = 26,
		SM_RESERVED4            = 27,
		SM_CXMIN                = 28,
		SM_CYMIN                = 29,
		SM_CXSIZE               = 30,
		SM_CYSIZE               = 31,
		SM_CXFRAME              = 32,
		SM_CYFRAME              = 33,
		SM_CXMINTRACK           = 34,
		SM_CYMINTRACK           = 35,
		SM_CXDOUBLECLK          = 36,
		SM_CYDOUBLECLK          = 37,
		SM_CXICONSPACING        = 38,
		SM_CYICONSPACING        = 39,
		SM_MENUDROPALIGNMENT    = 40,
		SM_PENWINDOWS           = 41,
		SM_DBCSENABLED          = 42,
		SM_CMOUSEBUTTONS        = 43,
		SM_CXFIXEDFRAME         = SM_CXDLGFRAME, 
		SM_CYFIXEDFRAME         = SM_CYDLGFRAME,  
		SM_CXSIZEFRAME          = SM_CXFRAME,    
		SM_CYSIZEFRAME          = SM_CYFRAME,    
		SM_SECURE               = 44,
		SM_CXEDGE               = 45,
		SM_CYEDGE               = 46,
		SM_CXMINSPACING         = 47,
		SM_CYMINSPACING         = 48,
		SM_CXSMICON             = 49,
		SM_CYSMICON             = 50,
		SM_CYSMCAPTION          = 51,
		SM_CXSMSIZE             = 52,
		SM_CYSMSIZE             = 53,
		SM_CXMENUSIZE           = 54,
		SM_CYMENUSIZE           = 55,
		SM_ARRANGE              = 56,
		SM_CXMINIMIZED          = 57,
		SM_CYMINIMIZED          = 58,
		SM_CXMAXTRACK           = 59,
		SM_CYMAXTRACK           = 60,
		SM_CXMAXIMIZED          = 61,
		SM_CYMAXIMIZED          = 62,
		SM_NETWORK              = 63,
		SM_CLEANBOOT            = 67,
		SM_CXDRAG               = 68,
		SM_CYDRAG               = 69,
		SM_SHOWSOUNDS           = 70,
		SM_CXMENUCHECK          = 71,  
		SM_CYMENUCHECK          = 72,
		SM_SLOWMACHINE          = 73,
		SM_MIDEASTENABLED       = 74,
		SM_MOUSEWHEELPRESENT    = 75,
		SM_XVIRTUALSCREEN       = 76,
		SM_YVIRTUALSCREEN       = 77,
		SM_CXVIRTUALSCREEN      = 78,
		SM_CYVIRTUALSCREEN      = 79,
		SM_CMONITORS            = 80,
		SM_SAMEDISPLAYFORMAT    = 81,
		SM_CMETRICS             = 83
	}
	#endregion

	#region SrollBarInfoFlags
	public enum ScrollBarInfoFlags
	{
		SIF_RANGE           = 0x0001,
		SIF_PAGE            = 0x0002,
		SIF_POS             = 0x0004,
		SIF_DISABLENOSCROLL = 0x0008,
		SIF_TRACKPOS        = 0x0010,
		SIF_ALL             = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
	}
	#endregion

	#region Enable ScrollBar flags
	public enum EnableScrollBarFlags
	{
		ESB_ENABLE_BOTH     = 0x0000,
		ESB_DISABLE_BOTH    = 0x0003,
		ESB_DISABLE_LEFT    = 0x0001,
		ESB_DISABLE_RIGHT   = 0x0002,
		ESB_DISABLE_UP      = 0x0001,
		ESB_DISABLE_DOWN    = 0x0002,
		ESB_DISABLE_LTUP    = ESB_DISABLE_LEFT,
		ESB_DISABLE_RTDN    = ESB_DISABLE_RIGHT
	}
	#endregion

	#region Scroll Requests
	public enum ScrollBarRequests
	{
		SB_LINEUP           = 0,
		SB_LINELEFT         = 0,
		SB_LINEDOWN         = 1,
		SB_LINERIGHT        = 1,
		SB_PAGEUP           = 2,
		SB_PAGELEFT         = 2,
		SB_PAGEDOWN         = 3,
		SB_PAGERIGHT        = 3,
		SB_THUMBPOSITION    = 4,
		SB_THUMBTRACK       = 5,
		SB_TOP              = 6,
		SB_LEFT             = 6,
		SB_BOTTOM           = 7,
		SB_RIGHT            = 7,
		SB_ENDSCROLL        = 8
	}
	#endregion

	#region SrollWindowEx flags
	public enum ScrollWindowExFlags
	{
		SW_SCROLLCHILDREN   = 0x0001,  
		SW_INVALIDATE       = 0x0002,  
		SW_ERASE            = 0x0004,  
		SW_SMOOTHSCROLL     = 0x0010  
	}
	#endregion

	#region ImageListFlags
	public enum  ImageListFlags
	{
		ILC_MASK             = 0x0001,
		ILC_COLOR            = 0x0000,
		ILC_COLORDDB         = 0x00FE,
		ILC_COLOR4           = 0x0004,
		ILC_COLOR8           = 0x0008,
		ILC_COLOR16          = 0x0010,
		ILC_COLOR24          = 0x0018,
		ILC_COLOR32          = 0x0020,
		ILC_PALETTE          = 0x0800      
	}
	#endregion

	#region List View Notifications
	public enum ListViewNotifications
	{
		LVN_FIRST             =  (0-100),
		LVN_GETDISPINFOW      = (LVN_FIRST-77),
		LVN_SETDISPINFOA      = (LVN_FIRST-51)
	}
	#endregion

	#region Richedit Control Messages
	public enum RichEditMessages
	{
		EM_CANPASTE         = Msgs.WM_USER + 50,
		EM_DISPLAYBAND      = Msgs.WM_USER + 51,
		EM_EXGETSEL         = Msgs.WM_USER + 52,
		EM_EXLIMITTEXT      = Msgs.WM_USER + 53,
		EM_EXLINEFROMCHAR   = Msgs.WM_USER + 54,
		EM_EXSETSEL         = Msgs.WM_USER + 55,
		EM_FINDTEXT         = Msgs.WM_USER + 56,
		EM_FORMATRANGE      = Msgs.WM_USER + 57,
		EM_GETCHARFORMAT    = Msgs.WM_USER + 58,
		EM_GETEVENTMASK     = Msgs.WM_USER + 59,
		EM_GETOLEINTERFACE  = Msgs.WM_USER + 60,
		EM_GETPARAFORMAT    = Msgs.WM_USER + 61,
		EM_GETSELTEXT       = Msgs.WM_USER + 62,
		EM_HIDESELECTION    = Msgs.WM_USER + 63,
		EM_PASTESPECIAL     = Msgs.WM_USER + 64,
		EM_REQUESTRESIZE    = Msgs.WM_USER + 65,
		EM_SELECTIONTYPE    = Msgs.WM_USER + 66,
		EM_SETBKGNDCOLOR    = Msgs.WM_USER + 67,
		EM_SETCHARFORMAT    = Msgs.WM_USER + 68,
		EM_SETEVENTMASK     = Msgs.WM_USER + 69,
		EM_SETOLECALLBACK   = Msgs.WM_USER + 70,
		EM_SETPARAFORMAT    = Msgs.WM_USER + 71,
		EM_SETTARGETDEVICE  = Msgs.WM_USER + 72,
		EM_STREAMIN         = Msgs.WM_USER + 73,
		EM_STREAMOUT        = Msgs.WM_USER + 74,
		EM_GETTEXTRANGE     = Msgs.WM_USER + 75,
		EM_FINDWORDBREAK    = Msgs.WM_USER + 76,
		EM_SETOPTIONS       = Msgs.WM_USER + 77,
		EM_GETOPTIONS       = Msgs.WM_USER + 78,
		EM_FINDTEXTEX       = Msgs.WM_USER + 79,
		EM_SETTYPOGRAPHYOPTIONS = Msgs.WM_USER + 202,
		EM_GETTYPOGRAPHYOPTIONS = Msgs.WM_USER + 203
	}

	[Flags]
	public enum RichEditMask
	{
		PFM_ALIGNMENT       = 0x00000008, /* The wAlignment member is valid. */
		PFM_BORDER          = 0x00000800, /* The wBorderSpace, wBorderWidth, and wBorders members are valid. */
		PFM_LINESPACING     = 0x00000100, /* The dyLineSpacing and bLineSpacingRule members are valid. */
		PFM_NUMBERING       = 0x00000020, /* The wNumbering member is valid. */
		PFM_NUMBERINGSTART  = 0x00008000, /* The wNumberingStart member is valid. */
		PFM_NUMBERINGSTYLE  = 0x00002000, /* The wNumberingStyle member is valid. */
		PFM_NUMBERINGTAB    = 0x00004000, /* The wNumberingTab member is valid. */
		PFM_OFFSET          = 0x00000004, /* The dxOffset member is valid. */
		PFM_OFFSETINDENT    = unchecked((int)0x80000000), /* The dxStartIndent member is valid. */
		PFM_RIGHTINDENT     = 0x00000002, /* The dxRightIndent member is valid. */
		PFM_SHADING         = 0x00001000, /* The wShadingWeight and wShadingStyle members are valid. */
		PFM_SPACEAFTER      = 0x00000080, /* The dySpaceAfter member is valid. */
		PFM_SPACEBEFORE     = 0x00000040, /* The dySpaceBefore member is valid. */
		PFM_STARTINDENT     = 0x00000001, /* The dxStartIndent member is valid  */
		PFM_STYLE           = 0x00000400, /* The sStyle member is valid. */
		PFM_TABSTOPS        = 0x00000010  /* The cTabCount and rgxTabs members are valid. */
	}

	public enum RichEditNumbering
	{
		PFN_NONE      = 0,
		PFN_BULLET    = 1,
		PFN_ARABIC    = 2,   /* tomListNumberAsArabic:   0, 1, 2,  ...*/  
		PFN_LCLETTER  = 3,   /* tomListNumberAsLCLetter: a, b, c,  ...*/  
		PFN_UCLETTER  = 4,   /* tomListNumberAsUCLetter: A, B, C,  ...*/  
		PFN_LCROMAN   = 5,   /* tomListNumberAsLCRoman:  i, ii, iii, ...*/
		PFN_UCROMAN   = 6    /* tomListNumberAsUCRoman:  I, II, III, ...*/
	}

	public enum RichEditAlignment
	{
		PFA_LEFT            = 0x0001,
		PFA_RIGHT           = 0x0002,  
		PFA_CENTER          = 0x0003,
		PFA_JUSTIFY         = 0x0004,
		PFA_FULL_INTERWORD  = 0x0004
	}

	/* Options for EM_SETTYPOGRAPHYOPTIONS */
	public enum RichEditTypography
	{
		TO_ADVANCEDTYPOGRAPHY = 1,
		TO_SIMPLELINEBREAK    = 2
	}

	public enum RichEditNumberingStyle
	{
		None      = 0,       /* Follows the number with a right parenthesis. */
		Enclose   = 0x100,   /* Encloses the number in parentheses. */
		Folllows  = 0x200,   /* Follows the number with a period.. */
		Number    = 0x300,   /* Displays only the number. */
		Continue  = 0x400,   /* Continues a numbered list without applying the next number or bullet.*/  
		UseStart  = 0x8000   /* Starts a new number with wNumberingStart */ 
	}
	#endregion

	#region Notify Icon
	[Flags]
	public enum NotifyInfoFlags 
	{
		NIIF_NONE       = 0x00000000,
		NIIF_INFO       = 0x00000001,
		NIIF_WARNING    = 0x00000002,
		NIIF_ERROR      = 0x00000003
	}

	[Flags]
	public enum NotifyFlags 
	{
		NIF_MESSAGE     = 0x00000001,
		NIF_ICON        = 0x00000002,
		NIF_TIP         = 0x00000004,
		NIF_STATE       = 0x00000008,
		NIF_INFO        = 0x00000010
	}

	public enum NotifyState 
	{
		NIS_HIDDEN      = 0x00000001,
		NIS_SHAREDICON  = 0x00000002
	}

	public enum NotifyCommand 
	{
		NIM_ADD         = 0x00000000,
		NIM_MODIFY      = 0x00000001,
		NIM_DELETE      = 0x00000002,
		NIM_SETFOCUS    = 0x00000003,
		NIM_SETVERSION  = 0x00000004
	}
	#endregion

	#region Windows

	public enum WindowsHookCodes
	{
		WH_MSGFILTER        = (-1),
		WH_JOURNALRECORD    = 0,
		WH_JOURNALPLAYBACK  = 1,
		WH_KEYBOARD         = 2,
		WH_GETMESSAGE       = 3,
		WH_CALLWNDPROC      = 4,
		WH_CBT              = 5,
		WH_SYSMSGFILTER     = 6,
		WH_MOUSE            = 7,
		WH_HARDWARE         = 8,
		WH_DEBUG            = 9,
		WH_SHELL            = 10,
		WH_FOREGROUNDIDLE   = 11,
		WH_CALLWNDPROCRET   = 12,
		WH_KEYBOARD_LL      = 13,
		WH_MOUSE_LL         = 14
	}
	

	public enum PeekMessageFlags
    {
        PM_NOREMOVE		= 0,
        PM_REMOVE		= 1,
        PM_NOYIELD		= 2
    }

	//[CLSCompliantAttribute(false)]
	public enum SetWindowPosFlags : uint
    {
        SWP_NOSIZE          = 0x0001,
        SWP_NOMOVE          = 0x0002,
        SWP_NOZORDER        = 0x0004,
        SWP_NOREDRAW        = 0x0008,
        SWP_NOACTIVATE      = 0x0010,
        SWP_FRAMECHANGED    = 0x0020,
        SWP_SHOWWINDOW      = 0x0040,
        SWP_HIDEWINDOW      = 0x0080,
        SWP_NOCOPYBITS      = 0x0100,
        SWP_NOOWNERZORDER   = 0x0200, 
        SWP_NOSENDCHANGING  = 0x0400,
        SWP_DRAWFRAME       = 0x0020,
        SWP_NOREPOSITION    = 0x0200,
        SWP_DEFERERASE      = 0x2000,
        SWP_ASYNCWINDOWPOS  = 0x4000
    }

    public enum SetWindowPosZ 
    {
        HWND_TOP        = 0,
        HWND_BOTTOM     = 1,
        HWND_TOPMOST    = -1,
        HWND_NOTOPMOST  = -2
    }

    public enum ShowWindowStyles : short
    {
        SW_HIDE             = 0,
        SW_SHOWNORMAL       = 1,
        SW_NORMAL           = 1,
        SW_SHOWMINIMIZED    = 2,
        SW_SHOWMAXIMIZED    = 3,
        SW_MAXIMIZE         = 3,
        SW_SHOWNOACTIVATE   = 4,
        SW_SHOW             = 5,
        SW_MINIMIZE         = 6,
        SW_SHOWMINNOACTIVE  = 7,
        SW_SHOWNA           = 8,
        SW_RESTORE          = 9,
        SW_SHOWDEFAULT      = 10,
        SW_FORCEMINIMIZE    = 11,
        SW_MAX              = 11
    }

	//[CLSCompliantAttribute(false)]
	public enum WindowStyles : uint
    {
        WS_OVERLAPPED       = 0x00000000,
        WS_POPUP            = 0x80000000,
        WS_CHILD            = 0x40000000,
        WS_MINIMIZE         = 0x20000000,
        WS_VISIBLE          = 0x10000000,
        WS_DISABLED         = 0x08000000,
        WS_CLIPSIBLINGS     = 0x04000000,
        WS_CLIPCHILDREN     = 0x02000000,
        WS_MAXIMIZE         = 0x01000000,
        WS_CAPTION          = 0x00C00000,
        WS_BORDER           = 0x00800000,
        WS_DLGFRAME         = 0x00400000,
        WS_VSCROLL          = 0x00200000,
        WS_HSCROLL          = 0x00100000,
        WS_SYSMENU          = 0x00080000,
        WS_THICKFRAME       = 0x00040000,
        WS_GROUP            = 0x00020000,
        WS_TABSTOP          = 0x00010000,
        WS_MINIMIZEBOX      = 0x00020000,
        WS_MAXIMIZEBOX      = 0x00010000,
        WS_TILED            = 0x00000000,
        WS_ICONIC           = 0x20000000,
        WS_SIZEBOX          = 0x00040000,
        WS_POPUPWINDOW      = 0x80880000,
        WS_OVERLAPPEDWINDOW = 0x00CF0000,
        WS_TILEDWINDOW      = 0x00CF0000,
        WS_CHILDWINDOW      = 0x40000000
    }

    public enum VirtualKeys
    {
        VK_LBUTTON		= 0x01,
        VK_CANCEL		= 0x03,
        VK_BACK			= 0x08,
        VK_TAB			= 0x09,
        VK_CLEAR		= 0x0C,
        VK_RETURN		= 0x0D,
        VK_SHIFT		= 0x10,
        VK_CONTROL		= 0x11,
        VK_MENU			= 0x12,
        VK_CAPITAL		= 0x14,
        VK_ESCAPE		= 0x1B,
        VK_SPACE		= 0x20,
        VK_PRIOR		= 0x21,
        VK_NEXT			= 0x22,
        VK_END			= 0x23,
        VK_HOME			= 0x24,
        VK_LEFT			= 0x25,
        VK_UP			= 0x26,
        VK_RIGHT		= 0x27,
        VK_DOWN			= 0x28,
        VK_SELECT		= 0x29,
        VK_EXECUTE		= 0x2B,
        VK_SNAPSHOT		= 0x2C,
        VK_HELP			= 0x2F,
        VK_0			= 0x30,
        VK_1			= 0x31,
        VK_2			= 0x32,
        VK_3			= 0x33,
        VK_4			= 0x34,
        VK_5			= 0x35,
        VK_6			= 0x36,
        VK_7			= 0x37,
        VK_8			= 0x38,
        VK_9			= 0x39,
        VK_A			= 0x41,
        VK_B			= 0x42,
        VK_C			= 0x43,
        VK_D			= 0x44,
        VK_E			= 0x45,
        VK_F			= 0x46,
        VK_G			= 0x47,
        VK_H			= 0x48,
        VK_I			= 0x49,
        VK_J			= 0x4A,
        VK_K			= 0x4B,
        VK_L			= 0x4C,
        VK_M			= 0x4D,
        VK_N			= 0x4E,
        VK_O			= 0x4F,
        VK_P			= 0x50,
        VK_Q			= 0x51,
        VK_R			= 0x52,
        VK_S			= 0x53,
        VK_T			= 0x54,
        VK_U			= 0x55,
        VK_V			= 0x56,
        VK_W			= 0x57,
        VK_X			= 0x58,
        VK_Y			= 0x59,
        VK_Z			= 0x5A,
        VK_NUMPAD0		= 0x60,
        VK_NUMPAD1		= 0x61,
        VK_NUMPAD2		= 0x62,
        VK_NUMPAD3		= 0x63,
        VK_NUMPAD4		= 0x64,
        VK_NUMPAD5		= 0x65,
        VK_NUMPAD6		= 0x66,
        VK_NUMPAD7		= 0x67,
        VK_NUMPAD8		= 0x68,
        VK_NUMPAD9		= 0x69,
        VK_MULTIPLY		= 0x6A,
        VK_ADD			= 0x6B,
        VK_SEPARATOR	= 0x6C,
        VK_SUBTRACT		= 0x6D,
        VK_DECIMAL		= 0x6E,
        VK_DIVIDE		= 0x6F,
        VK_ATTN			= 0xF6,
        VK_CRSEL		= 0xF7,
        VK_EXSEL		= 0xF8,
        VK_EREOF		= 0xF9,
        VK_PLAY			= 0xFA,  
        VK_ZOOM			= 0xFB,
        VK_NONAME		= 0xFC,
        VK_PA1			= 0xFD,
        VK_OEM_CLEAR	= 0xFE,
        VK_LWIN			= 0x5B,
        VK_RWIN			= 0x5C,
        VK_APPS			= 0x5D,   
        VK_LSHIFT		= 0xA0,   
        VK_RSHIFT		= 0xA1,   
        VK_LCONTROL		= 0xA2,   
        VK_RCONTROL		= 0xA3,   
        VK_LMENU		= 0xA4,   
        VK_RMENU		= 0xA5
    }

    public enum Msgs
    {
        WM_NULL                   = 0x0000,
        WM_CREATE                 = 0x0001,
        WM_DESTROY                = 0x0002,
        WM_MOVE                   = 0x0003,
        WM_SIZE                   = 0x0005,
        WM_ACTIVATE               = 0x0006,
        WM_SETFOCUS               = 0x0007,
        WM_KILLFOCUS              = 0x0008,
        WM_ENABLE                 = 0x000A,
        WM_SETREDRAW              = 0x000B,
        WM_SETTEXT                = 0x000C,
        WM_GETTEXT                = 0x000D,
        WM_GETTEXTLENGTH          = 0x000E,
        WM_PAINT                  = 0x000F,
        WM_CLOSE                  = 0x0010,
        WM_QUERYENDSESSION        = 0x0011,
        WM_QUIT                   = 0x0012,
        WM_QUERYOPEN              = 0x0013,
        WM_ERASEBKGND             = 0x0014,
        WM_SYSCOLORCHANGE         = 0x0015,
        WM_ENDSESSION             = 0x0016,
        WM_SHOWWINDOW             = 0x0018,
		WM_CTLCOLOR               = 0x0019,
		WM_WININICHANGE           = 0x001A,
        WM_SETTINGCHANGE          = 0x001A,
        WM_DEVMODECHANGE          = 0x001B,
        WM_ACTIVATEAPP            = 0x001C,
        WM_FONTCHANGE             = 0x001D,
        WM_TIMECHANGE             = 0x001E,
        WM_CANCELMODE             = 0x001F,
        WM_SETCURSOR              = 0x0020,
        WM_MOUSEACTIVATE          = 0x0021,
        WM_CHILDACTIVATE          = 0x0022,
        WM_QUEUESYNC              = 0x0023,
        WM_GETMINMAXINFO          = 0x0024,
        WM_PAINTICON              = 0x0026,
        WM_ICONERASEBKGND         = 0x0027,
        WM_NEXTDLGCTL             = 0x0028,
        WM_SPOOLERSTATUS          = 0x002A,
        WM_DRAWITEM               = 0x002B,
        WM_MEASUREITEM            = 0x002C,
        WM_DELETEITEM             = 0x002D,
        WM_VKEYTOITEM             = 0x002E,
        WM_CHARTOITEM             = 0x002F,
        WM_SETFONT                = 0x0030,
        WM_GETFONT                = 0x0031,
        WM_SETHOTKEY              = 0x0032,
        WM_GETHOTKEY              = 0x0033,
        WM_QUERYDRAGICON          = 0x0037,
        WM_COMPAREITEM            = 0x0039,
        WM_GETOBJECT              = 0x003D,
        WM_COMPACTING             = 0x0041,
        WM_COMMNOTIFY             = 0x0044 ,
        WM_WINDOWPOSCHANGING      = 0x0046,
        WM_WINDOWPOSCHANGED       = 0x0047,
        WM_POWER                  = 0x0048,
        WM_COPYDATA               = 0x004A,
        WM_CANCELJOURNAL          = 0x004B,
        WM_NOTIFY                 = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE        = 0x0051,
        WM_TCARD                  = 0x0052,
        WM_HELP                   = 0x0053,
        WM_USERCHANGED            = 0x0054,
        WM_NOTIFYFORMAT           = 0x0055,
        WM_CONTEXTMENU            = 0x007B,
        WM_STYLECHANGING          = 0x007C,
        WM_STYLECHANGED           = 0x007D,
        WM_DISPLAYCHANGE          = 0x007E,
        WM_GETICON                = 0x007F,
        WM_SETICON                = 0x0080,
        WM_NCCREATE               = 0x0081,
        WM_NCDESTROY              = 0x0082,
        WM_NCCALCSIZE             = 0x0083,
        WM_NCHITTEST              = 0x0084,
        WM_NCPAINT                = 0x0085,
        WM_NCACTIVATE             = 0x0086,
        WM_GETDLGCODE             = 0x0087,
        WM_SYNCPAINT              = 0x0088,
        WM_NCMOUSEMOVE            = 0x00A0,
        WM_NCLBUTTONDOWN          = 0x00A1,
        WM_NCLBUTTONUP            = 0x00A2,
        WM_NCLBUTTONDBLCLK        = 0x00A3,
        WM_NCRBUTTONDOWN          = 0x00A4,
        WM_NCRBUTTONUP            = 0x00A5,
        WM_NCRBUTTONDBLCLK        = 0x00A6,
        WM_NCMBUTTONDOWN          = 0x00A7,
        WM_NCMBUTTONUP            = 0x00A8,
        WM_NCMBUTTONDBLCLK        = 0x00A9,
		WM_NCXBUTTONDOWN          = 0x00AB,
		WM_NCXBUTTONUP            = 0x00AC,
        WM_KEYDOWN                = 0x0100,
        WM_KEYUP                  = 0x0101,
        WM_CHAR                   = 0x0102,
        WM_DEADCHAR               = 0x0103,
        WM_SYSKEYDOWN             = 0x0104,
        WM_SYSKEYUP               = 0x0105,
        WM_SYSCHAR                = 0x0106,
        WM_SYSDEADCHAR            = 0x0107,
        WM_KEYLAST                = 0x0108,
        WM_IME_STARTCOMPOSITION   = 0x010D,
        WM_IME_ENDCOMPOSITION     = 0x010E,
        WM_IME_COMPOSITION        = 0x010F,
        WM_IME_KEYLAST            = 0x010F,
        WM_INITDIALOG             = 0x0110,
        WM_COMMAND                = 0x0111,
        WM_SYSCOMMAND             = 0x0112,
        WM_TIMER                  = 0x0113,
        WM_HSCROLL                = 0x0114,
        WM_VSCROLL                = 0x0115,
        WM_INITMENU               = 0x0116,
        WM_INITMENUPOPUP          = 0x0117,
        WM_MENUSELECT             = 0x011F,
        WM_MENUCHAR               = 0x0120,
        WM_ENTERIDLE              = 0x0121,
        WM_MENURBUTTONUP          = 0x0122,
        WM_MENUDRAG               = 0x0123,
        WM_MENUGETOBJECT          = 0x0124,
        WM_UNINITMENUPOPUP        = 0x0125,
        WM_MENUCOMMAND            = 0x0126,
        WM_CTLCOLORMSGBOX         = 0x0132,
        WM_CTLCOLOREDIT           = 0x0133,
        WM_CTLCOLORLISTBOX        = 0x0134,
        WM_CTLCOLORBTN            = 0x0135,
        WM_CTLCOLORDLG            = 0x0136,
        WM_CTLCOLORSCROLLBAR      = 0x0137,
        WM_CTLCOLORSTATIC         = 0x0138,
        WM_MOUSEMOVE              = 0x0200,
        WM_LBUTTONDOWN            = 0x0201,
        WM_LBUTTONUP              = 0x0202,
        WM_LBUTTONDBLCLK          = 0x0203,
        WM_RBUTTONDOWN            = 0x0204,
        WM_RBUTTONUP              = 0x0205,
        WM_RBUTTONDBLCLK          = 0x0206,
        WM_MBUTTONDOWN            = 0x0207,
        WM_MBUTTONUP              = 0x0208,
        WM_MBUTTONDBLCLK          = 0x0209,
        WM_MOUSEWHEEL             = 0x020A,
		WM_XBUTTONDOWN            = 0x020B,
		WM_XBUTTONUP              = 0x020C,
		WM_XBUTTONDBLCLK          = 0x020D,
        WM_PARENTNOTIFY           = 0x0210,
        WM_ENTERMENULOOP          = 0x0211,
        WM_EXITMENULOOP           = 0x0212,
        WM_NEXTMENU               = 0x0213,
        WM_SIZING                 = 0x0214,
        WM_CAPTURECHANGED         = 0x0215,
        WM_MOVING                 = 0x0216,
        WM_DEVICECHANGE           = 0x0219,
        WM_MDICREATE              = 0x0220,
        WM_MDIDESTROY             = 0x0221,
        WM_MDIACTIVATE            = 0x0222,
        WM_MDIRESTORE             = 0x0223,
        WM_MDINEXT                = 0x0224,
        WM_MDIMAXIMIZE            = 0x0225,
        WM_MDITILE                = 0x0226,
        WM_MDICASCADE             = 0x0227,
        WM_MDIICONARRANGE         = 0x0228,
        WM_MDIGETACTIVE           = 0x0229,
        WM_MDISETMENU             = 0x0230,
        WM_ENTERSIZEMOVE          = 0x0231,
        WM_EXITSIZEMOVE           = 0x0232,
        WM_DROPFILES              = 0x0233,
        WM_MDIREFRESHMENU         = 0x0234,
        WM_IME_SETCONTEXT         = 0x0281,
        WM_IME_NOTIFY             = 0x0282,
        WM_IME_CONTROL            = 0x0283,
        WM_IME_COMPOSITIONFULL    = 0x0284,
        WM_IME_SELECT             = 0x0285,
        WM_IME_CHAR               = 0x0286,
        WM_IME_REQUEST            = 0x0288,
        WM_IME_KEYDOWN            = 0x0290,
        WM_IME_KEYUP              = 0x0291,
        WM_MOUSEHOVER             = 0x02A1,
        WM_MOUSELEAVE             = 0x02A3,
        WM_CUT                    = 0x0300,
        WM_COPY                   = 0x0301,
        WM_PASTE                  = 0x0302,
        WM_CLEAR                  = 0x0303,
        WM_UNDO                   = 0x0304,
        WM_RENDERFORMAT           = 0x0305,
        WM_RENDERALLFORMATS       = 0x0306,
        WM_DESTROYCLIPBOARD       = 0x0307,
        WM_DRAWCLIPBOARD          = 0x0308,
        WM_PAINTCLIPBOARD         = 0x0309,
        WM_VSCROLLCLIPBOARD       = 0x030A,
        WM_SIZECLIPBOARD          = 0x030B,
        WM_ASKCBFORMATNAME        = 0x030C,
        WM_CHANGECBCHAIN          = 0x030D,
        WM_HSCROLLCLIPBOARD       = 0x030E,
        WM_QUERYNEWPALETTE        = 0x030F,
        WM_PALETTEISCHANGING      = 0x0310,
        WM_PALETTECHANGED         = 0x0311,
        WM_HOTKEY                 = 0x0312,
        WM_PRINT                  = 0x0317,
        WM_PRINTCLIENT            = 0x0318,
        WM_HANDHELDFIRST          = 0x0358,
        WM_HANDHELDLAST           = 0x035F,
        WM_AFXFIRST               = 0x0360,
        WM_AFXLAST                = 0x037F,
        WM_PENWINFIRST            = 0x0380,
        WM_PENWINLAST             = 0x038F,
        WM_APP                    = 0x8000,
        WM_USER                   = 0x0400
    }

	//[CLSCompliantAttribute(false)]
	public enum Cursors : uint
    {
        IDC_ARROW		= 32512U,
        IDC_IBEAM       = 32513U,
        IDC_WAIT        = 32514U,
        IDC_CROSS       = 32515U,
        IDC_UPARROW     = 32516U,
        IDC_SIZE        = 32640U,
        IDC_ICON        = 32641U,
        IDC_SIZENWSE    = 32642U,
        IDC_SIZENESW    = 32643U,
        IDC_SIZEWE      = 32644U,
        IDC_SIZENS      = 32645U,
        IDC_SIZEALL     = 32646U,
        IDC_NO          = 32648U,
        IDC_HAND        = 32649U,
        IDC_APPSTARTING = 32650U,
        IDC_HELP        = 32651U
    }

	//[CLSCompliantAttribute(false)]
	public enum TrackerEventFlags : uint
    {
        TME_HOVER	= 0x00000001,
        TME_LEAVE	= 0x00000002,
        TME_QUERY	= 0x40000000,
        TME_CANCEL	= 0x80000000
    }

     public enum DialogCodes
    {
        DLGC_WANTARROWS			= 0x0001,
        DLGC_WANTTAB			= 0x0002,
        DLGC_WANTALLKEYS		= 0x0004,
        DLGC_WANTMESSAGE		= 0x0004,
        DLGC_HASSETSEL			= 0x0008,
        DLGC_DEFPUSHBUTTON		= 0x0010,
        DLGC_UNDEFPUSHBUTTON	= 0x0020,
        DLGC_RADIOBUTTON		= 0x0040,
        DLGC_WANTCHARS			= 0x0080,
        DLGC_STATIC				= 0x0100,
        DLGC_BUTTON				= 0x2000
    }
    public enum UpdateLayeredWindowsFlags
    {
        ULW_COLORKEY = 0x00000001,
        ULW_ALPHA    = 0x00000002,
        ULW_OPAQUE   = 0x00000004
    }

    public enum AlphaFlags : byte
    {
        AC_SRC_OVER  = 0x00,
        AC_SRC_ALPHA = 0x01
    }

 
    public enum HatchStyles
    {
        HS_HORIZONTAL       = 0,
        HS_VERTICAL         = 1,
        HS_FDIAGONAL        = 2,
        HS_BDIAGONAL        = 3,
        HS_CROSS            = 4,
        HS_DIAGCROSS        = 5
    }

 	public enum HitTest
	{
		HTERROR			= -2,
		HTTRANSPARENT   = -1,
		HTNOWHERE		= 0,
		HTCLIENT		= 1,
		HTCAPTION		= 2,
		HTSYSMENU		= 3,
		HTGROWBOX		= 4,
		HTSIZE			= 4,
		HTMENU			= 5,
		HTHSCROLL		= 6,
		HTVSCROLL		= 7,
		HTMINBUTTON		= 8,
		HTMAXBUTTON		= 9,
		HTLEFT			= 10,
		HTRIGHT			= 11,
		HTTOP			= 12,
		HTTOPLEFT		= 13,
		HTTOPRIGHT		= 14,
		HTBOTTOM		= 15,
		HTBOTTOMLEFT	= 16,
		HTBOTTOMRIGHT	= 17,
		HTBORDER		= 18,
		HTREDUCE		= 8,
		HTZOOM			= 9 ,
		HTSIZEFIRST		= 10,
		HTSIZELAST		= 17,
		HTOBJECT		= 19,
		HTCLOSE			= 20,
		HTHELP			= 21
	}

	public enum AnimateFlags
	{
		AW_HOR_POSITIVE			= 0x00000001,
		AW_HOR_NEGATIVE			= 0x00000002,
		AW_VER_POSITIVE			= 0x00000004,
		AW_VER_NEGATIVE			= 0x00000008,
		AW_CENTER				= 0x00000010,
		AW_HIDE					= 0x00010000,
		AW_ACTIVATE				= 0x00020000,
		AW_SLIDE				= 0x00040000,
		AW_BLEND				= 0x00080000,
		AW_System				= 0x00100000,
		AW_SlideCenter			= 0x00040010,
		AW_HorVerPositive		= 0x00040005,
		AW_HorVerNegative		= 0x0004000A,
		AW_HorPosVerNegative	= 0x00040009,
		AW_HorNegVerPositive	= 0x00040006
	}

    public enum GetWindowLongFlags
    {
        GWL_WNDPROC         = -4,
        GWL_HINSTANCE       = -6,
        GWL_HWNDPARENT      = -8,
        GWL_STYLE           = -16,
        GWL_EXSTYLE         = -20,
        GWL_USERDATA        = -21,
        GWL_ID              = -12
    }
    
	public enum SPIActions
	{
		SPI_GETBEEP                         = 0x0001,
		SPI_SETBEEP                         = 0x0002,
		SPI_GETMOUSE                        = 0x0003,
		SPI_SETMOUSE                        = 0x0004,
		SPI_GETBORDER                       = 0x0005,
		SPI_SETBORDER                       = 0x0006,
		SPI_GETKEYBOARDSPEED                = 0x000A,
		SPI_SETKEYBOARDSPEED                = 0x000B,
		SPI_LANGDRIVER                      = 0x000C,
		SPI_ICONHORIZONTALSPACING           = 0x000D,
		SPI_GETSCREENSAVETIMEOUT            = 0x000E,
		SPI_SETSCREENSAVETIMEOUT            = 0x000F,
		SPI_GETSCREENSAVEACTIVE             = 0x0010,
		SPI_SETSCREENSAVEACTIVE             = 0x0011,
		SPI_GETGRIDGRANULARITY              = 0x0012,
		SPI_SETGRIDGRANULARITY              = 0x0013,
		SPI_SETDESKWALLPAPER                = 0x0014,
		SPI_SETDESKPATTERN                  = 0x0015,
		SPI_GETKEYBOARDDELAY                = 0x0016,
		SPI_SETKEYBOARDDELAY                = 0x0017,
		SPI_ICONVERTICALSPACING             = 0x0018,
		SPI_GETICONTITLEWRAP                = 0x0019,
		SPI_SETICONTITLEWRAP                = 0x001A,
		SPI_GETMENUDROPALIGNMENT            = 0x001B,
		SPI_SETMENUDROPALIGNMENT            = 0x001C,
		SPI_SETDOUBLECLKWIDTH               = 0x001D,
		SPI_SETDOUBLECLKHEIGHT              = 0x001E,
		SPI_GETICONTITLELOGFONT             = 0x001F,
		SPI_SETDOUBLECLICKTIME              = 0x0020,
		SPI_SETMOUSEBUTTONSWAP              = 0x0021,
		SPI_SETICONTITLELOGFONT             = 0x0022,
		SPI_GETFASTTASKSWITCH               = 0x0023,
		SPI_SETFASTTASKSWITCH               = 0x0024,
		SPI_SETDRAGFULLWINDOWS              = 0x0025,
		SPI_GETDRAGFULLWINDOWS              = 0x0026,
		SPI_GETNONCLIENTMETRICS             = 0x0029,
		SPI_SETNONCLIENTMETRICS             = 0x002A,
		SPI_GETMINIMIZEDMETRICS             = 0x002B,
		SPI_SETMINIMIZEDMETRICS             = 0x002C,
		SPI_GETICONMETRICS                  = 0x002D,
		SPI_SETICONMETRICS                  = 0x002E,
		SPI_SETWORKAREA                     = 0x002F,
		SPI_GETWORKAREA                     = 0x0030,
		SPI_SETPENWINDOWS                   = 0x0031,
		SPI_GETHIGHCONTRAST                 = 0x0042,
		SPI_SETHIGHCONTRAST                 = 0x0043,
		SPI_GETKEYBOARDPREF                 = 0x0044,
		SPI_SETKEYBOARDPREF                 = 0x0045,
		SPI_GETSCREENREADER                 = 0x0046,
		SPI_SETSCREENREADER                 = 0x0047,
		SPI_GETANIMATION                    = 0x0048,
		SPI_SETANIMATION                    = 0x0049,
		SPI_GETFONTSMOOTHING                = 0x004A,
		SPI_SETFONTSMOOTHING                = 0x004B,
		SPI_SETDRAGWIDTH                    = 0x004C,
		SPI_SETDRAGHEIGHT                   = 0x004D,
		SPI_SETHANDHELD                     = 0x004E,
		SPI_GETLOWPOWERTIMEOUT              = 0x004F,
		SPI_GETPOWEROFFTIMEOUT              = 0x0050,
		SPI_SETLOWPOWERTIMEOUT              = 0x0051,
		SPI_SETPOWEROFFTIMEOUT              = 0x0052,
		SPI_GETLOWPOWERACTIVE               = 0x0053,
		SPI_GETPOWEROFFACTIVE               = 0x0054,
		SPI_SETLOWPOWERACTIVE               = 0x0055,
		SPI_SETPOWEROFFACTIVE               = 0x0056,
		SPI_SETCURSORS                      = 0x0057,
		SPI_SETICONS                        = 0x0058,
		SPI_GETDEFAULTINPUTLANG             = 0x0059,
		SPI_SETDEFAULTINPUTLANG             = 0x005A,
		SPI_SETLANGTOGGLE                   = 0x005B,
		SPI_GETWINDOWSEXTENSION             = 0x005C,
		SPI_SETMOUSETRAILS                  = 0x005D,
		SPI_GETMOUSETRAILS                  = 0x005E,
		SPI_SETSCREENSAVERRUNNING           = 0x0061,
		SPI_SCREENSAVERRUNNING              = 0x0061,
		SPI_GETFILTERKEYS                   = 0x0032,
		SPI_SETFILTERKEYS                   = 0x0033,
		SPI_GETTOGGLEKEYS                   = 0x0034,
		SPI_SETTOGGLEKEYS                   = 0x0035,
		SPI_GETMOUSEKEYS                    = 0x0036,
		SPI_SETMOUSEKEYS                    = 0x0037,
		SPI_GETSHOWSOUNDS                   = 0x0038,
		SPI_SETSHOWSOUNDS                   = 0x0039,
		SPI_GETSTICKYKEYS                   = 0x003A,
		SPI_SETSTICKYKEYS                   = 0x003B,
		SPI_GETACCESSTIMEOUT                = 0x003C,
		SPI_SETACCESSTIMEOUT                = 0x003D,
		SPI_GETSERIALKEYS                   = 0x003E,
		SPI_SETSERIALKEYS                   = 0x003F,
		SPI_GETSOUNDSENTRY                  = 0x0040,
		SPI_SETSOUNDSENTRY                  = 0x0041,
		SPI_GETSNAPTODEFBUTTON              = 0x005F,
		SPI_SETSNAPTODEFBUTTON              = 0x0060,
		SPI_GETMOUSEHOVERWIDTH              = 0x0062,
		SPI_SETMOUSEHOVERWIDTH              = 0x0063,
		SPI_GETMOUSEHOVERHEIGHT             = 0x0064,
		SPI_SETMOUSEHOVERHEIGHT             = 0x0065,
		SPI_GETMOUSEHOVERTIME               = 0x0066,
		SPI_SETMOUSEHOVERTIME               = 0x0067,
		SPI_GETWHEELSCROLLLINES             = 0x0068,
		SPI_SETWHEELSCROLLLINES             = 0x0069,
		SPI_GETMENUSHOWDELAY                = 0x006A,
		SPI_SETMENUSHOWDELAY                = 0x006B,
		SPI_GETSHOWIMEUI                    = 0x006E,
		SPI_SETSHOWIMEUI                    = 0x006F,
		SPI_GETMOUSESPEED                   = 0x0070,
		SPI_SETMOUSESPEED                   = 0x0071,
		SPI_GETSCREENSAVERRUNNING           = 0x0072,
		SPI_GETDESKWALLPAPER                = 0x0073,
		SPI_GETACTIVEWINDOWTRACKING         = 0x1000,
		SPI_SETACTIVEWINDOWTRACKING         = 0x1001,
		SPI_GETMENUANIMATION                = 0x1002,
		SPI_SETMENUANIMATION                = 0x1003,
		SPI_GETCOMBOBOXANIMATION            = 0x1004,
		SPI_SETCOMBOBOXANIMATION            = 0x1005,
		SPI_GETLISTBOXSMOOTHSCROLLING       = 0x1006,
		SPI_SETLISTBOXSMOOTHSCROLLING       = 0x1007,
		SPI_GETGRADIENTCAPTIONS             = 0x1008,
		SPI_SETGRADIENTCAPTIONS             = 0x1009,
		SPI_GETKEYBOARDCUES                 = 0x100A,
		SPI_SETKEYBOARDCUES                 = 0x100B,
		SPI_GETMENUUNDERLINES               = 0x100A,
		SPI_SETMENUUNDERLINES               = 0x100B,
		SPI_GETACTIVEWNDTRKZORDER           = 0x100C,
		SPI_SETACTIVEWNDTRKZORDER           = 0x100D,
		SPI_GETHOTTRACKING                  = 0x100E,
		SPI_SETHOTTRACKING                  = 0x100F,
		SPI_GETMENUFADE                     = 0x1012,
		SPI_SETMENUFADE                     = 0x1013,
		SPI_GETSELECTIONFADE                = 0x1014,
		SPI_SETSELECTIONFADE                = 0x1015,
		SPI_GETTOOLTIPANIMATION             = 0x1016,
		SPI_SETTOOLTIPANIMATION             = 0x1017,
		SPI_GETTOOLTIPFADE                  = 0x1018,
		SPI_SETTOOLTIPFADE                  = 0x1019,
		SPI_GETCURSORSHADOW                 = 0x101A,
		SPI_SETCURSORSHADOW                 = 0x101B,
		SPI_GETMOUSESONAR                   = 0x101C,
		SPI_SETMOUSESONAR                   = 0x101D,
		SPI_GETMOUSECLICKLOCK               = 0x101E,
		SPI_SETMOUSECLICKLOCK               = 0x101F,
		SPI_GETMOUSEVANISH                  = 0x1020,
		SPI_SETMOUSEVANISH                  = 0x1021,
		SPI_GETFLATMENU                     = 0x1022,
		SPI_SETFLATMENU                     = 0x1023,
		SPI_GETDROPSHADOW                   = 0x1024,
		SPI_SETDROPSHADOW                   = 0x1025,
		SPI_GETUIEFFECTS                    = 0x103E,
		SPI_SETUIEFFECTS                    = 0x103F,
		SPI_GETFOREGROUNDLOCKTIMEOUT        = 0x2000,
		SPI_SETFOREGROUNDLOCKTIMEOUT        = 0x2001,
		SPI_GETACTIVEWNDTRKTIMEOUT          = 0x2002,
		SPI_SETACTIVEWNDTRKTIMEOUT          = 0x2003,
		SPI_GETFOREGROUNDFLASHCOUNT         = 0x2004,
		SPI_SETFOREGROUNDFLASHCOUNT         = 0x2005,
		SPI_GETCARETWIDTH                   = 0x2006,
		SPI_SETCARETWIDTH                   = 0x2007,
		SPI_GETMOUSECLICKLOCKTIME           = 0x2008,
		SPI_SETMOUSECLICKLOCKTIME           = 0x2009,
		SPI_GETFONTSMOOTHINGTYPE            = 0x200A,
		SPI_SETFONTSMOOTHINGTYPE            = 0x200B,
		SPI_GETFONTSMOOTHINGCONTRAST        = 0x200C,
		SPI_SETFONTSMOOTHINGCONTRAST        = 0x200D,
		SPI_GETFOCUSBORDERWIDTH             = 0x200E,
		SPI_SETFOCUSBORDERWIDTH             = 0x200F,
		SPI_GETFOCUSBORDERHEIGHT            = 0x2010,
		SPI_SETFOCUSBORDERHEIGHT            = 0x2011
	}

	public enum SPIWinINIFlags
	{
		SPIF_UPDATEINIFILE		= 0x0001,
		SPIF_SENDWININICHANGE	= 0x0002,
		SPIF_SENDCHANGE			= 0x0002
	}
	#endregion
}