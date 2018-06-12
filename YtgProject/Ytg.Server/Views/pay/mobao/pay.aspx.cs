using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ytg.Core.Service;
using Ytg.Comm;

namespace com.mobaopay.merchant
{
    public partial class pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["tok"]))
                {
                    Response.End();
                    return;
                }
                //构建支付链接
                //根据订单唯一id获取订单信息
                IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                var item = recordService.GetAll().Where(c => c.Guid == Request.QueryString["tok"]).FirstOrDefault();
                if (null == item )
                {
                    Response.End();
                    return;
                }
                if (item.IsCompled || !item.IsEnable)
                {
                    Response.Write("请勿重复提交订单！");
                    return;
                }

                var userAmt = Math.Round(item.TradeAmt, 2).ToString();
                if (userAmt.IndexOf(".") < 0)
                    userAmt = userAmt + ".00";
                // 组织数据和签名
                Dictionary<string, string> payData = new Dictionary<string, string>();
                payData.Add("apiName", MobaopayConfig.Mobaopay_apiname_pay);
                payData.Add("apiVersion", MobaopayConfig.Mobaopay_api_version);
                payData.Add("platformID", MobaopayConfig.Platform_id);
                payData.Add("merchNo", MobaopayConfig.Merchant_acc);
                payData.Add("orderNo", item.MY18FY);
                payData.Add("tradeDate", item.OccDate.ToString("yyyyMMdd"));
                payData.Add("amt", userAmt);//string.Format("{0:N}", item.TradeAmt));
                payData.Add("merchUrl", MobaopayConfig.Merchant_notify_url);
                payData.Add("merchParam", "");//Request.Form["merchParam"]
                payData.Add("tradeSummary", "PayBill");//item.MY18FY
                /**
                 * bankCode为空，提交表单后浏览器在新窗口显示摩宝支付收银台页面，在这里可以通过账户余额支付或者选择银行支付；
                 * bankCode不为空，取值只能是接口文档中列举的银行代码，提交表单后浏览器将在新窗口直接打开选中银行的支付页面。
                 * 无论选择上面两种方式中的哪一种，支付成功后收到的通知都是同一接口。
                 */
                payData.Add("bankCode", item.MY18PT);

                string requestStr = MobaopayMerchant.Instance.generatePayRequest(payData);  // 组织签名源数据
                payData.Add("signMsg", MobaopaySignUtil.Instance.sign(requestStr));         // 生成签名数据

                // 生成表单并自动提交到支付网关。
                StringBuilder sbHtml = new StringBuilder();
                sbHtml.Append("<form id='mobaopaysubmit' name='mobaopaysubmit' action='" + MobaopayConfig.Mobaopay_gateway + "?' method='post'>");
                foreach (KeyValuePair<string, string> temp in payData)
                {
                    sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
                }
                sbHtml.Append("</form>");
                sbHtml.Append("<script>document.forms['mobaopaysubmit'].submit();</script>");
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(sbHtml.ToString());
            }
        }
    }
}