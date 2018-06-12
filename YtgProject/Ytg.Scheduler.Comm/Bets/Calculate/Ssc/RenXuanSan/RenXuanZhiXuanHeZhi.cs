using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    /// <summary>
    /// 直选和值 2016 01 21
    /// </summary>
    public class RenXuanZhiXuanHeZhi : BaseRenXuan
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);

            var openLst = openResult.Replace(",", "").Select(c => Convert.ToInt32(c.ToString())).ToList();

            List<int> winRes = new List<int>();
            foreach (var c in v._combinations)
            {
                winRes.Add(openLst[c[0]-1] + openLst[c[1]-1] + openLst[c[2]-1]);
            }

            int winCount = TotalBet(content, winRes);
            if (winCount > 0)
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

            if (string.IsNullOrEmpty(content) || content.Length < 1 || postionStr.Length < 3)
                return 0;

            int notes = 0;

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(),3);
            foreach (var c in v._combinations)
            {
                notes += TotalBet(content, null);
            }

            return notes;
        }

        private int TotalBet(string betContent, List<int> winRes)
        {
            List<int> hezhi = betContent.Split(',').Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();


            if (hezhi.Count >= 1)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var v = i.ToString("d3");
                    var vsum = v.Sum(p => Convert.ToInt16(p.ToString()));
                    if (hezhi.Any(m => m == vsum))
                    {

                        if (winRes != null)
                        {
                            if (winRes.Contains(vsum))
                            {
                                list.Add(v);
                                break;
                            }
                        }
                        else
                        {
                            list.Add(v);
                        }
                    }
                }
            }
            return list.Count;
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
                int num = Convert.ToInt32(item);
                if (num < 0 || num > 27) return string.Empty;
            }
            return betContent;
        }
    }
}
