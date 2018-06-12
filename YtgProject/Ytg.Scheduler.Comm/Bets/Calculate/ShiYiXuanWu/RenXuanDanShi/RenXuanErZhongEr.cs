using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanDanShi
{
    /// <summary>
    /// 单式 任选二中二 验证通过2015/05/25 2016 01 12
    /// </summary>
    public class RenXuanErZhongEr : BaseCalculate
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

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(',');
            if (items.Length < 1) return 0;
            return TotalBet(items);
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationBetContent(betContent.Replace('&', ','), 2);
        }

        private int TotalBet(string[] list)
        {
            var count = 0;
            count = list.Count(m =>  m.Split(' ').ToList().Any(n => n.Length == 2 && (Convert.ToInt32(n) >= 1 && Convert.ToInt32(n) <= 11)));
            return count;
        }

        private  int IsWin(string[] list, string result)
        {
            var opens = result.Replace(',', ' ');

            return list.Count(c => c.Split(' ').All(v => opens.IndexOf(v) != -1));
        }

        
    }
}
