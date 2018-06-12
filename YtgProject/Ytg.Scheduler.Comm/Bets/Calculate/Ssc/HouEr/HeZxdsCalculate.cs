using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.HouEr
{
    /// <summary>
    /// 后二/后二直选/直选单式 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class HeZxdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            if (openResult.Length == 5)
                openResult = openResult.Remove(0, 3);
            else
                openResult = openResult.Remove(0, 1);

            var list = item.BetContent.Split(',');
            var count = list.Count(m => m == openResult);
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
            var list = new List<string>();
            foreach (var item in array)
            {
                if (list.Contains(item))//是否有重复组合
                    continue;
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.All(m => nums.Count() == 2 && (m >= 0 && m <= 9)))
                {
                    list.Add(item);
                }
            }
            return string.Join(",", list);
        }
    }
}
