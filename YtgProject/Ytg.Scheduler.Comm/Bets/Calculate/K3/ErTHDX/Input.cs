using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErTHDX
{
    /// <summary>
    /// 手动输入 二同号单选  112&223&233
    /// </summary>
    public class Input : BiaoZhun
    {


        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult =string.Join("", openResult.Replace(",", "").OrderBy(c=>c.ToString()));
            var itms=item.BetContent.Split(',');
            int count=0;
            foreach (var im in itms)
            {
                if (!openResult.Contains(string.Join("", im.OrderBy(d => d.ToString()))))
                    continue;

                count++;
                break;
            }

            if (count>0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT);
            }
        }
        public override string HtmlContentFormart(string betContent)
        {
            //112&223&224&225
            betContent = betContent.Replace('&', ',');
            return this.VerificationBetContentOneNum(betContent, 2, 1, 6);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }
    }
}
