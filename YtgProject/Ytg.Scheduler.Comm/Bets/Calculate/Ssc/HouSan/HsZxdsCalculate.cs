using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三直选/直选单式 验证通过2015/05/24 2016 01 18
    /// </summary>
    public class HsZxdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            string result = null;
            if (openResult.Length == 5)
                result = openResult.Remove(0, 2);
            else
                result = openResult;

            var list = item.BetContent.Split(',');
            var count = list.Count(m => m == result);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

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
