using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security.Policy;
using System.Collections;
using System.Threading;
using System.Text;

using PCM.Engine.Utility;

namespace PCM.ScriptEngine
{
	/// <summary>
	/// ExecScript is an class that implements execution of *.cs files.
	/// </summary>
	public class ExecScript
	{

		#region Members


		//public delegate void PrintDelegate(string msg);


		/// <summary>
		/// C# Script arguments array (sub array of application arguments array).
		/// </summary>
		string[] scriptArgs;
		/// <summary>
		/// Callback to print application messages to appropriate output.
		/// </summary>
		static PrintDelegate print;
		/// <summary>
		/// Container for paresed command line parguments
		/// </summary>
		ExecuteOptions options;
		/// <summary>
		/// Flag to force to rethrow critical exceptions
		/// </summary>
		bool rethrow;
		#endregion 

		#region Properties
		/// <summary>
		/// Force caught exceptions to be rethrown.
		/// </summary>
		public bool Rethrow
		{
			get {return rethrow;}
			set {rethrow = value;}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public ExecScript()
		{
			rethrow = false;
			options = new ExecuteOptions();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public object Execute(string[] args, PrintDelegate printDelg)
		{
			object res=null;
			print = printDelg;
			if (args.Length > 0) 
			{
				try
				{
					
					#region Parse arguments
					//need to separeate application arguments from script ones
					//script engine arguments are followed by script arguments
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i].StartsWith("/"))
						{
							if (args[i] == "/nl")
							{
								options["noLogo"] = true;
							}
							else if (args[i] == "/c")
							{
								options["useCompiled"] = true;
							}
							else if (args[i].StartsWith("/ca"))
							{
								options["useCompiled"] = true;
								options["supressExecution"] = true;
							}
							else if (args[i].StartsWith("/cd"))
							{
								options["useCompiled"] = true;
								options["supressExecution"] = true;
								options["DLLExtension"] = true;
							}
							else if (args[i].StartsWith("/dbg"))
							{
								options["DBG"] = true;
							}
							else if (args[i].StartsWith("/r:"))
							{
								string[] assemblies = args[i].Remove(0, 3).Split(":".ToCharArray());
								options["refAssemblies"] = assemblies;
							}
							else if (args[i].StartsWith("/e"))
							{
								options["buildExecutable"] = true;
								options["supressExecution"] = true;
								options["buildWinExecutable"] = args[i].StartsWith("/ew");
							}
//							else if (args[i].StartsWith("/x"))
//							{
//								options["execute"] = true;
//								options["scriptFileName"] = args[i];	
//
//							}
							else if (args[0] == "/?" || args[0] == "-?")
							{
								//ShowHelp();
								options["processFile"] = false;
								break;
							}
							else if (args[0] == "/s")
							{
								//ShowSample();
								options["processFile"] = false;
								break;
							}
						}
						else
						{
							//this is the end of application arguments
							options["scriptFileName"] = args[i];	

							//prepare script arguments array
							scriptArgs = new string[args.Length - (1 + i)];
							Array.Copy(args, (1 + i), scriptArgs, 0, args.Length - (1 + i)); 
							break;
						}
					}
					#endregion

					if (options.GetBool("processFile"))
					{
						options["scriptFileName"] = FileParser.ResolveFile((string)options["scriptFileName"], null);
										
						if (!options.GetBool("noLogo"))
						{
							//Console.WriteLine(AppInfo.appLogo);
						}

						//compile
						string assemblyFileName = GetAvailableAssembly((string)options["scriptFileName"]);
						if (!options.GetBool("buildExecutable") || !options.GetBool("useCompiled") || 
							(options.GetBool("useCompiled") && assemblyFileName == null))
						{
							try
							{
								assemblyFileName = Compile((string)options["scriptFileName"]);
							}
							catch (Exception e) 
							{
								print("Error: Specified file could not be compiled.\n");
								throw e;
							}
						}

						//execute
						if (!options.GetBool("supressExecution")) 
						{
							try 
							{
								res=ExecuteAssembly(assemblyFileName);
							}
							catch (Exception e) 
							{
								print("Error: Specified file could not be executed.\n");
								throw e;
							}	

							//cleanup
							if (File.Exists(assemblyFileName) && !options.GetBool("useCompiled")) 
							{ 
								try
								{
									File.Delete(assemblyFileName); 
								}
								catch{}
							}
						} 
					}
				}
				catch (Exception e) 
				{
					if (rethrow)
					{
						throw e;
					}
					else
					{
						print("Exception: " + e.ToString());
					}
				}
			} 
			//else 
			//{
			//	ShowHelp();
			//}
			return res;
		}

