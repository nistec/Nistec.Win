using System;
using System.Reflection;
using System.Collections;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
  
namespace PCM.Engine.Utility
{
	/// <summary>
	/// Class for resolving assembly name to assembly file
	/// </summary>
	public class AssemblyResolver
	{
		#region Members
		/// <summary>
		/// File to be excluded from assembly search
		/// </summary>
		static public string ignoreFileName = "";
		#endregion

		#region Public Methods
		/// <summary>
		/// Resolves assembly name to assembly file
		/// </summary>
		/// <param name="assemblyName">The name of assembly</param>
		/// <param name="workingDir">The name of directory where local assemblies are expected to be</param>
		/// <returns></returns>
		static public Assembly ResolveAssembly(string assemblyName, string workingDir)
		{
			//try file with name AssemblyDisplayName + .dll 
			string[] asmFileNameTokens = assemblyName.Split(", ".ToCharArray(), 5);
			
			string asmFileName = Path.Combine(workingDir, asmFileNameTokens[0])+ ".dll";
			if (ignoreFileName != Path.GetFileName(asmFileName) && File.Exists(asmFileName))
			{
				try
				{
					AssemblyName asmName = AssemblyName.GetAssemblyName(asmFileName);
					if (asmName != null && asmName.FullName == assemblyName)
					{
						return Assembly.LoadFrom(asmFileName);
					}
				}
				catch{}
			}

			//try all dll files (in script folder) which contain namespace as a part of file name
			ArrayList asmFileList = new ArrayList(Directory.GetFileSystemEntries(workingDir, string.Format("*{0}*.dll", asmFileNameTokens[0])));
			foreach(string asmFile in asmFileList)	
			{
				try
				{
					if (ignoreFileName != Path.GetFileName(asmFile))
					{
						AssemblyName asmName = AssemblyName.GetAssemblyName(asmFile);
						if (asmName != null && asmName.FullName == assemblyName)
						{
							return Assembly.LoadFrom(asmFile);
						}
					}
				}
				catch{}
			}

			//try all the rest of dll files in script folder
			string[] asmFiles = Directory.GetFileSystemEntries(workingDir, "*.dll");
			foreach(string asmFile in asmFiles)	
			{
				if (asmFileList.Contains(asmFile))
					continue;
				try
				{
					if (ignoreFileName != Path.GetFileName(asmFile))
					{
						AssemblyName asmName = AssemblyName.GetAssemblyName(asmFile);
						if (asmName != null && asmName.FullName == assemblyName)
						{
							return Assembly.LoadFrom(asmFile);
						}
					}
				}
				catch{}
			}
			return null;
		}
		/// <summary>
		/// Resolves namespace into array of assembly locations (local and GAC ones).
		/// </summary>
		static public string[] FindAssembly(string nmSpace, string workingDir)
		{
			ArrayList retval = new ArrayList();
			string[] asmLocations = FindLocalAssembly(nmSpace, workingDir);
		
			if (asmLocations.Length != 0)	
			{
				foreach(string asmLocation in asmLocations)	//local assemblies
				{
					retval.Add(asmLocation);
				}
			}
			else
			{	
				string[] asmGACLocations = FindGlobalAssembly(nmSpace); //global assemblies
				foreach(string asmGACLocation in asmGACLocations)
				{
					retval.Add(asmGACLocation);
				}
			}
			return (string[])retval.ToArray(typeof(string));
		}
		/// <summary>
		/// Resolves namespace into array of local assembly locations.
		/// (it returns only one assembly location)
		/// </summary>
		static public string[] FindLocalAssembly(string refNamespace, string workingDir) 
		{
			ArrayList retval = new ArrayList();

			//try to predict assembly file name on the base of namespace
			string asesemblyLocation = String.Format("{0}\\{1}.dll", workingDir, refNamespace );
			
			if(ignoreFileName != Path.GetFileName(asesemblyLocation) && File.Exists(asesemblyLocation))
			{
				retval.Add(asesemblyLocation);
				return (string[])retval.ToArray(typeof(string));
			} 
						
			//try all dll files (in script folder) which contain namespace as a part of file name
			string tmp = string.Format("*{0}*.dll", refNamespace);
			ArrayList asmFileList = new ArrayList(Directory.GetFileSystemEntries(workingDir, string.Format("*{0}*.dll", refNamespace)));
			foreach(string asmFile in asmFileList)	
			{
				if (ignoreFileName != Path.GetFileName(asmFile) && IsNamespaceDefinedInAssembly(asmFile, refNamespace))
				{
					retval.Add(asmFile);
					return (string[])retval.ToArray(typeof(string));
				}
			}

			//try all the rest of dll files in script folder
			string[] asmFiles = Directory.GetFileSystemEntries(workingDir, "*.dll");
			foreach(string asmFile in asmFiles)	
			{
				if (asmFileList.Contains(asmFile))
					continue;

				if (ignoreFileName != Path.GetFileName(asmFile) && IsNamespaceDefinedInAssembly(asmFile, refNamespace))
				{
					retval.Add(asmFile);
					return (string[])retval.ToArray(typeof(string));
				}
			}
			return (string[])retval.ToArray(typeof(string));
		}

		/// <summary>
		/// Resolves namespace into array of global assembly (GAC) locations.
		/// </summary>
		static public string[] FindGlobalAssembly(String namespaceStr) 
		{
			ArrayList retval = new ArrayList();
			AssemblyEnum asmEnum = new AssemblyEnum(namespaceStr);
			String asmName;
			while ((asmName = asmEnum.GetNextAssembly()) != null)
			{
				string asmLocation = AssemblyCache.QueryAssemblyInfo(asmName);
				retval.Add(asmLocation);
			}
			return (string[])retval.ToArray(typeof(string));
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Search for namespace into local assembly file.
		/// </summary>
		static private bool IsNamespaceDefinedInAssembly(string asmFileName, string namespaceStr) 
		{
			if (File.Exists(asmFileName))
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(asmFileName);
					if (assembly != null)	
					{
						foreach (Module m in assembly.GetModules())
						{
							foreach (Type t in m.GetTypes())
							{
								if (namespaceStr == t.Namespace)
								{
									return true;
								}
							}
						}	
					}
				}
				catch {}
			}
			return false;
		}
		#endregion
	}
}
