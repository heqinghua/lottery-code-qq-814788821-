using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星直选单式 验证通过 2015/05/23 2016 01 17
    /// </summary>
    public class ZxdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var list = item.BetContent.Split(',');
            var count = list.Count(m => m == openResult.Replace(",", "").Substring(1,4));
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        /// <summary>
        /// 计算一共投了多少注
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
                if (itemCount != 4)
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
