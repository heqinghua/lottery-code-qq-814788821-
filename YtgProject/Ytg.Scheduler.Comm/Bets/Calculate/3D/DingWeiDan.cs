using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate._3D
{
    /// <summary>
    /// 3d 定位胆
    /// </summary>
    public class DingWeiDan : BaseCalculate
    {
    
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
           {
               var res = ("0,0,"+openResult).Split(',');//得到每一位的投注数
               var pos = item.BetContent.Split('_')[1];//位置

               var temp = item.BetContent.Split('_')[0].Select(x => x.ToString());
               int winCount = 0;
               for (int i = 0; i < res.Length; i++)
               {
                   var curPos = i + 1;
                   if (string.IsNullOrEmpty(res[i]) || curPos.ToString() != pos)
                       continue;
                   //每一位开奖结果
                   if (temp.Contains(res[i]))
                   {
                       winCount++;
                   }
               }
               var stepAmt = 0m;
               if (winCount > 0)
               {
                   //中奖
                   item.IsMatch = true;
                   item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
               }

        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split('_')[0].Length;
        }

       

        private static int IsWin(string[] list, string openResult)
        {
            //所选位数上的号码与开奖的位数上的号码一致即为中奖
            int winCount = 0;
            var items = openResult.Split(',');

            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(list[i]))
                    continue;
                if (list[i].Contains(items[i])) { winCount++; }
            }
             return winCount;
        }

        public override string HtmlContentFormart(string betContent)
        {
            if (betContent.IndexOf("_") == -1)
                return string.Empty;
            string pos = betContent.Split('_')[1];
            var array = betContent.Split('_')[0].Select(x => x.ToString());
            if (array.Any(m => Convert.ToInt32(m) < 0 || Convert.ToInt32(m) > 9)) return string.Empty;
            return betContent ;
        }
    }
}
                                     