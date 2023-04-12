using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace MControl.Runtime
{


    /// <summary>
    /// Class for serialization utilities
    /// </summary>
    public static class Serialization
    {
        #region Bin Serialize/Deserialize

        public static object BinDeserialize(Stream p_Stream)
        {
            BinaryFormatter f = new BinaryFormatter();
            object tmp;
            tmp = f.Deserialize(p_Stream);
            return tmp;
        }

        public static void BinSerialize(Stream p_Stream, object p_Object)
        {
            BinaryFormatter f = new BinaryFormatter();
            f.Serialize(p_Stream, p_Object);
        }

        public static object BinDeserialize(string p_strFileName)
        {
            object tmp;
            using (FileStream l_Stream = new FileStream(p_strFileName, FileMode.Open, FileAccess.Read))
            {
                tmp = BinDeserialize(l_Stream);
                l_Stream.Close();
            }
            return tmp;
        }

        public static void BinSerialize(string p_strFileName, object p_Object)
        {
            using (FileStream l_Stream = new FileStream(p_strFileName, FileMode.Create, FileAccess.Write))
            {
                BinSerialize(l_Stream, p_Object);
                l_Stream.Close();
            }
        }
        #endregion

        #region file Serialization

        /// <summary>
        /// SerializeToBase64
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static void SerializeToFile(object body, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, body);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }
        }

        /// <summary>
        /// DeserializeFromBytes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string filename)
        {
            T result = default(T);

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    result = (T)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }
        #endregion

        #region byte array
        /*
        /// <summary>
        /// Convert an object to a byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            try
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
            catch (SerializationException )
            {
                return null;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }

        /// <summary>
        /// Convert a byte array to an Object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream(arrBytes);
            BinaryFormatter binForm = new BinaryFormatter();
            try
            {
                //memStream.Write(arrBytes, 0, arrBytes.Length);
                //memStream.Seek(0, SeekOrigin.Begin);
                return (Object)binForm.Deserialize(memStream);
            }
            catch
            {
                return null;
            }
        }
         */
        #endregion

        #region base64 Serialization

        /// <summary>
        /// SerializeToBase64
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string SerializeToBase64(object body)
        {
            string result = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byte[] byteArray = ms.ToArray();
                    result = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// DeserializeFromBase64
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static T DeserializeFromBase64<T>(string base64String)
        {
            T result = default(T);

            if (base64String == null)
                return result;

            byte[] buf = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = (T)formatter.Deserialize(ms);

                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }


        /// <summary>
        /// DeserializeFromBase64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static object DeserializeFromBase64(string base64String)
        {
            object result = null;

            if (base64String == null)
                return null;

            byte[] buf = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);

                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }


        /// <summary>
        /// DeserializeFromBase64
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static object DeserializeFromBase64(string base64String, ref int size)
        {
            object result = null;

            if (base64String == null)
                return null;

            byte[] buf = Convert.FromBase64String(base64String);
            size = buf.Length;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);

                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }

        /// <summary>
        /// SerializeToBase64
        /// </summary>
        /// <param name="body"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string SerializeToBase64(object body, ref int size)
        {
            string result = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byte[] byteArray = ms.ToArray();
                    size = byteArray.Length;
                    result = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }
            return result;
        }

        public static string ConvertToBase64(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray, 0, byteArray.Length);

        }

        public static byte[] ConvertFromBase64(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
        #endregion

        #region bytes Serialization

        /// <summary>
        /// SerializeToBytes
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static byte[] SerializeToBytes(object body)
        {

            byte[] byteArray = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byteArray = ms.ToArray();
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return null;
                }
            }
            return byteArray;
        }

        /// <summary>
        /// DeserializeFromBytes
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static object DeserializeFromBytes(byte[] buf)
        {
            object result = null;

            if (buf == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }

        /// <summary>
        /// DeserializeFromBytes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static T DeserializeFromBytes<T>(byte[] buf)
        {
            T result = default(T);

            if (buf == null)
                return result;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = (T)formatter.Deserialize(ms);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                }
            }

            return result;
        }
        #endregion

        #region SizeOf

        /// <summary>
        /// SizeOf object in Bytes
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static int SizeOf(object body)
        {

            byte[] byteArray = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byteArray = ms.ToArray();
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return 0;
                }
            }
            return byteArray.Length;
        }
        #endregion

        #region structure


        /// <summary>
        /// Structure To ByteArray
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] StructureToByteArray(object obj)
        {
            try
            {
                int len = Marshal.SizeOf(obj);

                byte[] arr = new byte[len];

                IntPtr ptr = Marshal.AllocHGlobal(len);

                Marshal.StructureToPtr(obj, ptr, true);

                Marshal.Copy(ptr, arr, 0, len);

                Marshal.FreeHGlobal(ptr);

                return arr;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromByteArray(byte[] bytearray, Type type)//, ref object obj)
        {
            object obj = null;

            try
            {
                //int len = Marshal.SizeOf(obj);
                int len = bytearray.Length;

                IntPtr i = Marshal.AllocHGlobal(len);

                Marshal.Copy(bytearray, 0, i, len);

                obj = Marshal.PtrToStructure(i, type);//obj.GetType());

                Marshal.FreeHGlobal(i);

                return obj;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Structure To Base64
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StructureToBase64(object obj)
        {
            int size = 0;
            return StructureToBase64(obj, ref size);

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromBase64(string base64String, Type type)//, ref object obj)
        {
            int size = 0;
            return StructureFromBase64(base64String, type, ref size);
        }

        /// <summary>
        /// Structure To Base64
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string StructureToBase64(object obj, ref int size)
        {


            try
            {
                int len = Marshal.SizeOf(obj);

                byte[] byteArray = new byte[len];

                size = byteArray.Length;

                IntPtr ptr = Marshal.AllocHGlobal(len);

                Marshal.StructureToPtr(obj, ptr, true);

                Marshal.Copy(ptr, byteArray, 0, len);

                Marshal.FreeHGlobal(ptr);

                return Convert.ToBase64String(byteArray, 0, byteArray.Length);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromBase64(string base64String, Type type, ref int size)//, ref object obj)
        {
            object obj = null;

            try
            {
                byte[] bytearray = Convert.FromBase64String(base64String);

                //int len = Marshal.SizeOf(obj);
                int len = bytearray.Length;

                size = len;

                IntPtr i = Marshal.AllocHGlobal(len);

                Marshal.Copy(bytearray, 0, i, len);

                obj = Marshal.PtrToStructure(i, type);//obj.GetType());

                Marshal.FreeHGlobal(i);

                return obj;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region xml
        /*
        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="ns">namespace</param>
        /// <returns></returns>
        public static string SerializeToXml(object obj, string ns)
        {
            Type type = obj.GetType();
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(type, ns);
                serializer.Serialize(writer, type);
                writer.Flush();
                return writer.ToString();
            }
        }

       /// <summary>
        ///  Serialize To Xml using XmlSerializer
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="obj"></param>
        /// <param name="ns">namespace</param>
       /// <returns></returns>
        public static string SerializeToXml<T>(T obj, string ns)
        {
            Type type = typeof(T);
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(type, ns);
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
        public static string SerializeToXml(object obj)
        {
            Type type = obj.GetType();
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(type);
                serializer.Serialize(writer, type);
                writer.Flush();
                return writer.ToString();
            }
        }
             
        /// <summary>
        /// Serialize To Xml using XmlSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml<T>(T obj)
        {
            Type type = typeof(T);
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(type);
                serializer.Serialize(writer, type);
                writer.Flush();
                return writer.ToString();
            }
        }
        /// <summary>
        /// Deserialize from Xml using XmlSerializer and XmlRootAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserializeFromXml<T>(string xmlString)
        {
            Type type = typeof(T);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            XmlRootAttribute rootAttribute = new XmlRootAttribute(doc.DocumentElement.Name);
            rootAttribute.Namespace = doc.DocumentElement.NamespaceURI;

            XmlSerializer ser = new XmlSerializer(type, rootAttribute);

            return (T)ser.Deserialize(new XmlNodeReader(doc.DocumentElement));
        }

        /// <summary>
        /// Deserialize from Xml using XmlSerializer and specific namespace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <param name="ns">namespace</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string xmlString, string ns)
        {
            Type type = typeof(T);
            XmlSerializer serializer = new XmlSerializer(type, ns);
            using (StringReader ms = new StringReader(xmlString))
            {
                XmlReader reader = new XmlTextReader(ms);
                return (T)serializer.Deserialize(reader);
            }
        }

      

        ///// <summary>
        ///// Deserialize from Xml using XmlSerializer
        ///// </summary>
        ///// <param name="xmlString"></param>
        ///// <returns></returns>
        //public static T DeserializeFromXml<T>(string xmlString)
        //{
        //    Type type = typeof(T);
        //    XmlSerializer serializer = new XmlSerializer(type);
        //    using (StringReader ms = new StringReader(xmlString))
        //    {
        //        XmlReader reader = new XmlTextReader(ms);
        //        return (T)serializer.Deserialize(reader);
        //    }
        //}


        ///// <summary>
        ///// SerializeToXml
        ///// </summary>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //public static string SerializeToXml(object  body)
        //{
        //    return SerializeToXml(body, Encoding.UTF8);
        //}

        /// <summary>
        /// SerializeToXml
        /// </summary>
        /// <param name="body"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SerializeToXml(object body, Encoding encoding)
        {
            string result = null;
            Type type = body.GetType();

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, encoding);
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = encoding.GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Deserialize From Xml using utf-8 encoding
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xmlString, Type type)
        {
            return DeserializeFromXml(xmlString, type, Encoding.UTF8);
         }

        /// <summary>
        /// Deserialize From Xml with specific encoding
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xmlString, Type type, Encoding encoding)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = encoding.GetBytes(xmlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
        */
        #endregion

        #region jason

        public static string SerializeToJson(this object o)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            return jsonSerializer.Serialize(o);
        }

        public static T DeserializeFromJson<T>(this string s)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<T>(s);
        }

        public static string SerializeToJsonArray<T>(this List<T> list)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(list);
        }

        public static List<T> DeserializeFromJsonArray<T>(this string s)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Deserialize<List<T>>(s);
        }
        #endregion
    }

#if(false)
	/// <summary>
	/// Class for serialization utilities
	/// </summary>
	public class Serialization
	{
		public static object BinDeserialize(Stream p_Stream)
		{
			BinaryFormatter f = new BinaryFormatter();
			object tmp;
			tmp = f.Deserialize(p_Stream);
			return tmp;
		}

		public static void BinSerialize(Stream p_Stream, object p_Object)
		{
			BinaryFormatter f = new BinaryFormatter();
			f.Serialize(p_Stream,p_Object);
		}

		public static object BinDeserialize(string p_strFileName)
		{
			object tmp;
			using (FileStream l_Stream = new FileStream(p_strFileName,FileMode.Open,FileAccess.Read))
			{
				tmp = BinDeserialize(l_Stream);
				l_Stream.Close();
			}
			return tmp;
		}

		public static void BinSerialize(string p_strFileName, object p_Object)
		{
			using (FileStream l_Stream = new FileStream(p_strFileName,FileMode.Create,FileAccess.Write))
			{
				BinSerialize(l_Stream,p_Object);
				l_Stream.Close();
			}
		}

        /// <summary>
        /// Convert an object to a byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            try
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
            catch (SerializationException )
            {
                return null;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }

        /// <summary>
        /// Convert a byte array to an Object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream(arrBytes);
            BinaryFormatter binForm = new BinaryFormatter();
            try
            {
                //memStream.Write(arrBytes, 0, arrBytes.Length);
                //memStream.Seek(0, SeekOrigin.Begin);
                return (Object)binForm.Deserialize(memStream);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// SerializeToBase64
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string SerializeToBase64(object body)
        {
            string result = "";
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byte[] byteArray = ms.ToArray();
                    result = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return null;
                }
                finally
                {
                    //Clean up 
                    ms.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// DeserializeFromBase64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static object DeserializeFromBase64(string base64String)
        {
            object result = null;

            if (base64String == null)
                return null;

            byte[] buf = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);

                }
                finally
                {
                    ms.Close();
                }
            }

            return result;
        }


        /// <summary>
        /// DeserializeFromBase64
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static object DeserializeFromBase64(string base64String, ref int size)
        {
            object result = null;

            if (base64String == null)
                return null;

            byte[] buf = Convert.FromBase64String(base64String);
            size = buf.Length;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);

                }
                finally
                {
                    ms.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// SerializeToBase64
        /// </summary>
        /// <param name="body"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string SerializeToBase64(object body,ref int size)
        {
            string result = "";
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byte[] byteArray = ms.ToArray();
                    size = byteArray.Length;
                    result = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return null;
                }
                finally
                {
                    //Clean up 
                    ms.Close();
                }
            }
            return result;
        }

        public static string ConvertToBase64(byte[] byteArray)
        {
            return  Convert.ToBase64String(byteArray, 0, byteArray.Length);

        }

        public static byte[] ConvertFromBase64(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        /// <summary>
        /// SerializeToBytes
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static byte[] SerializeToBytes(object body)
        {

            byte[] byteArray = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byteArray = ms.ToArray();
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return null;
                }
                finally
                {
                    //Clean up 
                    ms.Close();
                }
            }
            return byteArray;
        }

        /// <summary>
        /// DeserializeFromBytes
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static object DeserializeFromBytes(byte[] buf)
        {
            object result = null;

            if (buf == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(buf, 0, buf.Length);

                    ms.Seek(0, 0);

                    BinaryFormatter formatter = new BinaryFormatter();

                    result = formatter.Deserialize(ms);
                }
                finally
                {
                    ms.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// SizeOf object in Bytes
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static int SizeOf(object body)
        {

            byte[] byteArray = null;
            // instantiate a MemoryStream and a new instance of our class          
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    // create a new BinaryFormatter instance 
                    BinaryFormatter formatter = new BinaryFormatter();
                    // serialize the class into the MemoryStream 
                    formatter.Serialize(ms, body);
                    byteArray = ms.ToArray();
                }
                catch (SerializationException e)
                {
                    string s = e.Message;
                    return 0;
                }
                finally
                {
                    //Clean up 
                    ms.Close();
                }
            }
            return byteArray.Length;
        }

        #region structure


        /// <summary>
        /// Structure To ByteArray
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] StructureToByteArray(object obj)
        {
            try
            {
                int len = Marshal.SizeOf(obj);

                byte[] arr = new byte[len];

                IntPtr ptr = Marshal.AllocHGlobal(len);

                Marshal.StructureToPtr(obj, ptr, true);

                Marshal.Copy(ptr, arr, 0, len);

                Marshal.FreeHGlobal(ptr);

                return arr;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromByteArray(byte[] bytearray, Type type)//, ref object obj)
        {
            object obj = null;

            try
            {
                //int len = Marshal.SizeOf(obj);
                int len = bytearray.Length;

                IntPtr i = Marshal.AllocHGlobal(len);

                Marshal.Copy(bytearray, 0, i, len);

                obj = Marshal.PtrToStructure(i, type);//obj.GetType());

                Marshal.FreeHGlobal(i);

                return obj;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Structure To Base64
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StructureToBase64(object obj)
        {
            int size = 0;
            return StructureToBase64(obj, ref size);

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromBase64(string base64String, Type type)//, ref object obj)
        {
            int size = 0;
            return StructureFromBase64(base64String,type,ref size);
        }

        /// <summary>
        /// Structure To Base64
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string StructureToBase64(object obj, ref int size)
        {


            try
            {
                int len = Marshal.SizeOf(obj);

                byte[] byteArray = new byte[len];

                size = byteArray.Length;

                IntPtr ptr = Marshal.AllocHGlobal(len);

                Marshal.StructureToPtr(obj, ptr, true);

                Marshal.Copy(ptr, byteArray, 0, len);

                Marshal.FreeHGlobal(ptr);

                return Convert.ToBase64String(byteArray, 0, byteArray.Length);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// ByteArray To Structure
        /// </summary>
        /// <param name="bytearray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object StructureFromBase64(string base64String, Type type, ref int size)//, ref object obj)
        {
            object obj = null;

            try
            {
                byte[] bytearray = Convert.FromBase64String(base64String);

                //int len = Marshal.SizeOf(obj);
                int len = bytearray.Length;

                size = len;

                IntPtr i = Marshal.AllocHGlobal(len);

                Marshal.Copy(bytearray, 0, i, len);

                obj = Marshal.PtrToStructure(i, type);//obj.GetType());

                Marshal.FreeHGlobal(i);

                return obj;
            }
            catch
            {
                return null;
            }
        }

        #endregion

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

 
        /// <summary>
        /// SerializeToXml
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string SerializeToXml(object  body)
        {
            return SerializeToXml(body, Encoding.UTF8);
        }

        /// <summary>
        /// SerializeToXml
        /// </summary>
        /// <param name="body"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SerializeToXml(object body, Encoding encoding)
        {
            string result = null;
            Type type = body.GetType();

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);

            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = new XmlTextWriter(ms, encoding);
                //result = serializer.Serialize((reader);
                serializer.Serialize(writer, body);
                byte[] byteArray = ms.GetBuffer();
                result = encoding.GetString(byteArray);
                writer.Close();
                ms.Close();
            }
            return result;
        }
        /// <summary>
        /// Deserialize From Xml using utf-8 encoding
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xmlString, Type type)
        {
            return DeserializeFromXml(xmlString, type, Encoding.UTF8);
         }

        /// <summary>
        /// Deserialize From Xml with specific encoding
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xmlString, Type type, Encoding encoding)
        {
            object result = null;

            // Create an instance of the XmlSerializer.
            XmlSerializer serializer = new XmlSerializer(type);
            byte[] byteArray = encoding.GetBytes(xmlString);

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                XmlReader reader = new XmlTextReader(ms);
                result = serializer.Deserialize(reader);
                reader.Close();
                ms.Close();
            }
            return result;
        }
	}
#endif

}
