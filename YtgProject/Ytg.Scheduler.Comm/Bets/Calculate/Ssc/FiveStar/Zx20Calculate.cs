using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星组选/组选20 验证通过 2015/05/23 2016 01 17
    /// </summary>
    public class Zx20Calculate : ZhiXuanFushiDetailsCalculate
    {
        /// <summary>
        /// 中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = TotalBet(item, openResult);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
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
            var array = item.BetContent.Split(',');
            if (array.Length != 2 || array[1].Length < 2)
                return 0;

            int count = 0;
            foreach (var er in array[0])
            {
                var fcount = array[1].Where(a => a != er).ToArray().Length;
                if (fcount < 2)
                    continue;
                count += CombinationHelper.Cmn(fcount, 2);
            }

            return count;
        }

        protected override int GetLen
        {
            get
            {
                return 2;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return betContent;
            var array = betContent.Split(',');
            if (array[0].Length < 1
                || array[1].Length < 2)
                return string.Empty;
            return betContent;

        }

        #region 计算具体中奖算法，和投注数
        private int TotalBet(BasicModel.LotteryBasic.BetDetail betDetail, string openResult)
        {
            var contentArray = betDetail.BetContent.Split(',');
            var erChongHao = contentArray[0];
            var danHao = contentArray[1];
            var res = "";
            openResult.Replace(",", "").OrderBy(r => r).ToList().ForEach(r => res += r);

            int winCount = 0;
            foreach (var er in erChongHao)
            {
                var danItems = danHao.Where(d => d != er).Select(d => d.ToString()).ToArray();
                if (danItems.Length < 2)
                    continue;
                Combinations<string> cx = new Combinations<string>(danItems, 2);
                var list = cx._combinations;

                winCount += list.Select(c => Order(c, er)).Where(l => l == res).Count();
            }
            return winCount;
        }


        string Order(IList<string> c, char er)
        {
            string res = "";
            var array = string.Format("{0}{1}{2}{2}{2}", c[0], c[1], er).OrderBy(x => x);
            foreach (var a in array)
                res += a;

            return res;
        }
        #endregion
    }
}
