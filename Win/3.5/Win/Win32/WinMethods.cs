using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Drawing;


namespace MControl.Win32
{
	/// <summary>
	/// Summary description for WinMethods.
	/// </summary>
	public class WinMethods
	{
		public WinMethods()	{}
	
		#region NativeMethods

		public static HandleRef HWND_NOTOPMOST;
		public static readonly int TTM_DELTOOL;
		public static readonly int TTM_ADDTOOL;
 


		[StructLayout(LayoutKind.Sequential)]
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

			public static WinMethods.RECT FromXYWH(int x, int y, int width, int height)
			{
				return new WinMethods.RECT(x, y, x + width, y + height);
			}

		}



		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class TOOLINFO_T
		{
			public int cbSize;
			public int uFlags;
			public IntPtr hwnd;
			public IntPtr uId;
			public WinMethods.RECT rect;
			public IntPtr hinst;
			public string lpszText;
			public IntPtr lParam;
			public TOOLINFO_T()
			{
				this.cbSize = Marshal.SizeOf(typeof(WinMethods.TOOLINFO_T));
			}

 
		}
 
 
//		public sealed class CommonHandles
//		{
//			// Methods
//			static CommonHandles()
//			{
//				WinMethods.CommonHandles.Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
//				WinMethods.CommonHandles.Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
//				WinMethods.CommonHandles.EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
//				WinMethods.CommonHandles.Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
//				WinMethods.CommonHandles.GDI = HandleCollector.RegisterType("GDI", 90, 50);
//				WinMethods.CommonHandles.HDC = HandleCollector.RegisterType("HDC", 100, 2);
//				WinMethods.CommonHandles.Icon = HandleCollector.RegisterType("Icon", 20, 500);
//				WinMethods.CommonHandles.Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
//				WinMethods.CommonHandles.Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
//				WinMethods.CommonHandles.Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
//			}
//
// 
//
//			public CommonHandles()
//			{
//			}
//
//
//			// Fields
//			public static readonly int Accelerator;
//			public static readonly int Cursor;
//			public static readonly int EMF;
//			public static readonly int Find;
//			public static readonly int GDI;
//			public static readonly int HDC;
//			public static readonly int Icon;
//			public static readonly int Kernel;
//			public static readonly int Menu;
//			public static readonly int Window;
//		}
// 
		[StructLayout(LayoutKind.Sequential, Pack=1)]
			public class INITCOMMONCONTROLSEX
		{
			public int dwSize;
			public int dwICC;
			public INITCOMMONCONTROLSEX()
			{
				this.dwSize = 8;
			}

 
		}
 
		#endregion

		#region Grid

		public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
		{
			return HandleCollector.Add(WinMethods.IntGetDCEx(hWnd, hrgnClip, flags), CommonHandles.HDC);
		}
		[DllImport("user32.dll", EntryPoint="GetDCEx", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);


		public static IntPtr CreateHalftoneHBRUSH()
		{
			short[] numArray1 = new short[8];
			for (int num1 = 0; num1 < 8; num1++)
			{
				numArray1[num1] = (short) (0x5555 << ((num1 & 1) & 0x1f));
			}
			IntPtr ptr1 = WinMethods.CreateBitmap(8, 8, 1, 1, numArray1);
			WinMethods.LOGBRUSH logbrush1 = new WinMethods.LOGBRUSH();
			logbrush1.lbColor = WinMethods.ToWin32(Color.Black);
			logbrush1.lbStyle = 3;
			logbrush1.lbHatch = ptr1;
			IntPtr ptr2 = WinMethods.CreateBrushIndirect(logbrush1);
			WinMethods.DeleteObject(new HandleRef(null, ptr1));
			return ptr2;
		}

 
		public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
		{
			return HandleCollector.Add(WinMethods.IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits),   CommonHandles.GDI);
		}

