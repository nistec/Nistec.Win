using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing; 
using MControl.Win32;

namespace MControl.Win32
{
	/// <summary>
	/// A NativeMethods Support 
	/// </summary>
	public sealed class NativeMethods
	{

		#region DateTime

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.SYSTEMTIME lParam);


		[StructLayout(LayoutKind.Sequential)]
			public struct NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}
 

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class NMDATETIMECHANGE
		{
			public NativeMethods.NMHDR nmhdr;
			public int dwFlags;
			public NativeMethods.SYSTEMTIME st;
			public NMDATETIMECHANGE(){}
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class NMDATETIMEFORMAT
		{
			public NativeMethods.NMHDR nmhdr;
			public string pszFormat;
			public NativeMethods.SYSTEMTIME st;
			public string pszDisplay;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
			public string szDisplay;
			public NMDATETIMEFORMAT(){}
		}

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

		internal static DateTime SysTimeToDateTime(NativeMethods.SYSTEMTIME s)
		{
			return new DateTime(s.wYear, s.wMonth, s.wDay, s.wHour, s.wMinute, s.wSecond);
		}


		public static NativeMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
		{
			NativeMethods.SYSTEMTIME systemtime1 = new NativeMethods.SYSTEMTIME();
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
		#endregion

		#region Drawing

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In, Out] NativeMethods.POINT pt, int cPoints);
	
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
