using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.SanBuTongHao
{
    /// <summary>
    ///三不同号
    /// </summary>
    public class BiaoZhun : BaseCalculate
    {
        /// <summary>
        /// 三不同号 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT = 50 ;

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {

            var betContentArray = item.BetContent.Replace(",","").Select(t => t.ToString()).ToList();
            Combinations<string> c = new Combinations<string>(betContentArray, 3);
            var list = c._combinations;
            //开奖结果
            var temp = openResult.Replace(",", "");
            var count = 0;
            if (list != null && list.Count > 0)
            {
                foreach (var x in list)
                {
                    if (temp.Contains(x[0]) && temp.Contains(x[1]) && temp.Contains(x[2]))
                    {
                        count++;
                        break;
                    }
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT); 
            }

        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace("&", ",");
            return VerificationSelectBetContent(betContent, 1, 6, 1,false);
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 3);
        }
    }
}
