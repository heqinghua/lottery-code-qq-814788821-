using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三其它/和值尾数 验证通过 2015/05/23  2016 01 18
    /// </summary>
    public class HezwsCalculate : FiveSpecialDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //计算具体情况
            var res = openResult.Remove(0, 4).Split(',').Sum(m => Convert.ToInt32(m));
            var weishu = Convert.ToInt32(res.ToString().Last().ToString());
            if (item.BetContent.Contains(weishu.ToString()))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Length;
        }
    }
}
