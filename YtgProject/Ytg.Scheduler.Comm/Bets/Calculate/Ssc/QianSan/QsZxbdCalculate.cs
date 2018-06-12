using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三组选/组选包胆 2016 01 18
    /// </summary>
    public class QsZxbdCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //因为只能投一个号码，所以...
            var res = openResult.Substring(0, 5).Split(',');//表示后三位
            var count = res.Distinct().Count();
            if (res.Contains(item.BetContent) && count != 1)
            {
                var zsan = this.GetBaseAmtLstItem(1,item);
                var zlan = this.GetBaseAmtLstItem(2, item);
                var baseAmt = 0m;
                if (count == 2)
                    baseAmt = zsan;
                else
                    baseAmt = zlan;

                item.IsMatch = true;
                //item.BonusLevel == 1700 ? count == 2 ? 570 : 280 : count == 2 ? 600 : 300
                item.WinMoney = TotalWinMoney(item, baseAmt, 0M, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return 54;
        }

        public override string HtmlContentFormart(string betContent)
        {
            
            int outValue;
            if (!int.TryParse(betContent, out outValue))
                return string.Empty;
            if (outValue < 0 || outValue > 9)
                return string.Empty;
            return betContent;

        }
    }
}
