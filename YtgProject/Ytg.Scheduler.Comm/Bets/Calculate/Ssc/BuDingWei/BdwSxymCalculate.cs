using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Comm;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 不定位/四星/四星一码  验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class BdwSxymCalculate : ZhiXuanDanShiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //前三位至少出现一个1
            var res = openResult.Remove(0, 2);
            res = Utils.ClearRepeat(res);
            var temp = item.BetContent.Replace(",", "").Select(m => Convert.ToInt32(m.ToString()));
            foreach (var bet in temp)
            {
                if (res.Contains(bet.ToString()))
                {
                    var stepAmt = 0m;
                    item.WinMoney += TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                    //break;
                }
            }
            if (item.WinMoney > 0) item.IsMatch = true;
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Replace(",", "").Length;
        }

       

        protected override int GroupLen
        {
            get
            {
                return 1;
            }
        }

        protected override int ItemLen
        {
            get
            {
                return 1;
            }
        }
    }
}
