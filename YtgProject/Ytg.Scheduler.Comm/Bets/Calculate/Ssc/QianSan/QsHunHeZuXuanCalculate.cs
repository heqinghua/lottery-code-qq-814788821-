using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三组合/混合组选 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class QsHunHeZuXuanCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var temp = openResult.Replace(",", "").Substring(0, 3);
            var result = string.Join("", temp.OrderBy(m => m.ToString()).ToList());
            var res = item.BetContent.Split(',');
            var count = 0;
            int zuliu = 0, zusan = 0;
            foreach (var bet in res)
            {
                var tempCount = bet.Distinct().Count();
                if (bet.Length == 3 && tempCount > 1 && string.Join("", bet.OrderBy(m => m.ToString()).ToList()) == result)
                {
                    if (tempCount == 2) zusan++;
                    else if (tempCount == 3) zuliu++;
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                if (zusan > 0)
                    // item.BonusLevel == 1700 ? 570 : 600
                    item.WinMoney += TotalWinMoney(item, GetBaseAmtLstItem(1,item), 0M, 1);
                if (zuliu > 0)//item.BonusLevel == 1700 ? 270 : 300
                    item.WinMoney += TotalWinMoney(item, GetBaseAmtLstItem(2, item), 0M, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var array = betContent.Replace("&",",").Split(',');
            var list = new List<string>();
            foreach (var item in array)
            {
                if (list.Contains(string.Join("", item.OrderBy(f => Convert.ToString(f)))))
                    continue;
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.Distinct().Count() == 1) continue;
                if (nums.All(m => nums.Count() == 3 && (m >= 0 && m <= 9)))
                {
                    list.Add(string.Join("", item.OrderBy(f => Convert.ToString(f))));
                }
            }
            return string.Join(",", list);
        }
    }
}
