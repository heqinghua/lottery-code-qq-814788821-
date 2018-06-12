using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 直选单式 验证通过2016 01 17
    /// </summary>
    public class ZxdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况，中了多少奖金，及是否中奖
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var list = item.BetContent.Split(',');
            var count = list.Count(m => m == openResult.Replace(",", ""));
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
                if (itemCount != 5)
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
