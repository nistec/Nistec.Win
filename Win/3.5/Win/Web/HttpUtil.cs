using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Web.Util;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using System.Security.Cryptography;
using System.Security.Permissions;


namespace MControl.Web
{
    /// <summary>
    /// HttpRequest
    /// </summary>
    public class HttpUtil
    {
        #region members
        /// <summary>
        /// Http WebRequest
        /// </summary>
        private HttpWebRequest request;
        /// <summary>
        /// postData
        /// </summary>
        private string m_postData;
        /// <summary>
        /// AsyncWorker
        /// </summary>
        public event EventHandler AsyncWorker;
        /// <summary>
        /// ManualReset
        /// </summary>
        public ManualResetEvent ManualReset = new ManualResetEvent(false);
        /// <summary>
        /// m_codePage
        /// </summary>
        private string m_CodePage;
        /// <summary>
        /// m_CodePageNum
        /// </summary>
        private int m_CodePageNum;
        /// <summary>
        /// ContentType
        /// </summary>
        private string m_ContentType;

        private bool m_IsUrlEncoded;

        private bool m_IsXml;

        private string m_url;

        private WebExceptionStatus m_WebExceptionStatus;

        private HttpStatusCode m_HttpStatusCode;

        private string m_HttpStatusDescription;

        private WebException m_Exception;

        //public bool ContentLengthByByte = false;
        #endregion

        #region ctor
        /// <summary>
        /// Initilaized new instance of HttpRequest class
        /// </summary>
        /// <param name="url"></param>
        public HttpUtil(string url):this(url,"POST")
        {
        }
        /// <summary>
        /// Initilaized new instance of HttpRequest class
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        public HttpUtil(string url, string method)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            m_url = url;
            m_ContentType = "application/x-www-form-urlencoded";
            m_CodePage = "utf-8";
            m_IsUrlEncoded = false;
            m_IsXml = false;
        }

