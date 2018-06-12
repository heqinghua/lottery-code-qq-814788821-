using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.SanMa
{
    /// <summary>
    /// 广东十一选五/三码/前三组选复式  验证通过 2015/05/24 2016/01/12
    /// </summary>
    public class QianSanZuXuanFuShi : BaseCalculate
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
            return Cmn(item.BetContent.Split(',').Length, 3);
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationSelectBetContent11x5(betContent.Replace('&', ','),false);

        }

        private bool IsWin(string result, string openResult)
        {
            var items = openResult.Split(',');
            var open = "";
            items.Take(3).OrderBy(v=>v).ToList().ForEach(v=>open+=v);
           
            Combinations<string> c = new Combinations<string>(result.Split(','), 3);
            var list = c._combinations;
            return list.Count(m => open.Contains(string.Join("", m.OrderBy(n => n).Select(n => n))))>0;
        }

        static int Fac(int k)
        {
            int t = 1;
            for (int i = 1; i <= k; i++)
                t *= i;
            return t;
        }
        static int Cmn(int M, int N)
        {
            int p;
            p = Fac(M) / (Fac(N) * Fac(M - N));
            return p;
        }

        
    }
}
