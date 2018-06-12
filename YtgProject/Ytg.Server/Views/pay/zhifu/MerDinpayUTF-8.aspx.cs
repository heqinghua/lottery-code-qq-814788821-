using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

using DinpayRSAAPI.COM.Dinpay.RsaUtils;
using Ytg.Core.Service;
using Ytg.Comm;

namespace CSharpTestPay
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

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
                var userAmt = Math.Round(item.TradeAmt, 2).ToString();
                if (userAmt.IndexOf(".") < 0)
                    userAmt = userAmt + ".00";


                try
                {
                    /////////////////////////////////接收表单提交参数//////////////////////////////////////
                    ////////////////////////To receive the parameter form HTML form//////////////////////

                    string input_charset1 = "UTF-8";//Request.Form["input_charset"].ToString().Trim();
                    string interface_version1 = "V3.0";// Request.Form["interface_version"].ToString().Trim();
                    string merchant_code1 = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Merchant_code;//Request.Form["merchant_code"].ToString().Trim();
                    string notify_url1 = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Notify_url;//Request.Form["notify_url"].ToString().Trim();
                    string order_amount1 = userAmt;//Request.Form["order_amount"].ToString().Trim();
                    string order_no1 = item.MY18FY; //Request.Form["order_no"].ToString().Trim();
                    string order_time1 = item.OccDate.ToString("yyyy-MM-dd HH:mm:ss"); //Request.Form["order_time"].ToString().Trim();
                    string sign_type1 = "RSA-S";//Request.Form["sign_type"].ToString().Trim();
                    string product_code1 = "mhpro";//Request.Form["product_code"].ToString().Trim();
                    string product_desc1 = "";//Request.Form["product_desc"].ToString().Trim();
                    string product_name1 = "美亚娱乐在线充值";//Request.Form["product_name"].ToString().Trim();
                    string product_num1 = "1"; //Request.Form["product_num"].ToString().Trim();
                    string return_url1 = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Return_url;//Request.Form["return_url"].ToString().Trim();
                    string service_type1 = "direct_pay";//Request.Form["service_type"].ToString().Trim();
                    string show_url1 = "";//Request.Form["show_url"].ToString().Trim();
                    string extend_param1 ="";//Request.Form["extend_param"].ToString().Trim();
                    string extra_return_param1 = "";//Request.Form["extra_return_param"].ToString().Trim();
                    string bank_code1 = item.MY18PT;//Request.Form["bank_code"].ToString().Trim();
                    string client_ip1 = Utils.GetIp(); //Request.Form["client_ip"].ToString().Trim();
                    string redo_flag1 = "";// Request.Form["redo_flag"].ToString().Trim();
                    string pay_type1 = "";//Request.Form["pay_type"].ToString().Trim();

                    ////////////////组装签名参数//////////////////

                    string signSrc = "";

                    //组织订单信息
                    if (bank_code1 != "")
                    {
                        signSrc = signSrc + "bank_code=" + bank_code1 + "&";
                    }
                    if (client_ip1 != "")
                    {
                        signSrc = signSrc + "client_ip=" + client_ip1 + "&";
                    }
                    if (extend_param1 != "")
                    {
                        signSrc = signSrc + "extend_param=" + extend_param1 + "&";
                    }
                    if (extra_return_param1 != "")
                    {
                        signSrc = signSrc + "extra_return_param=" + extra_return_param1 + "&";
                    }
                    if (input_charset1 != "")
                    {
                        signSrc = signSrc + "input_charset=" + input_charset1 + "&";
                    }
                    if (interface_version1 != "")
                    {
                        signSrc = signSrc + "interface_version=" + interface_version1 + "&";
                    }
                    if (merchant_code1 != "")
                    {
                        signSrc = signSrc + "merchant_code=" + merchant_code1 + "&";
                    }
                    if (notify_url1 != "")
                    {
                        signSrc = signSrc + "notify_url=" + notify_url1 + "&";
                    }
                    if (order_amount1 != "")
                    {
                        signSrc = signSrc + "order_amount=" + order_amount1 + "&";
                    }
                    if (order_no1 != "")
                    {
                        signSrc = signSrc + "order_no=" + order_no1 + "&";
                    }
                    if (order_time1 != "")
                    {
                        signSrc = signSrc + "order_time=" + order_time1 + "&";
                    }
                    if (pay_type1 != "")
                    {
                        signSrc = signSrc + "pay_type=" + pay_type1 + "&";
                    }
                    if (product_code1 != "")
                    {
                        signSrc = signSrc + "product_code=" + product_code1 + "&";
                    }
                    if (product_desc1 != "")
                    {
                        signSrc = signSrc + "product_desc=" + product_desc1 + "&";
                    }
                    if (product_name1 != "")
                    {
                        signSrc = signSrc + "product_name=" + product_name1 + "&";
                    }
                    if (product_num1 != "")
                    {
                        signSrc = signSrc + "product_num=" + product_num1 + "&";
                    }
                    if (redo_flag1 != "")
                    {
                        signSrc = signSrc + "redo_flag=" + redo_flag1 + "&";
                    }
                    if (return_url1 != "")
                    {
                        signSrc = signSrc + "return_url=" + return_url1 + "&";
                    }
                    if (service_type1 != "")
                    {
                        signSrc = signSrc + "service_type=" + service_type1;
                    }
                    if (show_url1 != "")
                    {
                        signSrc = signSrc + "&show_url=" + show_url1;
                    }

                    if (sign_type1 == "RSA-S")//RSA-S签名方法
                    {
                        /**  merchant_private_key，商户私钥，商户按照《密钥对获取工具说明》操作并获取商户私钥。获取商户私钥的同时，也要
                            获取商户公钥（merchant_public_key）并且将商户公钥上传到智付商家后台"公钥管理"（如何获取和上传请看《密钥对获取工具说明》），
                            不上传商户公钥会导致调试的时候报错“签名错误”。
                       */

                        //demo提供的merchant_private_key是测试商户号1111110166的商户私钥，请自行获取商户私钥并且替换。

                        string merchant_private_key = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Merchant_private_key;// "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBALf/+xHa1fDTCsLYPJLHy80aWq3djuV1T34sEsjp7UpLmV9zmOVMYXsoFNKQIcEzei4QdaqnVknzmIl7n1oXmAgHaSUF3qHjCttscDZcTWyrbXKSNr8arHv8hGJrfNB/Ea/+oSTIY7H5cAtWg6VmoPCHvqjafW8/UP60PdqYewrtAgMBAAECgYEAofXhsyK0RKoPg9jA4NabLuuuu/IU8ScklMQIuO8oHsiStXFUOSnVeImcYofaHmzIdDmqyU9IZgnUz9eQOcYg3BotUdUPcGgoqAqDVtmftqjmldP6F6urFpXBazqBrrfJVIgLyNw4PGK6/EmdQxBEtqqgXppRv/ZVZzZPkwObEuECQQDenAam9eAuJYveHtAthkusutsVG5E3gJiXhRhoAqiSQC9mXLTgaWV7zJyA5zYPMvh6IviX/7H+Bqp14lT9wctFAkEA05ljSYShWTCFThtJxJ2d8zq6xCjBgETAdhiH85O/VrdKpwITV/6psByUKp42IdqMJwOaBgnnct8iDK/TAJLniQJABdo+RodyVGRCUB2pRXkhZjInbl+iKr5jxKAIKzveqLGtTViknL3IoD+Z4b2yayXg6H0g4gYj7NTKCH1h1KYSrQJBALbgbcg/YbeU0NF1kibk1ns9+ebJFpvGT9SBVRZ2TjsjBNkcWR2HEp8LxB6lSEGwActCOJ8Zdjh4kpQGbcWkMYkCQAXBTFiyyImO+sfCccVuDSsWS+9jrc5KadHGIvhfoRjIj2VuUKzJ+mXbmXuXnOYmsAefjnMCI6gGtaqkzl527tw=";
                        //私钥转换成C#专用私钥
                        merchant_private_key = testOrder.HttpHelp.RSAPrivateKeyJava2DotNet(merchant_private_key);
                        //签名
                        string signData = testOrder.HttpHelp.RSASign(signSrc, merchant_private_key);
                        sign.Value = signData;
                    }
                    else  //RSA签名方法
                    {
                        RSAWithHardware rsa = new RSAWithHardware();
                        string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                        string password = "87654321";                //证书密码
                        RSAWithHardware rsaWithH = new RSAWithHardware();
                        rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                        string signData = rsaWithH.Sign(signSrc);    //签名
                        sign.Value = signData;
                    }


                    merchant_code.Value = merchant_code1;
                    bank_code.Value = bank_code1;
                    order_no.Value = order_no1;
                    order_amount.Value = order_amount1;
                    service_type.Value = service_type1;
                    input_charset.Value = input_charset1;
                    notify_url.Value = notify_url1;
                    interface_version.Value = interface_version1;
                    sign_type.Value = sign_type1;
                    order_time.Value = order_time1;
                    product_name.Value = product_name1;
                    client_ip.Value = client_ip1;
                    extend_param.Value = extend_param1;
                    extra_return_param.Value = extra_return_param1;
                    product_code.Value = product_code1;
                    product_desc.Value = product_desc1;
                    product_num.Value = product_num1;
                    return_url.Value = return_url1;
                    show_url.Value = show_url1;
                    redo_flag.Value = redo_flag1;
                    pay_type.Value = pay_type1;
                }
                finally
                {

                }

            }
            
        }
    }
}