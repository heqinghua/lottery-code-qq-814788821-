using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace com.mobaopay.merchant
{
    /// <summary>
    /// 类名： MobaopayQueryEntity
    /// 功能： 查询回复数据实体类，为了方便读取回复数据中的元素，将回复数据解析到该类中
    /// 
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，
    /// 按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public class MobaopayQueryEntity
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

        private string accDate;
        public string AccDate
        {
            get { return accDate; }
        }

        private string accNo;
        public string AccNo
        {
            get { return accNo; }
        }

        private string orderNo;
        public string OrderNo
        {
            get { return orderNo; }
        }

        private string status;
        public string Status
        {
            get { return status; }
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
                throw new Exception("响应信息格式错误：不存在'respDesc'节点。");
            }
            else
            {
                this.respDesc = respDescElt.InnerText;
            }

            // 根据响应码判断是否继续往下解析
            if ("00".Equals(respCode))
            {

                XmlNode accDateElt = doc.SelectSingleNode("/moboAccount/respData/accDate");
                if (null == accDateElt)
                {
                    throw new Exception("响应信息格式错误：不存在'accDate'节点。");
                }
                else
                {
                    this.accDate = accDateElt.InnerText;
                }

                XmlNode accNoElt = doc.SelectSingleNode("/moboAccount/respData/accNo");
                if (null == accNoElt)
                {
                    throw new Exception("响应信息格式错误：不存在'accNo'节点。");
                }
                else
                {
                    this.accNo = accNoElt.InnerText;
                }

                XmlNode orderNoElt = doc.SelectSingleNode("/moboAccount/respData/orderNo");
                if (null == orderNoElt)
                {
                    throw new Exception("响应信息格式错误：不存在'orderNo'节点。");
                }
                else
                {
                    this.orderNo = orderNoElt.InnerText;
                }

                XmlNode statusElt = doc.SelectSingleNode("/moboAccount/respData/Status");
                if (null == statusElt)
                {
                    throw new Exception("响应信息格式错误：不存在'status'节点。");
                }
                else
                {
                    this.status = statusElt.InnerText;
                }
            }

            XmlNode signMsgElt = doc.SelectSingleNode("/moboAccount/signMsg");
            if (null == signMsgElt)
            {
                throw new Exception("响应信息格式错误：不存在'signMsg'节点。");
            }
            else
            {
                this.signMsg = signMsgElt.InnerText;
            }

        }
    }
}