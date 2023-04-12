using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using Nistec;
using Nistec.Generic;
//using Nistec.Configuration;

namespace Nistec.ScriptBuilder
{
    public enum eConfig
    {
        ScriptLibrary,
        AppDllPath,
        AppTempPath
    }

    /// <summary>
    /// Summary description for Config.
    /// </summary>
    public sealed class Config : AppConfig
    {
        public static readonly Config Instance = new Config();
        //XmlTable _Config;


        //public Config()
        //{
        //    _Config = new XmlTable("Scripts");
        //    _Config.LoadFromFile("","Scripts","Key");
        //}

        public Config()
            : base()
        {

            if (!base.FileExists)
            {
                SetConfigDefault();
            }
            else
            {
                this.Read();
            }
        }

        //public string this[string key]
        //{
        //    get { return base[key].ToString(); }
        //    set { base[key] = value; }
        //}

        public string GetValue(string key, string defaultValue)
        {
            object o = this[key];
            if (o == null)
                return defaultValue;
            return o.ToString();
        }

        private void SetConfigDefault()
        {
            this["ScriptLibrary"] = "";
            this["AppDllPath"] = "";
            this["AppTempPath"] = "";
            base.Save();
        }

        public string[] GetReferences()
        {
            List<string> references = new List<string>();
            IDictionary<string,object> idict = this.ToDictionary();
            foreach (KeyValuePair<string, object> entry in idict)
            {
                if (entry.Key.EndsWith(".dll") && entry.Value !=null)
                {
                    references.Add(entry.Value.ToString());
                }
            }
            return references.ToArray();
        }

        public string[] GetSystemReferences()
        {
            List<string> references = new List<string>();
            IDictionary<string, object> idict = this.ToDictionary();
            foreach (KeyValuePair<string, object> entry in idict)
            {
                if (entry.Key.StartsWith("System.") && entry.Value != null)
                {
                    references.Add(entry.Value.ToString());
                }
            }
            if (references.Count == 0)
            {

                return new string[] {
				@"Mscorlib.dll",
				@"System.dll",
				@"System.Data.dll",
				@"System.Drawing.dll",
				@"System.Windows.Forms.dll",
				@"System.Xml.dll",
				@"System.Web.dll",
				@"System.Text.dll",
			    @"Microsoft.Vsa.dll"};
            }

            references.Add(@"Mscorlib.dll");
            references.Add(@"Microsoft.Vsa.dll");

            return references.ToArray();
        }
    }

    ///// <summary>
    ///// Summary description for Config.
    ///// </summary>
    //public static class Config
    //{
    //    private static string _ScriptLibrary;
    //    private static string _AppTempPath;
    //    private static string _AppDllPath;



    //    //public static string GetConnectionString()
    //    //{
    //    //    return GetRegistryValue("ConnectionString","");
    //    //}

    //    public static string ScriptLibrary
    //    {
    //        get
    //        {
    //            if (_ScriptLibrary == null)
    //            {
    //                _ScriptLibrary = GetRegistryValue("ScriptLibrary", "");
    //            }
    //            return _ScriptLibrary;
    //        }
    //    }

    //    //public static string GetDataAccessReference()
    //    //{
    //    //    return GetRegistryValue("DataAccessReference",@"Nistec.DataAccess.dll");
    //    //}

    //    //public static string GetAppScriptPath()
    //    //{
    //    //    return GetRegistryValue("AppScriptPath","");
    //    //}

    //    public static string GetAppDllPath
    //    {
    //        get
    //        {
    //            if (_AppDllPath == null)
    //            {
    //                _AppDllPath = GetRegistryValue("AppDllPath", "");
    //            }
    //            return _AppDllPath;
    //        }
    //    }

    //    public static string AppTempPath
    //    {
    //          get
    //        {
    //            if (_AppTempPath == null)
    //            {
    //                _AppTempPath = GetRegistryValue("AppTempPath", "");
    //            }
    //            return _AppTempPath;
    //        }
    //    }

    //    public static string GetRegistryValue(string key,string defaultValue)
    //    {
    //        Microsoft.Win32.RegistryKey rg=Microsoft.Win32.Registry.LocalMachine;
    //        rg=rg.OpenSubKey(@"Software\Nistec\ScriptBuilder");
    //        return rg.GetValue(key,defaultValue).ToString();
    //    }

    //}
}
