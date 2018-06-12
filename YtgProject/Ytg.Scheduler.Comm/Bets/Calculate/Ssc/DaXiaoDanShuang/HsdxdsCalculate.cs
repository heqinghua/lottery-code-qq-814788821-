using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 大小单双/大小单双/后三大小单双 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class HsdxdsCalculate : BaseDxds
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //大小单双6,8,0,1,5 单双单
            var res = openResult.Remove(0, 4).Split(',').Select(m => Convert.ToInt32(m)).ToList();
            var temp = item.BetContent.Split(',');
            var bd = res[0] >= 5 ? Da.ToString() : Xiao.ToString();
            var bds = res[0] % 2 == 0 ? Shuang.ToString() : Dan.ToString();

            var sd = res[1] >= 5 ? Da.ToString() : Xiao.ToString();
            var sds = res[1] % 2 == 0 ? Shuang.ToString(): Dan.ToString();

            var gd = res[2] >= 5 ? Da.ToString(): Xiao.ToString();
            var gds = res[2] % 2 == 0 ? Shuang.ToString() : Dan.ToString();


            var count = 0;
            count = temp[0].Split('-').Count(m => "-" + m.ToString() == bd || "-" + m.ToString() == bds)
                * temp[1].Split('-').Count(m => "-" + m.ToString() == sd || "-" + m.ToString() == sds)
                * temp[2].Split('-').Count(m => "-" + m.ToString() == gd || "-" + m.ToString() == gds);
            if (count >0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent= base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Split(',').Length != 3)
                return string.Empty;
            return betContent;
        }
    }
}
