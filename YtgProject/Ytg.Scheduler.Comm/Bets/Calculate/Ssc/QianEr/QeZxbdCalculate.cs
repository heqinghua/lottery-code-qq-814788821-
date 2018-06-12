using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前二/前二组选/组选包胆  验证通过 2015/05/24 2016 01 18
    /// </summary> 
    public class QeZxbdCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var res = openResult.Replace(",", "").Substring(0,2);//后两位
            if (res.Distinct().Count() == 2 && res.Contains(item.BetContent))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        /// <summary>
        /// 因为只能选择一个数字，所以就是默认9注
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return 9;
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
