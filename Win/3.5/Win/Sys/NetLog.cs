using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Principal;
using System.IO;
using System.Threading;

namespace MControl.Sys
{
    /// <summary>
    /// General logging module.
    /// Log path is from config file "NetlogPath" key;
    /// </summary>
    public class Netlog : IDisposable
    {
        public enum LogType
        {
            INFO,
            DEBUG,
            WARN,
            ERROR
        }

        #region static


        public static void WriteInfo(string text)
        {
            WriteLine("INFO", text);
        }
        public static void WriteDebug(string text)
        {
            WriteLine("DEBUG", text);
        }
        public static void WriteWarn(string text)
        {
            WriteLine("WARN", text);
        }
        public static void WriteError(string text)
        {
            WriteLine("ERROR", text);
        }

        public static void WriteInfoFormat(string text, params object[] args)
        {
            WriteLine("INFO", string.Format(text, args));
        }

        public static void WriteDebugFormat(string text, params object[] args)
        {
            WriteLine("DEBUG", string.Format(text, args));
        }
        public static void WriteWarnFormat(string text, params object[] args)
        {
            WriteLine("WARN", string.Format(text, args));
        }

        public static void WriteErrorFormat(string text, params object[] args)
        {
            WriteLine("ERROR", string.Format(text, args));
        }

        public static void WriteError(string text, Exception ex, Exception innerException)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(text);
            sb.Append(ex.Message);
            Exception exm = innerException;
            while (exm != null)
            {
                sb.Append(exm.Message);
                exm = exm.InnerException;
            }


            //string s = string.Format("{0}-[{1}]-{2}:", DateTime.Now, Thread.CurrentThread.Name, "ERROR") + sb.ToString();
            WriteLine("ERROR", sb.ToString());
        }

        public static void WriteException(string message, Exception e, bool addStackTrace)
        {
            //if (LogMode <= 0) return;

            StringBuilder sb = new StringBuilder();
            sb.Append(message);
            if (e != null)
            {
                sb.Append(e.Message);
                Exception innerEx = e.InnerException;
                while (innerEx != null)
                {
                    sb.Append(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
                if (addStackTrace)
                {
                    sb.AppendLine();
                    sb.AppendFormat("StackTrace:", e.StackTrace);
                }
            }
            WriteLine("ERROR", sb.ToString());
        }

        /// Writes specified text to log file.
        /// </summary>
        /// <param name="logType">Log file name.</param>
        /// <param name="text">Log text.</param>
        static void WriteLine(string logType, string text)
        {
            try
            {
                string log = string.Format("{0}-[{1}]-{2}:", DateTime.Now, Thread.CurrentThread.Name, logType) + text;

                string fileName = GetPathFix();

                // If there isn't such directory, create it.
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }

                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    StreamWriter w = new StreamWriter(fs);     // create a Char writer 
                    w.BaseStream.Seek(0, SeekOrigin.End);      // set the file pointer to the end
                    w.Write(log + "\r\n");
                    w.Flush();  // update underlying file
                    //w.Close();
                }
            }
            catch
            {
            }
        }

