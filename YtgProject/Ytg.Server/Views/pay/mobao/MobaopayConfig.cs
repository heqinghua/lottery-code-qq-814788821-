using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.mobaopay.merchant
{
    /// <summary>
    /// 类名： MobaopayConfig
    /// 功能： 存储基础配置信息
    /// 描述： 设置商户信息、证书地址和支付通知地址...
    /// 
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，
    /// 按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public class MobaopayConfig
    {
        private static string mbp_key = "";
        private static string mobaopay_gateway = "";
        private static string mobaopay_api_version = "";
        private static string mobaopay_apiname_pay = "";
        private static string mobaopay_apiname_realpay = "";
        private static string mobaopay_apiname_query = "";
        private static string mobaopay_apiname_refund = "";
        private static string platform_id = "";
        private static string merchant_acc = "";
        private static string merchant_notify_url = "";
        private static Dictionary<string, string> bankCodeDict = new Dictionary<string, string>();

        static MobaopayConfig()
        {
            //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //商户MD5密钥，切换到正式环境需要替换为正式密钥
            mbp_key = System.Configuration.ConfigurationManager.AppSettings["mbp_key"];//"22c41d776c24deddca95b1709a88f04b";

            //摩宝支付网关地址
            //商户用地址-测试
            mobaopay_gateway = System.Configuration.ConfigurationManager.AppSettings["mobaopay_gateway"];// "https://182.148.123.7/cgi-bin/netpayment/pay_gate.cgi";
            //商户用地址-正式
            //mobaopay_gateway = "https://trade.mobaopay.com/cgi-bin/netpayment/pay_gate.cgi";www.cccwsm.cn 

            //商户接受支付通知地址(商户自己系统的地址)
            merchant_notify_url = System.Configuration.ConfigurationManager.AppSettings["merchant_notify_url"]; //"http://192.168.31.234/MBPExampleNet/Callback.aspx";

            //商户平台号及商户帐号
            platform_id = System.Configuration.ConfigurationManager.AppSettings["platform_id"];//"MerchTest";
            merchant_acc = System.Configuration.ConfigurationManager.AppSettings["merchant_acc"]; //"210001110100250";

            //设置摩宝支付现在支持的银行代码
            bankCodeDict.Add("ICBC", "ICBC");   //工行
            bankCodeDict.Add("ABC", "ABC");     //农行
            bankCodeDict.Add("BOC", "BOC");     //中行
            bankCodeDict.Add("CCB", "CCB");     //建行
            bankCodeDict.Add("COMM", "COMM");   //交行
            bankCodeDict.Add("CMB", "CMB");     //招行
            bankCodeDict.Add("SPDB", "SPDB");   //浦发
            bankCodeDict.Add("CIB", "CIB");     //兴业
            bankCodeDict.Add("CMBC", "CMBC");   //民生
            bankCodeDict.Add("CGB", "CGB");     //广发
            bankCodeDict.Add("CNCB", "CNCB");   //中信
            bankCodeDict.Add("CEB", "CEB");     //光大
            bankCodeDict.Add("HXB", "HXB");     //华夏
            bankCodeDict.Add("PSBC", "PSBC");   //邮储
            bankCodeDict.Add("PAB", "PAB");     //平安

            //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓以下配置项不需要修改↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            mobaopay_api_version = "1.0.0.0";
            mobaopay_apiname_pay = "WEB_PAY_B2C";
            mobaopay_apiname_realpay = "CUST_REAL_PAY";
            mobaopay_apiname_query = "MOBO_TRAN_QUERY";
            mobaopay_apiname_refund = "MOBO_TRAN_RETURN";
        }

        public static string Mbp_key
        {
            get { return MobaopayConfig.mbp_key; }
            set { MobaopayConfig.mbp_key = value; }
        }

        public static string Mobaopay_gateway
        {
            get { return MobaopayConfig.mobaopay_gateway; }
            set { MobaopayConfig.mobaopay_gateway = value; }
        }

        public static string Mobaopay_api_version
        {
            get { return MobaopayConfig.mobaopay_api_version; }
            set { MobaopayConfig.mobaopay_api_version = value; }
        }

        public static string Mobaopay_apiname_pay
        {
            get { return MobaopayConfig.mobaopay_apiname_pay; }
            set { MobaopayConfig.mobaopay_apiname_pay = value; }
        }

        public static string Mobaopay_apiname_realpay
        {
            get { return MobaopayConfig.mobaopay_apiname_realpay; }
        }

        public static string Mobaopay_apiname_query
        {
            get { return MobaopayConfig.mobaopay_apiname_query; }
            set { MobaopayConfig.mobaopay_apiname_query = value; }
        }

        public static string Mobaopay_apiname_refund
        {
            get { return MobaopayConfig.mobaopay_apiname_refund; }
            set { MobaopayConfig.mobaopay_apiname_refund = value; }
        }

        public static string Platform_id
        {
            get { return MobaopayConfig.platform_id; }
            set { MobaopayConfig.platform_id = value; }
        }

        public static string Merchant_acc
        {
            get { return MobaopayConfig.merchant_acc; }
            set { MobaopayConfig.merchant_acc = value; }
        }

        public static string Merchant_notify_url
        {
            get { return MobaopayConfig.merchant_notify_url; }
            set { MobaopayConfig.merchant_notify_url = value; }
        }
    }
}