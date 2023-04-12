using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

#if(CLIENT)
[assembly: AssemblyTitle("MControl.Client.Net")]
[assembly: AssemblyFileVersion("3.5.0.0")]
#elif(SERVER)
[assembly: AssemblyTitle("MControl.Server.Net")]
[assembly: AssemblyFileVersion("3.5.0.0")]
#else
[assembly: AssemblyTitle("MControl.Win")]
[assembly: AssemblyFileVersion("3.5.0.0")]
#endif

[assembly: AssemblyDescription("Application Framework")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("MControl.Net")]
[assembly: AssemblyProduct("MControl.Win")]
[assembly: AssemblyCopyright("Copyright © MControl.Net 2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]
	
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f4a11dc1-4856-4045-8622-6a477628c4ce")]//"f4a11dc1-4856-4045-8622-6a477628c4ce")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("3.5.0.0")]



#region netFramework
#if(false)
namespace MControl.Net
{

    using System;
    using System.ComponentModel.Design;
    using System.Security.Permissions;
    using MControl.Net;


    /// <summary>
    /// netDal
    /// </summary>
    [Serializable]
    internal sealed class FrameworkNet
    {

        #region Members

        private static bool _IsNet = false;
        private const string ctlVersion = "3.5.0.0";
        private const string ctlNumber = "f4a11dc1-4856-4045-8622-6a477628c4ce";
        private const string ctlName = "Framework";

        #endregion

         #region Static

        private static bool IsMControl()
        {
            try
            {
                //byte[] pk1 = MControl.Net.License.nf_1.nf_4().GetName().GetPublicKeyToken();
                //byte[] pk2 = System.Reflection.Assembly.GetAssembly(typeof(MControl.Net.FrameworkNet)).GetName().GetPublicKeyToken();

                return true;// MControl.Net.License.nf_1.ed_7(pk1).Equals(MControl.Net.License.nf_1.ed_7(pk2));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        internal static bool NetFram(string method, string mode)
        {
            try
            {


                _IsNet = true;


                if (!_IsNet)
                {

                    //if (!IsMControl())
                    //{
                    //    throw new ArgumentException("Invalid MControl.Net Reference", "IsMControl");
                    //}
#if(CLIENT)
                    //System.Reflection.MethodBase methodBase = (System.Reflection.MethodBase)(new System.Diagnostics.StackTrace().GetFrame(2).GetMethod());
                    //_IsNet = MControl.Net.License.nf_1.nf_6(ctlName, ctlVersion, method, mode,methodBase);
#else
                    //_IsNet = MControl.Net.License.nf_1.nf_6(ctlName, ctlVersion, method, mode);

#endif

                    if (!_IsNet)
                    {
                        throw new ArgumentException("Invalid MControl.Net Reference", "Framework");
                    }
                }
                return _IsNet;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new ArgumentNullException("MControl.Net", "Invalid MControl.Net Reference");
            }
        }

        #endregion
    }
}
#endif
#endregion
