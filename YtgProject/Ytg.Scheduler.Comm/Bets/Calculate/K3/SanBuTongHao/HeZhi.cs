using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.SanBuTongHao
{
    /// <summary>
    /// 和值
    /// </summary>
    public class HeZhi : BaseCalculate
    {
        
        /// <summary>
        /// 和值为6/15的奖金
        /// </summary>
        protected  decimal AMT_615 = 25;

        /// <summary>
        /// 和值为7/14的奖金
        /// </summary>
        protected  decimal AMT_714 = 16 ;

        /// <summary>
        /// 和值为9/12的奖金
        /// </summary>
        protected  decimal AMT_912 = 10 ;

        /// <summary>
        /// 和值为10/11的奖金
        /// </summary>
        protected  decimal AMT_1011 = 9 ;

        /// <summary>
        /// 和值为8/13的奖金
        /// </summary>
        protected  decimal AMT_813 = 12 ;

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var heZhi = openResult.Split(',').Sum(c => Convert.ToInt32(c));

            var sumAmt = 0m;
            var ctArray= item.BetContent.Split(',');
            foreach (var ct in ctArray)
            {
                int outHeZhi;
                if (!int.TryParse(ct, out outHeZhi) || outHeZhi !=heZhi)
                    continue;
                var amt = 0m;
                switch (heZhi)
                {
                   
                    case 6:
                    case 15:
                        amt = AMT_615;
                        break;
                    case 7:
                    case 14:
                        amt = AMT_714;
                        break;
                    case 9:
                    case 12:
                        amt = AMT_912;
                        break;
                    case 10:
                    case 11:
                        amt = AMT_1011;
                        break;
                    case 8:
                    case 13:
                        amt = AMT_813;
                        break;
                }
                sumAmt += amt;
            }
            if (sumAmt > 0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, sumAmt);
            }
            
        }
        public override string HtmlContentFormart(string betContent)
        {
            return VerificationSelectBetContent(betContent.Replace("&", ","), 6, 15, -1);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }
    }
}
