
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PCM.Engine.Utility
{
	
	/// <summary>
	/// Class that implements parsing the single C# Script file
	/// </summary>
	/// <summary>
	/// Implementation of the IComparer for sorting operations of collections of FileParser instances
	/// </summary>
	class FileParserComparer : IComparer
	{
		int IComparer.Compare(object x, object y)
		{
			if (x == null && y == null)
				return 0;

			int retval = x == null ? -1 : (y == null ? 1 : 0); 

			if (retval == 0)
			{
				FileParser xParser = (FileParser)x;
				FileParser yParser = (FileParser)y;
				retval = string.Compare(xParser.fileName, yParser.fileName, true);
				if (retval == 0)
				{
					retval = ParsingParams.Compare(xParser.prams, yParser.prams);
				}
			}

			return retval;
		}
	}

	/// <summary>
	/// Class that manages parsing the main and all imported (if any) C# Script files
	/// </summary>
	public class ScriptParser
	{
		public System.Threading.ApartmentState apartmentState = System.Threading.ApartmentState.Unknown;
		public string[] FilesToCompile
		{
			get 
			{
				ArrayList retval = new ArrayList();
				foreach(FileParser file in fileParsers)
					retval.Add(file.FileToCompile);
				return (string[])retval.ToArray(typeof(string)); 
			}
		}
		public string[] ReferencedNamespaces
		{
			get {return (string[])referencedNamespaces.ToArray(typeof(string)); }
		}
		public string[] ReferencedAssemblies
		{
			get {return (string[])referencedAssemblies.ToArray(typeof(string)); }
		}
				

		public ScriptParser(string fileName)
		{
			referencedNamespaces = new ArrayList(); 
			referencedAssemblies = new ArrayList();
			
			//process main file
			FileParser mainFile = new FileParser(fileName, null, true, false, null);
			this.apartmentState = mainFile.apartmentState;

			foreach (string namespaceName in mainFile.ReferencedNamespaces)
				PushNamespace(namespaceName);

			foreach (string asmName in mainFile.ReferencedAssemblies)
				PushAssembly(asmName);

			this.searchDirs = new string[] {Path.GetDirectoryName(mainFile.fileName)}; //note: mainFile.fileName is warrantied to be a full name but fileName is not

			//process impported files if any
			foreach (ScriptInfo fileInfo in mainFile.ReferencedScripts)
				ProcessFile(fileInfo);

			//Main script file shall always be the first. Add it now as previously array was sorted a few times
			this.fileParsers.Insert(0, mainFile); 
		}
		
		private void ProcessFile(ScriptInfo fileInfo)
		{
			FileParserComparer fileComparer = new FileParserComparer();
			
			FileParser importedFile = new FileParser(fileInfo.fileName, fileInfo.parseParams, false, true, this.searchDirs); //do not parse it yet (the third param is false)
			if (fileParsers.BinarySearch(importedFile, fileComparer) < 0)
			{
				importedFile.ProcessFile(); //parse now namespaces, ref. assemblies and scripts; also it will do namespace renaming

				this.fileParsers.Add(importedFile);
				this.fileParsers.Sort(fileComparer);

				foreach (string namespaceName in importedFile.ReferencedNamespaces)
					PushNamespace(namespaceName);

				foreach (string asmName in importedFile.ReferencedAssemblies)
					PushAssembly(asmName);

				foreach(ScriptInfo scriptFile in importedFile.ReferencedScripts)
					ProcessFile(scriptFile);
			}
		}

		private ArrayList fileParsers = new ArrayList();
	
		public string[] SaveImportedScripts()
		{
			string workingDir = Path.GetDirectoryName(((FileParser)fileParsers[0]).fileName);
			ArrayList retval = new ArrayList();

			for (int i = 1; i < this.FilesToCompile.Length; i++)	//imported script file only
			{	
				string scriptFile = this.FilesToCompile[i]; 
				string newFileName =  Path.Combine(workingDir, Path.GetFileName(scriptFile));
				if (File.Exists(newFileName))
					File.SetAttributes(newFileName, FileAttributes.Normal);
				File.Copy(scriptFile, newFileName, true);
				retval.Add(newFileName);
			}
			return (string[])retval.ToArray(typeof(string));
		}
		public void DeleteImportedFiles()
		{
			for (int i = 1; i < this.FilesToCompile.Length; i++) //do not delete main script file (index == 0)
			{
				if (i != 0)	 
				{
					try
					{
						File.SetAttributes(this.FilesToCompile[i], FileAttributes.Normal);
						File.Delete(this.FilesToCompile[i]);
					}
					catch{}
				}
			}
		}
				
		private ArrayList referencedNamespaces;
		private ArrayList referencedAssemblies;
		private string[] searchDirs;

		private void PushNamespace(string nameSpace)
		{
			if (referencedNamespaces.Count > 1)
				referencedNamespaces.Sort();

			if (referencedNamespaces.BinarySearch(nameSpace) < 0)
				referencedNamespaces.Add(nameSpace);
		}
		private void PushAssembly(string asmName)
		{
			string entrtyName = asmName.ToLower();
			if (referencedAssemblies.Count > 1)
				referencedAssemblies.Sort();

			if (referencedAssemblies.BinarySearch(entrtyName) < 0)
				referencedAssemblies.Add(entrtyName);
		}
	}		
}