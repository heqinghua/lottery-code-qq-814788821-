using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    
    /// <summary>
    /// 组三复试 2016 01 21
    /// </summary>
    public class RenXuanZuSanFuShi : BaseRenXuan
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            openResult= openResult.Replace(",","");

            List<string> winRes = new List<string>();
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            foreach (var c in v._combinations)
            {
                winRes.Add(string.Format("{0}{1}{2}", openResult[c[0]-1], openResult[c[1]-1], openResult[c[2]-1]));             
            }
            content = content.Replace(",", "");
            var count = TotalBet(content, winRes);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            if (string.IsNullOrEmpty(content) || content.Length < 1 || postionStr.Length < 3)
                return 0;
            content = content.Replace(",", "");

            int notes = 0;
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            foreach (var c in v._combinations)
            {
                notes += TotalBet(content, null);
            }

            return notes;
        }

        #region 计算具体组合及注数

        private int TotalBet(string content,List<string> winRes)
        {
            List<int> zusan = content.Select(c => Convert.ToInt32(c.ToString())).ToList();
            List<string> list = new List<string>();
            if (zusan.Count >= 2)
            {
                foreach (var item in zusan)
                {
                    var arr = zusan.Where(m => m != item);
                    var v = string.Format("{0}{0}", item);
                    foreach (var m in arr)
                    {
                        if (winRes==null)
                            list.Add(string.Format("{0}{1}", v, m));
                        else
                        {
                            //判断中奖情况
                            // res里面3个值==item 和v
                            if (winRes.Where(c => c.Count(n => n.ToString() == item.ToString()) == 2 && c.Count(n => n.ToString() == m.ToString()) == 1).Count()>0)
                            {
                                list.Add(string.Format("{0}{1}", v, m));
                                break;
                            }
                            //if ((res.Count(n => n == item.ToString()) == 2 && res.Count(n => n == m.ToString()) == 1))
                            //{
                            //    list.Add(string.Format("{0}{1}", v, m));
                            //}
                        }
                    }
                }
            }
            return list.Count;
        }
        #endregion


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
