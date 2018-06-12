using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.QuWei
{
    /// <summary>
    /// 上下盘
    /// </summary>
    public class ShangXiaPan : BaseCalculate
    {
        /// <summary>
        /// 上
        /// </summary>
        public const int Shang = -23;
        /// <summary>
        /// 中
        /// </summary>
        public const int Zhong = -24;
        /// <summary>
        /// 下
        /// </summary>
        public const int Xia = -25;



        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {

            var winAmt = 0m;
            //开奖数据
            var opens = openResult.Split(',');
            //投注数据
            var bets = item.BetContent.Split(' ');
            var betCount = bets.Length;

            var shangCount = opens.Count(m => Convert.ToInt32(m) >= 0 && Convert.ToInt32(m) <= 40);
            if (shangCount == 20 && bets.Contains(Shang.ToString())) winAmt += GetBaseAmtLstItem(2, item);
            if (shangCount == 10 && bets.Contains(Zhong.ToString())) winAmt += GetBaseAmtLstItem(1, item);
            if (shangCount == 0 && bets.Contains(Xia.ToString())) winAmt += GetBaseAmtLstItem(3, item);

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
