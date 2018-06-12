using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    /// <summary>
    /// 直选单式  验证通过   2016 01 20
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
            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 2);
            foreach (var b in v._combinations)
            {
                string winResult = result[b[0]-1].ToString() + result[b[1]-1].ToString();
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
            string content= SplitRenXuanContent(item.BetContent,ref postionStr);
            if (!string.IsNullOrEmpty(content) && postionStr.Length >= 2)
            {
                var ctLength = content.Split(',').Length;
                int notes = 0;
                Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c)).ToList(), 2);
                foreach (var b in v._combinations)
                {
                    notes += ctLength;
                }

                return notes;
            }

            return 0;
        }
    }
}
