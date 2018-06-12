using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Pk10
{
    /// <summary>
    /// 前三复试
    /// </summary>
    public class PkQianYiFushiCalculate: BaseCalculate
    {

        private bool IsWin(string[] list, string openResult)
        {
            //所选位数上的号码与开奖的位数上的号码一致即为中奖  01,04,08,09,02,10,03,06,07,05
            return list.Contains(openResult.Split(',')[0]);
        }

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent.Split(' '), openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(' ').Length;

        }

        public override string HtmlContentFormart(string betContent)
        {
            var newContent = betContent.Replace('|', ',').Replace('&', ' ');
            newContent = VerificationSelectBetContent11x5Many(newContent);
            if (newContent.Split(',').Length == 1)
                return newContent;
            return string.Empty;
        }

    }
}
