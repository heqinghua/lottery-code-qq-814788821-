using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星组选/组选120 验证通过 2015/05/23 2016 01 17
    /// </summary>
    public class Zx120Calculate : ZhiXuanDanShiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var betContentArray=item.BetContent.Select(t=>t.ToString()).ToList();
            Combinations<string> c = new Combinations<string>(betContentArray, 5);
            var list = c._combinations;
            //开奖结果
            var temp = string.Join("", openResult.Replace(",", "").OrderBy(m => m).Select(m => m.ToString()));
            var count = 0;
            if (list != null && list.Count > 0)
            {
                count = list.Count(m => temp == string.Join("", m.OrderBy(n => n).Select(n => n.ToString())));
            }
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        /// <summary>
        /// 计算具体多少注
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 5);
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
