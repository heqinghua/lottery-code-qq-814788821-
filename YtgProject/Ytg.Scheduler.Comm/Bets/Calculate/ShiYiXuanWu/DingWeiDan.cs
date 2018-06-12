using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu
{
    /// <summary>
    /// 广东十一选五/定位胆/定位胆
    /// </summary>
    public class DingWeiDan : BaseCalculate
    {
      
       

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var isWin = IsWin(item, openResult);
            if (isWin)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }

        private bool IsWin(BasicModel.LotteryBasic.BetDetail item, string result)
        {
            var flag = false;
            var opens = result.Split(',').Take(3).ToList();
            var ay = item.BetContent.Split('_');
            var pos = ay[1];
            var content = ay[0].Split(',');

            for (var i = 0; i < opens.Count; i++)
            {
                var curPos = i + 1;
                if (curPos.ToString() != pos)
                    continue;
                if (content.Contains(opens[i]))
                    flag = true;
            }

            return flag;
        }


        public override string HtmlContentFormart(string betContent)
        {
            if (betContent.IndexOf("_") == -1)
                return string.Empty;
            var pos=betContent.Split('_')[1];
            var newContent= betContent.Split('_')[0];
            string ct= VerificationSelectBetContent11x5(newContent,false);
            return ct + "_" + pos;
        }



        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split('_')[0].Split(',').Length;
        }
    }
}
