using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanDanShi
{
    /// <summary>
    /// 三中三 单式 验证通过 2015/05/25 2016 01 12
    /// </summary>
    public class RenXuanSanZhongSan : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            int winCount = IsWin(item.BetContent.Split(','), openResult);
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
            return VerificationBetContent(betContent.Replace('&', ','), 3);
        }

        private int IsWin(string[] list, string result)
        {
            var opens = result.Replace(',', ' ');

            return list.Count(c => c.Split(' ').All(v => opens.IndexOf(v) != -1));
        }

        
    }
}
