using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanFuShi
{
    /// <summary>
    /// 七中五 验证通过 2015/05/25 2016 01 12
    /// </summary>
    public class RenXuanQiZhongWu : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winCount = IsWin(item.BetContent.Split(','), openResult);
            if (winCount>0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, winCount);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            var items = result.Split(',');
            if (items.Length < 7) return 0;
            var count = Cmn(items.Length, 7);
            return count;
            
        }

        public override string HtmlContentFormart(string betContent)
        {
            var newCt = VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
            if (newCt.Split(',').Length < 7)
                return "";
            return newCt;
        }
        
        private int IsWin(string[] list, string result)
        {
            var opens = result.Split(',');
            Combinations<string> com = new Combinations<string>(list, 7);
            var source = com._combinations;
            return source.Count(s => opens.All(o => s.Contains(o))) ;
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
