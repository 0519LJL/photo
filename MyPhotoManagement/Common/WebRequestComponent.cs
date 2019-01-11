using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyPhotoManagement.Common
{
    public class WebRequestComponent
    {
        /// <summary>
        /// 请求 url 方法,提供参数对并返回文本结果。(无异常,出错或无内容则为空)
        /// <para>上下文赋值 ExecutionContext: </para>
        /// <para>请求数据：RequestData(string), 状态码：HttpStatusCode(int), 异 常：WebException(WebException)</para>
        /// </summary>
        /// <param name="parameters">请求参数 键值对</param>
        /// <param name="heads">请求head键值对</param>
        /// <param name="method">请求方式 GET/POST 默认POST</param>
        /// <returns></returns>
        public static string HttpPostWithContext(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string method = "POST", string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var request = GetWebRequest(url, method, heads, contentType, accept, needXML);
            var requestData = AttachPostParameter(parameters);

            if (ExecutionContext.Current != null)
                ExecutionContext.Current.SetValue("RequestData", requestData);

            var postData = Encoding.UTF8.GetBytes(requestData);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (ExecutionContext.Current != null)
                {
                    ExecutionContext.Current.SetValue("HttpStatusCode", response != null ? response.StatusCode.GetHashCode() : -1);
                    //清空旧的异常对象
                    ExecutionContext.Current.SetValue("WebException", null);
                }
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                if (ExecutionContext.Current != null)
                {
                    ExecutionContext.Current.SetValue("HttpStatusCode", response != null ? response.StatusCode.GetHashCode() : -1);
                    ExecutionContext.Current.SetValue("WebException", ex);
                }
            }

            if (response == null || response.ContentLength == 0)
            {
                return string.Empty;
            }
            else
            {
                var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
                Encoding responseEncoding = Encoding.GetEncoding(characterSet);
                return GetResponseAsString(response, responseEncoding);
            }
        }

        /// </summary>
        /// <param name="parameters">请求参数 键值对</param>
        /// <param name="heads">请求head键值对</param>
        /// <param name="method">请求方式 GET/POST 默认POST</param>
        public static string HttpPost(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string method = "POST", string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var request = GetWebRequest(url, method, heads, contentType, accept, needXML);
            var requestData = AttachPostParameter(parameters);
            var postData = Encoding.UTF8.GetBytes(requestData);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentLength == 0)
                {
                    return string.Empty;
                }
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                if (response == null || response.StatusCode != HttpStatusCode.BadRequest)
                {
                    throw;
                }
            }
            var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
            Encoding responseEncoding = Encoding.GetEncoding(characterSet);
            return GetResponseAsString(response, responseEncoding);
        }

        /// <summary>
        /// 请求 url 方法, 整串数据
        /// </summary>
        /// <param name="parameters">请求参数 键值对</param>
        /// <param name="heads">请求head键值对</param>
        /// <param name="method">请求方式 GET/POST 默认POST</param>
        public static string HttpPost(string url, string requestData, Dictionary<string, object> heads = null, bool needXML = false, string method = "POST", string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var request = GetWebRequest(url, method, heads, contentType, accept, needXML);
            var postData = Encoding.UTF8.GetBytes(requestData);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response.ContentLength == 0)
            {
                return string.Empty;
            }

            var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
            Encoding responseEncoding = Encoding.GetEncoding(characterSet);
            return GetResponseAsString(response, responseEncoding);
        }

        /// <summary>
        /// 请求 url 方法, 文件上传
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="parameters">请求参数 键值对</param>
        /// <param name="heads">请求head键值对</param>
        /// <param name="method">请求方式 GET/POST 默认POST</param>
        public static string HttpPost(string url, byte[] bytes, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string method = "POST", string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            /* 字节流转成string */
            if (bytes != null && bytes.Length > 0)
            {
                if (parameters == null)
                {
                    parameters = new Dictionary<string, object>();
                }

                string byteToString = Convert.ToBase64String(bytes);
                parameters.Add("byteToString", byteToString);
            }

            var request = GetWebRequest(url, method, heads, contentType, accept, needXML);

            var requestData = AttachPostParameter(parameters);
            var postData = Encoding.UTF8.GetBytes(requestData);

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response.ContentLength == 0)
            {
                return string.Empty;
            }

            var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
            Encoding responseEncoding = Encoding.GetEncoding(characterSet);
            return GetResponseAsString(response, responseEncoding);
        }


        /// <summary>
        /// 请求 url 方法,参数对,请求方式 GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="heads"></param>
        /// <param name="needXML"></param>
        /// <param name="contentType"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var requestData = AttachPostParameter(parameters);
            url += url.LastIndexOf("?") > 0 ? requestData : (requestData == "" ? "" : "?") + requestData;
            var request = GetWebRequest(url, "Get", heads, contentType, accept, needXML);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentLength == 0)
                {
                    return string.Empty;
                }
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (response == null || response.StatusCode != HttpStatusCode.BadRequest)
                {
                    throw;
                }
            }

            var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
            Encoding responseEncoding = Encoding.GetEncoding(characterSet);
            return GetResponseAsString(response, responseEncoding);
        }

        public static byte[] HttpGetToByte(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var requestData = AttachPostParameter(parameters);
            url += url.LastIndexOf("?") > 0 ? requestData : (requestData == "" ? "" : "?") + requestData;
            var request = GetWebRequest(url, "Get", heads, contentType, accept, needXML);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentLength <= 0)
                    return default(byte[]);

                int totalLength = (int)response.ContentLength;
                int numBytesRead = 0;
                byte[] bytes = new byte[totalLength + 1024];
                //通过一个循环读取流中的数据，读取完毕，跳出循环
                using (Stream st = response.GetResponseStream())
                {

                    while (numBytesRead < totalLength)
                    {
                        int num = st.Read(bytes, numBytesRead, 1024);  //每次希望读取1024字节
                        if (num == 0)   //说明流中数据读取完毕
                            break;
                        numBytesRead += num;
                    }
                }
                return bytes;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (response == null || response.StatusCode != HttpStatusCode.BadRequest)
                {
                    throw;
                }
            }
            return default(byte[]);
        }

        public static string HttpGetString(string url, string requestData, Dictionary<string, object> heads = null, bool needXML = false, string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            url += url.LastIndexOf("?") > 0 ? requestData : (requestData == "" ? "" : "?") + requestData;
            var request = GetWebRequest(url, "Get", heads, contentType, accept, needXML);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentLength == 0)
                {
                    return string.Empty;
                }
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (response == null || response.StatusCode != HttpStatusCode.BadRequest)
                {
                    throw;
                }
            }

            var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
            Encoding responseEncoding = Encoding.GetEncoding(characterSet);
            return GetResponseAsString(response, responseEncoding);
        }

        /// <summary>
        /// 组装请求
        /// </summary>
        /// <param name="timeOut">超时时间 单位分钟 默认3</param>
        private static HttpWebRequest GetWebRequest(string url, string method, Dictionary<string, object> heads, string contentType, string accept = "text/html,application/xml,*/*", bool needXML = false, int timeOut = 3)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.UserAgent = "idh";
            request.Accept = accept;
            if (string.IsNullOrWhiteSpace(contentType))
            {
                if (needXML)
                {
                    request.ContentType = "text/xml";
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
            }
            else
            {
                request.ContentType = contentType;
            }


            request.ServicePoint.Expect100Continue = false;
            request.KeepAlive = true;
            /* 超时时间 */
            request.Timeout = timeOut * 60 * 1000;

            if (heads != null)
            {
                foreach (var head in heads)
                {
                    string name = head.Key;
                    string value = head.Value.ToString();

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                    {
                        continue;
                    }

                    request.Headers.Add(name, value);
                }
            }

            return request;
        }

        /// <summary>
        /// 组装请求参数
        /// </summary>
        public static string AttachPostParameter(Dictionary<string, object> parameters)
        {
            var postData = new StringBuilder();
            bool hasAndParameter = false;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    string name = parameter.Key;
                    if (string.IsNullOrEmpty(name))
                        continue;
                    string value = parameter.Value == null ? "" : parameter.Value.ToString();
                    if (hasAndParameter)
                        postData.Append("&");
                    postData.Append(name);
                    postData.Append("=");
                    if (!string.IsNullOrEmpty(value))
                        postData.Append(HttpUtility.UrlEncode(value));
                    hasAndParameter = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 组装响应流成文本
        /// </summary>
        private static string GetResponseAsString(HttpWebResponse request, Encoding encoding)
        {
            using (var stream = request.GetResponseStream())
            {
                var reader = new System.IO.StreamReader(stream, encoding);

                var str = reader.ReadToEnd();

                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (request != null) request.Close();

                return str;
            }
        }

        /// <summary>
        /// 读取文件字节流
        /// </summary>
        public static byte[] GetBytesByPath(string path, bool isRelative = true)
        {
            string pathServer;
            if (isRelative)
            {
                var server = HttpContext.Current.Server;
                pathServer = server.MapPath(path);
            }
            else
            {
                pathServer = path;
            }

            byte[] bytes = null;

            if (!File.Exists(pathServer))
            {
                return bytes;
            }

            using (FileStream fs = new FileStream(pathServer, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((int)fs.Length);
                }
            }

            return bytes;
        }

        /// <summary>
        /// WebException 处理返回具体的错误消息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <summary>
        /// 请求 url 方法,提供参数对并返回文本结果。(无异常,出错或无内容则为空)，兼容数组
        /// <para>上下文赋值 ExecutionContext: </para>
        /// <para>请求数据：RequestData(string), 状态码：HttpStatusCode(int), 异 常：WebException(WebException)</para>
        /// </summary>
        /// <param name="parameters">请求参数 键值对</param>
        /// <param name="heads">请求head键值对</param>
        /// <param name="method">请求方式 GET/POST 默认POST</param>
        /// <returns></returns>

        /// <summary>
        /// 组装请求参数，兼容数组
        /// </summary>
        public static string AttachPostParameterEx(Dictionary<string, object> parameters)
        {
            var postData = new StringBuilder();
            bool hasAndParameter = false;

            Action<string, string> actPost = (name, value) => {
                if (hasAndParameter)
                    postData.Append("&");

                postData.Append(name);
                postData.Append("=");
                if (!string.IsNullOrEmpty(value))
                    postData.Append(HttpUtility.UrlEncode(value));

                hasAndParameter = true;
            };

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    string name = parameter.Key;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (parameter.Value != null && parameter.Value.GetType().IsArray)
                    {
                        var arr = parameter.Value as object[];

                        foreach (var arrItem in arr)
                        {
                            if (arrItem != null)
                            {
                                actPost.Invoke(name, arrItem.ToString());
                            }
                        }
                    }
                    else
                    {
                        string value = parameter.Value == null ? "" : parameter.Value.ToString();
                        actPost.Invoke(name, value);
                    }

                }
            }
            return postData.ToString();
        }

        public static string HttpPostWithContextEx(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> heads = null, bool needXML = false, string method = "POST", string contentType = "", string accept = "text/html,application/xml,*/*")
        {
            var request = GetWebRequest(url, method, heads, contentType, accept, needXML);
            var requestData = AttachPostParameterEx(parameters);

            if (ExecutionContext.Current != null)
                ExecutionContext.Current.SetValue("RequestData", requestData);

            var postData = Encoding.UTF8.GetBytes(requestData);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (ExecutionContext.Current != null)
                {
                    ExecutionContext.Current.SetValue("HttpStatusCode", response != null ? response.StatusCode.GetHashCode() : -1);
                    //清空旧的异常对象
                    ExecutionContext.Current.SetValue("WebException", null);
                }
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                if (ExecutionContext.Current != null)
                {
                    ExecutionContext.Current.SetValue("HttpStatusCode", response != null ? response.StatusCode.GetHashCode() : -1);
                    ExecutionContext.Current.SetValue("WebException", ex);
                }
            }

            if (response == null || response.ContentLength == 0)
            {
                return string.Empty;
            }
            else
            {
                var characterSet = string.IsNullOrEmpty(response.CharacterSet) ? "UTF-8" : response.CharacterSet;
                Encoding responseEncoding = Encoding.GetEncoding(characterSet);
                return GetResponseAsString(response, responseEncoding);
            }
        }

        public static string WebExceptionHandle(WebException ex)
        {
            var webResponse = (System.Net.HttpWebResponse)ex.Response;

            var characterSet = string.IsNullOrEmpty(webResponse.CharacterSet) ? "UTF-8" : webResponse.CharacterSet;

            var encoding = Encoding.GetEncoding(characterSet);

            using (var stream = webResponse.GetResponseStream())
            {
                using (var reader = new System.IO.StreamReader(stream, encoding))
                {
                    var str = reader.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(str))
                        str = ex.ToString();

                    if (webResponse != null)
                        webResponse.Close();

                    return str;
                }
            }
        }

    }
}