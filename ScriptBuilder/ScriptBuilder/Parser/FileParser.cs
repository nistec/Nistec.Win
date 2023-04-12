
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Nistec.ScriptBuilder
{
	
	/// <summary>
	/// Class that implements parsing the single C# Script file
	/// </summary>
	class FileParser : ParserBase
	{
		public System.Threading.ApartmentState apartmentState = System.Threading.ApartmentState.Unknown;

		public FileParser()
		{
		}
		public FileParser(string fileName, ParsingParams prams, bool process, bool imported, string[] searchDirs)
		{
			this.imported = imported;
			this.prams = prams;
			this.searchDirs = searchDirs;
			this.fileName = ResolveFile(fileName, searchDirs);
			if (process)
				ProcessFile();
		}
		

		public string fileNameImported = ""; 
		public ParsingParams prams = null;
		
		public string FileToCompile
		{
			get {return imported ? fileNameImported : fileName;}
		}
		public string[] SearchDirs
		{
			get {return searchDirs; }
		}
		public bool Imported
		{
			get {return imported; }
		}

		public string[] ReferencedNamespaces
		{
			get {return (string[])referencedNamespaces.ToArray(typeof(string)); }
		}
		
		public string[] ReferencedAssemblies
		{
			get {return (string[])referencedAssemblies.ToArray(typeof(string)); }
		}

		public ScriptInfo[] ReferencedScripts
		{
			get {return (ScriptInfo[])referencedScripts.ToArray(typeof(ScriptInfo)); }
		}
		

		public void ProcessFile()
		{
			string codeStr = "";
			using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(0))) 
			{
				codeStr = sr.ReadToEnd();
			}
				
			Parse(codeStr);

			if (imported)
			{
				fileNameImported = Path.Combine(Path.GetTempPath(), string.Format("i_{0}_{1}{2}", Path.GetFileNameWithoutExtension(fileName), Path.GetDirectoryName(fileName).GetHashCode(), Path.GetExtension(fileName)));
				if (File.Exists(fileNameImported))
				{
					File.SetAttributes(fileNameImported, FileAttributes.Normal);
					File.Delete(fileNameImported);
				}

				using (StreamWriter scriptWriter = new StreamWriter(fileNameImported, false, Encoding.GetEncoding(0))) 
				{
					//scriptWriter.Write(ComposeHeader(fileNameImported)); //using a big header at start is overkill but maight be required in future
					scriptWriter.WriteLine(Import(codeStr));
					scriptWriter.WriteLine("///////////////////////////////////////////");
					scriptWriter.WriteLine("// Compiler-generated file - DO NOT EDIT!");
					scriptWriter.WriteLine("///////////////////////////////////////////");
				}
				File.SetAttributes(fileNameImported, FileAttributes.ReadOnly);
			}
		}
		private void Parse(string codeStr)
		{
			referencedScripts.Clear();
			referencedNamespaces.Clear();

			string clearedCode = codeStr;
			int codeStartPos = 0;

			//remove comments of '/**/' style
			clearedCode = RegexReplace(clearedCode, @"/\* ([^/] | /[^\*])*?  \*/", "", true);	

			codeStartPos = RegexFind(clearedCode, @"(namespace|class)\s");
			string declarationRegion = codeStartPos != -1 ? clearedCode.Substring(0, codeStartPos-1) : clearedCode;

			//extract all referenced scripts, assemblies and namespaces
			referencedScripts.AddRange(GetStatements(	declarationRegion, 
				@"(/[^/]|[^/]) //css_import\s*", 
				new ParseStatement(ParseImport)));

			referencedAssemblies.AddRange(GetStatements(	declarationRegion, 
				@"(/[^/]|[^/]) //css_reference\s*", 
				new ParseStatement(ParseAsmRefference)));

			referencedNamespaces.AddRange(GetStatements(	declarationRegion,
				@"using\s*", 
				new ParseStatement(ParseUsing)));

			string matchText = "";
			if (RegexFind(clearedCode.Substring(declarationRegion.Length-1), @"(\s+|\n)\[(\s*\w*\s*)\]\s*(\w*)\s*(static|static\s*static)(\s*\w*\s*)\s+ void\s*Main\s*\(", ref matchText) != -1)
			{
				if (matchText.IndexOf("STAThread") != -1)
					apartmentState = System.Threading.ApartmentState.STA;
				else if (matchText.IndexOf("MTAThread") != -1)
					apartmentState = System.Threading.ApartmentState.MTA;

			}
		}
		//works nice but not with .NET v2.0
		private void ParseAgreciveRE(string codeStr) 
		{
			referencedScripts.Clear();
			referencedNamespaces.Clear();

			string clearedCode = codeStr;
			int codeStartPos = 0;

			//remove comments of '/**/' style
			clearedCode = RegexReplace(clearedCode, @"/\* ([^/] | /[^\*])*?  \*/", "", true);	

			codeStartPos = RegexFind(clearedCode, @"(namespace|class)\s");
			string declarationRegion = codeStartPos != -1 ? clearedCode.Substring(0, codeStartPos-1) : clearedCode;

			//extract all referenced scripts, assemblies and namespaces
			referencedScripts.AddRange(RegexGetMatches(	declarationRegion, 
				@"(/[^/]|[^/]) //css_import \s* (\w|\W)*? ;", 
				new ParseStatement(ParseImport)));

			referencedAssemblies.AddRange(RegexGetMatches( declarationRegion, 
				@"(/[^/]|[^/]) //css_reference \s* (\w|\W)*? ;", 
				new ParseStatement(ParseAsmRefference)));

			referencedNamespaces.AddRange(RegexGetMatches( declarationRegion,
				@"using\s*(([^(]|[^\n])*)\w+;", 
				new ParseStatement(ParseUsing)));
		}
		private string Import(string codeStr)
		{
			string importedCode = codeStr;
			if (imported)
			{
				//replace 'static void Main(...)' with 'void i_Main(...)'
				//importedCode = RegexReplace(importedCode, @"(static|static\s*public|public\s*|private|private\s*static)\s*void\s*Main\s*\(", string.Format("static public void {0} (", "i_Main"), false);
				importedCode = RegexReplace(importedCode, @"(static|static\s*static)(\s*\w*\s*)\s+void\s*Main\s*\(", string.Format("static public void {0} (", "i_Main"), false);
				importedCode = importedCode.TrimEnd("\n\r".ToCharArray());

				if (prams != null)
				{
					foreach(string[] names in prams.RenameNamespaceMap)
					{
						importedCode = RegexReplace(importedCode, string.Format("namespace\\s*{0}\\s*", names[0]), string.Format("namespace {0}{1}", names[1], Environment.NewLine), false);
					}
				}
			}
			return importedCode;
		}
		

		private ArrayList referencedScripts = new ArrayList();
		private ArrayList referencedNamespaces = new ArrayList();
		private ArrayList referencedAssemblies = new ArrayList();

		private string[] searchDirs;
		private bool imported = false;
			
		/// <summary>
		/// Searches for script file by ginven script name. Search order:
		/// 1. Current directory
		/// 2. extraDirs (some arbitrary directories usually location of the imported scripts)
		/// 3. CSSCRIPT_DIR + \Lib
		/// 4. PATH
		/// Also fixes file name if user did not provide extension for script file (assuming .cs extension)
		/// </summary>
		public static string ResolveFile(string fileName, string[] extraDirs)
		{
			//current directory
			if (File.Exists(fileName))
			{
				return (new FileInfo(fileName).FullName);
			}
			else if (File.Exists(fileName + ".cs"))
			{
				return (new FileInfo(fileName + ".cs").FullName);
			}
			
			//arbitrary directories
			if (extraDirs != null)
			{
				foreach (string extraDir in extraDirs)
				{
					string dir = extraDir;
					if (File.Exists(fileName))
					{
						return (new FileInfo(Path.Combine(dir, fileName)).FullName);
					}
					else if (File.Exists(Path.Combine(dir, fileName) + ".cs"))
					{
						return (new FileInfo(Path.Combine(dir, fileName) + ".cs").FullName);
					}
				}
			}

			//CSSCRIPT_DIR + \Lib
			string libDir = Environment.GetEnvironmentVariable("CSSCRIPT_DIR");
			if (libDir != null)
			{	
				libDir = Path.Combine(libDir, "Lib");
				if (File.Exists(fileName))
				{
					return (new FileInfo(Path.Combine(libDir, fileName)).FullName);
				}
				else if (File.Exists(Path.Combine(libDir, fileName) + ".cs"))
				{
					return (new FileInfo(Path.Combine(libDir, fileName) + ".cs").FullName);
				}
			}

			//PATH
			string[] pathDirs = Environment.GetEnvironmentVariable("PATH").Split(';');
			foreach(string pathDir in pathDirs)
			{
				string dir = pathDir;
				if (File.Exists(fileName))
				{
					return (new FileInfo(Path.Combine(dir, fileName)).FullName);
				}
				else if (File.Exists(Path.Combine(dir, fileName) + ".cs"))
				{
					return (new FileInfo(Path.Combine(dir, fileName) + ".cs").FullName);
				}
			}
			
			throw new FileNotFoundException(string.Format("Could not find file \"{0}\"", fileName));
		}
		private object ParseUsing(string statement)
		{
			string namespaseStatement = statement.Replace("//","").Replace(";","").Replace("\n","").Replace("\r","").Replace("using", "").Trim();
			string[] parts = namespaseStatement.Split("=".ToCharArray());
			return parts[parts.Length-1].Trim();
		}
		private object ParseImport(string statement)
		{
			return new ScriptInfo(statement.Replace("//css_import","").Replace(";","").Replace("\n","").Replace("\r","").Trim());
		}
		private object ParseAsmRefference(string statement)
		{
			return statement.Replace("//css_reference","").Replace(";","").Replace("\n","").Replace("\r","").Trim();
		}
	}
}