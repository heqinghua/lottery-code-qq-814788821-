using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星组选24  验证通过 2015/05/23 2016 01 18
    /// </summary>
    public class Zx24Calculate : FiveSpecialDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            Combinations<string> c = new Combinations<string>(item.BetContent.Select(t=>t.ToString()).ToList(), 4);
            var list = c._combinations;
            //开奖结果
            var temp = string.Join("", openResult.Replace(",", "").Remove(0, 1).OrderBy(m => m).Select(m => m.ToString()));
            var count = 0;
            if (list != null && list.Count > 0)
            {

                count = list.Count(m => temp == string.Join("", m.OrderBy(n => n).Select(n => n.ToString())));
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 4);
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent= base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Length < 4)
                return string.Empty;
            return betContent;
        }
    }
}
