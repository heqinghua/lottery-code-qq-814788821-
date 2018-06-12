using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSi
{
    /// <summary>
    /// 组选122016 01 21
    /// </summary>
    public class RenXuanZu12 : BaseRenXuan
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            openResult = openResult.Replace(",", "");

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
            List<string> winRes = new List<string>();
            foreach (var b in v._combinations)
            {
                string ops = string.Join("", string.Join("", openResult[b[0] - 1], openResult[b[1] - 1], openResult[b[2] - 1], openResult[b[3] - 1]).OrderBy(x => x));
                winRes.Add(ops);
            }

            var count = TotalBet(content, winRes);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            var array = content.Split(',');
            if (array.Length != 2 || array[1].Length < 2 || postionStr.Length<4)
                return 0;

            int count = 0;
            foreach (var dan in array[0])
            {
                var fcount = array[1].Where(a => a != dan).ToArray().Length;
                if (fcount < 2)
                    continue;
                count += CombinationHelper.Cmn(fcount, 2);

            }

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
            int notes = 0;
            foreach (var b in v._combinations)
            {
                notes += count;
            }

            return notes;
        }



        /**
        * 从“二重号”选择一个号码，“单号”中选择两个号码组成一注。
        投注方案：二重号：8；单号：06\n\t开奖号码：*0688（顺序不限），即可中四星组选12
        */

        private static int TotalBet(string content, List<string> winRes)
        {
            var contentArray = content.Split(',');
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

                winCount += list.Select(c => Order(c, dan)).Where(l => winRes.Contains(l)).Count();
                if (winCount > 0)
                    break;
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

        protected override int PostionLen
        {
            get
            {
                return 4;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            betContent = betContent.Replace("|",",");
            string postionStr = string.Empty;
            string contentCenter = this.SplitRenXuanContent(betContent, ref postionStr);
            if (!this.VerificationPostion(postionStr)
                || string.IsNullOrEmpty(contentCenter))
                return string.Empty;


            var contentArray = contentCenter.Split(',');
            if (contentArray.Length!=2||
                contentArray[0].Replace("&","").Length < 1
                || contentArray[1].Replace("&", "").Length < 2)
                return string.Empty;

             if(VCompled(contentArray[0])
                 && VCompled(contentArray[1]))
                 return betContent.Replace("&","");
            return string.Empty;
        }

        private bool VCompled(string items) {
            var contentArray = items.Split('&').Select(x=>x);
            foreach (var item in contentArray)
            {
                int num = Convert.ToInt32(item);
                if (num < 0 || num > 9) return false;
            }
            return true;
        }
    }
}
