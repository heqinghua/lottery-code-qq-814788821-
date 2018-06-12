using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanFuShi
{
    /// <summary>
    /// 验证通过 2015/05/25 2015 01 12
    /// </summary>
    public class RenXuanLiuZhongWu : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winCount = IsWin(item.BetContent.Split(','), openResult);
            if (winCount>0)
            {
                decimal stepAmt = 0;
                item.IsMatch = true;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, winCount);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            //的第一位、第二位、第三位开奖号码中包含所选号码，即为中奖。
            var result = item.BetContent;
            var items = result.Split(',');
            if (items.Length < 6) return 0;
            return Cmn(items.Length, 6);
           
        }
        public override string HtmlContentFormart(string betContent)
        {
            var newCt = VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
            if (newCt.Split(',').Length < 6)
                return "";
            return newCt;
        }

        private int IsWin(string[] list, string result)
        {
            var opens = result.Split(','); 

            Combinations<string> com = new Combinations<string>(list, 6);
            var source = com._combinations;

            return source.Count(s =>opens.All(o=>s.Contains(o)));
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
