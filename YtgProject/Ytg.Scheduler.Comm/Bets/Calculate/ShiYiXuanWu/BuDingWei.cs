using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu
{
    /// <summary>
    /// 广东十一选五/不定位/不定位 2016 01 12
    /// </summary>
    public class BuDingWei : BaseCalculate
    {
      

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winCount = IsWin(item.BetContent.Split(','), openResult);
            if (winCount>0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, winCount);
            }
        }

        private  int IsWin(string[] list, string result)
        {
            var winCount = 0;
            var opens = result.Split(',').Take(3);
            var ary = result.Split(',').Take(3);
            foreach(var item in list) {
                if (ary.Contains(item))
                    winCount++;
            }
            return winCount;
            //if (opens.Any(m => list.Any(n => n == m))) flag = true;
            //return flag;
        }
        public override string HtmlContentFormart(string betContent)
        {
            return VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }
    }
}
