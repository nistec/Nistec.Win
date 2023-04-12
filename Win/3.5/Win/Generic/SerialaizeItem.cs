using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Data;
using System.ComponentModel;
using System.IO;
using MControl.Runtime;


namespace MControl.Generic
{
      
   
    /// <summary>
    /// SerialaizeItem base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SerialaizeItem<T>
    {
 
        /// <summary>
        /// Deserialize queue item from base64 string
        /// </summary>
        public static T Deserialize(string serItem)
        {
            object o = Serialization.DeserializeFromBase64(serItem);
            if (o == null)
                return default(T);
            if (!(o.GetType().IsAssignableFrom(typeof(T))))
            {
                throw new Exception("Type not valid");
            }
            return (T)o;
        }

        /// <summary>
        /// Deserialize item from byte array
        /// </summary>
        public static T Deserialize(byte[] bytes)
        {
            object o = Serialization.DeserializeFromBytes<T>(bytes);//.ByteArrayToObject(bytes);
            if (o == null)
                return default(T);
            if (!(o.GetType().IsAssignableFrom(typeof(T))))
            {
                throw new Exception("Type not valid");
            }
            return (T)o;
        }
        /// <summary>
        /// ReadFile
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T ReadFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return default(T);
            }
            return Deserialize(File.ReadAllBytes(filename));

        }

        /// <summary>
        /// Serialize Queue Item
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return Serialization.SerializeToBase64(this);
        }

        /// <summary>
        /// Serialize Queue Item To ByteArray
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return Serialization.SerializeToBytes(this);//.ObjectToByteArray(this);
        }

        /// <summary>
        /// Stores mime entity and it's child entities to specified stream.
        /// </summary>
        /// <param name="storeStream">Stream where to store mime entity.</param>
        public void ToStream(System.IO.Stream storeStream)
        {
            byte[] data = ToByteArray();
            storeStream.Write(data, 0, data.Length);
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            File.WriteAllBytes(filename, ToByteArray());

            //using (FileStream fs = File.Create(filename))
            //{
            //    fs.BeginWrite
            //    ToStream(fs);
            //    SysUtil.StreamCopy(message, fs);

            //    // Create message info file for the specified relay message.
            //    RelayMessageInfo messageInfo = new RelayMessageInfo(sender, to, date, false, targetHost);

            //    File.WriteAllBytes(filename, ToByteArray());
            //}
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="filename"></param>
        public void Delete(string filename)
        {
            SysUtil.DeleteFile(filename);
        }
    }

  
}
