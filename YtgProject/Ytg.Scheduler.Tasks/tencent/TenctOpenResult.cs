using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Tasks.tencent
{
    public class TenctOpenResult
    {
        private Dictionary<string, Online> ConcurrentQueueLst = new Dictionary<string, Online>();

        static object lockObj = new object();

        static TenctOpenResult mTenctOpenResult = null;

        public static TenctOpenResult CreateInstan()
        {
            if (mTenctOpenResult == null)
                mTenctOpenResult = new TenctOpenResult();
            return mTenctOpenResult;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Put(string key, Online item)
        {
            lock (lockObj)
            {
                Online outOnline = null;
                if (ConcurrentQueueLst.TryGetValue(key, out outOnline))
                    return false;
                ConcurrentQueueLst.Add(key, item);
                Console.WriteLine("key:"+key+" [[[[[["+item.result);
                return true;
            }

        }

        /// <summary>
        /// 如果有，返回并删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Online RemovedGet(string key)
        {
            lock (lockObj)
            {
                Online outOnline = null;
                if (ConcurrentQueueLst.TryGetValue(key, out outOnline))
                {
                    ConcurrentQueueLst.Remove(key);
                    return outOnline;
                }
                return null;
            }
        }




    }
}
