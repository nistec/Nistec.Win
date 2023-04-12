using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Runtime.InteropServices;

using Nistec.Win32;

namespace Nistec.GridView
{

	internal class GridToolTip : MarshalByRefObject
	{

		// Methods
		public GridToolTip(Grid dataGrid)
		{
			this.tipWindow = null;
			this.dataGrid = null;
			this.dataGrid = dataGrid;
		}

 
		public void AddToolTip(string toolTipString, IntPtr toolTipId, Rectangle iconBounds)
		{
			if (toolTipString == null)
			{
				throw new ArgumentNullException("GridToolTipNull");
			}
			if (iconBounds.IsEmpty)
			{
				throw new ArgumentNullException("GridToolTipEmptyIcon");
			}
			WinMethods.TOOLINFO_T toolinfo_t1 = new WinMethods.TOOLINFO_T();
			toolinfo_t1.cbSize = Marshal.SizeOf(toolinfo_t1);
			toolinfo_t1.hwnd = this.dataGrid.Handle;
			toolinfo_t1.uId = toolTipId;
			toolinfo_t1.lpszText = toolTipString;
			toolinfo_t1.rect = WinMethods.RECT.FromXYWH(iconBounds.X, iconBounds.Y, iconBounds.Width, iconBounds.Height);
			toolinfo_t1.uFlags = 0x10;
			WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.TTM_ADDTOOL, 0, toolinfo_t1);
		}

		public void CreateToolTipHandle()
		{
			if ((this.tipWindow == null) || (this.tipWindow.Handle == IntPtr.Zero))
			{
				WinMethods.INITCOMMONCONTROLSEX initcommoncontrolsex1 = new WinMethods.INITCOMMONCONTROLSEX();
				initcommoncontrolsex1.dwICC = 8;
				initcommoncontrolsex1.dwSize = Marshal.SizeOf(initcommoncontrolsex1);
				WinMethods.InitCommonControlsEx(initcommoncontrolsex1);
				CreateParams params1 = new CreateParams();
				params1.Parent = this.dataGrid.Handle;
				params1.ClassName = "tooltips_class32";
				params1.Style = 1;
				this.tipWindow = new NativeWindow();
				this.tipWindow.CreateHandle(params1);
				WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x418, 0, SystemInformation.MaxWindowTrackSize.Width);
				WinMethods.SetWindowPos(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.HWND_NOTOPMOST, 0, 0, 0, 0, 0x13);
				WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x403, 3, 0);
			}
		}

		public void DeactivateToolTip()
		{
			WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x401, 0, 0);
		}

		public void Destroy()
		{
            if (tipWindow != null)
            {
                this.tipWindow.DestroyHandle();
                this.tipWindow = null;
            }
		}

		public void RemoveToolTip(IntPtr toolTipId)
		{
			WinMethods.TOOLINFO_T toolinfo_t1 = new WinMethods.TOOLINFO_T();
			toolinfo_t1.cbSize = Marshal.SizeOf(toolinfo_t1);
			toolinfo_t1.hwnd = this.dataGrid.Handle;
			toolinfo_t1.uId = toolTipId;
			WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.TTM_DELTOOL, 0, toolinfo_t1);
		}


		// Fields
		private Grid dataGrid;
		private NativeWindow tipWindow;
	}
 
}
