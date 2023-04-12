using System.Windows.Forms;
using System.Drawing;
using System;
using System.IO;
using System.Reflection;


using Nistec.Win32;
using System.Runtime.InteropServices;
using Nistec.Win;

namespace Nistec.GridView 
{
 
    /// <summary>
    /// Native Methods
    /// </summary>
	internal sealed class NativeMethods 
	{

		
		#region win32
//		public sealed class CommonHandles
//		{
//			// Methods
//			static CommonHandles()
//			{
//				NativeMethods.CommonHandles.Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
//				NativeMethods.CommonHandles.Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
//				NativeMethods.CommonHandles.EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
//				NativeMethods.CommonHandles.Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
//				NativeMethods.CommonHandles.GDI = HandleCollector.RegisterType("GDI", 90, 50);
//				NativeMethods.CommonHandles.HDC = HandleCollector.RegisterType("HDC", 100, 2);
//				NativeMethods.CommonHandles.Icon = HandleCollector.RegisterType("Icon", 20, 500);
//				NativeMethods.CommonHandles.Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
//				NativeMethods.CommonHandles.Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
//				NativeMethods.CommonHandles.Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
//			}
//
// 
//			public CommonHandles(){}
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
//
//		public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
//		{
//			return HandleCollector.Add(NativeMethods.IntGetDCEx(hWnd, hrgnClip, flags), NativeMethods.CommonHandles.HDC);
//		}
//		[DllImport("user32.dll", EntryPoint="GetDCEx", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);
//
//
//		internal static IntPtr CreateHalftoneHBRUSH()
//		{
//			short[] numArray1 = new short[8];
//			for (int num1 = 0; num1 < 8; num1++)
//			{
//				numArray1[num1] = (short) (0x5555 << ((num1 & 1) & 0x1f));
//			}
//			IntPtr ptr1 = NativeMethods.CreateBitmap(8, 8, 1, 1, numArray1);
//			NativeMethods.LOGBRUSH logbrush1 = new NativeMethods.LOGBRUSH();
//			logbrush1.lbColor = NativeMethods.ToWin32(Color.Black);
//			logbrush1.lbStyle = 3;
//			logbrush1.lbHatch = ptr1;
//			IntPtr ptr2 = NativeMethods.CreateBrushIndirect(logbrush1);
//			NativeMethods.DeleteObject(new HandleRef(null, ptr1));
//			return ptr2;
//		}
//
// 
//		public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
//		{
//			return HandleCollector.Add(NativeMethods.IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits),   NativeMethods.CommonHandles.GDI);
//		}
//
//		[DllImport("gdi32.dll", EntryPoint="CreateBitmap", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern IntPtr IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);
//
//		[StructLayout(LayoutKind.Sequential)]
//			public class LOGBRUSH
//		{
//			public int lbStyle;
//			public int lbColor;
//			public IntPtr lbHatch;
//			public LOGBRUSH(){}
//		}
// 
//		public static int ToWin32(Color c)
//		{
//			return ((c.R | (c.G << 8)) | (c.B << 0x10));
//		}
//
// 
//		public static IntPtr CreateBrushIndirect(NativeMethods.LOGBRUSH lb)
//		{
//			return HandleCollector.Add(NativeMethods.IntCreateBrushIndirect(lb),  NativeMethods.CommonHandles.GDI);
//		}
//
// 
//		[DllImport("gdi32.dll", EntryPoint="CreateBrushIndirect", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern IntPtr IntCreateBrushIndirect(NativeMethods.LOGBRUSH lb);
// 
//		public static bool DeleteObject(HandleRef hObject)
//		{
//			HandleCollector.Remove((IntPtr) hObject,  NativeMethods.CommonHandles.GDI);
//			return NativeMethods.IntDeleteObject(hObject);
//		}
//
//		[DllImport("gdi32.dll", EntryPoint="DeleteObject", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern bool IntDeleteObject(HandleRef hObject);
// 
//
//		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
//		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
// 
//
//		[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
//		public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);
// 
//		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
//		{
//			HandleCollector.Remove((IntPtr) hDC,  NativeMethods.CommonHandles.HDC);
//			return NativeMethods.IntReleaseDC(hWnd, hDC);
//		}
//
//		[DllImport("user32.dll", EntryPoint="ReleaseDC", CharSet=CharSet.Auto, ExactSpelling=true)]
//		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);
// 
//		[DllImport("user32.dll", CharSet=CharSet.Auto)]
//		public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wparam, NativeMethods.TV_HITTESTINFO lparam);
//
//		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1), ComVisible(false)]
//			public class TV_HITTESTINFO
//		{
//			public int pt_x;
//			public int pt_y;
//			public int flags;
//			public int hItem;
//			public TV_HITTESTINFO(){}
//		}
// 

#endregion

