using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三其他/特殊  2016 01 18
    /// </summary>
    public class QsSpecialCalculate : SpecialDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //-10 -11 -12
            decimal stepAmt = 0;
            var bzAmt = this.GetBaseAmtLstItem(1, item);//豹子
            var szAmt = this.GetBaseAmtLstItem(2, item);//顺子
            var dzAmt = this.GetBaseAmtLstItem(3, item);//对子

            //-10 -11 -12
            var res = openResult.Substring(0, 5).Split(',');//得到后三位数字
            if (item.BetContent.Contains("-10"))//豹子
            {
                if (res.Distinct().Count() == 1)
                {
                    item.WinMoney += TotalWinMoney(item, bzAmt, stepAmt, 1);
                }
            }
            if (item.BetContent.Contains("-11"))//顺子
            {
                var temp = res.OrderBy(m => m).Select(m => Convert.ToInt32(m)).ToList();
                if ((temp[1] - temp[0]) == (temp[2] - temp[1]) && (temp[2] - temp[1]) == 1)
                {
                    item.WinMoney += TotalWinMoney(item, szAmt, stepAmt, 1);
                }
            }
            if (item.BetContent.Contains("-12"))//对子
            {
                if (res.Distinct().Count() == 2)
                {
                    item.WinMoney += TotalWinMoney(item, dzAmt, stepAmt, 1);
                }
            }
            if (item.WinMoney > 0) item.IsMatch = true;
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split('-').Where(c=>c!="").Count();
        }

        
    }
}
