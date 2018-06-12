using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanDanTuo
{
    /// <summary>
    /// 验证通过2015/05/25
    /// </summary>
    public class RenXuanSiZhongSi : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent.Split(','), openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            //胆码最多两个号码，一个号码不能同时出现在胆码和拖码中
            result = result.Trim(',');

            var items = result.Split(',');
            if (items.Length != 2) return 0;
            if (items[0].Split(' ').Length < 1 && items[0].Split(' ').Length > 3) return 0;
            if (result.Split(new char[] { ' ', ',' }).Distinct().Count() < 4) return 0;
            return TotalBet(items);
        }



        private int TotalBet(string[] list)
        {
            var count = 0;

            //需要分胆码=1 和胆码=2的情况
            var danma = list[0].Split(' ').Length;
            count = Cmn(list[1].Split(' ').Length, 4 - danma);
            return count;
        }
        private bool IsWin(string[] list, string result)
        {
            var flag = false;
            var opens = result.Split(',');
            if (list[0].Split(' ').All(m => result.IndexOf(m) > -1) && opens.Any(m => list[1].IndexOf(m) > -1)) flag = true;
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
