using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace com.mobaopay.merchant
{
    /// <summary>
    /// 类名： MobaopayRefundEntity
    /// 功能： 退款回复数据实体类，为了方便读取回复数据中的元素，将回复数据解析到该类中
    /// 
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，
    /// 按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public class MobaopayRefundEntity
    {
        private string respCode;
        public string RespCode
        {
            get { return respCode; }
        }

        private string respDesc;
        public string RespDesc
        {
            get { return respDesc; }
        }

        private string signMsg;
        public string SignMsg
        {
            get { return signMsg; }
        }

        public void Parse(string resp)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resp);

            XmlNode respCodeElt = doc.SelectSingleNode("/moboAccount/respData/respCode");
            if (null == respCodeElt)
            {
                throw new Exception("响应信息格式错误：不存在'respCode'节点。");
            }
            else
            {
                this.respCode = respCodeElt.InnerText;
            }

            XmlNode respDescElt = doc.SelectSingleNode("/moboAccount/respData/respDesc");
            if (null == respDescElt)
            {
                throw new Exception("响应信息格式错误：不存在'respDesc'节点");
            }
            else
            {
                this.respDesc = respDescElt.InnerText;
            }

            XmlNode signMsgElt = doc.SelectSingleNode("/moboAccount/signMsg");
            if (null == signMsgElt)
            {
                throw new Exception("响应信息格式错误：不存在'signMsg'节点");
                
            }
            else
            {
                this.signMsg = signMsgElt.InnerText;
            }
        }
    }
}