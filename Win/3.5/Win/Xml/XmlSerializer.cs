using System;
using System.Xml.Serialization;	 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;				 
using System.ComponentModel;	 
using System.IO.IsolatedStorage; 
using System.Text;
using System.Xml;

namespace MControl.Xml
{


    public enum SerializedFormatType
    {
        Binary, Document
    }

    /*
    public static class XmlTools
    {

        public static string ToXmlString<T>(this T input)
        {
            using (var writer = new StringWriter())
            {
                input.ToXml(writer); return writer.ToString();
            }
        }
        public static void ToXml<T>(this T objectToSerialize, Stream stream)
        {
            new XmlSerializer(typeof(T)).Serialize(stream, objectToSerialize);
        }

        public static void ToXml<T>(this T objectToSerialize, StringWriter writer)
        {
            new XmlSerializer(typeof(T)).Serialize(writer, objectToSerialize);
        } 

    }
    */

    /// <summary>
    /// Custom class used as a wrapper to the Xml serialization of an object to/from an Xml file.
    /// </summary>
    public static class Serializer
    {

        #region static Serialize old
        /*

        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="ns">namespace</param>
        /// <returns></returns>
        public static string Serialize(object obj,string ns)
        {
            Type type = obj.GetType();
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(type,ns);
                serializer.Serialize(writer, type);
                writer.Flush();
                return writer.ToString();
            }
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string Serialize(object body)
        {
            return Serialize(body, body.GetType());
        }

       
        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Serialize(object body, Type type)
        {
            string result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, Encoding.Default);
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = Encoding.Default.GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
         */
 
        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Serialize(object body, Type type, string encode)
        {
            string result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, Encoding.GetEncoding(encode));
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = Encoding.GetEncoding(encode).GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        
        #endregion

        #region generic Serialize

        /// <summary>
        /// Serialize To Xml using XmlSerializer with StringWriter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string result = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter ms = new StringWriter())
            {
                using (XmlWriter writer = new XmlTextWriter(ms))//, Encoding.UTF8);
                {
                    serializer.Serialize(writer, obj);
                    result = ms.ToString();
                }
            }
            return result;

            //return Serialize<T>(obj, (string)null);
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer with StringWriter and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj, string nameSpace)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            string result = null;

            Type type = typeof(T);

            XmlSerializer serializer = new XmlSerializer(type, nameSpace);

            using (StringWriter ms = new StringWriter())
            {
                using (XmlWriter writer = new XmlTextWriter(ms))//, Encoding.UTF8);
                {
                    serializer.Serialize(writer, obj);
                    result = ms.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer with StringWriter and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="nameSpace"></param>
        /// <param name="enableXMLSchema"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj, string nameSpace, bool enableXMLSchema)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string result = null;

            Type type = typeof(T);

            XmlSerializer serializer = new XmlSerializer(type, nameSpace);

            using (StringWriter ms = new StringWriter())
            {
                using (XmlWriter writer = new XmlTextWriter(ms))//, Encoding.UTF8);
                {
                    serializer.Serialize(writer, obj, XmlFormatter.GetNamespaces(nameSpace, enableXMLSchema));
                    result = ms.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer with StringWriter and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="nameSpace"></param>
        /// <param name="enableXMLSchema"></param>
        /// <param name="replaceDocumentElement"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, string nameSpace, bool enableXMLSchema, bool replaceDocumentElement)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string result = null;

            Type type = typeof(T);

            XmlSerializer serializer = new XmlSerializer(type, nameSpace);

            using (StringWriter ms = new StringWriter())
            {
                using (XmlWriter writer = new XmlTextWriter(ms))//, Encoding.UTF8);
                {
                    serializer.Serialize(writer, obj, XmlFormatter.GetNamespaces(nameSpace, enableXMLSchema));
                    result = ms.ToString();
                }
            }

            if (replaceDocumentElement)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string docElement = doc.DocumentElement.Name;
                result = result.Replace(docElement, type.Name);
            }

            return result;
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static void Serialize<T>(T obj, XmlWriter writer)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            Type type = typeof(T);
            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(writer, obj);
        }

        /// <summary>
        /// Serialize To Xml using XmlSerializer with MemoryStream and specific encode and namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="encode"></param>
        /// <param name="nameSpace"></param>
        /// <param name="enableXMLSchema"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, string encode, string nameSpace, bool enableXMLSchema)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string result = null;

            Type type = typeof(T);

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type, nameSpace);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, Encoding.GetEncoding(encode));
                serializer.Serialize(writer, obj, XmlFormatter.GetNamespaces(nameSpace, enableXMLSchema));
                byte[] byteArray = ms.GetBuffer();
                result = Encoding.GetEncoding(encode).GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        #endregion


