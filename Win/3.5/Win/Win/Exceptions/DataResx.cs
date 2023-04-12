using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace MControl.Win
{
  public sealed class DataResx
{
   
    private static object s_InternalSyncObject;
    private ResourceManager resources;
    private static DataResx loader;

    // Methods
    internal DataResx()
    {
        this.resources = new ResourceManager("SR_Data", base.GetType().Assembly);
    }

    private static DataResx GetLoader()
    {
        if (loader == null)
        {
            lock (InternalSyncObject)
            {
                if (loader == null)
                {
                    loader = new DataResx();
                }
            }
        }
        return loader;
    }

    public static object GetObject(string name)
    {
        DataResx loader = GetLoader();
        if (loader == null)
        {
            return null;
        }
        return loader.resources.GetObject(name, Culture);
    }

    public static string GetString(string name)
    {
        DataResx loader = GetLoader();
        if (loader == null)
        {
            return null;
        }
        return loader.resources.GetString(name, Culture);
    }

    public static string GetString(string name, params object[] args)
    {
        DataResx loader = GetLoader();
        if (loader == null)
        {
            return null;
        }
        string format = loader.resources.GetString(name, Culture);
        if ((args == null) || (args.Length <= 0))
        {
            return format;
        }
        for (int i = 0; i < args.Length; i++)
        {
            string text = args[i] as string;
            if ((text != null) && (text.Length > 0x400))
            {
                args[i] = text.Substring(0, 0x3fd) + "...";
            }
        }
        return string.Format(CultureInfo.CurrentCulture, format, args);
    }

    // Properties
    private static CultureInfo Culture
    {
        get
        {
            return null;
        }
    }

    private static object InternalSyncObject
    {
        get
        {
            if (s_InternalSyncObject == null)
            {
                object obj2 = new object();
                Interlocked.CompareExchange(ref s_InternalSyncObject, obj2, null);
            }
            return s_InternalSyncObject;
        }
    }

    public static ResourceManager Resources
    {
        get
        {
            return GetLoader().resources;
        }
    }
}

 


}
