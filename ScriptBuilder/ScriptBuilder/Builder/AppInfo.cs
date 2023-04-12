using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Nistec.ScriptBuilder
{


	//	application specific usage info
	//	/?	-	Display help info.
	//	/e	-	Compiles script into console application executable.
	//	/ew	-	Compiles script into Windows application executable.
	//	/c	-	Use compiled file (.csc) if found (to improve performance).
	//	/ca	-	Compiles script file into assembly (.csc).
	//	/cd	-	Compiles script file into assembly (.dll).
	//	/dbg	-Include debug information.
	//	/s	-	Prints content of sample script file.
	//	Example: "+AppInfo.appName+" /s > sample.cs)\n");

	/// <summary>
	/// Repository for application specific data
	/// </summary>
	public class AppInfo
	{

		//public delegate void PrintDelegate(string msg);
		public static string appName;// = "ScriptEngine";
		public static bool appConsole;// = false;
		public static string appParams;// = "[/nl]:";
		public static string appParamsHelp;// = "nl	-	No logo mode: No banner will be shown at execution time.\n";
        public static string ScriptLib;
        public static string DllLib;

        public static string MainMethod //"Execute";//"Main";
        {
            get { return Config.Instance.GetValue("MainMethod", "Main"); }
        }
        public static string ClassName
        {
            get { return Config.Instance.GetValue("ClassName", "AppScript"); }
        }


		static AppInfo()
		{
			AppInfo.appName = "ScriptEngine";
			AppInfo.appConsole = false;
			AppInfo.appParams = "[/nl]:";
			AppInfo.appParamsHelp = "nl	-	No logo mode: No banner will be shown at execution time.\n";
			AppInfo.ScriptLib=Config.Instance["ScriptLibrary"]+"";//"ScriptLibrary\\App\\";
            AppInfo.DllLib = Config.Instance["AppDllPath"]+"";//"ScriptLibrary\\Dll\\";
		}

		
		public static string appTitle
		{
			get { return "Script engine. Version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()+".\n";}
		}

		public static string GetScriptFile(string name)
		{
            return Path.Combine(ScriptLib, name.Replace(".csc", "") + ".csc");
		}

		public static string GetDllFile(string name)
		{
            return Path.Combine(DllLib, name.Replace(".dll", "") + ".dll");
		}

        public static string GetAppFileName(string appName, string RunningType)
        {
            return appName + "." + RunningType;
        }

        public static string GetAppPath(string appName, string RunningType)
        {
            if (RunningType.Equals("csc"))
                return Path.Combine(ScriptLib  ,  appName + "." + RunningType);
            else
                return Path.Combine(ScriptLib  ,  appName + "." + RunningType);
        }

        public static string GetAppTempPath(string appName, string RunningType)
        {
            return Path.Combine(ScriptLib  ,  appName + "." + RunningType);
        }

		public static string  GetHelp()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		
			sb.Append("Script engine info. \r\n");
			sb.Append("\r\n");
	
			//sb.Append(" /?	-	Display help info.\n");
			sb.Append(" /e	 -	Compile script for console application .\r\n");
			sb.Append(" /ew	 -	Compile script for Windows application .\r\n");
			sb.Append(" /c	 -	Use compiled file (.csc) to improve performance.\r\n");
			sb.Append(" /ca	 -	Compile script file into assembly (.csc).\r\n");
			sb.Append(" /cd	 -	Compile script file into assembly (.dll).\r\n");
			sb.Append(" /dbg -  Include debug information.\r\n");
			sb.Append("\r\n");
	
			//sb.Append("	-	    To include your own dll (explicitly referenced assembly). \r\n");
			//sb.Append("		    (Example: appName /r:myLib.dll myScript.cs).\r\n");
			sb.Append("\r\n");
			sb.Append("file	  -	Specifies name of a script file to be run.\r\n");
			sb.Append("\r\n");
			sb.Append("params -	Specifies optional parameters for a script file to be run.\r\n");
			sb.Append("\r\n");
	
			return sb.ToString ();
		}


        //public static string GetAppScriptPath()
        //{
        //    return GetRegistryValue("AppScriptPath","");
        //}

        //public static string GetAppDllPath()
        //{
        //    return GetRegistryValue("AppDllPath","");
        //}

        //public static string GetAppTempPath()
        //{
        //    return GetRegistryValue("AppTempPath","");
        //}

        //public static string GetRegistryValue(string key,string defaultValue)
        //{
        //    Microsoft.Win32.RegistryKey rg=Microsoft.Win32.Registry.LocalMachine;
        //    rg=rg.OpenSubKey(@"Software\Nistec\ScriptBuilder");
        //    return rg.GetValue(key,defaultValue).ToString();
        //}


	}
}
