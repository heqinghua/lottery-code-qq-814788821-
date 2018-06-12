
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ytg.Comm;

namespace Ytg.ServerWeb.Page.PageCode
{
    public class RequestManagerFactory
    {

        private static RequestManagerFactory requestManagerFactory;

        /// <summary>
        /// 创建单列模式
        /// </summary>
        /// <returns></returns>
        public static RequestManagerFactory CreateInstance()
        {
            if (requestManagerFactory == null)
                requestManagerFactory = new RequestManagerFactory();
            return requestManagerFactory;
        }

        public BaseRequestManager CreateBaseRequestManager(string requestUrl)
        {
           var settings = SettingHelper.GetInititaSetting();
            var settingItem = settings.Where(item => requestUrl.ToLower().IndexOf(item.Key.ToLower()) > 0).FirstOrDefault();
            if (settingItem.Value == null)
                return null;

            var type=Type.GetType(settingItem.Value.ClassFullName);
            return IoC.Resolve(type) as BaseRequestManager;
          
        }
    }
}
