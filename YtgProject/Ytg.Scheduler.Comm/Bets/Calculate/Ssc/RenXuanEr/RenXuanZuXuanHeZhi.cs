using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    /// <summary>
    /// 组选和值 2016 01 20
    /// </summary>
    public class RenXuanZuXuanHeZhi : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            int winCount = 0;
            openResult = openResult.Replace(",", "");

            List<string> winRes = new List<string>();
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var c in v._combinations)
            {
                winRes.Add(string.Format("{0}{1}",openResult[c[0]-1],openResult[c[1]-1]));
            }

            winCount = TotalBet(content, winRes);
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

            if (string.IsNullOrEmpty(content) || content.Length < 1 || postionStr.Length < 2)
                return 0;

            int notes = 0;
            int count = TotalBet(content, null); 
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var c in v._combinations)
            {
                notes += count;
            }

            return notes;
        }


        #region 计算具体中奖算法，和投注数
        private int TotalBet(string betContent,List<string> winRes)
        {
            List<int> hezhi = betContent.Split(',').Select(m => Convert.ToInt32(m.ToString())).ToList();          
            List<string> list = new List<string>();
            if (hezhi.Count > 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    string v = i.ToString("d2");
                    if (v.Distinct().Count() > 1)
                    {
                        if (hezhi.Any(m => v.Sum(n => Convert.ToInt16(n.ToString())) == m) && !list.Any(n => string.Join("", n.OrderBy(s => s)) == string.Join("", v.OrderBy(s => s))))
                        {
                            if (winRes == null)
                                list.Add(v);
                            else
                            {
                                if (winRes.Contains(v))
                                {
                                    list.Add(v);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return list.Count;
        }
        #endregion

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
            foreach (var item in contentArray)
            {
                int num=Convert.ToInt32(item);
                if (num < 1 || num > 17)
                    return string.Empty;
            }
            return betContent;
        }
    }
}
