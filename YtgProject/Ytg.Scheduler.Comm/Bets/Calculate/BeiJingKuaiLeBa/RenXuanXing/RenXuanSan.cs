using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.RenXuanXing
{
    /// <summary>
    /// 任选三
    /// </summary>
    public class RenXuanSan : BaseCalculate
    {
   

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //要考虑中三个和中两个的情况
            //中三个：46，中两个：6
            //开奖数据
            var opens = openResult.Split(',');
            //投注数据
            var bets = item.BetContent.Split(' ');
            var betCount = bets.Length;
            //统计opens bets里面有多少数据在里面。
            int count= opens.Count(m => bets.Any(n => m == n));
            decimal winAmt = 0;
            //3中2的可能
            //投了6个号码，中了4个，还有两个每中：现在是多少种可能中了3中三：Cmn(4,3),多少中可能中了三中二：Cmn(2,1)*Cmn(4,2)
            //找规律呀
            var baseAmt3 = this.GetBaseAmtLstItem(3, item);
            var baseAmt2 = this.GetBaseAmtLstItem(2, item);
            if (count >= 3)
            {
                
                //因为Cmn(0,1)=1所以，要加这个判断
                winAmt = Cmn(count, 3) * baseAmt3 + (betCount > count ? (Cmn(betCount - count, 1) * Cmn(count, 2) * baseAmt2) : 0);
            }
            else if (count == 2)
            {

                winAmt = Cmn(betCount - 2, 1) * baseAmt2;
            }

            if (winAmt != 0)
            {
                item.IsMatch = true;
                item.WinMoney = winAmt;
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(' ');
            if (items.Length < 3 || items.Length > 8) return 0;
            return Cmn(items.Length, 3);
        }


        int Fac(int k)
        {
            int t = 1;
            for (int i = 1; i <= k; i++)
                t *= i;
            return t;
        }
        int Cmn(int M, int N)
        {
            int p;
            p = Fac(M) / (Fac(N) * Fac(M - N));
            return p;
        }
    }
}
