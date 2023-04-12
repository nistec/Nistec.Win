using System;
using System.Diagnostics;
using System.ComponentModel;

namespace Nistec.WinForms
{

	[TypeConverter(typeof(ExplorerConverter))]
	public sealed class Explorer//:Component
	{
		#region Members

	    private System.Diagnostics.Process m_Process;
		private ProcessStartInfo startInfo;
		string m_FavoritesPath =""; 
		#endregion

		#region Constructor
		public Explorer()
		{
			startInfo =new ProcessStartInfo("IExplore.exe");
	
			// Get the path that stores favorite links.
			m_FavoritesPath = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
		}

		#endregion

		#region Dispose

//		protected override void Dispose( bool disposing )
//		{
//			//if( disposing )
//			//{
//			//if(components != null)
//			//{
//			//	components.Dispose();
//			//}
//			//}
//			base.Dispose( disposing );
//		}

		#endregion

		#region Properties

		[ReadOnly(true)]
		public System.Diagnostics.Process SysProcess
		{
			get{return m_Process;}
		}

		public ProcessStartInfo ProcessInfo
		{
			get{return startInfo;}
		}

		public string FavoritesPath
		{
			get{return m_FavoritesPath;}
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Opens the Internet Explorer application.
		/// </summary>
		public void OpenExploreWithFavorites()
		{
			// Start Internet Explorer. Defaults to the home page.
			Process.Start("IExplore.exe");
  			// Display the contents of the favorites folder in the browser.
			Process.Start(m_FavoritesPath);
 
		}
        
		/// <summary>
		/// Uses the ProcessStartInfo class to start new processes.
		/// </summary>
		public void OpenExplorer(string path)
		{
			startInfo.Arguments = path;
			m_Process=Process.Start(startInfo);
		}

		/// <summary>
		/// Uses the ProcessStartInfo class to start new processes, both in a minimized mode.
		/// </summary>
		public void OpenExplorer(ProcessWindowStyle windowStyle,string path)
		{
 			//ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
			startInfo.WindowStyle = windowStyle;
     		startInfo.Arguments = path;
    		m_Process=Process.Start(startInfo);
  		}
		#endregion

		#region Static Methods

		/// <summary>
		/// Opens urls and .html documents using Internet Explorer.
		/// </summary>
		public static System.Diagnostics.Process OpenUrl(string url)
		{
			// url's are not considered documents. They can only be opened
			// by passing them as arguments.
			return Process.Start("IExplore.exe", url);
		}

		#endregion
	}

	#region ExplorerConverter
	/// <summary>
	/// Summary description for ListBoxFinderConverter.
	/// </summary>
	public class ExplorerConverter : TypeConverter
	{
		public ExplorerConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// allows us to display the + symbol near the property name
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Explorer));
		}

	}
	#endregion

}


