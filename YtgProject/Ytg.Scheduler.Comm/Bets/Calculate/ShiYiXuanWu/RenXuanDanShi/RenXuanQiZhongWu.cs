using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanDanShi
{
    /// <summary>
    /// 七中五 验证通过2015/05/25 2016 01 12
    /// </summary>
    public class RenXuanQiZhongWu : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winCount=IsWin(item.BetContent.Split(','), openResult);
            if (winCount>0)
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
            return VerificationBetContent(betContent.Replace('&', ','), 7);
        }

        private int IsWin(string[] list, string result)
        {
            var opens = result.Replace(',', ' ');

            return list.Count(c => c.Split(' ').Count(v => opens.IndexOf(v) != -1) >= 5);
        }

        
    }
}
