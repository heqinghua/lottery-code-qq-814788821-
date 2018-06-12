using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErTHDX
{
    /// <summary>
    /// 标准选号 二同号单选 2016 01 13验证
    /// </summary>
    public class BiaoZhun : BaseCalculate
    {

        /// <summary>
        /// 二同号单选奖金 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT =100;

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = string.Join("", openResult.Replace(",", "").OrderBy(c => c));
            var betContentList = GetAllBets(item);
            bool isMath = betContentList.Any(c => string.Join("", c.Select(f => f.ToString()).OrderBy(d => d)) == openResult);
            if (isMath)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT);
            }

        }

        
        

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace('|', ',').Replace('&', ' ');
            betContent = VerificationSelectBetContentK3Many(betContent);
            //验证两组数据不能有相同的数字
            var array = betContent.Split(',');
            if (array.Length != 2)
                return string.Empty;
            if (array[0].Split(' ').Any(c => array[1].Split(' ').Contains(c)))
                return string.Empty;
            return betContent.Replace(" ", "");//去掉空格
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
                            select string.Format("{0}{0}{1}", s, g)).ToList();
                    return list;
                }
            }
        }
    }
}