		#endregion

		#region ExecuteOptions
		/// <summary>
		/// Application specific version of Hashtable
		/// </summary>
		private class ExecuteOptions : Hashtable
		{
			public ExecuteOptions()
			{
				this["processFile"] = true;
				this["scriptFileName"] = "";
			}
			public bool IsSet(string name)
			{
				return this[name] != null;
			}
			public bool GetBool(string name)
			{
				return this[name] != null ? (bool)this[name] : false; //default is false
			}
		}
		#endregion 

		#region Methods
		/// <summary>
		/// Checks/returns if compiled C# script file (ScriptName + "c") available and valid.
		/// </summary>
		private string GetAvailableAssembly(string scripFileName) 
		{
			string retval = null;
			string asmFileName = scripFileName + "c";
			if (File.Exists(asmFileName) && File.Exists(scripFileName))
			{
				FileInfo scriptFile = new FileInfo(scripFileName);
				FileInfo asmFile = new FileInfo(asmFileName);
				if( asmFile.LastWriteTime == scriptFile.LastWriteTime && 
					asmFile.LastWriteTimeUtc == scriptFile.LastWriteTimeUtc)
				{
					retval = asmFileName;
				}
			}
			return retval;
		} 

		/// <summary>
		/// Compiles C# script file into assembly.
		/// </summary>
		public string Compile(string scriptFile, string assemblyFile, bool debugBuild) 
		{
			if (assemblyFile != null)
				options["forceOutputAssembly"] = assemblyFile;
			else
				options["forceOutputAssembly"] = Path.GetTempFileName();
			if (debugBuild)
				options["DBG"] = true;
			return Compile(scriptFile);
		}

