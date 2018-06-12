using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 后三/后三组选/组三复式 验证通过 2015/05/23 2016 01 18
    /// </summary>
    public class HouSanZ3fsCalculate : FiveSpecialDetailsCalculate
    {
        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = TotalBet(item, openResult);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return TotalBet(item, "");
        }

        #region 计算具体组合及注数
        private int TotalBet(BasicModel.LotteryBasic.BetDetail betDetail, string openResult)
        {
            var res = new List<string>();
            if (!string.IsNullOrEmpty(openResult))
            {
                var temp = openResult.Split(',');
                if (temp.Length == 5)
                    res = temp.Skip(2).Take(3).ToList();
                else
                    res = temp.ToList();
            }

            List<int> zusan = betDetail.BetContent.Select(c => Convert.ToInt32(c.ToString())).ToList();
            List<string> list = new List<string>();
            if (zusan.Count >= 2)
            {
                foreach (var item in zusan)
                {
                    var arr = zusan.Where(m => m != item);
                    var v = string.Format("{0}{0}", item);
                    foreach (var m in arr)
                    {
                        if (string.IsNullOrEmpty(openResult))
                            list.Add(string.Format("{0}{1}", v, m));
                        else
                        {
                            //判断中奖情况
                            // res里面3个值==item 和v
                            if ((res.Count(n => n == item.ToString()) == 2 && res.Count(n => n == m.ToString()) == 1))
                            {
                                list.Add(string.Format("{0}{1}", v, m));
                            }
                        }
                    }
                }
            }
            return list.Count;
        }
        #endregion

        public override string HtmlContentFormart(string betContent)
        {
            betContent = base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            if (betContent.Length < 2)
                return string.Empty;
            return betContent;

        }
    }
}
