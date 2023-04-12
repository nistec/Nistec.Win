using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.ComponentModel;
using System.Data;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;   


namespace PCM.ScriptEngine.Compiler
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CompileScript
	{
		#region Members
		private const int _ErrorRetValue = -1;	
		private string _FileScript="";
		private string _FileAssembly="";
		private string[] _References;
		private string _Script="";
		private string _CompileMessage="";
		#endregion

		#region Constructors

		public CompileScript()
		{

		}

		public CompileScript(string script,string assemblyName)
		{
			_FileAssembly=assemblyName;
			//SetDefaultReferences();
			_Script=script;
		}

		//		public CreateAssembly(string FileScript,string assemblyName)
		//		{
		//			_FileScript=fileScript;
		//			_FileAssembly=assemblyName;
		//			SetDefaultReferences();
		//			SetScriptFromFile(fileScript);
		//		}

		public CompileScript(string fileScript,string assemblyName,string[] references)
		{
			_FileScript=fileScript;
			_FileAssembly= assemblyName;
			_References=references;
			SetScriptFromFile(fileScript);
		}

		#endregion

		#region Properties

		public string FileScript
		{
			get{return _FileScript;}
			set{_FileScript=value;}
		}
	
		public string FileAssembly
		{
			get{return _FileAssembly;}
			set{_FileAssembly=value;}
		}

		public string Script
		{
			get{return _Script;}
			set{_Script=value;}
		}

		private string[] References
		{
			get{return _References;}
			set{_References=value;}
		}

		public string CompileMessage
		{
			get{return _CompileMessage;}
		}

		#endregion

		#region PrivateMethods

		private void SetScriptFromFile(string fileScript)
		{
			_Script ="";
			System.IO.StreamReader SR = new StreamReader(fileScript);   	
			_Script = SR.ReadToEnd(); 
		}

//		public int SetScriptFromSource(string source,string[] references)
//		{
//			_Script =source;
//			_References=references;
//			//SetDefaultReferences ();
//			return Compile();
//		}

		public int SetScriptFromSource(string source)
		{
			_Script =source;
			SetDefaultReferences ();
			return Compile();
		}


		private void SetDefaultReferences()
		{
			//string pcmDll=GetIEngineReference();
			/// <summary>
			/// string array containing the names of the assemblies added as references to the engine by default.
			/// </summary>
			_References = new string [] 
			{
				@"Mscorlib.dll",
				@"System.dll",
				@"System.Data.dll",
				@"System.Drawing.dll",
				@"System.Windows.Forms.dll",
				@"System.Xml.dll",
				@"Microsoft.Vsa.dll"
				
			};

		}

//		private string GetIEngineReference()
//		{
//			Microsoft.Win32.RegistryKey rg=Microsoft.Win32.Registry.LocalMachine;
//			rg=rg.OpenSubKey(@"Software\PCM\Engine");
//			return rg.GetValue("IEngineReference",@"PCM.IEngine.dll").ToString();
//		}


		#endregion

		#region Public Methods

		private int Compile()
		{
			
			//Create an instance whichever code provider that is needed
			CodeDomProvider codeProvider = new CSharpCodeProvider();

			//create the language specific code compiler
			ICodeCompiler compiler = codeProvider.CreateCompiler();

			//add compiler parameters
			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = false;			
			compilerParams.IncludeDebugInformation = false;
			
			compilerParams.ReferencedAssemblies.Add(PCM.IEngine.Config.GetIEngineReference());
			compilerParams.ReferencedAssemblies.Add(PCM.IEngine.Config.GetDalReference());
			compilerParams.ReferencedAssemblies.Add(PCM.IEngine.Config.GetDataAccessReference());
		
			if(_References!=null)
			{
				foreach(string sref in _References)
				{
					compilerParams.ReferencedAssemblies.Add(sref);
				}
				//compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
				//compilerParams.ReferencedAssemblies.Add("System.dll");
				compilerParams.OutputAssembly = _FileAssembly; 
			}

						
			//actually compile the code
			CompilerResults results = compiler.CompileAssemblyFromSource(compilerParams, _Script);
	
			System.Text.StringBuilder sb=new StringBuilder (); 
	
			//Do we have any compiler errors
			if (results.Errors.Count > 0)
			{
				foreach (CompilerError error in results.Errors)
				{
					sb.Append (error.ErrorText+ "\r\n\r\n");
					//Console.WriteLine ("Compiler Error:  " + error.ErrorText + "\r\n\r\n");
				}
                _CompileMessage =sb.ToString ();
				//frmMsgDetails.Open (sb.ToString (),"Compiler Errors");
				return -1;
			}
		
			for (int i=0 ;i< results.Output.Count ;i++)
			{
				sb.Append (results.Output[i] + "\r\n\r\n");
			}
			sb.Append ("Compiled successfuly" + "\r\n\r\n");
			_CompileMessage =sb.ToString ();
			//frmMsgDetails.Open (sb.ToString (),"Compiler ");
		
			return 0;
		}

		#endregion
	}
}