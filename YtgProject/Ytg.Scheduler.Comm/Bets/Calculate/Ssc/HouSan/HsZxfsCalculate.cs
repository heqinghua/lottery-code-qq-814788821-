using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三直选/直选复式 验证通过 2015/05/24 2016 01 18
    /// </summary>
    public class HsZxfsCalculate : ZhiXuanFushiDetailsCalculate
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
                openResult= openResult.Replace(",", "");
                string result = null;
                if(openResult.Length==5)
                    result = list.Find(m => m == openResult.Remove(0, 2));
                else
                    result = list.Find(m => m == openResult);
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
                if (bets.Count() != 3)
                {
                    return null;
                }
                else
                {
                    var list = new List<string>();
                    var bai = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var shi = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var ge = bets[2].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    list = (from b in bai
                            from s in shi
                            from g in ge
                            select string.Format("{0}{1}{2}", b, s, g)).ToList();
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
