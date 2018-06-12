using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三组选/组六单式 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class ZuLiuDanShiCalculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //百位，十位，个位
            string temp= openResult.Replace(",", "");
            if(temp.Length==5)
                temp = temp.Remove(0, 2);

            var result = string.Join("", temp.OrderBy(m => m.ToString()).ToList());
            var res = item.BetContent.Split(',');
            var count = 0;
            foreach (var bet in res)
            {
                if (bet.Distinct().Count() == 3 && string.Join("", bet.OrderBy(m => m.ToString()).ToList()) == result)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                // item.BonusLevel == 1700 ? 280 : 300
                item.WinMoney = TotalWinMoney(item,GetBaseAmt(item), item.BonusLevel == 1700 ? 0.36M : 0.34M, count);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            //这里有验证规则
            var count = 0;
            var res = item.BetContent.Split(',');
            foreach (var bet in res)
            {
                if (bet.Distinct().Count() == 3)
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
                return 3;
            }
        }

        protected override int ItemLen
        {
            get
            {
                return 3;
            }
        }

        protected override bool IsSort
        {
            get
            {
                return true;
            }
        }
    }
}
