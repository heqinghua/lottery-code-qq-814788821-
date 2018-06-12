using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.SanMa
{
    /// <summary>
    /// 广东十一选五/三码/前三直选单式   验证通过 2015/05/24   2016/01/12
    /// </summary>
    public class QiSanZhiXuanDanShi : BaseCalculate
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent.Split(','), openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent.Trim(',');
            var items = result.Split(',');
            return items.Length;
        }


       
        public override string HtmlContentFormart(string betContent)
        {
            var htmlContent = betContent.Replace("&", ",");
            return VerificationBetContent(htmlContent, 3);
        }

        private  bool IsWin(string[] list, string openResult)
        {
            var items = openResult.Split(',');
            var open = string.Join(" ", items.Take(3));
            var count = list.Count(m => m == open);

            return count > 0;
        }

        
    }
}
