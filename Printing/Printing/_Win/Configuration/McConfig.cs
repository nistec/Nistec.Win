using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using MControl.Xml;
using MControl;
using MControl.Win;
using MControl.Runtime;

namespace MControl.Configuration
{
    //public class ConfigTest : McConfig
    //{
    //    public ConfigTest()
    //        : base()
    //    {
    //        base.Password = "";
    //    }
    //}

    /// <summary>
    /// Represent a Config file as Dictionary key-value
    /// <example>
    /// <sppSttings>
    /// <myname value='nissim' />
    /// <mycompany value='mcontrol' />
    /// </sppSttings>
    /// </example>
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class McConfig
    {
        public const string DefaultPassword = "giykse876435365&%$^#%@$#@)_(),kxa;l bttsklf12[]}{}{)(*XCJHG^%%";

        string filename;
        //string conigRoot = "configSections";
        string conigAppSettings = "appSettings";
        IDictionary<string,object> dictionary;
        string password;
        bool encrypted = false;
        bool auteSave = false;

        public event PropertyItemChangedEventHandler ItemChanged;
        public event EventHandler ConfigFileChanged;
        public event ErrorOcurredEventHandler ErrorOcurred;

        protected virtual void OnErrorOcurred(ErrorOcurredEventArgs e)
        {
            if (ErrorOcurred != null)
            {
                ErrorOcurred(this,e);
            }
        }

        protected virtual void OnItemChanged(string key,object value)
        {
            OnItemChanged(new PropertyItemChangedEventArgs(key,value)); 
        }
        
        protected virtual void OnItemChanged(PropertyItemChangedEventArgs e)
        {
            if (auteSave)
            {
                Save();
            }
            if (ItemChanged != null)
                ItemChanged(this, e);
        }

        protected virtual void OnConfigFileChanged(EventArgs e)
        {
            ConfigToDictionary();

            if (ConfigFileChanged != null)
                ConfigFileChanged(this, e);
        }


        
        /// <summary>
        /// McConfig ctor with a specefied filename
        /// </summary>
        /// <param name="filename"></param>
        public McConfig(string filename)
        {
            dictionary = new Dictionary<string, object>();
            this.filename = filename;
        }

        /// <summary>
        /// McConfig ctor with a specefied Dictionary
        /// </summary>
        /// <param name="dict"></param>
        public McConfig(IDictionary<string, object> dict)
        {
            dictionary = dict;
        }

        /// <summary>
        /// McConfig ctor with default filename CallingAssembly '.mconfig' in current direcory
        /// </summary>
        public McConfig()
        {
            //MethodInfo method = (System.Reflection.MethodInfo)(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            Assembly assm = Assembly.GetCallingAssembly();
            //string name= assm.GetName().Name;
            string location = assm.Location;

            //string filename = location + "." +".mconfig";
            dictionary = new Dictionary<string, object>();
            this.filename = location + ".mconfig"; ;

        }

        #region watcher

        FileSystemWatcher watcher;

        /// <summary>
        /// Initilaize the file system watcher
        /// </summary>
        public void InitWatcher()
        {
            string fpath = Path.GetDirectoryName(filename);
            string filter = Path.GetExtension(filename);

             // Create a new FileSystemWatcher and set its properties.
            watcher = new FileSystemWatcher();
            watcher.Path = fpath;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter ="*"+ filter;// "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(WatchFile_Changed);
            watcher.Created += new FileSystemEventHandler(WatchFile_Changed);
            watcher.Deleted += new FileSystemEventHandler(WatchFile_Changed);
            watcher.Renamed += new RenamedEventHandler(WatchFile_Renamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        /// <summary>
        /// Occoured when file changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileChanged(FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            if (e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType == WatcherChangeTypes.Created)
            {

                OnConfigFileChanged(EventArgs.Empty);//  ConfigToDictionary();
            }
        }
        /// <summary>
        /// Occoured when file renamed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileRenamed(RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
            if (e.FullPath == Filename)
            {
                OnConfigFileChanged(EventArgs.Empty);// ConfigToDictionary();
            }
        }


        void WatchFile_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == Filename)
            {
                OnFileChanged(e);
            }
        }
        void WatchFile_Renamed(object sender, RenamedEventArgs e)
        {
            if (e.OldFullPath ==Filename || e.FullPath==Filename)
            {
                OnFileRenamed(e);
            }
        }
        #endregion

        #region properties

        /// <summary>
        /// Get string value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetItem(string key)
        {
            object o = this[key];
            if (o==null)
            {
                return "";
            }
            return o.ToString();
        }


        /// <summary>
        /// Get or Set value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get 
            {
                if (!dictionary.ContainsKey(key))
                {
                    return null;
                }
                return dictionary[key]; 
            }

            set
            {
                if (dictionary.ContainsKey(key))
                {
                    if (dictionary[key] != value)
                    {
                        dictionary[key] = value;
                        OnItemChanged(key, value);
                    }
                }
                else
                {
                    dictionary[key] = value;
                    OnItemChanged(key, value);
                }

            }
        }

        /// <summary>
        /// Get or Set value indicating is McConfig will save changes to config file when each item value has changed 
        /// </summary>
        public bool AutoSave
        {
            get
            {
                return auteSave;
            }
            set
            {
                auteSave = value;
            }
        }

        /// <summary>
        /// Get or Set The config full file path
        /// </summary>
        public string Filename
        {
            get 
            {
                if (string.IsNullOrEmpty(filename))
                {
                    filename =Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)+ "\\McConfig.mcconfig";
                }
                return filename; 
            }
            set
            {
                filename = value;
            }
        }
        /// <summary>
        /// Get value indicating if the config file exists
        /// </summary>
        public bool FileExists
        {
            get
            {
                return File.Exists(Filename);
            }
        }
        /// <summary>
        /// Get or Set The Config root tag
        /// </summary>
        public string AppSettingsTag
        {
            get { return conigAppSettings; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                conigAppSettings = value;
            }
        }
        /// <summary>
        /// Get or Set the password for encryption
        /// </summary>
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                encrypted = !string.IsNullOrEmpty(password);
            }
        }
        /// <summary>
        /// Get all items as IDictionary
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> ToDictionary()
        {
            return dictionary;
        }

        #endregion

        /// <summary>
        /// Read Config File
        /// </summary>
        public void Read()
        {
            try
            {
                ConfigToDictionary();
            }
            catch (Exception ex)
            {
                OnErrorOcurred(new ErrorOcurredEventArgs("Error occured when try to read Config To Dictionary, Error: " +ex.Message));
            }
        }

        /// <summary>
        /// Save Changes 
        /// </summary>
        public void Save()
        {
            try
            {
                DictionaryToConfig();
            }
            catch (Exception ex)
            {
                         OnErrorOcurred(new ErrorOcurredEventArgs("Error occured when try to save Dictionary To Config, Error: " +ex.Message));
            }
        }
        /// <summary>
        /// Print all item to Console
        /// </summary>
        public void PrintConfig()
        {
            Console.WriteLine("<" + conigAppSettings + ">");

            foreach (KeyValuePair<string, object> entry in dictionary)
            {
                Console.WriteLine("key={0}, value={1}, Type={2}", entry.Key , entry.Value,entry.Value==null? "String": entry.Value.GetType().ToString());
            }
            Console.WriteLine("</" + conigAppSettings + ">");

        }
   
    
        /// <summary>
        /// Init new config file from dictionary
        /// </summary>
        /// <param name="dict"></param>
        public void Init()
        {
            XmlBuilder builder = new XmlBuilder();
            builder.AppendXmlDeclaration();
            builder.AppendEmptyElement(conigAppSettings, 0);
            foreach (KeyValuePair<string,object> entry in dictionary)
            {
                if (entry.Value == null)
                {
                    //builder.AppendElementAttributes(0, entry.Key, "", "value", string.Empty, "type", "String");
                    builder.AppendElementAttributes(0, "Add", "", new string[]{"key", entry.Key,"value", string.Empty, "type", "String"});
                }
                else
                {
                    //builder.AppendElementAttributes(0, entry.Key, "", "value", entry.Value.ToString(), "type", entry.Value.GetType().ToString());
                    builder.AppendElementAttributes(0, "Add", "", new string[] { "key", entry.Key, "value", entry.Value.ToString(), "type", entry.Value.GetType().ToString() });
                }
            }

            if (encrypted)
                EncryptFile(builder.Document.OuterXml);
            else
                builder.Document.Save(Filename);
        }

        private void ConfigToDictionary()
        {

            Dictionary<string,object> dict=new Dictionary<string,object>();

            XmlDocument doc = new XmlDocument();

            if (encrypted)
                doc.LoadXml(DecryptFile());
            else
                doc.Load(Filename);

            Console.WriteLine("Load Config: " + Filename);
            //XmlParser parser=new XmlParser(filename);

            XmlNode app = doc.SelectSingleNode("//" + conigAppSettings);
            XmlNodeList list = app.ChildNodes;

            for (int i = 0; i < list.Count; i++)
            {
                XmlNode node = list[i];
                //dict[node.Name] = node.Attributes["value"].Value;
                //dict[node.Name] =GetValue( node);
                dict[node.Attributes["key"].Value] = GetValue(node);
            }
            dictionary = dict;
        }

        private object GetValue(XmlNode node)
        {
            string value = node.Attributes["value"].Value;
            string type ="string";
            XmlAttribute attrib= node.Attributes["type"];

            if (attrib == null)
            {
                return value;
            }

            type = attrib.Value;
 
            if (type.ToLower().EndsWith("string"))
            {
                return value;
            }

            if (type.ToLower().Contains("int"))
            {
                return Types.ToInt(value,(int)0);
            }

            if (type.ToLower().Contains("date"))
            {
                return Types.ToDateTime(value, DateTime.Now);
            }

            if (type.ToLower().Contains("bool"))
            {
                return Types.ToBool(value, false);
            }

            if (type.ToLower().EndsWith("decimal"))
            {
                return Types.ToDecimal(value, 0.0m);
            }

            if (type.ToLower().EndsWith("float"))
            {
                return Types.ToFloat(value, 0.0F);
            }

            if (type.ToLower().EndsWith("double"))
            {
                return Types.ToDouble(value, 0.0D);
            }
            if (type.ToLower().EndsWith("byte"))
            {
                return Types.ToByte(value, 0);
            } 
            
            return value;
        }

        private void ValidateConfig()
        {
            if (!FileExists && dictionary.Count > 0)
            {
                Init();
            }
        }

        private void DictionaryToConfig()
        {
            ValidateConfig();

            XmlDocument doc = new XmlDocument();
            if (encrypted)
                doc.LoadXml(DecryptFile());
            else
                doc.Load(Filename);

            XmlNode app = doc.SelectSingleNode("//" + conigAppSettings);
            XmlNodeList list = app.ChildNodes;

            for (int i = 0; i < list.Count; i++)
            {
                XmlNode node = list[i];
                //dict[node.Name] = node.Attributes["value"].Value;
                //dict[node.Name] =GetValue( node);
               
                string key=node.Attributes["key"].Value;
                object value=dictionary[key];
                //node.Attributes["key"].Value= key ;

                 if (value != null)
                {
                  node.Attributes["value"].Value =value.ToString();
                  node.Attributes["type"].Value = value.GetType().ToString();
                }
                else
                {
                    node.Attributes["value"].Value = string.Empty;
                    node.Attributes["type"].Value ="String";
                }
            }

            //foreach (KeyValuePair<string, object> entry in dictionary)
            //{

            //    XmlNode node = app.SelectSingleNode(entry.Key);
            //    if (entry.Value != null)
            //    {
            //        node.Attributes["value"].Value = entry.Value.ToString();
            //        node.Attributes["type"].Value = entry.Value.GetType().ToString();
            //    }
            //    else
            //    {
            //        node.Attributes["value"].Value = string.Empty;
            //        node.Attributes["type"].Value ="String";
            //    }
            //}

            if (encrypted)
            {
                EncryptFile(doc.OuterXml);
            }
            else
                doc.Save(Filename);
        }

       

        private string DecryptFile()
        {
            Encryption en = new Encryption(password);
            return en.DecryptFileToString(Filename, true);
        }

        private bool EncryptFile(string ouput)
        {
            Encryption en = new Encryption(password);
            return en.EncryptStringToFile(ouput, Filename, true);
        }

     
    }
}
