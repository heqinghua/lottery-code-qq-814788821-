using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前二/前二组选/组选单式 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class QeZuXuanDanShiCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var res = openResult.Replace(",", "").Substring(0, 2);
            if (res.Distinct().Count() == 1) return;
            var temp = item.BetContent.Split(',');
            var count = 0;
            var t0 = res.Substring(0, 1);
            var t1 = res.Substring(1, 1);
            foreach (var r in temp)
            {
                if (r.IndexOf(t0) > -1 && r.IndexOf(t1) > -1)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Where(m => m.Distinct().Count() == 2).Count();
        }

        protected override int GroupLen
        {
            get
            {
                return 2;
            }
        }

        protected override bool IsSort
        {
            get
            {
                return true;
            }
        }


        protected override int ItemLen
        {
            get
            {
                return 2;
            }
        }
    }
}
