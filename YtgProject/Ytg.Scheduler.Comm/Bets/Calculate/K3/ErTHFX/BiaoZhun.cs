using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErTHFX
{
    /// <summary>
    /// 二同号复选 标准选号 2016 01 13
    /// </summary>
    public class BiaoZhun : BaseCalculate
    {
        /// <summary>
        /// 二同号单选奖金 默认7.5的返点 3&4&5
        /// </summary>
        protected readonly decimal AMT = 15 ;

        //
        string[] constArray = new string[]{ "11", "22", "33", "44", "55", "66" };


        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            foreach (var idx in item.BetContent.Split(','))
            {
                int outIdx=Convert.ToInt32(idx);
                if (openResult.Contains(constArray[outIdx-1]))
                {
                    item.IsMatch = true;
                    item.WinMoney = BiaoZhunTotalWinMoney(item, AMT);
                    return;
                }
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            //            2&3&4
            return this.VerificationSelectBetContent(betContent.Replace("&", ","), 1, 6, 1);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;

        }
    }
}
