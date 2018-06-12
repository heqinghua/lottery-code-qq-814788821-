using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Tasks
{
    public class AutoLotteryC
    {
        static List<string> AutoLotteryCodeIssue = new List<string>();

        static AutoLotteryC mAutoLotteryC = null;

        static object _lock = new object();
        public static AutoLotteryC CreateInstan()
        {
            if (mAutoLotteryC == null)
                mAutoLotteryC = new AutoLotteryC();
            return mAutoLotteryC;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Add(string key)
        {
            lock (_lock)
            {
                if (AutoLotteryCodeIssue.Contains(key))
                    return false;
                AutoLotteryCodeIssue.Add(key);
            }
            return true;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            lock (_lock)
            {
                if (AutoLotteryCodeIssue.Contains(key))
                {
                    AutoLotteryCodeIssue.Remove(key);
                    return true;
                }

                return false;
            }

        }
    }
}