		[DllImport("gdi32.dll", EntryPoint="CreateBitmap", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);

		[StructLayout(LayoutKind.Sequential)]
			public class LOGBRUSH
		{
			public int lbStyle;
			public int lbColor;
			public IntPtr lbHatch;
			public LOGBRUSH(){}
		}
 
		public static int ToWin32(Color c)
		{
			return ((c.R | (c.G << 8)) | (c.B << 0x10));
		}

 
		public static IntPtr CreateBrushIndirect(WinMethods.LOGBRUSH lb)
		{
			return HandleCollector.Add(WinMethods.IntCreateBrushIndirect(lb),  CommonHandles.GDI);
		}

 
		[DllImport("gdi32.dll", EntryPoint="CreateBrushIndirect", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntCreateBrushIndirect(WinMethods.LOGBRUSH lb);
 
//		public static bool DeleteObject(HandleRef hObject)
//		{
//			HandleCollector.Remove((IntPtr) hObject,  NativeMethods.CommonHandles.GDI);
//			return NativeMethods.IntDeleteObject(hObject);
//		}

//		[DllImport("gdi32.dll", EntryPoint="DeleteObject", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern bool IntDeleteObject(HandleRef hObject);
// 

//		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
//		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);
 
//		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
//		{
//			HandleCollector.Remove((IntPtr) hDC,  NativeMethods.CommonHandles.HDC);
//			return NativeMethods.IntReleaseDC(hWnd, hDC);
//		}

//		[DllImport("user32.dll", EntryPoint="ReleaseDC", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wparam, WinMethods.TV_HITTESTINFO lparam);

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1), ComVisible(false)]
		public class TV_HITTESTINFO
		{
			public int pt_x;
			public int pt_y;
			public int flags;
			public int hItem;
			public TV_HITTESTINFO(){}
		}

		#endregion

		#region SafeNativeMethods

		//			public sealed class CommonHandles
		//			{
		//				// Methods
		//				static CommonHandles()
		//				{
		//					WinMethods.CommonHandles.Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
		//					WinMethods.CommonHandles.Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
		//					WinMethods.CommonHandles.EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
		//					WinMethods.CommonHandles.Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
		//					WinMethods.CommonHandles.GDI = HandleCollector.RegisterType("GDI", 90, 50);
		//					WinMethods.CommonHandles.HDC = HandleCollector.RegisterType("HDC", 100, 2);
		//					WinMethods.CommonHandles.Icon = HandleCollector.RegisterType("Icon", 20, 500);
		//					WinMethods.CommonHandles.Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
		//					WinMethods.CommonHandles.Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
		//					WinMethods.CommonHandles.Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
		//				}
		//
		//				public CommonHandles()
		//				{
		//				}
		//
		//
		//				// Fields
		//				public static readonly int Accelerator;
		//				public static readonly int Cursor;
		//				public static readonly int EMF;
		//				public static readonly int Find;
		//				public static readonly int GDI;
		//				public static readonly int HDC;
		//				public static readonly int Icon;
		//				public static readonly int Kernel;
		//				public static readonly int Menu;
		//				public static readonly int Window;
		//			}
		// 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref WinMethods.RECT rectScrollRegion, ref WinMethods.RECT rectClip);
	
		//[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		//public static extern bool ScrollWindowEx(HandleRef hWnd, int nXAmount, int nYAmount, WinMethods.COMRECT rectScrollRegion, ref WinMethods.RECT rectClip, HandleRef hrgnUpdate, ref WinMethods.RECT prcUpdate, int flags);
 

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);
 
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SelectObject(HandleRef hdc, int obj);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
		[DllImport("gdi32.dll", EntryPoint="DeleteObject", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern bool IntDeleteObject(HandleRef hObject);
 

		public static bool DeleteObject(HandleRef hObject)
		{
			HandleCollector.Remove((IntPtr) hObject, CommonHandles.GDI);
			return WinMethods.IntDeleteObject(hObject);
		}

 
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(WinMethods.INITCOMMONCONTROLSEX icc);
 
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int GetRegionData(HandleRef hRgn, int size, byte[] data);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool ReleaseCapture();
 
		#endregion

		#region UnsafeNativeMethods

		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);
		[DllImport("user32.dll", ExactSpelling=true)]
		public static extern IntPtr GetProcessWindowStation();
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int GetSystemMetrics(int nIndex);
		//		[DllImport("user32.dll", SetLastError=true)]
		//		public static extern bool GetUserObjectInformation(HandleRef hObj, int nIndex, [MarshalAs(UnmanagedType.LPStruct)] SafeNativeMethods.USEROBJECTFLAGS pvBuffer, int nLength, ref int lpnLengthNeeded);
		[DllImport("gdi32.dll", EntryPoint="CreateDC", CharSet=CharSet.Auto)]
		private static extern IntPtr IntCreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);
		[DllImport("gdi32.dll", EntryPoint="CreateIC", CharSet=CharSet.Auto)]
		private static extern IntPtr IntCreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);
		[DllImport("user32.dll", EntryPoint="GetDC", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);
		[DllImport("user32.dll", EntryPoint="ReleaseDC", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);
		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern bool PeekMessage([In, Out] ref SafeNativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);
		//		[DllImport("user32.dll", CharSet=CharSet.Ansi, ExactSpelling=true)]
		//		public static extern bool PeekMessageA([In, Out] ref SafeNativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);
		//		[DllImport("user32.dll", CharSet=CharSet.Unicode, ExactSpelling=true)]
		//		public static extern bool PeekMessageW([In, Out] ref SafeNativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);
		[ReflectionPermission(SecurityAction.Assert, Unrestricted=true), SecurityPermission(SecurityAction.Assert)]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted=true), SecurityPermission(SecurityAction.Assert)]
		public static void PtrToStructure(IntPtr lparam, object data)
		{
			Marshal.PtrToStructure(lparam, data);
		}

		[ReflectionPermission(SecurityAction.Assert, Unrestricted=true), SecurityPermission(SecurityAction.Assert)]
		public static object PtrToStructure(IntPtr lparam, Type cls)
		{
			return Marshal.PtrToStructure(lparam, cls);
		}

		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			HandleCollector.Remove((IntPtr) hDC, CommonHandles.HDC);
			return WinMethods.IntReleaseDC(hWnd, hDC);
		}

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, WinMethods.TOOLINFO_T lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetCapture();
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SetCapture(HandleRef hwnd);
 

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SetFocus(HandleRef hWnd);
 

		//	public static void SendMessage(HanderRef handerRef, IntPtr  , 0x403, 3, 0)
		//	{
		//		//WinAPI.SendMessage();
		//	}

		#endregion

		#region WinForms WinMethods

		#region PrintPreview

		//		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		//		public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref Win32.RECT rectScrollRegion, ref Win32.RECT rectClip);
		// 
		//		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
		//		public static extern int SetScrollPos(HandleRef hWnd, int nBar, int nPos, bool bRedraw);
		//
		//		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		//		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);
		// 
		//
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int SetScrollInfo(HandleRef hWnd, int fnBar, SCROLLINFO si, bool redraw);
		//
		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern bool PeekMessage([In, Out] ref Win32.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		public static Win32.RECT FromXYWH(int x, int y, int width, int height)
		{
			return new Win32.RECT(x, y, x + width, y + height);
		}

		[Serializable]
			internal enum StackCrawlMark
		{
			LookForMe,
			LookForMyCaller,
			LookForMyCallersCaller
		}


		//		[StructLayout(LayoutKind.Sequential)]
		//			public struct RECT
		//		{
		//			public RECT(int left, int top, int right, int bottom)
		//			{
		//				this.left = left;
		//				this.top = top;
		//				this.right = right;
		//				this.bottom = bottom;
		//			}
		//
		//			public static WinMethods.RECT FromXYWH(int x, int y, int width, int height)
		//			{
		//				return new WinMethods.RECT(x, y, x + width, y + height);
		//			}
		//
		//
		//			public int left;
		//			public int top;
		//			public int right;
		//			public int bottom;
		//			//public RECT(int left, int top, int right, int bottom);
		//			//public static WinMethods.RECT FromXYWH(int x, int y, int width, int height);
		//		}

		[StructLayout(LayoutKind.Sequential)]
			public class SCROLLINFO
		{
			public SCROLLINFO()
			{
				this.cbSize = Marshal.SizeOf(typeof(WinMethods.SCROLLINFO));
			}

			public SCROLLINFO(int mask, int min, int max, int page, int pos)
			{
				this.cbSize = Marshal.SizeOf(typeof(WinMethods.SCROLLINFO));
				this.fMask = mask;
				this.nMin = min;
				this.nMax = max;
				this.nPage = page;
				this.nPos = pos;
			}

			public int cbSize;
			public int fMask;
			public int nMin;
			public int nMax;
			public int nPage;
			public int nPos;
			public int nTrackPos;
			//public SCROLLINFO();
			//public SCROLLINFO(int mask, int min, int max, int page, int pos);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public static int LOWORD(IntPtr n)
		{
			return WinMethods.LOWORD((int) n);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		public static int LOWORD(int n)
		{
			return (n & 0xffff);
		}

		public static int HIWORD(IntPtr n)
		{
			return WinMethods.HIWORD((int) n);
		}

		public static int HIWORD(int n)
		{
			return ((n >> 0x10) & 0xffff);
		}

		//		public static string GetString(string name)
		//		{
		//			return SR.GetString(null, name);
		//		}


		//		private static SR loader;
		//
		//		public static string GetString(CultureInfo culture, string name)
		//		{
		//			SR sr1 = SR.GetLoader();
		//			if (sr1 == null)
		//			{
		//				return null;
		//			}
		//			return sr1.resources.GetString(name, culture);
		//		}
		//
		//		private static SR GetLoader()
		//		{
		//			if (SR.loader == null)
		//			{
		//				lock (typeof(SR))
		//				{
		//					if (SR.loader == null)
		//					{
		//						SR.loader = new SR();
		//					}
		//				}
		//			}
		//			return SR.loader;
		//		}


 

 
		//private static readonly ContentAlignment anyBottom;
		//private static readonly ContentAlignment anyMiddle;

		public static StringAlignment TranslateLineAlignment(ContentAlignment align)
		{
			//if ((align & ControlPaint.anyBottom) != ((ContentAlignment) 0))
			if ((align == ContentAlignment.BottomCenter)||(align == ContentAlignment.BottomLeft)||(align == ContentAlignment.BottomRight) )
			{
				return StringAlignment.Far;
			}
			if ((align == ContentAlignment.MiddleCenter)||(align == ContentAlignment.MiddleLeft)||(align == ContentAlignment.MiddleRight) )
			{
				return StringAlignment.Center;
			}
			return StringAlignment.Near;
		}

		#endregion

		#region TabControl

		public static HandleRef HWND_TOPMOST;
		public static readonly int TCM_INSERTITEM;
		public static readonly int TCM_SETITEM;


		[Flags]
			public enum TabControlHitTest
		{
			// Fields
			TCHT_NOWHERE = 1,
			TCHT_ONITEMICON = 2,
			TCHT_ONITEMLABEL = 4
		}

		[StructLayout(LayoutKind.Sequential), ComVisible(false)]
			public class TCHITTESTINFO
		{
			public Point pt;
			public TabControlHitTest flags;
			public TCHITTESTINFO(){}
		}
 

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, TCITEM_T lParam);
 
		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);
		// 


		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
 

		public static IntPtr MAKELPARAM(int low, int high)
		{
			return (IntPtr) ((high << 0x10) | (low & 0xffff));
		}

		//		internal void RemoveWindowFromIDTable(IntPtr handle)
		//		{
		//			short num1 = (short) NativeWindow.hashForHandleId[handle];
		//			NativeWindow.hashForHandleId.Remove(handle);
		//			NativeWindow.hashForIdHandle.Remove(num1);
		//		}
		//
		//		internal void AddWindowToIDTable(IntPtr handle)
		//		{
		//			NativeWindow.hashForIdHandle[NativeWindow.globalID] = handle;
		//			NativeWindow.hashForHandleId[handle] = NativeWindow.globalID;
		//			Win32.WinAPI.SetWindowLong(new HandleRef(this, handle), -12, new HandleRef(this, (IntPtr) NativeWindow.globalID));
		//			NativeWindow.globalID = (short) (NativeWindow.globalID + 1);
		//		}


 
		[StructLayout(LayoutKind.Sequential)]
			public struct NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}
 

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class TCITEM_T
		{
			public int mask;
			public int dwState;
			public int dwStateMask;
			public string pszText;
			public int cchTextMax;
			public int iImage;
			public IntPtr lParam;
			public TCITEM_T(){}
		}


		//			[StructLayout(LayoutKind.Sequential, Pack=1)]
		//				public class INITCOMMONCONTROLSEX
		//			{
		//				public int dwSize;
		//				public int dwICC;
		//				public INITCOMMONCONTROLSEX()
		//				{
		//					this.dwSize = 8;
		//				}
		//			}

		[StructLayout(LayoutKind.Sequential)]
			public class DRAWITEMSTRUCT
		{
			public int CtlType;
			public int CtlID;
			public int itemID;
			public int itemAction;
			public int itemState;
			public IntPtr hwndItem;
			public IntPtr hDC;
			public Win32.RECT rcItem;
			public IntPtr itemData;
			public DRAWITEMSTRUCT(){}
		}
 

		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
 
		//		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		//		public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);
	
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wparam, TCHITTESTINFO lparam);

		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In, Out] ref Win32.RECT lParam);

		internal static string WindowMessagesVersion
		{
			get
			{
				return "WindowsForms11";
			}
		}
 


		#endregion

		#region Menu Bar

		//		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		//		public static extern bool DrawMenuBar(HandleRef hWnd);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

		#endregion

		#region DateTime

		[StructLayout(LayoutKind.Sequential)]
			public class SYSTEMTIME
		{
			public short wYear;
			public short wMonth;
			public short wDayOfWeek;
			public short wDay;
			public short wHour;
			public short wMinute;
			public short wSecond;
			public short wMilliseconds;
			public override string ToString()
			{
				return string.Concat(new string[] { "[SYSTEMTIME: ", this.wDay.ToString(), "/", this.wMonth.ToString(), "/", this.wYear.ToString(), " ", this.wHour.ToString(), ":", this.wMinute.ToString(), ":", this.wSecond.ToString(), "]" });
			}


			public SYSTEMTIME(){}
		}

		internal static SYSTEMTIME DateTimeToSysTime(DateTime time)
		{
			SYSTEMTIME systemtime1 = new SYSTEMTIME();
			systemtime1.wYear = (short) time.Year;
			systemtime1.wMonth = (short) time.Month;
			systemtime1.wDayOfWeek = (short) time.DayOfWeek;
			systemtime1.wDay = (short) time.Day;
			systemtime1.wHour = (short) time.Hour;
			systemtime1.wMinute = (short) time.Minute;
			systemtime1.wSecond = (short) time.Second;
			systemtime1.wMilliseconds = 0;
			return systemtime1;
		}

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, SYSTEMTIME lParam);


		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class SYSTEMTIMEARRAY
		{
			public short wYear1;
			public short wMonth1;
			public short wDayOfWeek1;
			public short wDay1;
			public short wHour1;
			public short wMinute1;
			public short wSecond1;
			public short wMilliseconds1;
			public short wYear2;
			public short wMonth2;
			public short wDayOfWeek2;
			public short wDay2;
			public short wHour2;
			public short wMinute2;
			public short wSecond2;
			public short wMilliseconds2;
			public SYSTEMTIMEARRAY(){}
		}

		public delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);


		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, SYSTEMTIMEARRAY lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool InvalidateRect(HandleRef hWnd, COMRECT rect, bool erase);
 
		[DllImport("user32.dll", ExactSpelling=true)]
		public static extern bool EnumChildWindows(HandleRef hwndParent, EnumChildrenCallback lpEnumFunc, HandleRef lParam);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool UpdateWindow(HandleRef hWnd);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);



		[StructLayout(LayoutKind.Sequential)]
			public class COMRECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
			public COMRECT(){}
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
			public static COMRECT FromXYWH(int x, int y, int width, int height)
			{
				return new COMRECT(x, y, x + width, y + height);
			}

			public override string ToString()
			{
				return string.Concat(new object[] { "Left = ", this.left, " Top ", this.top, " Right = ", this.right, " Bottom = ", this.bottom });
			}
 
		}

		#endregion

		#region Combo Box
		//ComboBox
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int[] wParam, int[] lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		//			[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//			public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
		// 

		//		public static IntPtr MAKELPARAM(int low, int high)
		//		{
		//			return (IntPtr) ((high << 0x10) | (low & 0xffff));
		//		}

 
		#endregion

		#region Text Box

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, ref int wParam, ref int lParam);
 

		#endregion


		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref Win32.RECT rect);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool GetClientRect(HandleRef hWnd, [In, Out] ref Win32.RECT rect);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetSysColorBrush(int nIndex);

		[DllImport("gdi32.dll", EntryPoint="CreateSolidBrush", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntCreateSolidBrush(int crColor);

		public static IntPtr CreateSolidBrush(int crColor)
		{
			return Win32.HandleCollector.Add(IntCreateSolidBrush(crColor), Win32.CommonHandles.GDI);
		}

		public static HandleRef NullHandleRef;
 

		[StructLayout(LayoutKind.Sequential)]
			public class MEASUREITEMSTRUCT
		{
			public int CtlType;
			public int CtlID;
			public int itemID;
			public int itemWidth;
			public int itemHeight;
			public IntPtr itemData;
			public MEASUREITEMSTRUCT(){}
		}

		#endregion

		#region DateTime

		//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		//		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, WinMethods.SYSTEMTIME lParam);


		//		[StructLayout(LayoutKind.Sequential)]
		//			public struct NMHDR
		//		{
		//			public IntPtr hwndFrom;
		//			public int idFrom;
		//			public int code;
		//		}
 

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class NMDATETIMECHANGE
		{
			public WinMethods.NMHDR nmhdr;
			public int dwFlags;
			public WinMethods.SYSTEMTIME st;
			public NMDATETIMECHANGE(){}
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class NMDATETIMEFORMAT
		{
			public WinMethods.NMHDR nmhdr;
			public string pszFormat;
			public WinMethods.SYSTEMTIME st;
			public string pszDisplay;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
			public string szDisplay;
			public NMDATETIMEFORMAT(){}
		}

		//		[StructLayout(LayoutKind.Sequential)]
		//			public class SYSTEMTIME
		//		{
		//			public short wYear;
		//			public short wMonth;
		//			public short wDayOfWeek;
		//			public short wDay;
		//			public short wHour;
		//			public short wMinute;
		//			public short wSecond;
		//			public short wMilliseconds;
		//			public override string ToString()
		//			{
		//				return string.Concat(new string[] { "[SYSTEMTIME: ", this.wDay.ToString(), "/", this.wMonth.ToString(), "/", this.wYear.ToString(), " ", this.wHour.ToString(), ":", this.wMinute.ToString(), ":", this.wSecond.ToString(), "]" });
		//			}
		//			public SYSTEMTIME(){}
		//		}

		internal static DateTime SysTimeToDateTime(WinMethods.SYSTEMTIME s)
		{
			return new DateTime(s.wYear, s.wMonth, s.wDay, s.wHour, s.wMinute, s.wSecond);
		}


		//		public static WinMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
		//		{
		//			WinMethods.SYSTEMTIME systemtime1 = new WinMethods.SYSTEMTIME();
		//			systemtime1.wYear = (short) time.Year;
		//			systemtime1.wMonth = (short) time.Month;
		//			systemtime1.wDayOfWeek = (short) time.DayOfWeek;
		//			systemtime1.wDay = (short) time.Day;
		//			systemtime1.wHour = (short) time.Hour;
		//			systemtime1.wMinute = (short) time.Minute;
		//			systemtime1.wSecond = (short) time.Second;
		//			systemtime1.wMilliseconds = 0;
		//			return systemtime1;
		//		}

	
		#endregion

		#region Drawing

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In, Out] WinMethods.POINT pt, int cPoints);
	
		[StructLayout(LayoutKind.Sequential)]
			public class POINT
		{
			public int x;
			public int y;

			public POINT(){}

			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

		}

		public static IntPtr GetFontHandle(Font font)
		{
			if (font != null)
			{
				FontHandleWrapper wrapper1 =new FontHandleWrapper(font);
				return wrapper1.Handle;
			}
			return IntPtr.Zero;
		}
		#endregion

		#region class FontHandleWrapper

		public sealed class FontHandleWrapper : MarshalByRefObject
		{
			internal FontHandleWrapper(Font font)
			{
				this.handle = font.ToHfont();
			}

			public void Dispose()
			{
				if (this.handle != IntPtr.Zero)
				{
					Win32.WinAPI.DeleteObject(new HandleRef(this, this.handle));
					this.handle = IntPtr.Zero;
				}
			}

			~FontHandleWrapper()
			{
				this.Dispose();
			}

 
			internal IntPtr Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Fields
			private IntPtr handle;
		}
		#endregion

	}
}
