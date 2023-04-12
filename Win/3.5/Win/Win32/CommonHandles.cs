using System;

namespace MControl.Win32
{
	public sealed class CommonHandles
	{
		// Methods
		static CommonHandles()
		{
			CommonHandles.Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
			CommonHandles.Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
			CommonHandles.EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
			CommonHandles.Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
			CommonHandles.GDI = HandleCollector.RegisterType("GDI", 90, 50);
			CommonHandles.HDC = HandleCollector.RegisterType("HDC", 100, 2);
			CommonHandles.Icon = HandleCollector.RegisterType("Icon", 20, 500);
			CommonHandles.Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
			CommonHandles.Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
			CommonHandles.Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
		}

 

		public CommonHandles()
		{
		}


		// Fields
		public static readonly int Accelerator;
		public static readonly int Cursor;
		public static readonly int EMF;
		public static readonly int Find;
		public static readonly int GDI;
		public static readonly int HDC;
		public static readonly int Icon;
		public static readonly int Kernel;
		public static readonly int Menu;
		public static readonly int Window;
	}
 

}
