using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.QuWei
{
    /// <summary>
    /// 奇偶盘 验证通过 2015/05/25
    /// </summary>
    public class JiOuPan : BaseCalculate
    {
        /// <summary>
        /// 奇
        /// </summary>
        public const int Ji = -26;
        /// <summary>
        /// 和
        /// </summary>
        public const int He = -27;
        /// <summary>
        /// 偶
        /// </summary>
        public const int Or = -28;


        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winAmt = 0m;
            //开奖数据
            var opens = openResult.Split(',');
            //投注数据
            var bets = item.BetContent.Split(' ');
            var betCount = bets.Length;
            
            var ouCount = opens.Count(m => Convert.ToInt32(m) % 2 == 0);
            if (ouCount == 0 && bets.Contains(Ji.ToString())) winAmt += GetBaseAmtLstItem(2, item);
            if (ouCount == 10 && bets.Contains(He.ToString())) winAmt += GetBaseAmtLstItem(1, item);
            if (ouCount == 20 && bets.Contains(Or.ToString())) winAmt += GetBaseAmtLstItem(3, item);

            if (winAmt != 0)
            {
                item.IsMatch = true;
                item.WinMoney = winAmt;
            }

        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(' ').Length;
        }
    }
}
