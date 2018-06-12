using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 一帆风顺 验证通过 2015/05/23 2016 01 17
    /// </summary>
    public class YffxCalculate : FiveSpecialDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var rarrays = openResult.Split(',');
            List<string> newArr = new List<string>();
            foreach (var ix in rarrays)
            {
                if (!newArr.Contains(ix.ToString()))
                    newArr.Add(ix.ToString());
            }
            openResult = string.Join(",", newArr);
            var list = openResult.Split(',').Where(m => item.BetContent.Contains(m));
            if (null == list || list.Count() == 0) return;
            else
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, list.Count());// list.Count() * 1000;
            }
        }
    }
}
