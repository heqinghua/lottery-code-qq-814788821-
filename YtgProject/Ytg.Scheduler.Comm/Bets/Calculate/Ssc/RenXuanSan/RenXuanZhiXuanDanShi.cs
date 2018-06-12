using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    /// <summary>
    /// 直选单试 2016 01 21
    /// </summary>
    public class RenXuanZhiXuanDanShi : RenXuanDanShi
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            int winCount = 0;
            var list = content.Split(',');
            var result = openResult.Replace(",", "");
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            foreach (var b in v._combinations)
            {
                string winResult = result[b[0]-1].ToString() + result[b[1]-1].ToString() + result[b[2]-1].ToString();
                winCount += list.Count(m => m == winResult);
                if (winCount > 0)
                    break;
            }

            if (winCount > 0)
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
            if (!string.IsNullOrEmpty(content) && postionStr.Length >= 3)
            {
                var ctLength = content.Split(',').Length;
                int notes = 0;
                Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c)).ToList(),3);
                foreach (var b in v._combinations)
                {
                    notes += ctLength;
                }

                return notes;
            }

            return 0;
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
            //01&02&03&33&44&55

            var contentArray = contentCenter.Split(',');
            var list = new List<string>();
            foreach (var item in contentArray)
            {
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.Count() != 3) return string.Empty;
                if (nums.All(m => Convert.ToInt32(m) >= 0 && Convert.ToInt32(m) <= 9)) list.Add(item);
            }
            return betContent;
        }

        

    }
}
