
using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security.Policy;
using System.Collections;
using System.Threading;
using System.Text;

namespace MControl.Runtime
{
    /// <summary>
    /// Executes "public static void Main(..)" of assembly in a separate domain.
    /// </summary>
    public class AssemblyExec
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

        #region Exec Dll
 
        /// <summary>
        /// Invoke assembly (dll)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ClassName"></param>
        /// <param name="methodName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static object InvokeAssembly(string fileName, string ClassName, string methodName, object[] Params)
        {

            object res = null;

            if (!File.Exists(fileName))
            {
                throw new Exception("Error:  Assembly file not exist");
            }

            try
            {
                //Load the Assembly
                Assembly assembly = Assembly.LoadFile(fileName);

                // Get a Type object corresponding to assembly.
                Type oType = assembly.GetType(ClassName);

                if (oType == null)
                {
                    throw new Exception("Error:  Load the Assembly Class Name Failed");
                }

                Type[] typeArray = null;

                if (Params != null)
                {
                    int prmCount = Params.Length;

                    // Create a Parameters Type array.
                    typeArray = new Type[prmCount];
                    for (int i = 0; i < prmCount; i++)
                    {
                        typeArray.SetValue(typeof(object), i);
                    }
                }


                MethodInfo mi = oType.GetMethod(methodName, typeArray);

                if (mi != null)
                {
                    object obj = assembly.CreateInstance(oType.Name);
                    res = mi.Invoke(obj, Params);
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        #endregion
    }
}
			
