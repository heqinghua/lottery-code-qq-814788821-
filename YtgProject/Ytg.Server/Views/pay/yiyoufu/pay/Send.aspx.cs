using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.pay.yiyoufu.pay
{
    public partial class Send : System.Web.UI.Page
    {
        /**
         点卡提交地址   http://Gate.ekepay.com/cardReceive.aspx  
    网银提交地址   http://Gate.ekepay.com/paybank.aspx
    微信提交地址   http://Gate.ekepay.com/paybank.aspx   
    支付宝提交地址 http://Gate.ekepay.com/paybank.aspx

         */

        const string PayUnUrl = "http://Gate.ekepay.com/paybank.aspx";//网银
        const string WeChartUrl = "http://Gate.ekepay.com/paybank.aspx";//微信
        const string ZhifuBaoUrl = "http://Gate.ekepay.com/paybank.aspx";//支付宝

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
                if (null == item)
                {
                    Response.End();
                    return;
                }
                if (item.IsCompled || !item.IsEnable)
                {
                    Response.Write("请勿重复提交订单！");
                    return;
                }

                Eka365pay(item.MY18FY, "http://" + Request.Url.Host + ":" + Request.Url.Port + "/views/pay/yiyoufu/pay/Receive.aspx", item);
                //流程工作完成后调用Eka365pay方法，参数为：（String 订单号，String 返回地址--这里默认为：http://"+Request.Url.Host+":"+Request.Url.Port+"/Eka365pay/"+"Receive.aspx）
                //实例：Eka365pay("012345674980123456", "http://" + Request.Url.Host + ":" + Request.Url.Port + "/Eka365pay/" + "Receive.aspx");

            }

        }

        /// <summary>
        /// 易优付支付
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="callBackurl">返回地址</param>
        private void Eka365pay(String orderid, String callBackurl, RecordTemp item)
        {
            //商户信息
            String shop_id = ConfigurationManager.AppSettings["ekaid"]; //商户ID
            String key = ConfigurationManager.AppSettings["ekakey"]; //商户密钥

            var userAmt = Math.Round(item.TradeAmt, 2).ToString();
            if (userAmt.IndexOf(".") < 0)
                userAmt = userAmt + ".00";
            //银行提交获取信息
            String bank_Type = item.MY18PT; //Request.Form["rtype"];//银行类型
            String bank_gameAccount = item.Guid; //Request.Form["txtUserName"];//充值账号
            String bank_payMoney = userAmt;// Request.Form["PayMoney"];//充值金额
            //银行卡支付
            String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", shop_id, bank_Type, bank_payMoney, orderid, callBackurl);


            string pUrl = PayUnUrl;//默认为网银支付
            if (item.MY18PT == "992")//支付宝
                pUrl = ZhifuBaoUrl;
            else if (item.MY18PT == "993")//位置
                pUrl = WeChartUrl;
            else
                pUrl = PayUnUrl;

            String PostUrl = String.Format("{0}?{1}&sign={2}", pUrl, param, FormsAuthentication.HashPasswordForStoringInConfigFile(param + key, "MD5").ToLower());
            Response.Redirect(PostUrl);//转发URL地址


            ////组织接口发送。
            //if (String.IsNullOrEmpty(Request.Form["card"]))
            //{
            //    //银行提交获取信息
            //    String bank_Type = Request.Form["rtype"];//银行类型
            //    String bank_gameAccount = Request.Form["txtUserName"];//充值账号
            //    String bank_payMoney = Request.Form["PayMoney"];//充值金额
            //    //银行卡支付
            //    String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", shop_id, bank_Type, bank_payMoney, orderid, callBackurl);
            //    String PostUrl = String.Format("http://www.10001000.com:14433/chargebank.aspx?{0}&sign={1}", param, FormsAuthentication.HashPasswordForStoringInConfigFile(param + key, "MD5").ToLower());
            //    Response.Redirect(PostUrl);//转发URL地址
            //}
            //else
            //{
            //    //获取卡类提交信息
            //    String card_No = Request.Form["cardNo"];//卡号
            //    String card_pwd = Request.Form["cardPwd"];//卡密
            //    String card_account = Request.Form["txtUserNameCard"];//充值账号
            //    String card_type = Request.Form["sel_card"].Split('，')[0];//卡类型
            //    String card_payMoney = Request.Form["sel_price"];//充值金额
            //    String restrict = "0";//使用范围
            //    String attach = "test";//附加内容，下行原样返回
            //    if (Request.Form["sel_card"].Split(',').Length > 1)
            //    {
            //        restrict = Request.Form["sel_card"].Split(',')[1];
            //    }
            //    //卡类支付
            //    String param = String.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}", card_type, shop_id, card_No, card_pwd, card_payMoney, restrict, orderid, callBackurl);
            //    String PostUrl = String.Format("http://www.10001000.com:14433/cardReceive.aspx?{0}&attach={1}&sign={2}", param, attach, FormsAuthentication.HashPasswordForStoringInConfigFile(param + key, "MD5").ToLower());

            //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
            //    //获取响应结果 此过程大概需要5秒
            //    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //    //获取响应流
            //    Stream stream = httpWebResponse.GetResponseStream();
            //    //用指定的字符编码为指定的流初始化 StreamReader 类的一个新实例。
            //    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
            //    {
            //        string useResult = streamReader.ReadToEnd();
            //        streamReader.Dispose();
            //        streamReader.Close();
            //        httpWebResponse.Close();

            //        if (useResult.Trim() == "opstate=0")
            //        {
            //            Response.Write("已经记录该卡，正在等待被使用.");
            //        }
            //        if (useResult.Trim() == "opstate=-1")
            //        {
            //            Response.Write("请求参数无效。");
            //        }
            //        if (useResult.Trim() == "opstate=-2")
            //        {
            //            Response.Write("签名错误。");
            //        }
            //        if (useResult.Trim() == "opstate=-3")
            //        {
            //            Response.Write("提交的卡密为重复提交，系统不进行消耗并进入下行流程。");
            //        } 
            //        if (useResult.Trim() == "opstate=-4")
            //        {
            //             Response.Write("提交的卡密不符合易优付定义的卡号密码面值规则。");
            //            //提交的卡密不符合易优付定义的卡号密码面值规则。
            //        }
            //        if (useResult.Trim() == "opstate=-999")
            //        {
            //             Response.Write("接口维护中。");
            //            ////这里把定单状态接口维护中。
            //        }
            //    }
            //}
        }
    }
}