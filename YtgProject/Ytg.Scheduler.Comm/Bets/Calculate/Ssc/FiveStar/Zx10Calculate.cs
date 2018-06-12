using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星组选/组选10 验证通过 2015/05/23  2016 01 17
    /// </summary>
    public class Zx10Calculate : ZhiXuanFushiDetailsCalculate
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
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), 20M, count);
            }
        }

        /// <summary>
        /// 计算总注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var array = item.BetContent.Split(',');
            if (array[0].Length < 1 || array[1].Length < 1)
                return 0;
            int count = 0;
            foreach (var er in array[0])
            {
                var fcount = array[1].Where(a => a != er).ToArray().Length;
                if (fcount < 1)
                    continue;
                count += fcount;
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
                || array[1].Length < 1)
                return string.Empty;
            return betContent;

        }

        #region 计算具体组合及注数
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
                var danItems = danHao.Where(d => d != er).Select(d => d.ToString()).ToList();
                if (danItems.Count < 1)
                    continue;
                foreach (var d in danItems)
                    if (Order(d, er) == res)
                        winCount++;
                
            }
            return winCount;
        }

        string Order(string c, char er)
        {
            string res = "";
            var array = string.Format("{0}{0}{1}{1}{1}", c,er).OrderBy(x => x);
            foreach (var a in array)
                res += a;

            return res;
        }
        #endregion
    }
}
