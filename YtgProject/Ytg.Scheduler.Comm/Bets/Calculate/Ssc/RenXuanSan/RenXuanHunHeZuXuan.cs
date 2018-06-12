using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    /// <summary>
    /// 混合组选  2016 01 21
    /// </summary>
    public class RenXuanHunHeZuXuan : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            var res = content.Split(',');

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            List<string> winRes = new List<string>();
            foreach (var c in v._combinations)
            {
                var vstr= string.Join("",string.Join("", openResult[c[0] - 1], openResult[c[1] - 1], openResult[c[2] - 1]).OrderBy(x => x));
                winRes.Add(vstr);
            }

            var count = 0;
            int zuliu = 0, zusan = 0;
            foreach (var bet in res)
            {
                var betStr= string.Join("", bet.OrderBy(m => m.ToString()).ToList());
                var tempCount = bet.Distinct().Count();
                if (winRes.Contains(betStr) )
                {
                    if (tempCount == 2) zusan++;
                    else if (tempCount == 3) zuliu++;
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                if (zusan > 0)
                    item.WinMoney += TotalWinMoney(item, GetBaseAmtLstItem(1, item), 0M, 1);
                if (zuliu > 0)
                    item.WinMoney += TotalWinMoney(item, GetBaseAmtLstItem(2, item), 0M, 1);
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
                if (bet.Length == 3 && bet.Distinct().Count() >1)
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
                if (nums.Count() != 3 || nums.Distinct().Count() == 1) continue; ;
                list.Add(string.Join("", nums));
            }
            return string.Join(",", list) + "_" + postionStr;

        }
    }
}