		public static Image LoadImage(string imageName)
		{			
		
			
			Stream strm = Type.GetType("Nistec.GridView.NativeMethods").Assembly.GetManifestResourceStream(imageName);
 
			Image im = null;
			if(strm != null)
			{
				im = new System.Drawing.Bitmap(strm);
				strm.Close();
			}

			return im;
		}

		public static void ErrMsg(string msg )
		{
			MsgBox.ShowError(msg);
		}

		public static System.Windows.Forms.HorizontalAlignment GetAlignment(System.Drawing.ContentAlignment align)
		{
			switch(align)
			{
				case ContentAlignment.BottomCenter :
				case ContentAlignment.MiddleCenter :
				case ContentAlignment.TopCenter :
					return HorizontalAlignment.Center ;
				case ContentAlignment.BottomLeft :
				case ContentAlignment.MiddleLeft :
				case ContentAlignment.TopLeft :
					return HorizontalAlignment.Left  ;
				case ContentAlignment.BottomRight :
				case ContentAlignment.MiddleRight :
				case ContentAlignment.TopRight :
					return HorizontalAlignment.Right  ;
			}
			return HorizontalAlignment.Left  ;
		}

		public static System.Drawing.ContentAlignment GetAlignment(System.Windows.Forms.HorizontalAlignment align)
		{
			switch(align)
			{
				case HorizontalAlignment.Center  :
					return ContentAlignment.MiddleCenter  ;
				case HorizontalAlignment.Left  :
					return ContentAlignment.MiddleLeft   ;
				case HorizontalAlignment.Right  :
					return ContentAlignment.MiddleRight   ;
			}
			return ContentAlignment.MiddleLeft  ;
		}

        public static GridColumnType DataTypeToColumnType(FieldType dataType)
        {
            switch (dataType)
            {
                case FieldType.Bool:
                    return GridColumnType.BoolColumn;
                case FieldType.Date:
                    return GridColumnType.DateTimeColumn;
                case FieldType.Number:
                    return GridColumnType.NumericColumn;
                default:
                    return GridColumnType.TextColumn;
            }

        }

        public static FieldType ColumntTypeToDataType(GridColumnType columnType)
        {
            switch (columnType)
            {
                case GridColumnType.BoolColumn:
                    return FieldType.Bool;
                case GridColumnType.DateTimeColumn:
                    return FieldType.Date;
                case GridColumnType.NumericColumn:
                    return FieldType.Number;
                default:
                    return FieldType.Text;
            }
        }

		#region Data


//			public DataTable GetTableSchema(string strSQL,string TableName)
//			{
//				SqlDataAdapter da = new SqlDataAdapter (strSQL,m_cnn);
//				DataSet ds = new DataSet ();
//				da.FillSchema  (ds,System.Data.SchemaType.Source , TableName);
//				return ds.Tables [TableName];
//			}
//
//			public DataTable GetPropertySchema(string strSQL)
//			{
//				SqlCommand cmd = new SqlCommand(strSQL,m_cnn);
//				SqlDataReader myReader = cmd.ExecuteReader();
//				return myReader.GetSchemaTable();
//			}  


		#endregion



	}
}