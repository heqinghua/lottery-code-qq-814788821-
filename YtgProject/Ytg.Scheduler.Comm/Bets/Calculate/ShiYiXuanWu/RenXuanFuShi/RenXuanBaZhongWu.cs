using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanFuShi
{
    /// <summary>
    /// 验证通过 2015//05 /25
    /// </summary>
    public class RenXuanBaZhongWu : BaseCalculate
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            var items = result.Split(',');
            if (items.Length < 8) return;
        
            var winCount = IsWin(items.ToList(), openResult);
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
            if (items.Length < 8) return 0;
            return Cmn(items.Length, 8);
        }
        public override string HtmlContentFormart(string betContent)
        {
            var newCt = VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
            if (newCt.Split(',').Length < 8)
                return "";
            return newCt;
        }
        private int IsWin(List<string> list, string result)
        {
            var opens = result.Split(',') ;
            Combinations<string> c = new Combinations<string>(list, 8);
            var source = c._combinations;

            return source.Count(r=>opens.All(a=>r.Contains(a)));
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