		/// <summary>
		/// Compiles C# script file.
		/// </summary>
		private string Compile(string scriptFileName) 
		{
			bool generateExe = options.GetBool("buildExecutable");
			bool localAssembliesUsed = false;
			string scriptDir = Path.GetDirectoryName(scriptFileName);
			string assemblyFileName = "";

			//parse source file in order to find all referenced assemblies
			//!!! assembly name is the same as namespace + ".dll"
			//if script doesn't follow this assumption user will need to 
			//specify assemblies explicitly  
			ScriptParser parser = new ScriptParser(scriptFileName);

			if (parser.apartmentState != System.Threading.ApartmentState.Unknown)
				System.Threading.Thread.CurrentThread.ApartmentState = parser.apartmentState;

			ICodeCompiler compiler = (new CSharpCodeProvider()).CreateCompiler();
			CompilerParameters compileParams = new CompilerParameters();
         	

			compileParams.IncludeDebugInformation = options.GetBool("DBG");
			compileParams.GenerateExecutable = generateExe;
			compileParams.GenerateInMemory = !generateExe;
			
			//some assemblies were referenced from command line
			if (options.IsSet("refAssemblies"))
			{
				foreach(string asmName in (string[])options["refAssemblies"])
				{
					string asmLocation = Path.Combine(scriptDir, asmName);
					if (File.Exists(asmLocation))
					{
						compileParams.ReferencedAssemblies.Add(asmLocation);
						localAssembliesUsed = true;
					}
				}
			}
	
		
			AssemblyResolver.ignoreFileName = Path.GetFileNameWithoutExtension(scriptFileName) + ".dll";

			foreach (string nmSpace in parser.ReferencedNamespaces) 
			{
				//find local and global assemblies assuming assembly name is the same as a namespace
				foreach (string asmLocation in AssemblyResolver.FindAssembly(nmSpace, scriptDir)) 
				{
					compileParams.ReferencedAssemblies.Add(asmLocation);

					if (!localAssembliesUsed)
						localAssembliesUsed = (Path.GetDirectoryName(asmLocation) == scriptDir);
				}
			}
			
			foreach (string asmName in parser.ReferencedAssemblies) //some assemblies were referenced from code
			{
				string asmLocation = Path.Combine(scriptDir, asmName);
				if (File.Exists(asmLocation))
				{
					compileParams.ReferencedAssemblies.Add(asmLocation);
					localAssembliesUsed = true;
				}
			}

			//compileParams.ReferencedAssemblies.Add(@"F:\ScriptApplication\DllSample\bin\Debug\DllSample.dll");
			//TODO:check this
			//compileParams.ReferencedAssemblies.Add(@"C:\PCM_1.0\References\BIN\Debug\PCM.IEngine.dll");
			//compileParams.ReferencedAssemblies.Add(@"C:\PCM_1.0\References\BIN\Debug\PCM.IEngine.dll");
			//compileParams.ReferencedAssemblies.Add(PCM.IEngine.Config.GetIEngineReference());
			//compileParams.ReferencedAssemblies.Add(PCM.IEngine.Config.GetDalReference());
	
			if (options.IsSet("forceOutputAssembly"))
			{
				assemblyFileName = (string) options["forceOutputAssembly"];
			}
			else
			{
				if (generateExe)
					assemblyFileName = Path.Combine(scriptDir, Path.GetFileNameWithoutExtension(scriptFileName) + ".exe");
				else if (options.GetBool("useCompiled") || localAssembliesUsed)
					if (options.GetBool("DLLExtension") )
						assemblyFileName = Path.Combine(scriptDir, Path.GetFileNameWithoutExtension(scriptFileName) + ".dll");
					else
						assemblyFileName = Path.Combine(scriptDir, scriptFileName + "c");
				else
					assemblyFileName = Path.GetTempFileName();
			}

			compileParams.OutputAssembly = assemblyFileName;

			if (generateExe && options.GetBool("buildWinExecutable"))
				compileParams.CompilerOptions = "/target:winexe";

			if (File.Exists(assemblyFileName))
				File.Delete(assemblyFileName);
			
			CompilerResults results = compiler.CompileAssemblyFromFileBatch(compileParams, parser.FilesToCompile);
			if (results.Errors.Count != 0) 
			{
				StringBuilder compileErr = new StringBuilder();
				foreach (CompilerError err in results.Errors) 
				{
					compileErr.Append(err.ToString());
				}
				throw new Exception(compileErr.ToString());				
			}
			else
			{
				parser.DeleteImportedFiles();

				if (!options.GetBool("DBG")) //pdb file might be needed for a debugger
				{
					string pdbFile = Path.Combine(Path.GetDirectoryName(assemblyFileName), Path.GetFileNameWithoutExtension(assemblyFileName)+".pdb");	
					if (File.Exists(pdbFile))
						File.Delete(pdbFile);
				}

				FileInfo scriptFile = new FileInfo(scriptFileName);
				FileInfo asmFile = new FileInfo(assemblyFileName);
				if (scriptFile!= null && asmFile != null)
				{
					asmFile.LastWriteTime = scriptFile.LastWriteTime; 
					asmFile.LastWriteTimeUtc = scriptFile.LastWriteTimeUtc;
				}
			}
			return assemblyFileName;
		}


		/// <summary>
		/// Executes compiled C# script file.
		/// Invokes static method 'Main' from the assembly.
		/// </summary>
		private object ExecuteAssembly(string assemblyFile) 
		{
			//execute assembly in a different domain to make it possible to unload assembly before clean up
			AssemblyExec assemblyExec = new AssemblyExec(assemblyFile, "AsmExecution");
			return assemblyExec.Execute(scriptArgs);
		} 

		#endregion

	}

}			
