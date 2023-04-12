using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace MControl.Web
{
    public class SoapRequest
    {
        public const string XmlDeclartion = @"<?xml version=""1.0"" encoding=""utf-8""?>";

        public static string NormelaizeXml(string xml)
        {
            Regex regex = new Regex(@">\s*<");
            xml = regex.Replace(xml, "><");
            return xml.Replace("\r\n", "").Replace("\n", "").Trim();
        }

        /// <summary>
        /// Execute Soap request
        /// </summary>
        public static string ExecSoapRequest(string url,
                                            int timeout,
                                            string action,
                                            string soapBody,
                                            out int statusCode,
                                            out string statusDescription)
         {
            string result = null;
            statusDescription = "Unknwon";
            statusCode = 0;
 
            string soapHeaderContent = string.Empty;
            try
            {
                if (timeout == 0)
                    timeout = 120000;

                //Log.DebugFormat("SoapRequest ,url:{0}, timeout:{1}, action:{2}", url, timeout, action);

                 HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";
                //request.Accept = "text/xml";
                request.Timeout = timeout;
                request.KeepAlive = false;
                request.UseDefaultCredentials = true;
                if (!string.IsNullOrEmpty(action))
                {
                    request.Headers["SOAPAction"] = action;
                }

                byte[] bytes = Encoding.UTF8.GetBytes(soapBody);

                request.ContentLength = bytes.Length;

                using (Stream OutputStream = request.GetRequestStream())
                {
                    if (!OutputStream.CanWrite)
                    {
                        throw new Exception("Could not wirte to RequestStream");
                    }
                    OutputStream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse resp = request.GetResponse())
                {
                    using (Stream ResponseStream = resp.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(ResponseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }
                    }
                }
                statusDescription = "Success";

            }
            catch (WebException wex)
            {
                //Log.Exception("SoapRequest WebException: ", wex);//, wex.InnerException);
                statusCode = (int)wex.Status;
                statusDescription = wex.Message;
            }
            catch (Exception ex)
            {
                //Log.Exception("SoapRequest Exception :", ex);//, ex.InnerException);
                statusCode = -1;
                statusDescription = ex.Message;
            }

            return result;
        }


    }
}
