using System;

namespace ScriptAppTester
{

	public enum ScriptCompileOption
	{
		/// <summary>
		/// Compiles script into console application executable.
		/// </summary>
		Console,
		/// <summary>
		/// Compiles script into Windows application executable.
		/// </summary>
		WinExe,	
		/// <summary>
		/// Use compiled file (.csc) if found (to improve performance).
		/// </summary>
		Compiled,
		/// <summary>
		/// Compiles script file into assembly (.csc).
		/// </summary>
		Csc,
		/// <summary>
		/// Compiles script file into assembly (.dll).
		/// </summary>
		Dll,
		/// <summary>
		/// Forces compiler to include debug information.
		/// </summary>
		Debug,
		/// <summary>
		/// Prints content of sample script file.
		/// </summary>
		Print
	}


	/// <summary>
	/// Summary description for ScriptOptions.
	/// </summary>
	public class ScriptOptions
	{
		public ScriptOptions()
		{
		}

		public static string GetCompileOption(ScriptCompileOption op)
		{
			string[] options=new string[]{"/e","/ew","/c","/ca","/cd","/dbg","/s"};
			return  options[(int)op];
		}

	}
}
