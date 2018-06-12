using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星组选6  验证通过 2015/05/23 2016 01 18
    /// </summary>
    public class Zx6Calculate : FiveSpecialDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            Combinations<string> c = new Combinations<string>(item.BetContent.Select(t => t.ToString()).ToList(), 2);
            var list = c._combinations;
            //开奖结果
            var temp = string.Join("", openResult.Replace(",", "").Remove(0, 1).OrderBy(m => m).Select(m => m.ToString()));
            var count = 0;
            if (list != null && list.Count > 0)
            {
                //二重号，一定需要后4位只有2个数字
                count = list.Count(m => temp.Distinct().Count() == 2 && temp.Contains(string.Join("", m.OrderBy(n => n).Select(n => n.ToString()))));
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 2);
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Length < 2)
                return string.Empty;
            return betContent;
        }
    }
}
