using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 不定位/三星/前三一码  验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class BdwQsymCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //前三位至少出现一个1
            var res = openResult.Replace(",", "");
            if (res.Length == 5)
                res = res.Substring(0, 3);

            res = Ytg.Comm.Utils.ClearRepeat(res);

            var temp = item.BetContent.Replace(",", "").Select(m => Convert.ToInt32(m.ToString()));
            int winCount = 0;
            foreach (var bet in temp)
            {
                if (res.Contains(bet.ToString()))
                {
                    winCount++;
                }
            }
            if (winCount > 0)
            {
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, winCount);
                item.IsMatch = true;
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Replace(",", "").Length;
        }

        protected override int GroupLen
        {
            get
            {
                return 1;
            }
        }

        protected override int ItemLen
        {
            get
            {
                return 1;
            }
        }
    }
}
