using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.ComponentModel;
using System.Xml.Schema;

namespace MControl.Xml
{
    #region GenericXmlSerializer

    /// <summary>
    /// Generic XmlSerializer
    /// </summary>
    /// <example>
    ///  public namespace Pro 
    ///  { 
    ///     public interface IDemo {}     
    ///     public class RealDemo : IDemo { public int X; }     
    ///     public class OtherDemo : IDemo { public double X; }      
    ///     public class Demo     
    ///     {         
    ///         public GenericXmlSerializer<IDemo> XDEmo;     
    ///     }       
    ///     public static void Main(string[] args)     
    ///     {         
    ///         var x = new Demo();         
    ///         x.XDEmo = new GenericXmlSerializer<IDemo>(new RealDemo());         
    ///         var s = new XmlSerializer(typeof(Demo));         
    ///         var sw = new StringWriter();         
    ///         s.Serialize(sw, x);         
    ///         Console.WriteLine(sw);     
    ///     }
    ///  }
    ///  //Output:
    /// <?xml version="1.0" encoding="utf-16"?> 
    /// <MainClass    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"   xmlns:xsd="http://www.w3.org/2001/XMLSchema">  
    /// <Foo type="Pro.RealFoo, Pro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">   
    ///     <RealFoo>    
    ///         <X>0</X>   
    ///     </RealFoo>  
    /// </Foo> 
    /// </MainClass> 
    /// </example>
    /// <typeparam name="T"></typeparam>
    public sealed class GenericXmlSerializer<T> : IXmlSerializable
    {
        public GenericXmlSerializer() { }

        public GenericXmlSerializer(T t)
        {
            this.Value = t;
        }     
        
