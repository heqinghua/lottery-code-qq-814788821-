using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanFuShi
{
    /// <summary>
    ///  验证通过 2015/05/25 2016 01 12
    /// </summary>
    public class RenXuanYiZhongYi : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {//01,02,03
            int winCount = IsWin(item.BetContent.Split(','), openResult);
            if (winCount > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, winCount);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var newCt = VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
            if (newCt.Split(',').Length <1)
                return "";
            return newCt;
        }


        private int IsWin(string[] list, string result)
        {
            int wincount = 0;
            foreach (var item in list)
            {
                if (result.IndexOf(item) >= 0)
                    wincount++;
            }
            return wincount;
        }

        
    }
}
