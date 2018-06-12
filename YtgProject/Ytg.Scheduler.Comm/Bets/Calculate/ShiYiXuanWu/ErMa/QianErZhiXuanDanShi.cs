using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.ErMa
{
    /// <summary>
    /// 广东十一选五/三码/前二直选单式  验证通过 2015/05/24 2016 01 12
    /// </summary>
    public class QianErZhiXuanDanShi : BaseCalculate
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
            return item.BetContent.Split(',').Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var htmlContent = betContent.Replace("&", ",");
            return VerificationBetContent(htmlContent, 2);
        }


        private  bool IsWin(string[] list, string openResult)
        {
            var flag = false;
            var items = openResult.Split(',');
            var open = string.Join(" ", items.Take(2));
            if (list.Count(m => m == open) > 0) flag = true;
            return flag;
        }

        
    }
}