        #region static Deserialize


        ///// <summary>
        ///// Deserialize from Xml using XmlSerializer and XmlRootAttribute
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="xmlString"></param>
        ///// <returns></returns>
        //public static T Deserialize<T>(string xmlString)
        //{
        //    Type type = typeof(T);
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(XmlFormatter.RemoveXmlDeclaration(xmlString.Trim()));

        //    XmlRootAttribute rootAttribute = new XmlRootAttribute(doc.DocumentElement.Name);
        //    rootAttribute.Namespace = doc.DocumentElement.NamespaceURI;

        //    XmlSerializer ser = new XmlSerializer(type, rootAttribute);

        //    return (T)ser.Deserialize(new XmlNodeReader(doc.DocumentElement));
        //}

        /// <summary>
        /// Deserialize from Xml using XmlSerializer and XmlDocument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Deserialize<T>(XmlReader reader)
        {
            T rVal = default(T);

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            rVal = (T)serializer.Deserialize(reader);

            return rVal;
        }

        /// <summary>
        /// Deserialize from Xml using XmlSerializer with StringReader and XmlDocument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString)
        {
            return Deserialize<T>(xmlString, null, false);
        }

        /// <summary>
        /// Deserialize from Xml using XmlSerializer with StringReader and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="Namespace">Namespace or null</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString, string Namespace)
        {
            return Deserialize<T>(xmlString, Namespace, false);
        }

        /// <summary>
        /// Deserialize from Xml using XmlSerializer with StringReader and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="Namespace">Namespace or null</param>
        /// <param name="removeXmlDeclaration">should remove XmlDeclaration</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString, /*string ElementName,*/ string Namespace, bool removeXmlDeclaration = false)
        {

            T rVal = default(T);
            if (removeXmlDeclaration)
                xmlString = XmlFormatter.RemoveXmlDeclaration(xmlString.Trim());

            XmlSerializer serializer = new XmlSerializer(typeof(T), (string)Namespace);// xRoot);
            using (StringReader ms = new StringReader(xmlString))
            {
                XmlReader reader = new XmlTextReader(ms);
                rVal = (T)serializer.Deserialize(reader);
            }

            return rVal;

        }


        /// <summary>
        /// Deserialize from Xml using XmlSerializer with specific encoding and namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="Namespace">Namespace or null</param>
        /// <param name="removeXmlDeclaration">should remove XmlDeclaration</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString, string encode, string Namespace, bool removeXmlDeclaration)
        {
            T rVal = default(T);
            if (removeXmlDeclaration)
                xmlString = XmlFormatter.RemoveXmlDeclaration(xmlString.Trim());
            //XmlRootAttribute xRoot = new XmlRootAttribute();
            //xRoot.ElementName = ElementName;
            //xRoot.Namespace = Namespace;
            //xRoot.IsNullable = false;

            XmlSerializer serializer = new XmlSerializer(typeof(T), (string)Namespace);// xRoot);
            byte[] bytes = Encoding.GetEncoding(encode).GetBytes(xmlString);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlReader reader = new XmlTextReader(ms);
                rVal = (T)serializer.Deserialize(reader);
            }

            return rVal;

