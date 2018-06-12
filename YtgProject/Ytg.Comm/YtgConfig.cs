using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ytg.Comm
{
    public class YtgConfig
    {
        static Dictionary<string, string> mConfigs = null;

        static Dictionary<string, string> mQuos = null;

        /// <summary>
        /// 获取充值允许访问的ip列表
        /// </summary>
        public static string mYtg_User_RechargeIps = System.Configuration.ConfigurationManager.AppSettings["ytg_user_rechargeIps"];
        
        public static  string GetItem(string key)
        {
            if (null == mConfigs)
            {
                string mFileName = System.Web.HttpContext.Current.Server.MapPath("/xml/Ytg.config");
                System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load(mFileName);
                Analysis(xdocument);
            }

            string outValue=string.Empty;
            mConfigs.TryGetValue(key, out outValue);
            return outValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQus()
        {
            if (null == mQuos)
            {
                string mFileName = System.Web.HttpContext.Current.Server.MapPath("/xml/Ytg.config");
                System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load(mFileName);
                Analysis(xdocument);
            }

            return mQuos;
        }

        

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="document"></param>
        private static void Analysis(System.Xml.Linq.XDocument document)
        {
            //解析普通配置项 xml: Mobile
            var element = document.Element("appSettings");
            if (null == element)
                return;
            var folders = element.Elements("add");
            mConfigs = new Dictionary<string, string>();

            foreach (var item in folders)
            {
                var key = item.Attribute("key");
                var value = item.Attribute("value");

                mConfigs.Add(key.Value, value.Value);
            }
            //quos
            mQuos = new Dictionary<string, string>();
            var quos = element.Elements("Quo");
            foreach (var quo in quos)
            {
                var key = quo.Attribute("key");
                var value = quo.Attribute("value");
                mQuos.Add(key.Value, value.Value);
            }

        }
    }
}