        /// <summary>
        /// Initilaized new instance of HttpRequest class
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        public HttpUtil(string url, string method, string codePage, bool isXml)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            m_url = url;
            if (isXml)
                m_ContentType = "text/xml; charset=" + codePage;
            else
                m_ContentType = "application/x-www-form-urlencoded";
            m_CodePage = codePage;
            m_IsXml = isXml;
        }


        #endregion

        #region properties
        /// <summary>
        /// Get HttpWebRequest
        /// </summary>
        public HttpWebRequest HttpWebRequest
        {
            get
            {
                return request;
            }
        }

        /// <summary>
        /// Get or Set UrlEncoded
        /// </summary>
        public bool IsUrlEncoded
        {
            get
            {
                return m_IsUrlEncoded;
            }
            set { m_IsUrlEncoded = value; }
        }
        /// <summary>
        /// Get or Set IsXml
        /// </summary>
        public bool IsXml
        {
            get
            {
                return m_IsXml;
            }
            set { m_IsXml = value; }
        }
        /// <summary>
        /// Get or Set ContentType
        /// </summary>
        public string ContentType
        {
            get
            {
                return m_ContentType;
            }
            set { m_ContentType = value; }
        }
        /// <summary>
        /// Get the WebExceptionStatus
        /// </summary>
        public WebExceptionStatus ExceptionStatus
        {
            get
            {
                return m_WebExceptionStatus;
            }
        }
        /// <summary>
        /// Get the HttpStatusCode
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return m_HttpStatusCode;
            }
        }
        /// <summary>
        /// Get the HttpStatus Description
        /// </summary>
        public string HttpStatusDescription
        {
            get
            {
                return m_HttpStatusDescription;
            }
        }

        /// <summary>
        /// Get the WebException
        /// </summary>
        public WebException HttpWebException
        {
            get
            {
                return m_Exception;
            }
        }

        #endregion

        #region private handles

        private string HandleWebExcption(WebException webExcp)
        {
            Console.WriteLine("A WebException has been caught.");
            m_Exception = webExcp;
            m_WebExceptionStatus = webExcp.Status;


            // If status is WebExceptionStatus.ProtocolError, 
            //   there has been a protocol error and a WebResponse 
            //   should exist. Display the protocol error.
            if (m_WebExceptionStatus == WebExceptionStatus.ProtocolError)
            {
                Console.Write("The server returned protocol error ");
                // Get HttpWebResponse so that you can check the HTTP status code.

                HttpWebResponse httpResponse = null;
                StreamReader resStream = null;
                try
                {
                    httpResponse = (HttpWebResponse)webExcp.Response;
                    m_HttpStatusCode = httpResponse.StatusCode;
                    m_HttpStatusDescription = httpResponse.StatusDescription;

                    resStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.GetEncoding(m_CodePage));//.UTF8);
                    return resStream.ReadToEnd();
                }
                catch { }
                finally
                {
                    if (httpResponse != null)
                        httpResponse.Close();
                    if (resStream != null)
                        resStream.Close();
                    httpResponse = null;
                    resStream = null;
                }
            }
            return null;
        }

        private byte[] GetByte(string postData)
        {
            byte[] byteArray = null;

            // Convert the string into a byte array.
            if (m_CodePageNum > 0)
            {
                byteArray = Encoding.GetEncoding(m_CodePageNum).GetBytes(postData);//.UTF8.GetBytes(postData);
            }
            else
            {
                byteArray = Encoding.GetEncoding(m_CodePage).GetBytes(postData);//.UTF8.GetBytes(postData);
            }
            return byteArray;
        }
        #endregion

        #region Async
        /// <summary>
        /// OnAsyncWorker
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAsyncWorker(EventArgs e)
        {
            if (AsyncWorker != null)
                AsyncWorker(this, e);
        }

 

        /// <summary>
        /// Async Request with empty code page
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string AsyncRequest(string postData)
        {
            return AsyncRequest(postData, "");
        }

        /// <summary>
        /// Async Request UTF8
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string AsyncRequestUTF8(string postData)
        {
            return AsyncRequest(postData, "utf-8");
        }

        /// <summary>
        /// Async Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public string AsyncRequest(string postData, string codePage)
        {
            Stream streamResponse = null;
            StreamReader streamRead = null;
            HttpWebResponse response = null;
            string result = null; 

            try
            {
                m_postData = postData;
                // Create a new HttpWebRequest object.
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //string contentType = "application/x-www-form-urlencoded";
                if (!string.IsNullOrEmpty(codePage))
                {
                    m_ContentType += "; charset = " + codePage;
                    m_CodePage = codePage;
                }

                // Set the ContentType property. 
                request.ContentType = m_ContentType;

                // Set the Method property to 'POST' to post data to the URI.
                //request.Method = "POST";
                // Start the asynchronous operation.    
                request.BeginGetRequestStream(new AsyncCallback(ReadCallback), request);

                OnAsyncWorker(EventArgs.Empty);

                // Keep the main thread from continuing while the asynchronous
                // operation completes. A real world application
                // could do something useful such as updating its user interface. 
                ManualReset.WaitOne();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                streamResponse = response.GetResponseStream();
                streamRead = new StreamReader(streamResponse);
                result = streamRead.ReadToEnd();
                Console.WriteLine(result);
                // Close the stream object.
                //streamResponse.Close();
                //streamRead.Close();

                // Release the HttpWebResponse.
                //response.Close();
                m_WebExceptionStatus = WebExceptionStatus.Success;

            }
            catch (System.Net.WebException webExcp)
            {
                result = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
                throw ex;
            }
            finally
            {
                // Close the stream object.
                if (streamResponse != null)
                    streamResponse.Close();
                if (streamRead != null)
                    streamRead.Close();
                // Release the HttpWebResponse.
                if (response != null)
                    response.Close();
                streamResponse = null;
                streamRead = null;
                response = null;
            }
            return result;
        }


        /// <summary>
        /// AsyncRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public string AsyncRequest(string postData, int codePage)
        {
            Stream streamResponse = null;
            StreamReader streamRead = null;
            HttpWebResponse response = null;
            string result = null;
            try
            {

                m_postData = postData;
                // Create a new HttpWebRequest object.
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //string contentType = "application/x-www-form-urlencoded";
                m_CodePageNum = codePage;
                m_ContentType += "; charset = " + codePage.ToString();


                // Set the ContentType property. 
                request.ContentType = m_ContentType;


                // Set the Method property to 'POST' to post data to the URI.
                //request.Method = "POST";
                // Start the asynchronous operation.    
                request.BeginGetRequestStream(new AsyncCallback(ReadCallback), request);

                OnAsyncWorker(EventArgs.Empty);

                // Keep the main thread from continuing while the asynchronous
                // operation completes. A real world application
                // could do something useful such as updating its user interface. 
                ManualReset.WaitOne();

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                streamResponse = response.GetResponseStream();
                streamRead = new StreamReader(streamResponse);

                result = streamRead.ReadToEnd();
                Console.WriteLine(result);
                // Close the stream object.
                //streamResponse.Close();
                //streamRead.Close();

                // Release the HttpWebResponse.
                //response.Close();

                m_WebExceptionStatus = WebExceptionStatus.Success;

            }
            catch (System.Net.WebException webExcp)
            {
                result = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
                throw ex;
            }
            finally
            {
                // Close the stream object.
                if (streamResponse != null)
                    streamResponse.Close();
                if (streamRead != null)
                    streamRead.Close();
                // Release the HttpWebResponse.
                if (response != null)
                    response.Close();
                streamResponse = null;
                streamRead = null;
                response = null;
            }
            return result;
        }

        /// <summary>
        /// ReadCallback
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            Stream postStream = null;
            //bool shouldManualReset = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                // End the operation.
                postStream = request.EndGetRequestStream(asynchronousResult);

                //Console.WriteLine("Please enter the input data to be posted:");
                //string postData = Console.ReadLine ();

                byte[] byteArray = GetByte(m_postData); 

                // Convert the string into a byte array.
                //if (m_CodePageNum > 0)
                //{
                //    byteArray = Encoding.GetEncoding(m_CodePageNum).GetBytes(m_postData);//.UTF8.GetBytes(postData);
                //}
                //else
                //{
                //    byteArray = Encoding.GetEncoding(m_CodePage).GetBytes(m_postData);//.UTF8.GetBytes(postData);
                //}

                // Write to the request stream.

                //if (ContentLengthByByte)
                //{
                //    //option B
                //    request.ContentLength = byteArray.Length;
                //    postStream.Write(byteArray, 0, byteArray.Length);
                //}
                //else
                //{
                //option A
                postStream.Write(byteArray, 0, m_postData.Length);
                //}
                //shouldManualReset = true;
                //postStream.Close();
                //postStream = null;
                //ManualReset.Set();
            }
            //catch (System.Net.WebException webExcp)
            //{
            //    //
            //}
            catch //(Exception ex)
            {
                //throw ex;
            }
            finally
            {
                if (postStream != null)
                    postStream.Close();
                postStream = null;
                //if (shouldManualReset)
                //{
                    ManualReset.Set();
                //}
            }
        }
        #endregion

        #region Sync
        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="method">POST or GET</param>
        /// <returns></returns>
        public string DoRequest(string postData)
        {
            return DoRequest(postData, "utf-8");
        }

        /// <summary>
        /// DoRequest
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="maxRetry"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public string DoRequest(string postData, string codePage, int maxRetry, int delay)
        {
            //WebException exc = null;
            int retry = 0;
            string result = null;
            do
            {
                try
                {
                    retry++;
                    //exc = null;
                    if (retry > 1)
                    {
                        request = (HttpWebRequest)WebRequest.Create(m_url);
                        m_HttpStatusCode = HttpStatusCode.OK;
                        m_HttpStatusDescription = "";
                    }
                    result = DoRequest(postData, codePage);

                    if (ExceptionStatus == WebExceptionStatus.Success)
                        return result;

                    if (retry < maxRetry)
                    {
                        Thread.Sleep(delay);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            } while (/*exc != null &&*/ retry < maxRetry);

            //if (exc != null)
            //{
            //    throw exc;
            //}
            //throw new Exception("UnExpected  error");

            return result;

        }

 
#if(false)

        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        public string DoRequest(string postData,string codePage)
        {
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            string result = null;
            try
            {
                request.Method = "POST";
                //ASCIIEncoding encoding = new ASCIIEncoding();
                //byte[] byteArray = encoding.GetBytes(postData);

                if (!string.IsNullOrEmpty(codePage))
                {
                    m_ContentType += "; charset = " + codePage;
                    m_CodePage = codePage;
                }
                // Set the ContentType property. 
                request.ContentType = m_ContentType;

                //byte[] byteArray = Encoding.GetEncoding(codePage).GetBytes(postData);

                byte[] byteArray = Encoding.GetEncoding(m_CodePage).GetBytes(postData);//.UTF8.GetBytes(postData);

                // Set the content type of the data being posted.
                //request.ContentType = "application/x-www-form-urlencoded;charset = " + codePage;//utf-8";

                // Set the content length of the string being posted.
                request.ContentLength = byteArray.Length;
                newStream = request.GetRequestStream();

                newStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                newStream.Close();
                newStream = null;

                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(m_CodePage));//.UTF8);
                result = readStream.ReadToEnd();
                response.Close();
                m_WebExceptionStatus = WebExceptionStatus.Success;

            }
            catch (System.Net.WebException webExcp)
            {
                result = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
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
            return result;

        }

               /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        public string DoRequest(string postData, int codePage)
        {
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            string result = null;
            try
            {
                request.Method = "POST";
                //ASCIIEncoding encoding = new ASCIIEncoding();
                //byte[] byteArray = encoding.GetBytes(postData);

                m_ContentType += "; charset = " + codePage.ToString();

                // Set the ContentType property. 
                request.ContentType = m_ContentType;

                // Convert the string into a byte array.
                byte[] byteArray = Encoding.GetEncoding(codePage).GetBytes(postData);//.UTF8.GetBytes(postData);

                // Set the content type of the data being posted.
                //request.ContentType = "application/x-www-form-urlencoded;charset = " + codePage;//utf-8";

                // Set the content length of the string being posted.
                request.ContentLength = byteArray.Length;
                newStream = request.GetRequestStream();

                newStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                newStream.Close();
                newStream = null;

                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(codePage));//.UTF8);
                result = readStream.ReadToEnd();
                response.Close();
                m_WebExceptionStatus = WebExceptionStatus.Success;

            }
            catch (System.Net.WebException webExcp)
            {
                result = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
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
            return result;
        }

#endif

        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        public string DoRequest(string postData, string codePage)
        {

            return DoRequest(m_url, postData, "POST", codePage, ContentType, IsUrlEncoded, 120000);
/*
            string response = null;
            WebRequest request = null;
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            WebResponse wresponse = null;

            try
            {
                if (string.IsNullOrEmpty(codePage))
                    codePage = "utf-8";
                Encoding enc = Encoding.GetEncoding(codePage);
                if (IsUrlEncoded)
                {
                    postData = System.Web.HttpUtility.UrlEncode(postData, enc);
                }
                request = WebRequest.Create(m_url);
                request.Timeout = 120000;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                byte[] byteArray = enc.GetBytes(postData);
                request.ContentType = ContentType;// "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                // Get the response.
                wresponse = request.GetResponse();
                receiveStream = wresponse.GetResponseStream();
                readStream = new StreamReader(receiveStream, enc);
                response = readStream.ReadToEnd();

                m_WebExceptionStatus = WebExceptionStatus.Success;

                return response;
            }
            catch (System.Net.WebException webExcp)
            {
                response = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
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
            return response;
*/
        }

  

         #endregion

        #region SSL
#if(false)
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
        /// <param name="xmlPrefix"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DoPostSSL(string url, string xmlPrefix, string data)
        {
            return DoPostSSL(url,null, null, xmlPrefix, data, "utf-8");
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
        public static string DoPostSSL(string url, string user, string password, string xmlPrefix, string data, string codePage)
        {
            string res = "";
            WebRequest request = null;
            WebResponse Response = null;
            Stream InputStream = null;
            Stream OutputStream = null;
            StreamReader streamReader = null;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

                request = WebRequest.Create(url);
                request.Method = "POST";

                string contentType = "application/x-www-form-urlencoded";

                if (string.IsNullOrEmpty(codePage))
                {
                    codePage = "utf-8";
                }
                contentType += "; charset = " + codePage;

                // Set the ContentType property. 
                request.ContentType = contentType;

                request.PreAuthenticate = true;
                
                //request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                
                if (user != null && password != null)
                {
                    request.Credentials = new NetworkCredential(user, password);
                }
                byte[] bytes;
                //bytes = System.Text.Encoding.UTF8.GetBytes(xmlPrefix + System.Web.HttpUtility.UrlEncode(data, System.Text.Encoding.UTF8));
                bytes = System.Text.Encoding.GetEncoding(codePage).GetBytes(xmlPrefix + System.Web.HttpUtility.UrlEncode(data, System.Text.Encoding.GetEncoding(codePage)));
                request.ContentLength = bytes.Length;
                OutputStream = request.GetRequestStream();
                OutputStream.Write(bytes, 0, bytes.Length);
                OutputStream.Close();


                Response = request.GetResponse();
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
#endif
        #endregion

        #region static
  
        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        public static string DoGet(string url, string postData, string codePage)
        {
            Stream receiveStream = null;
            StreamReader readStream = null;
            try
            {

                string qs = string.IsNullOrEmpty(postData) ? "" : "?" + postData;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + qs);
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(codePage));//.UTF8);

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
                if (receiveStream != null)
                    receiveStream.Close();
                if (readStream != null)
                    readStream.Close();
            }
        }

    
        public static string DoRequest(string url, string postData, string method, string encoding)
        {
            return DoRequest(url, postData, method, encoding, null, false, 100000);
        }
        public static string DoRequest(string url, string postData, string method, string encoding,  int timeout)
        {
            return DoRequest(url, postData, method, encoding, null, false, timeout);
        }

        public static string DoRequest(string url, string postData,string method, string encoding, string contentType, bool isUrlEncoded, int timeout)
        {

            string response = null;

            WebRequest request = null;
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            WebResponse wresponse = null;

            try
            {
                Encoding enc = Encoding.GetEncoding(encoding);
                if (isUrlEncoded)
                {
                    postData = System.Web.HttpUtility.UrlEncode(postData, enc);
                    if (string.IsNullOrEmpty(contentType))
                    {
                        contentType = "application/x-www-form-urlencoded";
                    }
                }

                if (method.ToUpper() == "GET")
                {
                    string qs = string.IsNullOrEmpty(postData) ? "" : "?" + postData;

                    request = WebRequest.Create(url + qs);
                    request.Timeout = timeout <= 0 ? 100000 : timeout;
                    if (!string.IsNullOrEmpty(contentType))
                        request.ContentType = contentType;// string.IsNullOrEmpty(contentType) ? "application/x-www-form-urlencoded" : contentType;

                }
                else
                {
                    request = WebRequest.Create(url);
                    request.Method = "POST";
                    request.Credentials = CredentialCache.DefaultCredentials;

                    request.Timeout = timeout <= 0 ? 100000 : timeout;
                    request.ContentType = string.IsNullOrEmpty(contentType) ? "application/x-www-form-urlencoded" : contentType;

                    byte[] byteArray = enc.GetBytes(postData);
                    request.ContentLength = byteArray.Length;

                    newStream = request.GetRequestStream();
                    newStream.Write(byteArray, 0, byteArray.Length);
                    newStream.Close();

                }

 
                // Get the response.
                wresponse = request.GetResponse();
                receiveStream = wresponse.GetResponseStream();
                readStream = new StreamReader(receiveStream, enc);
                response = readStream.ReadToEnd();

                return response;
            }
            catch (System.Net.WebException webExcp)
            {
                throw webExcp;
            }
            catch (System.IO.IOException ioe)
            {
                throw ioe;
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

        public static string DoRequestSSL(string url, string postData, string encoding, int timeout, string user, string pass)
        {
            return DoRequestSSL(url, postData, encoding, null, false, timeout, user, pass);
        }

        public static string DoRequestSSL(string url, string postData, string encoding, string contentType, bool isUrlEncoded,int timeout, string user, string pass) 
        {

            string response = null;

            WebRequest request = null;
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            WebResponse wresponse = null;
            try
            {

                TrustedCertificatePolicy policy = new TrustedCertificatePolicy();

                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

                Encoding enc = Encoding.GetEncoding(encoding);
                if (isUrlEncoded)
                {
                    postData = System.Web.HttpUtility.UrlEncode(postData, enc);
                }
                request = WebRequest.Create(url);
                request.PreAuthenticate = true;
                request.Credentials = new NetworkCredential(user, pass);
                //request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                request.Timeout = timeout <= 0 ? 100000 : timeout;
                request.ContentType = string.IsNullOrEmpty(contentType) ? "application/x-www-form-urlencoded" : contentType;

                byte[] byteArray = enc.GetBytes(postData);// Encoding.GetEncoding("Windows-1255").GetBytes(postData);
               //request.ContentType = "text/xml";

                request.ContentLength = byteArray.Length;
                newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                // Get the response.
                wresponse = request.GetResponse();
                receiveStream = wresponse.GetResponseStream();
                readStream = new StreamReader(receiveStream, enc);
                response = readStream.ReadToEnd();

                return response;
            }
            catch (System.Net.WebException webExcp)
            {
                throw webExcp;
            }
            catch (System.IO.IOException ioe)
            {
                throw ioe;
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

        public static bool CheckValidationResult(Object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public class TrustedCertificatePolicy : System.Net.ICertificatePolicy
        {
            public TrustedCertificatePolicy() { }

            public bool CheckValidationResult
            (
                System.Net.ServicePoint sp,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Net.WebRequest request, int problem)
            {
                return true;
            }
        }
        #endregion

        #region _Obsolete
#if(false)
        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        [Obsolete("Use DoRequest insted")]
        public static string DoPost(string url, string postData, string codePage)
        {
            string contentType = "application/x-www-form-urlencoded";

            return DoRequest(url, postData, "POST", codePage, contentType, false, 120000);
            /*
                        Stream newStream = null;
                        Stream receiveStream = null;
                        StreamReader readStream = null;
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                            request.Method = "POST";
                            //ASCIIEncoding encoding = new ASCIIEncoding();
                            //byte[] byteArray = encoding.GetBytes(postData);

                            string contentType = "application/x-www-form-urlencoded";

                            if (string.IsNullOrEmpty(codePage))
                            {
                                codePage = "utf-8";
                            }
                            contentType += "; charset = " + codePage;

                            // Set the ContentType property. 
                            request.ContentType = contentType;

                            byte[] byteArray = Encoding.GetEncoding(codePage).GetBytes(postData);

                            // Set the content type of the data being posted.
                            //request.ContentType = "application/x-www-form-urlencoded;charset = " + codePage;//utf-8";

                            // Set the content length of the string being posted.
                            request.ContentLength = byteArray.Length;
                            newStream = request.GetRequestStream();

                            newStream.Write(byteArray, 0, byteArray.Length);

                            // Close the Stream object.
                            newStream.Close();
                            newStream = null;

                            request.Credentials = CredentialCache.DefaultCredentials;

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                            // Get the stream associated with the response.
                            receiveStream = response.GetResponseStream();

                            // Pipes the stream to a higher level stream reader with the required encoding format. 
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(codePage));//.UTF8);

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
             */
        }

        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <param name="contentType">codePage</param>
        /// <returns></returns>
        [Obsolete("Use DoRequest insted")]
        public static string DoPost(string url, string postData, string codePage, string contentType)
        {
            return DoRequest(url, postData, "POST", codePage, contentType, false, 120000);
        }

        /// <summary>
        /// DoRequest
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="maxRetry"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        [Obsolete("Use DoPost(string postData, string codePage, int maxRetry, int delay) insted")]
        public string DoRequest(string postData, int codePage, int maxRetry, int delay)
        {
            //WebException exc = null;
            int retry = 0;
            string result = null;

            do
            {
                try
                {
                    retry++;
                    //exc = null;
                    if (retry > 1)
                    {
                        request = (HttpWebRequest)WebRequest.Create(m_url);
                        m_HttpStatusCode = HttpStatusCode.OK;
                        m_HttpStatusDescription = "";

                    }
                    result = DoRequest(postData, codePage);
                    if (ExceptionStatus == WebExceptionStatus.Success)
                        return result;

                    if (retry < maxRetry)
                    {
                        Thread.Sleep(delay);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            } while (/*exc != null &&*/ retry < maxRetry);

            //if (exc != null)
            //{
            //    throw exc;
            //}
            //throw new Exception("UnExpected  error");

            return result;

        }


        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        [Obsolete("Use DoPost(string postData, string codePage) insted")]
        public string DoRequest(string postData, int codePage)
        {

            string response = null;
            WebRequest request = null;
            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            WebResponse wresponse = null;

            try
            {
                Encoding enc = Encoding.GetEncoding(codePage);
                if (IsUrlEncoded)
                {
                    postData = System.Web.HttpUtility.UrlEncode(postData, enc);
                }
                request = WebRequest.Create(m_url);
                request.Timeout = 120000;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                byte[] byteArray = enc.GetBytes(postData);
                request.ContentType = ContentType;// "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                // Get the response.
                wresponse = request.GetResponse();
                receiveStream = wresponse.GetResponseStream();
                readStream = new StreamReader(receiveStream, enc);
                response = readStream.ReadToEnd();

                m_WebExceptionStatus = WebExceptionStatus.Success;

                return response;
            }
            catch (System.Net.WebException webExcp)
            {
                response = HandleWebExcption(webExcp);
            }
            catch (Exception ex)
            {
                m_WebExceptionStatus = WebExceptionStatus.UnknownError;
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
            return response;

        }

        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        [Obsolete("Use DoPost(string url, string postData, string codePage) insted")]
        public static string DoPost(string url, string postData, int codePage)
        {


            Stream newStream = null;
            Stream receiveStream = null;
            StreamReader readStream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                string contentType = "application/x-www-form-urlencoded";
                contentType += "; charset = " + codePage.ToString();

                // Set the ContentType property. 
                request.ContentType = contentType;

                byte[] byteArray = Encoding.GetEncoding(codePage).GetBytes(postData);

                // Set the content type of the data being posted.
                //request.ContentType = "application/x-www-form-urlencoded;charset = " + codePage;//utf-8";

                // Set the content length of the string being posted.
                request.ContentLength = byteArray.Length;
                request.Credentials = CredentialCache.DefaultCredentials;
                newStream = request.GetRequestStream();

                newStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                newStream.Close();
                newStream = null;


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(codePage));//.UTF8);

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

        /// <summary>
        /// Send HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="codePage">codePage</param>
        /// <returns></returns>
        [Obsolete("Use DoGet(string url, string postData, string codePage) insted")]
        public static string DoGet(string url, string postData, int codePage)
        {
            Stream receiveStream = null;
            StreamReader readStream = null;
            try
            {
                string qs = string.IsNullOrEmpty(postData) ? "" : "?" + postData;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + qs);
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(codePage));//.UTF8);

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
                if (receiveStream != null)
                    receiveStream.Close();
                if (readStream != null)
                    readStream.Close();
            }
        }
#endif
        #endregion

    }

}

