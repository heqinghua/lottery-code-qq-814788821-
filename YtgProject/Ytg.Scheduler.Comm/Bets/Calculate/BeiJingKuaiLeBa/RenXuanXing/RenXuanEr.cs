using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.RenXuanXing
{
    /// <summary>
    /// 任选2  验证通过 2015/05/25
    /// </summary>
    public class RenXuanEr : BaseCalculate
    {
  
 
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var baseAmt = this.GetBaseAmtLstItem(2,item);
            //每注奖金25
            //开奖数据
            var opens = openResult.Split(',');
            //投注数据
            var bets =item.BetContent.Split(' ');
            //统计opens bets里面有多少数据在里面。
            if (bets.Length < 2 || bets.Length > 8) return;
        
            Combinations<string> com = new Combinations<string>(bets,2);
            var source= com._combinations;
            var isWin = source.Count(c=>c.All(x=>opens.Contains(x)))>0;

            if (isWin)
            {
                item.IsMatch = true;
                item.WinMoney = baseAmt;
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(' ');
            if (items.Length < 2 || items.Length > 8) return 0;
            return Cmn(items.Length, 2);
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