        static string GetPathFix()
        {
            string path = Configuration.NetConfig.ToString("NetlogPath");
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "Logs");

            }
            path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            return path + "\\Netlog" + DateTime.Today.ToString("yyyyMMdd") + ".log";

        }
        #endregion

        #region Logger instance

        static Netlog logger;
        
        public static Netlog Log
        {
            get
            {
                if (logger == null)
                {
                    logger = new Netlog();
                }
                return logger;
            }
        }
        #endregion

        #region methods

        public void Info(string text)
        {
            if (EnableInfo)
                WriteLog("INFO", text);
        }
        public void Debug(string text)
        {
            if (EnableDebug)
                WriteLog("DEBUG", text);
        }
        public void Warn(string text)
        {
            if (EnableWarn)
                WriteLog("WARN", text);
        }

        public void Error(string text)
        {
            if (EnableError)
                WriteLog("ERROR", text);
        }

        public void InfoFormat(string text, params object[] args)
        {
            if (EnableInfo)
                WriteLog("INFO", string.Format(text, args));
        }
        public void DebugFormat(string text, params object[] args)
        {
            if (EnableDebug)
                WriteLog("DEBUG", string.Format(text, args));
        }
        public void WarnFormat(string text, params object[] args)
        {
            if (EnableWarn)
                WriteLog("WARN", string.Format(text, args));
        }

        public void ErrorFormat(string text, params object[] args)
        {
            if (EnableError)
                WriteLog("ERROR", string.Format(text, args));
        }

        public void Error(string text, Exception ex, Exception innerException)
        {
            if (!EnableError)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append(text);
            sb.Append(ex.Message);
            Exception exm = innerException;
            while (exm != null)
            {
                sb.Append(exm.Message);
                exm = exm.InnerException;
            }


            //string s = string.Format("{0}-[{1}]-{2}:", DateTime.Now, Thread.CurrentThread.Name, "ERROR") + sb.ToString();
            WriteLog("ERROR", sb.ToString());
        }

        public void Exception(string message, Exception e, bool addStackTrace)
        {
            if (!EnableError)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(message);
            if (e != null)
            {
                sb.Append(e.Message);
                Exception innerEx = e.InnerException;
                while (innerEx != null)
                {
                    sb.Append(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
                if (addStackTrace)
                {
                    sb.AppendLine();
                    sb.AppendFormat("StackTrace:", e.StackTrace);
                }
            }
            WriteLog("ERROR", sb.ToString());
        }
        #endregion

        #region writer
        /// <summary>
        /// Writes specified text to log file.
        /// </summary>
        /// <param name="logType">Log type.</param>
        /// <param name="text">Log text.</param>
        private void WriteLog(string logType, string text)
        {
            try
            {
                string log = string.Format("{0}-[{1}]-{2}:", DateTime.Now, Thread.CurrentThread.Name, logType) + text;
 
                //string fileName = PathFix();

                string fileName = m_path + "\\" + m_logname + DateTime.Today.ToString("yyyyMMdd") + ".log";


                // If there isn't such directory, create it.
                //if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                //{
                //    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                //}

                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write,  FileShare.ReadWrite))
                {
                    StreamWriter w = new StreamWriter(fs);     // create a Char writer 
                    w.BaseStream.Seek(0, SeekOrigin.End);      // set the file pointer to the end
                    w.Write(log + "\r\n");
                    w.Flush();  // update underlying file
                }
            }
            catch
            {
            }
        }

        ///// <summary>
        ///// Fixes path separator, replaces / \ with platform separator char.
        ///// </summary>
        ///// <returns></returns>
        //string PathFix()//string path)
        //{
        //    //if (string.IsNullOrEmpty(m_path))
        //    //{
        //    //    m_path = Configuration.NetConfig.ToString("NetlogPath");

        //    //    if (string.IsNullOrEmpty(m_path))
        //    //    {
        //    //        m_path =Path.Combine( Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) , "Logs");
        //    //    }
        //    //    //path = Directory.GetCurrentDirectory();
        //    //}

        //    //string logName = SysNet.GetExecutingAssemblyName();

        //    return m_path + "\\" + m_logname + DateTime.Today.ToString("yyyyMMdd") + ".log";

        //    // return path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
        //}

        /// <summary>
        /// Fixes path separator, replaces / \ with platform separator char.
        /// </summary>
        void PathFix()
        {
            if (string.IsNullOrEmpty(m_path))
            {
                m_path = LogPath;

                if (string.IsNullOrEmpty(m_path))
                {
                    m_path = Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "Logs");
                }
                //path = Directory.GetCurrentDirectory();
            }

            m_path = m_path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            if (string.IsNullOrEmpty(m_logname))
            {
                this.m_logname = "Netlog";
            }

            if (!Directory.Exists(m_path))
            {
                Directory.CreateDirectory(m_path);
            }
        }

        #endregion

        #region members
        string m_logname = "Netlog";
        string m_path;
        bool m_EnableAll = true;
        bool m_EnableDebug = true;
        bool m_EnableInfo = true;
        bool m_EnableWarn = true;
        bool m_EnableError = true;
        #endregion

        #region properties
        /// <summary>
        /// Get log path from "NetlogPath" config
        /// </summary>
        public string LogPath
        {
            get { return m_path; }
        }
        /// <summary>
        /// Get log path from "NetlogName" config
        /// </summary>
        public string LogName
        {
            get { return m_logname; }
        }
        /// <summary>
        /// Get indicate if log enabled from "NetlogEnable" config
        /// </summary>
        public bool EnableAll
        {
            get { return m_EnableAll; }
        }
        /// <summary>
        /// Get indicate if debug log enabled from "NetlogEnableDebug" config
        /// </summary>
        public bool EnableDebug
        {
            get { return EnableAll && m_EnableDebug; }
        }
        /// <summary>
        /// Get indicate if info log enabled fom "NetlogEnableInfo" config
        /// </summary>
        public bool EnableInfo
        {
            get { return EnableAll && m_EnableInfo; }
        }
        /// <summary>
        /// Get indicate if warn log enabled from "NetlogEnableWarn" config
        /// </summary>
        public bool EnableWarn
        {
            get { return EnableAll && m_EnableWarn; }
        }
        /// <summary>
        /// Get indicate if error log enabled from "NetlogEnableError" config
        /// </summary>
        public bool EnableError
        {
            get { return EnableAll && m_EnableError; }
        }
        #endregion

        #region ctor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Netlog():this(null,null)
        {
        }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Netlog(string logName):this(logName,null)
        {
  
        }
        public Netlog(string logName, string path)
        {
            this.m_logname = logName;
            this.m_path = path;
            PathFix();

            m_EnableAll = Configuration.NetConfig.Get<bool>("NetlogEnable", true);
            m_EnableDebug = Configuration.NetConfig.Get<bool>("NetlogEnableDebug", true);
            m_EnableInfo = Configuration.NetConfig.Get<bool>("NetlogEnableInfo", true);
            m_EnableWarn = Configuration.NetConfig.Get<bool>("NetlogEnableWarn", true);
            m_EnableError = Configuration.NetConfig.Get<bool>("NetlogEnableError", true);

        }
        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        public void Dispose()
        {
            m_path = null;
            m_logname = null;
        }
        #endregion
    }

}