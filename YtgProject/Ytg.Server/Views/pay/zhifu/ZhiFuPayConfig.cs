using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ytg.ServerWeb.Views.pay.zhifu
{
    public static class ZhiFuPayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private static string merchant_code = "";

        private static string notify_url = "";

        private static string return_url = "";

        private static string merchant_private_key = "";

        private static string payDns = "";
        static ZhiFuPayConfig() {
            merchant_code = System.Configuration.ConfigurationManager.AppSettings["ZFmerchant_code"];//

            notify_url = System.Configuration.ConfigurationManager.AppSettings["ZFnotify_url"];//

            return_url = System.Configuration.ConfigurationManager.AppSettings["ZFreturn_url"];//

            merchant_private_key = System.Configuration.ConfigurationManager.AppSettings["merchant_private_key"];//

            payDns = System.Configuration.ConfigurationManager.AppSettings["ZFpayDns"];//
        }

        
        /// <summary>
        /// 商户号
        /// </summary>
        public static string Merchant_code
        {
            get { return merchant_code; }
        }

        /// <summary>
        /// 通知地址
        /// </summary>
        public static string Notify_url
        {
            get { return notify_url; }
        }


        /// <summary>
        /// 前台地址
        /// </summary>
        public static string Return_url
        {
            get { return return_url; }
        }

        /// <summary>
        /// 前台地址
        /// </summary>
        public static string Merchant_private_key
        {
            get { return merchant_private_key; }
        }

        /// <summary>
        /// 前台地址
        /// </summary>
        public static string PayDns
        {
            get { return payDns; }
        }
    }
}