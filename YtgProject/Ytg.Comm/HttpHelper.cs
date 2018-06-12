using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Ytg.Comm
{
    public class HttpHelper
    {
        /// <summary>
        /// HTTP获取请求结果
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <returns></returns>
        public static string HttpGetRequest(string url, NameValueCollection parameters)
        {
            string rString = string.Empty;
            StreamReader sr = null;
            HttpWebResponse res = null;
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                string param = RequestString(parameters, myEncoding);
                var posturl = string.Format("{0}?{1}", url, param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(posturl);
                req.Method = "GET";
                res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                    rString = sr.ReadLine(); 
                }
            }
            catch {}
            finally
            {
                if (res != null)
                {
                    res.Close();
                    sr.Close();
                }
            }
            return rString;
        }

        /// <summary>
        /// http Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string HttpPostRequest(string url, NameValueCollection parameters)
        {
            string rString = string.Empty;
            StreamReader sr = null;
            HttpWebResponse res = null;
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                string param = RequestString(parameters, myEncoding);
                byte[] postBytes = Encoding.Default.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded,charset=gb2312";
                req.ContentLength = postBytes.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                    reqStream.Close();
                }
                res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                    rString = sr.ReadToEnd().ToString();
                }
            }
            catch 
            {}
            finally
            {
                res.Close();
                sr.Close();
            }
            return rString;
        }

        /// <summary>
        /// URL参数处理方法
        /// </summary>
        /// <param name="parameters">URL参数</param>
        /// <param name="myEncoding">编码名称</param>
        /// <returns></returns>
        private static string RequestString(NameValueCollection parameters, Encoding myEncoding)
        {
            var param = "";
            foreach (string key in parameters.Keys)
            {
                param += string.Format("{0}={1}&", HttpUtility.UrlEncode(key, myEncoding), HttpUtility.UrlEncode(parameters[key], myEncoding));
            }
            if (param.Length > decimal.Zero)
            {
                param = param.Substring(0, param.Length - 1);
            }
            return param;
        }
    
    
    
    }
}
