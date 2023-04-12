
using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security.Policy;
using System.Collections;
using System.Threading;
using System.Text;

namespace PCM.Engine.Utility

{
	/// <summary>
	/// Executes "public static void Main(..)" of assembly in a separate domain.
	/// </summary>
	class AssemblyExec
	{
		#region Members
		AppDomain appDomain;
		RemoteExecAssembly remoteExecAssembly;
		string assemblyFileName;

		#endregion

		#region Constructor

		public AssemblyExec(string fileNname, string domainName)
		{
			assemblyFileName = fileNname;
			AppDomainSetup setup = new AppDomainSetup();
			setup.ApplicationBase = Path.GetDirectoryName(assemblyFileName);
			setup.PrivateBinPath = AppDomain.CurrentDomain.BaseDirectory;
			setup.ApplicationName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
			setup.ShadowCopyFiles = "true";
			setup.ShadowCopyDirectories = Path.GetDirectoryName(assemblyFileName);
			appDomain = AppDomain.CreateDomain(domainName, null, setup);
	
			remoteExecAssembly = (RemoteExecAssembly)appDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().Location, typeof(RemoteExecAssembly).ToString());
		}
		#endregion

		#region Methods

		public object Execute(string[] args)
		{
			return remoteExecAssembly.ExecuteAssembly(assemblyFileName, args);
		}
		public void Unload()
		{
			AppDomain.Unload(appDomain);
			appDomain = null;
		}
		#endregion
	}
}
			
