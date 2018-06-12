using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    /// <summary>
    /// 组六单式  2016 0121
    /// </summary>
    public class RenXuanZuLiuDanShi : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            openResult = openResult.Replace(",", "");
            var res = content.Split(',');


            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            List<string> winRes = new List<string>();
            foreach (var c in v._combinations)
            {
                winRes.Add(string.Join("", openResult[c[0]-1], openResult[c[1]-1], openResult[c[2]-1]));
            }

            var count = 0;
            foreach (var bet in res)
            {
                if (winRes.Any(x => x.Contains(bet[0]) && x.Contains(bet[1]) && x.Contains(bet[2])))
                {
                    count++;
                    break;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {

            if (string.IsNullOrEmpty(item.BetContent))
                return 0;

            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            if (postionStr.Length < 3)
                return 0;


            var count = 0;
            var res = content.Split(',');
            foreach (var bet in res)
            {
                if (bet.Length == 3 && bet.Distinct().Count() == 3)
                {
                    count++;
                }
            }

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            var notes = 0;
            foreach (var c in v._combinations)
            {
                notes += count;
            }

            return notes;

        }


        protected override int PostionLen
        {
            get
            {
                return 3;
            }
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
            foreach (var item in contentArray)
            {
                var nums = item.OrderBy(x => x);

                if (list.Contains(string.Join("", nums)))
                    continue;
                if (nums.Count() != 3 || nums.Distinct().Count() != 3) continue; ;
                list.Add(string.Join("", nums));
            }
            return string.Join(",", list) + "_" + postionStr;

        }
    }
}
