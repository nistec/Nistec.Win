using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
//using MControl.Data.SqlClient;
//using MControl.Data;
using System.Data;

using System.IO;
using System.Xml.Serialization;

using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;


namespace MControl.Xml
{

    public class ObjectToXml 
    {
        public static ObjectToXml Instance = new ObjectToXml();

        public string Serialize(object CodeObject)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<");
            xml.Append(CodeObject.GetType().FullName);
            xml.Append(">");

            // Now, walk all the properties of the object.
            PropertyDescriptorCollection properties;
            //PropertyDescriptor  p;

            properties = TypeDescriptor.GetProperties(CodeObject);

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
    }



    //[DefaultSerializationProvider(typeof(XmlSerializationProvider))]
    public abstract class XmlSerializerBase
    {
        public abstract string Serialize(
      IDesignerSerializationManager m,
     object CodeObject);

    }

    //internal class XmlSerializationProvider : IDesignerSerializationProvider
    //{
    //    public object GetSerializer(
    //        IDesignerSerializationManager manager,
    //        object currentSerializer,
    //        Type objectType,
    //        Type serializerType) 
    //{

    //    // Null values will be given a null type by this serializer.
    //    // This test handles this case.
    //    if (objectType == null) 
    //    {
    //        return StringXmlSerializer.Instance;
    //    }

    //    if (typeof(IConvertible).IsSubclassOf(objectType)) 
    //    {
    //        return StringXmlSerializer.Instance;
    //    }
    //    Type[] types = new Type[1];
    //    types[0] = objectType;

    //    if (objectType.GetConstructor(new object[]) != null) 
    //    {
    //        return ObjectXmlSerializer.Instance;
    //    }

    //    return null;
    //}
    //}

    public class StringXmlSerializer : XmlSerializerBase
    {
        public static StringXmlSerializer Instance = new StringXmlSerializer();

        public override string Serialize(
             IDesignerSerializationManager m,
            object CodeObject)
        {

            if (CodeObject == null) return string.Empty;

            IConvertible c = CodeObject as IConvertible;
            if (c == null)
            {
                // Rather than throwing exceptions, add a list of errors 
                // to the serialization manager.
                m.ReportError("Object is not IConvertible");
                return null;
            }

            return c.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class ObjectXmlSerializer : XmlSerializerBase
    {
        public static ObjectXmlSerializer Instance = new ObjectXmlSerializer();

        public override string Serialize(
            IDesignerSerializationManager m,
            object CodeObject)
        {

            StringBuilder xml = new StringBuilder();
            xml.Append("<");
            xml.Append(CodeObject.GetType().FullName);
            xml.Append(">");

            // Now, walk all the properties of the object.
            PropertyDescriptorCollection properties;
            //PropertyDescriptor  p;

            properties = TypeDescriptor.GetProperties(CodeObject);

            foreach (PropertyDescriptor p in properties)
            {
                if (!p.ShouldSerializeValue(CodeObject))
                {
                    continue;
                }

                object value = p.GetValue(CodeObject);
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

            xml.AppendFormat("</{0}>", CodeObject.GetType().FullName);
            return xml.ToString();
        }
    }


    public class CodeSerializer : CodeDomSerializer
    {
        Type ObjectType;

        public CodeSerializer(Type objectType)
        {
            ObjectType = objectType;

        }
 
        public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
        {
            // This is how we associate the component with the serializer.
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
            GetSerializer(ObjectType.BaseType, typeof(CodeDomSerializer));

            /* This is the simplest case, in which the class just calls the base class
                to do the work. */
            return baseClassSerializer.Deserialize(manager, codeObject);
        }

        public override  object Serialize(IDesignerSerializationManager manager, object value)
        {
            /* Associate the component with the serializer in the same manner as with
                Deserialize */
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
                GetSerializer(ObjectType.BaseType, typeof(CodeDomSerializer));

            object codeObject = baseClassSerializer.Serialize(manager, value);

            /* Anything could be in the codeObject.  This sample operates on a
                CodeStatementCollection. */
            if (codeObject is CodeStatementCollection)
            {
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;

                // The code statement collection is valid, so add a comment.
                string commentText = "This comment was added to this object by a custom serializer.";
                CodeCommentStatement comment = new CodeCommentStatement(commentText);
                statements.Insert(0, comment);
            }
            return codeObject;
        }
    }

    //[DesignerSerializer(typeof(CodeSerializer), typeof(CodeDomSerializer))]
    //public class MyComponent : Component
    //{
    //    private string localProperty = "Component Property Value";
    //    public string LocalProperty
    //    {
    //        get
    //        {
    //            return localProperty;
    //        }
    //        set
    //        {
    //            localProperty = value;
    //        }
    //    }
    //}

}
