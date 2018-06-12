using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三组合/组三单式 2015/05/24 验证通过  2016 01 18
    /// </summary>
    public class QsZsdsCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //百位，十位，个位
            var temp = openResult.Replace(",", "").Substring(0, 3);
            var result = string.Join("", temp.OrderBy(m => m.ToString()).ToList());
            var res = item.BetContent.Split(',');
            var count = 0;
            foreach (var bet in res)
            {
                if (string.Join("", bet.OrderBy(m => m.ToString()).ToList()) == result)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                var stepAmt = 0m;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            //这里有验证规则
            var count = 0;
            var res = item.BetContent.Split(',');
            foreach (var bet in res)
            {
                if (bet.Length == 3 && bet.Distinct().Count() == 2)
                {
                    count++;
                }
            }
            return count;
        }

        protected override int GroupLen
        {
            get
            {
                return 2;
            }
        }

        protected override int ItemLen
        {
            get
            {
                return 3;
            }
        }
    }
}
