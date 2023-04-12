﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MControl;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace MControl.Generic
{
    public interface IGenericArgs
    {
        string[] ToArray();
 
        int Length{get;}
    }

    public class GenericArgs : Dictionary<string, string>
    {
        public const string ArgName = "carg";

        public GenericArgs()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }

        public static GenericArgs Empty
        {
            get { return new GenericArgs(); }
        }

        public static bool ParseKeyValue(string msg, out string key, out string value)
        {
            key = msg;
            value = msg;
            try
            {
                const string pattern = @"^\s*(?<Keyword>\w+)\s*(?<Value>.*)";
                Regex regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                // Replace invalid characters with empty strings.
                //string cleanText= regex.Replace(Txt, @"[^\w\.@-]", ""); 

                Match m = regex.Match(msg);

                if (m.Success)
                {
                    key = m.Groups[1].Value;
                    value = m.Groups[2].Value;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #region properties

        public string Get(string key)
        {
            if (this.ContainsKey(key))
                return this[key];
            return "";

        }

        public string GetAlter(params string[] keys)
        {
            foreach (string key in keys)
            {
                if (this.ContainsKey(key))
                    return this[key];
            }

            return "";
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (this.ContainsKey(key))
                return GenericTypes.Convert<T>(this[key], defaultValue);
            return defaultValue;

        }

        public T Get<T>(string key)
        {
            if (this.ContainsKey(key))
                return GenericTypes.Convert<T>(this[key]);
            return GenericTypes.Default<T>();

        }

        public string Get(string key, bool decypt)
        {
            string value = Get(key);
            if (string.IsNullOrEmpty(value))
                return "";
            if (decypt)
            {
                return MControl.Sys.RquestQuery.DecryptEx32(value);
            }
            return value;
        }
        

        //public new string this[string key]
        //{
        //    get
        //    {
        //        if (this.ContainsKey(key))
        //            return this[key];
        //        return "";
        //    }
        //    set
        //    {
        //        this[key] = value;
        //    }
        //}

        #endregion

        #region encode/decode

        public static string EncodeArgs(string qs)
        {
            return MControl.Sys.BaseConverter.ToBase32(qs);
        }
        public static GenericArgs DecodeArgs(string cargs)
        {
            string qs= MControl.Sys.BaseConverter.FromBase32(cargs);
            return GenericArgs.ParseCommand(qs);
        }
        public static string EncodeRequestArgs(string qs)
        {
            return EncodeArgs("?" + ArgName + "=" + qs);
        }
        public static string EncodeRequestArgs(GenericArgs arg)
        {
            return EncodeArgs("?" + ArgsToQueryString(arg));
        }
        public static GenericArgs DecodeRequestArgs(System.Web.HttpRequest request)
        {
            string arg = request.QueryString[ArgName];
            if (string.IsNullOrEmpty(arg))
            {
                return GenericArgs.ParseCommand(arg);
            }
            return DecodeArgs(arg);
        }
        #endregion

        #region ParseArgs

        public static string[] ParseArgs(string args, char splitter)
        {

            if (string.IsNullOrEmpty(args))
            {
                return null;
            }
            string[] strArray = args.Split(new char[] { splitter });


            return strArray;
        }

        public static string[] ParseArgs(string args)
        {

            if (string.IsNullOrEmpty(args))
            {
                return null;
            }
            string[] strArray = args.Split(new char[] { '|', ';', ',' });


            return strArray;
        }

        public static string ToArgs(IGenericArgs args)
        {
            if (args == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();

            foreach (string s in args.ToArray())
            {
                sb.Append(s + "|");
            }

            sb = sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
       

        public static GenericArgs ParseCommand(string cmd)
        {
            GenericArgs dictionary = new GenericArgs();

            if (cmd == null)
                cmd = string.Empty;
            cmd = cmd.ToLowerInvariant();

            string str = cmd;
 
            if (string.IsNullOrEmpty(cmd))
            {
                return dictionary;
            }

            //var dictionary = new Dictionary<string, string>();
            foreach (string str3 in str.Split(new char[] { ';','|' }))
            {
                if (!string.IsNullOrEmpty(str3))
                {
                    string[] strArray = str3.Split(new char[] { '=' });
                    if (strArray.Length == 2)
                    {
                        dictionary[strArray[0]] = strArray[1];
                    }
                    else
                    {
                        dictionary[str3] = null;
                    }
                }
            }

            return dictionary;
        }


        public static GenericArgs ParseRequest(System.Web.HttpRequest request)
        {

            if (request == null)
            {
                throw new ArgumentException("invalid request");
            }
            return ParseRequest( request.RawUrl);
        }

        public static GenericArgs ParseRequest(string url)
        {
   
            if (url == null)
                url = string.Empty;
            //url = url.ToLowerInvariant();

            string qs = string.Empty;
             
            if (url.Contains("?"))
            {
                qs = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }

            return ParseQueryString(qs);
        }
        #endregion

        #region converter

        public static string QueryStringFormat(string pattern, params object[] args)
        {
            return string.Format(pattern, args);
        }

        public static string ArgsToQueryString(GenericArgs args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> entry in args)
            {
                sb.AppendFormat("&{0}={1}", entry.Key, entry.Value);
            }

            sb = sb.Remove(0, 1);

            return sb.ToString();
        }
        #endregion

        #region ParseQueryString

        public static GenericArgs ParseQueryString(params string[] qs)
        {
            StringBuilder sb = new StringBuilder();
            int len=qs.Length;
            for (int i = 0; i < qs.Length; i++)
            {
                sb.Append(qs[i]);
                if (i < len - 1 && !qs[i].EndsWith("&") && !qs[i + 1].StartsWith("&"))
                {
                    sb.Append("&");
                }
            }

           return ParseQueryString(sb.ToString());
        }

        public static string CLeanQueryString(string qs)
        {
            return qs.Replace("&amp;","&");
        }

        public static GenericArgs ParseQueryString(string qs)
        {
            GenericArgs dictionary = new GenericArgs();

            if (qs == null)
                qs = string.Empty;
            //url = url.ToLowerInvariant();

            string str = CLeanQueryString(qs);

            if (string.IsNullOrEmpty(str))
            {
                return dictionary;
            }
            if (!str.Contains('='))
            {
                return dictionary;
            }
            //var dictionary = new Dictionary<string, string>();
            foreach (string arg in str.Split(new char[] { '&' }))
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    string[] strArray = arg.Split(new char[] { '=' });
                    if (strArray.Length == 2)
                    {
                        string key = Regx.RegexReplace("amp;", strArray[0],"");
                        dictionary[key] = strArray[1];
                    }
                    else
                    {
                        dictionary[arg] = null;
                    }
                }
            }

            return dictionary;
        }
        public static GenericArgs ParseQueryString(NameValueCollection qs)
        {
            GenericArgs dictionary = new GenericArgs();
            if (qs != null)
            {
                for (int i = 0; i < qs.Count; i++)
                {
                    dictionary[qs.Keys[i]] = qs[i];
                }
            }

            return dictionary;
        }
        #endregion

        #region SplitArgs

        public static bool SplitArgs<T1, T2>(string args, char splitter, ref T1 arg1, ref T2 arg2)
        {
            //arg1 = default(T1);
            //arg2 = default(T2);

            if (args == null)
            {
                return false;
            }
            string[] list = args.Split(splitter);
            
            if (list == null || list.Length == 0)
            {
                return false;
            }

            arg1 = GenericTypes.Convert<T1>(list[0]);
            if (list.Length > 1)
                arg2 = GenericTypes.Convert<T2>(list[1]);
            return true;
        }
        public static bool SplitArgs<T1, T2, T3>(string args, char splitter, ref T1 arg1, ref T2 arg2, ref T3 arg3)
        {
            //arg1 = default(T1);
            //arg2 = default(T2);
            //arg3 = default(T3);
            if (args == null)
            {
                return false;
            }
            string[] list = args.Split(splitter);

            if (list == null || list.Length == 0)
            {
                return false;
            }

            arg1 = GenericTypes.Convert<T1>(list[0]);
            if (list.Length > 1)
                arg2 = GenericTypes.Convert<T2>(list[1]);
            if (list.Length > 2)
                arg3 = GenericTypes.Convert<T3>(list[2]);
            return true;
        }
        public static bool SplitArgs<T1, T2, T3, T4>(string args, char splitter, ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4)
        {
            //arg1 = default(T1);
            //arg2 = default(T2);
            //arg3 = default(T3);
            //arg4 = default(T4);
            if (args == null)
            {
                return false;
            }
            string[] list = args.Split(splitter);

            if (list == null || list.Length == 0)
            {
                return false;
            }

            arg1 = GenericTypes.Convert<T1>(list[0]);
            if (list.Length > 1)
                arg2 = GenericTypes.Convert<T2>(list[1]);
            if (list.Length > 2)
                arg3 = GenericTypes.Convert<T3>(list[2]);
            if (list.Length > 3)
                arg4 = GenericTypes.Convert<T4>(list[3]);
            return true;
        }
        public static bool SplitArgs<T1, T2, T3, T4, T5>(string args, char splitter, ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5)
        {
            //arg1 = default(T1);
            //arg2 = default(T2);
            //arg3 = default(T3);
            //arg4 = default(T4);
            if (args == null)
            {
                return false;
            }
            string[] list = args.Split(splitter);

            if (list == null || list.Length == 0)
            {
                return false;
            }

            arg1 = GenericTypes.Convert<T1>(list[0]);
            if (list.Length > 1)
                arg2 = GenericTypes.Convert<T2>(list[1]);
            if (list.Length > 2)
                arg3 = GenericTypes.Convert<T3>(list[2]);
            if (list.Length > 3)
                arg4 = GenericTypes.Convert<T4>(list[3]);
            if (list.Length > 4)
                arg5 = GenericTypes.Convert<T5>(list[4]);
            return true;
        }

        #endregion
    }
}
