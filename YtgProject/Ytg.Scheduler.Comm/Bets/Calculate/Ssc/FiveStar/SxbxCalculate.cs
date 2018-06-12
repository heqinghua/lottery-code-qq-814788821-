using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星特殊/三星报喜 2015/04/28 验证通过 2016 01 17
    /// </summary>
    public class SxbxCalculate : FiveSpecialDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //需要计算了
            var list = item.BetContent.ToArray();
            var count = 0;
            foreach (var p in list)
            {
                if (openResult.Count(n => n.ToString() == p.ToString()) >= 3)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item,GetBaseAmt(item,ref stepAmt),stepAmt , count);  
            }
        }

        /// <summary>
        /// 计算多少注
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Length;
        }
    }
}
