using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 五星/五星直选/五星组合  验证通过 2015/05/23  2016 01 17
    /// </summary>
    public class WxZhCalculate : ZhiXuanFushiDetailsCalculate
    {
        /// <summary>
        /// 开奖的时候 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //得到所有可能
            var list = GetAllBets(item);
            var result = openResult.Replace(",", "");

            decimal _1Monery = 0m;
            decimal _2Monery = 0m;
            decimal _3Monery = 0m;
            decimal _4Monery = 0m;
            decimal _5Monery = 0m;

            list.ForEach(m =>
            {
                //需要判断中五位/四位/三位/二位/1位，奖金不一样
                decimal stepAmt = 0;

                if (m == result && _5Monery==0)//中五位，五星
                {
                    //2  item.BonusLevel == 1700 ? 170000 : 180000
                    _5Monery += TotalWinMoney(item, GetBaseAmtLstItem(5, item, ref stepAmt), stepAmt, 1);
                }
                if (m.Remove(0, 1) == result.Substring(1, 4) && _4Monery==0)//中四位，四星
                {
                    // item.BonusLevel == 1700 ? 17000 : 18000
                    _4Monery += TotalWinMoney(item, GetBaseAmtLstItem(4, item, ref stepAmt), stepAmt, 1);
                } if (m.Remove(0, 2) == result.Substring(2, 3) && _3Monery == 0)//中三位，三星
                {
                    // item.BonusLevel == 1700 ? 1700 : 1800
                    _3Monery += TotalWinMoney(item, GetBaseAmtLstItem(3, item, ref stepAmt), stepAmt, 1);
                }
                if (m.Remove(0, 3) == result.Substring(3, 2) && _2Monery==0)//中二位，二星
                {
                    // item.BonusLevel == 1700 ? 170 : 180
                    _2Monery += TotalWinMoney(item, GetBaseAmtLstItem(2, item, ref stepAmt), stepAmt, 1);
                }
                if (m.Remove(0, 4) == result.Substring(4, 1) && _1Monery==0)//中一位，一星
                {
                    //item.BonusLevel == 1700 ? 17 : 18
                    _1Monery += TotalWinMoney(item, GetBaseAmtLstItem(1, item, ref stepAmt), stepAmt, 1);
                }
            });
            item.WinMoney = _1Monery + _2Monery + _3Monery + _4Monery + _5Monery;
            if (item.WinMoney > 0) item.IsMatch = true;
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

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return base.TotalBetCount(item) * 5;
        }
    }
}
