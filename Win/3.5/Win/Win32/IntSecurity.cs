using System;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics;
using System.IO;

namespace MControl.Win32
{
	/// <summary>
	/// Summary description for IntSecurity.
	/// </summary>
	public class IntSecurity
	{


		#region members
		// Fields
		private static CodeAccessPermission adjustCursorClip;
//		private static CodeAccessPermission affectThreadBehavior;
//		private static CodeAccessPermission allPrinting;
//		private static PermissionSet allPrintingAndUnmanagedCode;
		private static CodeAccessPermission allWindows;
//		private static CodeAccessPermission changeWindowRegionForTopLevel;
//		private static CodeAccessPermission clipboardRead;
//		private static CodeAccessPermission controlFromHandleOrLocation;
//		private static CodeAccessPermission createAnyWindow;
//		private static CodeAccessPermission createGraphicsForControl;
//		private static CodeAccessPermission defaultPrinting;
//		private static CodeAccessPermission fileDialogCustomization;
//		private static CodeAccessPermission fileDialogOpenFile;
//		private static CodeAccessPermission fileDialogSaveFile;
//		private static CodeAccessPermission getCapture;
//		private static CodeAccessPermission getParent;
//		private static CodeAccessPermission manipulateWndProcAndHandles;
//		private static CodeAccessPermission minimizeWindowProgramatically;
//		private static CodeAccessPermission modifyCursor;
		private static CodeAccessPermission modifyFocus;
//		private static CodeAccessPermission noPrinting;
//		private static CodeAccessPermission objectFromWin32Handle;
//		private static CodeAccessPermission safePrinting;
//		private static CodeAccessPermission safeSubWindows;
//		private static CodeAccessPermission safeTopLevelWindows;
//		private static CodeAccessPermission screenDC;
		public static readonly TraceSwitch SecurityDemand;
//		private static CodeAccessPermission sendMessages;
//		private static CodeAccessPermission sensitiveSystemInformation;
//		private static CodeAccessPermission topLevelWindow;
//		private static CodeAccessPermission topMostWindow;
//		private static CodeAccessPermission transparentWindows;
		private static CodeAccessPermission unmanagedCode;
//		private static CodeAccessPermission unrestrictedEnvironment;
//		private static CodeAccessPermission unrestrictedWindows;
//		private static CodeAccessPermission win32HandleManipulation;
//		private static CodeAccessPermission windowAdornmentModification;

		#endregion

		#region Methods
		static IntSecurity()
		{
			IntSecurity.SecurityDemand = new TraceSwitch("SecurityDemand", "Trace when security demands occur.");
		}

		public IntSecurity()
		{
		}

		internal static void DemandFileIO(FileIOPermissionAccess access, string fileName)
		{
			new FileIOPermission(access, IntSecurity.UnsafeGetFullPath(fileName)).Demand();
		}

		internal static string UnsafeGetFullPath(string fileName)
		{
			string text1 = fileName;
			FileIOPermission permission1 = new FileIOPermission(PermissionState.None);
			permission1.AllFiles = FileIOPermissionAccess.PathDiscovery;
			permission1.Assert();
			try
			{
				text1 = Path.GetFullPath(fileName);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return text1;
		}

		public static CodeAccessPermission UnmanagedCode
		{
			get
			{
				if (IntSecurity.unmanagedCode == null)
				{
					IntSecurity.unmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				}
				return IntSecurity.unmanagedCode;
			}
		}

		#endregion

		#region Properties

		public static CodeAccessPermission ModifyFocus
		{
			get
			{
				if (IntSecurity.modifyFocus == null)
				{
					IntSecurity.modifyFocus = IntSecurity.AllWindows;
				}
				return IntSecurity.modifyFocus;
			}
		}
 
		public static CodeAccessPermission AllWindows
		{
			get
			{
				if (IntSecurity.allWindows == null)
				{
					IntSecurity.allWindows = new UIPermission(UIPermissionWindow.AllWindows);
				}
				return IntSecurity.allWindows;
			}
		}
		public static CodeAccessPermission AdjustCursorClip
		{
			get
			{
				if (IntSecurity.adjustCursorClip == null)
				{
					IntSecurity.adjustCursorClip = IntSecurity.AllWindows;
				}
				return IntSecurity.adjustCursorClip;
			}
		}



		#endregion
	}
}
