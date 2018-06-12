using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets
{
    /// <summary>
    /// 当前玩法、期号是否正在运行计算，追号内容 
    /// </summary>
    public class IssueCatchNumsing
    {
        public static object lockObject = new object();

        private Dictionary<string, List<Running>> mRunnings = new Dictionary<string, List<Running>>();

        private static IssueCatchNumsing mIssueCatchNumsing=null;
        public static IssueCatchNumsing CreateInstance()
        {
            if (mIssueCatchNumsing == null)
                mIssueCatchNumsing = new IssueCatchNumsing();
            return mIssueCatchNumsing;
        }

        /// <summary>
        /// 当期是否完成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="outState"></param>
        /// <returns></returns>
        public bool HasRunning(string key, out List<Running> outState)
        {
            lock (lockObject)
            {
                if (!this.mRunnings.TryGetValue(key, out outState))
                {
                    return true;
                }
            }
            var ct = outState.Where(x => x.IsCompled == false).Count();
            if (ct > 0)
                return false;
            return true;
        }

        /// <summary>
        /// 增加标记
        /// </summary>
        /// <returns></returns>
        public bool PutRunning(string key, Running outState)
        {
            lock (lockObject)
            {
                if (!this.mRunnings.ContainsKey(key))
                {
                    this.mRunnings.Add(key, new List<Running>());
                }
                var res = this.mRunnings[key];
                res.Add(outState);
            }

            return true;
        }

        /// <summary>
        /// 完成追号注数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public bool CompledRunning(string key, int pageIndex)
        {
            List<Running> outResult = null;
            if (!this.HasRunning(key, out outResult) && outResult != null)
            {
                var compledCount = 0;
                foreach (var item in outResult)
                {
                    if (item.PageIndex == pageIndex)
                        item.IsCompled = true;
                    compledCount++;
                }
                if (compledCount == outResult.Count)
                    this.mRunnings.Remove(key);
            }
           // LogManager.Error(key + "----" + "线程处理完成");
            return true;
        }

        public void CompledRunning(string key)
        {
            lock (lockObject)
            {
                if (this.mRunnings.ContainsKey(key))
                    this.mRunnings.Remove(key);
            }
           // LogManager.Error(key + "----" + "移除");
        }
    
    }

    public class Running{
    
        public int PageIndex{get;set;}

        public bool IsCompled{get;set;}
    }
}
