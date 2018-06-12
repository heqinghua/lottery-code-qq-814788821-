using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utg.ServerWeb.Admin.BootStrapper
{
    /// <summary>
    /// 配置文件辅助类
    /// </summary>
    public class ConfigHelper
    {
        private static string WebSiteDns = System.Web.Configuration.WebConfigurationManager.AppSettings["webSiteDns"];//前台访问地址

        public static string GetWebSiteDns()
        {
            return WebSiteDns;
        }
    }
}