using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    /// <summary>
    /// 组选单式  2016 01 20
    /// </summary>
    public class RenXuanZuXuanDanShi : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            var res = openResult.Replace(",", "");
            var temp = content.Split(',');

            List<string> winRes = new List<string>();
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var c in v._combinations)
            {
                winRes.Add(string.Format("{0}{1}", res[c[0] - 1], res[c[1] - 1]));
            }

            var count = 0;
            foreach (var r in temp)
            {
                if (winRes.Any(x => x.Contains(r[0]) && x.Contains(r[1])))
                {
                    count++;
                    break;
                }
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
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            if (postionStr.Length < 2)
                return 0;

            int notes = 0;
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var c in v._combinations)
            {
                notes += content.Split(',').Where(m => m.Distinct().Count() == 2).Count();
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
            //01&02&03&33&44&55

            var contentArray = contentCenter.Split(',');
            var list = new List<string>();
            foreach (var item in contentArray)
            {
                var nums = item.OrderBy(x => x);

                if (list.Contains(string.Join("",nums)))
                    continue;
                if (nums.Count() != 2 || nums.Distinct().Count() == 1) continue; ;
                list.Add(string.Join("", nums));
            }
            return string.Join(",", list)+"_"+postionStr;
        }
    }
}
