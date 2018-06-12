using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 大小单双/大小单双/前三大小单双 验证通过 2015/05/24  2016 01 18
    /// </summary>
    public class QsdxdsCalculate : BaseDxds
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //大小单双
            var res = openResult.Substring(0, 5).Split(',').Select(m => Convert.ToInt32(m)).ToList();
            var temp = item.BetContent.Split(',');
            var wd = res[0] >= 5 ? Da.ToString(): Xiao.ToString();
            var wds = res[0] % 2 == 0 ? Shuang.ToString() : Dan.ToString();

            var qd = res[1] >= 5 ? Da.ToString() :Xiao.ToString();
            var qds = res[1] % 2 == 0 ? Shuang.ToString(): Dan.ToString();

            var bd = res[2] >= 5 ? Da.ToString() : Xiao.ToString();
            var bds = res[2] % 2 == 0 ? Shuang.ToString() : Dan.ToString();


            var count = 0;
            count = temp[0].Split('-').Count(m => "-" + m.ToString() == wd || "-" + m.ToString() == wds)
                * temp[1].Split('-').Count(m => "-" + m.ToString() == qd || "-" + m.ToString() == qds)
                * temp[2].Split('-').Count(m => "-" + m.ToString() == bd || "-" + m.ToString() == bds);
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
            if (betContent.Split(',').Length != 3)
                return string.Empty;
            return betContent;
        }
        
    }
}
