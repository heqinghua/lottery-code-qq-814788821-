using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Ytg.Comm;

namespace Ytg.ServerWeb.BootStrapper
{
    public class HandleRequest
    {
        private static string Name { get; } = "ytgproject";

        //10秒过期
        private static int Seconds=5000;

       
       

        /// <summary>
        /// 是否允许访问
        /// </summary>
        /// <returns></returns>
        public static  bool IsValidRequest()
        {
            var allowExecute = false;
            var key = string.Concat(Name, "-", Utils.GetIp());
            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true, //这是我们可以拥有的最小数据吗？
                    null, // 没有依赖关系
                    DateTime.Now.AddMilliseconds(Seconds), // 绝对过期
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null); //没有回调

                allowExecute = true;
            }
            return allowExecute;
        }

    }
}