using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Tasks.Jobs
{
    public class SscOpenResultHisoer
    {

        //时时彩开奖历史记录，主要用户记录组三
        private static Dictionary<string, int> sscDicts = new Dictionary<string, int>();

        static object lockObjec = new object();

        /// <summary>
        /// 获取当前彩种是否有开豹子
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        public static int GetBaoZi(string lotteryCode)
        {
            lock (lockObjec)
            {
                if (!sscDicts.ContainsKey(lotteryCode))
                    return 0;

                return sscDicts[lotteryCode];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void AppendResult(string lotteryCode, string result)
        {
            lock (lockObjec)
            {
                bool isBz = result.Replace(",", "").Select(x => Convert.ToInt32(x.ToString())).Distinct().Count() == 2;
                int nextZsCount = 0;//10期内必开组三
                if (isBz)
                    nextZsCount = 10;
                if (!sscDicts.ContainsKey(lotteryCode))
                {
                    sscDicts.Add(lotteryCode, nextZsCount);
                }
                else
                {
                    sscDicts[lotteryCode] = nextZsCount;
                }
            }
        }


    }

}
