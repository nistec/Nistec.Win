
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms; 
using System.Reflection;

using System.Text;
using System.Runtime.InteropServices;
using MControl.ScriptBuilder;
     
namespace AppEngine.Client
{
	//delegate void PrintDelegate(string msg);


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
	/// Wrapper class that runs Executor within console application context.
	/// </summary>
	class ScriptClientTester
	{

		public static string GetCompileOption(ScriptCompileOption op)
		{
			string[] options=new string[]{"/e","/ew","/c","/ca","/cd","/dbg","/s"};
			return  options[(int)op];
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			string[] arg=null; 
			object res=null;

			ExecScript exec =new ExecScript();

			ScriptCompileOption mode=ScriptCompileOption.Compiled;

			if(mode==ScriptCompileOption.Compiled)
			{
                //arg=new string [2];
                //arg[0]=@"D:\MControl\Bin_3.5.0\NetCell_3.5\References\ScriptLibrary\test3_1.6.csc";
                //arg[1] = @"D:\MControl\Bin_3.5.0\NetCell_3.5\References\ScriptLibrary\test3_1.6.dll";
                //string s=exec.Compile(arg[0],arg[1],true);
                //Console.WriteLine(s);

                object o = ExecDll.RunWithArgs(@"D:\MControl\Bin_3.5.0\NetCell_3.5\References\ScriptLibrary\test_1.dll","AppScript","Execute",new string[]{"0","2"});
                
                Console.WriteLine(o);
	
			}
			else if(mode==ScriptCompileOption.Console)
			{
				arg=new string [2];
				arg[0]=GetCompileOption(ScriptCompileOption.Console);
				arg[1]="D:\\PCMx\\ScriptLibrary\\App\\Testing4.csc";
				res=exec.Execute(arg,new PrintDelegate(Print));
				Console.WriteLine((string)res);
			}			
			else if(mode==ScriptCompileOption.Csc)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Dll);
				arg[1]="D:\\PCMx\\ScriptLibrary\\App\\Testing3.csc";
				res=exec.Execute(arg,new PrintDelegate(Print));
				Console.WriteLine(res.ToString());

			}
			else if(mode==ScriptCompileOption.Dll)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Dll);
				arg[1]="D:\\PCMx\\ScriptLibrary\\App\\Testing1.csc";
				res=exec.Execute(arg,new PrintDelegate(Print));
				Console.WriteLine(res.ToString());
			}
			else if(mode==ScriptCompileOption.WinExe)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Console);
				arg[1]="D:\\PCMx\\ScriptLibrary\\App\\Testing4.csc";
				res=exec.Execute(arg,new PrintDelegate(Print));
				Console.WriteLine(res.ToString());

			}

			Console.ReadLine ();
			
			//res=ExecDll.RunAssembly (arg[1],"Script","GetVal",null);
		
			//System.Text.StringBuilder sb=new StringBuilder (); 
            //string[] rest=new string [10]; 
			//AppInfo.appName = new FileInfo(Application.ExecutablePath).Name;
			//Executor exec = new Executor();
			//ExecScript exec =new ExecScript();
			//string s=exec.Compile(arg[1],arg[2],true);
		
			//res=exec.Execute(arge,new PrintDelegate(Print));
		
			//for(int i=0;i < 10000;i++)
			//{
				//PCM.Common.Timer strt=new PCM.Common.Timer ();
				//strt.Start ();
				//res=ExecDll.RunAssembly (arg[1],"Script","GetVal",null);

				//res=exec.Execute(arg,  new PCM.Engine.AppEngine.PrintDelegate(Print));
				//strt.Stop();
                 
				//Console.WriteLine(res.ToString() );
				//rest[i]=strt.TimerInMiliSeconds.ToString();
				//sb.Append (strt.TimerInMiliSeconds.ToString() + "\n\r");
			//}
//			Console.WriteLine(res.ToString() );
//			Console.WriteLine(sb.ToString () );
//			Console.ReadLine ();
		}

		/// <summary>
		/// Implementation of displaying application messages.
		/// </summary>
		static void Print(string msg)
		{
			Console.WriteLine(msg);
			Console.ReadLine ();
		}
	}
	/// <summary>
	/// Repository for application specific data
	/// </summary>
	class AppInfo
	{
		public static string appName = "cscscript";
		public static bool appConsole = true;
		public static string appLogo
		{
			get { return "C# Script execution engine. Version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()+".\nCopyright (C) 2004-2005 Oleg Shilo.\n";}
		}
		public static string appLogoShort
		{
			get { return "C# Script execution engine. Version "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()+".\n";}
		}
		public static string appParams = "[/nl]:";
		public static string appParamsHelp = "nl	-	No logo mode: No banner will be shown at execution time.\n";
	}
}
