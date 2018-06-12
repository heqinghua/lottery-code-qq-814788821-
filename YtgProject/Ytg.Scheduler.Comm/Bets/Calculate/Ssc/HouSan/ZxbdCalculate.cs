using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三组选/组选包胆 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class ZxbdCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //因为只能投一个号码，所以...
            var res = openResult.Remove(0, 4).Split(',');//表示后三位
            var count = res.Distinct().Count();
            if (res.Contains(item.BetContent) && count != 1)
            {
                var amt = 0m;
                var baseAmt = GetBaseAmtLstItem(1, item);//组三
                var baseAmt1 = GetBaseAmtLstItem(2, item);//组六
                if (count == 2)
                    amt = baseAmt;
                else
                    amt = baseAmt1;

                item.IsMatch = true;
                item.WinMoney = TotalWinMoney(item, amt, 0M, 1);
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
