using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星特殊/好事成双  2015/04/28 hqh 验证通过 2016 01 17
    /// </summary>
    public class HscsCalculate : FiveSpecialDetailsCalculate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult">1,2,3,4,5</param>
        /// <param name="item">345</param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        { 
            var count = 0;
            var list = openResult.Split(',');
            foreach (var p in item.BetContent)
            {
                if (list.Count(n => n == p.ToString()) >= 2)
                {
                    count++;
                    item.IsMatch = true;
                }
            }
            decimal stepAmt=0;
           var baseAmt = GetBaseAmt(item, ref stepAmt);// item.BonusLevel == 1700 ? 20.75M : 22M;

            item.WinMoney = TotalWinMoney(item, baseAmt, stepAmt, count);
        }
    }
}
