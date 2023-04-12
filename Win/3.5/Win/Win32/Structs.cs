using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MControl.Win32
{

	#region GDI CommonHandles

//	public sealed class CommonHandles
//	{
//		// Methods
//		static CommonHandles()
//		{
//			CommonHandles.Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
//			CommonHandles.Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
//			CommonHandles.EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
//			CommonHandles.Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
//			CommonHandles.GDI = HandleCollector.RegisterType("GDI", 90, 50);
//			CommonHandles.HDC = HandleCollector.RegisterType("HDC", 100, 2);
//			CommonHandles.Icon = HandleCollector.RegisterType("Icon", 20, 500);
//			CommonHandles.Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
//			CommonHandles.Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
//			CommonHandles.Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
//		}
// 
//		public CommonHandles()
//		{
//		}
//
//		// Fields
//		public static readonly int Accelerator;
//		public static readonly int Cursor;
//		public static readonly int EMF;
//		public static readonly int Find;
//		public static readonly int GDI;
//		public static readonly int HDC;
//		public static readonly int Icon;
//		public static readonly int Kernel;
//		public static readonly int Menu;
//		public static readonly int Window;
//	}
	#endregion

	#region GDI HandleCollector

//	public sealed class HandleCollector
//	{
//		internal delegate void HandleChangeEventHandler(string handleType, IntPtr handleValue, int currentHandleCount);
//
//		// Fields
//		//private static HandleChangeEventHandler HandleAdded;
//		//private static HandleChangeEventHandler HandleRemoved;
//		private static int handleTypeCount;
//		private static HandleType[] handleTypes;
//
//		// Events
//		internal static  event HandleChangeEventHandler HandleAdded;
//		internal static  event HandleChangeEventHandler HandleRemoved;
//
//		static HandleCollector()
//		{
//			HandleCollector.handleTypes = null;
//			HandleCollector.handleTypeCount = 0;
//		}
//
//		public HandleCollector()
//		{
//		}
//
//		public static IntPtr Add(IntPtr handle, int type)
//		{
//			HandleCollector.handleTypes[type - 1].Add(handle);
//			return handle;
//		}
// 
//		public static int RegisterType(string typeName, int expense, int initialThreshold)
//		{
//			int num1;
//			lock (typeof(HandleCollector))
//			{
//				if ((HandleCollector.handleTypeCount == 0) || (HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length))
//				{
//					HandleCollector.HandleType[] typeArray1 = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
//					if (HandleCollector.handleTypes != null)
//					{
//						Array.Copy(HandleCollector.handleTypes, 0, typeArray1, 0, HandleCollector.handleTypeCount);
//					}
//					HandleCollector.handleTypes = typeArray1;
//				}
//				HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
//				num1 = HandleCollector.handleTypeCount;
//			}
//			return num1;
//		}
// 
//		internal static IntPtr Remove(IntPtr handle, int type)
//		{
//			return HandleCollector.handleTypes[type - 1].Remove(handle);
//		}
//
//
//
//		// Nested Types
//		private class HandleType
//		{
//			internal HandleType(string name, int expense, int initialThreshHold)
//			{
//				this.name = name;
//				this.initialThreshHold = initialThreshHold;
//				this.threshHold = initialThreshHold;
//				this.handleCount = 0;
//				this.deltaPercent = 100 - expense;
//			}
//			internal void Add(IntPtr handle)
//			{
//				bool flag1 = false;
//				lock (this)
//				{
//					this.handleCount++;
//					flag1 = this.NeedCollection();
//					lock (typeof(HandleCollector))
//					{
//						if (HandleCollector.HandleAdded != null)
//						{
//							HandleCollector.HandleAdded(this.name, handle, this.GetHandleCount());
//						}
//					}
//					if (!flag1)
//					{
//						return;
//					}
//				}
//				if (flag1)
//				{
//					GC.Collect();
//					int num1 = (100 - this.deltaPercent) / 4;
//					System.Threading.Thread.Sleep(num1);
//				}
//			}
// 
//			internal int GetHandleCount()
//			{
//				int num1;
//				lock (this)
//				{
//					num1 = this.handleCount;
//				}
//				return num1;
//			}
//			internal bool NeedCollection()
//			{
//				if (this.handleCount > this.threshHold)
//				{
//					this.threshHold = this.handleCount + ((this.handleCount * this.deltaPercent) / 100);
//					return true;
//				}
//				int num1 = (100 * this.threshHold) / (100 + this.deltaPercent);
//				if ((num1 >= this.initialThreshHold) && (this.handleCount < ((int) (num1 * 0.9f))))
//				{
//					this.threshHold = num1;
//				}
//				return false;
//			}
//			internal IntPtr Remove(IntPtr handle)
//			{
//				IntPtr ptr1;
//				lock (this)
//				{
//					this.handleCount--;
//					this.handleCount = Math.Max(0, this.handleCount);
//					lock (typeof(HandleCollector))
//					{
//						if (HandleCollector.HandleRemoved != null)
//						{
//							HandleCollector.HandleRemoved(this.name, handle, this.GetHandleCount());
//						}
//					}
//					ptr1 = handle;
//				}
//				return ptr1;
//			}
// 
//			// Fields
//			private readonly int deltaPercent;
//			private int handleCount;
//			private int initialThreshHold;
//			internal readonly string name;
//			private int threshHold;
//		}
//	}
	#endregion

	#region POINTSTRUCT

	[StructLayout(LayoutKind.Sequential)]
	public struct POINTSTRUCT
	{
		public int x;
		public int y;
		public POINTSTRUCT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
	#endregion

	#region String
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct STRINGBUFFER
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
		public string szText;
	}

	#endregion

	#region LogBrush

	/// <summary>
	/// LogBrush
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LogBrush
	{
		/// <summary>
		/// lbStyle
		/// </summary>
		public int lbStyle; 
		/// <summary>
		/// lbColor
		/// </summary>
		public int lbColor; 
		/// <summary>
		/// lbHatch
		/// </summary>
		public int lbHatch; 
	}

	#endregion

	#region GDI Rect

	/// <summary>
	/// Rect
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Rect
	{
		/// <summary>
		/// Left
		/// </summary>
		public int Left;
		/// <summary>
		/// Top
		/// </summary>
		public int Top;
		/// <summary>
		/// Right
		/// </summary>
		public int Right;
		/// <summary>
		/// Bottom
		/// </summary>
		public int Bottom;
	}

	#endregion

	#region CREATESTRUCT

	[StructLayout(LayoutKind.Sequential)]
	public struct CREATESTRUCT
	{
		IntPtr    lpCreateParams; 
		IntPtr    hInstance; 
		IntPtr    hMenu; 
		IntPtr    hwndParent; 
		int       cy; 
		int       cx; 
		int       y; 
		int       x; 
		Int32     style; 
		string    lpszName; 
		string    lpszClass; 
		UInt32    dwExStyle; 
	}
	#endregion

	#region SpinControls
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct UDACCEL
	{
		public  UInt32 nSec;
		public  UInt32 nInc;
	}
	#endregion
	
 	#region INITCOMMONCONTROLSEX
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public class INITCOMMONCONTROLSEX 
	{
		public int dwSize;
		public int dwICC;
	}
	#endregion

	#region TBBUTTON
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct TBBUTTON 
	{
		public int iBitmap;
		public int idCommand;
		public byte fsState;
		public byte fsStyle;
		public byte bReserved0;
		public byte bReserved1;
		public int dwData;
		public int iString;
	}
	#endregion

	#region NMHDR
	[StructLayout(LayoutKind.Sequential)]
	public struct NMHDR
	{
		public IntPtr hwndFrom;
		public int idFrom;
		public int code;
	}
	#endregion

	#region TOOLTIPTEXTA
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	public struct TOOLTIPTEXTA
	{
		public NMHDR hdr;
		public IntPtr lpszText;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
		public string szText;
		public IntPtr hinst;
		public int uFlags;
	}
	#endregion

	#region TOOLTIPTEXT
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TOOLTIPTEXT
	{
		public NMHDR hdr;
		public IntPtr lpszText;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
		public string szText;
		public IntPtr hinst;
		public int uFlags;
	}
	#endregion

	#region NMCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMCUSTOMDRAW
	{
		public NMHDR hdr;
		public int dwDrawStage;
		public IntPtr hdc;
		public RECT rc;
		public int dwItemSpec;
		public int uItemState;
		public int lItemlParam;
	}
	#endregion

	#region NMTBCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMTBCUSTOMDRAW
	{
		public NMCUSTOMDRAW nmcd;
		public IntPtr hbrMonoDither;
		public IntPtr hbrLines;
		public IntPtr hpenLines;
		public int clrText;
		public int clrMark;
		public int clrTextHighlight;
		public int clrBtnFace;
		public int clrBtnHighlight;
		public int clrHighlightHotTrack;
		public RECT rcText;
		public int nStringBkMode;
		public int nHLStringBkMode;
	}
	#endregion
	
	#region NMLVCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMLVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iSubItem;
	} 
	#endregion

	#region TBBUTTONINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TBBUTTONINFO
	{
		public int cbSize;
		public int dwMask;
		public int idCommand;
		public int iImage;
		public byte fsState;
		public byte fsStyle;
		public short cx;
		public IntPtr lParam;
		public IntPtr pszText;
		public int cchText;
	}
	#endregion

	#region REBARBANDINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct REBARBANDINFO
	{
		public int cbSize;
		public int fMask;
		public int fStyle;
		public int clrFore;
		public int clrBack;
		public IntPtr lpText;
		public int cch;
		public int iImage;
		public IntPtr hwndChild;
		public int cxMinChild;
		public int cyMinChild;
		public int cx;
		public IntPtr hbmBack;
		public int wID;
		public int cyChild;
		public int cyMaxChild;
		public int cyIntegral;
		public int cxIdeal;
		public int lParam;
		public int cxHeader;
	}
	#endregion

	#region NMTOOLBAR
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMTOOLBAR 
	{
		public NMHDR		hdr;
		public int		    iItem;
		public TBBUTTON	    tbButton;
		public int		    cchText;
		public IntPtr		pszText;
		public RECT		    rcButton; 
	}
	#endregion
	
	#region NMREBARCHEVRON
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMREBARCHEVRON
	{
		public NMHDR hdr;
		public int uBand;
		public int wID;
		public int lParam;
		public RECT rc;
		public int lParamNM;
	}
	#endregion

	#region BITMAP
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAP
	{
		public long   bmType; 
		public long   bmWidth; 
		public long   bmHeight; 
		public long   bmWidthBytes; 
		public short  bmPlanes; 
		public short  bmBitsPixel; 
		public IntPtr bmBits; 
	}
	#endregion
 
	#region BITMAPINFO_FLAT
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAPINFO_FLAT 
	{
		public int      bmiHeader_biSize;
		public int      bmiHeader_biWidth;
		public int      bmiHeader_biHeight;
		public short    bmiHeader_biPlanes;
		public short    bmiHeader_biBitCount;
		public int      bmiHeader_biCompression;
		public int      bmiHeader_biSizeImage;
		public int      bmiHeader_biXPelsPerMeter;
		public int      bmiHeader_biYPelsPerMeter;
		public int      bmiHeader_biClrUsed;
		public int      bmiHeader_biClrImportant;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
		public byte[] bmiColors; 
	}
	#endregion

	#region RGBQUAD
	public struct RGBQUAD 
	{
		public byte		rgbBlue;
		public byte		rgbGreen;
		public byte		rgbRed;
		public byte		rgbReserved;
	}
	#endregion
	
	#region BITMAPINFOHEADER
	[StructLayout(LayoutKind.Sequential)]
	public class BITMAPINFOHEADER 
	{
		public int      biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
		public int      biWidth;
		public int      biHeight;
		public short    biPlanes;
		public short    biBitCount;
		public int      biCompression;
		public int      biSizeImage;
		public int      biXPelsPerMeter;
		public int      biYPelsPerMeter;
		public int      biClrUsed;
		public int      biClrImportant;
	}
	#endregion

	#region BITMAPINFO
	[StructLayout(LayoutKind.Sequential)]
	public class BITMAPINFO 
	{
		public BITMAPINFOHEADER bmiHeader = new BITMAPINFOHEADER();
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
		public byte[] bmiColors; 
	}
	#endregion

	#region PALETTEENTRY
	[StructLayout(LayoutKind.Sequential)]
	public struct PALETTEENTRY 
	{
		public byte		peRed;
		public byte		peGreen;
		public byte		peBlue;
		public byte		peFlags;
	}
	#endregion

	#region HD_HITTESTINFO
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct HD_HITTESTINFO 
	{  
		public POINT pt;  
		public uint flags; 
		public int iItem; 
	}
	#endregion
 
	#region DLLVERSIONINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct DLLVERSIONINFO
	{
		public int cbSize;
		public int dwMajorVersion;
		public int dwMinorVersion;
		public int dwBuildNumber;
		public int dwPlatformID;
	}
	#endregion

	#region NMTVCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct NMTVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iLevel;
	}
	#endregion

	#region TVITEMEX
	[StructLayout(LayoutKind.Sequential)]
	public struct TVITEMEX
	{
		public int mask;
		public IntPtr hItem;
		public int state;
		public int stateMask;
		public string pszText;
		public int cchTextMax;
		public int iImage;
		public int iSelectedImage;
		public int cChildren;
		public IntPtr lParam;
		public int iIntegral;
	}
	#endregion

	#region TVITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct TVITEM 
	{
		public	uint      mask;
		public	IntPtr    hItem;
		public	uint      state;
		public	uint      stateMask;
		public	IntPtr    pszText;
		public	int       cchTextMax;
		public	int       iImage;
		public	int       iSelectedImage;
		public	int       cChildren;
		public	int       lParam;
	} 
	#endregion

	#region LVITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct LVITEM
	{
		public	uint mask;
		public	int iItem;
		public	int iSubItem;
		public	uint state;
		public	uint stateMask;
		public	IntPtr pszText;
		public	int cchTextMax;
		public	int iImage;
		public	int lParam;
		public	int iIndent;
	}
	#endregion

	#region HDITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct HDITEM
	{
		public	uint    mask;
		public	int     cxy;
		public	IntPtr  pszText;
		public	IntPtr  hbm;
		public	int     cchTextMax;
		public	int     fmt;
		public	int     lParam;
		public	int     iImage;      
		public	int     iOrder;
	}	
	#endregion

	#region WINDOWPLACEMENT
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct WINDOWPLACEMENT
	{	
		public uint length; 
		public uint flags; 
		public uint showCmd; 
		public POINT ptMinPosition; 
		public POINT ptMaxPosition; 
		public RECT  rcNormalPosition; 
	}
	#endregion

	#region SCROLLINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct SCROLLINFO
	{
		public 	uint   cbSize;
		public 	uint   fMask;
		public 	int    nMin;
		public 	int    nMax;
		public 	uint   nPage;
		public 	int    nPos;
		public 	int    nTrackPos;
	}
	#endregion

	#region NOTIFYICONDATA
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)] 
	//[CLSCompliantAttribute(false)]
	public struct NOTIFYICONDATA
	{
		public UInt32           cbSize;                       // DWORD
		public IntPtr           hWnd;                         // HWND
		public UInt32           uID;                          // UINT
		public NotifyFlags      uFlags;                       // UINT
		public UInt32           uCallbackMessage;             // UINT
		public IntPtr           hIcon;                        // HICON
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
		public string           szTip;                        // char[128]
		public NotifyState      dwState;                      // DWORD   
		public NotifyState      dwStateMask;                  // DWORD
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
		public string           szInfo;                       // char[256]
		public UInt32           uTimeoutOrVersion;            // UINT
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)]
		public string           szInfoTitle;                  // char[64]
		public NotifyInfoFlags  dwInfoFlags;                  // DWORD
	}
	#endregion

	#region PARAFORMAT2
	/*
	typedef struct _paraformat { 
	  UINT cbSize; 
	  DWORD dwMask; 
	  WORD  wNumbering; 
	  WORD  wEffects; 
	  LONG  dxStartIndent; 
	  LONG  dxRightIndent; 
	  LONG  dxOffset; 
	  WORD  wAlignment; 
	  SHORT cTabCount; 
	  LONG  rgxTabs[MAX_TAB_STOPS]; 
	  LONG  dySpaceBefore; 
	  LONG  dySpaceAfter; 
	  LONG  dyLineSpacing; 
	  SHORT sStyle; 
	  BYTE  bLineSpacingRule; 
	  BYTE  bOutlineLevel; 
	  WORD  wShadingWeight; 
	  WORD  wShadingStyle;
	  WORD  wNumberingStart; 
	  WORD  wNumberingStyle; 
	  WORD  wNumberingTab; 
	  WORD  wBorderSpace; 
	  WORD  wBorderWidth; 
	  WORD  wBorders; 
	} PARAFORMAT2;
   */


	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	//[CLSCompliantAttribute(false)]
	public struct PARAFORMAT2 
	{ 
		public UInt32  cbSize; 
		public UInt32  dwMask; 
		public UInt16  wNumbering; 
		public UInt16  wEffects; 
		public Int32   dxStartIndent; 
		public Int32   dxRightIndent; 
		public Int32   dxOffset; 
		public UInt16  wAlignment; 
		public Int16   cTabCount; 
		[MarshalAs( UnmanagedType.ByValArray, SizeConst=32) ]
		public Int32[] rgxTabs; 
		public Int32   dySpaceBefore; 
		public Int32   dySpaceAfter; 
		public Int32   dyLineSpacing; 
		public Int16   sStyle; 
		public Byte    bLineSpacingRule; 
		public Byte    bOutlineLevel; 
		public UInt16  wShadingWeight; 
		public UInt16  wShadingStyle;
		public UInt16  wNumberingStart; 
		public UInt16  wNumberingStyle; 
		public UInt16  wNumberingTab; 
		public UInt16  wBorderSpace; 
		public UInt16  wBorderWidth; 
		public UInt16  wBorders; 
	}  
	#endregion

	#region Size and Points

	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct MSG 
    {
        public IntPtr hwnd;
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public int pt_x;
        public int pt_y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public Rectangle rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }

    [StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		public static Win32.RECT FromXYWH(int x, int y, int width, int height)
		{
			return new Win32.RECT(x, y, x + width, y + height);
		}

		public static implicit operator Rectangle( RECT rect ) 
		{
			return new Rectangle( rect.left, rect.top, 
				rect.right - rect.left, rect.bottom - rect.top );
		}
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
		//public POINT(){}
		public POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;

		//public SIZE(){}
		public SIZE(int cx, int cy)
		{
			this.cx = cx;
			this.cy = cy;
		}
    }

    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct LOGBRUSH
    {
        public int lbStyle; 
        public int lbColor; 
        public int lbHatch; 
    }

	#endregion

	#region Mouse

	[StructLayout(LayoutKind.Sequential)]
	//[CLSCompliantAttribute(false)]
	public struct TRACKMOUSEEVENTS
	{
		public uint cbSize;
		public uint dwFlags;
		public IntPtr hWnd;
		public uint dwHoverTime;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSEHOOKSTRUCT 
	{ 
		public POINT     pt; 
		public IntPtr    hwnd; 
		public int       wHitTestCode; 
		public IntPtr    dwExtraInfo; 
	}
	#endregion
 
}
