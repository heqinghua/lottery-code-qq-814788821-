using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星组选12 验证通过 2016 01 18
    /// </summary>
    public class Zx12Calculate : ZhiXuanFushiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = TotalBet(item, openResult);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            //var array = item.BetContent.Split(',');
            //if (array.Length != 2 || array[1].Length < 2)
            //    return 0;
            //int count = 0;
            //foreach (var dan in array[1])
            //{
            //    var fcount = array[0].Where(a => a != dan).ToArray().Length;
            //    if (fcount < 2)
            //        continue;
            //    count += CombinationHelper.Cmn(fcount, 2);
            //}

            //return count;

            var contentArray = item.BetContent.Split(',');
            var erChongHao = contentArray[0];
            var danHao = contentArray[1];
         
         
            int winCount = 0;
            foreach (var dan in erChongHao)
            {
                var erItems = danHao.Where(d => d != dan).Select(d => d.ToString()).ToArray();
                if (erItems.Length < 2)
                    continue;
                Combinations<string> cx = new Combinations<string>(erItems, 2);
                var list = cx._combinations;
                winCount += list.Count;
            }
            return winCount;
        }



        /**
        * 从“二重号”选择一个号码，“单号”中选择两个号码组成一注。
        投注方案：二重号：8；单号：06\n\t开奖号码：*0688（顺序不限），即可中四星组选12
        */

        private static int TotalBet(BasicModel.LotteryBasic.BetDetail betDetail, string openResult)
        {
            var contentArray = betDetail.BetContent.Split(',');
            var erChongHao = contentArray[0];
            var danHao = contentArray[1];
            var res = "";
            openResult.Replace(",", "").Substring(1,4).OrderBy(r => r).ToList().ForEach(r => res += r);

            int winCount = 0;
            foreach (var dan in erChongHao)
            {
                var erItems = danHao.Where(d => d != dan).Select(d => d.ToString()).ToArray();
                if (erItems.Length < 2)
                    continue;
                Combinations<string> cx = new Combinations<string>(erItems, 2);
                var list = cx._combinations;

                winCount += list.Select(c => Order(c, dan)).Where(l => res.IndexOf(l) != -1).Count();
            }
            return winCount;
        }
        static string Order(IList<string> c, char dan)
        {
            string res = "";
            var array = string.Format("{0}{1}{2}{2}", c[0], c[1], dan).OrderBy(x => x);
            foreach (var a in array)
                res += a;

            return res;
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
                return string.Empty;
            var array = betContent.Split(',');
            if (array[0].Length < 1
                || array[1].Length < 2)
                return string.Empty;
            return betContent;
        }
    }
}