            //Type type=typeof(T);
            //XmlSerializer serializer = new XmlSerializer(type,ns);
            //using (StringReader ms = new StringReader(xmlString))
            //{
            //    XmlReader reader = new XmlTextReader(ms);
            //   return(T) serializer.Deserialize(reader);
            //}
        }

        /// <summary>
        /// Deserialize from Xml using XmlSerializer
        /// </summary>
        /// <param name="smlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(string smlString, Type type)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = Encoding.Default.GetBytes(smlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Deserialize from Xml using XmlSerializer
        /// </summary>
        /// <param name="smlString"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static object Deserialize(string smlString, Type type, string encode)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = Encoding.GetEncoding(encode).GetBytes(smlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
        #endregion

    }

    public class DocumentSerializer
    {
        #region Serializer class

        /// <summary>
        /// Constructor for this class.
        /// </summary>
        public DocumentSerializer()
        {
        }

        /// <summary>
        /// Load an object from an Xml file that is in an Xml Document format.
        /// <newpara></newpara>
        /// <example>
        /// See Load method that uses the SerializedFormatType argument for more information.
        /// </example>
        /// </summary>
        public virtual Object Load(Object obj, string XmlFilePathName)
        {
            obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, null);
            return obj;
        }

        /// <summary>
        /// Load an object from an Xml file that is in the specified format.
        /// <newpara></newpara>
        /// </summary>
        /// <param name="obj">Object to be loaded.</param>
        /// <param name="XmlFilePathName">File Path name of the Xml file containing object(s) serialized to Xml.</param>
        /// <param name="SerializedFormat">Xml serialized format to load the object from.</param>
        /// <returns>Returns an Object loaded from the Xml file. If the Object could not be loaded returns null.</returns>
        public virtual Object Load(Object obj, string XmlFilePathName, SerializedFormatType SerializedFormat)
        {
            switch (SerializedFormat)
            {
                case SerializedFormatType.Binary:
                    obj = this.LoadFromBinaryFormat(obj, XmlFilePathName, null);
                    break;

                case SerializedFormatType.Document:
                default:
                    obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, null);
                    break;
            }

            return obj;
        }

        public virtual Object Load(Object obj, string XmlFilePathName,
            SerializedFormatType SerializedFormat, IsolatedStorageFile isolatedStorageFolder)
        {
            switch (SerializedFormat)
            {
                case SerializedFormatType.Binary:
                    obj = this.LoadFromBinaryFormat(obj, XmlFilePathName, isolatedStorageFolder);
                    break;

                case SerializedFormatType.Document:
                default:
                    obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, isolatedStorageFolder);
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Load an object from an Xml file that is in an Xml Document format, at a Isolated storage location.
        /// </summary>
        /// <param name="obj">Object to be loaded.</param>
        /// <param name="XmlFilePathName">File name (no path) of the Xml file containing object(s) serialized to Xml.</param>
        /// <param name="isolatedStorageFolder">Isolated Storage object that is a user and assembly specific folder location
        /// from which to Load the Xml file.</param>
        /// <returns>Returns an Object loaded from the Xml file. If the Object could not be loaded returns null.</returns>
        public virtual Object Load(Object obj, string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, isolatedStorageFolder);
            return obj;
        }

        private Object LoadFromBinaryFormat(Object obj,
            string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            FileStream fileStream = null;

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (isolatedStorageFolder == null)
                    fileStream = new FileStream(XmlFilePathName, FileMode.Open);
                else
                    fileStream = new IsolatedStorageFileStream(XmlFilePathName, FileMode.Open, isolatedStorageFolder);

                obj = binaryFormatter.Deserialize(fileStream);
            }
            finally
            {
                //Make sure to close the file even if an exception is raised...
                if (fileStream != null)
                    fileStream.Close();
            }

            return obj;
        }

        private Object LoadFromDocumentFormat(Object obj,
            string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            TextReader txrTextReader = null;

            try
            {
                Type ObjectType = obj.GetType();

                XmlSerializer xserDocumentSerializer = new XmlSerializer(ObjectType);

                if (isolatedStorageFolder == null)
                    txrTextReader = new StreamReader(XmlFilePathName);
                else
                    txrTextReader = new StreamReader(new IsolatedStorageFileStream(XmlFilePathName, FileMode.Open, isolatedStorageFolder));

                obj = xserDocumentSerializer.Deserialize(txrTextReader);
            }
            finally
            {
                //Make sure to close the file even if an exception is raised...
                if (txrTextReader != null)
                    txrTextReader.Close();
            }

            return obj;
        }

        /// <summary>
        /// Save an object to an Xml file that is in an Xml Document format.
        /// <newpara></newpara>
        /// <example>
        /// See Save method that uses the SerializedFormatType argument for more information.
        /// </example>
        /// </summary>
        public virtual bool Save(Object ObjectToSave, string XmlFilePathName)
        {
            bool success = false;
            success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, null);
            return success;
        }

        /// <summary>
        /// Save an object to an Xml file that is in the specified format.
        /// <newpara></newpara>
        /// </summary>
        /// <param name="ObjectToSave">Object to be saved.</param>
        /// <param name="XmlFilePathName">File Path name of the Xml file to contain the object serialized to Xml.</param>
        /// <param name="SerializedFormat">Xml serialized format to load the object from.</param>
        /// <returns>Returns success of the object save.</returns>
        public virtual bool Save(Object ObjectToSave, string XmlFilePathName, SerializedFormatType SerializedFormat)
        {
            bool success = false;

            switch (SerializedFormat)
            {
                case SerializedFormatType.Binary:
                    success = this.SaveToBinaryFormat(ObjectToSave, XmlFilePathName, null);
                    break;

                case SerializedFormatType.Document:
                default:
                    success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, null);
                    break;
            }

            return success;
        }

        public virtual bool Save(Object ObjectToSave, string XmlFilePathName,
            SerializedFormatType SerializedFormat, IsolatedStorageFile isolatedStorageFolder)
        {
            bool success = false;

            switch (SerializedFormat)
            {
                case SerializedFormatType.Binary:
                    success = this.SaveToBinaryFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
                    break;

                case SerializedFormatType.Document:
                default:
                    success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
                    break;
            }

            return success;
        }


        /// <summary>
        /// Save an object to an Xml file that is in an Xml Document forward, at a Isolated storage location.
        /// </summary>
        /// <param name="ObjectToSave">Object to be saved.</param>
        /// <param name="XmlFilePathName">File name (no path) of the Xml file to contain the object serialized to Xml.</param>
        /// <param name="isolatedStorageFolder">Isolated Storage object that is a user and assembly specific folder location
        /// from which to save the Xml file.</param>
        /// <returns></returns>
        public virtual bool Save(Object ObjectToSave, string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            bool success = false;
            success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
            return success;
        }

        private bool SaveToDocumentFormat(Object ObjectToSave,
            string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            TextWriter textWriter = null;
            bool success = false;

            try
            {
                Type ObjectType = ObjectToSave.GetType();

                //Create serializer object using the type name of the Object to serialize.
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectType);

                if (isolatedStorageFolder == null)
                    textWriter = new StreamWriter(XmlFilePathName);
                else
                    textWriter = new StreamWriter(new IsolatedStorageFileStream(XmlFilePathName, FileMode.OpenOrCreate, isolatedStorageFolder));

                xmlSerializer.Serialize(textWriter, ObjectToSave);

                success = true;
            }
            finally
            {
                //Make sure to close the file even if an exception is raised...
                if (textWriter != null)
                    textWriter.Close();
            }

            return success;
        }

        private bool SaveToBinaryFormat(Object ObjectToSave,
            string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
        {
            FileStream fileStream = null;
            bool success = false;

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (isolatedStorageFolder == null)
                    fileStream = new FileStream(XmlFilePathName, FileMode.OpenOrCreate);
                else
                    fileStream = new IsolatedStorageFileStream(XmlFilePathName, FileMode.OpenOrCreate, isolatedStorageFolder);

                binaryFormatter.Serialize(fileStream, ObjectToSave);

                success = true;
            }
            finally
            {
                //Make sure to close the file even if an exception is raised...
                if (fileStream != null)
                    fileStream.Close();
            }

            return success;
        }
        #endregion

    }
