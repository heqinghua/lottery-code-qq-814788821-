using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3
{
    /// <summary>
    /// 三连号通选
    /// </summary>
    public class SanLianHao:BaseCalculate
    {
        /// <summary>
        /// 二同号单选奖金 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT = 10 ;

        string[] constArray = new string[] { "123", "234", "345", "456" };

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            if (constArray.Contains(openResult))
            {
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT); 
                item.IsMatch = true;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            return "通选";
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return 1;
        }
    }
}
