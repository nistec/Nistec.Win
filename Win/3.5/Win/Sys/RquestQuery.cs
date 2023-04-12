using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Collections.Generic;
using MControl;
using System.Diagnostics;
using System.Globalization;

namespace MControl.Sys
{
   
  
    /// <summary>
    /// RquestQuery Encryption
    /// </summary>
    public class RquestQuery
    {
        #region non static
        public readonly System.Collections.Specialized.NameValueCollection Query;

        public bool IsEmpty
        {
            get { return Query == null || Query.Count == 0; }
        }

        public RquestQuery(string qs, bool allowEmpty)
            : this(qs, allowEmpty, '&', '=')
        {
         
        }

        public RquestQuery(string qs, bool allowEmpty,char outerSplitter,char innerSplitter)
        {
            Query = new System.Collections.Specialized.NameValueCollection();

            if (string.IsNullOrEmpty(qs))
            {
                if (allowEmpty)
                    return;
                throw new ArgumentNullException();
            }
            try
            {

                string[] items = qs.Split(outerSplitter);

                foreach (string s in items)
                {
                    string[] arg = s.Split(innerSplitter);
                    if (arg.Length == 2)
                    {
                        Query.Add(arg[0], arg[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect query string :" + ex.Message);
            }
        }

        public string this[int index]
        {
            get { return Query[index]; }
        }
        public string this[string name]
        {
            get { return Query[name]; }
        }

        public T Get<T>(string field)
        {
            return GenericTypes.Convert<T>(Query[field]);
        }

        public T Get<T>(string field,T defoultValue)
        {
            return GenericTypes.Convert<T>(Query[field], defoultValue);
        }
        #endregion

        #region Arguments

        /// <summary>
        /// EncryptQuery
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptEx32(string data)
        {
            if (string.IsNullOrEmpty(data))
                return "";
            if (data.StartsWith("0X"))
                return data;
            return "0X" + BaseConverter.ToBase32(data);
        }

        /// <summary>
        /// DecryptQuery
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DecryptEx32(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length < 2)
            {
                throw new ArgumentException("argument is incorrect");//, must be more then 5 characters");
            }
            if (!data.StartsWith("0X"))
            {
                return data;
            }
            return BaseConverter.FromBase32(data.Remove(0, 2));
        }
/*
   
        /// <summary>
        /// EncryptId
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encType"></param>
        /// <returns></returns>
        public static string EncryptENB62(int data)
        {
            return BaseConverter.ToBase62(data);
        }

        /// <summary>
        /// DecryptId
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int DecryptENB62(string data)//, bool checkType = false)
        {
            if (data == null || data.Length < 1)
            {
                throw new ArgumentException("argument is incorrect");
            }
            //if (checkType)
            //{
            //    int id;
            //    if (int.TryParse(data, out id))
            //        return id;
            //}
            return (int)BaseConverter.FromBase62(data);
        }
 */      

        /// <summary>
        /// EncryptBigId ToBase62
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encType"></param>
        /// <returns></returns>
        public static string EncryptBigId(long data)
        {
            return BaseConverter.ToBase62(data);
        }

        /// <summary>
        /// DecryptBigId FromBase62
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long DecryptBigId(string data)//, bool checkType = false)
        {
            if (data == null || data.Length < 1)
            {
                throw new ArgumentException("argument is incorrect");
            }
            //if (checkType)
            //{
            //    long id;
            //    if (long.TryParse(data, out id))
            //        return id;
            //}
            return BaseConverter.FromBase62(data);
        }
       

        /// <summary>
        /// EncryptQuery
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encType"></param>
        /// <returns></returns>
        public static string EncryptQuery(string data, EncyptionType encType= EncyptionType.Ex32)
        {
            switch (encType)
            {
                case EncyptionType.Ex32:
                    return "0X" + BaseConverter.ToBase32(data);
                case EncyptionType.ENB32:
                    return "ENB32" + BaseConverter.ToBase32(data);
                case EncyptionType.ENU64:
                    return "ENU64" + Encryption.Encrypt(data, true);
                case EncyptionType.ENHEX:
                    return "ENHEX" + BaseConverter.ToHexString(data);
                default:
                    return "ENB64" + Encryption.Encrypt(data, false);
            }
        }
        /// <summary>
        /// DecryptQuery
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encType"></param>
        /// <returns></returns>
        public static string DecryptQuery(string data, EncyptionType encType = EncyptionType.Ex32)
        {
            if (encType == EncyptionType.Ex32)
            {
                return DecryptEx32(data);// BaseConverter.FromBase32(data.Remove(0, 2));
            }
            if (data == null || data.Length < 5)
            {
                throw new ArgumentException("argument is incorrect, must be more then 5 characters");
            }

            switch (encType)
            {
                case EncyptionType.ENB32:
                    return BaseConverter.FromBase32(data.Remove(0, 5));
                case EncyptionType.ENU64:
                    return Encryption.Decrypt(data.Remove(0, 5), true);
                case EncyptionType.ENHEX:
                    return BaseConverter.FromHexString(data.Remove(0, 5));
                case EncyptionType.ENB64:
                    return Encryption.Decrypt(data.Remove(0, 5), false);
                default:
                    data = System.Web.HttpUtility.UrlDecode(data);
                    data = data.Replace(" ", "+");
                    return Encryption.Decrypt(data, false);
            }
        }

        /// <summary>
        /// Decrypt Query with Optional
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DecryptQueryOptional(string data)
        {
            if (data == null || data.Length < 2)
            {
                throw new ArgumentException("argument is incorrect, must be more then 2 characters");
            }

            if (data.StartsWith("0X"))
            {
                return BaseConverter.FromBase32(data.Remove(0, 2));
            }
            if (data.Length < 5)
            {
                throw new ArgumentException("argument is incorrect, must be more then 5 characters");
            }
            //data = data.ToUpper();
            if (data.StartsWith("ENB32"))
            {
                return BaseConverter.FromBase32(data.Remove(0, 5));
            }
            if (data.StartsWith("ENHEX"))
            {
                return BaseConverter.FromHexString(data.Remove(0, 5));
            }
            if (data.StartsWith("ENU64"))
            {
                return Encryption.Decrypt(data.Remove(0, 5), true);
            }
            if (data.StartsWith("ENB64"))
            {
                return Encryption.Decrypt(data.Remove(0, 5), false);
            }

            //data = System.Web.HttpUtility.UrlDecode(data);
            //data = data.Replace(" ", "+");
            return Encryption.Decrypt(data, true);
        }

        ///// <summary>
        ///// EncryptQuery
        ///// </summary>
        ///// <param name="args"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public static string EncryptQuery(string args)
        //{
        //    if (string.IsNullOrEmpty(args))
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    return  Encryption.Encrypt(args, true);
        //}

        public static string EncryptQuery(string query, params string[] args)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException();
            }
            //return EncryptQuery(string.Format(query, args));
            return Encryption.Encrypt(string.Format(query, args), true);
        }


        public static RquestQuery DecryptQuery(System.Web.HttpRequest request, bool allowEmpty, bool useDecode = false)
        {
            return DecryptQuery(request.Url.Query.Substring(1), allowEmpty, useDecode);
        }
        /// <summary>
        /// DecryptQuery
        /// </summary>
        /// <param name="args"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static RquestQuery DecryptQuery(string args, bool allowEmpty, bool useDecode=false)
        {
            if (string.IsNullOrEmpty(args))
            {
                if (allowEmpty)
                    return new RquestQuery(args, allowEmpty);

                throw new ArgumentNullException();
            }
            try
            {
                string qs = Encryption.Decrypt(args, useDecode);

                return new RquestQuery(qs,allowEmpty);
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect query string :" + ex.Message);
            }
        }

        #endregion

        #region Encrypt query 32

        public static string EncryptQuery32(string query, params object[] args)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException();
            }

            return EncryptEx32(string.Format(query, args));
        }
        /// <summary>
        /// DecryptQuery32
        /// </summary>
        /// <param name="args"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static RquestQuery DecryptQuery32(string args, bool allowEmpty)
        {
            if (string.IsNullOrEmpty(args))
            {
                if (allowEmpty)
                    return new RquestQuery(args, allowEmpty);

                throw new ArgumentNullException();
            }
            try
            {
                string qs = DecryptEx32(args);

                return new RquestQuery(qs, allowEmpty);
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect query string :" + ex.Message);
            }
        }

        #endregion


    }

  
}
