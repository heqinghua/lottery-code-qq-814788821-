using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErBuTH
{
    /// <summary>
    /// 二不同号    1&2&3&4&5&6 2016 01 13
    /// </summary>
    public class BiaoZhun : BaseCalculate
    {

        /// <summary>
        /// 二同号单选奖金 默认7.5的返点
        /// </summary>
        protected readonly decimal AMT = 8 ;

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
           
            var betContentArray = item.BetContent.Select(t => t.ToString()).ToList();
            Combinations<string> c = new Combinations<string>(betContentArray, 2);
            var list = c._combinations;
            //开奖结果
            var temp = openResult.Replace(",","");
            var count = 0;
            if (list != null && list.Count > 0)
            {
                foreach (var x in list)
                {
                    if (temp.Contains(x[0]) && temp.Contains(x[1]))
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
            betContent= this.VerificationSelectBetContent(betContent.Replace("&", ","), 1, 6, 1);
            if (betContent.Split(',').Length < 2)
                return "";
            return betContent;
        }


        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return CombinationHelper.Cmn(item.BetContent.Replace(",", "").Length, 2);
        }
    }
}
