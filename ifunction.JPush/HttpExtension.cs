using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ifunction.JPush.V3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ifunction
{
    /// <summary>
    /// Class HttpExtensions.
    /// </summary>
    internal static class HttpExtensions
    {
        #region Read response

        #region As Text

        /// <summary>
        /// Reads the response as text.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        public static string ReadResponseAsText(this HttpWebRequest httpWebRequest, Encoding encoding = null)
        {
            HttpStatusCode statusCode;
            return ReadResponseAsText(httpWebRequest, encoding, out statusCode);
        }

        /// <summary>
        /// Reads the response as text.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns>System.String.</returns>
        public static string ReadResponseAsText(this HttpWebRequest httpWebRequest, Encoding encoding, out HttpStatusCode statusCode)
        {
            WebHeaderCollection headers;
            return ReadResponseAsText(httpWebRequest, encoding, out statusCode, out headers);
        }

        /// <summary>
        /// Reads the response as text.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">ReadResponseAsText</exception>
        public static string ReadResponseAsText(this HttpWebRequest httpWebRequest, Encoding encoding, out HttpStatusCode statusCode, out WebHeaderCollection headers)
        {
            statusCode = default(HttpStatusCode);
            string result = string.Empty;
            headers = null;

            if (httpWebRequest != null)
            {
                WebResponse response = null;
                HttpWebResponse webResponse = null;

                try
                {
                    response = httpWebRequest.GetResponse();
                    result = response.ReadAsText(encoding, false);

                    webResponse = (HttpWebResponse)response;
                }
                catch (WebException webEx)
                {
                    webResponse = (HttpWebResponse)webEx.Response;
                    result = webEx.Response.ReadAsText(encoding, false);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("ReadResponseAsText", ex);
                }
                finally
                {
                    if (webResponse != null)
                    {
                        statusCode = webResponse.StatusCode;
                        headers = webResponse.Headers;
                    }

                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets text content from WebResponse by specified encoding.
        /// </summary>
        /// <param name="webResponse">The WebResponse instance.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The content string.</returns>
        /// <exception cref="InvalidOperationException">ReadAsText</exception>
        public static string ReadAsText(this WebResponse webResponse, Encoding encoding = null)
        {
            return ReadAsText(webResponse, encoding, true);
        }

        /// <summary>
        /// Reads as text.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="closeResponse">if set to <c>true</c> [close response].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="InvalidOperationException">ReadAsText</exception>
        private static string ReadAsText(this WebResponse webResponse, Encoding encoding, bool closeResponse)
        {
            string result = string.Empty;

            if (webResponse != null)
            {
                try
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        if (encoding == null)
                        {
                            encoding = Encoding.UTF8;
                        }

                        StreamReader streamReader = new StreamReader(responseStream, encoding, true);
                        result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("ReadAsText", ex);
                }
                finally
                {
                    if (closeResponse)
                    {
                        webResponse.Close();
                    }
                }
            }

            return result;
        }

        #endregion

        #endregion

        #region Fill Data On HttpWebRequest

        /// <summary>
        /// Fills the file data.
        /// Reference: http://stackoverflow.com/questions/566462/upload-files-with-httpwebrequest-multipart-form-data
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="postData">The post data.</param>
        /// <param name="fileCollection">The file collection.
        /// Key: file name. e.g.: sample.txt
        /// Value: file data in byte array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void FillFileData(this HttpWebRequest httpWebRequest, NameValueCollection postData, Dictionary<string, byte[]> fileCollection, string paramName)
        {
            try
            {
                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

                httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                using (var stream = new MemoryStream())
                {

                    byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                    string formDataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

                    if (postData != null)
                    {
                        foreach (string key in postData.Keys)
                        {
                            string formItem = string.Format(formDataTemplate, key, postData[key]);
                            byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(formItem);
                            stream.Write(formItemBytes, 0, formItemBytes.Length);
                        }
                    }

                    stream.Write(boundaryBytes, 0, boundaryBytes.Length);

                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

                    if (fileCollection != null)
                    {
                        foreach (var key in fileCollection.Keys)
                        {
                            string header = string.Format(headerTemplate, paramName, key);
                            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
                            stream.Write(headerBytes, 0, headerBytes.Length);

                            stream.Write(fileCollection[key], 0, fileCollection[key].Length);

                            stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        }
                    }

                    httpWebRequest.ContentLength = stream.Length;
                    stream.Position = 0;
                    byte[] tempBuffer = new byte[stream.Length];
                    stream.Read(tempBuffer, 0, tempBuffer.Length);

                    using (Stream requestStream = httpWebRequest.GetRequestStream())
                    {
                        requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                        requestStream.Flush();
                        requestStream.Close();
                    }

                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("FillFileData", ex);
            }
        }

        /// <summary>
        /// Fills the file data on HTTP web request.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="postData">The post data.</param>
        /// <param name="fileFullName">Full name of the file.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <exception cref="InvalidOperationException">FillFileData</exception>
        public static void FillFileData(this HttpWebRequest httpWebRequest, NameValueCollection postData, string fileFullName, string paramName)
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(fileFullName);
                var fileName = Path.GetFileName(fileFullName);

                Dictionary<string, byte[]> fileCollection = new Dictionary<string, byte[]>();
                fileCollection.Add(fileName, fileData);

                FillFileData(httpWebRequest, postData, fileCollection, paramName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("FillFileData", ex);
            }
        }

        /// <summary>
        /// Fills post data on HttpWebRequest.
        /// </summary>
        /// <param name="httpWebRequest">The HttpWebRequest instance.</param>
        /// <param name="method">The method.</param>
        /// <param name="dataMappings">The data mappings.</param>
        /// <param name="encoding">The encoding.</param>
        public static void FillData(this HttpWebRequest httpWebRequest, string method, Dictionary<string, string> dataMappings, Encoding encoding = null)
        {
            if (httpWebRequest != null)
            {
                if (encoding == null)
                {
                    encoding = Encoding.ASCII;
                }

                StringBuilder sb = new StringBuilder();
                if (dataMappings != null)
                {
                    foreach (string key in dataMappings.Keys)
                    {
                        string value = string.Empty;
                        if (dataMappings[key] != null)
                        {
                            value = dataMappings[key];
                        }
                        sb.Append(key + "=" + value.Trim() + "&");
                    }

                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                byte[] data = encoding.GetBytes(sb.ToString());

                httpWebRequest.Method = method;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = data.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }

        /// <summary>
        /// Fills the data on HTTP web request.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="method">The method.</param>
        /// <param name="data">The data.</param>
        public static void FillData(this HttpWebRequest httpWebRequest, string method, byte[] data)
        {
            if (httpWebRequest != null && data != null)
            {
                httpWebRequest.Method = method;
                httpWebRequest.ContentType = "text/xml; charset=utf-8";
                httpWebRequest.ContentLength = data.Length;
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
            }
        }

        /// <summary>
        /// Fills the data on HTTP web request.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="method">The method.</param>
        /// <param name="data">The data.</param>
        public static void FillData(this HttpWebRequest httpWebRequest, string method, string data)
        {
            FillData(httpWebRequest, method, data, null);
        }

        /// <summary>
        /// Fills the data on HTTP web request.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <param name="method">The method.</param>
        /// <param name="data">The data.</param>
        /// <param name="encodingToByte">The encoding to byte.</param>
        public static void FillData(this HttpWebRequest httpWebRequest, string method, string data, Encoding encodingToByte)
        {
            byte[] byteArray = null;

            if (!string.IsNullOrWhiteSpace(data))
            {
                if (encodingToByte == null)
                {
                    encodingToByte = Encoding.UTF8;
                }
                byteArray = encodingToByte.GetBytes(data);
            }

            FillData(httpWebRequest, method, byteArray);
        }

        #endregion

        /// <summary>
        /// Gets the post data from HTTP web request.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetPostData(this HttpRequest httpRequest)
        {
            byte[] data = null;

            if (httpRequest != null)
            {
                MemoryStream ms = new MemoryStream();
                httpRequest.InputStream.CopyTo(ms);
                data = ms.ToArray();
            }

            return data;
        }

        /// <summary>
        /// To the URL encoded text.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <returns>System.String.</returns>
        public static string ToUrlEncodedText(this string originalText)
        {
            if (originalText != null)
            {
                originalText = HttpUtility.UrlEncode(originalText, Encoding.UTF8);
            }

            return originalText;
        }
    }
}
