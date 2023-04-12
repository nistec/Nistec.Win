using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace MControl.Win32
{
	/// <summary>
	/// Windows API Functions
	/// </summary>
	//[CLSCompliantAttribute(false)]
	public class  WinAPI
    {

        #region sample

        //#region Constants

        //private const int SW_HIDE = 0;

        //private const int SW_SHOWNORMAL = 1;

        //private const int SW_SHOW = 5;

        //#endregion Constants

        //#region APIs

        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //private static extern bool EnableWindow(IntPtr hwnd, bool enabled);

        //#endregion APIs

        //public static void ShowApp()
        //{

        //    IntPtr h = FindWindow(null, "Form1");

        //    ShowWindow(h, SW_SHOW);

        //    EnableWindow(h, true);

        //}

        //public static void HideApp()
        //{

        //    IntPtr h = FindWindow(null, "Form1");

        //    ShowWindow(h, SW_HIDE);

        //    EnableWindow(h, false);

        //}

        #endregion

        #region delegates

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		#endregion
 
		#region Constructors
		private WinAPI()
		{
		}
		#endregion

		#region USER 32 Send Message

		//[DllImport("user32.dll", EntryPoint="SendMessageA")]
		//public static extern int SendMessage (IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, SpinControlMsg msg, int wParam, ref UDACCEL lParam );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TVITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref LVITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HDITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HD_HITTESTINFO hti);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref PARAFORMAT2 format);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
 



		#endregion

		#region USER32 static extern Methods

		/// <summary>
		/// GetFocus
		/// </summary>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetFocus();

		/// <summary>
		/// SetFocus
		/// </summary>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		/// <summary>
		/// MoveWindow
		/// </summary>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

		/// <summary>
		/// ShowScrollBar
		/// </summary>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int ShowScrollBar(IntPtr hWnd, int bar,  int show);

		/// <summary>
		/// GetPixel
		/// </summary>
//		[DllImport("gdi32.dll")]
//		static public extern int GetPixel(IntPtr hDC, int XPos, int YPos);

		/// <summary>
		/// ShowWindow
		/// </summary>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool ShowWindow(IntPtr hWnd, short State);

		/// <summary>
		/// GetDC
		/// </summary>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint="GetDC", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);
 
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return HandleCollector.Add(IntGetDC(hWnd), Win32.CommonHandles.HDC);
		}



		/// <summary>
		/// ReleaseDC
		/// </summary>
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("user32.dll", EntryPoint="ReleaseDC", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);
 

		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			HandleCollector.Remove((IntPtr) hDC, CommonHandles.HDC);
			return IntReleaseDC(hWnd, hDC);
		}



		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetSysColorBrush(int nIndex);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetActiveWindow();
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SetActiveWindow(HandleRef hWnd);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr FindWindow(string className, string windowName);
 
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
            
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int newLong);
            
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int bRetValue, uint fWinINI);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool AnimateWindow(IntPtr hWnd, uint dwTime, uint dwFlags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool InvalidateRect(IntPtr hWnd, ref RECT rect, bool erase);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SetCursor(IntPtr hCursor);

		[DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetCapture();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ReleaseCapture();

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SetCapture(HandleRef hwnd);
 
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool WaitMessage();

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool TranslateMessage(ref MSG msg);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool DispatchMessage(ref MSG msg);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern uint SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
	
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

		[DllImport("user32.dll", EntryPoint="SendMessageA")]
		public static extern int SendMessage (IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

		[DllImport("user32")]
		public static extern IntPtr GetWindowDC (IntPtr hWnd );

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, uint flags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern ushort GetKeyState(int virtKey);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetParent(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool DrawFocusRect(IntPtr hWnd, ref RECT rect);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool HideCaret(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ShowCaret(IntPtr hWnd);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowText(IntPtr hWnd, string text);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetWindowText(IntPtr hWnd, out STRINGBUFFER text, int maxCount);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhook);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool UpdateWindow(IntPtr hWnd);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool OpenClipboard(IntPtr hWndNewOwner);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool CloseClipboard();
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool EmptyClipboard();
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern IntPtr SetClipboardData( uint Format, IntPtr hData);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int SendDlgItemMessage( IntPtr hWnd, int Id, int msg, int wParam, int lParam );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int InvalidateRect(IntPtr hWnd,  IntPtr rect, int bErase);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int InvalidateRect(IntPtr hWnd,  ref RECT rect, int bErase);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetClassName(IntPtr hWnd,  out STRINGBUFFER ClassName, int nMaxCount);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT wp);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam); 
		[DllImport("user32.dll", CharSet=CharSet.Auto)] 
		static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer); 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int GetSystemMetrics(int nIndex);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int SetScrollInfo(IntPtr hwnd,  int bar, ref SCROLLINFO si, int fRedraw);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int BringWindowToTop(IntPtr hWnd);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, 
			ref RECT rcScroll, ref RECT rcClip, IntPtr UpdateRegion, ref RECT rcInvalidated, uint flags);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int IsWindow(IntPtr hWnd);
		[DllImport("shell32.Dll", CharSet=CharSet.Auto)]
		static public extern int Shell_NotifyIcon( NotifyCommand cmd, ref NOTIFYICONDATA data );
		[DllImport("User32.Dll", CharSet=CharSet.Auto)]
		static public extern int TrackPopupMenuEx( IntPtr hMenu, uint uFlags, int x,int y, IntPtr hWnd, IntPtr ignore );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int GetCursorPos( ref POINT pnt );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int GetCaretPos( ref POINT pnt );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int ValidateRect( IntPtr hWnd, ref RECT rc );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern IntPtr FindWindowEx( IntPtr hWnd, IntPtr hChild, string strClassName, string strName );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern IntPtr FindWindowEx( IntPtr hWnd, IntPtr hChild, string strClassName, IntPtr strName );
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int GetSysColor(int color);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScrollWindow(IntPtr hWnd, int xAmount, int yAmount, ref RECT rectScrollRegion, ref RECT rectClip);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, IntPtr rectClip, IntPtr hrgnUpdate, IntPtr prcUpdate, int flags);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetForegroundWindow();
 
		[DllImport("user32.dll", ExactSpelling=true)]
		public static extern IntPtr GetProcessWindowStation();
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool LockWindowUpdate(IntPtr hWnd);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In, Out] ref RECT lParam);

		[DllImport("user32.dll", EntryPoint="WindowFromPoint", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr _WindowFromPoint(POINTSTRUCT pt);

		public static IntPtr WindowFromPoint(int x, int y)
		{
			POINTSTRUCT pointstruct1 = new POINTSTRUCT(x, y);
			return _WindowFromPoint(pointstruct1);
		}


		public static IntPtr CreateMenu()
		{
			return HandleCollector.Add(IntCreateMenu(), CommonHandles.Menu);
		}

		[DllImport("user32.dll", EntryPoint="CreateMenu", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntCreateMenu();



		#endregion

		#region USER32 Static Properties
		//		private static int wmMouseEnterMessage;
		//
		public static int WM_MOUSEENTER
		{
			get
			{
				//if (wmMouseEnterMessage == -1)
				//{
				//	wmMouseEnterMessage = RegisterWindowMessage("WinFormsMouseEnter");
				//}
				//return wmMouseEnterMessage;
				return RegisterWindowMessage("WinFormsMouseEnter");
			}
		}

		#endregion

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref Win32.RECT rectScrollRegion, ref Win32.RECT rectClip);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
		public static extern int SetScrollPos(HandleRef hWnd, int nBar, int nPos, bool bRedraw);
	
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);
 

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int SetScrollInfo(HandleRef hWnd, int fnBar, SCROLLINFO si, bool redraw);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool PeekMessage([In, Out] ref Win32.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);
 
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);
	
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool DrawMenuBar(HandleRef hWnd);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }
        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);


		#region GDI static extern Methods

		/// <summary>
		/// CombineRgn
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int CombineRgn(IntPtr dest, IntPtr src1, IntPtr src2, int flags);

		/// <summary>
		/// CreateRectRgnIndirect
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateRectRgnIndirect(ref Rect rect); 

		/// <summary>
		/// GetClipBox
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetClipBox(IntPtr hDC, ref Rect rectBox); 

		/// <summary>
		/// SelectClipRgn
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn); 

		/// <summary>
		/// CreateBrushIndirect
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateBrushIndirect(ref LogBrush brush); 

		/// <summary>
		/// PatBlt
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool PatBlt(IntPtr hDC, int x, int y, int width, int height, int flags); 

		/// <summary>
		/// DeleteObject
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", EntryPoint="DeleteObject", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern bool IntDeleteObject(HandleRef hObject);
 
		public static bool DeleteObject(HandleRef hObject)
		{
			HandleCollector.Remove((IntPtr) hObject, Win32.CommonHandles.GDI);
			return IntDeleteObject(hObject);
		}



		/// <summary>
		/// DeleteDC
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool DeleteDC(IntPtr hDC);

		/// <summary>
		/// SelectObject
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		/// <summary>
		/// CreateCompatibleDC
		/// </summary>
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateRectRgnIndirect(ref Win32.RECT rect); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int GetClipBox(IntPtr hDC, ref Win32.RECT rectBox); 
  
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool PatBlt(IntPtr hDC, int x, int y, int width, int height, uint flags); 

		[DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
		public static extern IntPtr SelectPalette(HandleRef hdc, HandleRef hpal, int bForceBackground);
 
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
	
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int SetBkColor(HandleRef hDC, int clr);
		
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int SetTextColor(HandleRef hDC, int crColor);
 
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
		public static extern int RealizePalette(HandleRef hDC);
 
		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetStockObject(int nIndex);
 
		[DllImport("gdi32.dll", EntryPoint="CreateSolidBrush", CharSet=CharSet.Auto, ExactSpelling=true)]
		private static extern IntPtr IntCreateSolidBrush(int crColor);
 
		[DllImport("gdi32.dll")]
		static public extern bool StretchBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
			IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, int WidthScr, int HeightScr, uint Rop);

		[DllImport("gdi32.dll")]
		static public extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
	
		[DllImport("gdi32.dll")]
		static public extern bool BitBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
			IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, uint Rop);
	
		[DllImport("gdi32.dll")]
		static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
	
		[DllImport("gdi32.dll")]
		static public extern int SetMapMode(IntPtr hDC, int fnMapMode);
	
		[DllImport("gdi32.dll")]
		static public extern int GetObjectType(IntPtr handle);
	
		[DllImport("gdi32")]
		public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_FLAT bmi, 
			int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);
	
		[DllImport("gdi32")]
		public static extern int GetDIBits(IntPtr hDC, IntPtr hbm, int StartScan, int ScanLines, int lpBits, BITMAPINFOHEADER bmi, int usage);
	
		[DllImport("gdi32")]
		public static extern int GetDIBits(IntPtr hdc, IntPtr hbm, int StartScan, int ScanLines, int lpBits, ref BITMAPINFO_FLAT bmi, int usage);
	
		[DllImport("gdi32")]
		public static extern IntPtr GetPaletteEntries(IntPtr hpal, int iStartIndex, int nEntries, byte[] lppe);
	
		[DllImport("gdi32")]
		public static extern IntPtr GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);
	
		[DllImport("gdi32")]
		public static extern uint SetDCBrushColor(IntPtr hdc,  uint crColor);
	
		[DllImport("gdi32")]
		public static extern IntPtr CreateSolidBrush(uint crColor);
	
		[DllImport("gdi32")]
		public static extern int SetBkMode(IntPtr hDC, BackgroundMode mode);

		[DllImport("gdi32")]
		public static extern int SetViewportOrgEx(IntPtr hdc,  int x, int y,  int param);
	
		[DllImport("gdi32")]
		public static extern uint SetTextColor(IntPtr hDC, uint colorRef);
	
		[DllImport("gdi32")]
		public static extern int SetStretchBltMode(IntPtr hDC, int StrechMode);


		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr CreatePen(int nStyle, int nWidth, int crColor);
		//		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		//		public static extern IntPtr CreateSolidBrush(int crColor);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool LineTo(IntPtr hdc, int x, int y);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr pt);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SetBkColor(IntPtr hDC, int clr);
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern int SetROP2(IntPtr hDC, int nDrawMode);
	
		#endregion

		#region GDI Static Methods

		public static IntPtr CreateSolidBrush(int crColor)
		{
			return HandleCollector.Add(IntCreateSolidBrush(crColor), CommonHandles.GDI);
		}


		public static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
		{
			IntPtr ptr1 = System.Drawing.Graphics.GetHalftonePalette();
			IntPtr ptr2 = SelectPalette(new System.Runtime.InteropServices.HandleRef(null, dc), new System.Runtime.InteropServices.HandleRef(null, ptr1), force ? 0 : 1);
			if ((ptr2 != IntPtr.Zero) && realizePalette)
			{
				RealizePalette(new System.Runtime.InteropServices.HandleRef(null, dc));
			}
			return ptr2;
		}
		#endregion

		#region Class Structs

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
			public RECT rcItem;
			public IntPtr itemData;
			public DRAWITEMSTRUCT(){}
		}
		#endregion

		#region Constans values
		public const string TOOLBARCLASSNAME = "ToolbarWindow32";
		public const string REBARCLASSNAME = "ReBarWindow32";
		public const string PROGRESSBARCLASSNAME = "msctls_progress32";
		public const string SCROLLBAR = "SCROLLBAR";
		#endregion

		#region Kernel32.dll functions
		[DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern int GetCurrentThreadId();

		[DllImport( "kernel32.dll", CharSet=CharSet.Auto )]
		public extern static int GetShortPathName(string lpszLongPath, 
			StringBuilder lpszShortPath, int cchBuffer);

		[DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern int GetLastError();
		
		[DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern void SetLastError( int error );

		[DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
		internal static extern int LCMapStringA(int Locale, int dwMapFlags, [MarshalAs(UnmanagedType.LPArray)] byte[] lpSrcStr, int cchSrc, [MarshalAs(UnmanagedType.LPArray)] byte[] lpDestStr, int cchDest);
 
		[DllImport("kernel32", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern int LCMapString(int Locale, int dwMapFlags, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSrcStr, int cchSrc, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDestStr, int cchDest);

		#endregion
	
		#region Uxtheme.dll functions

		[DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
		public static extern int GetCurrentThemeName(StringBuilder 
			pszThemeFileName, int dwMaxNameChars, 
			StringBuilder pszColorBuff, int cchMaxColorChars, 
			StringBuilder pszSizeBuff, int cchMaxSizeChars);

		[DllImport("UxTheme.dll")]
		public static extern bool IsAppThemed();

		[DllImport("uxtheme.dll")]
		static public extern int SetWindowTheme(IntPtr hWnd, string AppID, string ClassID);
	
		#endregion
	
		#region advapi32.dll Methods
		
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		public static extern bool ConvertStringSidToSidW(string stringSid, ref IntPtr sId);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		public static extern bool LookupAccountSidW(string lpSystemName,
			IntPtr sId,
			StringBuilder Name,
			ref long cbName,
			StringBuilder domainName,
			ref long cbDomainName,
			ref int psUse);


		public string GetName(string sId)
		{
			const int size = 255; 
			string domainName = String.Empty;
			string userName = String.Empty;
			long cbUserName = size;
			long cbDomainName = size;
			IntPtr ptrSid = new IntPtr(0);
			int psUse = 0;
			StringBuilder bufName = new StringBuilder(size);
			StringBuilder bufDomain = new StringBuilder(size);

			if (ConvertStringSidToSidW(sId, ref ptrSid))
			{
				if (LookupAccountSidW(String.Empty, 
					ptrSid, bufName, 
					ref cbUserName, bufDomain, 
					ref cbDomainName, ref psUse))
				{
					userName = bufName.ToString();
					domainName = bufDomain.ToString();
					return String.Format(System.Globalization.CultureInfo.CurrentCulture, @"{0}\{1}", domainName, userName);
				}
				else return String.Empty;
			}
			else return String.Empty;
		}

		#endregion

		#region comctl32.dll functions
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControls();
		[DllImport("comctl32.dll", EntryPoint="DllGetVersion")]
		public extern static int GetCommonControlDLLVersion(ref DLLVERSIONINFO dvi);
		[DllImport("comctl32.dll")]
		public static extern IntPtr ImageList_Create(int width, int height, uint flags, int count, int grow);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Destroy(IntPtr handle);
		[DllImport("comctl32.dll")]
		public static extern int ImageList_Add(IntPtr imageHandle, IntPtr hBitmap, IntPtr hMask);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Remove(IntPtr imageHandle, int index);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_BeginDrag(IntPtr imageHandle, int imageIndex, int xHotSpot, int yHotSpot);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_DragEnter(IntPtr hWndLock, int x, int y);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_DragMove(int x, int y);
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_DragLeave(IntPtr hWndLock);
		[DllImport("comctl32.dll")]
		public static extern void ImageList_EndDrag();
		#endregion

		#region Win32 Macro-Like
		public static int GET_X_LPARAM(int lParam)
		{
			return (lParam & 0xffff);
		}
	 

		public static int GET_Y_LPARAM(int lParam)
		{
			return (lParam >> 16);
		}

		public static Point GetPointFromLPARAM(int lParam)
		{
			return new Point(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
		}

		public static int LOW_ORDER(int param)
		{
			return (param & 0xffff);
		}

		public static int HIGH_ORDER(int param)
		{
			return (param >> 16);
		}

		#endregion

        #region Util

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int lstrlen(string s);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //internal static extern int RegisterWindowMessage(string msg);


        public static int MAKELONG(int low, int high)
        {
            return ((high << 0x10) | (low & 0xffff));
        }

        public static IntPtr MAKELPARAM(int low, int high)
        {
            return (IntPtr)((high << 0x10) | (low & 0xffff));
        }

        private static int GetEmbededNullStringLengthAnsi(string s)
        {
            int num1 = s.IndexOf('\0');
            if (num1 > -1)
            {
                string text1 = s.Substring(0, num1);
                string text2 = s.Substring(num1 + 1);
                return ((GetPInvokeStringLength(text1) + GetEmbededNullStringLengthAnsi(text2)) + 1);
            }
            return GetPInvokeStringLength(s);
        }

        public static int GetPInvokeStringLength(string s)
        {
            if (s == null)
            {
                return 0;
            }
            if (Marshal.SystemDefaultCharSize == 2)
            {
                return s.Length;
            }
            if (s.Length == 0)
            {
                return 0;
            }
            if (s.IndexOf('\0') > -1)
            {
                return GetEmbededNullStringLengthAnsi(s);
            }
            return lstrlen(s);
        }


        public static int HIWORD(int n)
        {
            return ((n >> 0x10) & 0xffff);
        }


        public static int HIWORD(IntPtr n)
        {
            return HIWORD((int)n);
        }

        public static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        public static int LOWORD(IntPtr n)
        {
            return LOWORD((int)n);
        }

        public static int SignedHIWORD(int n)
        {
            return (short)((n >> 0x10) & 0xffff);
        }


        public static int SignedHIWORD(IntPtr n)
        {
            return SignedHIWORD((int)n);
        }

        public static int SignedLOWORD(int n)
        {
            return (short)(n & 0xffff);
        }

        public static int SignedLOWORD(IntPtr n)
        {
            return SignedLOWORD((int)n);
        }

        #endregion

        #region UUID

        public static Guid CreateGuid()
        {
            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result == (int)RetUuidCodes.RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid value);

        [Flags]
        public enum RetUuidCodes : int
        {
            RPC_S_OK = 0, //The call succeeded.
            RPC_S_UUID_LOCAL_ONLY = 1824, //The UUID is guaranteed to be unique to this computer only.
            RPC_S_UUID_NO_ADDRESS = 1739 //Cannot get Ethernet or token-ring hardware address for this computer.
        }

        /// <summary>
        /// This function converts a string generated by the StringFromCLSID function back into the original class identifier.
        /// </summary>
        /// <param name="sz">String that represents the class identifier</param>
        /// <param name="clsid">On return will contain the class identifier</param>
        /// <returns>
        /// Positive or zero if class identifier was obtained successfully
        /// Negative if the call failed
        /// </returns>
        [DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true)]
        public static extern int CLSIDFromString(string sz, out Guid clsid);


        #endregion
    }

}
