using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.ErMa
{
    /// <summary>
    /// 广东十一选五/三码/前二组选复式 验证通过 2015/05/25 2016 01 12
    /// </summary>
    public class QianErZuXuanFuShi : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent, openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return Cmn(item.BetContent.Split(',').Length, 2);
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);

        }

        private  List<string> TotalBet(string[] items)
        {
            var list = new List<string>();
            foreach (var item in items)
            {
                var nums = item.Split(' ');
                if (nums.Distinct().Count() != 2) continue;
                if (nums.Any(m => m.Length == 2 || (Convert.ToInt32(m) > 0 && Convert.ToInt32(m) < 12))) list.Add(item);
            }
            list.ForEach(m =>
            {
                Console.WriteLine(m);
            });
            return list;
        }

        private  bool IsWin(string result, string openResult)
        {
            var flag = false;
            var items = openResult.Split(',');
            var open = items.Take(2);
            flag = open.All(m => result.IndexOf(m) > -1);
            return flag;
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
