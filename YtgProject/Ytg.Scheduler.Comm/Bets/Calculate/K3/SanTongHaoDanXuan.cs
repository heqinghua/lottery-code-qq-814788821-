using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3
{
    /// <summary>
    /// 三同号单选
    /// </summary>
    public class SanTongHaoDanXuan:BaseCalculate
    {
        /// <summary>
        /// 二同号单选奖金 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT = 320;
        
        string[] constArray = new string[] { "111", "222", "333", "444", "555", "666" };

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            foreach (var idx in item.BetContent.Split(','))
            {
                int outIdx =Convert.ToInt32(idx);
                if (openResult==constArray[outIdx - 1])
                {
                    item.IsMatch = true;
                    item.WinMoney = BiaoZhunTotalWinMoney(item, AMT); 
                    return;
                }
                
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationSelectBetContent(betContent.Replace("&", ","), 1, 6, 1);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }
    }
}
