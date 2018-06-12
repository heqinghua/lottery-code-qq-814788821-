using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSi
{
    /// <summary>
    /// 组选6 2016 01 21
    /// </summary>
    public class RenXuanZu6 : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            //开奖结果
            openResult = openResult.Replace(",", "");
            
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
            List<string> winRes = new List<string>();
            foreach (var b in v._combinations)
            {
                var res = string.Join("", openResult[b[0]-1], openResult[b[1]-1], openResult[b[2]-1], openResult[b[3]-1]).OrderBy(c=>c);
                winRes.Add(string.Join("",res));
            }

            Combinations<string> cx = new Combinations<string>(content.Replace(",","").Select(t => t.ToString()).ToList(), 2);
            var list = cx._combinations;
            var count = 0;
            if (list != null && list.Count > 0)
            {
                //二重号，一定需要后4位只有2个数字
                count = list.Count(m => winRes.Count(w => string.Join("",w.Distinct())==string.Join("", m.OrderBy(c=>c))) > 0);//temp.Distinct().Count() == 2 && 
            }
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
            if (!string.IsNullOrEmpty(content) && postionStr.Length >= 4)
            {
                int count = CombinationHelper.Cmn(content.Replace(",","").Length, 2);

                Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
                int notes = 0;
                foreach (var b in v._combinations)
                {
                    notes += count;
                }

                return notes;
            }

            return 0;
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
