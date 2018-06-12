using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星直选/直选复式  2016 01 17 
    /// </summary>

    public class ZxfsCalculate : ZhiXuanFushiDetailsCalculate
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return;
            else
            {
                var list = GetAllBets(item);
                var result = list.Find(m => m == openResult.Replace(",", ""));
                if (result != null)
                {
                    item.IsMatch = true;
                    decimal stepAmt = 0;
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
                if (bets.Count() != 5)
                {
                    return null;
                }
                else
                {
                    var list = new List<string>();
                    var wan = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var qian = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var bai = bets[2].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var shi = bets[3].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var ge = bets[4].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    list = (from w in wan
                            from q in qian
                            from b in bai
                            from s in shi
                            from g in ge
                            select string.Format("{0}{1}{2}{3}{4}", w, q, b, s, g)).ToList();
                    return list;
                }
            }
        }
    }
}
