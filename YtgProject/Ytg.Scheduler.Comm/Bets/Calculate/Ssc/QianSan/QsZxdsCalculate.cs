using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三直选/直选单式 2016 01 18
    /// </summary>
    public class QsZxdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var list = item.BetContent.Split(',');
            var count = list.Count(m => m == openResult.Replace(",", "").Substring(0, 3));
            if (count > 0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            int betCount = item.BetContent.Split(',').Count();
            return betCount;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var array = betContent.Replace("&", ",").Split(',');
            List<string> result = new List<string>();
            foreach (var item in array)
            {
                int itemCount = item.Length;
                if (itemCount != 3)
                    continue;
                int otx = 0;
                if (!int.TryParse(item, out otx))
                    continue;
                result.Add(item);
            }

            string input = string.Join(",", result.Distinct().ToArray());
            return input;
        }
    }
}
