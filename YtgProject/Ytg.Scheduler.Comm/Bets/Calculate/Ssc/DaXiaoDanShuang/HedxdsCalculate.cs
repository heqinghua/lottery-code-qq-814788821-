using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 大小单双/大小单双/后二大小单双 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class HedxdsCalculate : BaseDxds
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            
            //大小单双1,2,3,4,5
            openResult = openResult.Replace(",", "");
            if (openResult.Length == 5)
                openResult = openResult.Remove(0, 3);
            else
                openResult = openResult.Remove(0, 1);

            var res = openResult.Select(m => Convert.ToInt32(m.ToString()));
            var temp = item.BetContent.Split(',');
            var wd = res.First() >= 5 ? Da.ToString() : Xiao.ToString();
            var wds = res.First() % 2 == 0 ? Shuang.ToString() : Dan.ToString();

            var qd = res.Last() >= 5 ?Da.ToString(): Xiao.ToString();
            var qds = res.Last() % 2 == 0 ? Shuang.ToString() : Dan.ToString();

            var count = 0;
            count = temp[0].Split('-').Count(m => "-" + m.ToString() == wd || "-" + m.ToString() == wds)
                * temp[1].Split('-').Count(m => "-" + m.ToString() == qd || "-" + m.ToString() == qds);
            if (count >0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Split(',').Length != 2)
                return string.Empty;
            return betContent;
        }



    }
}
