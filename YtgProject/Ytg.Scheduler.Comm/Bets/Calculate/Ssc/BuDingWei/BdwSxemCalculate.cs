using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 不定位/四星/四星二码  验证通过 2015/05/24
    /// </summary>
    public class BdwSxemCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            Combinations<string> c = new Combinations<string>(item.BetContent.Replace(",", "").Select(m => m.ToString()).ToList(), 2);
            var list = c._combinations;
            //开奖结果
            var housan = openResult.Replace(",", "").Remove(0, 1);
            var temp = housan;// string.Join("", housan.OrderBy(m => m).Select(m => m.ToString()));
            var count = 0;
            if (list != null && list.Count > 0)
            {
               // count = list.Count(m => temp.Contains(string.Join("", m.OrderBy(n => n).Select(n => n.ToString()))));
                count = list.Count(m => m.All(x => temp.Contains(x)));
            }
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
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 2);
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Split(',').Length < 2)
                return string.Empty;
            return betContent;
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
