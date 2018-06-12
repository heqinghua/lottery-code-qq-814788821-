using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.HouEr
{
    /// <summary>
    /// 后二/后二直选/直选复式 验证通过 2015/05/24  2016 01 18
    /// </summary>
    public class HeZxfsCalculate : ZhiXuanFushiDetailsCalculate
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
                openResult = openResult.Replace(",", "");
                string result;
                if (openResult.Length == 5)
                    result = list.Find(m => m == openResult.Remove(0, 3));
                else
                    result = list.Find(m => m == openResult.Remove(0, 1));

                if (result != null)
                {
                    item.IsMatch = true;
                    decimal stepAmt = 0;
                    item.WinMoney = TotalWinMoney(item, GetBaseAmt(item,ref stepAmt), stepAmt, 1);
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
                if (bets.Count() != 2)
                {
                    return null;
                }
                else
                {
                    var list = new List<string>();
                    var shi = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    var ge = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
                    list = (from s in shi
                            from g in ge
                            select string.Format("{0}{1}", s, g)).ToList();
                    return list;
                }
            }
        }

        protected override int GetLen
        {
            get
            {
                return 2;
            }
        }
    }
}
