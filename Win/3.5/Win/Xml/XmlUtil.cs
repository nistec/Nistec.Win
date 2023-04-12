using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Text.RegularExpressions;

namespace MControl.Xml
{

    //public enum NodeType
    //{
    //    Element,
    //    Attribute,
    //    CDATA
    //}

    public class XmlUtil
    {
        #region methods
        //// Methods
        //public static XmlAttribute CreateAttribute(string Name, string Value, XmlDocument xmlDoc)
        //{
        //    XmlAttribute attribute = xmlDoc.CreateAttribute(Name);
        //    attribute.InnerText = Value;
        //    return attribute;
        //}
        //public static XmlAttribute AppendAttribute(string Name, string Value, XmlDocument xmlDoc, XmlNode xNode)
        //{
        //    XmlAttribute attribute = CreateAttribute(Name, Value, xmlDoc);
        //    return xNode.Attributes.Append(attribute);
        //}

        //public static XmlNode CreateCDATA(string Name, string Value, XmlDocument xmlDoc)
        //{
        //    XmlNode node = xmlDoc.CreateNode(XmlNodeType.CDATA, Name, "");
        //    node.InnerText = Value;
        //    return node;
        //}

        //public static XmlNode CreateNode(string Name, string Value, XmlDocument xmlDoc)
        //{
        //    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, Name, "");
        //    node.InnerText = Value;
        //    return node;
        //}

        //public static string GetAttributeValue(XmlNode xNode, string AttributeName)
        //{
        //    try
        //    {
        //        return  xNode.Attributes[AttributeName].Value;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new XmlException(AttributeName, exception);
        //    }
        //}

        //public static string GetAttributeValue(XmlNode xNode, string Xpath, string attributeName)
        //{
        //    XmlNode node = null;
        //    XmlAttribute attribute = null;
        //    try
        //    {
        //       node = xNode.SelectSingleNode(Xpath);
        //        if (node == null)
        //        {
        //            throw new ArgumentException(xNode.Name + Xpath);
        //        }
        //        attribute = node.Attributes[attributeName];
        //        if (attribute == null)
        //        {
        //            throw new ArgumentException("Invalid Attributes: " + attributeName);
        //        }
        //        return  attribute.Value;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new XmlException(xNode.Name + Xpath, exception);
        //    }
        // }

        //public static XmlNodeList SelectNodes(XmlNode Node, string Xpath)
        //{
        //    XmlNodeList list = Node.SelectNodes(Xpath);
        //    if ((list == null) || (list.Count == 0))
        //    {
        //        throw new ArgumentException (Node.Name + Xpath);
        //    }
        //    return list;
        //}

        //public static XmlNode SelectSingleNode(XmlNode Node, string Xpath)
        //{
        //    XmlNode node = Node.SelectSingleNode(Xpath);
        //    if (node == null)
        //    {
        //        throw new ArgumentException(Node.Name + Xpath);
        //    }
        //    return node;
        //}
        #endregion

        /// <summary>
        ///Serialize to xml using string builder and property descriptor
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<");
            xml.Append(obj.GetType().FullName);
            xml.Append(">");

            // Now, walk all the properties of the object.
            PropertyDescriptorCollection properties;
            //PropertyDescriptor  p;

            properties = TypeDescriptor.GetProperties(obj);

            foreach (PropertyDescriptor p in properties)
            {
                if (!p.ShouldSerializeValue(obj))
                {
                    continue;
                }

                object value = p.GetValue(obj);
                Type valueType = null;
                if (value != null) valueType = value.GetType();

                // You have a valid property to write.
                xml.AppendFormat("<{0}>{1}</{0}>", p.Name, value);
            }

            xml.AppendFormat("</{0}>", obj.GetType().FullName);
            return xml.ToString();
        }

        public abstract class XmlSerializerBase
        {
            public abstract string Serialize(
          IDesignerSerializationManager m,
         object obj);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeConvertible(
                IDesignerSerializationManager m,
               object obj)
        {

            if (obj == null) return string.Empty;

            IConvertible c = obj as IConvertible;
            if (c == null)
            {
                // Rather than throwing exceptions, add a list of errors 
                // to the serialization manager.
                m.ReportError("Object is not IConvertible");
                return null;
            }

            return c.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
       
        /// <summary>
        /// Serialize to xml using string builder and property descriptor
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(
     IDesignerSerializationManager m,
     object obj)
        {

            StringBuilder xml = new StringBuilder();
            xml.Append("<");
            xml.Append(obj.GetType().FullName);
            xml.Append(">");

            // Now, walk all the properties of the object.
            PropertyDescriptorCollection properties;
            //PropertyDescriptor  p;

            properties = TypeDescriptor.GetProperties(obj);

            foreach (PropertyDescriptor p in properties)
            {
                if (!p.ShouldSerializeValue(obj))
                {
                    continue;
                }

                object value = p.GetValue(obj);
                Type valueType = null;
                if (value != null) valueType = value.GetType();

                // Get the serializer for this property
                XmlSerializerBase s = m.GetSerializer(
                    valueType,
                    typeof(XmlSerializerBase)) as XmlSerializerBase;

                if (s == null)
                {
                    // Because there is no serializer, 
                    // this property must be passed over.  
                    // Tell the serialization manager
                    // of the error.
                    m.ReportError(string.Format(
                        "Property {0} does not support XML serialization",
                        p.Name));
                    continue;
                }

                // You have a valid property to write.
                xml.AppendFormat("<{0}>", p.Name);
                xml.Append(s.Serialize(m, value));
                xml.AppendFormat("</{0}>", p.Name);

            }

            xml.AppendFormat("</{0}>", obj.GetType().FullName);
            return xml.ToString();
        }




  	
        public static DataSet ReadXmlFile(string file)
         {
             try
             {
                 System.Data.DataSet DSet = new DataSet();
                 DSet.ReadXml(file, XmlReadMode.Auto);
                 return DSet;
             }
             catch (ApplicationException ex)
             {
                 throw new ApplicationException(ex.Message);
             }
         }

        public static DataSet ReadXmlStream(string s)
         {
             try
             {
                 StringReader stream = new StringReader(s);
                 DataSet DSet = new DataSet();
                 XmlTextReader reader = new XmlTextReader(stream);
                 DSet.ReadXml(reader, XmlReadMode.Auto);

                 return DSet;
             }
             catch (ApplicationException ex)
             {
                 throw new ApplicationException(ex.Message);
             }
         }

        public static string NormelaizeXml(string xml)
        {
            Regex regex = new Regex(@">\s*<");
            xml = regex.Replace(xml, "><");
            return xml.Replace("\r\n", "").Replace("\n", "").Trim();
        }
    }

}
