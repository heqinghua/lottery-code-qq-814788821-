using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 前三/前三直选/直选复式 验证通过 2015/05/24
    /// </summary>
    public class QsZxfsCalculate : ZhiXuanFushiDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return;
            else
            {
                var list = GetAllBets(item);
                var result = list.Find(m => m == openResult.Replace(",", "").Substring(0, 3));
                if (result != null)
                {
                    item.IsMatch = true;
                    var stepAmt = 0m;
                    item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                }
            }
        }

        /// <summary>
        /// 根据投注内容得到所有的组合情况
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllBets(BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent))
                return null;
            else
            {
                var bets = item.BetContent.Split(',');
                if (bets.Count() != 3)
                {
                    return null;
                }
                else
                {
                    var list = new List<string>();
                    var wan = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var qian = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var bai = bets[2].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    list = (from w in wan
                            from q in qian
                            from b in bai
                            select string.Format("{0}{1}{2}", w, q, b)).ToList();
                    return list;
                }
            }
        }

        protected override int GetLen
        {
            get
            {
                return 3;
            }
        }
    }
}
