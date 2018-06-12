using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三组选/组选和值  验证通过 2015/05/23  2016 01 18
    /// </summary>
    public class QsZxuanhzCalculate : HouSanZxhzCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            int zuliu = 0, zusan = 0;
            var count = TotalBet(item, openResult, ref zuliu, ref zusan);
            if (count > 0)
            {
                item.IsMatch = true;
                if (zuliu > 0)//item.BonusLevel == 1700 ? 280 : 300
                    item.WinMoney = TotalWinMoney(item, GetBaseAmtLstItem(2, item), 0M, 1);
                if (zusan > 0)// item.BonusLevel == 1700 ? 570 : 600
                    item.WinMoney = TotalWinMoney(item, GetBaseAmtLstItem(1, item), 0M, 1);
            }
        }

        /// <summary>
        /// 计算注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            int zuliu = 0, zusan = 0;
            return TotalBet(item, "", ref zuliu, ref zusan);
        }

        #region 计算具体组合及注数
        private int TotalBet(BasicModel.LotteryBasic.BetDetail item, string openResult, ref int zuliu, ref int zusan)
        {
            var sum = 0;
            var res = new List<string>();
            var first = new List<string>();

            if (!string.IsNullOrEmpty(openResult))
            {
                res = openResult.Split(',').ToList();
                first = res.Take(3).ToList();
                sum = first.Sum(m => Convert.ToInt32(m));
            }
            List<int> hezhi = item.BetContent.Split(',').Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();
            if (hezhi.Count > 0)
            {
                for (int i = 0; i < 1000; i++)
                {
                    string v = i.ToString("d3");
                    var vsum = v.Sum(p => Convert.ToInt16(p.ToString()));
                    var count = v.Distinct().Count();
                    if (count > 1)
                    {
                        if (hezhi.Any(m => v.Sum(n => Convert.ToInt16(n.ToString())) == m) && !list.Any(n => string.Join("", n.OrderBy(s => s)) == string.Join("", v.OrderBy(s => s))))
                        {
                            if (string.IsNullOrEmpty(openResult))
                                list.Add(v);
                            else if (sum == vsum)
                            {
                                if (count == 2) zusan++;
                                else if (count == 3) zuliu++;
                                list.Add(v);
                            } 
                        }
                    }
                }
            }

            zuliu = 0;
            zusan = 0;

            if (first.Distinct().Count() == 2)
                zusan = 1;
            else
                zuliu = 1;

            return list.Count;
        }
        #endregion


        
    }
}
