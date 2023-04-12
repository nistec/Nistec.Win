using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Collections;

namespace Nistec.Printing.View
{
    public class ResexLibrary
    {
        private static Hashtable resourceManagers = new Hashtable();

        #region Methods


        public static string GetPaperSize(string key)
        {
            return GetString(key, "PaperSize");
        }

        public static string GetPaperWidth(string key)
        {
            return GetString(key, "PaperWidth");
        }

        public static string GetPaperHeight(string key)
        {
            return GetString(key, "PaperHeight");
        }

        public static string GetString(string key, string modname)
        {
            try
            {
                ResourceManager rm = (ResourceManager)resourceManagers[modname];
                if (rm == null)
                {
                    rm = new ResourceManager("Nistec.Printing.View.Resources."
                        + modname,
                        Assembly.GetExecutingAssembly());

                    resourceManagers.Add(modname, rm);
                }

                return rm.GetString(key);
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                return "";
            }
        }

        #endregion
    }
}
