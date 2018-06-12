using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    /// <summary>
    /// 组选复试   2016 01 20
    /// </summary>
    public class RenXuanZuXuanFuShi : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = 0;
            var res = openResult.Replace(",", "");
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            List<string> winRes = new List<string>();
            Combinations<int> v = new Combinations<int>(postionStr.Select(cx => Convert.ToInt32(cx.ToString())).ToList(), 2);
            foreach (var cx in v._combinations)
            {
                winRes.Add(string.Format("{0}{1}", res[cx[0]-1],res[cx[1]-1]));
            }
            Combinations<string> c = new Combinations<string>(content.Split(',').ToList(), 2);
            var list = c._combinations;

            foreach (var l in list)
            {
                if (winRes.Any(x => x.Contains(l[0]) && x.Contains(l[1])))
                    count++;
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
            if (string.IsNullOrEmpty(item.BetContent))
                return 0;
            //注数
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            int notes = 0;

            if (content.Length < 2 || postionStr.Length < 2)
                return 0;

            int contentLen = content.Split(',').Length;
            Combinations<int> v = new Combinations<int>(postionStr.Select(c=>Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var c in v._combinations)
            {
                notes += CombinationHelper.Cmn(contentLen, 2);
            }
            return notes;
        }

        public override string HtmlContentFormart(string betContent)
        {
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            betContent = betContent.Replace("&", ",");
            string postionStr = string.Empty;
            string contentCenter = this.SplitRenXuanContent(betContent, ref postionStr);
            if (!this.VerificationPostion(postionStr)
                || string.IsNullOrEmpty(contentCenter))
                return string.Empty;


            var contentArray = contentCenter.Split(',');
            var list = new List<string>();
            int itemCount = 0;
            foreach (var item in contentArray)
            {
                int num = Convert.ToInt32(item);
                if (num < 0 || num > 9) return string.Empty;
                itemCount++;
            }
            if (itemCount >= 2)
                return betContent;
            return string.Empty;
        }
        
    }
}