#if(false)
	public enum SerializedFormatType
	{
		Binary, Document
	}	

	/// <summary>
	/// Custom class used as a wrapper to the Xml serialization of an object to/from an Xml file.
	/// </summary>
	public class Serializer
	{

        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string Serialize(object body)
        {
            return Serialize(body, body.GetType());
        }

        #region static

        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Serialize(object body, Type type)
        {
            string result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, Encoding.Default);
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = Encoding.Default.GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Serialize(object body, Type type, string encode)
        {
            string result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, Encoding.GetEncoding(encode));
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = Encoding.GetEncoding(encode).GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Deserialize from Xml using XmlSerializer
        /// </summary>
        /// <param name="smlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(string smlString, Type type)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = Encoding.Default.GetBytes(smlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Deserialize from Xml using XmlSerializer
        /// </summary>
        /// <param name="smlString"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static object Deserialize(string smlString, Type type, string encode)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = Encoding.GetEncoding(encode).GetBytes(smlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
        #endregion


        /// <summary>
		/// Constructor for this class.
		/// </summary>
		public Serializer()
		{
		}

		/// <summary>
		/// Load an object from an Xml file that is in an Xml Document format.
		/// <newpara></newpara>
		/// <example>
		/// See Load method that uses the SerializedFormatType argument for more information.
		/// </example>
		/// </summary>
		public virtual Object Load(Object obj, string XmlFilePathName)
		{   		
			obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, null);
			return obj;
		}

		/// <summary>
		/// Load an object from an Xml file that is in the specified format.
		/// <newpara></newpara>
		/// </summary>
		/// <param name="obj">Object to be loaded.</param>
		/// <param name="XmlFilePathName">File Path name of the Xml file containing object(s) serialized to Xml.</param>
		/// <param name="SerializedFormat">Xml serialized format to load the object from.</param>
		/// <returns>Returns an Object loaded from the Xml file. If the Object could not be loaded returns null.</returns>
		public virtual Object Load(Object obj, string XmlFilePathName, SerializedFormatType SerializedFormat)
		{   
			switch (SerializedFormat)
			{
				case SerializedFormatType.Binary :
					obj = this.LoadFromBinaryFormat(obj, XmlFilePathName, null);
					break;

				case SerializedFormatType.Document :
				default :
					obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, null);
					break; 
			}
		
			return obj;
		}

		public virtual Object Load(Object obj, string XmlFilePathName, 
			SerializedFormatType SerializedFormat, IsolatedStorageFile isolatedStorageFolder)
		{   
			switch (SerializedFormat)
			{
				case SerializedFormatType.Binary :
					obj = this.LoadFromBinaryFormat(obj, XmlFilePathName, isolatedStorageFolder);
					break;

				case SerializedFormatType.Document :
				default :
					obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, isolatedStorageFolder);
					break; 
			}
		
			return obj;
		}

		/// <summary>
		/// Load an object from an Xml file that is in an Xml Document format, at a Isolated storage location.
		/// </summary>
		/// <param name="obj">Object to be loaded.</param>
		/// <param name="XmlFilePathName">File name (no path) of the Xml file containing object(s) serialized to Xml.</param>
		/// <param name="isolatedStorageFolder">Isolated Storage object that is a user and assembly specific folder location
		/// from which to Load the Xml file.</param>
		/// <returns>Returns an Object loaded from the Xml file. If the Object could not be loaded returns null.</returns>
		public virtual Object Load(Object obj, string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{
			obj = this.LoadFromDocumentFormat(obj, XmlFilePathName, isolatedStorageFolder);
			return obj;
		}

		private Object LoadFromBinaryFormat(Object obj, 
			string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{   	
			FileStream fileStream = null;

			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();

				if (isolatedStorageFolder == null)
					fileStream = new FileStream(XmlFilePathName, FileMode.Open);
				else
					fileStream = new IsolatedStorageFileStream(XmlFilePathName, FileMode.Open, isolatedStorageFolder);

				obj = binaryFormatter.Deserialize(fileStream);
			}
			finally
			{
				//Make sure to close the file even if an exception is raised...
				if (fileStream != null)
					fileStream.Close();				
			}			

			return obj;
		}

		private Object LoadFromDocumentFormat(Object obj, 
			string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{   	
			TextReader txrTextReader = null;

			try
			{
				Type ObjectType = obj.GetType();

				XmlSerializer xserDocumentSerializer = new XmlSerializer(ObjectType);

				if (isolatedStorageFolder == null)
					txrTextReader = new StreamReader(XmlFilePathName);
				else
					txrTextReader = new StreamReader(new IsolatedStorageFileStream(XmlFilePathName, FileMode.Open, isolatedStorageFolder));

				obj = xserDocumentSerializer.Deserialize(txrTextReader);
			}
			finally
			{
				//Make sure to close the file even if an exception is raised...
				if (txrTextReader != null)
					txrTextReader.Close();				
			}			

			return obj;
		}

		/// <summary>
		/// Save an object to an Xml file that is in an Xml Document format.
		/// <newpara></newpara>
		/// <example>
		/// See Save method that uses the SerializedFormatType argument for more information.
		/// </example>
		/// </summary>
		public virtual bool Save(Object ObjectToSave, string XmlFilePathName)
		{
			bool success = false;
			success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, null);			
			return success;
		}

		/// <summary>
		/// Save an object to an Xml file that is in the specified format.
		/// <newpara></newpara>
		/// </summary>
		/// <param name="ObjectToSave">Object to be saved.</param>
		/// <param name="XmlFilePathName">File Path name of the Xml file to contain the object serialized to Xml.</param>
		/// <param name="SerializedFormat">Xml serialized format to load the object from.</param>
		/// <returns>Returns success of the object save.</returns>
		public virtual bool Save(Object ObjectToSave, string XmlFilePathName, SerializedFormatType SerializedFormat)
		{
			bool success = false;

			switch (SerializedFormat)
			{
				case SerializedFormatType.Binary :
					success = this.SaveToBinaryFormat(ObjectToSave, XmlFilePathName, null);
					break;

				case SerializedFormatType.Document :
				default :
					success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, null);
					break; 
			}
		
			return success;
		}

		public virtual bool Save(Object ObjectToSave, string XmlFilePathName, 
			SerializedFormatType SerializedFormat, IsolatedStorageFile isolatedStorageFolder)
		{
			bool success = false;

			switch (SerializedFormat)
			{
				case SerializedFormatType.Binary :
					success = this.SaveToBinaryFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
					break;

				case SerializedFormatType.Document :
				default :
					success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
					break; 
			}
		
			return success;
		}


		/// <summary>
		/// Save an object to an Xml file that is in an Xml Document forward, at a Isolated storage location.
		/// </summary>
		/// <param name="ObjectToSave">Object to be saved.</param>
		/// <param name="XmlFilePathName">File name (no path) of the Xml file to contain the object serialized to Xml.</param>
		/// <param name="isolatedStorageFolder">Isolated Storage object that is a user and assembly specific folder location
		/// from which to save the Xml file.</param>
		/// <returns></returns>
		public virtual bool Save(Object ObjectToSave, string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{
			bool success = false;
			success = this.SaveToDocumentFormat(ObjectToSave, XmlFilePathName, isolatedStorageFolder);
			return success;
		}

		private bool SaveToDocumentFormat(Object ObjectToSave, 
			string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{
			TextWriter textWriter = null;
			bool success = false;

			try
			{
				Type ObjectType = ObjectToSave.GetType();

				//Create serializer object using the type name of the Object to serialize.
				XmlSerializer xmlSerializer = new XmlSerializer(ObjectType);

				if (isolatedStorageFolder == null)
					textWriter = new StreamWriter(XmlFilePathName);
				else
					textWriter = new StreamWriter(new IsolatedStorageFileStream(XmlFilePathName, FileMode.OpenOrCreate, isolatedStorageFolder));
				
				xmlSerializer.Serialize(textWriter, ObjectToSave);

				success = true;
			}
			finally
			{
				//Make sure to close the file even if an exception is raised...
				if (textWriter != null)
					textWriter.Close();								
			}

			return success;
		}

		private bool SaveToBinaryFormat(Object ObjectToSave, 
			string XmlFilePathName, IsolatedStorageFile isolatedStorageFolder)
		{
			FileStream fileStream = null;
			bool success = false;

			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();

				if (isolatedStorageFolder == null)
					fileStream = new FileStream(XmlFilePathName, FileMode.OpenOrCreate);
				else
					fileStream = new IsolatedStorageFileStream(XmlFilePathName, FileMode.OpenOrCreate, isolatedStorageFolder);
				
				binaryFormatter.Serialize(fileStream, ObjectToSave);

				success = true;
			}
			finally
			{
				//Make sure to close the file even if an exception is raised...
				if (fileStream != null)
					fileStream.Close();								
			}

			return success;
		}

	}
#endif
}
