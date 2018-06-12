using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc
{
    public class ZuHeWinHelper
    {
        /// <summary>
        /// 计算组合 
        /// </summary>
        /// <param name="bets"></param>
        /// <param name="openResult"></param>
        /// <returns></returns>
        public static int WinCount(string bet, string openResult)
        {
            int winCount = 0;
            var sl=bet.Split(',');
            for (var i = 0; i < sl.Length; i++)
            {
                if (sl[i].Contains(openResult[i]))
                    winCount++;
            }
           // Console.WriteLine(winCount);
            return winCount;
        }

     
    }
}
