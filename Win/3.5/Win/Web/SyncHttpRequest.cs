using System;
using System.Net; 
using System.Text;
using System.IO;  
using System.Web.Util;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace mControl.Web
{

    /// <summary>
    /// Create web request
    /// </summary>
    public class SyncHttpRequest
    {

  
        /// <summary>
        /// Send HttpWebRequest POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Send(string url, string postData)
        {
            return Send(url, postData, "POST");
        }
        
        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="method">POST or GET</param>
        /// <returns></returns>
        public static string Send(string url, string postData, string method)
        {
            HttpWebRequest request = null;
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            try
            {
                if (method == "POST")
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    //ASCIIEncoding encoding = new ASCIIEncoding();
                    //byte[] byteArray = encoding.GetBytes(postData);

                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                    // Set the content type of the data being posted.
                    request.ContentType = "application/x-www-form-urlencoded;charset = utf-8";

                    // Set the content length of the string being posted.
                    request.ContentLength = byteArray.Length;
                    newStream = request.GetRequestStream();

                    newStream.Write(byteArray, 0, byteArray.Length);

                    // Close the Stream object.
                    newStream.Close();
                    newStream = null;

                    request.Credentials = CredentialCache.DefaultCredentials;
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url + "?" + postData);
                    request.Method = "GET";
                }


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string result = readStream.ReadToEnd();
                response.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (newStream != null)
                    newStream.Close();
                if (receiveStream != null)
                    receiveStream.Close();
                if (readStream != null)
                    readStream.Close();
            }
        }

        public static bool CheckValidationResult(Object sender,
                               X509Certificate cert,
                               X509Chain chain,
                               SslPolicyErrors sslErrors)
        {
            return true;
        }

        /// <summary>
        /// send Http Post WebRequest with SSL
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlPrefix"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SendPostSSL(string user, string password, string xmlPrefix, string url, string data)
        {
            string res = "";
            WebRequest Request = null;
            WebResponse Response = null;
            Stream InputStream = null;
            Stream OutputStream = null;
            StreamReader streamReader = null;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                
                Request = WebRequest.Create(url);
                Request.Method = "POST";
                Request.PreAuthenticate = true;
                Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                if (user != null && password != null)
                {
                    Request.Credentials = new NetworkCredential(user, password);
                }
                byte[] bytes;
                bytes = System.Text.Encoding.UTF8.GetBytes(xmlPrefix + System.Web.HttpUtility.UrlEncode(data, System.Text.Encoding.UTF8));
                Request.ContentLength = bytes.Length;
                OutputStream = Request.GetRequestStream();
                OutputStream.Write(bytes, 0, bytes.Length);
                OutputStream.Close();


                Response = Request.GetResponse();
                InputStream = Response.GetResponseStream();
                streamReader = new StreamReader(InputStream);

                res = streamReader.ReadToEnd();

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Response != null)
                    Response.Close();
                if (InputStream != null)
                    InputStream.Close();
                if (streamReader != null)
                    streamReader.Close();

            }
        }


        //public static string SendHttpGET(string url, string data, out bool hResult)
        //{
        //    string htmlContent = "";

        //    try
        //    {
        //        WebRequest Request = null;
        //        WebResponse Response = null;
        //        string Urltemp = "";
        //        Urltemp = url + "?" + data;
        //        Request = WebRequest.Create(Urltemp);
        //        Request.Method = "GET";    

        //        Response = Request.GetResponse();
        //        Stream InputStream = Response.GetResponseStream();
        //        StreamReader ReadStream = new StreamReader(InputStream);

        //        htmlContent = ReadStream.ReadToEnd();
        //        Response.Close();
        //        InputStream.Close();
        //        ReadStream.Close();

        //        hResult = true;
        //        return htmlContent;
        //    }
        //    catch (Exception Ex)
        //    {
        //        hResult = false;
        //        return Ex.Message;
        //    }
        //}

        //public static string SendHttpPOST(string url, string data, out bool hResult)
        //{
        //    string htmlContent = "";

        //    try
        //    {
        //        WebRequest Request = null;
        //        WebResponse Response = null;
        //        Request = WebRequest.Create(url);
        //        Request.Method = "POST";   
        //        Request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

        //        byte[] bytes;
        //        bytes = System.Text.Encoding.UTF8.GetBytes(System.Web.HttpUtility.UrlEncode(data, System.Text.Encoding.UTF8));
        //        Request.ContentLength = bytes.Length;
        //        Stream OutputStream = Request.GetRequestStream();
        //        OutputStream.Write(bytes, 0, bytes.Length);
        //        OutputStream.Close();

        //        Response = Request.GetResponse();
        //        Stream InputStream = Response.GetResponseStream();
        //        StreamReader ReadStream = new StreamReader(InputStream);

        //        htmlContent = ReadStream.ReadToEnd();
        //        Response.Close();
        //        InputStream.Close();
        //        ReadStream.Close();

        //        hResult = true;
        //        return htmlContent;
        //    }
        //    catch (Exception Ex)
        //    {
        //        hResult = false;
        //        return Ex.Message;
        //    }
        //}

	
        //public static string SendMSXML(string url, string data, WebSendMode mode,out int hResult)
        //{
        //    string htmlContent = "";    
			
        //    try
        //    {
				 
        //        ServerXMLHTTP40Class   xmlHttp = new ServerXMLHTTP40Class(); 
				
        //        xmlHttp.open("POST",url,false,null,null);
        //        xmlHttp.setRequestHeader ("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
        //        xmlHttp.send (data);
        //        htmlContent = xmlHttp.responseText; 
        //        hResult = 0;
        //        return htmlContent;
        //    }
        //    catch(Exception Ex)
        //    {
        //        hResult = -10001;
        //        return Ex.Message; 
        //    }
        //}
	}
}
