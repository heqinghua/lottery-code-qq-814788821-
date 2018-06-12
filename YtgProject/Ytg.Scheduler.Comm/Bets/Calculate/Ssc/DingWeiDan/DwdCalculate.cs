using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 定位胆 验证通过 2015/05/24
    /// </summary>
    public class DwdCalculate : ZhiXuanFushiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //
            var res = openResult.Split(',');//得到每一位的投注数
            var pos = item.BetContent.Split('_')[1];//位置

            var temp = item.BetContent.Split('_')[0].Select(x=>x.ToString());
            int winCount = 0;
            for (int i = 0; i < res.Length; i++)
            {
                var curPos = i + 1;
                if (string.IsNullOrEmpty(res[i]) || curPos.ToString() != pos)
                    continue;
                //每一位开奖结果
                if (temp.Contains(res[i]))
                {
                    winCount++;
                }
            }
            var stepAmt = 0m;
            if (winCount > 0)
            {
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                item.IsMatch = true;
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split('_')[0].Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            if (betContent.IndexOf("_") == -1)
                return string.Empty;
            string pos = betContent.Split('_')[1];
            var array = betContent.Split('_')[0].Select(x => x.ToString());
            if (array.Any(m => Convert.ToInt32(m) < 0 || Convert.ToInt32(m) > 9)) return string.Empty;
            return betContent ;
        }
    }
}
