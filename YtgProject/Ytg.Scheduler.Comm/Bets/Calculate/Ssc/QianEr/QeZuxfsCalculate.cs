using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前二/前二组选/组选复式 2016 01 18
    /// </summary>
    public class QeZuxfsCalculate : FiveSpecialDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            Combinations<string> c = new Combinations<string>(item.BetContent.Select(m => m.ToString()).ToList(), 2);
            var list = c._combinations;
            var count = 0;
            var res = openResult.Replace(",", "").Substring(0, 2);
            var t0 = res.Substring(0, 1);
            var t1 = res.Substring(1, 1);
            if (t0 == t1) return;
            foreach (var l in list)
            {
                if (l.IndexOf(t0) > -1 && l.IndexOf(t1) > -1) count++;
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Length, 2);
        }
    }
}
