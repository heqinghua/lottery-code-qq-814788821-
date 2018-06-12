using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace com.mobaopay.merchant
{
    /// <summary>
    /// 类名： MobaopayMerchant
    /// 功能： 工具类，方便组织请求支付串、组织请求url、发送请求到支付网关和验证网关回复签名。
    /// 
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，
    /// 按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public sealed class MobaopayMerchant
    {
        private static readonly MobaopayMerchant instance = new MobaopayMerchant();
        public static MobaopayMerchant Instance
        {
            get { return instance; }
        }

        private MobaopayMerchant()
        { }

        /// <summary>
        /// 验证响应数据签名，失败则抛出异常
        /// </summary>
        /// <param name="result"></param>
        private void checkResult(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回数据为空。");
            }

            // 载入xml
            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(result);

            // 获取签名源字符串
            XmlNode respDataNode = resultDoc.SelectSingleNode("/moboAccount/respData");
            if (null == respDataNode)
            {
                throw new Exception("回复数据格式不正确，不存在/moboAccount/respData节点。");
            }
            String respData = respDataNode.OuterXml;
            // 获取签名字符串
            XmlNode signMsgNode = resultDoc.SelectSingleNode("/moboAccount/signMsg");
            if (null == signMsgNode)
            {
                throw new Exception("回复数据格式不正确，不存在/moboAccount/signMsg节点。");
            }
            string signMsg = signMsgNode.InnerText;

            // 验证签名
            if (string.IsNullOrEmpty(respData) || string.IsNullOrEmpty(signMsg))
            {
                throw new Exception("回复数据格式不正确，源字符串或签名串不为空。");
            }
            else
            {
                respData = respData.Replace(" />", "/>");
            }

            if (!MobaopaySignUtil.Instance.verifyData(signMsg, respData))
            {
                throw new Exception("回复数据签名验证失败。");
            }
        }


        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="requestStr">待发送源数据</param>
        /// <param name="serverUrl">服务地址</param>
        /// <returns></returns>
        public string transact(string requestStr, string serverUrl)
        {
            if (string.IsNullOrEmpty(requestStr))
            {
                throw new Exception("请求数据不能为空。");
            }
            if (string.IsNullOrEmpty(serverUrl))
            {
                throw new Exception("请求地址不能为空。");
            }

            string checkUrl = serverUrl.ToLower();
            bool isHttps = true;
            if (!checkUrl.StartsWith("https"))
            {
                //throw new Exception("URL地址必须以https开头");
                isHttps = false;
            }

            requestStr = requestStr.Replace("\\+", "%2B");
            var postData = Encoding.UTF8.GetBytes(requestStr);

            // 收发数据
            HttpWebRequest request = null;
            if (isHttps)
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(serverUrl) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(serverUrl) as HttpWebRequest;
            }
            request.Method = "POST";
            request.Timeout = 60000;
            request.AllowAutoRedirect = true;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.1) Gecko/20061204 Firefox/2.0.0.3";
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.KeepAlive = false;
            request.ContentLength = postData.Length;
            request.ServicePoint.Expect100Continue = false;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Flush();
            }

            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream streamReceive = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(streamReceive, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }

            // 验证收到的数据
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回参数错误！");
            }
            checkResult(result);
            return result;
        }

        //public string generatePayUrl(Dictionary<string, string> paramsDict, string payReqUrl)
        //{
        //    string myParams = string.Format("?apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&merchUrl={7}&merchParam={8}&tradeSummary={9}&signMsg={10}",
        //        paramsDict["apiName"],
        //        paramsDict["apiVersion"],
        //        paramsDict["platformID"],
        //        paramsDict["merchNo"],
        //        paramsDict["orderNo"],
        //        paramsDict["tradeDate"],
        //        paramsDict["amt"],
        //        System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["merchUrl"])),
        //        System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["merchParam"])),
        //        System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["tradeSummary"])),
        //        paramsDict["signMsg"]);
        //    return payReqUrl + myParams;
        //}

        /// <summary>
        /// 生成退款请求数据
        /// </summary>
        /// <param name="paramsDict"></param>
        /// <returns></returns>
        public string generateRefundRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
            {
                throw new Exception("tradeSummary不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string tradeSummary = sourceData["tradeSummary"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&tradeSummary={7}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, tradeSummary);
            return result;
        }

        /// <summary>
        /// 生成订单查询请求数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public string generateQueryRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt);
            return result;
        }

        /// <summary>
        /// 获取支付请求数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public string generatePayRequest(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }
            if (!sourceData.ContainsKey("merchUrl") || string.IsNullOrEmpty(sourceData["merchUrl"]))
            {
                throw new Exception("merchUrl不能为空");
            }
            if (!sourceData.ContainsKey("merchParam"))
            {
                throw new Exception("merchParam可以为空，但必须存在！");
            }
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
            {
                throw new Exception("tradeSummary不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string merchUrl = sourceData["merchUrl"];
            string merchParam = sourceData["merchParam"]; // System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(sourceData["merchParam"]));
            string tradeSummary = sourceData["tradeSummary"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&merchUrl={7}&merchParam={8}&tradeSummary={9}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, merchUrl, merchParam, tradeSummary);
            return result;
        }
    }
}
