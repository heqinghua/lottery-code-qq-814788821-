using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星直选复式 验证通过  2016 01 17
    /// </summary>
    public class ZxfsCalculate : ZhiXuanFushiDetailsCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return;
            else
            {
                var list = GetAllBets(item);
                var result = list.Find(m => m == openResult.Replace(",", "").Substring(1,4));
                if (result != null)
                {
                    item.IsMatch = true;
                    decimal stepAmt = 0;
                    item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                }
            }
        }

        protected override int GetLen
        {
            get
            {
                return 4;
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
                if (bets.Count() != 4)
                {
                    return null;
                }
                else
                {
                    var list = new List<string>();
                    var qian = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var bai = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var shi = bets[2].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var ge = bets[3].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    list = (from q in qian
                            from b in bai
                            from s in shi
                            from g in ge
                            select string.Format("{0}{1}{2}{3}", q, b, s, g)).ToList();
                    return list;
                }
            }
        }
    }
}
