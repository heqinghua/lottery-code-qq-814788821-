using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.BootStrapper
{
    public static class SiteHelper
    {
        static IList<SysSetting> SysSetting = null;
        static SiteHelper()
        {
            ISysSettingService sysSettingService = IoC.Resolve<ISysSettingService>();
            SysSetting = sysSettingService.SeleteAll();
        }

        static string GetItemValue(string key)
        {
            var fs = SysSetting.Where(x => x.Key == key).FirstOrDefault();
            if (fs == null)
                return "";
            return fs.Value;
        }


        /// <summary>
        /// 站点是否关闭
        /// </summary>
        /// <returns></returns>
        public static bool IsOpenSite()
        {
            var fs=SysSetting.Where(x => x.Key == "WZKG").FirstOrDefault();
            if (fs == null)
                return false;
            return fs.Value == "0" ? true : false;
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        /// <returns></returns>
        public static string GetSiteName()
        {
            return GetItemValue("PTMC");
        }

        /// <summary>
        /// 站点关闭提示内容
        /// </summary>
        /// <returns></returns>
        public static string GetSiteCloseContent()
        {
            return GetItemValue("WZGGKG");
        }

        /// <summary>
        /// 中奖排行开关
        /// </summary>
        /// <returns></returns>
        public static bool ZJPHKG()
        {
            var fs = SysSetting.Where(x => x.Key == "ZJPHKG").FirstOrDefault();
            if (fs == null)
                return false;
            return fs.Value == "0" ? true : false;
        }

        /// <summary>
        /// 中奖排行最低金额
        /// </summary>
        /// <returns></returns>
        public static decimal GetMinWinMonery()
        {
            decimal outDecimal = 0;
            if (decimal.TryParse(GetItemValue("HYSPZDZJJE"), out outDecimal))
                return outDecimal;
            return 0;
        }

        

         /// <summary>
        /// 虚拟上榜会员
        /// </summary>
        /// <returns></returns>
        public static List<XuNiItem> GetWinsMonery()
        {
            ISysSettingService sysSettingService = IoC.Resolve<ISysSettingService>();
            var setting=sysSettingService.GetSetting("XNSPHYNC");
            if (setting == null)
                return new List<XuNiItem>();
            string content = setting.Value;
            if (string.IsNullOrEmpty(content))
                return new List<XuNiItem>();
            List<XuNiItem> XuNiItems = new List<XuNiItem>();
            var array = content.Split(',');
            foreach (var a in array)
            {
                if (string.IsNullOrEmpty(a))
                    continue;
                var item = a.Split('|');
                if (item.Length != 2)
                    continue;
                XuNiItem xu = new XuNiItem();
                xu.UserCode = item[0];
                decimal outDec = 0;
                if (!decimal.TryParse(item[1], out outDec))
                    outDec = 1000;
                xu.UserWinMonery = outDec;
                XuNiItems.Add(xu);
            }

            return XuNiItems;

        }

        static Dictionary<int, string> LotteryIdNames = null;
        public static Dictionary<int, string> GetLotteryIdName()
        {
            if (LotteryIdNames == null)
            {
                LotteryIdNames = new Dictionary<int, string>();
                string[] config = System.Configuration.ConfigurationManager.AppSettings["lotteryConfig"].Split('|');
                foreach (var item in config)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    var itemArray = item.Split(',');
                    if (itemArray.Length != 2)
                        continue;
                    int key = Convert.ToInt32(itemArray[0]);
                    if (LotteryIdNames.ContainsKey(key))
                        continue;
                    LotteryIdNames.Add(key, itemArray[1]);
                }
            }

            return LotteryIdNames;

        }

        static string lhcBei = System.Configuration.ConfigurationManager.AppSettings["liuhecaibat"].ToString();
        /// <summary>
        /// 获取六合彩倍数
        /// </summary>
        /// <returns></returns>
        public static double GetLiuHeMiult() {
            if (string.IsNullOrEmpty(lhcBei))
                return 42;
            return Convert.ToDouble(lhcBei);
        }

        static string payDns = System.Configuration.ConfigurationManager.AppSettings["payDns"].ToString();


        /// <summary>
        /// 充值二维码路径
        /// </summary>
        public static string rectImagePath= System.Configuration.ConfigurationManager.AppSettings["chongzhiu"].ToString();

        /// <summary>
        /// 获取支付dns
        /// </summary>
        /// <returns></returns>
        public static string GetParDns()
        {
            return payDns;
        }
    }
    public class XuNiItem {

        /// <summary>
        /// 虚拟会员账号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 虚拟中奖金额
        /// </summary>
        public decimal UserWinMonery { get; set; }
    }
}