using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSan
{
    /// <summary>
    /// 组选和值
    /// </summary>
    public class RenXuanZuXuanHeZhi : BaseRenXuan
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult=openResult.Replace(",","");
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            List<int> winRes = new List<int>();

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
            List<string> lst = new List<string>();
            foreach (var c in v._combinations)
            {
                var p = openResult[c[0] - 1].ToString();
                var p1 = openResult[c[1] - 1].ToString();
                var p2 = openResult[c[2] - 1].ToString();
                lst.Add(p);
                lst.Add(p1);
                lst.Add(p2);

                var sum = Convert.ToInt32(p) + Convert.ToInt32(p1) + Convert.ToInt32(p2);
                winRes.Add(sum);
            }

            int zuliu = 0, zusan = 0;
            var count = TotalBet(content, winRes, ref zuliu, ref zusan);
            zuliu = 0;
            zusan = 0;

            if (lst.Distinct().Count() == 2)
                zusan = 1;
            else
                zuliu = 1;

            if (count > 0)
            {
                item.IsMatch = true;
                if (zuliu > 0)
                    item.WinMoney = TotalWinMoney(item, GetBaseAmtLstItem(2, item), 0M, 1);
                if (zusan > 0)
                    item.WinMoney = TotalWinMoney(item, GetBaseAmtLstItem(1, item), 0M, 1);
            }
        }

        /// <summary>
        /// 计算注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {

            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            if (string.IsNullOrEmpty(content) || content.Length < 1 || postionStr.Length < 3)
                return 0;

            int zuliu = 0, zusan = 0;
            int count=TotalBet(content,null,ref zuliu,ref zusan);
            int notes = 0;
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 3);
             
            foreach (var c in v._combinations)
            {
                notes += count;
            }

            return notes;

        }

        #region 计算具体组合及注数
        private int TotalBet(string content, List<int> winRes, ref int zuliu, ref int zusan)
        {
            List<int> hezhi = content.Split(',').Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();
            if (hezhi.Count > 0)
            {
                for (int i = 0; i < 1000; i++)
                {
                    string v = i.ToString("d3");
                    var vsum = v.Sum(p => Convert.ToInt16(p.ToString()));
                    var count = v.Distinct().Count();
                    if (count > 1)
                    {
                        if (hezhi.Any(m => v.Sum(n => Convert.ToInt16(n.ToString())) == m) && !list.Any(n => string.Join("", n.OrderBy(s => s)) == string.Join("", v.OrderBy(s => s))))
                        {
                            if (winRes == null)
                                list.Add(v);
                            else if (winRes.Contains(vsum))
                            {
                                if (count == 2) zusan++;
                                else if (count == 3) zuliu++;
                                list.Add(v);
                            }
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

            foreach (var item in contentArray)
            {
                int parseValue = Convert.ToInt32(item);
                if (parseValue < 1 || parseValue > 26)
                    return string.Empty;
            }
            return betContent;
        }
    }
}
