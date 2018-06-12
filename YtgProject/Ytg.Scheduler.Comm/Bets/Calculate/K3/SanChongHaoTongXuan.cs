using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3
{
    /// <summary>
    /// 三重号通选
    /// </summary>
    public class SanChongHaoTongXuan : BaseCalculate
    {
        /// <summary>
        /// 三重号通选 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT = 50;

        string[] constArray = new string[] { "111", "222", "333", "444", "555", "666" };

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            if (constArray.Contains(openResult))
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT);
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
