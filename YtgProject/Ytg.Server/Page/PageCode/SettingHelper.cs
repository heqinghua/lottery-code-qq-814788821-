
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Comm.Security;
using Ytg.ServerWeb.Page.PageCode;

namespace Ytg.ServerWeb.Page.PageCode
{
    /// <summary>
    /// 配置文件辅助类
    /// </summary>
    public static class SettingHelper
    {
        static object lockKey = new object();

        /// <summary>
        /// 移动服务端配置相关
        /// </summary>
        static Dictionary<string, MobileSettingItemInfo> mMobileInititalSettings=null;


        public static Dictionary<string, MobileSettingItemInfo> GetInititaSetting()
        {
            lock (lockKey)
            {
                if (mMobileInititalSettings == null)
                {
                    string mFileName = System.Web.HttpContext.Current.Server.MapPath("/Page/Settings.xml");
                    System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load(mFileName);

                    GetMobileSetting(xdocument);
                }
            }
            return mMobileInititalSettings;
        }

        /// <summary>
        /// 解析移动服务配置相关信息
        /// </summary>
        /// <param name="xdocument"></param>
        private static void GetMobileSetting(System.Xml.Linq.XDocument xdocument)
        {
            //解析普通配置项 xml: Mobile
            var element = xdocument.Element("Settings").Elements("JSONServer").FirstOrDefault();
            if (null == element)
                return;
            var folders = element.Elements("Folder");
            mMobileInititalSettings = new Dictionary<string, MobileSettingItemInfo>();

            foreach (var item in folders)
            {
                var folderNameAttribute = item.Attribute("name");
                if (null == folderNameAttribute)
                    throw new ArgumentNullException("Folder attribute is null!");
                if (!item.HasElements)
                    throw new ArgumentNullException("Folder is not childrens!");

                var nameItems = item.Elements("item");
                foreach (var nameItem in nameItems)
                {
                    var pageAttribute = nameItem.Attribute("page");
                    var classAttribute = nameItem.Attribute("class");
                    if (null == pageAttribute || null == classAttribute)
                        continue;
                    MobileSettingItemInfo settingItem = new MobileSettingItemInfo();
                    settingItem.Page = folderNameAttribute.Value + pageAttribute.Value;
                    settingItem.ClassFullName = classAttribute.Value;
                    if (mMobileInititalSettings.ContainsKey(settingItem.Page))
                        continue;
                    mMobileInititalSettings.Add(settingItem.Page, settingItem);
                }
            }

        }

    }
}
