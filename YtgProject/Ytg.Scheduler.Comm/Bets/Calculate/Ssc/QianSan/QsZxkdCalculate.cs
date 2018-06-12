using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三直选/直选跨度  验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class QsZxkdCalculate : FiveSpecialDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = TotalBet(item, openResult);
            if (count > 0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return TotalBet(item, "");
        }

        #region 计算具体组合及注数
        private int TotalBet(BasicModel.LotteryBasic.BetDetail item, string openResult)
        {
            var cha = 0;
            if (!string.IsNullOrEmpty(openResult))
            {
                var res = openResult.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                var temp = res.Take(3);
                cha = temp.Max() - temp.Min();
            }
            List<int> kuadu = item.BetContent.Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();
            if (kuadu.Count >= 1)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var v = i.ToString("d3");
                    var min = v.Min(p => Convert.ToInt16(p.ToString()));
                    var max = v.Max(p => Convert.ToInt16(p.ToString()));
                    if (kuadu.Any(m => m == (max - min)))
                    {
                        if (string.IsNullOrEmpty(openResult))
                            list.Add(v);
                        else if (cha == (max - min))
                        {
                            list.Add(v);
                        }
                    }
                }
            }
            return list.Count;
        }
        #endregion
    }
}
