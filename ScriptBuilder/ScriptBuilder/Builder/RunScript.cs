using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Nistec.ScriptBuilder
{
	public delegate void PrintDelegate(string msg);
	/// <summary>
	/// Class Run Script implements class library for ExecScript.
	/// </summary>
	public class RunScript
	{
		public RunScript()
		{
			rethrow = false;
		}
		/// <summary>
		/// Force caught exceptions to be rethrown.
		/// </summary>
		static public bool Rethrow
		{
			get {return rethrow;}
			set {rethrow = value;}
		}
		/// <summary>
		/// Invokes ExecScript (script engine)
		/// </summary>
		static public void Execute(PrintDelegate print, string[] args)
		{
			AppInfo.appName = new FileInfo(Application.ExecutablePath).Name;
			ExecScript exec = new ExecScript();
			exec.Rethrow = Rethrow;
			exec.Execute(args, new PrintDelegate(print != null ? print : new PrintDelegate(DefaultPrint)));
		}
		/// <summary>
		/// Invokes ExecScript (script engine)
		/// </summary>
		public void Execute(PrintDelegate print, string[] args, bool rethrow)
		{
			AppInfo.appName = new FileInfo(Application.ExecutablePath).Name;
			ExecScript exec = new ExecScript();
			exec.Rethrow = rethrow;
			exec.Execute(args, new PrintDelegate(print != null ? print : new PrintDelegate(DefaultPrint)));
		}
		/// <summary>
		/// Compiles script into assembly with ExecScript
		/// </summary>
		/// <param name="scriptFile">The name of script file to be compiled.</param>
		/// <param name="assemblyFile">The name of compiled assembly. If set to null a temnporary file name will be used.</param>
		/// <param name="debugBuild">true if debug information should be included in assembly; otherwise, false.</param>
		/// <returns></returns>
		static public string Compile(string scriptFile, string assemblyFile, bool debugBuild)
		{
			ExecScript exec = new ExecScript();
			exec.Rethrow = true;
			return exec.Compile(scriptFile, assemblyFile, debugBuild);
		}
		/// <summary>
		/// Compiles script into assembly with ExecScript and loads it in current AppDomain
		/// </summary>
		/// <param name="scriptFile">The name of script file to be compiled.</param>
		/// <param name="assemblyFile">The name of compiled assembly. If set to null a temnporary file name will be used.</param>
		/// <param name="debugBuild">true if debug information should be included in assembly; otherwise, false.</param>
		/// <returns></returns>
		static public Assembly Load(string scriptFile, string assemblyFile, bool debugBuild)
		{
			ExecScript exec = new ExecScript();
			exec.Rethrow = true;
			string outputFile = exec.Compile(scriptFile, assemblyFile, debugBuild);

			AssemblyName asmName = AssemblyName.GetAssemblyName(outputFile);
			return AppDomain.CurrentDomain.Load(asmName);
		}
		/// <summary>
		/// Default implementation of displaying application messages.
		/// </summary>
		static void DefaultPrint(string msg)
		{
			//do nothing
		}
		static bool rethrow;
	}
}