        public T Value { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }
            Type type = this.Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            serializer.Serialize(writer, this.Value);
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes) throw new FormatException("expected a type attribute!");
            string type = reader.GetAttribute("type");
            reader.Read(); // consume the value         
            if (type == "null")
                return;// leave T at default value         
            XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
            this.Value = (T)serializer.Deserialize(reader); 
            reader.ReadEndElement();
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
    }
    #endregion

    public class XmlFormatter
    {
        #region namespace

        public static XmlNamespaceManager GetNamespace(XmlDocument document) 
        { 
            XmlNamespaceManager mgr = new XmlNamespaceManager(document.NameTable); 
            //mgr.AddNamespace("ns0", "http://dev1/MyWebService1.wsdl"); 
            return mgr; 
        }
        public static void AddXmlNamespace(XmlNamespaceManager mgr,string prfix, string uri)
        {
            mgr.AddNamespace(prfix, uri); 
        }

        public static string GetXmlNamespace(string prfix, string uri)
        {
          return string.Format("xmlns:{0}={1}",prfix, uri);
        }

        public static string GetXmlDefaultNamespace(string prfix)
        {
            return string.Format("xmlns:{0}={1}", prfix, TargetNamespace);
        } 

        //This will returns the set of included namespaces for the serializer.
        public static XmlSerializerNamespaces GetNamespaces()
        {
            XmlSerializerNamespaces ns;
            ns = new XmlSerializerNamespaces();
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            ns.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            return ns;
        }

        /*
        XmlSerializerNamespaces defaultNamespaces;
        private static XmlSerializerNamespaces DefaultNamespaces
        {
            get
            {
                if (defaultNamespaces == null)
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.AddInternal("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.AddInternal("xsd", "http://www.w3.org/2001/XMLSchema");
                    if (defaultNamespaces == null)
                    {
                        defaultNamespaces = namespaces;
                    }
                }
                return defaultNamespaces;
            }
        }
         */ 
        public static XmlSerializerNamespaces GetNamespaces(string xmlns, bool enableXMLSchema)
        {
            bool hasNamespace = !string.IsNullOrEmpty(xmlns);

            if (!hasNamespace && !enableXMLSchema)
            {
                return null;
            }

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (hasNamespace)
            {
                ns.Add("xmlns", xmlns);
            }
            if (enableXMLSchema)
            {
                ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                ns.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            }
            return ns;
        }

        public const string DefaultNamespace = "xmlns=\"http://www.w3.org/2001/XMLSchema\"";

        //Returns the target namespace for the serializer.
        public static string TargetNamespace
        {
            get
            {
                return "http://www.w3.org/2001/XMLSchema";
            }
        }

        public static string RemoveXmlDeclaration(string xml)
        {
           return Regx.RegexReplace("<\\?xml[^>]*>", xml, "");
        }
        #endregion

        #region Members
        readonly string m_rootElementName;
        readonly string m_namespace;
        readonly string m_encoding;
        #endregion

        #region Ctor

        public XmlFormatter(string rootElement, string ns, string encoding)
        {
            m_rootElementName = rootElement;
            m_namespace = ns;
            m_encoding = encoding;
        }
        #endregion

        #region Dictionary To Xml methods

        public string DictionaryToXmlString(IDictionary properties)
        {
             try
            {

                XmlDocument doc = BuildXml(properties);
                
                return doc.OuterXml;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //doc.LoadXml("<Error>" + error + "</Error>");
                return null;
            }
         }

        public XmlElement DictionaryToXmlElement(IDictionary properties)
        {
            try
            {
                XmlDocument doc = BuildXml(properties);
                return doc.DocumentElement;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //doc.LoadXml("<Error>" + error + "</Error>");
                return null;
            }

        }

        public void WriteDictionaryToXml(XmlWriter xmlWriter, IDictionary properties)
        {
            try
            {

                WriteDictionaryElement(xmlWriter, m_rootElementName, properties);
                xmlWriter.Flush();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        void WriteDictionaryElement(XmlWriter xmlWriter, string elementName, IDictionary properties)
        {
            xmlWriter.WriteStartElement(elementName, m_namespace);

            foreach (string propName in properties.Keys)
            {
                object propValue = properties[propName];
                if (propValue == null)
                    continue;

                if (propValue is IDictionary)
                {
                    WriteDictionaryElement(xmlWriter, propName, (IDictionary)propValue);
                }
                else if (propValue is XmlNode)
                {
                    xmlWriter.WriteStartElement(propName, m_namespace);
                    xmlWriter.WriteNode(new XmlNodeReader((XmlNode)propValue), false);
                    xmlWriter.WriteEndElement();
                }
                else
                {
                    string propValueStr = propValue.ToString();
                    if (!string.IsNullOrEmpty(propValueStr))
                        xmlWriter.WriteElementString(propName, m_namespace, propValueStr);
                }
            }

            xmlWriter.WriteEndElement();
        }

        public XmlDocument BuildXml(IDictionary properties)
        {
            Encoding enc = Encoding.GetEncoding(m_encoding);
            XmlDocument doc = new XmlDocument();

            using (MemoryStream ms = new MemoryStream(8192))
            {
                XmlWriter xmlWriter = new XmlTextWriter(ms, enc);

                xmlWriter.WriteStartDocument();

                WriteElement(xmlWriter, m_rootElementName, properties);

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                ms.Position = 0;
                doc.Load(ms);
            }
            return doc;
        }

        //public Stream BuildXml(IDictionary properties)
        //{
        //    Encoding enc = Encoding.GetEncoding(m_encoding);
        //    MemoryStream ms = new MemoryStream(8192);
        //    XmlWriter xmlWriter = new XmlTextWriter(ms, enc);

        //    xmlWriter.WriteStartDocument();

        //    WriteElement(xmlWriter, m_rootElementName, properties);

        //    xmlWriter.WriteEndDocument();

        //    xmlWriter.Flush();
        //    ms.Position = 0;

        //    return ms;
        //}

        void WriteElement(XmlWriter xmlWriter, string elementName, IDictionary properties)
        {
            xmlWriter.WriteStartElement(elementName, m_namespace);

            foreach (string propName in properties.Keys)
            {
                object propValue = properties[propName];
                if (propValue == null)
                    continue;

                if (propValue is IDictionary)
                {
                    WriteElement(xmlWriter, propName, (IDictionary)propValue);
                }
                else if (propValue is XmlNode)
                {
                    xmlWriter.WriteStartElement(propName, m_namespace);
                    xmlWriter.WriteNode(new XmlNodeReader((XmlNode)propValue), false);
                    xmlWriter.WriteEndElement();
                }
                else
                {
                    string propValueStr = propValue.ToString();
                    if (!string.IsNullOrEmpty(propValueStr))
                        xmlWriter.WriteElementString(propName, m_namespace, propValueStr);
                }
            }

            xmlWriter.WriteEndElement();
        }
        #endregion
          
        #region static Dictionary xml Serialization

        public static string DictionaryToXml(IDictionary dictionary, string entityName)
        {
            XmlFormatter formatter = new XmlFormatter(entityName, XmlFormatter.TargetNamespace, "utf-8");
            return formatter.DictionaryToXmlString(dictionary);
        }

        public static void WriteXmlToDictionary(string xmlString, IDictionary dictionary)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlString);
                XmlNode node = doc.DocumentElement;
                if (node == null)
                {
                    throw new System.Xml.XmlException("Root tag not found");
                }
                XmlNodeList list = node.ChildNodes;
                lock (dictionary.SyncRoot)
                {
                    foreach (XmlNode n in list)
                    {
                        dictionary.Add(n.Name, n.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
         }

        public static IDictionary XmlToDictionary(string xmlString)
        {
            IDictionary properties = new Hashtable();
            WriteXmlToDictionary(xmlString, properties);
            return properties;
        }
        #endregion

        #region static PropertyDescriptor Serialize

        /*
        /// <summary>
        /// Serialize object by PropertyDescriptor To Xml
        /// </summary>
        /// <param name="CodeObject"></param>
        /// <returns></returns>
        public static string SerializePropertiesToXml(object CodeObject)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<");
            xml.Append(CodeObject.GetType().FullName);
            xml.Append(">");

            // Now, walk all the properties of the object.
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(CodeObject); ;

            foreach (PropertyDescriptor p in properties)
            {
                if (!p.ShouldSerializeValue(CodeObject))
                {
                    continue;
                }

                object value = p.GetValue(CodeObject);
                Type valueType = null;
                if (value != null) valueType = value.GetType();

                // You have a valid property to write.
                xml.AppendFormat("<{0}>{1}</{0}>", p.Name, value);
            }

            xml.AppendFormat("</{0}>", CodeObject.GetType().FullName);
            return xml.ToString();
        }

*/

        /// <summary>
        /// Serialize object by PropertyDescriptor To Xml
        /// </summary>
        /// <param name="CodeObject"></param>
        /// <returns></returns>
        public static string SerializePropertiesToXml(object obj, string nameSpase)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (string.IsNullOrEmpty(nameSpase))
                nameSpase = XmlFormatter.TargetNamespace;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj); ;

            Encoding enc = Encoding.UTF8;//.GetEncoding(m_encoding);
            XmlDocument doc = new XmlDocument();

            using (MemoryStream ms = new MemoryStream(8192))
            {
                XmlWriter xmlWriter = new XmlTextWriter(ms, enc);

                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement(obj.GetType().Name, nameSpase);


                foreach (PropertyDescriptor p in properties)
                {
                    if (!p.ShouldSerializeValue(obj))
                    {
                        continue;
                    }
                    string elementName = p.Name;
                    object value = p.GetValue(obj);
                    Type valueType = null;
                    if (value != null) valueType = value.GetType();

                    //TODO WHAT IF VALUE IS A CLASS
                    
                    if (value is XmlNode)
                    {
                        xmlWriter.WriteStartElement(p.Name);//, m_namespace);
                        xmlWriter.WriteNode(new XmlNodeReader((XmlNode)value), false);
                        xmlWriter.WriteEndElement();
                    }
                    else
                    {
                        string propValueStr = value == null ? "" : value.ToString();
                        if (!string.IsNullOrEmpty(propValueStr))
                            xmlWriter.WriteElementString(p.Name, /*m_namespace,*/ propValueStr);
                    }
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                ms.Position = 0;
                doc.Load(ms);
            }

            return doc.OuterXml;
        }

        /// <summary>
        /// Serialize object by PropertyDescriptor To Xml
        /// </summary>
        /// <param name="CodeObject"></param>
        /// <returns></returns>
        public static T DeserializeProperties<T>(string xmlString, string nameSpase)
        {
            T obj = System.Activator.CreateInstance<T>();

           
            if (string.IsNullOrEmpty(nameSpase))
                nameSpase = XmlFormatter.TargetNamespace;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj); ;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.DocumentElement;
            if (node == null)
            {
                throw new System.Xml.XmlException("Root tag not found");
            }
            XmlNodeList list = node.ChildNodes;

            foreach (XmlNode n in list)
            {
                //dictionary.Add(n.Name, n.InnerText);
                properties[n.Name].SetValue(obj, n.InnerText);

            }
            return obj;
        }

        #endregion

        #region static methods

        public static XmlElement ToXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch (Exception ex)
            {

                string error = ex.Message;
                doc.LoadXml("<Error>" + error + "</Error>");
            }

            return doc.DocumentElement;
        }


        public static object XmlDeserialize(string xml, Type type)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlRootAttribute rootAttribute = new XmlRootAttribute(doc.DocumentElement.Name);
            rootAttribute.Namespace = doc.DocumentElement.NamespaceURI;

            XmlSerializer ser = new XmlSerializer(type, rootAttribute);
            return ser.Deserialize(new XmlNodeReader(doc.DocumentElement));
        }

        public static object XmlDeserialize(XmlElement xml, Type type, string Namespace)
        {
            XmlSerializer ser = new XmlSerializer(type, Namespace);
            return ser.Deserialize(new XmlNodeReader(xml));
        }

        #endregion

       /*

        //Creates an object from an XML string.
        public static object FromXml(string Xml, System.Type ObjType)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType);
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;
        }

        //Serializes the <i>Obj</i> to an XML string.
        public static string ToXml(object Obj, System.Type ObjType)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType, TargetNamespace);
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj, GetNamespaces());
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }
        */

    }
}
